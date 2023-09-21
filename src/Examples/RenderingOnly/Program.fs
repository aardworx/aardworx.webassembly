open Aardvark.Base
open Aardvark.Rendering
open Aardvark.Dom
open Aardvark.Application
open Aardvark.FontProvider
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardworx.Rendering.WebGL.Streams
open Microsoft.FSharp.NativeInterop

// This example renders a very basic scene and allows the user to control the camera.
// It works on a similar abstraction-level to `Aardvark.Application.Slim` and therefore
// doesn't use ELM-style updates or any kind of "model-type".
// Please refer to the other examples for ELM-style applications.


module Shader =
    open FShade
    
    let sam =
        samplerCube {
            texture uniform?Skybox
            filter Filter.MinMagMipLinear
            addressU WrapMode.Clamp
            addressV WrapMode.Clamp
            addressW WrapMode.Clamp
        }
    let env (v : Effects.Vertex) =
        fragment {
            let vp = uniform.ProjTrafoInv * v.pos
            let vp = Vec.normalize (vp.XYZ / vp.W)
            let wd = uniform.ViewTrafoInv * V4d(vp, 0.0) |> Vec.xyz |> Vec.normalize
            
            let wd = V3d(wd.X, wd.Z, wd.Y)
            
            return sam.Sample(wd)
        }
    

/// A utility function for creating an Overlay with a little help text.  
let createHelpText() =
    let doc = Window.Document

    let t = doc.CreateElement("div")
    t.Style.Position <- "fixed"
    t.Style.Top <- "0px"
    t.Style.Right <- "0px"
    t.Style.PaddingRight <- "20px"
    t.Style.FontFamily <- "monospace"
    t.Style.FontSize <- "1.2em"
    t.Style.Background <- "rgba(0,0,0,0.2)"
    t.Style.BorderBottomLeftRadius <- "20px"
    t.Style.PointerEvents <- "none"
    t.Style.UserSelect <- "none"
    t.InnerHTML <- "<ul><li>Use WSAD for moving the camera</li><li>Use the left mouse button to look around</li><li><pre id=\"fps\"></pre></li></ul>"
    
    doc.Body.AppendChild t
    
    doc.GetElementById "fps"
    
/// Another utility for creating our RenderControl.
let createRenderControl (app : WebGLApplication) =
    let doc = Window.Document

    // Here we create a canvas element and style it to fill the whole screen.
    let c = doc.CreateCanvasElement()
    c.Style.Width <- "100%"
    c.Style.Height <- "100%"
    c.Style.Background <- "black"
    c.Style.Visibility <- "hidden"
    c.Style.Position <- "fixed"
    c.Style.Top <- "0px"
    c.Style.Left <- "0px"
    c.Style.SetProperty("outline", "none", "important")
    c.Style.Background <- "linear-gradient(to top, #051937, #314264, #5d6f95, #8ba1c9, #bbd5ff)"
    
    // we then append it to the DOM
    doc.Body.AppendChild c

    // for interop with our rendering we need to "wrap" the canvas in a `RenderControl`
    let ctrl = app.CreateRenderControl(c, Antialiasing.None)

    let mutable fps : HTMLElement = null
    // when the first frame was rendered we set the canvas to visible, 
    // remove the loading-spinner from then DOM and add our help-text.
    ctrl.AfterFirstFrame.Add(fun () ->
        let l = doc.GetElementById("loader")
        if not (isNull l) then l.Remove()
        c.Style.RemoveProperty "visibility"
        fps <- createHelpText()
    )
    
    
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let mutable counter = 0
    ctrl.AfterRender.Add (fun _ ->
        if counter >= 50 then
            sw.Stop()
            let v = float counter / sw.Elapsed.TotalSeconds
            if not (isNull fps) then fps.InnerText <- sprintf "%.1ffps" v
            counter <- 0
            sw.Restart()
        else
            counter <- counter + 1
    )
    
    ctrl
   
// Since we're going to render text we need a font.
// We use `Aardvark.FontProvider` for loading it at compile-time s.t. the 
// font-data gets embedded in our program.
type Roboto = GoogleFontProvider< Family = "Roboto Mono", Bold = true >
  
open System.Threading.Tasks
  
type IntWorker() =
    inherit Aardworx.WebAssembly.AbstractWorker()

    override x.Run(ctx) =
        task {
            let rand = RandomSystem()
            while true do
                do! Task.Delay(500)
                let arr = Array.zeroCreate<byte> (64 <<< 20)
                ctx.Send(WorkerMessage.Binary arr)
        }
  
