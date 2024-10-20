﻿namespace Aardworx.Rendering.WebGL
        
open Microsoft.JSInterop
open Silk.NET.OpenGLES
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardvark.Rendering
open Aardvark.Base
open Aardvark.SceneGraph
open Aardvark.Application
open FSharp.Data.Adaptive

      
[<AbstractClass>]
type WebGLSwapChain internal(device : Device, main : HTMLCanvasElement, dst : HTMLCanvasElement, blit : JsObj) =

    static let defaultSignature =
        new FramebufferSignature(
            Unchecked.defaultof<_>,
            Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
            Some TextureFormat.Depth24Stencil8,
            1,1
        )
        
    member x.FramebufferSignature = defaultSignature

    abstract RenderFrame : action : (Framebuffer -> Silk.NET.OpenGLES.GL -> unit) -> unit
    abstract Release : unit -> unit
    
    member x.Dispose() : unit =
        blit.Invoke("destroy", [||])
        x.Release()

    interface System.IDisposable with
        member x.Dispose() = x.Dispose()

type private WebGLSwapChainFXAA internal(device : Device, main : HTMLCanvasElement, dst : HTMLCanvasElement, blit : JsObj) =
    inherit WebGLSwapChain(device, main, dst, blit)
    
    static let colorOnlySignature =
        new FramebufferSignature(
            Unchecked.defaultof<_>,
            Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
            None,
            1,1
        )
        
    static let signature =
        new FramebufferSignature(
            Unchecked.defaultof<_>,
            Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
            Some TextureFormat.Depth24Stencil8,
            1,1
        )
        
    let mutable fbo : option<Framebuffer * Texture> = None
    let mutable back : option<Framebuffer> = None

    let currentTexture = cval Unchecked.defaultof<IBackendTexture>
    
    
    static let effect =
        lazy (
            FShade.Effect.ofFunction (
                FXAA.fxaaExtreme
            )
        )

    let renderFXAA =
        let obj = RenderObject()
        
        obj.RenderPass <- RenderPass.main
        obj.Activate <- fun () -> { new System.IDisposable with member x.Dispose() = () }
        obj.BlendState <- BlendState.Default
        obj.DepthState <- { DepthState.Default with Test = AVal.constant DepthTest.Always }
        obj.RasterizerState <- RasterizerState.Default
        obj.Mode <- IndexedGeometryMode.TriangleStrip
        obj.DrawCalls <- DrawCalls.Direct (AVal.constant [DrawCallInfo 4])
        obj.Indices <- None
        obj.IsActive <- AVal.constant true
        obj.StencilState <- StencilState.Default
        obj.Surface <- Surface.Effect effect.Value
        obj.Uniforms <-
            UniformProvider.ofList [
                "DiffuseColorTexture", currentTexture :> IAdaptiveValue
            ]
        obj.InstanceAttributes <- AttributeProvider.Empty
        obj.VertexAttributes <- 
            AttributeProvider.ofList [
                DefaultSemantic.Positions, BufferView.ofArray [| V3f.NNN; V3f.PNN; V3f.NPN; V3f.PPN |]
                DefaultSemantic.DiffuseColorCoordinates, BufferView.ofArray [| V2f.OO; V2f.IO; V2f.OI; V2f.II|]
            ]
        
        device.Runtime.CompileRender(colorOnlySignature, ASet.single (obj :> IRenderObject))

    override x.RenderFrame(action : Framebuffer -> Silk.NET.OpenGLES.GL -> unit) =
        let htmlSize =
            let r = dst.GetBoundingClientRect()
            r.Size
            
        let renderSize =
            V2i(round htmlSize)

        dst.Width <- renderSize.X
        dst.Height <- renderSize.Y
        main.Width <- renderSize.X
        main.Height <- renderSize.Y
        
        let backBuffer = 
            match back with
            | Some fbo when fbo.Size = renderSize -> fbo
            | _ -> 
                let f = device.DefaultFramebuffer renderSize
                back <- Some f
                f

        let tempBuffer, color =
            match fbo with
            | Some (fbo, c) when fbo.Size = renderSize -> 
                fbo, c
            | _ ->
                match fbo with
                | Some (f,c) -> 
                    f.Dispose()
                    c.Dispose()
                | None ->
                    ()

                let c = device.CreateTexture2D(TextureFormat.Rgba8, renderSize)
                let d = device.CreateTexture2D(TextureFormat.Depth24Stencil8, renderSize)
                let f = device.CreateFramebuffer(signature, [DefaultSemantic.Colors, c.[0,0]; DefaultSemantic.DepthStencil, d.[0,0]])
                fbo <- Some (f, c)
                f, c
            
        if not (System.Object.ReferenceEquals(currentTexture.Value, color)) then 
            transact (fun () ->
                currentTexture.Value <- color
            )

        device.Run(fun gl ->
            action tempBuffer gl
            renderFXAA.Run(AdaptiveToken.Top, RenderToken.Empty, backBuffer)
            blit.Invoke("blit", [|main :> obj|])
        )

    override x.Release() =
        renderFXAA.Dispose()
        
