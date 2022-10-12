namespace Aardworx.Rendering.WebGL

open System
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open System.Runtime.CompilerServices
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"

module internal ColFormat =
    let private zeroValues =
        LookupTable.lookupTable [
            typeof<uint8>, 0uy :> obj
            typeof<int8>, 0y :> obj
            typeof<uint16>, 0us :> obj
            typeof<int16>, 0s :> obj
            typeof<uint32>, 0u :> obj
            typeof<int32>, 0 :> obj
            typeof<uint64>, 0UL :> obj
            typeof<int64>, 0L :> obj
            typeof<float16>, float16(0.0f) :> obj
            typeof<float32>, 0.0f :> obj
            typeof<float>, 0.0 :> obj
        ]

    let private oneValues =
        LookupTable.lookupTable [
            typeof<uint8>, Byte.MaxValue :> obj
            typeof<int8>, SByte.MaxValue :> obj
            typeof<uint16>, UInt16.MaxValue :> obj
            typeof<int16>, Int16.MaxValue :> obj
            typeof<uint32>, UInt32.MaxValue :> obj
            typeof<int32>, Int32.MaxValue :> obj
            typeof<uint64>, UInt64.MaxValue :> obj
            typeof<float16>, float16(1.0f) :> obj
            typeof<float32>, 1.0f :> obj
            typeof<float>, 1.0 :> obj
        ]
        
    let private conversions =
        LookupTable.lookupTable [
            (typeof<uint8>, typeof<int8>),      (fun (v : uint8) -> int8 (int v - 128)) :> obj
            (typeof<uint8>, typeof<uint16>),    (fun (v : uint8) -> (float v / 255.0) * 65535.0 |> uint16 ) :> obj
            (typeof<uint8>, typeof<int16>),     (fun (v : uint8) -> int ((float v / 255.0) * 65535.0) - 32768 |> int16) :> obj
            (typeof<uint8>, typeof<uint32>),    (fun (v : uint8) -> (float v / 255.0) * 4294967295.0 |> uint32 ) :> obj
            (typeof<uint8>, typeof<int32>),     (fun (v : uint8) -> int64 ((float v / 255.0) * 4294967295.0) - 2147483648L |> int) :> obj
            (typeof<uint8>, typeof<float32>),   (fun (v : uint8) -> float32 v / 255.0f) :> obj
            (typeof<uint8>, typeof<float>),     (fun (v : uint8) -> float v / 255.0) :> obj
            (typeof<float>, typeof<uint8>),     (fun (v : float) -> clamp 0.0 1.0 v * 255.0 |> byte) :> obj


            (typeof<uint16>, typeof<uint8>),    (fun (v : uint16) -> (float v / 65535.0) * 255.0 |> uint8) :> obj 
            (typeof<uint16>, typeof<int16>),    (fun (v : uint16) -> int v - 32768 |> int16) :> obj
            (typeof<uint16>, typeof<uint32>),   (fun (v : uint16) -> (float v / 65535.0) * 4294967295.0 |> uint32) :> obj
            (typeof<uint16>, typeof<float32>),  (fun (v : uint16) -> float32 v / 65535.0f) :> obj 
            (typeof<uint16>, typeof<float>),    (fun (v : uint16) -> float v / 65535.0) :> obj 
            (typeof<float>, typeof<uint16>),    (fun (v : float) -> clamp 0.0 1.0 v * 65535.0 |> uint16) :> obj



            (typeof<int8>, typeof<uint8>), (fun (v : int8) -> int v + 128 |> uint8) :> obj
            
            (typeof<float32>, typeof<uint8>), (fun (v : float32) -> clamp 0.0f 1.0f v * 255.0f |> byte) :> obj

            
            (typeof<float32>, typeof<float>), (fun (v : float32) -> float v) :> obj
            (typeof<float>, typeof<float32>), (fun (v : float) -> float32 v) :> obj

        ]

    let toPixelFormatAndType =
        LookupTable.lookupTable [
            TextureFormat.R8, (PixelFormat.Red, PixelType.UnsignedByte)
            TextureFormat.Rg8, (PixelFormat.RG, PixelType.UnsignedByte)
            TextureFormat.Rgb8, (PixelFormat.Rgb, PixelType.UnsignedByte)
            TextureFormat.Rgba8, (PixelFormat.Rgba, PixelType.UnsignedByte)
            
            TextureFormat.R8i, (PixelFormat.RedInteger, PixelType.Byte)
            TextureFormat.Rg8i, (PixelFormat.RGInteger, PixelType.Byte)
            TextureFormat.Rgb8i, (PixelFormat.RgbInteger, PixelType.Byte)
            TextureFormat.Rgba8i, (PixelFormat.RgbaInteger, PixelType.Byte)
            
            TextureFormat.R16, (PixelFormat.Red, PixelType.UnsignedShort)
            TextureFormat.Rg16, (PixelFormat.RG, PixelType.UnsignedShort)
            TextureFormat.Rgb16, (PixelFormat.Rgb, PixelType.UnsignedShort)
            TextureFormat.Rgba16, (PixelFormat.Rgba, PixelType.UnsignedShort)
            
            TextureFormat.R16i, (PixelFormat.RedInteger, PixelType.Short)
            TextureFormat.Rg16i, (PixelFormat.RGInteger, PixelType.Short)
            TextureFormat.Rgb16i, (PixelFormat.RgbInteger, PixelType.Short)
            TextureFormat.Rgba16i, (PixelFormat.RgbaInteger, PixelType.Short)
            
            TextureFormat.R32i, (PixelFormat.RedInteger, PixelType.Int)
            TextureFormat.Rg32i, (PixelFormat.RGInteger, PixelType.Int)
            TextureFormat.Rgb32i, (PixelFormat.RgbInteger, PixelType.Int)
            TextureFormat.Rgba32i, (PixelFormat.RgbaInteger, PixelType.Int)
            
            TextureFormat.R32f, (PixelFormat.Red, PixelType.Float)
            TextureFormat.Rg32f, (PixelFormat.RG, PixelType.Float)
            TextureFormat.Rgb32f, (PixelFormat.Rgb, PixelType.Float)
            TextureFormat.Rgba32f, (PixelFormat.Rgba, PixelType.Float)

            // TODO: finish list

            TextureFormat.Depth24Stencil8, (PixelFormat.DepthComponent, PixelType.Float)

        ]

    let unsafeZero<'a> = zeroValues typeof<'a> |> unbox<'a>
    let unsafeOne<'a> = oneValues typeof<'a> |> unbox<'a>

    let converter<'a, 'b> = 
        if typeof<'a> = typeof<'b> then unbox<'a -> 'b> id<'a>
        else conversions (typeof<'a>, typeof<'b>) |> unbox<'a -> 'b>

