namespace Aardworx.Rendering.WebGL

open System
open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardworx.WebAssembly
open Aardvark.Base
open Aardvark.Rendering
open FSharp.Data.Adaptive

#nowarn "9"

module private TextureHelpers =
    
    let rec estimateTextureSize (format : TextureFormat) (size : V3i) (levels : int) (layers : option<int>) (samples : option<int>) =
        let inline nextSize (v : V3i) = V3i(v.X / 2 |> max 1, v.Y / 2 |> max 1, v.Z / 2 |> max 1)


        let self = int64 size.X * int64 size.Y * int64 size.Z * int64 (TextureFormat.pixelSizeInBytes format) * int64 (defaultArg samples 1) * int64 (defaultArg layers 1)
        if levels > 1 then self + estimateTextureSize format (nextSize size) (levels - 1) layers samples
        else self


[<Sealed>]
type Texture(device : Device, handle : uint32, target : TextureTarget, dimension : TextureDimension, format : TextureFormat, size : V3i, levels : int, samples : option<int>, layers : option<int>) =
    inherit Resource(device, "Texture", handle, TextureHelpers.estimateTextureSize format size levels layers samples)

    member x.Target = target

    member x.Dimension = 
        x.CheckDisposed()
        dimension

    member x.Format = 
        x.CheckDisposed()
        format

    member x.Size = 
        x.CheckDisposed()
        size

    member x.Levels = 
        x.CheckDisposed()
        levels

    member x.Samples = 
        x.CheckDisposed()
        samples

    member x.Layers = 
        x.CheckDisposed()
        layers

    member x.EffectiveLayers =
        x.CheckDisposed()
        match dimension with
        | TextureDimension.TextureCube ->
            match layers with
            | Some l -> 6 * l
            | None -> 6
        | _ ->
            match layers with
            | Some l -> l
            | None -> 1


    interface IBackendTexture with
        member this.Count = defaultArg this.Layers 1
        member this.Dimension = this.Dimension
        member this.Format = this.Format
        member this.Handle = this.Handle :> obj
        member this.MipMapLevels = this.Levels
        member this.Runtime = device.Runtime :> _
        member this.Samples = defaultArg this.Samples 1
        member this.Size = this.Size
        member this.WantMipMaps = this.Levels > 1

    override x.Destroy(gl : GL) =
        if handle <> 0u then
            gl.DeleteTexture handle

    override x.ToString() =
        let dim = 
            match dimension with
            | TextureDimension.Texture1D -> "1D"
            | TextureDimension.Texture2D -> "2D"
            | TextureDimension.Texture3D -> "3D"
            | TextureDimension.TextureCube -> "Cube"
            | _ -> ""

        let ms =
            match samples with
            | Some _ -> "MS"
            | None -> ""

        let arr =
            match layers with
            | Some _ -> "Array"
            | None -> ""

        if x.IsDisposed then 
            sprintf "Texture%s%s%s { disposed }" dim ms arr
        else 
            let handle = sprintf "handle = %d; " handle

            let layers =
                match layers with
                | Some l -> sprintf "; layers = %d" l
                | None -> ""
            let samples =
                match samples with
                | Some l -> sprintf "; samples = %d" l
                | None -> ""
            let levels =
                if levels > 1 then sprintf "; levels = %d" levels
                else ""

            sprintf "Texture%s%s%s { %sformat = %A; size = %A%s%s%s }" dim ms arr handle format size levels layers samples


