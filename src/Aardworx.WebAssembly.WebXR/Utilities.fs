namespace Aardworx.WebAssembly.WebXR

open System
open Aardvark.Base
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardworx.WebAssembly.WebXR
open FSharp.Data.Adaptive

#nowarn "9"

type XRRenderInfo =
    {
        View                    : aval<Trafo3d>
        Proj                    : aval<Trafo3d>
        ViewportSize            : aval<V2i>
        FramebufferSignature    : IFramebufferSignature
    }

module WebXR =
    
    let run (runtime : IRuntime) (mode : XRMode) (features : list<XRFeature>) (compile : XRSession -> XRRenderInfo -> IRenderTask) =
        task {
            match runtime with
            | :? Aardworx.Rendering.WebGL.Runtime as runtime ->
                let! s = XRSystem.IsSessionSupported mode
                if s then
                    
                    let! session =
                        XRSystem.RequestSession(
                            runtime.Device,
                            mode,
                            optionalFeatures = features
                        )
                        
                        
                    let view = cval Trafo3d.Identity
                    let proj = cval Trafo3d.Identity
                    let eyeIndex = cval 0
                    
                    let! a = WebXR.makeXRCompatible runtime.Device.Context
                    printfn "makeXRCompatible: %A" a
                    
                    //let layer = WebXR.createXRWebGLLayer s device.Context { XRWebGLLayerOptions.Default with Stencil = true; IgnoreDepthValues = false }
                    let layerObj = session.CreateWebGLLayer(runtime.Device, { XRWebGLLayerOptions.Default with Stencil = true; IgnoreDepthValues = false })
                    printfn "layer: %A" layerObj.Handle
                    
                    printfn "Framebuffer.Size: %A" layerObj.Framebuffer.Size
                    printfn "Framebuffer.Handle: %A" layerObj.Framebuffer.Handle
                    printfn "Antialias: %A" layerObj.Antialias
                    printfn "IgnoreDepthValues: %A" layerObj.IgnoreDepthValues
                    printfn "FixedFoveation: %A" layerObj.FixedFoveation
                    
                    
                    let layer = layerObj.Handle
                    printfn "createXRWebGLLayer %A" layer
                    
                    
                    let state = { XRRenderState.Default with BaseLayer = Some layerObj; DepthNear = Some 0.1; DepthFar = Some 100.0 }
                    session.RenderState <- state
                    printfn "updateRenderState"
                    
                    let! refSpaceObj = session.RequestReferenceSpace(XRSpaceDescription.Local)
                    let refSpace = refSpaceObj.Handle
                    printfn "RequestReferenceSpace: %d" refSpace
                    
                    try
                        let fbo = layerObj.Framebuffer
                        let clear =
                            let cmd = runtime.Device.CreateCommandStream(CommandStreamMode.Managed)
                            cmd.Begin()
                            cmd.BaseStream.BindTexture(Silk.NET.OpenGLES.TextureTarget.Texture2D, 0u)
                            cmd.PushFramebuffer(fbo)
                            cmd.BaseStream.ClearColor(0.0f, 0.0f, 0.0f, 0.0f)
                            cmd.BaseStream.ClearDepthf(1.0f)
                            cmd.BaseStream.ClearStencil 0
                            cmd.BaseStream.Clear (Silk.NET.OpenGLES.ClearBufferMask.ColorBufferBit ||| Silk.NET.OpenGLES.ClearBufferMask.DepthBufferBit ||| Silk.NET.OpenGLES.ClearBufferMask.StencilBufferBit)
                            cmd.PopFramebuffer()
                            cmd.End()
                            cmd
                            
                        let viewports = Dict<XRView, V2d * V2d>()
                        let projTrafos = Dict<XRView, Trafo3d>()
                        
                        let mutable prev : GamepadButtonState[][] = [||]
                        
                        let getGamepads() =
                            session.InputSources |> Array.choose (fun i ->
                                match i.Gamepad with
                                | Some g -> Some g
                                | None -> None
                            )
                        
                        let renderTask = compile session { View = view; Proj = proj; ViewportSize = AVal.constant layerObj.Framebuffer.Size; FramebufferSignature = fbo.Signature }
                         
                        let mutable running = true
                        let tcs = System.Threading.Tasks.TaskCompletionSource<bool>()
                        let rec render (time : float) (frame : XRFrame) =
                            try
                                let viewerPose = frame.GetViewerPose refSpaceObj
                                let gs = getGamepads()
                                printfn "render"
                                //printfn "Meshes: %A" frame.MeshSetCount
                                
                                let newState =
                                    gs |> Array.mapi (fun gid g ->
                                        let bs = g.Buttons
                                        let oldState =
                                            if gid < prev.Length then prev.[gid]
                                            else [||]
                                        
                                        
                                        for bid in 0 .. bs.Length - 1 do
                                            let oldState =
                                                if bid < oldState.Length then oldState.[bid]
                                                else GamepadButtonState(0, 0, 0.0f)
                                            
                                            if not oldState.Pressed && bs.[bid].Pressed then
                                                printfn "DOWN %d/%d" gid bid
                                            elif oldState.Pressed && not bs.[bid].Pressed then
                                                printfn "UP %d/%d" gid bid
                                        bs
                                    )
                                prev <- newState
                                
                                clear.Run()
                            
                                for ei, v in Array.indexed viewerPose.Views do
                                    let viewTrafo =
                                        Trafo3d v.Transform.Inverse
                                    let projTrafo =
                                        projTrafos.GetOrCreate(v, fun v ->
                                            let m = v.ProjectionMatrix
                                            Trafo3d(M44d m, M44d m.Inverse)
                                        )
                               
                                   
                                        
                                    transact (fun () ->
                                        view.Value <- viewTrafo
                                        proj.Value <- projTrafo
                                        eyeIndex.Value <- ei
                                    )
                                    
                                    
                                    
                                    let (o, s) =
                                        viewports.GetOrCreate(v, fun v ->
                                            layerObj.GetViewport v
                                        )
                                    let desc =
                                        {
                                            framebuffer = fbo
                                            viewport = Box2i(V2i o, V2i o + V2i s - V2i.II) 
                                        }
                                    renderTask.Run desc        
                                
                                if running then
                                    session.RequestAnimationFrame render
                                else
                                    tcs.SetResult true
                            with e ->
                                printfn "%A" e
                            
                        session.RequestAnimationFrame render
                            
                            
                        session.OnEnd.Add(fun () ->
                            running <- false    
                        )
                        
                        return! tcs.Task
                    with e ->
                        printfn "ERROR: %A" e
                        return false
                else
                    return false
            | _ ->
                return false
        }
    