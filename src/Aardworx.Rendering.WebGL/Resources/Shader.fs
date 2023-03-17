namespace Aardworx.Rendering.WebGL

open System
open System.Runtime.InteropServices
open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardvark.Base
open Aardvark.Rendering
open FShade
open FShade.GLSL
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

[<AutoOpen>]
module internal RenderBufferFormatExtensions =
    module TextureFormat =
        let toShaderType =
            LookupTable.lookupTable [
                TextureFormat.R3G3B2, typeof<V3d>
                TextureFormat.Rgb4, typeof<V3d>
                TextureFormat.Rgb5, typeof<V3d>
                TextureFormat.Rgb8, typeof<V3d>
                TextureFormat.Rgb10, typeof<V3d>
                TextureFormat.Rgb12, typeof<V3d>
                TextureFormat.Rgb16, typeof<V3d>
                TextureFormat.Rgba2, typeof<V4d>
                TextureFormat.Rgba4, typeof<V4d>
                TextureFormat.Rgba8, typeof<V4d>
                TextureFormat.Rgb10A2, typeof<V4d>
                TextureFormat.Rgba12, typeof<V4d>
                TextureFormat.Rgba16, typeof<V4d>
                TextureFormat.DepthComponent16, typeof<float>
                TextureFormat.DepthComponent24, typeof<float>
                TextureFormat.DepthComponent32, typeof<float>
                TextureFormat.R8, typeof<float>
                TextureFormat.R16, typeof<float>
                TextureFormat.Rg8, typeof<V2d>
                TextureFormat.Rg16, typeof<V2d>
                TextureFormat.R16f, typeof<float>
                TextureFormat.R32f, typeof<float>
                TextureFormat.Rg16f, typeof<V2d>
                TextureFormat.Rg32f, typeof<V2d>
                TextureFormat.R8i, typeof<int>
                TextureFormat.R8ui, typeof<int>
                TextureFormat.R16i, typeof<int>
                TextureFormat.R16ui, typeof<int>
                TextureFormat.R32i, typeof<int>
                TextureFormat.R32ui, typeof<int>
                TextureFormat.Rg8i, typeof<V2i>
                TextureFormat.Rg8ui, typeof<V2i>
                TextureFormat.Rg16i, typeof<V2i>
                TextureFormat.Rg16ui, typeof<V2i>
                TextureFormat.Rg32i, typeof<V2i>
                TextureFormat.Rg32ui, typeof<V2i>
                TextureFormat.Rgba32f, typeof<V4d>
                TextureFormat.Rgb32f, typeof<V3d>
                TextureFormat.Rgba16f, typeof<V4d>
                TextureFormat.Rgb16f, typeof<V3d>
                TextureFormat.Depth24Stencil8, typeof<float>
                TextureFormat.R11fG11fB10f, typeof<V3d>
                TextureFormat.Rgb9E5, typeof<V3d>
                TextureFormat.Srgb8, typeof<V3d>
                TextureFormat.Srgb8Alpha8, typeof<V4d>
                TextureFormat.DepthComponent32f, typeof<float>
                TextureFormat.Depth32fStencil8, typeof<float>
                TextureFormat.StencilIndex8, typeof<int>
                TextureFormat.Rgba32ui, typeof<V4i>
                TextureFormat.Rgb32ui, typeof<V3i>
                TextureFormat.Rgba16ui, typeof<V3i>
                TextureFormat.Rgb16ui, typeof<V3i>
                TextureFormat.Rgba8ui, typeof<V4i>
                TextureFormat.Rgb8ui, typeof<V3i>
                TextureFormat.Rgba32i, typeof<V4i>
                TextureFormat.Rgb32i, typeof<V3i>
                TextureFormat.Rgba16i, typeof<V4i>
                TextureFormat.Rgb16i, typeof<V3i>
                TextureFormat.Rgba8i, typeof<V4i>
                TextureFormat.Rgb8i, typeof<V3i>
                TextureFormat.Rgb10A2ui, typeof<V4i>
            ]

type Shader(device : Device, handle : uint32) =
    inherit Resource(device, "Shader", handle, 0L)

    override x.Destroy(gl : GL) =
        gl.DeleteShader handle

type Program(device : Device, inputSemantics : Map<int, Symbol>, iface : GLSLProgramInterface, samplers : Sampler[], handle : uint32, kill : Program -> unit) =
    inherit Resource(device, "Program", handle, 0L)
    let onDispose = Event<unit>()


    let inputSlots = inputSemantics |> Map.toSeq |> Seq.map (fun (a,b) -> b, a) |> Map.ofSeq

    [<CLIEvent>]
    member x.OnDispose = onDispose.Publish
    member x.Interface = iface
    member x.InputSemantics = inputSemantics
    member x.InputSlots = inputSlots
    member x.Samplers = samplers

    interface IBackendSurface with
        member x.Handle = handle

    override x.Destroy(gl : GL) =
        gl.DeleteProgram handle
        onDispose.Trigger()
        kill x