type private WebGLSwapChainSimple internal(device : Device, main : HTMLCanvasElement, dst : HTMLCanvasElement, blit : JsObj) =
    inherit WebGLSwapChain(device, main, dst, blit)
   
    let mutable fbo : option<Framebuffer> = None
    
    override x.RenderFrame(action : Framebuffer -> Silk.NET.OpenGLES.GL -> unit) =
        let htmlSize =
            let r = dst.GetBoundingClientRect()
            r.Size
            
        let renderSize =
            V2i(round htmlSize)

        dst.Width <- renderSize.X
        dst.Height <- renderSize.Y
        main.Width <- renderSize.X
        main.Height <- renderSize.Y
        
        let fbo = 
            match fbo with
            | Some fbo when fbo.Size = renderSize -> fbo
            | _ -> 
                let f = device.DefaultFramebuffer renderSize
                fbo <- Some f
                f
        device.Run(fun gl ->
            action fbo gl
            blit.Invoke("blit", [|main :> obj|])
        )

    override x.Release() = ()
            
type private WebGLSwapChainMSAA internal(device : Device, main : HTMLCanvasElement, dst : HTMLCanvasElement, blit : JsObj, samples : int) =
    inherit WebGLSwapChain(device, main, dst, blit)
   
    let mutable fbo : option<Renderbuffer * Renderbuffer * Framebuffer * Texture * Framebuffer> = None
    
    
    
    override x.RenderFrame(action : Framebuffer -> Silk.NET.OpenGLES.GL -> unit) =
        let htmlSize =
            let r = dst.GetBoundingClientRect()
            r.Size
            
        let renderSize =
            V2i(round htmlSize)

        dst.Width <- renderSize.X
        dst.Height <- renderSize.Y
        main.Width <- renderSize.X
        main.Height <- renderSize.Y
        
        let fbo, fboRes = 
            match fbo with
            | Some (_,_,fbo, _, fboRes) when fbo.Size = renderSize -> fbo, fboRes
            | _ ->
                match fbo with
                | Some (c,d,f,c1,f1) ->
                    c.Dispose()
                    d.Dispose()
                    f.Dispose()
                    c1.Dispose()
                    f1.Dispose()
                | None ->
                    ()
                
                let s = device.GetDefaultFramebufferSignature()
                
                let c = device.CreateRenderbuffer(TextureFormat.Rgba8, renderSize, samples)
                let d = device.CreateRenderbuffer(TextureFormat.Depth24Stencil8, renderSize, samples)
                let f = device.CreateFramebuffer(s, [DefaultSemantic.Colors, c :> IFramebufferOutput; DefaultSemantic.DepthStencil, d :> IFramebufferOutput])
                  
                 
                let c1 = device.CreateTexture2D(TextureFormat.Rgba8, renderSize)
                let f1 = device.CreateFramebuffer(s, [DefaultSemantic.Colors, c1.[0,0] :> IFramebufferOutput])
                 
                 
                fbo <- Some (c, d, f, c1, f1)
                f, f1
        device.Run(fun gl ->
            action fbo gl
            
            gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, fbo.Handle)
            gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, fboRes.Handle)
            gl.ReadBuffer ReadBufferMode.ColorAttachment0
            gl.DrawBuffers(1u, [| DrawBufferMode.ColorAttachment0 |])
            WrappedCommands.glBlitFramebuffer(
                0, 0, renderSize.X, renderSize.Y,
                0, 0, renderSize.X, renderSize.Y,
                ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Nearest
            )
            
            gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, fboRes.Handle)
            gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0u)
            
            gl.ReadBuffer ReadBufferMode.ColorAttachment0
            gl.DrawBuffers(1u, [| DrawBufferMode.Back |]) 
            WrappedCommands.glBlitFramebuffer(0, 0, renderSize.X, renderSize.Y, 0, 0, renderSize.X, renderSize.Y, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest)
            gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0u)
            gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0u)
            
            blit.Invoke("blit", [|main :> obj|])
        )

    override x.Release() = ()
    
