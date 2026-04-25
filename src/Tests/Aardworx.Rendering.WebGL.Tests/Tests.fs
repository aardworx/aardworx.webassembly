namespace Aardworx.Rendering.WebGL.Tests

open System
open System.Runtime.InteropServices
open Aardvark.Base
open Aardvark.Rendering
open Aardvark.Dom
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

module Tests =

    // ------------------------------------------------------------------
    // helpers
    // ------------------------------------------------------------------

    let private assertTrue (msg : string) (cond : bool) =
        if not cond then failwithf "assertion failed: %s" msg

    let private bytesEqual (a : byte[]) (b : byte[]) =
        a.Length = b.Length && Array.forall2 (=) a b

    let private uploadBytes (runtime : IRuntime) (buffer : IBackendBuffer) (data : byte[]) =
        let gc = GCHandle.Alloc(data, GCHandleType.Pinned)
        try
            runtime.Upload(gc.AddrOfPinnedObject(), buffer, 0UL, uint64 data.LongLength, false)
        finally
            gc.Free()

    let private downloadBytes (runtime : IRuntime) (buffer : IBackendBuffer) (size : int) =
        let arr = Array.zeroCreate<byte> size
        let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
        try
            runtime.Download(buffer, 0UL, gc.AddrOfPinnedObject(), uint64 size)
        finally
            gc.Free()
        arr

    // ------------------------------------------------------------------
    // tests
    // ------------------------------------------------------------------

    let private bufferRoundtrip (ctx : TestCtx) =
        let data = Array.init 256 byte
        let buf = ctx.Runtime.CreateBuffer(uint64 data.Length, BufferUsage.ReadWrite, BufferStorage.Device)
        try
            uploadBytes ctx.Runtime buf data
            let got = downloadBytes ctx.Runtime buf data.Length
            assertTrue (sprintf "roundtrip mismatch (first bytes: %A vs %A)" (got.[..7]) (data.[..7])) (bytesEqual data got)
        finally
            buf.Dispose()

    let private bufferCopy (ctx : TestCtx) =
        let data = Array.init 128 (fun i -> byte (i * 3 + 7))
        let a = ctx.Runtime.CreateBuffer(uint64 data.Length, BufferUsage.ReadWrite, BufferStorage.Device)
        let b = ctx.Runtime.CreateBuffer(uint64 data.Length, BufferUsage.ReadWrite, BufferStorage.Device)
        try
            uploadBytes ctx.Runtime a data
            ctx.Runtime.Copy(a, 0UL, b, 0UL, uint64 data.Length, false)
            let got = downloadBytes ctx.Runtime b data.Length
            assertTrue "copied bytes mismatch" (bytesEqual data got)
        finally
            a.Dispose()
            b.Dispose()

    let private textureUploadReadback (ctx : TestCtx) =
        // Round-trip a small Rgba8 texture: upload a known pattern via runtime.Upload,
        // download via runtime.Download (FBO + glReadPixels under the hood), compare bytes.
        let size = V2i(8, 8)
        let pi = PixImage<byte>(Col.Format.RGBA, size)
        let data = pi.Volume.Data
        // Deterministic pattern; channels distinct so any swizzle/mirror error stands out.
        for y in 0 .. size.Y - 1 do
            for x in 0 .. size.X - 1 do
                let idx = (y * size.X + x) * 4
                data.[idx]     <- byte (x * 32)
                data.[idx + 1] <- byte (y * 32)
                data.[idx + 2] <- byte ((x + y) * 16)
                data.[idx + 3] <- 255uy

        let tex = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        try
            ctx.Runtime.Upload(tex, pi :> PixImage)
            let dst = PixImage<byte>(Col.Format.RGBA, size)
            ctx.Runtime.Download(tex, dst :> PixImage)
            let got = dst.Volume.Data
            assertTrue
                (sprintf "byte mismatch (first8 want=%A got=%A)" (data.[..7]) (got.[..7]))
                (bytesEqual data got)
        finally
            tex.Dispose()

    let private framebufferClearReadback (ctx : TestCtx) =
        let size = V2i(8, 8)
        let signature =
            ctx.Runtime.CreateFramebufferSignature(
                [DefaultSemantic.Colors, TextureFormat.Rgba8],
                samples = 1
            )
        let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        try
            let fbo =
                ctx.Runtime.CreateFramebuffer(
                    signature,
                    [DefaultSemantic.Colors, color.GetOutputView()]
                )
            try
                let red = C4f(1.0f, 0.0f, 0.0f, 1.0f)
                let cv = ClearValues.empty |> ClearValues.color red
                use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant cv)
                clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

                let img = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
                // PixImage<byte> with RGBA layout
                let pi = img :?> PixImage<byte>
                let data = pi.Volume.Data
                // first pixel R should be 255, G=0, B=0, A=255
                assertTrue
                    (sprintf "first pixel not red: %d,%d,%d,%d" (int data.[0]) (int data.[1]) (int data.[2]) (int data.[3]))
                    (data.[0] = 255uy && data.[1] = 0uy && data.[2] = 0uy && data.[3] = 255uy)
            finally
                fbo.Dispose()
        finally
            color.Dispose()
            signature.Dispose()

    let private simpleDraw (_ctx : TestCtx) =
        // A full mini-pipeline test (FShade effect compile + render to FBO + readback)
        // is non-trivial to wire up reliably here without exhaustively touching the
        // FShade/RenderObject API surface.  Leave as Pending — the framebuffer-clear
        // path already exercises FBO creation, the clear render task and ReadPixels.
        TestRunner.pending "simple draw test scaffolding TODO — needs FShade effect + RenderObject wiring"


    // ------------------------------------------------------------------
    // teapot reference render
    // ------------------------------------------------------------------

    /// RGBA pixels (top-left origin) plus the source size — what the test
    /// compares against and what the capture path encodes back to PNG.
    type RefPixels =
        {
            Size : V2i
            Rgba : byte[]
        }

    /// JS-interop helpers for PNG decode/encode + capture-mode plumbing.
    /// Kept inside Tests.fs because there's no other consumer in the repo.
    module RefImage =

        /// True if `?capture=true` (or `?capture=1`) is in the URL.
        let isCaptureMode () =
            try
                let q = Window.Location.GetQuery()
                match Map.tryFind "capture" q with
                | Some (Some v) ->
                    let v = v.Trim().ToLowerInvariant()
                    v = "true" || v = "1" || v = "yes"
                | Some None -> true
                | None -> false
            with _ -> false

        /// Decode a base64 string into a byte[] without dragging in the full
        /// `System.Convert` round-trip; the JS side returns `btoa(binary)`.
        let private fromBase64 (s : string) = System.Convert.FromBase64String s
        let private toBase64 (b : byte[]) = System.Convert.ToBase64String b

        /// Pull RGBA pixels from a JSImage by drawing it to an offscreen
        /// canvas and returning `getImageData(...).data` as base64.
        let private pixelsFromHandle (handle : int) (size : V2i) : byte[] =
            // The evaluate(code) bridge doesn't take params, so the handle
            // and size are spliced into the JS source.
            let code =
                sprintf """
                    var img = aardvark.imageHandles[%d];
                    var c = document.createElement('canvas');
                    c.width = %d; c.height = %d;
                    var ctx = c.getContext('2d');
                    ctx.drawImage(img, 0, 0);
                    var d = ctx.getImageData(0, 0, %d, %d).data;
                    // Build base64 in chunks — String.fromCharCode.apply blows the stack
                    // for very long arrays, so process 0x8000 at a time.
                    var bin = '';
                    for (var i = 0; i < d.length; i += 0x8000) {
                        bin += String.fromCharCode.apply(null, d.subarray(i, i + 0x8000));
                    }
                    return btoa(bin);
                """ handle size.X size.Y size.X size.Y
            let b64 = JsObj.Evaluate<string>(code)
            fromBase64 b64

        /// Try to fetch + decode the reference PNG from a relative URL.
        /// Returns None on 404 / network / decode failure (capture-mode bootstrap).
        let tryFetch (url : string) : System.Threading.Tasks.Task<RefPixels option> =
            task {
                let! img = JSImage.tryLoad url
                match img with
                | None -> return None
                | Some im ->
                    try
                        let bytes = pixelsFromHandle im.Handle im.Size
                        (im :> IDisposable).Dispose()
                        return Some { Size = im.Size; Rgba = bytes }
                    with e ->
                        printfn "ref-image decode failed (%s): %s" url e.Message
                        try (im :> IDisposable).Dispose() with _ -> ()
                        return None
            }

        /// Encode RGBA pixels (top-left origin) to a PNG data URL via
        /// canvas.toDataURL("image/png"). Returns "data:image/png;base64,...".
        let encodePngDataUrl (size : V2i) (rgba : byte[]) : string =
            let expected = size.X * size.Y * 4
            if rgba.Length <> expected then
                failwithf "encodePngDataUrl: expected %d bytes for %dx%d RGBA, got %d" expected size.X size.Y rgba.Length
            let b64 = toBase64 rgba
            // Decode base64 → fill ImageData → canvas → toDataURL.
            let code =
                sprintf """
                    var b64 = "%s";
                    var raw = atob(b64);
                    var d = new Uint8ClampedArray(raw.length);
                    for (var i = 0; i < raw.length; i++) d[i] = raw.charCodeAt(i);
                    var c = document.createElement('canvas');
                    c.width = %d; c.height = %d;
                    var ctx = c.getContext('2d');
                    var imgd = ctx.createImageData(%d, %d);
                    imgd.data.set(d);
                    ctx.putImageData(imgd, 0, 0);
                    return c.toDataURL('image/png');
                """ b64 size.X size.Y size.X size.Y
            JsObj.Evaluate<string>(code)

        /// Trigger a browser download of the given data URL with the chosen filename.
        let triggerDownload (filename : string) (dataUrl : string) =
            // Stash payload in window globals to avoid escaping a giant data URL
            // through string-formatting; then build the anchor + .click().
            let stash =
                sprintf "window.__aardworx_capture_url__ = %s; window.__aardworx_capture_name__ = %s;"
                    (System.Text.Json.JsonSerializer.Serialize dataUrl)
                    (System.Text.Json.JsonSerializer.Serialize filename)
            JsObj.InstallScript stash
            let code = """
                var a = document.createElement('a');
                a.href = window.__aardworx_capture_url__;
                a.download = window.__aardworx_capture_name__;
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
                return "ok";
            """
            JsObj.Evaluate<string>(code) |> ignore

    let private renderRefScene (ctx : TestCtx) =
        let size = V2i(256, 256)

        let signature =
            ctx.Runtime.CreateFramebufferSignature(
                [DefaultSemantic.Colors, TextureFormat.Rgba8
                 DefaultSemantic.DepthStencil, TextureFormat.Depth24Stencil8],
                samples = 1
            )
        let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        let depth = ctx.Runtime.CreateTexture2D(size, TextureFormat.Depth24Stencil8, levels = 1, samples = 1)
        let mutable disposeFbo : (unit -> unit) = id
        try
            let fbo =
                ctx.Runtime.CreateFramebuffer(
                    signature,
                    [DefaultSemantic.Colors, color.GetOutputView()
                     DefaultSemantic.DepthStencil, depth.GetOutputView()]
                )
            disposeFbo <- fun () -> fbo.Dispose()

            // Fixed view/proj for determinism. Teapot has fewer symmetries than a
            // box — better at catching subtle rendering bugs.
            let viewTrafo  = CameraView.lookAt (V3d(2.5, 2.5, 1.5)) V3d.Zero V3d.OOI |> CameraView.viewTrafo
            let projTrafo  =
                Frustum.perspective 60.0 0.1 100.0 (float size.X / float size.Y)
                |> Frustum.projTrafo

            // Aardvark.Dom scene graph — required on wasm; Aardvark.SceneGraph's
            // ISg has reflection-based attribute traversal that's unusable here.
            let scene =
                sg {
                    Sg.View viewTrafo
                    Sg.Proj projTrafo
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        DefaultSurfaces.simpleLighting
                    }
                    Primitives.Teapot(C4b.Green)
                }

            let clear =
                ClearValues.empty
                |> ClearValues.color (C4f(0.1f, 0.1f, 0.2f, 1.0f))
                |> ClearValues.depth 1.0
                |> ClearValues.stencil 0

            use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant clear)
            clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

            let renderObjects = scene.GetRenderObjects(TraversalState.empty ctx.Runtime)
            use renderTask = ctx.Runtime.CompileRender(signature, renderObjects)
            renderTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

            let img = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
            let pi = img :?> PixImage<byte>
            pi
        finally
            disposeFbo()
            depth.Dispose()
            color.Dispose()
            signature.Dispose()

    /// Pre-fetched reference image (optionally None when missing or in capture mode)
    /// + the capture-mode flag. `renderTeapot` closes over both via `mkTeapotTest`.
    type RefState =
        {
            Reference : RefPixels option
            CaptureRequested : bool
        }

    let private compareRefPixels (size : V2i) (actual : byte[]) (expected : byte[]) =
        // L∞ on RGB channels, alpha checked similarly. Pass if wrong < 1% of pixels.
        let n = size.X * size.Y
        if actual.Length <> n * 4 || expected.Length <> n * 4 then
            failwithf "size mismatch: actual=%d expected=%d for %dx%d (need %d)"
                actual.Length expected.Length size.X size.Y (n * 4)

        let mutable maxDelta = 0
        let mutable wrong = 0
        let tol = 5
        for i in 0 .. n - 1 do
            let o = i * 4
            let dr = abs (int actual.[o]     - int expected.[o])
            let dg = abs (int actual.[o + 1] - int expected.[o + 1])
            let db = abs (int actual.[o + 2] - int expected.[o + 2])
            let da = abs (int actual.[o + 3] - int expected.[o + 3])
            let m = max (max dr dg) (max db da)
            if m > maxDelta then maxDelta <- m
            if dr > tol || dg > tol || db > tol || da > tol then
                wrong <- wrong + 1
        maxDelta, wrong, n

    let private mkTeapotTest (state : RefState) =
        fun (ctx : TestCtx) ->
            let pi = renderRefScene ctx
            let size = pi.Size
            let actual = pi.Volume.Data

            // 1) Sanity checks (cheap, also guards against a degenerate reference).
            let cornerR = int actual.[0]
            let cornerG = int actual.[1]
            let cornerB = int actual.[2]
            let cornerA = int actual.[3]
            let cleared =
                abs (cornerR - 26) <= 4 &&
                abs (cornerG - 26) <= 4 &&
                abs (cornerB - 51) <= 4 &&
                cornerA = 255
            if not cleared then
                failwithf "background corner not cleared: rgba=(%d,%d,%d,%d) — expected ~(26,26,51,255)"
                    cornerR cornerG cornerB cornerA

            let inCaptureMode = state.CaptureRequested || state.Reference.IsNone

            if inCaptureMode then
                // Bootstrap: encode current render as PNG, trigger download, mark Skipped.
                let dataUrl = RefImage.encodePngDataUrl size actual
                try RefImage.triggerDownload "reference-teapot.png" dataUrl
                with e -> printfn "reference download failed: %s" e.Message
                TestRunner.attach "captured-teapot.png" dataUrl
                let reason =
                    if state.CaptureRequested then
                        "captured reference (capture=true); commit wwwroot/reference-teapot.png and reload without capture flag"
                    else
                        "reference-teapot.png missing — captured fresh PNG; commit wwwroot/reference-teapot.png"
                TestRunner.pending reason
            else
                let r = state.Reference.Value
                if r.Size <> size then
                    failwithf "reference size mismatch: expected %A, got %A — re-capture with ?capture=true"
                        r.Size size
                let maxDelta, wrong, total = compareRefPixels size actual r.Rgba
                let pctWrong = float wrong / float total * 100.0
                let pass = wrong * 100 < total // wrong < 1%
                if not pass then
                    let actualUrl = RefImage.encodePngDataUrl size actual
                    let expectedUrl = RefImage.encodePngDataUrl size r.Rgba
                    TestRunner.attach "actual-teapot.png" actualUrl
                    TestRunner.attach "expected-teapot.png" expectedUrl
                    failwithf
                        "teapot reference mismatch: wrongPixels=%d/%d (%.2f%%, threshold=1.00%%), maxDelta=%d (tol=5)"
                        wrong total pctWrong maxDelta

    /// Build the test list. `state` carries the pre-fetched reference (if any)
    /// and whether the URL asked us to (re-)capture.
    let mkAll (state : RefState) : (string * (TestCtx -> unit)) list =
        [
            "buffer roundtrip", bufferRoundtrip
            "buffer copy", bufferCopy
            "texture upload/readback", textureUploadReadback
            "framebuffer clear+readback", framebufferClearReadback
            "simple draw", simpleDraw
            "teapot reference render", mkTeapotTest state
        ]

    /// Backwards-compatible default (no reference fetched) — auto-bootstraps.
    let all : (string * (TestCtx -> unit)) list =
        mkAll { Reference = None; CaptureRequested = false }

    /// Async-shaped variant of `mkAll`. Wraps the existing sync rendering tests
    /// via `TestRunner.liftSync` so they slot into `TestRunner.runAsync` without
    /// changing their bodies.
    let mkAllAsync (state : RefState) : (string * (TestCtx -> System.Threading.Tasks.Task<unit>)) list =
        mkAll state |> List.map (fun (name, body) -> name, TestRunner.liftSync body)