// here we create a `RenderControl`, setup the camera and "compile" the scene for rendering.
let run() =
    task {
        // Since our `WebGLApplication` expects the document to be loaded we need to wait until everything is ready.
        do! Window.Document.Ready
        
        
        
        let! bk = JSImage.load (RelativeUrl "./chapel_bk.png")
        let! ft = JSImage.load (RelativeUrl "./chapel_ft.png")
        let! rt = JSImage.load (RelativeUrl "./chapel_rt.png")
        let! lf = JSImage.load (RelativeUrl "./chapel_lf.png")
        let! dn = JSImage.load (RelativeUrl "./chapel_dn.png")
        let! up = JSImage.load (RelativeUrl "./chapel_up.png")
        let texture = JSTextureCube([|ft; bk; up; dn; rt; lf|], true) :> ITexture
      
        // Create the `WebGLApplication` and a `RenderControl` using our utility from above.
        let app = new WebGLApplication(CommandStreamMode.Managed)
        let ctrl = createRenderControl app

        let time =
            let sw = System.Diagnostics.Stopwatch.StartNew()
            ctrl.Time |> AVal.map (fun _ -> sw.Elapsed)
        
        
        
        // the camera can be controlled with Aardvark.Application's DefaultCameraController
        let view =
            CameraView.lookAt (V3d(3,4,3)) V3d.Zero V3d.OOI
            |> DefaultCameraController.control ctrl.Mouse ctrl.Keyboard ctrl.Time

        let proj =
            ctrl.Sizes |> AVal.map (fun s ->
                Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
            )
            
        

        let trafo =
            time |> AVal.map (fun t ->
                Trafo3d.RotationZ t.TotalSeconds
            )
        
        let gridSize = cval 2
        
        
        ctrl.Keyboard.DownWithRepeats.Values.Add (fun k ->
            match k with
            | Keys.OemPlus -> transact (fun () -> gridSize.Value <- gridSize.Value + 1)
            | Keys.OemMinus -> transact (fun () -> gridSize.Value <- max 0 (gridSize.Value - 1))
            | _ -> ()
        )
        
        // let's start to setup a scene.
        let scene =
            sg {
                // first off we need to set the camera
                Sg.View (AVal.map CameraView.viewTrafo view) 
                Sg.Proj (AVal.map Frustum.projTrafo proj)
                
                Sg.Trafo trafo
                
                // for shading we simply use Aardvark.Rendering's default shaders doing transformations and 
                // applying a simple phong-illumination with a headlight.
                Sg.Shader {
                    DefaultSurfaces.trafo
                    DefaultSurfaces.simpleLighting
                }

                // render a centered floor-plane of size 20
                sg {
                    Sg.Scale 10.0
                    Primitives.Quad(Quad3d(V3d(-1, -1, 0), V3d(2, 0, 0), V3d(0.0, 2.0, 0.0)), C4b.SandyBrown)
                }
                
                // render a green teapot centered at the origin
                Primitives.Teapot(C4b.Green)
                
                sg {
                    Sg.Translate(3.0, 0.0, 0.0)
                    Primitives.Teapot(C4b.Green)
                }
                
                sg {
                    Sg.Translate(V3d(0.0, 0.0, 3.0))
                    Primitives.Sphere(0.5, C4b.Yellow)
                }
                
                sg {
                    Sg.Translate(gridSize |> AVal.map (fun s -> -V3d(s,s, 0.0) / 2.0))
                    aset {
                        for x in ASet.range (AVal.constant 1) gridSize do
                            for y in ASet.range (AVal.constant 1) gridSize do
                                sg {
                                    Sg.Translate(x, y, 1.0)
                                    Primitives.Sphere(0.5, C4b.Yellow, tessellation = 15)
                                }
                    }
                }
           
                
                // a text (using our font from above) 
                sg {
                    Sg.Scale 0.3
                    Sg.Trafo (Trafo3d.RotationX Constant.PiHalf)
                    Sg.Translate(0.0, 1.0, 0.5)
                    Sg.Text(time |> AVal.map string, align = TextAlignment.Center, font = Roboto.Font, color = AVal.constant C4b.White)
                }
                
                
                sg {
                    Sg.Uniform("Skybox", AVal.constant texture)
                    Sg.Shader {
                        Shader.env
                    }
                    Primitives.ScreenQuad 0.999
                }
                
            }
        
        // in order to render the scene we need to compile it to a `RenderTask`
        let rt =
            let objs = scene.GetRenderObjects (TraversalState.empty app.Runtime)
            app.Runtime.CompileRender(ctrl.FramebufferSignature, objs)

        // tell the `RenderControl` to run our task whenever necessary
        ctrl.RenderTask <- rt
         
        ctrl.AfterFirstFrame.Add (fun _ ->
            task {
                let! workerTest = Worker.start<IntWorker>()
                
                let reader =
                    task {
                        let mutable total = 0
                        while true do
                            let! msg = workerTest.Receive()
                            match msg with
                            | WorkerMessage.Binary arr ->
                                total <- total + arr.Length
                                printfn "Received %A" (Mem total)
                            | _ ->
                                ()
                    }
                ()
            } |> ignore
        )
    }


[<EntryPoint>]
let main _ =
    run() |> ignore
    0