[<AutoOpen>]
module internal Visitor = 

    type TextureFormatVisitor<'r> =
        abstract Accept<'a when 'a : unmanaged> : fmt : Col.Format * channels : int -> 'r

    type private Accepter<'r>() =
        static let table =
            LookupTable.lookupTable [
                TextureFormat.R8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.Gray, 1))
                TextureFormat.Rg8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.GrayAlpha, 2))
                TextureFormat.Rgb8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.RGB, 3))
                TextureFormat.Rgba8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.RGBA, 4))
                TextureFormat.Bgr8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.BGR, 3))
                TextureFormat.Bgra8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<byte>(Col.Format.BGRA, 4))

                TextureFormat.R16, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<uint16>(Col.Format.Gray, 1))
                TextureFormat.Rg16, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<uint16>(Col.Format.GrayAlpha, 2))
                TextureFormat.Rgb16, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<uint16>(Col.Format.RGB, 3))
                TextureFormat.Rgba16, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<uint16>(Col.Format.RGBA, 4))

                TextureFormat.R32f, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<float32>(Col.Format.Gray, 1))
                TextureFormat.Rg32f, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<float32>(Col.Format.GrayAlpha, 2))
                TextureFormat.Rgb32f, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<float32>(Col.Format.RGB, 3))
                TextureFormat.Rgba32f, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<float32>(Col.Format.RGBA, 4))


                TextureFormat.Depth24Stencil8, (fun (v : TextureFormatVisitor<'r>) -> v.Accept<float32>(Col.Format.Gray, 1))
                // TODO: complete
            ]

        static member Accept(fmt : TextureFormat, v : TextureFormatVisitor<'r>) =
            table fmt v

    type TextureFormat with
        member x.Visit(v : TextureFormatVisitor<'r>) =
            Accepter<'r>.Accept(x, v)

    [<AbstractClass>]
    type PixVolumeVisitor<'r>() =
        static let table =
            LookupTable.lookupTable [
                typeof<int8>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<int8>(unbox img, 0y, 127y))
                typeof<uint8>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<uint8>(unbox img, 0uy, 255uy))
                typeof<int16>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<int16>(unbox img, 0s, Int16.MaxValue))
                typeof<uint16>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<uint16>(unbox img, 0us, UInt16.MaxValue))
                typeof<int32>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<int32>(unbox img, 0, Int32.MaxValue))
                typeof<uint32>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<uint32>(unbox img, 0u, UInt32.MaxValue))
                typeof<int64>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<int64>(unbox img, 0L, Int64.MaxValue))
                typeof<uint64>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<uint64>(unbox img, 0UL, UInt64.MaxValue))
                typeof<float16>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<float16>(unbox img, float16(0.0f), float16(1.0f)))
                typeof<float32>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<float32>(unbox img, 0.0f, 1.0f))
                typeof<float>, (fun (self : PixVolumeVisitor<'r>, img : PixVolume) -> self.Visit<float>(unbox img, 0.0, 1.0))
            ]
        abstract member Visit<'a when 'a : unmanaged> : PixVolume<'a> * zero : 'a * one : 'a -> 'r

        interface IPixVolumeVisitor<'r> with
            member x.Visit<'a>(img : PixVolume<'a>) =
                table typeof<'a> (x, img)

    [<AbstractClass>]
    type PixImageVisitor<'r>() =
        static let table =
            LookupTable.lookupTable [
                typeof<int8>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<int8>(unbox img, 0y, 127y))
                typeof<uint8>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<uint8>(unbox img, 0uy, 255uy))
                typeof<int16>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<int16>(unbox img, 0s, Int16.MaxValue))
                typeof<uint16>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<uint16>(unbox img, 0us, UInt16.MaxValue))
                typeof<int32>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<int32>(unbox img, 0, Int32.MaxValue))
                typeof<uint32>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<uint32>(unbox img, 0u, UInt32.MaxValue))
                typeof<int64>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<int64>(unbox img, 0L, Int64.MaxValue))
                typeof<uint64>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<uint64>(unbox img, 0UL, UInt64.MaxValue))
                typeof<float16>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<float16>(unbox img, float16(0.0f), float16(1.0f)))
                typeof<float32>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<float32>(unbox img, 0.0f, 1.0f))
                typeof<float>, (fun (self : PixImageVisitor<'r>, img : PixImage) -> self.Visit<float>(unbox img, 0.0, 1.0))
            ]
        abstract member Visit<'a when 'a : unmanaged> : PixImage<'a> * zero : 'a * one : 'a -> 'r

        interface IPixImageVisitor<'r> with
            member x.Visit<'a>(img : PixImage<'a>) =
                table typeof<'a> (x, img)

