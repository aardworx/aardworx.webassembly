open Aardvark.Base
open Aardvark.Rendering
open Aardvark.Dom
open Aardvark.Application
open Aardvark.FontProvider
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardworx.WebAssembly.Dom
open App

// here we create a `RenderControl`, setup the camera and "compile" the scene for rendering.
let run() =
    task {
        // Since our `WebGLApplication` expects the document to be loaded we need to wait until everything is ready.
        do! Window.Document.Ready
        
        // Create the `WebGLApplication` and a `RenderControl` using our utility from above.
        let gl = new WebGLApplication(CommandStreamMode.Managed, false)
        
        Boot.run gl App.app

    }


[<EntryPoint>]
let main _ =
    run() |> ignore
    0