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
  
module Sg =
    
    module Shader =
        open FShade
        
        let color =
            sampler2d {
                texture uniform?Color
                filter Filter.MinMagPoint
                addressU WrapMode.Wrap
                addressV WrapMode.Wrap
            }
        
        let depth =
            sampler2d {
                texture uniform?Depth
                filter Filter.MinMagPoint
                addressU WrapMode.Wrap
                addressV WrapMode.Wrap 
            }
        
        type UniformScope with
            member x.Factor : int = uniform?Factor
        
        
        type Fragment =
            {
                [<Color>] c : V4d
                [<Depth>] d : float
            }
        let blit (v : Effects.Vertex) =
            fragment {
                let mutable sum = V4d.Zero
                
                let px = uniform.Factor * V2i (v.tc * V2d uniform.ViewportSize)
                
                for x in 0 .. uniform.Factor - 1 do
                    for y in 0 .. uniform.Factor - 1 do
                        sum <- sum + color.[px + V2i(x,y)]
                
                let avg = sum / float (uniform.Factor * uniform.Factor)
                
                let d = depth.SampleLevel(v.tc, 0.0).X
                if d >= 1.0 || avg.W <= 0.0 then discard()
                
                
                return { c = avg; d = d }
            }
    
    let superResolution (screenSize : aval<V2i>) (factor : int) (scene : ISceneNode) =
        Sg.Delay(fun state ->
            let r = state.Runtime
            
            let signature = 
                r.CreateFramebufferSignature [
                    DefaultSemantic.Colors, TextureFormat.Rgba8
                    DefaultSemantic.DepthStencil, TextureFormat.Depth24Stencil8
                ]
            let rt = r.CompileRender(signature, scene.GetRenderObjects state)
            
            let renderSize = screenSize |> AVal.map (fun s -> s * factor)
            
            let color, depth = 
                rt |> RenderTask.renderToColorAndDepthWithClear renderSize (clear { color (C4f(0.0f, 0.0f, 0.0f, 0.0f)); depth 1.0; stencil 0 })
                
            let p = RenderPass.after "blub" RenderPassOrder.Arbitrary state.Pass
            
            sg {
                Sg.Shader {
                    Shader.blit
                }
                Sg.Pass p
                Sg.BlendMode BlendMode.Blend
                Sg.Uniform("Color", color)
                Sg.Uniform("Depth", depth)
                Sg.Uniform("Factor", AVal.constant factor)
                Sg.Uniform("ViewportSize", screenSize)
                Primitives.FullscreenQuad
            }
        )

open Aardworx.WebXR
 
module Shader2 =
    open FShade
    
    
    let sam =
        sampler2dArray {
            texture uniform?DiffuseColorTexture
            addressU WrapMode.Wrap
            addressV WrapMode.Wrap
            filter Filter.MinMagPoint
        }
    
    type Fragment =
        {
            [<Color>] c : V4d
            [<Depth>] d : float
        }
    
    let blitDepth (v : Effects.Vertex) =
        fragment {
            let idx : int = uniform?EyeIndex
            let scale : float = uniform?Scale
            let vp4 = uniform.ProjTrafoInv * v.pos
            let vp = vp4.XYZ / vp4.W
            
            let depth = sam.SampleLevel(v.tc, idx, 0.0).X * scale
            let vp = V4d((vp / vp.Z) * depth, 1.0)
            let pp = uniform.ProjTrafo * vp
            let d = pp.Z / pp.W
            let d0 = depth / 1.5
            return { c = V4d(d0, d0, d0, 1.0); d = 1.0 } //0.5 * d + 0.5 }
        }
    
    let texArray (v : Effects.Vertex) =
        fragment {
            let v = sam.SampleLevel(v.tc, 0, 0.0).X
            return V4d(v,v,v,1.0)
        }
 
type Bla = GoogleFontProvider<"Roboto Mono">
 
