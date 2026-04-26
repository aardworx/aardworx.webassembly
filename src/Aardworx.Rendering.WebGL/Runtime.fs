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

        member this.TryGetBufferView(name: Symbol): voption<BufferView> =
            match Map.tryFind name buffers with
            | Some (_, b) -> BufferView(AVal.cast<IBuffer> b, Map.find name attributes) |> ValueSome
            | _ -> ValueNone

        member this.UsedMemory: Mem = 
            manager.Capactiy |> Mem


type Runtime(device : Device, defaultCommandStreamMode : CommandStreamMode) as this =
    let manager = ResourceManager(device)
    do device.Runtime <- this :> IRuntime

    // Per-Runtime scratch PBO for ReadPixels' large-read path. Reused across
    // calls to avoid per-readback GenBuffer / BufferData / DeleteBuffer churn,
    // which is a measurable cost on macOS Safari (Metal-bridge IPC per call).
    // Lazily created on first big read; resized only when a larger one needs it.
    let mutable scratchPbo : uint32 = 0u
    let mutable scratchPboSize : unativeint = 0un

    /// Threshold below which ReadPixels skips the PBO entirely and reads
    /// directly into the pinned client array. The PBO indirection costs ~6
    /// extra GL calls; for tiny reads (1-pixel picks, small thumbnails) those
    /// dominate the actual data transfer. 4 KiB covers up to 32x32 RGBA8.
    static let directReadByteThreshold = 4096un

    /// Ensure the scratch PBO exists and is at least `needed` bytes; resize
    /// in place if needed. Returns the GL handle. Runs on the GL thread via
    /// device.Run.
    let ensureScratchPbo (needed : unativeint) =
        device.Run (fun gl ->
            if scratchPbo = 0u then
                scratchPbo <- gl.GenBuffer()
            if needed > scratchPboSize then
                // Orphan + reallocate. STREAM_READ hints that we read once
                // and discard, which matches the readback access pattern.
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, scratchPbo)
                gl.BufferData(BufferTargetARB.PixelPackBuffer, needed, VoidPtr.zero, BufferUsageARB.StreamRead)
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                scratchPboSize <- needed
            scratchPbo
        )

    member x.Device = device
    member x.ResourceManager = manager

    member this.CompileRender(fboSignature : IFramebufferSignature, objects : aset<IRenderObject>) =
        let signature = fboSignature :?> FramebufferSignature

        let mode = defaultCommandStreamMode

        new RenderTask(manager, signature, mode, objects) :> IRenderTask

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
    
    
    
    member this.ResolveMultisamples(src : IFramebufferOutput, dst : TextureImage, imgTrafo) =
        if imgTrafo <> ImageTrafo.Identity then Log.warn "ResolveMultisamples ignores ImageTrafo: %A" imgTrafo
        device.RunCommand (fun cmd ->
            match src with
            | :? TextureLevel as l ->
                cmd.Blit(l.[0], dst, true)
            | :? ITextureLevel as l ->
                let tex = l.Texture :?> Texture
                cmd.Blit(tex.[l.Level, l.Slices.Min], dst, true)
            | :? Renderbuffer as r ->
                cmd.Blit(r, dst, true)
            | _ ->
    
                failf "bad input"
        )

    interface IRuntime with
        
        member x.Blit(src : IFramebufferOutput, srcRange : Box3i, dst : IFramebufferOutput, dstRange : Box3i) =
            let inline size (range : Box3i) = range.Max - range.Min
        
            let s = size srcRange
            let d = size dstRange
            if s = d then
                if src.Samples = dst.Samples then
                    x.Copy(src, srcRange.Min, dst, dstRange.Min, s)
                else
                    if srcRange.Min = V3i.Zero && dstRange.Min = V3i.Zero && s.XY = src.Size then
                        let dst = 
                            match dst with
                            | :? TextureLevel as l -> l.[0]
                            | :? ITextureLevel as l ->
                                let tex = l.Texture :?> Texture
                                tex.[l.Level, l.Slices.Min]
                            | _ -> failwith "dst not a texture"
                            
                        x.ResolveMultisamples(src, dst, ImageTrafo.Identity)
                    else
                        failwith "bad resolve"
            else
                failwith "not implemented"
        
        
        member x.SupportsLayeredShaderInputs = false
        member x.ShaderDepthRange = Range1f(-1.0f, 1.0f)
        member x.DebugConfig =
            if device.Debug then DebugLevel.Full
            else DebugLevel.None
            
        // member this.AssembleModule(effect, signature, _mode) =
        //     let signature = signature :?> FramebufferSignature
        //     signature.AssembleModule(effect)

        //member x.DebugLevel = DebugLevel.None
        member x.MaxRayRecursionDepth = 0
        member x.SupportsRaytracing = false
        member x.SupportsInvocationReorder = false
        member x.SupportsMicromaps = false
        member x.SupportsPositionFetch = false
        member x.GetMaxMicromapSubdivisionLevel(_format) = 0
        member x.DebugLabelsEnabled = false

        member x.CreateAccelerationStructure(_, _) =
            failwith "not rt"

        member x.PrepareMicromap(_) =
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
                let byteCount = unativeint img.Array.Length

                try
                    if byteCount <= directReadByteThreshold then
                        // Small-read fast path. Skip the PBO entirely — for 1-pixel picks
                        // and other tiny reads the PBO indirection (Gen/Bind/BufferData/
                        // GetBufferSubData/Delete) costs more than the data transfer
                        // itself. macOS Safari's WebGL2 → Metal IPC makes those extra
                        // calls especially expensive.
                        device.RunCommand (fun gl ->
                            gl.BaseStream.BindFramebuffer(FramebufferTarget.Framebuffer, src.Handle)
                            gl.BaseStream.ReadBuffer (unbox<ReadBufferMode> (int ReadBufferMode.ColorAttachment0 + index))
                            gl.BaseStream.ReadPixels(
                                offset.X, src.Size.Y - size.Y - offset.Y,
                                uint32 size.X, uint32 size.Y, pfmt, ptyp,
                                gc.AddrOfPinnedObject())
                            gl.BaseStream.BindFramebuffer(FramebufferTarget.Framebuffer, 0u))
                    else
                        // Large-read path: use a Device-cached PBO that's lazily created
                        // and only ever resized upward. The original code Gen/BufferData/
                        // Delete'd a fresh PBO every call, which thrashes the Metal-backed
                        // allocator on Safari and also generates GC pressure on Chrome.
                        let pbo = ensureScratchPbo byteCount
                        device.RunCommand (fun gl ->
                            gl.BaseStream.BindFramebuffer(FramebufferTarget.Framebuffer, src.Handle)
                            gl.BaseStream.ReadBuffer (unbox<ReadBufferMode> (int ReadBufferMode.ColorAttachment0 + index))
                            gl.BaseStream.BindBuffer(BufferTargetARB.PixelPackBuffer, pbo)
                            gl.BaseStream.ReadPixels(
                                offset.X, src.Size.Y - size.Y - offset.Y,
                                uint32 size.X, uint32 size.Y, pfmt, ptyp, 0n)
                            gl.BaseStream.GetBufferSubData(
                                BufferTargetARB.PixelPackBuffer, 0n, byteCount,
                                gc.AddrOfPinnedObject())
                            gl.BaseStream.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                            gl.BaseStream.BindFramebuffer(FramebufferTarget.Framebuffer, 0u))

                    // glReadPixels yields bottom-up rows; flip in place so the
                    // returned PixImage's backing array is top-down (matches the
                    // .Volume.Data contract — a TransformedPixImage view would
                    // leave the bytes bottom-up).
                    // Buffer.BlockCopy operates on raw bytes, so it works for
                    // any primitive element type (byte/float32/int32/...). The
                    // previous `:?> byte[]` cast threw InvalidCastException for
                    // non-byte formats like Rgba32f (used by the pick buffer),
                    // silently breaking picking.
                    let arr = img.Array
                    let rowBytes = int (abs img.VolumeInfo.DY)
                    let rows = int img.VolumeInfo.SY
                    let tmp = Array.zeroCreate<byte> rowBytes
                    for y in 0 .. rows / 2 - 1 do
                        let topOff = y * rowBytes
                        let botOff = (rows - 1 - y) * rowBytes
                        System.Buffer.BlockCopy(arr, topOff, tmp, 0, rowBytes)
                        System.Buffer.BlockCopy(arr, botOff, arr, topOff, rowBytes)
                        System.Buffer.BlockCopy(tmp, 0, arr, botOff, rowBytes)
                    img

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

        member this.CompileCompute(_compute) =
            failf "ComputeShaders not supported"

        member this.MaxLocalSize = 
            failf "ComputeShaders not supported"

        member this.CreateInputBinding(_compute, _uniforms) = 
            failf "ComputeShaders not supported"

        member this.CreateComputeShader(_compute) = 
            failf "ComputeShaders not supported"

        member this.CompileRender(fboSignature, objects) =
            this.CompileRender(fboSignature, objects)

        member this.CompileClear(fboSignature, clearValues : aval<ClearValues>) =
            let fboSignature = fboSignature :?> FramebufferSignature
            
            let fboHandle = NativePtr.alloc 1
            let cmd = device.CreateCommandStream(defaultCommandStreamMode)
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
                    let dst = output.Framebuffer :?> Framebuffer
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

        member this.PrepareEffect(signature, effect, mode) =
            manager.CreateProgram(unbox signature, effect) :> IBackendSurface

            
        member this.Copy(srcBuffer: IBackendBuffer, srcOffset: uint64, dstBuffer: IBackendBuffer, dstOffset: uint64, size: uint64, _flush: bool): unit =
            let srcBuffer = srcBuffer :?> Buffer
            let dstBuffer = dstBuffer :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcBuffer.Sub(int64 srcOffset, int64 size), dstBuffer.Sub(int64 dstOffset, int64 size))
            )

        member this.Download(srcBuffer: IBackendBuffer, srcOffset: uint64, dstData: nativeint, size: uint64): unit =
            let srcBuffer = srcBuffer :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcBuffer.Sub(int64 srcOffset, int64 size), dstData)
            )
        member this.Upload(srcData: nativeint, dst: IBackendBuffer, dstOffset: uint64, size: uint64, _flush: bool): unit =
            let dstBuffer = dst :?> Buffer
            device.RunCommand (fun cmd ->
                cmd.Copy(srcData, dstBuffer.Sub(int64 dstOffset, int64 size))
            )

        
          
        member this.Upload<'T when 'T : unmanaged>(tex : ITextureSubResource, tensor : NativeTensor4<'T>, fmt : Col.Format, offset : V3i, size : V3i) =
            let tfmt = tex.Texture.Format
            use pbo = device.GetPixelBuffer(tfmt, size)
            pbo.Write(tensor, fmt)
            let dst = tex.Texture :?> Texture
            let dstSub = dst.[tex.Level, tex.Slice, offset .. offset + size - V3i.III]
            
            device.RunCommand (fun cmd ->
                cmd.Copy(pbo, dstSub)
            )
            

        member this.Copy(src: IBackendTexture, srcBaseSlice: int, srcBaseLevel: int, dst: IBackendTexture, dstBaseSlice: int, dstBaseLevel: int, slices: int, levels: int): unit = 
            raise (System.NotImplementedException())
        member this.DownloadAsync(srcBuffer, srcOffset, dstData, size) = raise (System.NotImplementedException())

        member this.CreateOcclusionQuery(precise) = raise (System.NotImplementedException())
        member this.CreatePipelineQuery(statistics) = raise (System.NotImplementedException())
        member this.CreateSparseTexture(size, levels, slices, dim, format, brickSize, maxMemory) = raise (System.NotImplementedException())
        member this.CreateStreamingTexture(mipMaps) = raise (System.NotImplementedException())
        member this.CreateTextureView(texture, levels, slices, isArray) = raise (System.NotImplementedException())
        member this.CreateTimeQuery() = raise (System.NotImplementedException())
        member this.CreateLodRenderer(config, data) = raise (System.NotImplementedException())

        member this.Download<'T when 'T : unmanaged>(tex : ITextureSubResource, tensor : NativeTensor4<'T>, fmt : Col.Format, offset : V3i, size : V3i) : unit =
            // WebGL has no glGetTexImage; use the FBO + glReadPixels trick.
            let texture = tex.Texture :?> Texture
            let level = tex.Level
            let slice = tex.Slice

            // Compute effective size (V3i.Zero = full target)
            let levelSize =
                let s = texture.Size
                let div n = max 1 (n >>> level)
                V3i(div s.X, div s.Y, div s.Z)

            let size = if size = V3i.Zero then V3i(levelSize.X - offset.X, levelSize.Y - offset.Y, max 1 (levelSize.Z - offset.Z)) else size

            if size.Z > 1 then
                failwith "WebGL Download: 3D / multi-slice regions not supported (only single 2D layer at a time)"

            match texture.Dimension with
            | TextureDimension.Texture2D | TextureDimension.TextureCube -> ()
            | d -> failwithf "WebGL Download: unsupported texture dimension %A" d

            // ReadPixels into a PixelBuffer, then copy out to the tensor.
            let pboSize = V3i(size.X, size.Y, 1)
            use pbo = device.GetPixelBuffer(texture.Format, pboSize)

            device.Run (fun gl ->
                let mutable fbo = 0u
                gl.GenFramebuffers(1u, &fbo)
                let prevFbo =
                    let mutable v = 0
                    gl.GetInteger(GLEnum.FramebufferBinding, &v)
                    uint32 v
                let prevPack =
                    let mutable v = 0
                    gl.GetInteger(GLEnum.PixelPackBufferBinding, &v)
                    uint32 v
                try
                    gl.BindFramebuffer(FramebufferTarget.Framebuffer, fbo)

                    let attachTarget =
                        match texture.Dimension with
                        | TextureDimension.TextureCube ->
                            // slice 0..5 maps to +X..-Z
                            unbox<TextureTarget> (int TextureTarget.TextureCubeMapPositiveX + slice)
                        | _ ->
                            TextureTarget.Texture2D

                    if texture.Dimension = TextureDimension.Texture2D && texture.Layers.IsSome then
                        gl.FramebufferTextureLayer(
                            FramebufferTarget.Framebuffer,
                            FramebufferAttachment.ColorAttachment0,
                            texture.Handle, level, slice)
                    else
                        gl.FramebufferTexture2D(
                            FramebufferTarget.Framebuffer,
                            FramebufferAttachment.ColorAttachment0,
                            attachTarget,
                            texture.Handle, level)

                    let status = gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer)
                    if status <> GLEnum.FramebufferComplete then
                        failwithf "WebGL Download: framebuffer incomplete (0x%X)" (int status)

                    gl.ReadBuffer(ReadBufferMode.ColorAttachment0)
                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, pbo.Handle)

                    // ReadPixels reads bottom-up; our offset.Y is top-down — flip Y.
                    let yReadGL = levelSize.Y - offset.Y - size.Y
                    gl.ReadPixels(offset.X, yReadGL, uint32 size.X, uint32 size.Y, pbo.PixelFormat, pbo.PixelType, VoidPtr.zero)

                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, prevPack)
                finally
                    gl.BindFramebuffer(FramebufferTarget.Framebuffer, prevFbo)
                    gl.DeleteFramebuffers(1u, &fbo)
            )

            // Copy from PBO to user tensor (with Y-flip to compensate for GL bottom-up).
            pbo.Read(tensor, fmt)
        member this.DownloadDepth(texture, level, slice, offset, target) = raise (System.NotImplementedException())
        member this.DownloadStencil(texture, level, slice, offset, target) = raise (System.NotImplementedException())
        // member this.ResourceManager = raise (System.NotImplementedException())
        // member this.Run(arg1, arg2) = raise (System.NotImplementedException())


    // new(ctx) = new Runtime(new Device(ctx))
    // new(selector : string) = Runtime(new Device(WebGLContext.Create selector))
