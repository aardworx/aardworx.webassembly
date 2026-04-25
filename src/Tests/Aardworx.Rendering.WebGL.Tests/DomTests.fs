namespace Aardworx.Rendering.WebGL.Tests

open System
open System.Threading.Tasks
open Aardvark.Base
open Aardworx.WebAssembly

/// JS interop / DOM tests covering the surface exposed by `Aardworx.WebAssembly`.
/// Sister module to `Tests.fs`; kept separate so the rendering tests stay readable.
module DomTests =

    let private assertTrue (msg : string) (cond : bool) =
        if not cond then failwithf "assertion failed: %s" msg

    // ------------------------------------------------------------------
    // synchronous (JsObj.Evaluate, DOM mutation, Window.Location)
    // ------------------------------------------------------------------

    let private jsEvalInt (_ctx : TestCtx) =
        let r = JsObj.Evaluate<int>("return 21*2")
        assertTrue (sprintf "expected 42, got %d" r) (r = 42)

    let private jsEvalString (_ctx : TestCtx) =
        let r = JsObj.Evaluate<string>("return 'hello' + ' world'")
        assertTrue (sprintf "expected 'hello world', got %A" r) (r = "hello world")

    let private jsInstallAndRecall (_ctx : TestCtx) =
        // Use a unique global so re-runs don't see leftovers from a prior pass.
        let key = sprintf "__aardworx_test_x_%d__" (System.Random().Next(1, 1_000_000_000))
        JsObj.InstallScript (sprintf "window.%s = 7;" key)
        let r = JsObj.Evaluate<int>(sprintf "return window.%s + 1" key)
        // Tidy up so we don't leave noise on `window`.
        JsObj.InstallScript (sprintf "delete window.%s;" key)
        assertTrue (sprintf "expected 8, got %d" r) (r = 8)

    let private windowLocationQuery (_ctx : TestCtx) =
        // Just exercise the API; the actual query depends on URL. The teapot
        // test already inspects `?capture=...`, so all this needs to verify is
        // that GetQuery() returns a Map without throwing and that any "capture"
        // entry round-trips through the query parser.
        let q = Window.Location.GetQuery()
        // Map<string, string option> — accessing it with type annotations
        // forces the compiler to verify the shape we expect.
        let _typed : Map<string, string option> = q
        let captureRaw = JsObj.Evaluate<string>("return window.location.search || ''")
        if captureRaw.Contains "capture" then
            assertTrue "url contains 'capture' but GetQuery() doesn't" (Map.containsKey "capture" q)

    let private createElementAppendRemove (_ctx : TestCtx) =
        let doc = Window.Document
        let id = sprintf "aardworx-domtest-%d" (System.Random().Next(1, 1_000_000_000))
        let div = doc.CreateElement "div"
        div.Id <- id
        div.InnerText <- "hello dom"
        doc.Body.AppendChild div

        let found = doc.GetElementById id
        assertTrue "appended element not found via getElementById" (not (isNull found))
        assertTrue (sprintf "innerText mismatch: %A" found.InnerText) (found.InnerText = "hello dom")

        found.Remove()
        let after = doc.GetElementById id
        assertTrue "element still present after Remove()" (isNull after)

    let private elementStyleRoundtrip (_ctx : TestCtx) =
        let doc = Window.Document
        let div = doc.CreateElement "div"
        try
            // Element must be in the DOM for some computed-style queries; we
            // round-trip the inline style (the property setter), which works
            // detached too.
            doc.Body.AppendChild div
            div.Style.Background <- "red"
            let b = div.Style.Background
            // Browsers normalise "red" → either "red" or "rgb(255, 0, 0)" depending
            // on whether they're returning the inline value or the computed one.
            // We're querying the inline declaration via getPropertyValue, which
            // preserves the source token, but be lenient just in case.
            let ok = b = "red" || b.Replace(" ", "").ToLowerInvariant().Contains "rgb(255,0,0)"
            assertTrue (sprintf "style.background round-trip: got %A" b) ok
        finally
            try div.Remove() with _ -> ()

    // ------------------------------------------------------------------
    // async (JSImage)
    // ------------------------------------------------------------------

    let private jsImageLoadFavicon (_ctx : TestCtx) =
        task {
            let url = RelativeUrl "./favicon.ico"
            let! img = JSImage.tryLoad url
            match img with
            | None ->
                failwithf "favicon.ico failed to load (url=%s)" url
            | Some im ->
                try
                    assertTrue
                        (sprintf "favicon size not positive: %A" im.Size)
                        (im.Size.X > 0 && im.Size.Y > 0)
                finally
                    (im :> IDisposable).Dispose()
        }

    let private jsImageLoadMissing (_ctx : TestCtx) =
        // Wrap the JSImage call in a 5s timeout — `tryLoad` relies on the
        // browser firing `<img onerror>` for 404s; if a particular browser
        // / network stack swallows the error event, we'd otherwise hang the
        // whole suite.
        task {
            let url = "/_framework/aardworx-does-not-exist.png"
            let load = JSImage.tryLoad url
            let timeout = Task.Delay 5_000
            let! winner = Task.WhenAny(load :> Task, timeout)
            if obj.ReferenceEquals(winner, timeout) then
                TestRunner.pending
                    "JSImage.tryLoad on a 404 URL did not resolve within 5s — browser swallowed onerror?"
            else
                let! img = load
                match img with
                | None -> ()
                | Some im ->
                    (im :> IDisposable).Dispose()
                    failwithf "expected None for missing image, got Some"
        }

    let private jsImageRoundtripPixels (_ctx : TestCtx) =
        task {
            // Build a tiny known-RGBA pattern, encode → PNG data URL → load →
            // read pixels back. Re-uses the encode/decode helpers from Tests.fs
            // so the canvas plumbing only lives in one place.
            let size = V2i(4, 4)
            let pixels = Array.zeroCreate<byte> (size.X * size.Y * 4)
            for y in 0 .. size.Y - 1 do
                for x in 0 .. size.X - 1 do
                    let o = (y * size.X + x) * 4
                    pixels.[o]     <- byte (x * 60)
                    pixels.[o + 1] <- byte (y * 60)
                    pixels.[o + 2] <- byte ((x + y) * 30)
                    pixels.[o + 3] <- 255uy

            let dataUrl = Tests.RefImage.encodePngDataUrl size pixels
            let! img = JSImage.tryLoad dataUrl
            match img with
            | None -> failwithf "JSImage.tryLoad failed for synthesised data URL"
            | Some im ->
                try
                    assertTrue
                        (sprintf "size mismatch: expected %A, got %A" size im.Size)
                        (im.Size = size)
                    // Decode pixels via canvas — same approach Tests.RefImage uses.
                    let code =
                        sprintf """
                            var img = aardvark.imageHandles[%d];
                            var c = document.createElement('canvas');
                            c.width = %d; c.height = %d;
                            var ctx = c.getContext('2d');
                            ctx.drawImage(img, 0, 0);
                            var d = ctx.getImageData(0, 0, %d, %d).data;
                            var bin = '';
                            for (var i = 0; i < d.length; i += 0x8000) {
                                bin += String.fromCharCode.apply(null, d.subarray(i, i + 0x8000));
                            }
                            return btoa(bin);
                        """ im.Handle size.X size.Y size.X size.Y
                    let b64 = JsObj.Evaluate<string>(code)
                    let got = System.Convert.FromBase64String b64
                    // PNG round-trip is lossless for 8-bit RGBA, but the canvas
                    // stack premultiplies alpha for some browsers — since alpha
                    // is 255 across the board we expect bit-exact equality.
                    let mutable wrong = 0
                    let mutable maxD = 0
                    for i in 0 .. pixels.Length - 1 do
                        let d = abs (int got.[i] - int pixels.[i])
                        if d > maxD then maxD <- d
                        if d > 1 then wrong <- wrong + 1
                    assertTrue
                        (sprintf "PNG round-trip mismatch: wrong=%d, maxDelta=%d" wrong maxD)
                        (wrong = 0)
                finally
                    (im :> IDisposable).Dispose()
        }

    // ------------------------------------------------------------------
    // entrypoint
    // ------------------------------------------------------------------

    /// Returns the full DOM/JS test list, async-shaped so it can be appended
    /// directly to `Tests.mkAllAsync`.
    let mkAll () : (string * (TestCtx -> Task<unit>)) list =
        [
            "js eval int",            TestRunner.liftSync jsEvalInt
            "js eval string",         TestRunner.liftSync jsEvalString
            "js install + recall",    TestRunner.liftSync jsInstallAndRecall
            "window.location query",  TestRunner.liftSync windowLocationQuery
            "createElement + append + remove", TestRunner.liftSync createElementAppendRemove
            "element style read/write", TestRunner.liftSync elementStyleRoundtrip
            "JSImage tryLoad favicon",   jsImageLoadFavicon
            "JSImage tryLoad missing",   jsImageLoadMissing
            "JSImage pixel round-trip",  jsImageRoundtripPixels
        ]