[<AbstractClass; Sealed; Extension>]
type DeviceTextureExtensions private() =
    
    
    static let getTarget (dim : TextureDimension) (layered : bool) (ms : bool) =
        match dim with

        | TextureDimension.Texture1D ->
            raise <| NotSupportedException "1D textures not available"

        | TextureDimension.Texture2D ->
            if layered then
                if ms then TextureTarget.Texture2DMultisampleArray
                else TextureTarget.Texture2DArray
            else
                if ms then TextureTarget.Texture2DMultisample
                else TextureTarget.Texture2D
                
        | TextureDimension.Texture3D ->
            if layered then raise <| NotSupportedException "3D array textures not available"
            if ms then raise <| NotSupportedException "3D multisample textures not available"
            TextureTarget.Texture3D

        | TextureDimension.TextureCube ->
            if layered then TextureTarget.TextureCubeMapArray
            else TextureTarget.TextureCubeMap

        | dim ->
            raise <| NotSupportedException(sprintf "bad TextureDimension: %A" dim)
            


    [<Extension>]
    static member CreateTexture(this : Device, dim : TextureDimension, format : TextureFormat, size : V3i, ?levels : int, ?layers : int, ?samples : int) =
        let levels = defaultArg levels 1

        if size.AllSmallerOrEqual 0 then
            let target = getTarget dim (Option.isSome layers) (Option.isSome samples)
            new Texture(this, 0u, target, dim, format, size, levels, samples, layers)
        else
            let size = V3i(max size.X 1, max size.Y 1, max size.Z 1)
            match dim with
            | TextureDimension.Texture1D ->
                if size.Y > 1 || size.Z > 1 then raise <| NotSupportedException "Texture1D cannot have height/depth"
                match samples with
                | Some _ -> raise <| NotSupportedException "Texture1D cannot be multisampled"
                | None -> ()

            | TextureDimension.Texture2D ->
                if size.Z > 1 then  raise <| NotSupportedException "Texture2D cannot have depth"

            | TextureDimension.TextureCube ->
                if size.Z > 1 then raise <| NotSupportedException "TextureCube cannot have depth"
                if size.X <> size.Y then raise <| NotSupportedException "TextureCube must be square"
                match samples with
                | Some _ -> raise <| NotSupportedException "TextureCube cannot be multisampled (use a layered 2D texture instead)"
                | None -> ()

                if Option.isSome layers then
                    raise <| NotSupportedException "TextureCube cannot be layered on your GPU (use a layered 2D texture instead)"

            | TextureDimension.Texture3D ->
                match layers with
                | Some _ -> raise <| NotSupportedException "Texture3D cannot be layered"
                | None -> ()
                match samples with
                | Some _ -> raise <| NotSupportedException "Texture3D cannot be multisampled"
                | None -> ()

            | _ ->
                failwithf "unsupported TextureDimension: %A" dim

            match samples with
            | Some _ -> if levels > 1 then raise <| NotSupportedException "multisampled textures cannot have mipMap levels"
            | None -> ()

            let target = getTarget dim (Option.isSome layers) (Option.isSome samples)

            let handle = 
                this.Run (fun gl ->
                    let ifmt = unbox<SizedInternalFormat> (int format)
                    let mutable handle = 0u

                    gl.GenTextures(1u, &handle)
                    gl.BindTexture(target, handle)

                    try
                        try
                            match dim with
                            | TextureDimension.Texture1D ->
                                match layers with
                                | Some layers ->
                                    gl.TexStorage2D(target, uint32 levels, ifmt, uint32 size.X, uint32 layers)
                                | None ->
                                    fail "no 1D textures"
                            | TextureDimension.Texture2D ->
                                match layers with
                                | None ->
                                    match samples with
                                    | None ->
                                        gl.TexStorage2D(target, uint32 levels, ifmt, uint32 size.X, uint32 size.Y)
                                    | Some samples ->
                                        gl.TexStorage2DMultisample(target, uint32 samples, ifmt, uint32 size.X, uint32 size.Y, true)
                                | Some layers ->
                                    match samples with
                                    | None ->
                                        gl.TexStorage3D(target, uint32 levels, ifmt, uint32 size.X, uint32 size.Y, uint32 layers)
                                    | Some samples ->
                                        gl.TexStorage3DMultisample(target, uint32 samples, ifmt, uint32 size.X, uint32 size.Y, uint32 layers, true)


                            | TextureDimension.TextureCube -> // Cube
                                match layers with
                                | None ->
                                    gl.TexStorage2D(target, uint32 levels, ifmt, uint32 size.X, uint32 size.Y)
                                | Some layers ->
                                    gl.TexStorage3D(target, uint32 levels, ifmt, uint32 size.X, uint32 size.Y, uint32 (6 * layers))

                            | TextureDimension.Texture3D ->
                                gl.TexStorage3D(target, uint32 levels, ifmt, uint32 size.X, uint32 size.Y, uint32 size.Z)

                            | dim ->
                                failwithf "bad TextureDimension: %A" dim

                            //gl.TexParameterI(target, TextureParameterName.DepthStencilTextureMode

                            handle

                        finally
                            gl.BindTexture(target, 0u)
                    with e ->
                        gl.DeleteTexture handle
                        reraise()
                )


            new Texture(this, handle, target, dim, format, size, levels, samples, layers)
        
    [<Extension>]
    static member CreateTexture1D(this : Device, format : TextureFormat, size : int, ?levels : int, ?layers : int, ?samples : int) =
        this.CreateTexture(TextureDimension.Texture1D, format, V3i(size,1,1), ?levels = levels, ?layers = layers, ?samples = samples)

    [<Extension>]
    static member CreateTexture2D(this : Device, format : TextureFormat, size : V2i, ?levels : int, ?layers : int, ?samples : int) =
        this.CreateTexture(TextureDimension.Texture2D, format, V3i(size,1), ?levels = levels, ?layers = layers, ?samples = samples)
        
    [<Extension>]
    static member CreateTexture3D(this : Device, format : TextureFormat, size : V3i, ?levels : int, ?layers : int, ?samples : int) =
        this.CreateTexture(TextureDimension.Texture3D, format, size, ?levels = levels, ?layers = layers, ?samples = samples)
        
    [<Extension>]
    static member CreateTextureCube(this : Device, format : TextureFormat, size : int, ?levels : int, ?layers : int, ?samples : int) =
        this.CreateTexture(TextureDimension.TextureCube, format, V3i(size, size, 1), ?levels = levels, ?layers = layers, ?samples = samples)


    [<Extension>]
    static member SetTexture(this : CommandStream, index : int, tex : Texture) =
        this.BaseStream.ActiveTexture(unbox<TextureUnit>(int TextureUnit.Texture0 + index))
        this.BaseStream.BindTexture(tex.Target, tex.Handle)

        
    [<Extension>]
    static member SetTexture(this : CommandStream, index : int, tex : aval<Texture>) =
        let target = tex |> APtr.mapVal (fun t -> t.Target)
        let handle = tex |> APtr.mapVal (fun t -> t.Handle)

        this.BaseStream.ActiveTexture(unbox<TextureUnit>(int TextureUnit.Texture0 + index))
        this.BaseStream.BindTexture(target, handle)
        
    [<Extension>]
    static member SetTexture(this : CommandStream, index : int, tex : ares<Texture>) =
        this.SetTexture(index, this.Acquire tex)
        