type private AdaptiveRenderCaller() =
    inherit AdaptiveObject()
 
[<RequireQualifiedAccess>]
type Antialiasing =
    | None
    | MSAA of int
    | FXAA
           

type WebGLRenderControl internal(runtime : Runtime, swapChain : WebGLSwapChain, element : HTMLCanvasElement) =
    let rt = runtime :> IRuntime
    let device = runtime.Device
    
    let signature =
        new FramebufferSignature(
            runtime.Device, 
            Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
            Some TextureFormat.Depth24Stencil8,
            1, 1
        )
    
    let clearColor = cval (C4f(0.0f, 0.0f, 0.0f, 0.0f))
    
    let clearTask = 
        let clearValues = ClearValues.empty |> ClearValues.depth 1.0 |> ClearValues.stencil 0
        let clearValues = clearColor |> AVal.map (fun c -> clearValues |> ClearValues.color c)
        rt.CompileClear(signature, clearValues)
        
    let mutable renderTask = RenderTask.empty
    let mutable vsync = true
    let mutable dirty = false
    let sizes = cval V2i.II
    let t0 = System.DateTime.Now
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let time = AVal.custom (fun _ -> t0 + sw.Elapsed)

    let mutable selfFbo : option<Framebuffer * Renderbuffer * Renderbuffer> = None
    let caller = AdaptiveRenderCaller()
    
    let beforeRender = Event<unit>()
    let afterRender = Event<unit>()
    let afterFirstFrame = Event<unit>()
    let mutable initial = true
    let mutable isDisposed = false
    let mutable lastSize = V2i.II
    
    let keyboard = lazy (new NodeKeyboard(element))
    let mouse = lazy (new NodeMouse(element))

    let render() =
        if not isDisposed then
            dirty <- false
            swapChain.RenderFrame (fun fbo gl ->
                let size = fbo.Size
                if size <> sizes.Value then
                    transact (fun () -> sizes.Value <- size)
                beforeRender.Trigger()
                let framebuffer = fbo
                   
                caller.EvaluateAlways AdaptiveToken.Top (fun token ->
                    clearTask.Run(token, RenderToken.Empty, framebuffer)
                    renderTask.Run(token, RenderToken.Empty, framebuffer)
                )
                
                //gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer.Handle)
                //gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0u)
                //gl.ReadBuffer(ReadBufferMode.ColorAttachment0)
                //gl.DrawBuffers [| DrawBufferMode.Back |]
                //gl.BlitFramebuffer(0, 0, size.X, size.Y, 0, 0, size.X, size.Y, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest)
                //gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer.Handle)
                //gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0u)
            
            )
            transact time.MarkOutdated
            afterRender.Trigger()
            if initial then
                initial <- false
                afterFirstFrame.Trigger()

    let invalidate() =
        if not isDisposed && not dirty then
            dirty <- true
            if vsync then Window.RequestAnimationFrame render
            else Window.SetTimeout(0, render) |> ignore//System.Threading.Tasks.Task.Factory.StartNew render |> ignore
            
    let sub = caller.AddMarkingCallback invalidate
    
    let resizeChecker =
        task {
            while not isDisposed do
                let htmlSize =
                    let r = element.GetBoundingClientRect()
                    r.Size
            
                let renderSize =
                    V2i(round htmlSize)

                if lastSize <> renderSize then
                    lastSize <- renderSize
                    invalidate()
                do! System.Threading.Tasks.Task.Delay 100
                
        }
    
    // invalidate without animationframe (when browser invsible)
    do dirty <- true; Window.SetTimeout(0, render) |> ignore
    
    member x.Dispose() =
        isDisposed <- true
        sub.Dispose()
        clearTask.Dispose()
        match selfFbo with
        | Some (f, c, d) -> 
            f.Dispose()
            c.Dispose()
            d.Dispose()
            selfFbo <- None
        | None ->
            ()

        if keyboard.IsValueCreated then keyboard.Value.Dispose()
        if mouse.IsValueCreated then mouse.Value.Dispose()
        swapChain.Dispose()
    
    member x.Keyboard = keyboard.Value :> IKeyboard
    member x.Mouse = mouse.Value :> IMouse

    member x.VSync
        with get() = vsync
        and set v = 
            vsync <- v
            invalidate()

    member x.Visible
        with get() = element.Style.Visibility <> "hidden"
        and set v =
            if v then element.Style.Visibility <- ""
            else element.Style.Visibility <- "hidden"

    member x.Cursor 
        with get() =
            match element.Style.Cursor with
            | "none" -> Cursor.None
            | "ns-resize" -> Cursor.VerticalResize
            | "ew-resize" -> Cursor.HorizontalResize
            | "pointer" -> Cursor.Hand
            | "text" -> Cursor.Text
            | "crosshair" -> Cursor.Crosshair
            | _ -> Cursor.Default
        and set (c : Cursor) =
            let htmlCursor = 
                match c with
                | Cursor.None -> "none"
                | Cursor.VerticalResize -> "ns-resize"
                | Cursor.HorizontalResize -> "ew-resize"
                | Cursor.Hand -> "pointer"
                | Cursor.Text -> "text"
                | Cursor.Crosshair -> "crosshair"
                | _ -> "default"
            element.Style.Cursor <- htmlCursor
    member x.FramebufferSignature = signature
    member x.Runtime = runtime
    member x.Size = sizes :> aval<_>
    member x.Sizes = sizes :> aval<_>
    member x.Time = time :> aval<_>
    member x.RenderTask
        with get() = renderTask
        and set (t : IRenderTask) =
            renderTask <- t
            invalidate()
    
    member x.Samples = 1
    member x.SubSampling 
        with get() = 1.0
        and set _ = ()

    member x.BeforeRender = beforeRender.Publish
    member x.AfterRender = afterRender.Publish
    member x.AfterFirstFrame = afterFirstFrame.Publish

    member x.ClearColor 
        with get() = clearColor.Value
        and set v = transact (fun () -> clearColor.Value <- v)

    interface System.IDisposable with
        member x.Dispose() = x.Dispose()

    interface IRenderTarget with
        member x.FramebufferSignature = x.FramebufferSignature
        member x.RenderTask
            with get() = x.RenderTask
            and set t = x.RenderTask <- t
        member x.Sizes = x.Size
        member x.Time = x.Time
        member x.Runtime = x.Runtime
        member x.Samples = x.Samples
        member x.SubSampling 
            with get() = x.SubSampling
            and set v = x.SubSampling <- v
        member x.BeforeRender = x.BeforeRender
        member x.AfterRender = x.AfterRender
        
    interface IRenderControl with
        member x.Keyboard = x.Keyboard
        member x.Mouse = x.Mouse
        member x.Cursor 
            with get() = x.Cursor
            and set v = x.Cursor <- v

