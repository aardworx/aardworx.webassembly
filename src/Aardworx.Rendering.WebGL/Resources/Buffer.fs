namespace Aardworx.Rendering.WebGL

open System
open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardworx.WebAssembly
open Microsoft.FSharp.NativeInterop
open Aardvark.Rendering
open FSharp.Data.Adaptive
open Aardvark.Base
open System.Runtime.InteropServices

#nowarn "9"

[<Flags>]
type BufferUsage =
    | None      = 0x0000
    | Vertex    = 0x0001
    | Index     = 0x0002
    | Storage   = 0x0004
    | Uniform   = 0x0008
    | Indirect  = 0x0010
    | Client    = 0x0080
    | Dynamic   = 0x0100

type Buffer(device : Device, handle : uint32, sizeInBytes : int64, usage : BufferUsage) =
    inherit Resource(device, "Buffer", handle, sizeInBytes)
    
    member x.Size = sizeInBytes
    member x.Usage = usage
    

    override x.Destroy(gl : GL) =
        gl.DeleteBuffer handle

    interface BufferRange with
        member x.Buffer = x
        member x.Offset = 0L
        member x.Size = x.Size

    interface IBackendBuffer with
        member x.Handle = handle :> obj
        member x.SizeInBytes = nativeint sizeInBytes
        member x.Runtime = device.Runtime :> IBufferRuntime


    member x.GetSlice(firstByte : option<int64>, lastByte : option<int64>) =
        let rOffset = 
            defaultArg firstByte 0L

        let rSize =
            match lastByte with
            | Some lastByte -> 1L + lastByte - rOffset
            | None -> sizeInBytes - rOffset

        let rEnd = rOffset + rSize

        if rOffset < 0L || rOffset > sizeInBytes || rSize < 0L || rEnd > sizeInBytes then 
            failf "BufferRange out of bounds [%d, %d) (size: %d)" rOffset rEnd sizeInBytes


        { new BufferRange with
            member __.Buffer = x
            member __.Offset = rOffset
            member __.Size = rSize
        }
        
    member x.GetSlice(firstByte : option<int>, lastByte : option<int>) =
        let rOffset = 
            defaultArg firstByte 0 |> int64

        let rSize =
            match lastByte with
            | Some lastByte -> 1L + int64 lastByte - rOffset
            | None -> sizeInBytes - rOffset

        let rEnd = rOffset + rSize

        if rOffset < 0L || rOffset > sizeInBytes || rSize < 0L || rEnd > sizeInBytes then 
            failf "BufferRange out of bounds [%d, %d) (size: %d)" rOffset rEnd sizeInBytes

        { new BufferRange with
            member __.Buffer = x
            member __.Offset = rOffset
            member __.Size = rSize
        }
        
    member x.Sub(offset : int64, size : int64) =
        if offset < 0L then failf "negative BufferRange offset: %d" offset
        if size < 0L || offset + size > sizeInBytes then failf "bad BufferRange size: %d" size

        { new BufferRange with
            member __.Buffer = x
            member __.Offset = offset
            member __.Size = size
        }

    member x.Sub(offset : int, size : int) =
        x.Sub(int64 offset, int64 size)

    member x.Sub(offset : int64) =
        x.Sub(offset, sizeInBytes - offset)

    member x.Sub(offset : int) =
        x.Sub(int64 offset)
        


    static member cast<'a when 'a : unmanaged> (buffer : Buffer) =
        new Buffer<'a>(buffer)
        
    static member cast<'a, 'b when 'a : unmanaged and 'b : unmanaged> (buffer : Buffer<'a>) : Buffer<'b> =
        new Buffer<'b>(buffer.Buffer)
        
    static member cast<'a when 'a : unmanaged> (range : BufferRange) =
        let aSize = int64 sizeof<'a>
        let typed = new Buffer<'a>(range.Buffer)
        let offset = range.Offset
        let cnt = range.Size / aSize |> int
        let b = range.Buffer
        { new BufferRange<'a> with
            member _.Buffer = b
            member _.Buffer = typed
            member _.Offset = offset
            member _.Count = cnt
            member _.Size = range.Size
        }
       
and BufferRange =
    abstract Buffer : Buffer
    abstract Offset : int64
    abstract Size : int64

and Buffer<'a>(buffer : Buffer) =
    static let elemSize = int64 sizeof<'a>

    let count = buffer.Size / elemSize |> int

    member x.Buffer : Buffer = 
        buffer

    member x.Count : int = 
        count
    
    member x.Handle = 
        buffer.Handle

    member internal x.HandleTask = 
        buffer.HandleTask

    member x.IsDisposed = 
        buffer.IsDisposed

    member x.TryAddReference() =
        buffer.TryAddReference()

    member x.AddReference() =
        buffer.AddReference()

    member x.Dispose() =
        buffer.Dispose()

    member x.GetSlice(minIndex : option<int>, maxIndex : option<int>) =
        let minIndex = defaultArg minIndex 0
        let maxIndex = 
            match maxIndex with
            | Some idx -> idx
            | None -> count - 1

        let cnt = 1 + maxIndex - minIndex
        if minIndex < 0 || minIndex > count || cnt < 0 || minIndex + cnt > count then 
            failf "BufferRange out of bounds [%d, %d), size: %d"  minIndex (minIndex + cnt) count

        let size = elemSize * int64 cnt
        let b = x.Buffer
        let offset = elemSize * int64 minIndex
        { new BufferRange<'a> with
            member __.Buffer = b
            member __.Buffer = x
            member __.Count = cnt
            member __.Offset = offset
            member __.Size = size
        }

    member x.Sub(index : int, cnt : int) =
        if index < 0 then failf "bad BufferRange<'a> offset: %d" index
        if cnt < 0 || index + cnt > count then failf "bad BufferRange<'a> count: %d" cnt
        let o = int64 index * elemSize
        {
            new BufferRange<'a> with
                member __.Buffer = buffer
                member __.Buffer = x
                member __.Size = int64 cnt * elemSize
                member __.Count = cnt
                member __.Offset = o
        }

    member x.Sub(index : int) =
        x.Sub(index, count - index)


    interface BufferRange<'a> with
        member x.Buffer = x.Buffer
        member x.Buffer = x
        member x.Offset = 0L
        member x.Count = count
        member x.Size = elemSize * int64 count
        

    interface IDisposable with
        member x.Dispose() = x.Dispose()

and BufferRange<'a> =
    inherit BufferRange
    abstract Buffer : Buffer<'a>
    //abstract ByteOffset : int64
    abstract Count : int

module Buffer =
    let cast<'a when 'a : unmanaged> (buffer : Buffer) =
        new Buffer<'a>(buffer)

[<AbstractClass; Sealed; Extension>]
type DeviceBufferExtensions private() =
    [<Extension>]
    static member GetSlice(range : BufferRange, firstByte : option<int64>, lastByte : option<int64>) =
        let rOffset = 
            defaultArg firstByte 0L

        let rSize =
            match lastByte with
            | Some lastByte -> 1L + lastByte - rOffset
            | None -> range.Size - rOffset

        let rEnd = rOffset + rSize

        if rOffset < 0L || rOffset > range.Size || rSize < 0L || rEnd > range.Size then 
            failf "BufferRange out of bounds [%d, %d) (size: %d)" rOffset rEnd range.Size

        let buffer = range.Buffer
        let offset = range.Offset + rOffset

        { new BufferRange with
            member __.Buffer = buffer
            member __.Offset = offset
            member __.Size = rSize
        }
        
    [<Extension>]
    static member GetSlice(range : BufferRange, firstByte : option<int>, lastByte : option<int>) =
        let rOffset = 
            defaultArg firstByte 0 |> int64

        let rSize =
            match lastByte with
            | Some lastByte -> 1L + int64 lastByte - rOffset
            | None -> range.Size - rOffset

        let rEnd = rOffset + rSize

        if rOffset < 0L || rOffset > range.Size || rSize < 0L || rEnd > range.Size then 
            failf "BufferRange out of bounds [%d, %d) (size: %d)" rOffset rEnd range.Size

        let buffer = range.Buffer
        let offset = range.Offset + rOffset

        { new BufferRange with
            member __.Buffer = buffer
            member __.Offset = offset
            member __.Size = rSize
        }
        
    [<Extension>]
    static member GetSlice(range : BufferRange<'a>, minIndex : option<int>, maxIndex : option<int>) =
        let minIndex = 
            defaultArg minIndex 0

        let cnt =
            match maxIndex with
            | Some idx -> 1 + idx - minIndex
            | None -> range.Count - minIndex

        let endIndex = minIndex + cnt

        if minIndex < 0 || minIndex > range.Count || cnt < 0 || endIndex > range.Count then 
            failf "BufferRange out of bounds [%d, %d) (size: %d)" minIndex endIndex range.Count

        let buffer = range.Buffer
        let offset = range.Offset + int64 minIndex * int64 sizeof<'a>
        let size = int64 cnt * int64 sizeof<'a>
        let bb = buffer.Buffer
        { new BufferRange<'a> with
            member __.Buffer = bb
            member __.Buffer = buffer
            member __.Offset = offset
            member __.Count = cnt
            member __.Size = size
        }

    [<Extension>]
    static member Sub(range : BufferRange, offset : int64, size : int64) =
        if offset < 0L then failf "negative BufferRange offset: %d" offset
        if size < 0L || offset + size > range.Size then failf "bad BufferRange size: %d" size

        let b = range.Buffer
        let o = range.Offset + offset
        { new BufferRange with
            member __.Buffer = b
            member __.Offset = o
            member __.Size = size
        }
        
    [<Extension>]
    static member Sub(range : BufferRange, offset : int, size : int) =
        range.Sub(int64 offset, int64 size)
        
    [<Extension>]
    static member Sub(range : BufferRange<'a>, start : int, count : int) =
        if start < 0 then failf "negative BufferRange offset: %d" start
        if count < 0 || start + count > range.Count then failf "bad BufferRange size: %d" count

        let b = range.Buffer
        let o = range.Offset + int64 sizeof<'a> * int64 start
        let s = int64 count * int64 sizeof<'a>
        { new BufferRange<'a> with
            member __.Buffer = b.Buffer
            member __.Buffer = b
            member __.Offset = o
            member __.Count = count
            member __.Size = s
        }

    [<Extension>]
    static member CreateBuffer(device : Device, size : int64, usage : BufferUsage) =
        if size <= 0L then
            let b = new Buffer(device, 0u, 0L, usage)
            device.ResourceDestroyed(b.UniqueName)
            b
        else
            let target =
                if usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
            let handle =
                device.Run(fun gl ->
                    let handle = gl.GenBuffer()
                    gl.BindBuffer(target, handle)
                    let usage =
                        if usage.HasFlag BufferUsage.Dynamic then GLEnum.DynamicDraw
                        else GLEnum.StaticDraw
                    gl.BufferData(target, unativeint size, VoidPtr.zero, usage)
                    
                    gl.BindBuffer(target, 0u)
                    handle
                )
            new Buffer(device, handle, size, usage)
        
    [<Extension>]
    static member CreateBuffer<'a when 'a : unmanaged>(device : Device, data : 'a[], ?usage : BufferUsage) =
        let usage = defaultArg usage (BufferUsage.Vertex ||| BufferUsage.Index ||| BufferUsage.Storage)

        let b = device.CreateBuffer(int64 sizeof<'a> * data.LongLength, usage)
        device.RunCommand (fun cmd -> cmd.Copy(data, b))
        b



    [<Extension>]
    static member Copy(this : CommandStream, src : nativeint, dst : BufferRange) =
        if dst.Size > 0L then
            let e = this.BaseStream
        
            let target =
                if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
            e.BindBuffer(target, dst.Buffer.Handle)
            e.BufferSubData(target, nativeint dst.Offset, unativeint dst.Size, src)
            e.BindBuffer(target, 0u)
        
    [<Extension>]
    static member Copy(this : CommandStream, src : INativeBuffer, dst : BufferRange) =
        let size = min (unativeint src.SizeInBytes) (unativeint dst.Size)
        if size > 0un then
            let e = this.BaseStream

            let target =
                if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
        
            let ptr = src.Pin()
            e.AddTemporaryResource { new IDisposable with member x.Dispose() = src.Unpin() }

            e.BindBuffer(target, dst.Buffer.Handle)
            e.BufferSubData(target, nativeint dst.Offset, size, ptr)
            e.BindBuffer(target, 0u)
        
    [<Extension>]
    static member Copy(this : CommandStream, src : 'a[], dst : BufferRange) =
        let size = min (src.LongLength * int64 sizeof<'a>) dst.Size
        if size > 0L then
            let e = this.BaseStream


            let target =
                if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
        
            let pSrc =
                if typeof<'a> = typeof<C4b> then
                    let temp = Array.zeroCreate<C4b> src.Length
                    e.CopyBgra(APtr.cast (APtr.pinArray src), APtr.cast (APtr.pinArray temp), APtr.constant src.Length)
                    APtr.pinArrayPtr temp
                else
                    APtr.pinArrayPtr src

            e.BindBuffer(target, dst.Buffer.Handle)
            e.BufferSubData(APtr.constant target, APtr.constant (nativeint dst.Offset), APtr.constant (unativeint size), pSrc)
            e.BindBuffer(target, 0u)

    [<Extension>]
    static member Copy(this : CommandStream, src : Memory<'a>, dst : BufferRange) =
        if dst.Size > 0L then
            let e = this.BaseStream

            let target =
                if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
        
            let pSrc =
                if typeof<'a> = typeof<C4b> then
                    let temp = Array.zeroCreate<C4b> src.Length
                    e.CopyBgra(APtr.cast (APtr.pinMemoryPtr src), APtr.cast (APtr.pinArray temp), APtr.constant src.Length)
                    APtr.pinArrayPtr temp
                else
                    APtr.pinMemoryPtr src

            e.BindBuffer(target, dst.Buffer.Handle)
            e.BufferSubData(APtr.constant target, APtr.constant (nativeint dst.Offset), APtr.constant (unativeint dst.Size), pSrc)
            e.BindBuffer(target, 0u)

        
    [<Extension>]
    static member Copy(this : CommandStream, src : Array, dst : BufferRange) =
        let elementType = src.GetType().GetElementType()
        let elementSize = Marshal.SizeOf elementType
        let e = this.BaseStream
        let size = min (src.LongLength * int64 elementSize) dst.Size
        if size > 0L then
            let target =
                if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                else BufferTargetARB.PixelPackBuffer
        

            let pSrc =
                if elementType = typeof<C4b> then
                    let temp = Array.zeroCreate<C4b> src.Length
                    e.CopyBgra(APtr.cast (APtr.pinSystemArray src), APtr.cast (APtr.pinArray temp), APtr.constant src.Length)
                    APtr.pinArrayPtr temp
                else
                    APtr.pinSystemArrayPtr src

            e.BindBuffer(target, dst.Buffer.Handle)
            e.BufferSubData(APtr.constant target, APtr.constant (nativeint dst.Offset), APtr.constant (unativeint size), pSrc)
            e.BindBuffer(target, 0u)




    
        
    [<Extension>]
    static member Copy(this : CommandStream, src : aval<nativeint>, dst : BufferRange) =
        if dst.Size > 0L then
            if src.IsConstant  then
                this.Copy(AVal.force src, dst)
            else
            
                let target =
                    if dst.Buffer.Usage.HasFlag BufferUsage.Index then BufferTargetARB.ElementArrayBuffer
                    else BufferTargetARB.PixelPackBuffer
            
                let e = this.BaseStream
                let src = APtr.ofAVal src
                e.BindBuffer(target, dst.Buffer.Handle)
                e.BufferSubData(APtr.constant target, APtr.constant (nativeint dst.Offset), APtr.constant (unativeint dst.Size), src)
                e.BindBuffer(target, 0u)

        
    [<Extension>]
    static member Copy(this : CommandStream, src : aval<nativeint>, dst : aval<BufferRange>) =
        if src.IsConstant && dst.IsConstant then
            this.Copy(AVal.force src, AVal.force dst)
        else
             
            let e = this.BaseStream
            let handle = dst |> APtr.mapVal (fun b -> b.Buffer.Handle)
            let offset = dst |> APtr.mapVal (fun b -> nativeint b.Offset)
            let size = dst |> APtr.mapVal (fun b -> unativeint b.Size)
            let src = APtr.ofAVal src
            let sizeIsZero = dst |> APtr.mapVal (fun b -> if b.Size = 0L then 1 else 0)
            
            e.Switch(sizeIsZero,
                [
                    0, fun e -> 
                        e.BindBuffer(APtr.constant BufferTargetARB.PixelPackBuffer, handle)
                        e.BufferSubData(APtr.constant BufferTargetARB.PixelPackBuffer, offset, size, src)
                        e.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                ],
                ignore
            )



    [<Extension>]
    static member Copy(this : CommandStream, src : BufferRange, dst : BufferRange) =
        if src.Size <> dst.Size then failf "inconsistent copy size: %d vs %d" src.Size dst.Size
        if src.Size > 0L then
            let e = this.BaseStream
            e.BindBuffer(BufferTargetARB.CopyReadBuffer, src.Buffer.Handle)
            e.BindBuffer(BufferTargetARB.CopyWriteBuffer, dst.Buffer.Handle)
            e.CopyBufferSubData(CopyBufferSubDataTarget.CopyReadBuffer, CopyBufferSubDataTarget.CopyWriteBuffer, nativeint src.Offset, nativeint dst.Offset, unativeint src.Size)
            e.BindBuffer(BufferTargetARB.CopyReadBuffer, 0u)
            e.BindBuffer(BufferTargetARB.CopyWriteBuffer, 0u)
        

    [<Extension>]
    static member Copy(this : CommandStream, src : BufferRange, dst : nativeint) =
        let e = this.BaseStream
        if src.Size > 0L then
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, src.Buffer.Handle)
            e.GetBufferSubData(BufferTargetARB.PixelPackBuffer, nativeint src.Offset, unativeint src.Size, dst)
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
        
    [<Extension>]
    static member Copy(this : CommandStream, src : BufferRange, dst : Array) =
        let elementType = dst.GetType().GetElementType()
        let elementSize = Marshal.SizeOf elementType
        let size = min (int64 elementSize * int64 dst.Length) src.Size
        if size > 0L then
            let e = this.BaseStream
            let ptr = APtr.pinSystemArrayPtr dst
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, src.Buffer.Handle)
            e.GetBufferSubData(APtr.constant BufferTargetARB.PixelPackBuffer, APtr.constant (nativeint src.Offset), APtr.constant (unativeint size), ptr)
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
            if elementType = typeof<C4b> then
                e.Bgra(APtr.cast (APtr.pinSystemArray dst), APtr.constant dst.Length)

    [<Extension>]
    static member Copy(this : CommandStream, src : BufferRange, dst : 'a[]) =
        let e = this.BaseStream
        let size = min (int64 sizeof<'a> * int64 dst.Length) src.Size
        if size > 0L then
            let ptr = APtr.pinArrayPtr dst
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, src.Buffer.Handle)
            e.GetBufferSubData(APtr.constant BufferTargetARB.PixelPackBuffer, APtr.constant (nativeint src.Offset), APtr.constant (unativeint size), ptr)
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
            if typeof<'a> = typeof<C4b> then
                e.Bgra(APtr.cast (APtr.pinArray dst), APtr.constant dst.Length)

    [<Extension>]
    static member Copy(this : CommandStream, src : BufferRange, dst : Memory<'a>) =
        let e = this.BaseStream
        let size = min (int64 sizeof<'a> * int64 dst.Length) src.Size
        if size > 0L then
            let ptr = APtr.pinMemoryPtr dst
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, src.Buffer.Handle)
            e.GetBufferSubData(APtr.constant BufferTargetARB.PixelPackBuffer, APtr.constant (nativeint src.Offset), APtr.constant (unativeint size), ptr)
            e.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
            if typeof<'a> = typeof<C4b> then
                e.Bgra(APtr.cast (APtr.pinMemory dst), APtr.constant dst.Length)


            
    [<Extension>]
    static member Copy(this : CommandStream, src : IBuffer, dst : BufferRange) =
        match src with
        | :? ArrayBuffer as src ->
            this.Copy(src.Data, dst)
        | :? Buffer as src ->
            this.Copy(src :> BufferRange, dst)
        | :? INativeBuffer as src ->
            this.Copy(src, dst)
        | src ->
            failf "bad buffer: %A" src

    [<Extension>]
    static member CreateBuffer(this : Device, src : IBuffer, ?usage : BufferUsage) =
        let usage = defaultArg usage (BufferUsage.Vertex ||| BufferUsage.Index ||| BufferUsage.Storage)
        match src with
        | :? ArrayBuffer as src ->
            let es = Marshal.SizeOf src.ElementType
            let b = this.CreateBuffer(int64 es * int64 src.Data.Length, usage)
            this.RunCommand(fun c -> c.Copy(src.Data, b))

            b
        | :? Buffer as src ->
            src.AddReference()
            src
        | :? INativeBuffer as src ->
            let b = this.CreateBuffer(int64 src.SizeInBytes, usage)
            src.Use (fun ptr ->
                this.RunCommand(fun c -> c.Copy(ptr, b))
            )
            b
        | src ->
            failf "bad buffer: %A" src
        



        
    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : IndexedGeometryMode, calls : DrawCallInfo[]) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode

        let pCalls = calls |> APtr.pinArrayPtr

        this.BaseStream.MultiDrawArrays(
            APtr.constant prim, 
            pCalls, APtr.constant calls.Length,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : IndexedGeometryMode, calls : Memory<DrawCallInfo>) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode

        let pCalls = calls |> APtr.pinMemoryPtr

        this.BaseStream.MultiDrawArrays(
            APtr.constant prim, 
            pCalls, APtr.constant calls.Length,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndirect(this : CommandStream, mode : IndexedGeometryMode, count : int, buffer : Buffer) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode
        this.BaseStream.MultiDrawArraysIndirect(
            APtr.constant prim, APtr.constant buffer.Handle, APtr.constant count,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : IndexedGeometryMode, calls : DrawCallInfo[]) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode

        let pCalls = calls |> APtr.pinArrayPtr

        this.BaseStream.MultiDrawElements(
            APtr.constant prim, 
            pCalls, APtr.constant calls.Length,
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : IndexedGeometryMode, calls : Memory<DrawCallInfo>) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode

        let pCalls = calls |> APtr.pinMemoryPtr

        this.BaseStream.MultiDrawElements(
            APtr.constant prim, 
            pCalls, APtr.constant calls.Length,
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndexedIndirect(this : CommandStream, mode : IndexedGeometryMode, count : int, calls : Buffer) =
        let prim = PrimitiveType.ofIndexedGeometryMode mode

        this.BaseStream.MultiDrawElementsIndirect(
            APtr.constant prim, 
            APtr.constant calls.Handle, APtr.constant count,
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

         
    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : aval<IndexedGeometryMode>, calls : aval<DrawCallInfo[]>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

        let pCalls = calls |> APtr.pinAdaptiveArrayPtr

        this.BaseStream.MultiDrawArrays(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : aval<IndexedGeometryMode>, calls : aval<Memory<DrawCallInfo>>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

        let pCalls = calls |> APtr.pinAdaptiveMemoryPtr

        this.BaseStream.MultiDrawArrays(
            prim, 
            pCalls, calls |> APtr.mapVal(fun c -> c.Length),
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndirect(this : CommandStream, mode : aval<IndexedGeometryMode>, count : aval<int>, buffer : aval<Buffer>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode
        let handle = buffer |> APtr.mapVal (fun b -> b.Handle)
        this.BaseStream.MultiDrawArraysIndirect(
            prim, handle, count |> APtr.ofAVal,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : aval<IndexedGeometryMode>, calls : aval<DrawCallInfo[]>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

        let pCalls = calls |> APtr.pinAdaptiveArrayPtr

        this.BaseStream.MultiDrawElements(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : aval<IndexedGeometryMode>, calls : aval<Memory<DrawCallInfo>>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

        let pCalls = calls |> APtr.pinAdaptiveMemoryPtr

        this.BaseStream.MultiDrawElements(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndexedIndirect(this : CommandStream, mode : aval<IndexedGeometryMode>, count : aval<int>, buffer : aval<Buffer>) =
        let prim = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode
        let handle = buffer |> APtr.mapVal (fun b -> b.Handle)
        this.BaseStream.MultiDrawElementsIndirect(
            prim, handle, count |> APtr.ofAVal,
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )







    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : IndexedGeometryMode, calls : aval<DrawCallInfo[]>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant

        let pCalls = calls |> APtr.pinAdaptiveArrayPtr

        this.BaseStream.MultiDrawArrays(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDraw(this : CommandStream, mode : IndexedGeometryMode, calls : aval<Memory<DrawCallInfo>>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant

        let pCalls = calls |> APtr.pinAdaptiveMemoryPtr

        this.BaseStream.MultiDrawArrays(
            prim, 
            pCalls, calls |> APtr.mapVal(fun c -> c.Length),
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndirect(this : CommandStream, mode : IndexedGeometryMode, count : aval<int>, buffer : aval<Buffer>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant
        let handle = buffer |> APtr.mapVal (fun b -> b.Handle)
        this.BaseStream.MultiDrawArraysIndirect(
            prim, handle, count |> APtr.ofAVal,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : IndexedGeometryMode, calls : aval<DrawCallInfo[]>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant

        let pCalls = calls |> APtr.pinAdaptiveArrayPtr

        this.BaseStream.MultiDrawElements(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member MultiDrawIndexed(this : CommandStream, mode : IndexedGeometryMode, calls : aval<Memory<DrawCallInfo>>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant

        let pCalls = calls |> APtr.pinAdaptiveMemoryPtr

        this.BaseStream.MultiDrawElements(
            prim, 
            pCalls, calls |> APtr.mapVal (fun c -> c.Length),
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )

    [<Extension>]
    static member MultiDrawIndexedIndirect(this : CommandStream, mode : IndexedGeometryMode, count : aval<int>, buffer : aval<Buffer>) =
        let prim = mode |> PrimitiveType.ofIndexedGeometryMode |> APtr.constant
        let handle = buffer |> APtr.mapVal (fun b -> b.Handle)
        this.BaseStream.MultiDrawElementsIndirect(
            prim, handle, count |> APtr.ofAVal,
            APtr.ofNativePtr this.State.IndexType,
            APtr.constant (NativePtr.toNativeInt this.State.VertexBufferBindingInfo)
        )
        
    [<Extension>]
    static member Draw(this : CommandStream, mode : IndexedGeometryMode, indexed : bool, draws : DrawCalls) =
        match draws with
        | Direct calls ->
            if calls.IsConstant then
                match AVal.force calls with
                | [] -> ()
                | [c] -> 
                    if indexed then this.DrawIndexed(mode, c)
                    else this.Draw(mode, c)
                | calls ->
                    let ptr = calls |> List.toArray
                    if indexed then this.MultiDrawIndexed(mode, ptr)
                    else this.MultiDraw(mode, ptr)
                    
            else
                let ptr = calls |> AVal.map List.toArray
                if indexed then this.MultiDrawIndexed(mode, ptr)
                else this.MultiDraw(mode, ptr)

        | Indirect buffer ->
            let data = 
                buffer |> AVal.map (fun ib ->
                    match ib.Buffer with
                    | :? ArrayBuffer as b -> 
                        Memory(b.Data :?> DrawCallInfo[], 0, ib.Count)
                    | :? INativeBuffer as b ->
                        let res = Array.zeroCreate ib.Count
                        b.Use (fun ptr ->
                            use dst = fixed res
                            let size = min (sizeof<DrawCallInfo> * ib.Count) (int b.SizeInBytes)
                            Marshal.Copy(ptr, NativePtr.toNativeInt dst, size)
                        )
                        Memory(res)
                    | :? Buffer as b ->
                        let res = Array.zeroCreate<DrawCallInfo> ib.Count
                        this.Device.RunCommand (fun c -> c.Copy(b, res))
                        Memory(res)
                    | b ->
                        failf "bad buffer: %A" b
                )
            if indexed then this.MultiDrawIndexed(mode, data)
            else this.MultiDraw(mode, data)
            
    [<Extension>]
    static member Draw(this : CommandStream, mode : IndexedGeometryMode, calls : DrawCalls) =
        this.Draw(mode, false, calls)
        
    [<Extension>]
    static member DrawIndexed(this : CommandStream, mode : IndexedGeometryMode, calls : DrawCalls) =
        this.Draw(mode, true, calls)



module private BufferSliceTests =
    let run(buf : Buffer) =
        let a : BufferRange = buf.[1..]
        let _b : BufferRange = a.[1..]
        
        let _c : BufferRange = buf.[1L..]
        let _d : BufferRange = a.[1L..]
    
        let t = Buffer.cast<int> buf
        let c : BufferRange<int> = t.[1..]
        let _d : BufferRange<int> = c.[1..]
        ()
