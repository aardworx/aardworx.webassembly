open System
open Aardvark.Base
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardworx.Rendering.WebGL.Tests

let run () =
    task {
        try
            do! Window.Document.Ready

            let doc = Window.Document
            let resultsDiv =
                let r = doc.GetElementById "results"
                if isNull r then
                    let r = doc.CreateElement "div"
                    r.Id <- "results"
                    doc.Body.AppendChild r
                    r
                else r

            // Hidden canvas — WebGLApplication creates its own internal canvas, but
            // having a placeholder helps when developers want to peek.
            let canvas = doc.CreateCanvasElement()
            canvas.Style.Display <- "none"
            doc.Body.AppendChild canvas

            // Pre-fetch the golden reference PNG. Done up here (before kicking off
            // the sync test runner) because PNG decode goes through async JSImage.
            let captureRequested = Tests.RefImage.isCaptureMode()
            let! reference =
                task {
                    if captureRequested then
                        // Capture mode short-circuits — don't even try to fetch.
                        return None
                    else
                        try
                            return! Tests.RefImage.tryFetch (RelativeUrl "./reference-teapot.png")
                        with e ->
                            printfn "reference fetch threw: %s" e.Message
                            return None
                }

            let refState : Tests.RefState =
                {
                    Reference = reference
                    CaptureRequested = captureRequested
                }

            try
                let app = new WebGLApplication(CommandStreamMode.Managed, true)
                let ctx : TestCtx =
                    {
                        Device = app.Device
                        Runtime = app.Runtime :> IRuntime
                    }

                resultsDiv.InnerText <- "running tests..."
                let results = TestRunner.run (Tests.mkAll refState) ctx
                TestRunner.renderResults resultsDiv results
                TestRunner.exportToWindow results
            with e ->
                let msg = sprintf "FATAL: %s\n%s" e.Message (e.StackTrace |> Option.ofObj |> Option.defaultValue "")
                resultsDiv.InnerHTML <- ""
                let pre = doc.CreateElement "pre"
                pre.ClassName <- "error"
                pre.InnerText <- msg
                resultsDiv.AppendChild pre
                TestRunner.exportFatal msg
        with e ->
            // last-ditch fatal reporter — Window.Document.Ready may itself throw
            try TestRunner.exportFatal (e.ToString()) with _ -> ()
    }

[<EntryPoint>]
let main _ =
    run () |> ignore
    0
