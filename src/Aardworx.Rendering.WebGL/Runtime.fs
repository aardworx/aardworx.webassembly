namespace Aardworx.Rendering.WebGL

open System
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"

type GeometryPool(device : Device, attributes : Map<Symbol, Type>) =

    let sizes =
        attributes |> Map.map (fun _ t ->
            Marshal.SizeOf t
        )


    let initialCapacity = 4n <<< 20


    let mutable capacity = initialCapacity
    let mutable buffers =
        sizes |> Map.map (fun _ elementSize ->
            elementSize, cval (device.CreateBuffer(int64 elementSize * int64 initialCapacity, BufferUsage.Vertex))
        )

    let manager = new Management.MemoryManager<_>(Management.Memory.nop, initialCapacity)

    let fixBuffers() =
        let newCapacity = manager.Capactiy
        if newCapacity <> capacity then
            transact (fun () -> 
                sizes |> Map.iter (fun name elementSize ->
                    let _,o = Map.find name buffers
                    let newBuffer = device.CreateBuffer(int64 elementSize * int64 newCapacity, BufferUsage.Vertex)

                    let s = min newBuffer.Size o.Value.Size
                    device.RunCommand(fun cmd -> cmd.Copy(o.Value.[..s-1L], newBuffer.[..s-1L]))
                    o.Value.Dispose()
                    o.Value <- newBuffer
                )
            )
                

    //let manager =
    //    new Management.MemoryManager<_>(mem, 4n <<< 20)

    

    interface IGeometryPool with
        member this.Alloc(fvc : int, geometry: IndexedGeometry): Management.Block<unit> = 
            let dst = manager.Alloc(nativeint fvc)
            fixBuffers()
            device.RunCommand(fun cmd ->
                let offset = dst.Offset
                buffers |> Map.iter (fun name (es, buffer) ->
                    match geometry.IndexedAttributes.TryGetValue name with
                    | true, src ->
                        cmd.Copy(src, buffer.Value.[int64 es * int64 offset..])
                    | _ ->
                        ()
                )
            )

            dst

        member this.Count: int = 
            int manager.Capactiy
        member this.Dispose(): unit = 
            buffers |> Map.iter (fun _ (_,b) -> b.Value.Dispose())
            buffers <- Map.empty

        member this.Free(arg1: Management.Block<unit>): unit = 
            manager.Free arg1
            fixBuffers()

        member this.TryGetBufferView(name: Symbol): Option<BufferView> = 
            match Map.tryFind name buffers with
            | Some (_, b) -> BufferView(AVal.cast<IBuffer> b, Map.find name attributes) |> Some
            | _ -> None

        member this.UsedMemory: Mem = 
            manager.Capactiy |> Mem