type UniformBuffer(pool : UniformBufferPool, device : Device, manager : Management.ChunkedMemoryManager<_>, block : Management.Block<Buffer * nativeint>) =
    inherit ResourceBase(device, "UniformBuffer", 0L)
    
    member internal x.CheckDisposed() =
        if block.IsFree then raise <| ObjectDisposedException "UniformBuffer"
    
    member x.Pool = pool

    member x.Device =
        x.CheckDisposed()
        device

    member x.Buffer =
        x.CheckDisposed()
        block.Memory.Value |> fst

    member x.Offset =
        x.CheckDisposed()
        block.Offset

    member x.Size =
        x.CheckDisposed()
        block.Size

    override x.Free() =
        if not block.IsFree then
            manager.Free block



    member x.Write(action : nativeint -> 'a) =
        let b, ptr = block.Memory.Value
        let res = action (ptr + block.Offset)
        pool.AddDirty(b, ptr, block.Offset)
        device.AddPending(pool, fun gl -> pool.Flush gl)
        res

and UniformBufferPool(device : Device, size : int, fields : list<GLSLUniformBufferField>) =
    inherit ResourceBase(device, "UniformBufferPool", 0L)

    static let count = 1024

    static let mem (device : Device) =
        {
            Management.malloc = fun s -> 
                let res =
                    device.Run (fun _gl ->
                        let buffer = device.CreateBuffer(int64 s, BufferUsage.Client ||| BufferUsage.Uniform)
                        let ptr = Marshal.AllocHGlobal s
                        buffer, ptr
                    )
                res

            Management.mfree = fun (b,p) _s -> 
                b.Dispose()
                Marshal.FreeHGlobal p

            Management.mcopy = fun (src, psrc) srcOffset (dst, pdst) dstOffset size -> 
                if size > 0n then
                    Marshal.Copy(psrc, pdst, size)
                    device.Run(fun gl ->
                        gl.BindBuffer(BufferTargetARB.CopyReadBuffer, src.Handle)
                        gl.BindBuffer(BufferTargetARB.CopyWriteBuffer, dst.Handle)
                        gl.CopyBufferSubData(
                            CopyBufferSubDataTarget.CopyReadBuffer,
                            CopyBufferSubDataTarget.CopyWriteBuffer,
                            srcOffset, dstOffset,
                            unativeint size
                        )
                        gl.BindBuffer(BufferTargetARB.CopyReadBuffer, 0u)
                        gl.BindBuffer(BufferTargetARB.CopyWriteBuffer, 0u)
                    )
            Management.mrealloc = fun _a _o _n ->
                failf "not implemented"
        }

    let dirtyLock = obj()
    let mutable dirty = Dict<Buffer, struct(nativeint * System.Collections.Generic.HashSet<nativeint>)>()
    let mutable fullDirty =  Dict<Buffer, nativeint>()
    let align = nativeint device.Info.BufferLimits.MinUniformBufferAlign

    let manager = 
        new Management.ChunkedMemoryManager<_>(
            mem device, max align (nativeint size) * nativeint count
            // TODO: flexible count??
        )

    let cache = Dict<list<int * GLSLType * IAdaptiveValue>, AdaptiveUniformBuffer>()

    member x.Fields = fields

    member internal x.AddDirty(buffer : Buffer, ptr : nativeint, offset : nativeint) : unit =
        lock dirtyLock (fun () ->
            if not (fullDirty.ContainsKey buffer) then
                let struct(_, set) = dirty.GetOrCreate(buffer, fun _ -> struct(ptr, System.Collections.Generic.HashSet()))
                set.Add offset |> ignore
                if set.Count * 16 > count then
                    dirty.Remove buffer |> ignore
                    fullDirty.[buffer] <- ptr
        )

    member x.Flush(gl : GL) =
        let struct(dirty, full) =
            lock dirtyLock (fun () ->
                let d = dirty
                let f = fullDirty
                dirty <- Dict()
                fullDirty <- Dict()
                struct(d, f)
            )

        if dirty.Count > 0 || full.Count > 0 then
            for KeyValue(buffer, src) in full do
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, buffer.Handle)
                gl.BufferSubData(BufferTargetARB.PixelPackBuffer, 0n, unativeint buffer.Size, VoidPtr.ofNativeInt src)
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)

            for KeyValue(buffer, struct (src, offsets)) in dirty do
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, buffer.Handle)
                for offset in offsets do
                    gl.BufferSubData(BufferTargetARB.PixelPackBuffer, offset, unativeint size, VoidPtr.ofNativeInt (src + offset))
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
        
    member x.NewBuffer() =
        let block = manager.Alloc(align ,nativeint size)
        new UniformBuffer(x, device, manager, block)

    member x.CreateUniformBuffer(tryGetValue : string -> voption<IAdaptiveValue>) =
        let inputs =
            fields |> List.choose (fun f ->
                match tryGetValue f.ufName with
                | ValueSome value ->
                    Some (f.ufOffset, f.ufType, value)
                | ValueNone ->
                    Log.warn "could not find uniform value: %A" f.ufName
                    None
            )

        cache.GetOrCreate(inputs, fun inputs ->
            let handle = ref Unchecked.defaultof<UniformBuffer>

            let create () = 
                let dst = x.NewBuffer()
                handle.Value <- dst
                let writers = 
                    inputs |> List.map (fun (offset, typ, value) ->
                        let writer = 
                            value.Accept {
                                new IAdaptiveValueVisitor<_> with
                                    member x.Visit(_value : aval<'a>) =
                                        UniformWriters.getWriter 0 typ typeof<'a>
                            }
                        offset, writer, value
                    )

                
                let updater = 
                    AVal.custom (fun token ->
                        dst.Write(fun ptr ->
                            for offset, writer, value in writers do
                                writer.Write(token, value, ptr + nativeint offset)

                        )
                    )
                dst, updater

            let destroy() =
                handle.Value.Dispose()

            AdaptiveUniformBuffer(device, create, destroy)
        )

    override x.Free() =
        manager.Dispose()
        cache.Clear()

and AdaptiveUniformBuffer(device : Device, create : unit -> UniformBuffer * aval<unit>, destroy : unit -> unit) =
    
    let mutable refCount = 0
    let mutable buffer = Unchecked.defaultof<UniformBuffer>
    let mutable update = Unchecked.defaultof<aval<unit>>

    member x.Acquire() =
        lock x (fun () ->
            let o = refCount
            if o = 0 then
                let b, u = create()
                buffer <- b
                update <- u
                refCount <- 1
            else 
                refCount <- o + 1
        )

    member x.Release() =
        lock x (fun () ->
            let o = refCount
            if o = 1 then
                destroy()
                buffer <- Unchecked.defaultof<_>
                update <- Unchecked.defaultof<_>
                refCount <- 0
            elif o > 1 then 
                refCount <- refCount - 1
            else
                Log.warn "releasing disposed AdaptiveUniformBuffer"
        )

    member x.Device = device
    member x.Buffer = buffer
    member x.Update(t : AdaptiveToken) = update.GetValue t
    member internal x.Updater = update

module internal ShaderReflection =
    module private UniformType =
        let rec toGLSLType (t : UniformType) =
            match t with
            | UniformType.Bool -> GLSLType.Bool
            | UniformType.BoolVec2 -> GLSLType.Vec(2, GLSLType.Bool)
            | UniformType.BoolVec3 -> GLSLType.Vec(3, GLSLType.Bool)
            | UniformType.BoolVec4 -> GLSLType.Vec(4, GLSLType.Bool)

            | UniformType.Double -> GLSLType.Float 64
            | UniformType.DoubleVec2 -> GLSLType.Vec(2, GLSLType.Float 64)
            | UniformType.DoubleVec3 -> GLSLType.Vec(3, GLSLType.Float 64)
            | UniformType.DoubleVec4 -> GLSLType.Vec(4, GLSLType.Float 64)
            | UniformType.DoubleMat2 -> GLSLType.Mat(2, 2, GLSLType.Float 64)
            | UniformType.DoubleMat2x3 -> GLSLType.Mat(2, 3, GLSLType.Float 64)
            | UniformType.DoubleMat2x4 -> GLSLType.Mat(2, 4, GLSLType.Float 64)
            | UniformType.DoubleMat3x2 -> GLSLType.Mat(3, 2, GLSLType.Float 64)
            | UniformType.DoubleMat3 -> GLSLType.Mat(3, 3, GLSLType.Float 64)
            | UniformType.DoubleMat3x4 -> GLSLType.Mat(3, 4, GLSLType.Float 64)
            | UniformType.DoubleMat4x2 -> GLSLType.Mat(4, 2, GLSLType.Float 64)
            | UniformType.DoubleMat4x3 -> GLSLType.Mat(4, 3, GLSLType.Float 64)
            | UniformType.DoubleMat4 -> GLSLType.Mat(4, 4, GLSLType.Float 64)
            
            | UniformType.Float -> GLSLType.Float 32
            | UniformType.FloatVec2 -> GLSLType.Vec(2, GLSLType.Float 32)
            | UniformType.FloatVec3 -> GLSLType.Vec(3, GLSLType.Float 32)
            | UniformType.FloatVec4 -> GLSLType.Vec(4, GLSLType.Float 32)
            | UniformType.FloatMat2 -> GLSLType.Mat(2, 2, GLSLType.Float 32)
            | UniformType.FloatMat2x3 -> GLSLType.Mat(2, 3, GLSLType.Float 32)
            | UniformType.FloatMat2x4 -> GLSLType.Mat(2, 4, GLSLType.Float 32)
            | UniformType.FloatMat3x2 -> GLSLType.Mat(3, 2, GLSLType.Float 32)
            | UniformType.FloatMat3 -> GLSLType.Mat(3, 3, GLSLType.Float 32)
            | UniformType.FloatMat3x4 -> GLSLType.Mat(3, 4, GLSLType.Float 32)
            | UniformType.FloatMat4x2 -> GLSLType.Mat(4, 2, GLSLType.Float 32)
            | UniformType.FloatMat4x3 -> GLSLType.Mat(4, 3, GLSLType.Float 32)
            | UniformType.FloatMat4 -> GLSLType.Mat(4, 4, GLSLType.Float 32)

            | UniformType.Int -> GLSLType.Int(true, 32)
            | UniformType.IntVec2 -> GLSLType.Vec(2, GLSLType.Int(true, 32))
            | UniformType.IntVec3 -> GLSLType.Vec(3, GLSLType.Int(true, 32))
            | UniformType.IntVec4 -> GLSLType.Vec(4, GLSLType.Int(true, 32))
            | UniformType.UnsignedInt -> GLSLType.Int(false, 32)
            | UniformType.UnsignedIntVec2 -> GLSLType.Vec(2, GLSLType.Int(false, 32))
            | UniformType.UnsignedIntVec3 -> GLSLType.Vec(3, GLSLType.Int(false, 32))
            | UniformType.UnsignedIntVec4 -> GLSLType.Vec(4, GLSLType.Int(false, 32))
            | t -> failf "bad uniform-type: %A" t

        let isSampler (t : UniformType) =
            match t with 
            | UniformType.Sampler1D
            | UniformType.Sampler2D
            | UniformType.Sampler3D
            | UniformType.SamplerCube
            | UniformType.Sampler1DShadow
            | UniformType.Sampler2DShadow
            | UniformType.Sampler2DRect
            | UniformType.Sampler2DRectShadow
            | UniformType.Sampler1DArray
            | UniformType.Sampler2DArray
            | UniformType.SamplerBuffer
            | UniformType.Sampler1DArrayShadow
            | UniformType.Sampler2DArrayShadow
            | UniformType.SamplerCubeShadow
            | UniformType.IntSampler1D
            | UniformType.IntSampler2D
            | UniformType.IntSampler3D
            | UniformType.IntSamplerCube
            | UniformType.IntSampler2DRect
            | UniformType.IntSampler1DArray
            | UniformType.IntSampler2DArray
            | UniformType.IntSamplerBuffer
            | UniformType.UnsignedIntSampler1D
            | UniformType.UnsignedIntSampler2D
            | UniformType.UnsignedIntSampler3D
            | UniformType.UnsignedIntSamplerCube
            | UniformType.UnsignedIntSampler2DRect
            | UniformType.UnsignedIntSampler1DArray
            | UniformType.UnsignedIntSampler2DArray
            | UniformType.UnsignedIntSamplerBuffer
            | UniformType.SamplerCubeMapArray
            | UniformType.SamplerCubeMapArrayShadow
            | UniformType.IntSamplerCubeMapArray
            | UniformType.UnsignedIntSamplerCubeMapArray
            | UniformType.Sampler2DMultisample
            | UniformType.IntSampler2DMultisample
            | UniformType.UnsignedIntSampler2DMultisample
            | UniformType.Sampler2DMultisampleArray
            | UniformType.IntSampler2DMultisampleArray
            | UniformType.UnsignedIntSampler2DMultisampleArray ->
                true
            | _ ->
                false

        let toSamplerType (t : UniformType) =
            match t with 
            | UniformType.Sampler1D ->
                Some { 
                    original = typeof<Sampler1d>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.Sampler2D ->
                Some { 
                    original = typeof<Sampler2d>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.Sampler3D ->
                Some { 
                    original = typeof<Sampler3d>
                    dimension = SamplerDimension.Sampler3d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.SamplerCube ->
                Some { 
                    original = typeof<SamplerCube>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.Sampler1DShadow ->
                Some { 
                    original = typeof<Sampler1dShadow>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = true
                    isArray = false 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.Sampler2DShadow ->
                Some { 
                    original = typeof<Sampler2dShadow>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = true
                    isArray = false 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.Sampler1DArray ->
                Some { 
                    original = typeof<Sampler1dArray>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.Sampler2DArray ->
                Some { 
                    original = typeof<Sampler2dArray>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.Sampler1DArrayShadow ->
                Some { 
                    original = typeof<Sampler1dArrayShadow>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = true
                    isArray = true 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.Sampler2DArrayShadow ->
                Some { 
                    original = typeof<Sampler2dArrayShadow>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = true
                    isArray = true 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.SamplerCubeShadow ->
                Some { 
                    original = typeof<SamplerCubeArrayShadow>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = true
                    isArray = true 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.IntSampler1D ->
                Some { 
                    original = typeof<IntSampler1d>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.IntSampler2D ->
                Some { 
                    original = typeof<IntSampler2d>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.IntSampler3D ->
                Some { 
                    original = typeof<IntSampler3d>
                    dimension = SamplerDimension.Sampler3d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.IntSamplerCube ->
                Some { 
                    original = typeof<IntSamplerCube>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }

            | UniformType.IntSampler1DArray ->
                Some { 
                    original = typeof<IntSampler1d>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.IntSampler2DArray ->
                Some { 
                    original = typeof<IntSampler2d>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
                
            | UniformType.UnsignedIntSampler1D ->
                Some { 
                    original = typeof<IntSampler1d>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.UnsignedIntSampler2D ->
                Some { 
                    original = typeof<IntSampler2d>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.UnsignedIntSampler3D ->
                Some { 
                    original = typeof<IntSampler3d>
                    dimension = SamplerDimension.Sampler3d
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.UnsignedIntSamplerCube ->
                Some { 
                    original = typeof<IntSamplerCube>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = false 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }

            | UniformType.UnsignedIntSampler1DArray ->
                Some { 
                    original = typeof<IntSampler1d>
                    dimension = SamplerDimension.Sampler1d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.UnsignedIntSampler2DArray ->
                Some { 
                    original = typeof<IntSampler2d>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.SamplerCubeMapArray ->
                Some { 
                    original = typeof<SamplerCubeArray>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Float 32)
                }
            | UniformType.SamplerCubeMapArrayShadow ->
                Some { 
                    original = typeof<SamplerCubeArrayShadow>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = true
                    isArray = true 
                    isMS = false
                    valueType = Float 32
                }
            | UniformType.IntSamplerCubeMapArray ->
                Some { 
                    original = typeof<IntSamplerCubeArray>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.UnsignedIntSamplerCubeMapArray ->
                Some { 
                    original = typeof<IntSamplerCubeArray>
                    dimension = SamplerDimension.SamplerCube
                    isShadow = false
                    isArray = true 
                    isMS = false
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.Sampler2DMultisample ->
                Some { 
                    original = typeof<Sampler2dMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = true
                    valueType = Vec(4, Float 32)
                }
            | UniformType.IntSampler2DMultisample ->
                Some { 
                    original = typeof<IntSampler2dMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = true
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.UnsignedIntSampler2DMultisample ->
                Some { 
                    original = typeof<IntSampler2dMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = false 
                    isMS = true
                    valueType = Vec(4, Int(false, 32))
                }
            | UniformType.Sampler2DMultisampleArray ->
                Some { 
                    original = typeof<Sampler2dArrayMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = true
                    valueType = Vec(4, Float 32)
                }
            | UniformType.IntSampler2DMultisampleArray ->
                Some { 
                    original = typeof<IntSampler2dArrayMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = true
                    valueType = Vec(4, Int(true, 32))
                }
            | UniformType.UnsignedIntSampler2DMultisampleArray ->
                Some { 
                    original = typeof<IntSampler2dArrayMS>
                    dimension = SamplerDimension.Sampler2d
                    isShadow = false
                    isArray = true 
                    isMS = true
                    valueType = Vec(4, Int(false, 32))
                }
            | _ ->
                None

    module private AttributeType =
        let rec toGLSLType (t : AttributeType) =
            UniformType.toGLSLType (unbox (int t))

    let validateInterface (device : Device) (iface : GLSLProgramInterface) (program : uint32) =
        device.Run(fun gl ->

            
            let inputs =
                let mutable attribCount = 0
                gl.GetProgram(program, ProgramPropertyARB.ActiveAttributes, &attribCount)

                List.init attribCount (fun ai ->
                    let mutable size = 0
                    let mutable typ = AttributeType.Bool
                    let name = gl.GetActiveAttrib(program, uint32 ai, &size, &typ)
                    let l = gl.GetAttribLocation(program, name)
                    { paramName = name; paramSemantic = name; paramLocation = l; paramInterpolation = None; paramType = AttributeType.toGLSLType typ }
                )
            
            for fsi in iface.inputs do
                match inputs |> List.tryFind (fun gli -> gli.paramSemantic = fsi.paramSemantic) with
                | Some gli ->
                    if gli.paramLocation <> fsi.paramLocation then
                        failf "invalid location for input %A: %d (should be %d)" fsi.paramSemantic fsi.paramLocation gli.paramLocation

                    if gli.paramType <> fsi.paramType then
                        failf "invalid type for input %A: %A (should be %A)" fsi.paramSemantic fsi.paramType gli.paramType
                | None ->
                    // not active???
                    ()



            let samplers =
                let mutable cnt = 0
                gl.GetProgram(program, ProgramPropertyARB.ActiveUniforms, &cnt)

                List.init cnt id |> List.choose (fun i ->
                    let mutable size = 0
                    let mutable typ = UniformType.Bool
                    let name = gl.GetActiveUniform(program,uint32 i, &size, &typ)
                    match UniformType.toSamplerType typ with
                    | Some typ ->
                        Some { 
                            samplerSet = -1; 
                            samplerBinding = -1; 
                            samplerName = name
                            samplerCount = size
                            samplerTextures = []
                            samplerType = typ
                        }
                    | None ->
                        None
                    

                )

            for gls in samplers do
                match MapExt.tryFind gls.samplerName iface.samplers with
                | Some fss ->
                    if fss.samplerType.original <> gls.samplerType.original then
                        failf "invalid type for sampler %A: %A (should be %A)" gls.samplerName fss.samplerType gls.samplerType
                    if fss.samplerCount <> gls.samplerCount then
                        failf "invalid count for sampler %A: %A (should be %A)" gls.samplerName fss.samplerCount gls.samplerCount
                | None ->
                    // not active???
                    ()

            // TODO: structs and arrays!
            let uniformBuffers =
                let mutable cnt = 0
                gl.GetProgram(program, ProgramPropertyARB.ActiveUniformBlocks, &cnt)

                List.init cnt (fun bi ->
                    let name =
                        let mutable len = 0u
                        let buffer = Array.zeroCreate<byte> 1024
                        gl.GetActiveUniformBlockName(program, uint32 bi, &len, Span buffer)
                        System.Text.Encoding.UTF8.GetString(buffer, 0, int len)

                    let mutable binding = 0
                    gl.GetActiveUniformBlock(program, uint32 bi, UniformBlockPName.UniformBlockBinding, &binding)
                    
                    let mutable size = 0
                    gl.GetActiveUniformBlock(program, uint32 bi, UniformBlockPName.UniformBlockDataSize, &size)

                    let mutable fieldCount = 0
                    gl.GetActiveUniformBlock(program, uint32 bi, UniformBlockPName.UniformBlockActiveUniforms, &fieldCount)

                    let indicesAsInt = Array.zeroCreate fieldCount
                    gl.GetActiveUniformBlock(program, uint32 bi, UniformBlockPName.UniformBlockActiveUniformIndices, Span indicesAsInt)

                    let indices = Array.map uint32 indicesAsInt

                    let offsets = Array.zeroCreate fieldCount
                    let typesAsInt = Array.zeroCreate fieldCount
                    gl.GetActiveUniforms(program, uint32 indices.Length, ReadOnlySpan indices, UniformPName.UniformOffset, Span offsets)
                    gl.GetActiveUniforms(program, uint32 indices.Length, ReadOnlySpan indices, UniformPName.UniformType, Span typesAsInt)
                    let types = Array.map unbox<UniformType> typesAsInt

                    let fields =
                        List.init fieldCount (fun fi ->
                            let idx = indices.[fi]

                            let name =
                                let mutable len = 0
                                let mutable typ = Unchecked.defaultof<UniformType>
                                gl.GetActiveUniform(program, idx, &len, &typ)
                                //System.Text.Encoding.UTF8.GetString(buffer, 0, int len)

                            {
                                ufOffset = offsets.[fi]
                                ufType = UniformType.toGLSLType types.[fi]
                                ufName = name
                            }
                        )


                    {
                        ubName = name
                        ubFields = fields
                        ubSet = -1
                        ubBinding = binding
                        ubSize = size
                    }

                )

            for glu in uniformBuffers do
                match MapExt.tryFind glu.ubName iface.uniformBuffers with
                | Some fsu ->
                    if glu.ubSize <> fsu.ubSize then
                        failf "invalid size for uniform-block %A: %A (should be %A)" glu.ubName fsu.ubSize glu.ubSize
                    

                    let glfs = glu.ubFields |> List.sortBy (fun f -> f.ufOffset)
                    let fsfs = fsu.ubFields |> List.sortBy (fun f -> f.ufOffset)

                    if List.length glfs <> List.length fsfs then
                        failf "invalid number of fields for uniform-block %A: %A (should be %A)" glu.ubName (List.length fsfs) (List.length glfs)

                    for index, (glf, fsf) in List.indexed (List.zip glfs fsfs) do
                        if glf.ufName <> fsf.ufName then
                            failf "invalid uniform-field name at position %d in %A: %A (should be %A)" index glu.ubName fsf.ufName glf.ufName

                        if glf.ufOffset <> fsf.ufOffset then
                            failf "invalid uniform-field offset for %A in %A: %A (should be %A)" glf.ufName glu.ubName fsf.ufOffset glf.ufOffset
                            
                        if glf.ufType <> fsf.ufType then
                            failf "invalid uniform-field type for %A in %A: %A (should be %A)" glf.ufName glu.ubName fsf.ufType glf.ufType


                | None ->
                    // not active???
                    ()

        )



[<AbstractClass; Sealed; Extension>]
type ShaderExtensions private() = 



    static let uniformPools = ConcurrentDict<Device, ConcurrentDict<struct(int * list<GLSLUniformBufferField>), UniformBufferPool>>(Dict())

    static let versionRx =
        System.Text.RegularExpressions.Regex @"^\#version[ \t]+(.*)"

    static let shaderType =
        LookupTable.lookupTable [
            ShaderStage.Vertex, ShaderType.VertexShader
            ShaderStage.Compute, ShaderType.ComputeShader
            ShaderStage.Fragment, ShaderType.FragmentShader
            ShaderStage.Geometry, ShaderType.GeometryShader
            ShaderStage.TessControl, ShaderType.TessControlShader
            ShaderStage.TessEval, ShaderType.TessEvaluationShader
        ]

    static let programCache =
        Dict<Device * Effect * Map<string, TextureFormat * int>, Program>()
        
    static let backend =
        Backend.Create {
            version = GLSLVersion(3,0,0, "es")
            enabledExtensions = Set.empty //Set.ofList ["GL_ARB_separate_shader_objects"]
            createUniformBuffers = true
            createPerStageUniforms = false
            createDescriptorSets = false
            stepDescriptorSets = false
            createInputLocations = true
            createOutputLocations = true
            createPassingLocations = false
            reverseMatrixLogic = true
            depthWriteMode = false
            useInOut = true
            bindingMode = BindingMode.None
        }

    static let printLog (title : string) (code : string) (log : string) =
        Log.start "%s" title
        let lines = code.Split([|"\r\n"; "\n"|], StringSplitOptions.None)
        let digits =
            if lines.Length < 100 then 2
            else Fun.Log10 (float lines.Length) |> ceil |> int

        let str (l : int) =
            let s = string l
            if s.Length < digits then String(' ', digits - s.Length) + s
            else s

        for i in 0 .. lines.Length - 1 do
            Report.WarnNoPrefix("{0}", sprintf "%s %s" (str (i+1)) lines.[i])
        Log.start "errors"
        let lines = log.Split([|"\r\n"; "\n"|], StringSplitOptions.None)
        for l in lines do Report.ErrorNoPrefix("{0}", l)
        Log.stop()
        Log.stop()

    [<Extension>]
    static let getGLSLShader(outputs : Map<string, (TextureFormat * int)>) (effect : Effect) =
        let key =
            let suffix = outputs |> Map.toSeq |> Seq.map (fun (name, (_, i)) -> sprintf "%s:%d" name i) |> String.concat "_"
            sprintf "%s_%s" effect.Id suffix

        let glsl = 
            let sw = System.Diagnostics.Stopwatch.StartNew()

            let create() =
                
                let shaderType (sem : string) (fmt : TextureFormat) =
                    match sem with
                    | "Normals" -> typeof<V3d>
                    | _ -> TextureFormat.toShaderType fmt

                let module_ = 
                    effect |> Effect.toModule { 
                        depthRange = Range1d(-1.0, 1.0)
                        flipHandedness = false
                        lastStage = ShaderStage.Fragment
                        outputs = outputs |> Map.map (fun k (a,b) -> shaderType k a, b)
                    }

                let glsl = 
                    ModuleCompiler.compileGLSL backend module_
                    
                try
                    let data =
                        use ms = new System.IO.MemoryStream()
                        GLSLShader.serialize ms glsl
                        ms.ToArray() |> Convert.ToBase64String
                    JS.LocalStorage.Set(key, data)
                    Log.line "[Shader] cache written: %s (%A)" key sw.MicroTime
                    Log.line "%s" glsl.code
                with _ ->
                    Log.warn "[Shader] cache write failed: %s (%A)" key sw.MicroTime
                    ()

                glsl

            match LocalStorage.TryGet key with
            | Some glsl ->
                try
                    let arr = Convert.FromBase64String glsl
                    use ms = new System.IO.MemoryStream(arr)
                    let glsl = GLSLShader.deserialize ms
                    Log.line "[Shader] cache hit: %s (%A)" key sw.MicroTime
                    glsl
                with _ ->
                    Log.warn "[Shader] cache read failed: %s" key
                    create()
            | None ->
                create()
        
        glsl
        
    //let pickle (glsl : GLSLShader) =
    //    use ms = new System.IO.MemoryStream()
    //    use w = new System.IO.BinaryWriter(ms)

    //    let writeGLSLTye


    //    for KeyValue(name, i) in glsl.iface.images do
    //        w.Write name
    //        w.Write i.imageBinding
    //        w.Write i.imageName
    //        w.Write i.imageSet
    //        w.Write (int i.imageType.dimension)
    //        match i.imageType.format with
    //        | Some f -> w.Write 1uy; w.Write (int f)
    //        | None -> w.Write 0uy
    //        w.Write i.imageType.isArray
    //        w.Write i.imageType.isMS
    //        w.Write i.imageType.original.AssemblyQualifiedName
    //        w.Write i.imageType.valueType
        
        
        //()

    [<Extension>]
    static member CreateUniformBuffer(device : Device, target : GLSLUniformBuffer, tryGetValue : string -> voption<IAdaptiveValue>) =
        let pool = 
            let r = uniformPools.GetOrCreate(device, fun _ -> ConcurrentDict(Dict()))
            r.GetOrCreate(struct(target.ubSize, target.ubFields), fun struct(size, fields) ->
                let pool = new UniformBufferPool(device, size, fields)
                device.OnDispose.Add(fun () -> 
                    uniformPools.Remove device |> ignore
                    pool.Dispose()
                )
                pool
            )


        pool.CreateUniformBuffer(tryGetValue)

    [<Extension>]
    static member CreateShader(device : Device, stage : ShaderStage, code : string) =
        let code = code.Replace("sample in", "in").Replace("sample out", "out").Replace("gl_SamplePosition", "vec2(0,0)")
        let handle = 
            device.Run (fun gl ->
                let handle = gl.CreateShader(shaderType stage)
                gl.ShaderSource(handle, code)
                gl.CompileShader(handle)

                let mutable status = 0
                gl.GetShader(handle, ShaderParameterName.CompileStatus, &status)
                if status = 0 then
                    let log = 
                        let mutable len = 0u
                        let buf = Array.zeroCreate<byte> 16384
                        gl.GetShaderInfoLog(handle, &len, Span buf)
                        if len > 0u then System.Text.Encoding.UTF8.GetString(buf, 0, int len)
                        else ""

                    gl.DeleteShader handle

                    printLog "shader compile failed" code log
                    failwithf "shader compile failed: %s" log
                else
                    let log = 
                        let mutable len = 0u
                        let buf = Array.zeroCreate<byte> 16384
                        gl.GetShaderInfoLog(handle, &len, Span buf)
                        if len > 0u then System.Text.Encoding.UTF8.GetString(buf, 0, int len)
                        else ""
                    if not (String.IsNullOrWhiteSpace log) then
                        Log.warn "shader compile had warnings: %A" log
                    
                handle
            )

        new Shader(device, handle)
  
  
    [<Extension>]
    static member AssembleModule(outputs : FramebufferSignature, effect : Effect) =
        let outputs =
            outputs.ColorAttachments 
            |> Map.toSeq
            |> Seq.map (fun (id, { Name = name; Format = fmt }) ->
                string name, (fmt, id)
            )
            |> Map.ofSeq
        

        let shaderType (sem : string) (fmt : TextureFormat) =
            match sem with
            | "Normals" -> typeof<V3d>
            | _ -> TextureFormat.toShaderType fmt

        effect |> Effect.toModule { 
            depthRange = Range1d(-1.0, 1.0)
            flipHandedness = false
            lastStage = ShaderStage.Fragment
            outputs = outputs |> Map.map (fun k (a,b) -> shaderType k a, b)
        }
        

    [<Extension>]
    static member CreateProgram(device : Device, effect : Effect, signature : FramebufferSignature) =
        let outputs =
            signature.ColorAttachments 
            |> Map.toSeq
            |> Seq.map (fun (id, { Name = name; Format = fmt }) ->
                string name, (fmt, id)
            )
            |> Map.ofSeq
        
        
        lock programCache (fun () ->
            programCache.GetOrCreate((device, effect, outputs), fun (device, effect, outputs) ->
                let glsl = getGLSLShader outputs effect
                
                let stageCode = 
                    versionRx.Replace(glsl.code, fun m ->
                        sprintf "%s\n#define SHADER_STAGE\nprecision highp float;\nprecision highp int;\nprecision highp sampler2DShadow;\n" m.Value 
                    )

                //Log.line "%s" stageCode


                let samplers = System.Collections.Generic.List<Sampler>()

                let iface =
                    { glsl.iface with
                        uniformBuffers =
                            let mutable i = 0
                            glsl.iface.uniformBuffers |> MapExt.map (fun _name b ->
                                let idx = i
                                i <- i + 1
                                { b with ubBinding = idx }
                            )
                        samplers =
                            let mutable i = 0
                            glsl.iface.samplers |> MapExt.map (fun _name s ->
                                let idx = i
                                //i <- i + List.length s.samplerTextures

                                for _name, sam in s.samplerTextures do
                                    samplers.Add(device.CreateSampler sam)
                                    i <- i + 1

                                { s with samplerBinding = idx }
                            )
                    }

                let shaders =
                    match glsl.iface.shaders with
                    | GLSLProgramShaders.Graphics { stages = s } -> s
                    | _ -> MapExt.empty
                    
                let sw = System.Diagnostics.Stopwatch.StartNew()
                let shaders = 
                    shaders
                    |> MapExt.keys
                    |> Set.toList
                    |> List.map (fun s ->
                        let stageCode = 
                            versionRx.Replace(glsl.code, fun m ->
                                sprintf "%s\n#define %A\nprecision highp float;\nprecision highp int;\nprecision highp sampler2DShadow;\n" m.Value s
                            )
                        device.CreateShader(s, stageCode)
                    )
                    
                let handle = 
                    device.Run (fun gl ->
                        let p = gl.CreateProgram()

                        for s in shaders do 
                            gl.AttachShader(p, s.Handle)
                            s.Dispose()

                        gl.LinkProgram(p)
                        let mutable status = 0
                        gl.GetProgram(p, ProgramPropertyARB.LinkStatus, &status)
                        gl.UseProgram p
                        if status = 0 then
                            let mutable buffer : byte[] = null
                            let mutable len = 0u
                            gl.GetProgramInfoLog(p, &len, buffer)

                            buffer <- Array.zeroCreate (int len)
                            gl.GetProgramInfoLog(p, &len, buffer)
                            let log = System.Text.Encoding.UTF8.GetString(buffer)
                            
                            gl.DeleteProgram p
                            printLog "program linking failed" stageCode log
                        //else
                        //    let log = gl.GetProgramInfoLog(p)
                        //    if not (String.IsNullOrWhiteSpace log) then
                        //        Log.warn "program linking had warnings: %A" log
                        
                        for KeyValue(name, b) in iface.uniformBuffers do
                            let idx = gl.GetUniformBlockIndex(p, name)
                            gl.UniformBlockBinding(p, idx, uint32 b.ubBinding)

                        for KeyValue(name, s) in iface.samplers do
                            let idx = gl.GetUniformLocation(p, name)
                            gl.Uniform1(idx, s.samplerBinding)
                            

                        

                        gl.UseProgram 0u
                        // TODO: validate layout in debug mode
                        //if device.Debug then
                        //    DeviceShaderExtensions.ValidateUniformLayout(device, p, iface)
                        p
     
                    )

                let kill (_ : Program) =
                    lock programCache (fun () ->
                        programCache.Remove((device, effect, outputs)) |> ignore
                    )


                let inputSemantics =
                    iface.inputs |> List.map (fun p ->
                        p.paramLocation, Symbol.Create p.paramSemantic
                    )
                    |> Map.ofList

                let res = new Program(device, inputSemantics, iface, CSharpList.toArray samplers, handle, kill)
                device.ResourceDestroyed(res.UniqueName)
                
                sw.Stop()
                Log.line "compile took %A" sw.MicroTime
                res
            )
        )
  

    [<Extension>]
    static member GetEffectInterface(outputs : FramebufferSignature, effect : Effect) =
        let p = outputs.Device.CreateProgram(effect, outputs)
        p.Interface
      
    [<Extension>]
    static member CreateProgram(signature : FramebufferSignature, effect : Effect) =
        signature.Device.CreateProgram(effect, signature)

    [<Extension>]
    static member SetProgram(this : CommandStream, program : Program) =
        this.BaseStream.UseProgram(program.Handle)



    [<Extension>]
    static member SetProgram(this : CommandStream, program : aval<Program>) =
        this.BaseStream.UseProgram(program |> APtr.mapVal (fun p -> p.Handle))
        
    [<Extension>]
    static member SetProgram(this : CommandStream, program : ares<Program>) =
        this.SetProgram(this.Acquire program)

    [<Extension>]
    static member SetUniformBuffer(this : CommandStream, binding : int, buffer : AdaptiveUniformBuffer) =

        buffer.Acquire()
        let block = buffer.Buffer
        this.AddUpdater buffer.Updater
        this.BaseStream.AddTemporaryResource {
            new IDisposable with
                member x.Dispose() =
                    this.RemoveUpdater buffer.Updater
                    buffer.Release()
                    
        }
        this.BaseStream.BindBufferRange(BufferTargetARB.UniformBuffer, uint32 binding, block.Buffer.Handle, block.Offset, unativeint block.Size)