// here we create a `RenderControl`, setup the camera and "compile" the scene for rendering.
let run() =
    task {
        // Since our `WebGLApplication` expects the document to be loaded we need to wait until everything is ready.
        do! Window.Document.Ready
        
        // let! s = WebXR.isSupported WebXR.Inline
        // printfn "webXR: %A" s
        //
        //
        // let! s = WebXR.requestSession WebXR.Inline
        // printfn "webXR: %A" s
        //
        // WebXR.updateRenderState s
        //
        // let! s = WebXR.requestAnimationFrame s
        // printfn "frame: %A" s
        
        
        let! bk = JSImage.load (RelativeUrl "./chapel_bk.png")
        let! ft = JSImage.load (RelativeUrl "./chapel_ft.png")
        let! rt = JSImage.load (RelativeUrl "./chapel_rt.png")
        let! lf = JSImage.load (RelativeUrl "./chapel_lf.png")
        let! dn = JSImage.load (RelativeUrl "./chapel_dn.png")
        let! up = JSImage.load (RelativeUrl "./chapel_up.png")
        let texture = JSTextureCube([|ft; bk; up; dn; rt; lf|], true) :> ITexture
      
        // Create the `WebGLApplication` and a `RenderControl` using our utility from above.
        //let app = new WebGLApplication(CommandStreamMode.Managed)

        let id = RandomElementId()
        let canvas, device = 
            let c = Window.Document.CreateCanvasElement()
            c.Style.Width <- "100%"
            c.Style.Height <- "100%"
            // c.Style.Visibility <- "hidden"
            // c.Style.Display <- "none"
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
        
            
        
        
        let b = Window.Document.CreateElement("button")
        b.InnerHTML <- "Start"
        
        let runtime = new Runtime(device, CommandStreamMode.Managed)
        let signature = device.GetDefaultFramebufferSignature()
        
        let view = cval Trafo3d.Identity
        let proj = cval Trafo3d.Identity
        let tex = cval (new Texture(device, 0u, Silk.NET.OpenGLES.TextureTarget.Texture2DArray, TextureDimension.Texture2D, TextureFormat.R32f, V3i(256, 256, 1), 1, None, None) :> ITexture)
        let eyeIndex = cval 0
        let scale = cval 1.0
        let scene =
            sg {
                Sg.View view
                Sg.Proj proj
                
                
                sg {
                    Sg.Uniform("DiffuseColorTexture", tex)
                    Sg.Uniform("EyeIndex", eyeIndex)
                    Sg.Uniform("Scale", scale)
                    Sg.Shader {
                        Shader2.blitDepth
                    }
                    Primitives.FullscreenQuad
                }
                
                sg {
                    Sg.Pass (RenderPass.after "blub" RenderPassOrder.Arbitrary RenderPass.main)
             
                
                    
                    Sg.Uniform("DiffuseColorTexture", tex)
                    
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        Shader2.texArray
                        //DefaultSurfaces.vertexColor
                    }
                    
                    Primitives.Box(V3d.III * 0.4)
                    
                    Sg.Translate(0,1,0)
                    Sg.Text(scale |> AVal.map string, Bla.Font)
                    
                }
            }
        
        let renderTask = runtime.CompileRender(signature, scene.GetRenderObjects (TraversalState.empty runtime))
        
    
        
        Window.AddEventListener("resize", fun _ ->
            let rect = canvas.GetBoundingClientRect()
            canvas.Width <- int (rect.SizeX * Window.DevicePixelRatio)
            canvas.Height <- int (rect.SizeY * Window.DevicePixelRatio)
        )
        
        b.AddEventListener("click", false, fun e ->
            task {
                let mode = WebXR.ImmersiveAR
                
                let! s = WebXR.isSupported mode
                printfn "isSupported: %A" s
                if s then
                    
                    let! session =
                        XRSystem.RequestSession(
                            mode,
                            optionalFeatures = [
                                XRFeature.HandTracking
                                XRFeature.DepthSensing { FormatPreference = [XRDepthFormat.Float32]; UsagePreference = [XRDepthUsagePreference.GpuOptimized] }
                            ])
                    
                    printfn "FORMAT: %A" session.DepthDataFormat
                    printfn "USAGE: %A" session.DepthUsage
                    
                    let s = session.Handle
                    printfn "requestSession: %A" s
                    
                    let! a = WebXR.makeXRCompatible device.Context
                    printfn "makeXRCompatible: %A" a
                    
                    //let layer = WebXR.createXRWebGLLayer s device.Context { XRWebGLLayerOptions.Default with Stencil = true; IgnoreDepthValues = false }
                    let layer = session.CreateWebGLLayer(device, { XRWebGLLayerOptions.Default with Stencil = true; IgnoreDepthValues = false })
                    printfn "layer: %A" layer.Handle
                    
                    printfn "Framebuffer.Size: %A" layer.Framebuffer.Size
                    printfn "Framebuffer.Handle: %A" layer.Framebuffer.Handle
                    printfn "Antialias: %A" layer.Antialias
                    printfn "IgnoreDepthValues: %A" layer.IgnoreDepthValues
                    printfn "FixedFoveation: %A" layer.FixedFoveation
                    
                    
                    let layer = layer.Handle
                    printfn "createXRWebGLLayer %A" layer
                    
                    
                    let state = { WebXR.XRRenderState.Default with BaseLayer = Some layer; DepthNear = Some 0.1; DepthFar = Some 100.0 }
                    WebXR.updateRenderState s state
                    printfn "updateRenderState"
                    
                    let! refSpace = WebXR.requestReferenceSpace s "local"
                    printfn "requestReferenceSpace %A" refSpace
                    
                    
                    let l = Window.Document.GetElementById("loader")
                    if not (isNull l) then l.Remove()
                    
                    let rec render(frame : string) =
                        let doc = System.Text.Json.JsonDocument.Parse frame
                        
                        let mapToArray (mapping : System.Text.Json.JsonElement -> 'a) (m : System.Text.Json.JsonElement) =
                            let res = Array.zeroCreate (m.GetArrayLength())
                            for i in 0 .. res.Length - 1 do
                                res.[i] <- mapping m.[i]
                            res
                        
                        let handle = doc.RootElement.GetProperty("framebuffer").GetUInt32()
                        let vps =
                            [|
                                let arr = doc.RootElement.GetProperty("viewports")
                                for i in 0 .. arr.GetArrayLength() - 1 do
                                    let vp = arr.[i]
                                    let x = vp.GetProperty("x").GetDouble() |> int
                                    let y = vp.GetProperty("y").GetDouble() |> int
                                    let w = vp.GetProperty("width").GetDouble() |> int
                                    let h = vp.GetProperty("height").GetDouble() |> int
                                    
                                    let scale = vp.GetProperty("depthScale").GetDouble()
                                    
                                    let view = vp.GetProperty("view") |> mapToArray (fun e -> e.GetDouble()) |> M44d |> Mat.transpose
                                    let proj = vp.GetProperty("proj") |> mapToArray (fun e -> e.GetDouble()) |> M44d |> Mat.transpose
                                    
                                    let depthTexId = vp.GetProperty("depthTextureId").GetUInt32()
                                    //printfn "%d: %A" i depthTexId
                                    yield (V2i(x,y),V2i(w,h), view, proj, depthTexId, scale)
                            |]
                                
                        let signature =
                            device.CreateFramebufferSignature(
                                Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
                                Some TextureFormat.Depth24Stencil8
                            )
                                
                        let framebufferSize =
                            let w = doc.RootElement.GetProperty("framebufferWidth").GetDouble() |> int
                            let h = doc.RootElement.GetProperty("framebufferHeight").GetDouble() |> int
                            V2i(w, h)
                            
                        let fbo = new Framebuffer(device, signature, framebufferSize, Map.empty, None, handle)
                        
                     
                        device.RunCommand (fun cmd ->
                            cmd.BaseStream.BindTexture(Silk.NET.OpenGLES.TextureTarget.Texture2D, 0u)
                            cmd.PushFramebuffer(fbo)
                            cmd.BaseStream.ClearColor(0.0f, 0.0f, 0.0f, 0.0f)
                            cmd.BaseStream.ClearDepthf(1.0f)
                            cmd.BaseStream.ClearStencil 0
                            cmd.BaseStream.Clear (Silk.NET.OpenGLES.ClearBufferMask.ColorBufferBit ||| Silk.NET.OpenGLES.ClearBufferMask.DepthBufferBit ||| Silk.NET.OpenGLES.ClearBufferMask.StencilBufferBit)
                            
                            //cmd.PopFramebuffer()
                            //cmd.BaseStream.BindFramebuffer(Silk.NET.OpenGLES.FramebufferTarget.Framebuffer, 0u)
                            cmd.PopFramebuffer()
                        )
                        for i, (o, s, v, p, depthTexId, ds) in Array.indexed vps do
                            let depthTex = new Texture(device, depthTexId, Silk.NET.OpenGLES.TextureTarget.Texture2DArray, TextureDimension.Texture2D, TextureFormat.R32f, V3i(256, 256, 1), 1, None, None)
                            transact (fun () ->
                                view.Value <- Trafo3d(v.Inverse, v)
                                proj.Value <- Trafo3d(p, p.Inverse)
                                tex.Value <- depthTex
                                eyeIndex.Value <- i
                                scale.Value <- ds
                            )
                            
                            let desc =
                                {
                                    framebuffer = fbo
                                    viewport = Box2i(o, o + s - V2i.II) 
                                }
                            renderTask.Run desc
                        
                        device.Run(fun gl ->
                            gl.BindFramebuffer(Silk.NET.OpenGLES.FramebufferTarget.Framebuffer, fbo.Handle)
                            gl.BindTexture(Silk.NET.OpenGLES.TextureTarget.Texture2D, 0u)
                        )
                        
                        WebXR.requestAnimationFrame s layer refSpace device.Context render
                  
                    WebXR.requestAnimationFrame s layer refSpace device.Context render
            } |> ignore
        )
        b.Style.Position <- "absolute"
        b.Style.Top <- "10px"
        b.Style.Left <- "10px"
        b.Style.ZIndex <- "100000"
        
        Window.Document.Body.AppendChild b
        
        
        
        
        
        
        //printfn "XR compat: %A" res
        //
        // let ctx = app.Runtime.Device.Context.GL.Context :?> Silk.NET.Core.Contexts.DefaultNativeContext
        // printfn "HANDLE: %A" ctx.Library.Handle
        // let ctrl = createRenderControl app
        //
        // let time =
        //     let sw = System.Diagnostics.Stopwatch.StartNew()
        //     ctrl.Time |> AVal.map (fun _ -> sw.Elapsed.TotalSeconds.ToString("0"))
        //
        //
        //
        // // the camera can be controlled with Aardvark.Application's DefaultCameraController
        // let view =
        //     CameraView.lookAt (V3d(3,4,3)) V3d.Zero V3d.OOI
        //     |> DefaultCameraController.control ctrl.Mouse ctrl.Keyboard ctrl.Time
        //
        // let proj =
        //     ctrl.Sizes |> AVal.map (fun s ->
        //         Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
        //     )
        //     
        //
        //
        // let trafo = AVal.constant Trafo3d.Identity
        //     // time |> AVal.map (fun t ->
        //     //     Trafo3d.RotationZ t.TotalSeconds
        //     // )
        //
        // let gridSize = cval 2
        //
        //
        // ctrl.Keyboard.DownWithRepeats.Values.Add (fun k ->
        //     match k with
        //     | Keys.OemPlus -> transact (fun () -> gridSize.Value <- gridSize.Value + 1)
        //     | Keys.OemMinus -> transact (fun () -> gridSize.Value <- max 0 (gridSize.Value - 1))
        //     | _ -> ()
        // )
        //
        // // let's start to setup a scene.
        // let scene =
        //     sg {
        //         // first off we need to set the camera
        //         Sg.View (AVal.map CameraView.viewTrafo view) 
        //         Sg.Proj (AVal.map Frustum.projTrafo proj)
        //         
        //         Sg.Trafo trafo
        //         
        //         // for shading we simply use Aardvark.Rendering's default shaders doing transformations and 
        //         // applying a simple phong-illumination with a headlight.
        //         Sg.Shader {
        //             DefaultSurfaces.trafo
        //             DefaultSurfaces.simpleLighting
        //         }
        //
        //         // render a centered floor-plane of size 20
        //         sg {
        //             Sg.Scale 10.0
        //             Primitives.Quad(Quad3d(V3d(-1, -1, 0), V3d(2, 0, 0), V3d(0.0, 2.0, 0.0)), C4b.SandyBrown)
        //         }
        //         
        //         // render a green teapot centered at the origin
        //         Primitives.Teapot(C4b.Green)
        //         
        //         sg {
        //             Sg.Translate(3.0, 0.0, 0.0)
        //             Primitives.Teapot(C4b.Green)
        //         }
        //         
        //         sg {
        //             Sg.Translate(V3d(0.0, 0.0, 3.0))
        //             Primitives.Sphere(0.5, C4b.Yellow)
        //         }
        //         
        //         sg {
        //             Sg.Translate(gridSize |> AVal.map (fun s -> -V3d(s,s, 0.0) / 2.0))
        //             aset {
        //                 for x in ASet.range (AVal.constant 1) gridSize do
        //                     for y in ASet.range (AVal.constant 1) gridSize do
        //                         sg {
        //                             Sg.Translate(x, y, 1.0)
        //                             Primitives.Sphere(0.5, C4b.Yellow, tessellation = 15)
        //                         }
        //             }
        //         }
        //    
        //         
        //         // a text (using our font from above)
        //         
        //         //Sg.superResolution ctrl.Size 4 (
        //         sg {
        //             Sg.Scale 0.3
        //             Sg.Trafo (Trafo3d.RotationX Constant.PiHalf)
        //             Sg.Translate(0.0, 1.0, 0.5)
        //             Sg.Text(time |> AVal.map string, align = TextAlignment.Center, font = Roboto.Font, color = AVal.constant C4b.White)
        //         }
        //         //)
        //         
        //         
        //         sg {
        //             Sg.Uniform("Skybox", AVal.constant texture)
        //             Sg.Shader {
        //                 Shader.env
        //             }
        //             Primitives.ScreenQuad 0.999
        //         }
        //         
        //     }
        //
        // // in order to render the scene we need to compile it to a `RenderTask`
        // let rt =
        //     let objs = scene.GetRenderObjects (TraversalState.empty app.Runtime)
        //     app.Runtime.CompileRender(ctrl.FramebufferSignature, objs)
        //
        // // tell the `RenderControl` to run our task whenever necessary
        // ctrl.RenderTask <- rt
        // //  
        // // ctrl.AfterFirstFrame.Add (fun _ ->
        // //     task {
        // //         let! workerTest = Worker.start<IntWorker>()
        // //         
        // //         let reader =
        // //             task {
        // //                 let mutable total = 0
        // //                 while true do
        // //                     let! msg = workerTest.Receive()
        // //                     match msg with
        // //                     | WorkerMessage.Binary arr ->
        // //                         total <- total + arr.Length
        // //                         printfn "Received %A" (Mem total)
        // //                     | _ ->
        // //                         ()
        // //             }
        // //         ()
        // //     } |> ignore
        // // )
    }


[<EntryPoint>]
let main _ =
    run() |> ignore
    0