type Runtime(device : Device) as this =
    let manager = ResourceManager(device)
    do device.Runtime <- this :> IRuntime

    member x.Device = device
    member x.ResourceManager = manager

    member this.CompileRender(fboSignature : IFramebufferSignature, objects : aset<IRenderObject>) =
        let signature = fboSignature :?> FramebufferSignature

        let mode =
            if device.Debug then CommandStreamMode.Debug
            else CommandStreamMode.Managed // TODO: fix native

        new RenderTask(manager, signature, mode, objects) :> IRenderTask

    interface IRuntime with
        member x.DebugConfig =
            if device.Debug then DebugLevel.Full
            else DebugLevel.None
            
        member this.AssembleModule(effect, signature, _mode) =
            let signature = signature :?> FramebufferSignature
            signature.AssembleModule(effect)

        //member x.DebugLevel = DebugLevel.None
        member x.MaxRayRecursionDepth = 0
        member x.SupportsRaytracing = false

        member x.CreateAccelerationStructure(_, _, _) = 
            failwith "not rt"
  
        member x.TryUpdateAccelerationStructure(_, _) =
            failwith "no rt"

        member x.CompileTrace(_,_) =
            failwith "no rt"

        member this.Clear(fbo : IFramebuffer, clearValues : ClearValues) =
            device.RunCommand (fun cmd ->
                cmd.PushFramebuffer (fbo :?> Framebuffer)
                cmd.Clear (AVal.constant clearValues)
                cmd.PopFramebuffer()
            )
            
        member this.Clear(tex : IBackendTexture, clearValues : ClearValues) : unit =
            failwith "not implemented"

        //member this.ClearColor(texture, color) =
        //    let tex = texture :?> Texture
        //    device.RunCommand (fun cmd ->
        //        cmd.Clear(tex.[0,0], color)
        //    )
        //member this.ClearDepthStencil(texture, depth, stencil) =
        //    let tex = texture :?> Texture
        //    device.RunCommand(fun cmd ->
        //        cmd.ClearDepthStencil(tex.[0,0], ?depth = depth, ?stencil = stencil)
        //    )

        member this.GenerateMipMaps(texture) = 
            device.RunCommand (fun cmd ->
                cmd.GenerateMipMaps(texture :?> Texture)
            )
            
        member this.ResolveMultisamples(src, dst, imgTrafo) =
            let dst = dst :?> Texture
            if imgTrafo <> ImageTrafo.Identity then Log.warn "ResolveMultisamples ignores ImageTrafo: %A" imgTrafo
            device.RunCommand (fun cmd ->
                match src with
                | :? TextureLevel as l ->
                    cmd.Blit(l.[0], dst.[0,0], true)
                | :? ITextureLevel as l ->
                    let tex = l.Texture :?> Texture
                    cmd.Blit(tex.[l.Level, l.Slices.Min], dst.[0,0], true)
                | :? Renderbuffer as r ->
                    cmd.Blit(r, dst.[0,0], true)
                | _ ->

                    failf "bad input"
            )

        member this.CreateFramebufferSignature(attachments, depth, samples, _layers, _perLayerUniforms) =
            new FramebufferSignature(device, attachments, depth, 1, samples) :> IFramebufferSignature
            

        member this.CreateFramebuffer(signature, attachments) =
            let signature = signature :?> FramebufferSignature
            device.CreateFramebuffer(signature, attachments) :> IFramebuffer
          
        member this.Copy(src : IFramebuffer, dst : IFramebuffer) =
            let inline drawBuffers (f : Framebuffer) (slot : int) =
                if f.Handle = 0u then
                    if slot = 0 then [| DrawBufferMode.Back |]
                    else failwithf "bad slot for default FBO: %A" slot
                else
                    let arr = Array.zeroCreate<DrawBufferMode> f.Signature.ColorAttachmentSlots
                    arr.[slot] <- unbox (int DrawBufferMode.ColorAttachment0 + slot)
                    arr
                
            let src = src :?> Framebuffer
            let dst = dst :?> Framebuffer
            if src.Handle <> dst.Handle then
                device.RunCommand (fun cmd ->
                    cmd.PushFramebuffer()
                    cmd.BaseStream.BindFramebuffer(FramebufferTarget.ReadFramebuffer, src.Handle)
                    cmd.BaseStream.BindFramebuffer(FramebufferTarget.DrawFramebuffer, dst.Handle)

                    if src.Handle = 0u then
                        let depthMask = if Option.isSome dst.Signature.Depth then ClearBufferMask.DepthBufferBit ||| ClearBufferMask.StencilBufferBit else Unchecked.defaultof<_>
                        let colorSlot = Map.tryFind DefaultSemantic.Colors dst.Signature.AttachmentIndices
                        match colorSlot with
                        | Some colorSlot ->
                            cmd.ReadBuffer(ReadBufferMode.Back)
                            cmd.DrawBuffers(drawBuffers dst colorSlot)
                            cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask ||| ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest)
                        | None ->
                            if int depthMask <> 0 then
                                cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask, BlitFramebufferFilter.Nearest)
                                
                    elif dst.Handle = 0u then
                        let depthMask = if Option.isSome src.Signature.Depth then ClearBufferMask.DepthBufferBit ||| ClearBufferMask.StencilBufferBit else Unchecked.defaultof<_>
                        let colorSlot = Map.tryFind DefaultSemantic.Colors src.Signature.AttachmentIndices
                        match colorSlot with
                        | Some colorSlot ->
                            cmd.ReadBuffer(colorSlot)
                            cmd.DrawBuffers [| DrawBufferMode.Back |]
                            cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask ||| ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest)
                        | None ->
                            if int depthMask <> 0 then
                                cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask, BlitFramebufferFilter.Nearest)
                               
                    else
                        let mutable depthMask =
                            if Map.containsKey DefaultSemantic.DepthStencil src.Attachments && Map.containsKey DefaultSemantic.DepthStencil dst.Attachments then
                                ClearBufferMask.DepthBufferBit ||| ClearBufferMask.StencilBufferBit
                            else
                                unbox<ClearBufferMask> 0
                               
                        for (sem, srcIndex) in Map.toSeq src.Signature.AttachmentIndices do
                            match Map.tryFind sem dst.Signature.AttachmentIndices with
                            | Some dstIndex ->
                                cmd.ReadBuffer(srcIndex)
                                cmd.DrawBuffers(drawBuffers dst dstIndex)
                                cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask ||| ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest)
                                depthMask <- Unchecked.defaultof<_>
                            | None ->
                                ()
                                
                        if int depthMask <> 0 then
                            cmd.BlitFramebuffer(V2i.Zero, src.Size, V2i.Zero, dst.Size, depthMask, BlitFramebufferFilter.Nearest)
                       
                    cmd.PopFramebuffer()
                )
          
        member this.ReadPixels(src : IFramebuffer, sem : Symbol, offset : V2i, size : V2i) : PixImage =

            let src = src :?> Framebuffer
            match Map.tryFind sem src.Signature.AttachmentIndices with
            | Some index ->
                let format = src.Signature.ColorAttachments.[index].Format
                let typ = TextureFormat.toDownloadFormat format
                let (pfmt, ptyp) = ColFormat.toPixelFormatAndType format
                let img = PixImage.Create(typ, int64 size.X, int64 size.Y)
                let gc = GCHandle.Alloc(img.Array, GCHandleType.Pinned)
                try
                    device.RunCommand (fun gl ->
                        gl.PushFramebuffer src
                        gl.ReadBuffer index
                        gl.BaseStream.ReadPixels(offset.X, src.Size.Y - size.Y - offset.Y, uint32 size.X, uint32 size.Y, pfmt, ptyp, gc.AddrOfPinnedObject())

                        gl.PopFramebuffer()
                    )
                    img.Transformed(ImageTrafo.MirrorY)
                finally
                    gc.Free()
            | None ->
                failwith ""
            

        
            //let img = PixImage<int>(Col.Format.RGBA, V2i.II) 
            //img.Volume.Data.[0] <- -1
            //img.Volume.Data.[1] <- -1
            //img.Volume.Data.[2] <- -1
            //img.Volume.Data.[3] <- -1
            
            //img :> PixImage

        member this.Compile(_compute) =
            failf "ComputeShaders not supported"

        member this.MaxLocalSize = 
            failf "ComputeShaders not supported"

        member this.NewInputBinding(_compute) = 
            failf "ComputeShaders not supported"

        member this.CreateComputeShader(_compute) = 
            failf "ComputeShaders not supported"

        member this.CompileRender(fboSignature, objects) =
            let signature = fboSignature :?> FramebufferSignature

            let mode =
                if device.Debug then CommandStreamMode.Debug
                else CommandStreamMode.Managed // TODO: fix native

            new RenderTask(manager, signature, mode, objects) :> IRenderTask

        member this.CompileClear(fboSignature, clearValues : aval<ClearValues>) =
            let fboSignature = fboSignature :?> FramebufferSignature
            
            let fboHandle = NativePtr.alloc 1
            let cmd = device.CreateCommandStream()
            cmd.PushFramebuffer()
            cmd.SetFramebuffer(FramebufferTarget.Framebuffer, fboSignature, APtr.ofNativePtr fboHandle)
            cmd.Clear clearValues
            cmd.PopFramebuffer()

            { new AbstractRenderTask() with
                override x.Runtime = Some (this :> IRuntime)
                override x.FramebufferSignature = Some (fboSignature :> IFramebufferSignature)
                override x.Release() =
                    cmd.Dispose()
                    NativePtr.free fboHandle

                override x.Perform(token, _, output) =
                    let dst = output.framebuffer :?> Framebuffer
                    NativePtr.write fboHandle dst.Handle
                    cmd.Run(token)

                override x.PerformUpdate(token,_) =
                    cmd.Update token

                override x.Use(a) = a()

            } :> IRenderTask



        member this.DeviceCount = 1
        member this.SupportedPipelineStatistics = Set.empty

        member this.CreateRenderbuffer(size, format, samples) =
            let samples = if samples > 1 then Some samples else None
            device.CreateRenderbuffer(format, size, ?samples = samples) :> IRenderbuffer
             
        member this.CreateTexture(size, dimension, format, levels, samples) = 
            let samples = if samples > 1 then Some samples else None
            let levels = if levels > 1 then Some levels else None
            device.CreateTexture(dimension, format, size, ?levels = levels, ?samples = samples) :> IBackendTexture

        member this.CreateTextureArray(size, dimension, format, levels, samples, count) = 
            let samples = if samples > 1 then Some samples else None
            device.CreateTexture(dimension, format, size, levels = levels, layers = count, ?samples = samples) :> IBackendTexture
            


        member this.ShaderCachePath
            with get () = None
            and set _ = ()


        member this.ContextLock =
            { new IDisposable with member x.Dispose() = () }
            
        member this.OnDispose =
            { new FSharp.Control.IEvent<unit> with
                member x.Subscribe (obs : IObserver<unit>) =
                    // TODO: proper dispose-event
                    { new IDisposable with member x.Dispose() = () }
                member x.AddHandler _ = ()
                member x.RemoveHandler _ = ()
            }

        member this.CreateGeometryPool(arg1) =
            new GeometryPool(device, arg1) :> IGeometryPool

            
        member this.PrepareTexture(input) =
            let res = device.CreateTexture input
            res :> IBackendTexture
            
        member this.CreateBuffer(size, _usage, _storage) = 
            device.CreateBuffer(int64 size, Aardworx.Rendering.WebGL.BufferUsage.Vertex) :> IBackendBuffer
        
        member this.PrepareBuffer(data, usage : Aardvark.Rendering.BufferUsage, _storage) = 
            device.CreateBuffer(data) :> IBackendBuffer
            
        member this.PrepareRenderObject(signature, object) = 
            manager.PrepareRenderObject(unbox signature, object) :> IPreparedRenderObject

        member this.PrepareSurface(signature, surface) =
            match surface with
            | :? FShadeSurface as s -> manager.CreateProgram(unbox signature, s.Effect) :> IBackendSurface
            | :? Program as p -> p.AddReference(); p :> IBackendSurface
            | _ -> failwith "not implemented"

            
        member this.Copy(srcBuffer: IBackendBuffer, srcOffset: nativeint, dstBuffer: IBackendBuffer, dstOffset: nativeint, size: nativeint): unit = 
            let srcBuffer = srcBuffer :?> Buffer
            let dstBuffer = dstBuffer :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcBuffer.Sub(int64 srcOffset, int64 size), dstBuffer.Sub(int64 dstOffset, int64 size))
            )
            
        member this.Copy(srcBuffer: IBackendBuffer, srcOffset: nativeint, dstData: nativeint, size: nativeint): unit = 
            let srcBuffer = srcBuffer :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcBuffer.Sub(int64 srcOffset, int64 size), dstData)
            )
        member this.Copy(srcData: nativeint, dst: IBackendBuffer, dstOffset: nativeint, size: nativeint): unit = 
            let dstBuffer = dst :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcData, dstBuffer.Sub(int64 dstOffset, int64 size))
            )

        member this.Copy(src: IFramebufferOutput, srcOffset: V3i, dst: IFramebufferOutput, dstOffset: V3i, size: V3i): unit = 
            device.RunCommand(fun cmd ->
                match src with
                | :? TextureLevel as l ->
                    let srcImage = l.[0, srcOffset .. srcOffset + size - V3i.III]
                    match dst with
                    | :? TextureLevel as r ->
                        let dstImage = r.[0, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? ITextureLevel as r ->
                        let rr = r.Texture :?> Texture
                        let dstImage = rr.[r.Level, r.Slices.Min, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? Renderbuffer as r ->
                        cmd.Blit(srcImage, r.[dstOffset.XY .. dstOffset.XY + size.XY - V2i.II], false)
                    | _ ->
                        failf "bad destination"
                        
                | :? ITextureLevel as l ->
                    let rr = l.Texture :?> Texture
                    let srcImage = rr.[l.Level, l.Slices.Min, srcOffset .. srcOffset + size - V3i.III]
                    match dst with
                    | :? TextureLevel as r ->
                        let dstImage = r.[0, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? ITextureLevel as r ->
                        let rr = r.Texture :?> Texture
                        let dstImage = rr.[r.Level, r.Slices.Min, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? Renderbuffer as r ->
                        cmd.Blit(srcImage, r.[dstOffset.XY .. dstOffset.XY + size.XY - V2i.II], false)
                    | _ ->
                        failf "bad destination"
                | :? Renderbuffer as r ->
                    let srcImage = r.[srcOffset.XY .. srcOffset.XY + size.XY - V2i.II]
                    match dst with
                    | :? TextureLevel as r ->
                        let dstImage = r.[0, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? ITextureLevel as r ->
                        let rr = r.Texture :?> Texture
                        let dstImage = rr.[r.Level, r.Slices.Min, dstOffset .. dstOffset + size - V3i.III]
                        cmd.Blit(srcImage, dstImage, false)
                    | :? Renderbuffer as r ->
                        cmd.Blit(srcImage, r.[dstOffset.XY .. dstOffset.XY + size.XY - V2i.II], false)
                    | _ ->
                        failf "bad destination"
                | _ ->
                    failf "bad input"
            )
            //device.Run (fun gl ->
            //    match src with
            //    | :? ITextureLevel as src ->
            //        match dst with
            //        | :? ITextureLevel as dst ->
            //            let srcHandle = src.Texture :?> Texture
            //            let dstHandle = src.Texture :?> Texture
            //            let sf = gl.GenFramebuffer()
            //            let df = gl.GenFramebuffer()
            //            gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, sf)
            //            gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, df)
            //            gl.FramebufferTexture2D(FramebufferTarget.ReadFramebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, srcHandle.Handle, src.Level)
            //            gl.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, dstHandle.Handle, dst.Level)
                        
            //            gl.ReadBuffer ReadBufferMode.ColorAttachment0
            //            gl.DrawBuffers [|DrawBufferMode.ColorAttachment0|]

            //            gl.BlitFramebuffer(
            //                0, 0, src.Size.X, src.Size.Y,
            //                0, 0, dst.Size.X, dst.Size.Y,
            //                ClearBufferMask.ColorBufferBit,
            //                BlitFramebufferFilter.Nearest
            //            )

            //            gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0u)
            //            gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0u)

            //            gl.DeleteFramebuffer sf
            //            gl.DeleteFramebuffer df
            //        | _ ->
            //            raise (System.NotImplementedException())
            //    | _ ->
            //        raise (System.NotImplementedException())
            //)
            

        member this.Copy(src: IBackendTexture, srcBaseSlice: int, srcBaseLevel: int, dst: IBackendTexture, dstBaseSlice: int, dstBaseLevel: int, slices: int, levels: int): unit = 
            raise (System.NotImplementedException())
        member this.CopyAsync(srcBuffer, srcOffset, dstData, size) = raise (System.NotImplementedException())

        member this.CreateOcclusionQuery(precise) = raise (System.NotImplementedException())
        member this.CreatePipelineQuery(statistics) = raise (System.NotImplementedException())
        member this.CreateSparseTexture(size, levels, slices, dim, format, brickSize, maxMemory) = raise (System.NotImplementedException())
        member this.CreateStreamingTexture(mipMaps) = raise (System.NotImplementedException())
        member this.CreateTextureView(texture, levels, slices, isArray) = raise (System.NotImplementedException())
        member this.CreateTimeQuery() = raise (System.NotImplementedException())

        member this.Download(tex, tensor, fmt, offset, size): unit = 
            raise (System.NotImplementedException())
        member this.DownloadDepth(texture, level, slice, offset, target) = raise (System.NotImplementedException())
        member this.DownloadStencil(texture, level, slice, offset, target) = raise (System.NotImplementedException())
        member this.ResourceManager = raise (System.NotImplementedException())
        member this.Run(arg1, arg2) = raise (System.NotImplementedException())
        member this.Upload(tex, tensor, fmt, offset, size) = raise (System.NotImplementedException())


    new(ctx) = new Runtime(new Device(ctx))
    new(selector : string) = Runtime(new Device(WebGLContext.Create selector))
