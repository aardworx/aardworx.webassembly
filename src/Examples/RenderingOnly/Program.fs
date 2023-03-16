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
    t.InnerHTML <- "<ul><li>Use WSAD for moving the camera</li><li>Use the left mouse button to look around</li></ul>"
    
    doc.Body.AppendChild t
    
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

    // when the first frame was rendered we set the canvas to visible, 
    // remove the loading-spinner from then DOM and add our help-text.
    ctrl.AfterFirstFrame.Add(fun () ->
        let l = doc.GetElementById("loader")
        if not (isNull l) then l.Remove()
        c.Style.RemoveProperty "visibility"
        createHelpText()
    )
    
    ctrl
   
// Since we're going to render text we need a font.
// We use `Aardvark.FontProvider` for loading it at compile-time s.t. the 
// font-data gets embedded in our program.
type Roboto = GoogleFontProvider< Family = "Roboto Mono", Bold = true >
  
// here we create a `RenderControl`, setup the camera and "compile" the scene for rendering.
let run() =
    task {
        // Since our `WebGLApplication` expects the document to be loaded we need to wait until everything is ready.
        do! Window.Document.Ready
        
        
        let msg0 = cval "Hello"
        let msg1 = cval "World"
        
        let length = msg1 |> APtr.mapVal (fun s -> s.Length)
        
        let app = new WebGLApplication()
        let bla = new JSCommandEncoder(app.Device)
        
        app.Runtime.Device.Run(fun gl ->
            gl.Enable(Silk.NET.OpenGLES.EnableCap.Blend)    
        )
        
        let a = [| 255uy; 128uy; 37uy; 123uy; 1uy; 2uy; 3uy; 4uy |]
        let b = Array.zeroCreate<byte> a.Length
        let cnt = [| 8n |]
        b.[0] <- 42uy
        // let pa = APtr.pinArray a
        // let pb = APtr.pinArray b
        // let pc = APtr.pinArray cnt
        //
        // pa.Acquire()
        // pb.Acquire()
        // pc.Acquire()
        //
        // pa.Update AdaptiveToken.Top
        // pb.Update AdaptiveToken.Top
        // pc.Update AdaptiveToken.Top
        
        let v = [|1|]
        
        do
            use pa = fixed a
            use pb = fixed b
            let pc = APtr.pinArray cnt
            
            let pia = APtr.pinArray [| NativePtr.toNativeInt pa |]
            let pib = APtr.pinArray [| NativePtr.toNativeInt pb |]
            let buffArr = Array.zeroCreate<uint32> 1
            let buff = APtr.pinArray buffArr
            bla.Begin()
            
            bla.Switch(APtr.pinArray v,
                [
                    1, fun cmd -> (cmd :?> JSCommandEncoder).JS [| "console.log('one');" |]
                    2, fun cmd -> (cmd :?> JSCommandEncoder).JS [| "console.log('two');" |]
                ], fun cmd -> ())
            
            bla.JS [|
                "console.log(GL.currentContext.GLctx);"
            |]
            
            bla.ActiveTexture Silk.NET.OpenGLES.TextureUnit.Texture1
            bla.GenBuffers(APtr.constant 1u, buff)
            bla.Push(pb)
            bla.CopyII(pia, pib, pc)
            bla.Pop(pb)
            
            bla.Custom (fun _ ->
                printfn "hi there"    
            )
            
            bla.End()
            
            bla.Run(AdaptiveToken.Top)
            
            printfn "buff: %A" buffArr.[0]
            printfn "input:  %A" a
            printfn "result: %A" b
            
            
            v.[0] <- 2
            bla.Run(AdaptiveToken.Top)
            
        
        exit 0
        
        
        // Create the `WebGLApplication` and a `RenderControl` using our utility from above.
        let app = new WebGLApplication()
        let ctrl = createRenderControl app

        // the camera can be controlled with Aardvark.Application's DefaultCameraController
        let view =
            CameraView.lookAt (V3d(3,4,3)) V3d.Zero V3d.OOI
            |> DefaultCameraController.control ctrl.Mouse ctrl.Keyboard ctrl.Time

        let proj =
            ctrl.Sizes |> AVal.map (fun s ->
                Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
            )
            
        let sw = System.Diagnostics.Stopwatch.StartNew()
        let active =
            ctrl.Time |> AVal.map (fun _ ->
                if sw.Elapsed.TotalSeconds % 1.0 > 0.5 then
                    true
                else
                    false
            )

        // let's start to setup a scene.
        let scene =
            sg {
                // first off we need to set the camera
                Sg.View (AVal.map CameraView.viewTrafo view)
                Sg.Proj (AVal.map Frustum.projTrafo proj)
                
                //Sg.Active active
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
                
                active |> ASet.bind (function
                    | true ->
                        ASet.single (
                            sg {
                                Sg.Translate(3.0, 0.0, 0.0)
                                Primitives.Octahedron(C4b.Yellow)
                            }
                        )
                    | false ->
                        ASet.single (
                            sg {
                                Sg.Translate(3.0, 0.0, 0.0)
                                Primitives.Teapot(C4b.Green)
                            }
                        )
                )
                
                // a yellow octahedron hovering 1 unit above the teapot
                sg {
                    Sg.Translate(0.0, 0.0, 1.0)
                    Primitives.Octahedron(C4b.Yellow)
                }
                
                // a text (using our font from above) 
                sg {
                    Sg.Scale 0.3
                    Sg.Trafo (Trafo3d.RotationX Constant.PiHalf)
                    Sg.Translate(0.0, 1.0, 0.5)
                    Sg.Text("Aardworx.Rendering.WebGL", align = TextAlignment.Center, font = Roboto.Font, color = AVal.constant C4b.White)
                }
            }
        
        // in order to render the scene we need to compile it to a `RenderTask`
        let task =
            let objs = scene.GetRenderObjects (TraversalState.empty app.Runtime)
            app.Runtime.CompileRender(ctrl.FramebufferSignature, objs)

        // tell the `RenderControl` to run our task whenever necessary
        ctrl.RenderTask <- task
    }


[<EntryPoint>]
let main _ =
    run() |> ignore
    0