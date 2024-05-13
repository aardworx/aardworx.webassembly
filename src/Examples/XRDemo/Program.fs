open System.Net.Http
open Aardvark.Base
open Aardvark.Rendering
open Aardvark.Dom
open Aardvark.Application
open Aardvark.FontProvider
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.WebAssembly.WebXR
open Aardworx.Rendering.WebGL
open Aardworx.Rendering.WebGL.Streams
open Microsoft.FSharp.NativeInterop
open Microsoft.AspNetCore.Components.WebAssembly.Http
open Aardworx.WebAssembly.WebXR
 
 
 
let run() =
    task {
        do! Window.Document.Ready
  
   
        let id = RandomElementId()
        let canvas, device = 
            let c = Window.Document.CreateCanvasElement()
            c.Style.Width <- "100%"
            c.Style.Height <- "100%"
            c.Id <- id
            Window.Document.Body.AppendChild c
            let ctx = 
                let sel = $"#{id}"
                let mutable res = None
                while Option.isNone res do
                    try
                        let r = WebGLContext.Create sel
                        res <- Some r
                    with e ->
                        printfn "retry: %A" e
                        ()
                res.Value
                
            
                
            let device = Device(ctx, false)
            c, device
        
            
        
        //
        // let b = Window.Document.CreateElement("button")
        // b.InnerHTML <- "Start"
        // b.Style.Width <- "100%"
        // b.Style.Height <- "100%"
        // b.Style.FontSize <- "50px"
        let runtime = Runtime(device, CommandStreamMode.Managed)
        //
        // WebXR.run runtime XRMode.ImmersiveAR [] (fun _session info ->
        //     let scene =
        //         sg {
        //             Sg.View info.View
        //             Sg.Proj info.Proj
        //          
        //             Sg.Shader {
        //                 DefaultSurfaces.trafo
        //                 DefaultSurfaces.simpleLighting
        //             }
        //                
        //             sg {
        //                 Primitives.Box(V3d.III, color = C4b.Red)
        //             }
        //             
        //         }
        //     
        //     let renderTask = runtime.CompileRender(info.FramebufferSignature, scene.GetRenderObjects (TraversalState.empty runtime))
        //     renderTask
        // ) |> ignore
        let b = Window.Document.CreateElement("button")
        b.InnerHTML <- "Start"
        b.Style.Position <- "fixed"
        b.Style.Left <- "0"
        b.Style.Top <- "0"
        b.Style.Width <- "100%"
        b.Style.Height <- "100%"
        b.Style.FontSize <- "50px"
        b.Style.ZIndex <- "100000"
        b.OnClick.Add (fun _ ->
            b.Remove()
            WebXR.run runtime XRMode.ImmersiveAR [] (fun _session info ->
                let scene =
                    sg {
                        Sg.View info.View
                        Sg.Proj info.Proj
                     
                        Sg.Shader {
                            DefaultSurfaces.trafo
                            DefaultSurfaces.simpleLighting
                        }
                           
                        sg {
                            Primitives.Box(V3d.III, color = C4b.Red)
                        }
                        
                    }
                
                let renderTask = runtime.CompileRender(info.FramebufferSignature, scene.GetRenderObjects (TraversalState.empty runtime))
                renderTask
            ) |> ignore
        )
        Window.Document.Body.AppendChild b
        let l = Window.Document.GetElementById("loader")
        if not (isNull l) then l.Remove()
    
        ()
    }


[<EntryPoint>]
let main _ =
    run() |> ignore
    0