type WebGLApplication(commandStreamMode : CommandStreamMode, debug : bool) =
        
    static let blitCode =
        """
            (function() {
                window.compileBlit = function(canvas) {
                    let ctx = canvas.getContext('2d');
                
                    return { 
                        blit: function(src) {
                            ctx.drawImage(src, 0, 0);
                        },
                        destroy: function() {
                        }
                    };
                    // let gl = canvas.getContext("webgl2", { antialias: false, premultipliedAlpha: true, alpha: true });
                    // let tex = gl.createTexture();
                    // let fbo = gl.createFramebuffer();
                    // gl.bindTexture(gl.TEXTURE_2D, tex);
// 
                    // gl.bindFramebuffer(gl.READ_FRAMEBUFFER, fbo);
                    // gl.framebufferTexture2D(gl.READ_FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, tex, 0);
                    // gl.readBuffer(gl.COLOR_ATTACHMENT0);
// 
                    // gl.bindFramebuffer(gl.DRAW_FRAMEBUFFER, null);
                    // gl.drawBuffers([gl.BACK]);    
                    // gl.pixelStorei(gl.UNPACK_PREMULTIPLY_ALPHA_WEBGL, true);
    // 
                    // let check = function() {
                    //     let err = gl.getError();
                    //     if(err != gl.NO_ERROR) {
                    //         console.log("Error: " + err);
                    //     }
                    // };
    // 
                    // return { 
                    //     blit: function(src) {
                    //         gl.bindTexture(gl.TEXTURE_2D, tex);
                    //         check();
                    //         gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, src);
                    //         check();
                    //                 
                    //         gl.bindFramebuffer(gl.READ_FRAMEBUFFER, fbo);
                    //         check();
                    //         gl.framebufferTexture2D(gl.READ_FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, tex, 0);
                    //         check();
                    //         gl.readBuffer(gl.COLOR_ATTACHMENT0);
                    //         check();
// 
                    //         gl.bindFramebuffer(gl.DRAW_FRAMEBUFFER, null);
                    //         check();
                    //         gl.drawBuffers([gl.BACK]);    
                    //         check();
                    //         gl.pixelStorei(gl.UNPACK_PREMULTIPLY_ALPHA_WEBGL, true);
                    //         check();
            // 
                    //         let status = gl.checkFramebufferStatus(gl.READ_FRAMEBUFFER);
                    //         if(status != gl.FRAMEBUFFER_COMPLETE) {
                    //             console.log("Error: " + status);
                    //         }   
                    //         
                    //         gl.blitFramebuffer(0,0,src.width,src.height,0,canvas.height,canvas.width,0, gl.COLOR_BUFFER_BIT, gl.NEAREST);
                    //         check();
                    //     },
                    //     destroy: function() {
                    //         gl.deleteTexture(tex);
                    //         gl.deleteFramebuffer(fbo);
                    //         let ext = gl.getExtension('WEBGL_lose_context');
                    //         if(ext) ext.loseContext();
                    //     }
                    // };
                };
            })();
        """

    static let initialize = lazy (JsObj.InstallScript blitCode)

    do initialize.Value
        
    let id = RandomElementId()
    let canvas, device = 
        let c = Window.Document.CreateCanvasElement()
        c.Style.Visibility <- "hidden"
        c.Style.Display <- "none"
        c.Style.Position <- "fixed"
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
        let device = Device(ctx, debug)
        
        c, device
        
    let runtime = Runtime(device, commandStreamMode)
    
    new(mode : CommandStreamMode) =
        new WebGLApplication(mode, (mode = mode))
    
    member x.Device = device
    member x.Runtime = runtime

    member x.CreateRenderControl(dst : HTMLCanvasElement, ?antialiasing : Antialiasing) =
        let antialiasing = defaultArg antialiasing Antialiasing.None
        let blit = Window.Invoke("compileBlit", [| dst :> obj |])
        let swap = 
            match antialiasing with
            | Antialiasing.FXAA -> new WebGLSwapChainFXAA(device, canvas, dst, blit) :> WebGLSwapChain
            | Antialiasing.MSAA s -> new WebGLSwapChainSimple(device, canvas, dst, blit) :> WebGLSwapChain //new WebGLSwapChainMSAA(device, canvas, dst, blit, s) :> WebGLSwapChain
            | Antialiasing.None -> new WebGLSwapChainSimple(device, canvas, dst, blit) :> WebGLSwapChain
        new WebGLRenderControl(runtime, swap, dst)

    member x.Dispose() =
        canvas.Remove()

    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
        
    new() = new WebGLApplication(CommandStreamMode.Managed)
        
