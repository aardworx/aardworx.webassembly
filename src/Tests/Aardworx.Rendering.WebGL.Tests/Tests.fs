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

    let private framebufferClearReadbackFloat (ctx : TestCtx) =
        // Regression: ReadPixels' row-flip used to `:?> byte[]`, which throws
        // InvalidCastException for non-byte formats. The Aardvark.Dom pick
        // buffer is Rgba32f, so this silently broke picking.
        let size = V2i(4, 4)
        let signature =
            ctx.Runtime.CreateFramebufferSignature(
                [DefaultSemantic.Colors, TextureFormat.Rgba32f],
                samples = 1
            )
        let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba32f, levels = 1, samples = 1)
        try
            let fbo =
                ctx.Runtime.CreateFramebuffer(
                    signature,
                    [DefaultSemantic.Colors, color.GetOutputView()]
                )
            try
                let cv =
                    ClearValues.empty
                    |> ClearValues.color (C4f(0.25f, 0.5f, 0.75f, 1.0f))
                use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant cv)
                clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

                let img = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
                let pi = img :?> PixImage<float32>
                let d = pi.Volume.Data
                let approx (a : float32) (b : float32) = abs (a - b) < 0.001f
                assertTrue
                    (sprintf "first float pixel not (0.25,0.5,0.75,1.0): %f,%f,%f,%f" d.[0] d.[1] d.[2] d.[3])
                    (approx d.[0] 0.25f && approx d.[1] 0.5f && approx d.[2] 0.75f && approx d.[3] 1.0f)
            finally
                fbo.Dispose()
        finally
            color.Dispose()
            signature.Dispose()

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

    // ------------------------------------------------------------------
    // ReadPixels benchmark — for diagnosing the macOS Safari readback
    // perf hit. Fails the test if anything throws but otherwise just
    // surfaces wall-clock timings in the error field so the result row
    // shows them in the DOM and in the Playwright JSON.
    // ------------------------------------------------------------------

    let private readPixelsBench (ctx : TestCtx) =
        let signature =
            ctx.Runtime.CreateFramebufferSignature(
                [DefaultSemantic.Colors, TextureFormat.Rgba8],
                samples = 1
            )
        let target = V2i(256, 256)
        let color = ctx.Runtime.CreateTexture2D(target, TextureFormat.Rgba8, levels = 1, samples = 1)
        try
            let fbo =
                ctx.Runtime.CreateFramebuffer(
                    signature,
                    [DefaultSemantic.Colors, color.GetOutputView()]
                )
            try
                // Pre-fill so glReadPixels has real bytes (not undefined).
                let cv = ClearValues.empty |> ClearValues.color (C4f(0.5f, 0.25f, 0.75f, 1.0f))
                use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant cv)
                clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

                // Three buckets:
                //   (a) 1x1 picks         — direct path (≤ 4 KiB threshold)
                //   (b) 32x32 thumbnails  — direct path
                //   (c) 256x256 frames    — PBO path (cached scratch buffer)
                // Each block runs a small warm-up then N timed iterations.
                let bench (label : string) (size : V2i) (warmup : int) (iters : int) =
                    let sw = System.Diagnostics.Stopwatch()
                    for _ in 1 .. warmup do
                        let _ = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
                        ()
                    sw.Restart()
                    for _ in 1 .. iters do
                        let _ = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
                        ()
                    sw.Stop()
                    let totalMs = sw.Elapsed.TotalMilliseconds
                    let perMs = totalMs / float iters
                    sprintf "%s %dx%d × %d in %.1f ms (%.3f ms/call)" label size.X size.Y iters totalMs perMs

                let r1 = bench "1px"  (V2i(1, 1))     16  500
                let r2 = bench "32px" (V2i(32, 32))   8   200
                let r3 = bench "256px"(V2i(256, 256)) 2   30
                // Surface the numbers as a (skipped) result with no failure —
                // this way the test row in the DOM shows the timings even when
                // everything is healthy and there's nothing to assert against.
                TestRunner.pending (sprintf "%s | %s | %s" r1 r2 r3)
            finally
                fbo.Dispose()
        finally
            color.Dispose()
            signature.Dispose()

    // ------------------------------------------------------------------
    // Render-then-pick benchmark — rotates the teapot per frame, then does
    // a readback. Measures the realistic "pick after a real frame change"
    // cost (render + GPU sync + readback), not just the steady-state read
    // overhead the bare-readback bench captures.
    // ------------------------------------------------------------------

    let private renderPickBench (ctx : TestCtx) =
        let size = V2i(256, 256)
        let signature =
            ctx.Runtime.CreateFramebufferSignature(
                [DefaultSemantic.Colors, TextureFormat.Rgba8
                 DefaultSemantic.DepthStencil, TextureFormat.Depth24Stencil8],
                samples = 1
            )
        let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        let depth = ctx.Runtime.CreateTexture2D(size, TextureFormat.Depth24Stencil8, levels = 1, samples = 1)
        try
            let fbo =
                ctx.Runtime.CreateFramebuffer(
                    signature,
                    [DefaultSemantic.Colors, color.GetOutputView()
                     DefaultSemantic.DepthStencil, depth.GetOutputView()]
                )
            try
                let viewTrafo = CameraView.lookAt (V3d(2.5, 2.5, 1.5)) V3d.Zero V3d.OOI |> CameraView.viewTrafo
                let projTrafo = Frustum.perspective 60.0 0.1 100.0 (float size.X / float size.Y) |> Frustum.projTrafo

                // Rotation cval driven from the host loop — bumping it inside a
                // `transact` invalidates the model trafo and forces the render
                // task to actually issue draw calls (otherwise renderTask.Run is
                // a no-op once the scene is steady-state).
                let angle = cval 0.0
                let scene =
                    sg {
                        Sg.View viewTrafo
                        Sg.Proj projTrafo
                        Sg.Trafo (angle |> AVal.map Trafo3d.RotationZ)
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
                let renderObjects = scene.GetRenderObjects(TraversalState.empty ctx.Runtime)
                use renderTask = ctx.Runtime.CompileRender(signature, renderObjects)

                let runFrame () =
                    transact (fun () -> angle.Value <- angle.Value + 0.05)
                    clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)
                    renderTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)

                let bench (label : string) (warmup : int) (iters : int) (after : unit -> unit) =
                    let sw = System.Diagnostics.Stopwatch()
                    for _ in 1 .. warmup do
                        runFrame()
                        after()
                    sw.Restart()
                    for _ in 1 .. iters do
                        runFrame()
                        after()
                    sw.Stop()
                    let total = sw.Elapsed.TotalMilliseconds
                    sprintf "%s × %d in %.1f ms (%.3f ms/frame)" label iters total (total / float iters)

                // (a) render only — establishes the per-frame floor.
                let r1 = bench "render"        4 100 (fun () -> ())
                // (b) render + 1-pixel pick — the realistic hover-pick path.
                let r2 = bench "render+1pxpick" 4 100 (fun () ->
                    let _ = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i(128, 128), V2i(1, 1))
                    ())
                // (c) render + full-frame readback — worst-case (e.g. JPEG stream).
                let r3 = bench "render+256pxread" 2 30 (fun () ->
                    let _ = ctx.Runtime.ReadPixels(fbo, DefaultSemantic.Colors, V2i.Zero, size)
                    ())
                TestRunner.pending (sprintf "%s | %s | %s" r1 r2 r3)
            finally
                fbo.Dispose()
        finally
            depth.Dispose()
            color.Dispose()
            signature.Dispose()

    /// Build the test list. `state` carries the pre-fetched reference (if any)
    /// and whether the URL asked us to (re-)capture.
    // ------------------------------------------------------------------
    // tests for newly-implemented IRuntime members
    // ------------------------------------------------------------------

    let private clearTextureColor (ctx : TestCtx) =
        // Clear an Rgba8 texture directly (no signature/FBO wrapping by the
        // caller) and verify via Download that the pixels match.
        let size = V2i(8, 8)
        let tex = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        try
            // Pre-fill with garbage so we can detect a real clear.
            let pre = PixImage<byte>(Col.Format.RGBA, size)
            for i in 0 .. pre.Volume.Data.Length - 1 do pre.Volume.Data.[i] <- 0xFFuy
            ctx.Runtime.Upload(tex, pre :> PixImage)

            let cv =
                ClearValues.empty
                |> ClearValues.color (C4f(0.0f, 0.5f, 1.0f, 1.0f))
            ctx.Runtime.Clear(tex, cv)

            let dst = PixImage<byte>(Col.Format.RGBA, size)
            ctx.Runtime.Download(tex, dst :> PixImage)
            let d = dst.Volume.Data
            // 0.5f → 127/128, 1.0f → 255 — accept either rounding for the green channel.
            assertTrue
                (sprintf "first pixel not (0,~128,255,255): %d,%d,%d,%d" (int d.[0]) (int d.[1]) (int d.[2]) (int d.[3]))
                (d.[0] = 0uy && (d.[1] = 127uy || d.[1] = 128uy) && d.[2] = 255uy && d.[3] = 255uy)
        finally
            tex.Dispose()

    let private copyTextureSlicesLevels (ctx : TestCtx) =
        // Round-trip: upload → CopyTexture → download. Single mip / single
        // slice (the common case); the iteration in the impl is exercised by
        // looping over level=0..0 / slice=0..0 just like a multi-level call.
        let size = V2i(16, 16)
        let pi = PixImage<byte>(Col.Format.RGBA, size)
        for y in 0 .. size.Y - 1 do
            for x in 0 .. size.X - 1 do
                let idx = (y * size.X + x) * 4
                pi.Volume.Data.[idx + 0] <- byte (x * 16)
                pi.Volume.Data.[idx + 1] <- byte (y * 16)
                pi.Volume.Data.[idx + 2] <- byte ((x ^^^ y) * 8)
                pi.Volume.Data.[idx + 3] <- 255uy
        let src = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        let dst = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
        try
            ctx.Runtime.Upload(src, pi :> PixImage)
            ctx.Runtime.Copy(src, 0, 0, dst, 0, 0, slices = 1, levels = 1)
            let got = PixImage<byte>(Col.Format.RGBA, size)
            ctx.Runtime.Download(dst, got :> PixImage)
            assertTrue
                (sprintf "copy mismatch (first pixel: %A → %A)" pi.Volume.Data.[..3] got.Volume.Data.[..3])
                (bytesEqual pi.Volume.Data got.Volume.Data)
        finally
            src.Dispose()
            dst.Dispose()

    let private downloadAsyncBuffer (ctx : TestCtx) =
        // DownloadAsync should produce data identical to synchronous Download
        // and return a thunk that, when invoked, is a no-op (data is already
        // there). On WebGL the work is done eagerly, so the returned function
        // is essentially a sync receipt.
        let data = Array.init 192 (fun i -> byte ((i * 13 + 5) &&& 0xFF))
        let buf = ctx.Runtime.CreateBuffer(uint64 data.Length, BufferUsage.ReadWrite, BufferStorage.Device)
        try
            uploadBytes ctx.Runtime buf data
            let arr = Array.zeroCreate<byte> data.Length
            let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
            try
                let waiter = ctx.Runtime.DownloadAsync(buf, 0UL, gc.AddrOfPinnedObject(), uint64 data.Length)
                waiter ()
            finally
                gc.Free()
            assertTrue "DownloadAsync data mismatch" (bytesEqual data arr)
        finally
            buf.Dispose()

    /// Poll a query's TryGetResult while yielding to the browser event loop
    /// between attempts. WebGL is single-threaded JS — a busy spin would
    /// deadlock the page since the GPU's "result available" status only
    /// becomes visible after the event loop tick.
    let private pollQuery<'a> (q : IQuery<unit, 'a>) (timeoutMs : int) : System.Threading.Tasks.Task<'a option> =
        task {
            let sw = System.Diagnostics.Stopwatch.StartNew()
            let mutable result = q.TryGetResult()
            while result.IsNone && sw.ElapsedMilliseconds < int64 timeoutMs do
                do! System.Threading.Tasks.Task.Delay(5)
                result <- q.TryGetResult()
            return result
        }

    let private occlusionQueryRoundtrip (ctx : TestCtx) =
        task {
            // Render a real teapot (same setup as the reference render) so the
            // fragment shader actually runs. glClear bypasses the fragment
            // stage, so it would count as 0 samples — useless as a test.
            // After the draw, an any-samples-passed query must return > 0.
            // Then verify an empty Begin/End reports 0 (the trivial case).
            use q = ctx.Runtime.CreateOcclusionQuery(precise = false)

            let size = V2i(128, 128)
            let signature =
                ctx.Runtime.CreateFramebufferSignature(
                    [DefaultSemantic.Colors, TextureFormat.Rgba8
                     DefaultSemantic.DepthStencil, TextureFormat.Depth24Stencil8],
                    samples = 1)
            let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
            let depth = ctx.Runtime.CreateTexture2D(size, TextureFormat.Depth24Stencil8, levels = 1, samples = 1)
            try
                let fbo =
                    ctx.Runtime.CreateFramebuffer(
                        signature,
                        [DefaultSemantic.Colors, color.GetOutputView()
                         DefaultSemantic.DepthStencil, depth.GetOutputView()])
                try
                    let viewTrafo = CameraView.lookAt (V3d(2.5, 2.5, 1.5)) V3d.Zero V3d.OOI |> CameraView.viewTrafo
                    let projTrafo =
                        Frustum.perspective 60.0 0.1 100.0 (float size.X / float size.Y)
                        |> Frustum.projTrafo
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
                        |> ClearValues.color (C4f(0.0f, 0.0f, 0.0f, 1.0f))
                        |> ClearValues.depth 1.0
                        |> ClearValues.stencil 0
                    use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant clear)
                    clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)
                    let renderObjects = scene.GetRenderObjects(TraversalState.empty ctx.Runtime)
                    use renderTask = ctx.Runtime.CompileRender(signature, renderObjects)

                    // Draw inside Begin/End → fragment shader runs → samples > 0.
                    q.Begin()
                    renderTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)
                    q.End()
                    let! drawn = pollQuery q 5000
                    match drawn with
                    | None -> failwith "occlusion query timed out (5s) waiting for teapot draw"
                    | Some s ->
                        assertTrue
                            (sprintf "occlusion query reported 0 samples for a teapot draw — query is not counting fragments")
                            (s > 0UL)

                    // Empty Begin/End → no draws → 0 samples.
                    q.Reset()
                    q.Begin()
                    q.End()
                    let! empty = pollQuery q 5000
                    match empty with
                    | None -> failwith "occlusion query timed out (5s) waiting for empty Begin/End"
                    | Some s ->
                        assertTrue
                            (sprintf "occlusion query reported %d samples for empty Begin/End (expected 0)" s)
                            (s = 0UL)
                finally
                    fbo.Dispose()
            finally
                depth.Dispose()
                color.Dispose()
                signature.Dispose()
        }

    let private timeQueryRoundtrip (ctx : TestCtx) =
        task {
            // Skip silently if the GPU lacks the WebGL2 timer extension —
            // common on iOS Safari and some Linux drivers.
            let device = (ctx.Runtime :?> Aardworx.Rendering.WebGL.Runtime).Device
            if not device.Info.Features.TimerQuery then
                ()
            else
                use q = ctx.Runtime.CreateTimeQuery()
                q.Begin()
                let size = V2i(32, 32)
                let signature =
                    ctx.Runtime.CreateFramebufferSignature(
                        [DefaultSemantic.Colors, TextureFormat.Rgba8], samples = 1)
                let color = ctx.Runtime.CreateTexture2D(size, TextureFormat.Rgba8, levels = 1, samples = 1)
                try
                    let fbo = ctx.Runtime.CreateFramebuffer(signature, [DefaultSemantic.Colors, color.GetOutputView()])
                    try
                        let cv = ClearValues.empty |> ClearValues.color (C4f(1.0f, 0.0f, 0.0f, 1.0f))
                        use clearTask = ctx.Runtime.CompileClear(signature, AVal.constant cv)
                        clearTask.Run(AdaptiveToken.Top, RenderToken.Empty, OutputDescription.ofFramebuffer fbo)
                    finally fbo.Dispose()
                finally
                    color.Dispose()
                    signature.Dispose()
                q.End()
                let! elapsed = pollQuery q 5000
                match elapsed with
                | None -> failwith "time query timed out (5s) waiting for result"
                | Some t ->
                    assertTrue
                        (sprintf "time query returned negative elapsed time: %A" t)
                        (t.TotalNanoseconds >= 0L)
        }

    let mkAll (state : RefState) : (string * (TestCtx -> unit)) list =
        [
            "buffer roundtrip", bufferRoundtrip
            "buffer copy", bufferCopy
            "buffer downloadAsync", downloadAsyncBuffer
            "texture upload/readback", textureUploadReadback
            "texture copy (slice/level)", copyTextureSlicesLevels
            "texture clear (color)", clearTextureColor
            "framebuffer clear+readback", framebufferClearReadback
            "framebuffer clear+readback (Rgba32f)", framebufferClearReadbackFloat
            "teapot reference render", mkTeapotTest state
            "readpixels benchmark", readPixelsBench
            "render+pick benchmark", renderPickBench
        ]

    /// Backwards-compatible default (no reference fetched) — auto-bootstraps.
    let all : (string * (TestCtx -> unit)) list =
        mkAll { Reference = None; CaptureRequested = false }

    /// Async-shaped variant of `mkAll`. Lifts the sync rendering tests via
    /// `TestRunner.liftSync` and appends the genuinely-async query tests
    /// (which need to yield to the browser between TryGetResult polls).
    let mkAllAsync (state : RefState) : (string * (TestCtx -> System.Threading.Tasks.Task<unit>)) list =
        let sync = mkAll state |> List.map (fun (name, body) -> name, TestRunner.liftSync body)
        let asyncTests : (string * (TestCtx -> System.Threading.Tasks.Task<unit>)) list =
            [
                "occlusion query", occlusionQueryRoundtrip
                "time query", timeQueryRoundtrip
            ]
        sync @ asyncTests