type PixelBufferVisitor<'r> =
    abstract ReadOnly : bool
    abstract member Visit<'a when 'a : unmanaged> : data : NativeTensor4<'a> * format : Col.Format -> 'r


[<AbstractClass>]
type PixelBuffer(device : Device, handle : uint32,format : TextureFormat, size : V3i, sizeInBytes : int64) =
 
    static let asTensor (v : NativeVolume<'a>) =

        let dd = Vec.dot v.Size v.Delta

        NativeTensor4<'a>(
            v.Pointer,
            Tensor4Info(
                v.Info.Origin,
                V4l(v.SX, v.SY, 1L, v.SZ),
                V4l(v.DX, v.DY, dd, v.DZ)
            )
        )

    static let copy 
        (tSrc : NativeTensor4<'a>) (srcFormat : Col.Format) 
        (tDst : NativeTensor4<'b>) (dstFormat : Col.Format) =
        
        if typeof<'a> = typeof<'b> then
            let tDst = unbox<NativeTensor4<'a>> tDst
            if srcFormat = dstFormat then 
                NativeTensor4.copy tSrc (unbox tDst)
            else
                match struct(srcFormat, dstFormat) with
                | Col.Format.Gray, Col.Format.GrayAlpha ->
                    tDst.[*,*,*,0] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,1] <- ColFormat.unsafeOne<'a>

                | Col.Format.Gray, (Col.Format.RGB | Col.Format.BGR) ->
                    tDst.[*,*,*,0] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,1] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,2] <- tSrc.[*,*,*,0]

                | Col.Format.Gray, (Col.Format.RGBA | Col.Format.BGRA) ->
                    tDst.[*,*,*,0] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,1] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,2] <- tSrc.[*,*,*,0]
                    tDst.[*,*,*,3] <- ColFormat.unsafeOne<'a>

                | (Col.Format.RGB | Col.Format.BGR | Col.Format.RGBA | Col.Format.BGRA), Col.Format.Gray ->
                    let toFloat = ColFormat.converter<'a, float>
                    let ofFloat = ColFormat.converter<float, 'a>
                    let pSrc = NativePtr.add tSrc.Pointer (int tSrc.Info.Origin)
                    let pDst = NativePtr.add tDst.Pointer (int tDst.Info.Origin)
                    let dw = tSrc.DW

                    let weights =
                        match srcFormat with
                        | Col.Format.RGB | Col.Format.RGBA -> [| 0.2126; 0.7152; 0.0722 |]
                        | _ -> [| 0.0722; 0.7152; 0.2126 |]

                    tSrc.Info.ForeachXYZIndex(tDst.Info, fun (i : int64) (di : int64) ->
                        let mutable avg = 0.0
                        let mutable o = 0L
                        for ci in 0 .. 2 do
                            avg <- avg + weights.[ci] * toFloat (NativePtr.get pSrc (int (i + o)))
                            o <- o + dw

                        NativePtr.set pDst (int di) (ofFloat avg)
                    )
                    
                | Col.Format.RGB, Col.Format.RGBA ->
                    tDst.[*,*,*,0..2] <- tSrc
                    tDst.[*,*,*,3] <- ColFormat.unsafeOne<'a>

                | Col.Format.RGB, Col.Format.BGR ->
                    NativeTensor4.copy (tSrc.MirrorW()) tDst
                    
                | Col.Format.RGB, Col.Format.BGRA ->
                    NativeTensor4.copy (tSrc.MirrorW()) tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- ColFormat.unsafeOne<'a>

                | Col.Format.BGR, Col.Format.BGRA ->
                    tDst.[*,*,*,0..2] <- tSrc
                    tDst.[*,*,*,3] <- ColFormat.unsafeOne<'a>
                    
                | Col.Format.BGR, Col.Format.RGB ->
                    NativeTensor4.copy (tSrc.MirrorW()) tDst
                    
                | Col.Format.BGR, Col.Format.RGBA ->
                    NativeTensor4.copy (tSrc.MirrorW()) tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- ColFormat.unsafeOne<'a>

                | Col.Format.RGBA, Col.Format.RGB ->
                    NativeTensor4.copy tSrc.[*,*,*,0..2] tDst
                    
                | Col.Format.RGBA, Col.Format.BGR ->
                    NativeTensor4.copy (tSrc.[*,*,*,0..2].MirrorW()) tDst
                    
                | Col.Format.RGBA, Col.Format.BGRA ->
                    NativeTensor4.copy (tSrc.[*,*,*,0..2].MirrorW()) tDst.[*,*,*,0..2]
                    NativeVolume.copy tSrc.[*,*,*,3] tDst.[*,*,*,3]

                | Col.Format.BGRA, Col.Format.RGB ->
                    NativeTensor4.copy (tSrc.[*,*,*,0..2].MirrorW()) tDst
                    
                | Col.Format.BGRA, Col.Format.BGR ->
                    NativeTensor4.copy tSrc.[*,*,*,0..2] tDst
                    
                | Col.Format.BGRA, Col.Format.RGBA ->
                    NativeTensor4.copy (tSrc.[*,*,*,0..2].MirrorW()) tDst.[*,*,*,0..2]
                    NativeVolume.copy tSrc.[*,*,*,3] tDst.[*,*,*,3]

                | _ ->
                    failwithf "unsupported format conversion from %A to %A" srcFormat dstFormat
        else
            let conv = ColFormat.converter<'a, 'b>
            let one = conv ColFormat.unsafeOne
            if srcFormat = dstFormat then
                NativeTensor4.copyWith conv tSrc tDst
            else
                match struct(srcFormat, dstFormat) with
                | Col.Format.Gray, Col.Format.GrayAlpha ->
                    NativeVolume.copyWith conv tSrc.[*,*,*,0] tDst.[*,*,*,0]
                    tDst.[*,*,*,1] <- one

                | Col.Format.Gray, (Col.Format.RGB | Col.Format.BGR) ->
                    NativeVolume.copyWith conv tSrc.[*,*,*,0] tDst.[*,*,*,0]
                    NativeVolume.copy tDst.[*,*,*,0] tDst.[*,*,*,1]
                    NativeVolume.copy tDst.[*,*,*,0] tDst.[*,*,*,2]

                | Col.Format.Gray, (Col.Format.RGBA | Col.Format.BGRA) ->
                    NativeVolume.copyWith conv tSrc.[*,*,*,0] tDst.[*,*,*,0]
                    NativeVolume.copy tDst.[*,*,*,0] tDst.[*,*,*,1]
                    NativeVolume.copy tDst.[*,*,*,0] tDst.[*,*,*,2]
                    tDst.[*,*,*,3] <- one

                | (Col.Format.RGB | Col.Format.BGR | Col.Format.RGBA | Col.Format.BGRA), Col.Format.Gray ->
                    let toFloat = ColFormat.converter<'a, float>
                    let ofFloat = ColFormat.converter<float, 'b>
                    let pSrc = NativePtr.add tSrc.Pointer (int tSrc.Info.Origin)
                    let pDst = NativePtr.add tDst.Pointer (int tDst.Info.Origin)
                    let dw = tSrc.DW

                    let weights =
                        match srcFormat with
                        | Col.Format.RGB | Col.Format.RGBA -> [| 0.2126; 0.7152; 0.0722 |]
                        | _ -> [| 0.0722; 0.7152; 0.2126 |]

                    tSrc.Info.ForeachXYZIndex(tDst.Info, fun (i : int64) (di : int64) ->
                        let mutable avg = 0.0
                        let mutable o = 0L
                        for ci in 0 .. 2 do
                            avg <- avg + weights.[ci] * toFloat (NativePtr.get pSrc (int (i + o)))
                            o <- o + dw

                        NativePtr.set pDst (int di) (ofFloat avg)
                    )
                        
                | Col.Format.RGB, Col.Format.RGBA ->
                    NativeTensor4.copyWith conv tSrc tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- one

                | Col.Format.RGB, Col.Format.BGR ->
                    NativeTensor4.copyWith conv (tSrc.MirrorW()) tDst
                    
                | Col.Format.RGB, Col.Format.BGRA ->
                    NativeTensor4.copyWith conv (tSrc.MirrorW()) tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- one

                | Col.Format.BGR, Col.Format.BGRA ->
                    NativeTensor4.copyWith conv tSrc tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- one
                    
                | Col.Format.BGR, Col.Format.RGB ->
                    NativeTensor4.copyWith conv (tSrc.MirrorW()) tDst
                    
                | Col.Format.BGR, Col.Format.RGBA ->
                    NativeTensor4.copyWith conv (tSrc.MirrorW()) tDst.[*,*,*,0..2]
                    tDst.[*,*,*,3] <- one

                | Col.Format.RGBA, Col.Format.RGB ->
                    NativeTensor4.copyWith conv tSrc.[*,*,*,0..2] tDst
                    
                | Col.Format.RGBA, Col.Format.BGR ->
                    NativeTensor4.copyWith conv (tSrc.[*,*,*,0..2].MirrorW()) tDst
                    
                | Col.Format.RGBA, Col.Format.BGRA ->
                    NativeTensor4.copyWith conv (tSrc.[*,*,*,0..2].MirrorW()) tDst.[*,*,*,0..2]
                    NativeVolume.copyWith conv tSrc.[*,*,*,3] tDst.[*,*,*,3]

                | Col.Format.BGRA, Col.Format.RGB ->
                    NativeTensor4.copyWith conv (tSrc.[*,*,*,0..2].MirrorW()) tDst
                    
                | Col.Format.BGRA, Col.Format.BGR ->
                    NativeTensor4.copyWith conv tSrc.[*,*,*,0..2] tDst
                    
                | Col.Format.BGRA, Col.Format.RGBA ->
                    NativeTensor4.copyWith conv (tSrc.[*,*,*,0..2].MirrorW()) tDst.[*,*,*,0..2]
                    NativeVolume.copyWith conv tSrc.[*,*,*,3] tDst.[*,*,*,3]

                | _ ->
                    failwithf "unsupported format conversion from %A to %A" srcFormat dstFormat

    let pfmt, ptyp = ColFormat.toPixelFormatAndType format

    member x.Visit (visitor : PixelBufferVisitor<'r>) =
        x.Map(visitor.ReadOnly, fun pointer ->
            format.Visit {
                new TextureFormatVisitor<_> with
                    member x.Accept<'a when 'a : unmanaged>(format : Col.Format, channelCount : int) =
                        let self = 
                            NativeTensor4<'a>(
                                NativePtr.ofNativeInt pointer, 
                                Tensor4Info(
                                    0L,
                                    V4l(V3l size, int64 channelCount),
                                    V4l(
                                        int64 channelCount, 
                                        int64 channelCount * int64 size.X,
                                        int64 channelCount * int64 size.X * int64 size.Y,
                                        1L
                                    )
                                )
                            )
                        visitor.Visit(self, format)
            }
        )

    member x.SizeInBytes = sizeInBytes
    member x.Device = device
    member x.Handle = handle

    member private x.Map(readonly : bool, action : nativeint -> 'r) =
        if readonly then
            device.Run(fun gl ->
                let ptr = Marshal.AllocHGlobal (int sizeInBytes)
                try
                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, handle)
                    gl.GetBufferSubData(BufferTargetARB.PixelPackBuffer, 0n, unativeint sizeInBytes, ptr)
                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                    action ptr
                finally
                    Marshal.FreeHGlobal ptr
            )
        else
            let ptr = Marshal.AllocHGlobal (int sizeInBytes)
            try
                let res = action ptr
                device.Run(fun gl ->
                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, handle)
                    gl.BufferSubData(BufferTargetARB.PixelPackBuffer, 0n, unativeint sizeInBytes, VoidPtr.ofNativeInt ptr)
                    gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                )
                res
            finally
                Marshal.FreeHGlobal ptr
    member x.Format = format
    member x.Size = size

    member x.PixelFormat = pfmt
    member x.PixelType = ptyp
    
    member private x.WriteInternal(src : NativeVolume<'a>[], fmt : Col.Format) =
        x.Map(false, fun pointer ->
            format.Visit {
                new TextureFormatVisitor<obj> with
                    member x.Accept<'b when 'b : unmanaged>(col : Col.Format, channelCount : int) =
                        let tDst = 
                            NativeTensor4<'b>(
                                NativePtr.ofNativeInt pointer, 
                                Tensor4Info(
                                    0L,
                                    V4l(V3l size, int64 channelCount),
                                    V4l(
                                        int64 channelCount, 
                                        int64 channelCount * int64 size.X,
                                        int64 channelCount * int64 size.X * int64 size.Y,
                                        1L
                                    )
                                )
                            )

                        for z in 0 .. src.Length - 1 do
                            let sSrc = src.[z]
                            let sDst = tDst.[*,*,z,*]
                            copy (asTensor sSrc) fmt (asTensor sDst) col
                        null
                            
            } |> ignore
        )
        
    member private x.WriteInternal(src : NativeTensor4<'a>, fmt : Col.Format) =
        x.Map(false, fun pointer ->
            format.Visit {
                new TextureFormatVisitor<obj> with
                    member x.Accept<'b when 'b : unmanaged>(col : Col.Format, channelCount : int) =
                        let tDst = 
                            NativeTensor4<'b>(
                                NativePtr.ofNativeInt pointer, 
                                Tensor4Info(
                                    0L,
                                    V4l(V3l size, int64 channelCount),
                                    V4l(
                                        int64 channelCount, 
                                        int64 channelCount * int64 size.X,
                                        int64 channelCount * int64 size.X * int64 size.Y,
                                        1L
                                    )
                                )
                            )

                        copy src fmt tDst col
                        null
                            
            } |> ignore
        )
        
    member private x.ReadInternal(dst : NativeTensor4<'a>, fmt : Col.Format) =
        x.Map(true, fun pointer ->
            format.Visit {
                new TextureFormatVisitor<obj> with
                    member x.Accept<'b when 'b : unmanaged>(col : Col.Format, channelCount : int) =
                        let tSrc = 
                            NativeTensor4<'b>(
                                NativePtr.ofNativeInt pointer, 
                                Tensor4Info(
                                    0L,
                                    V4l(V3l size, int64 channelCount),
                                    V4l(
                                        int64 channelCount, 
                                        int64 channelCount * int64 size.X,
                                        int64 channelCount * int64 size.X * int64 size.Y,
                                        1L
                                    )
                                )
                            )
                            
                        copy tSrc col dst fmt
                        null
                            
            } |> ignore
        )
 
    member internal x.Write(src : PixelBuffer, srcOffset : V3i, mirrorY : bool) =
        let oy = 
            if mirrorY then src.Size.Y - srcOffset.Y - size.Y
            else srcOffset.Y
       
        let o = V3l(srcOffset.X, oy, srcOffset.Z)
        let e = o + V3l size - V3l.III
        src.Visit {
            new PixelBufferVisitor<obj> with
                member __.ReadOnly = true
                member __.Visit(tSrc : NativeTensor4<'a>, col : Col.Format) =
                    let subSrc =
                        let sub = tSrc.[o.X .. e.X, o.Y .. e.Y, o.Z .. e.Z, *]
                        sub
                        //if mirrorY then sub.MirrorY()
                        //else sub
                    x.WriteInternal(subSrc, col)
                    null
        } |> ignore
       


    member x.Read(dst : NativeTensor4<'a>, fmt : Col.Format) =
        x.ReadInternal(dst.MirrorY(), fmt)

    member x.Read(dst : NativeVolume<'a>, fmt : Col.Format) =
        let tDst =
            NativeTensor4<'a>(
                dst.Pointer, 
                Tensor4Info(
                    dst.Info.Origin,
                    V4l(dst.SX, dst.SY, 1L, dst.SZ),
                    V4l(dst.DX, dst.DY, dst.DX * dst.SX + dst.DY * dst.SY, dst.DZ)
                )
            )
        x.ReadInternal(tDst.MirrorY(), fmt)
        
    member x.Read(dst : NativeMatrix<'a>, fmt : Col.Format) =
        let tDst =
            NativeTensor4<'a>(
                dst.Pointer, 
                Tensor4Info(
                    dst.Info.Origin,
                    V4l(dst.SX, 1L, 1L, dst.SY),
                    V4l(dst.DX, dst.DX * dst.SX, dst.DX * dst.SX, dst.DY)
                )
            )
        x.ReadInternal(tDst, fmt)
 
    member x.Write(src : NativeTensor4<'a>, fmt : Col.Format) =
        x.WriteInternal(src.MirrorY(), fmt)

    member x.Write(src : NativeVolume<'a>, fmt : Col.Format) =
        let tSrc =
            NativeTensor4<'a>(
                src.Pointer, 
                Tensor4Info(
                    src.Info.Origin,
                    V4l(src.SX, src.SY, 1L, src.SZ),
                    V4l(src.DX, src.DY, Vec.dot src.Size src.Delta, src.DZ)
                )
            )
        x.WriteInternal(tSrc.MirrorY(), fmt)
        
    member x.Write(src : NativeMatrix<'a>, fmt : Col.Format) =
        let dd = Vec.dot src.Size src.Delta
        let tSrc =
            NativeTensor4<'a>(
                src.Pointer, 
                Tensor4Info(
                    src.Info.Origin,
                    V4l(src.SX, 1L, 1L, src.SY),
                    V4l(src.DX, dd, dd, src.DY)
                )
            )
        x.WriteInternal(tSrc, fmt)
 
    member x.Write(src : NativeVolume<'a>[], fmt : Col.Format) =
        x.WriteInternal(src |> Array.map (fun s -> s.MirrorY()), fmt)

    member x.Write(src : PixVolume) =
        src.Visit { 
            new PixVolumeVisitor<obj>() with
                member __.Visit(volume : PixVolume<'a>, zero : 'a, one : 'a) =
                    NativeTensor4.using volume.Tensor4 (fun tSrc ->
                        x.WriteInternal(tSrc.MirrorY(), volume.Format)
                        null
                    )
                       
        } |> ignore
        
    member x.Read(dst : PixVolume) : unit =
        dst.Visit { 
            new PixVolumeVisitor<obj>() with
                member __.Visit(volume : PixVolume<'a>, _, _) =
                    NativeTensor4.using volume.Tensor4 (fun tDst ->
                        x.ReadInternal(tDst.MirrorY(), volume.Format)
                        null
                    )
        } |> ignore

    member x.Write(src : PixImage) =
        let volume =
            src.Visit {
                new IPixImageVisitor<PixVolume> with
                    member x.Visit(img : PixImage<'a>) =
                        let info = img.VolumeInfo
                        PixVolume<'a>(
                            img.Format, 
                            Tensor4<'a>(
                                img.Volume.Data,
                                Tensor4Info(
                                    info.Origin,
                                    V4l(info.Size.XY, 1L, info.Size.Z),
                                    V4l(info.DX, info.DY, info.SX * info.DX + info.SY * info.DY, info.DZ)
                                )
                            )
                        ) :> PixVolume
            }
        x.Write volume
        
    member x.Write(src : PixImage[]) =
        src.[0].Visit {
            new PixImageVisitor<obj>() with
                member __.Visit(img0 : PixImage<'a>, zero : 'a, one : 'a) =
                    let src = src |> Array.map unbox<PixImage<'a>>
                    let ptrs : NativeVolume<'a>[] = Array.zeroCreate src.Length

                    let rec run (i : int) =
                        if i >= src.Length then
                            x.Write(ptrs, img0.Format)
                        else
                            NativeVolume.using src.[i].Volume (fun si ->
                                ptrs.[i] <- si
                                run (i + 1)
                            )   

                    run 0

                    null
        } |> ignore
        
    member x.Read(dst : PixImage) =
        let volume =
            dst.Visit {
                new IPixImageVisitor<PixVolume> with
                    member x.Visit(img : PixImage<'a>) =
                        let info = img.VolumeInfo
                        PixVolume<'a>(
                            img.Format, 
                            Tensor4<'a>(
                                img.Volume.Data,
                                Tensor4Info(
                                    info.Origin,
                                    V4l(info.Size.XY, 1L, info.Size.Z),
                                    V4l(info.DX, info.DY, info.SX * info.DX + info.SY * info.DY, info.DZ)
                                )
                            )
                        ) :> PixVolume
            }
        x.Read volume

    member x.ToPixImage() =
        let target = 
            format.Visit {
                new TextureFormatVisitor<PixImage> with
                    member x.Accept<'t when 't : unmanaged>(fmt : Col.Format, _) =
                        PixImage<'t>(fmt, size.XY) :> PixImage
            }
        x.Read target
        target

    member x.ToPixVolume() =
        let target = 
            format.Visit {
                new TextureFormatVisitor<PixVolume> with
                    member x.Accept<'t when 't : unmanaged>(fmt : Col.Format, _) =
                        PixVolume<'t>(fmt, size) :> PixVolume
            }
        x.Read target
        target

    abstract Dispose : unit -> unit

    interface IDisposable with
        member x.Dispose() = x.Dispose()

[<AbstractClass; Sealed; Extension>]
type DevicePixelBufferExtensions private() =
    [<Extension>]
    static member GetPixelBuffer(device : Device, format : TextureFormat, size : V3i) =
        let bytes = int64 (TextureFormat.pixelSizeInBytes format) * int64 size.X * int64 size.Y * int64 size.Z
        
        let handle = 
            device.Run(fun gl ->
                let h = gl.GenBuffer()
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, h)
                gl.BufferData(BufferTargetARB.PixelPackBuffer, unativeint bytes, VoidPtr.zero, BufferUsageARB.DynamicCopy)
                gl.BindBuffer(BufferTargetARB.PixelPackBuffer, 0u)
                h
            )

        { new PixelBuffer(device, handle, format, size, bytes) with
            member x.Dispose() = device.Run (fun gl -> gl.DeleteBuffer handle)
        }
