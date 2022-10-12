namespace Aardworx.Rendering.WebGL

open Aardvark.Base
open Aardvark.Rendering
open Silk.NET.OpenGLES
open System.Runtime.CompilerServices

type Renderbuffer(device : Device, format : TextureFormat, size : V2i, samples : option<int>, handle : uint32) =
    inherit Resource(device, "Renderbuffer", handle, int64 size.X * int64 size.Y)

    member x.Format = format
    member x.Size = size
    member x.Samples = samples

    interface IRenderbuffer with
        member x.Handle = handle :> obj

    interface IFramebufferOutput with
        member x.Runtime = device.Runtime :> ITextureRuntime
        member x.Format = format
        member x.Size = size
        member x.Samples = defaultArg samples 0

    interface SubRenderbuffer with
        member x.Renderbuffer = x
        member x.Offset = V2i.Zero
        member x.Size = size

    override x.Destroy(gl : GL) =
        gl.DeleteRenderbuffer handle


and SubRenderbuffer =
    abstract Renderbuffer : Renderbuffer
    abstract Offset : V2i
    abstract Size : V2i


[<AbstractClass; Sealed; Extension>]
type DeviceRenderbufferExtensions private() =
    
    [<Extension>]
    static member Sub(this : SubRenderbuffer, offset : V2i, size : V2i) =
        if offset.AnySmaller 0 then failf "SubRenderbuffer offset must be positive: %A" offset
        if size.AnySmaller 0 then failf "SubRenderbuffer size must be positive: %A" size
        if (offset + size).AnyGreater this.Size then failf "SubRenderbuffer out of bounds: (%A, %A) with size %A" offset size this.Size

        let b = this.Renderbuffer
        let o = this.Offset + offset
        let s = size
        { new SubRenderbuffer with
            member x.Renderbuffer = b
            member x.Offset = o
            member x.Size = s
        }
    [<Extension>]
    static member GetSlice(this : SubRenderbuffer, minPixel : option<V2i>, maxPixel : option<V2i>) =
        let minPixel = defaultArg minPixel V2i.Zero
        let maxPixel = defaultArg maxPixel (this.Size - V2i.II)
        this.Sub(minPixel, V2i.II + maxPixel - minPixel)

        
    [<Extension>]
    static member GetSlice(this : SubRenderbuffer, minX : option<int>, maxX : option<int>, minY : option<int>, maxY : option<int>) =
        let minX = defaultArg minX 0
        let minY = defaultArg minY 0
        let maxX = defaultArg maxX (this.Size.X - 1)
        let maxY = defaultArg maxY (this.Size.Y - 1)
        this.Sub(V2i(minX, minY), V2i(1 + maxX - minX, 1 + maxY - minY))


    [<Extension>]
    static member CreateRenderbuffer(device : Device, format : TextureFormat, size : V2i, ?samples : int) =
        
        let handle =
            device.Run (fun gl ->
                let h = gl.GenRenderbuffer()

                gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, h)
                match samples with
                | Some s ->
                    gl.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, uint32 s, unbox<InternalFormat> (int format), uint32 size.X, uint32 size.Y)
                | None ->
                    gl.RenderbufferStorage(RenderbufferTarget.Renderbuffer, unbox<InternalFormat> (int format), uint32 size.X, uint32 size.Y)
                gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0u)

                h

            )

        new Renderbuffer(device, format, size, samples, handle)

