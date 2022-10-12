namespace Aardworx.Rendering.WebGL

#nowarn "44"

open System
open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardvark.Base
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

type TextureRange =
    abstract Texture : Texture
    
    abstract BaseLevel : int
    abstract Levels : int

    abstract BaseLayer : int
    abstract Layers : int
    
and SubTextureLevel =
    abstract TextureLevel : TextureLevel
    abstract Offset : V3i
    abstract Size3D : V3i
    
and SubTextureImage =
    inherit SubTextureLevel
    abstract TextureImage : TextureImage

and TextureLevel =
    inherit TextureRange
    inherit SubTextureLevel
    inherit IFramebufferOutput
    abstract Level : int

and TextureLayer =
    inherit TextureRange
    abstract Layer : int

and TextureImage =
    inherit SubTextureImage
    inherit TextureLayer
    inherit TextureLevel

[<AutoOpen>]
module TextureSlicingExtensions =

    type SubTextureLevel with
        member inline x.Texture = x.TextureLevel.Texture
        member inline x.Level = x.TextureLevel.Level
        member inline x.BaseLayer = x.TextureLevel.BaseLayer
        member inline x.Layers = x.TextureLevel.Layers

        member inline x.PixelBufferSize =
            match x.Texture.Dimension with
            | TextureDimension.Texture1D -> 
                V3i(x.Size3D.X, x.Layers, 1)
            | TextureDimension.Texture2D
            | TextureDimension.TextureCube ->
                V3i(x.Size3D.XY, x.Layers)
            | _ ->
                x.Size3D

        member inline x.NewPixelBuffer() =
            x.Texture.Device.GetPixelBuffer(x.Texture.Format, x.PixelBufferSize)
                
    type SubTextureImage with
        member inline x.Texture = x.TextureImage.Texture
        member inline x.Level = x.TextureImage.Level
        member inline x.Layer = x.TextureImage.Layer

    let inline private invalidArg fmt =
        fmt |> Printf.kprintf (fun str -> raise <| ArgumentException str)

    module private TextureSlice = 
        let private levelSize (size : V3i) (level : int) =
            let d = 1 <<< level
            V3i(
                (if size.X = 0 then 0 else max 1 (size.X / d)), 
                (if size.Y = 0 then 0 else max 1 (size.Y / d)), 
                (if size.Z = 0 then 0 else max 1 (size.Z / d))
            )

        let range (texture : Texture) (baseLevel : int) (levels : int) (baseLayer : int) (layers : int) =
            let layerCount = texture.EffectiveLayers
            if baseLevel < 0 then invalidArg "baseLevel must be positive"
            elif baseLevel >= texture.Levels then invalidArg "baseLevel must be smaller %d" texture.Levels
        
            if levels < 1 then invalidArg "levels must be greater than 0"
            if baseLevel + levels > texture.Levels then invalidArg "texture has insufficient levels" 

            if baseLayer < 0 then invalidArg "baseLayer must be positive"
            elif baseLayer >= layerCount then invalidArg "baseLayer must be smaller than %d" layerCount
        
            if layers < 1 then invalidArg "layers must be greater than 0"
            if baseLayer + layers > layerCount then invalidArg "texture has insufficient layers" 


            { new TextureRange with
                member x.Texture = texture
                member x.BaseLevel = baseLevel
                member x.Levels = levels
                member x.BaseLayer = baseLayer
                member x.Layers = layers
            }
        
        let level (texture : Texture) (level : int) (baseLayer : int) (layers : int) =
            let layerCount = texture.EffectiveLayers

            if level < 0 then invalidArg "level must be positive"
            elif level >= texture.Levels then invalidArg "level must be smaller %d" texture.Levels
        
            if baseLayer < 0 then invalidArg "baseLayer must be positive"
            elif baseLayer >= layerCount then invalidArg "baseLayer must be smaller than %d" layers
        
            if layers < 1 then invalidArg "layers must be greater than 0"
            if baseLayer + layers > layerCount then invalidArg "texture has insufficient layers" 

            let size = levelSize texture.Size level
            let device = texture.Device
            { new TextureLevel with
                member x.Texture = texture
                member x.BaseLevel = level
                member x.Levels = 1
                member x.BaseLayer = baseLayer
                member x.Layers = layers
                member x.Level = level
                member x.Size3D = size
                member x.Offset = V3i.Zero
                member x.TextureLevel = x

                member x.Runtime = device.Runtime :> _
                member x.Format = unbox (int x.Texture.Format)
                member x.Samples = defaultArg x.Texture.Samples 1
                member x.Size = x.Size3D.XY
            }
        
        let layer (texture : Texture) (baseLevel : int) (levels : int) (layer : int) =
            let layerCount = texture.EffectiveLayers

            if baseLevel < 0 then invalidArg "baseLevel must be positive"
            elif baseLevel >= texture.Levels then invalidArg "baseLevel must be smaller %d" texture.Levels
        
            if levels < 1 then invalidArg "levels must be greater than 0"
            if baseLevel + levels > texture.Levels then invalidArg "texture has insufficient levels" 

            if layer < 0 then invalidArg "layer must be positive"
            elif layer >= layerCount then invalidArg "layer must be smaller than %d" layerCount
        
            { new TextureLayer with
                member x.Texture = texture
                member x.BaseLevel = baseLevel
                member x.Levels = levels
                member x.BaseLayer = layer
                member x.Layers = 1
                member x.Layer = layer
            }
         
        let image (texture : Texture) (level : int) (layer : int) =
            let layerCount = texture.EffectiveLayers

            if level < 0 then invalidArg "baseLevel must be positive"
            elif level >= texture.Levels then invalidArg "baseLevel must be smaller %d" texture.Levels
        
            if layer < 0 then invalidArg "layer must be positive"
            elif layer >= layerCount then invalidArg "layer must be smaller than %d" layerCount
        
            let size = levelSize texture.Size level
            let device = texture.Device


            { new TextureImage with
                member x.Texture = texture
                member x.BaseLevel = level
                member x.Levels = 1
                member x.BaseLayer = layer
                member x.Layers = 1
                member x.Layer = layer
                member x.Level = level
                member x.Size3D = size
                member x.Offset = V3i.Zero
                member x.TextureImage = x
                member x.TextureLevel = x :> _
                
                member x.Runtime = device.Runtime :> _
                member x.Format = unbox (int x.Texture.Format)
                member x.Samples = defaultArg x.Texture.Samples 1
                member x.Size = x.Size3D.XY
            }
        
    module private SubTexture =
        let level3 (minPixel : option<V3i>) (maxPixel : option<V3i>) (level : TextureLevel) =
            let minPixel = defaultArg minPixel V3i.Zero
            let maxPixel = defaultArg maxPixel (level.Size3D - V3i.III)
            if minPixel.AnySmaller 0 then invalidArg "minPixel must be positive"
            if maxPixel.AnyGreaterOrEqual level.Size3D then invalidArg "maxPixel must be in bounds"
            let size = V3i.III + maxPixel - minPixel

            { new SubTextureLevel with
                member x.TextureLevel = level
                member x.Offset = minPixel
                member x.Size3D = size
            }
            
        let level2 (minPixel : option<V2i>) (maxPixel : option<V2i>) (level : TextureLevel) =
            level3 
                (minPixel |> Option.map (fun v -> V3i(v, 0)))
                (maxPixel |> Option.map (fun v -> V3i(v, 0)))
                level

        let level1 (minPixel : option<int>) (maxPixel : option<int>) (level : TextureLevel) =
            level3 
                (minPixel |> Option.map (fun v -> V3i(v, 0, 0)))
                (maxPixel |> Option.map (fun v -> V3i(v, 0, 0)))
                level

        let image3 (minPixel : option<V3i>) (maxPixel : option<V3i>) (image : TextureImage) =
            let minPixel = defaultArg minPixel V3i.Zero
            let maxPixel = defaultArg maxPixel (image.Size3D - V3i.III)
            if minPixel.AnySmaller 0 then invalidArg "minPixel must be positive"
            if maxPixel.AnyGreaterOrEqual image.Size3D then invalidArg "maxPixel must be in bounds"
            let size = V3i.III + maxPixel - minPixel

            { new SubTextureImage with
                member x.TextureLevel = image :> _
                member x.TextureImage = image
                member x.Offset = minPixel
                member x.Size3D = size
            }
         
        let image2 (minPixel : option<V2i>) (maxPixel : option<V2i>) (level : TextureImage) =
            image3 
                (minPixel |> Option.map (fun v -> V3i(v, 0)))
                (maxPixel |> Option.map (fun v -> V3i(v, 0)))
                level

        let image1 (minPixel : option<int>) (maxPixel : option<int>) (level : TextureImage) =
            image3 
                (minPixel |> Option.map (fun v -> V3i(v, 0, 0)))
                (maxPixel |> Option.map (fun v -> V3i(v, 0, 0)))
                level

    type Texture with
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, minLayer : option<int>, maxLayer : option<int>) =
            let layers = x.EffectiveLayers
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (layers - 1)
            TextureSlice.range x minLevel (1 + maxLevel - minLevel) minLayer (1 + maxLayer - minLayer)
            
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, layer : int) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            TextureSlice.layer x minLevel (1 + maxLevel - minLevel) layer

        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>) =
            let layers = x.EffectiveLayers
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (layers - 1)
            TextureSlice.level x level minLayer (1 + maxLayer - minLayer)
            
        member x.Item
            with get(level : int) =
                let layers = x.EffectiveLayers
                TextureSlice.level x level 0 layers

        member x.Item
            with get(level : int, layer : int) =
                TextureSlice.image x level layer

        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level3 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.[level, layer]
            |> SubTexture.image3 minPixel maxPixel
            
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level2 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.[level, layer]
            |> SubTexture.image2 minPixel maxPixel
            
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<int>, maxPixel : option<int>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level1 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<int>, maxPixel : option<int>) =
            x.[level, layer]
            |> SubTexture.image1 minPixel maxPixel

    type TextureRange with
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, minLayer : option<int>, maxLayer : option<int>) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)
            TextureSlice.range 
                x.Texture 
                (x.BaseLevel + minLevel) (1 + maxLevel - minLevel)
                (x.BaseLayer + minLayer) (1 + maxLayer - minLayer)
            
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, layer : int) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            
            TextureSlice.layer 
                x.Texture 
                (x.BaseLevel + minLevel) (1 + maxLevel - minLevel)
                (x.BaseLayer + layer)

        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>) =
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)
            
            TextureSlice.level 
                x.Texture 
                (x.BaseLevel + level)
                (x.BaseLayer + minLayer) (1 + maxLayer - minLayer)

        member x.Item
            with get(level : int, layer : int) =
                TextureSlice.image x.Texture (x.BaseLevel + level) (x.BaseLayer + layer)
                
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level3 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.[level, layer]
            |> SubTexture.image3 minPixel maxPixel
            
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level2 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.[level, layer]
            |> SubTexture.image2 minPixel maxPixel
            
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>, minPixel : option<int>, maxPixel : option<int>) =
            x.GetSlice(level, minLayer, maxLayer)
            |> SubTexture.level1 minPixel maxPixel

        member x.GetSlice(level : int, layer : int, minPixel : option<int>, maxPixel : option<int>) =
            x.[level, layer]
            |> SubTexture.image1 minPixel maxPixel


    type TextureLevel with
        member x.GetSlice(minLayer : option<int>, maxLayer : option<int>) =
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)
            TextureSlice.level x.Texture x.Level (x.BaseLayer + minLayer) (1 + maxLayer - minLayer)

        member x.Item
            with get(layer : int) =
                TextureSlice.image x.Texture x.Level (x.BaseLayer + layer)
                
        member x.GetSlice(minLayer : option<int>, maxLayer : option<int>, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.GetSlice(minLayer, maxLayer)
            |> SubTexture.level3 minPixel maxPixel

        member x.GetSlice(layer : int, minPixel : option<V3i>, maxPixel : option<V3i>) =
            x.[layer]
            |> SubTexture.image3 minPixel maxPixel
            
        member x.GetSlice(minLayer : option<int>, maxLayer : option<int>, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.GetSlice(minLayer, maxLayer)
            |> SubTexture.level2 minPixel maxPixel

        member x.GetSlice(layer : int, minPixel : option<V2i>, maxPixel : option<V2i>) =
            x.[layer]
            |> SubTexture.image2 minPixel maxPixel
            
        member x.GetSlice(minLayer : option<int>, maxLayer : option<int>, minPixel : option<int>, maxPixel : option<int>) =
            x.GetSlice(minLayer, maxLayer)
            |> SubTexture.level1 minPixel maxPixel

        member x.GetSlice(layer : int, minPixel : option<int>, maxPixel : option<int>) =
            x.[layer]
            |> SubTexture.image1 minPixel maxPixel

    type TextureLayer with  
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            TextureSlice.layer x.Texture (x.BaseLevel + minLevel) (1 + maxLevel - minLevel) x.Layer
        
        member x.Item
            with get(level : int) =
                TextureSlice.image  x.Texture (x.BaseLevel + level) x.Layer

    let private compilerTests (texture : Texture) =
        let range : TextureRange = texture.[*,*]
        let level : TextureLevel = texture.[0,*]
        let layer : TextureLayer = texture.[*,0]
        let image : TextureImage = texture.[0,0]

        ignore<SubTextureLevel> level
        ignore<SubTextureLevel> image
        ignore<SubTextureImage> image


        // Texture slices
        texture.[*,*]       |> ignore<TextureRange>
        texture.[1..2,3..4] |> ignore<TextureRange>
        texture.[1,*]       |> ignore<TextureLevel>
        texture.[1,3..4]    |> ignore<TextureLevel>
        texture.[1..2,3]    |> ignore<TextureLayer>
        texture.[*,3]       |> ignore<TextureLayer>
        texture.[1,2]       |> ignore<TextureImage>

        texture.[2, *, V3i.Zero .. V3i.III] |> ignore<SubTextureLevel>
        texture.[2, 3, V3i.Zero .. V3i.III] |> ignore<SubTextureImage>
        texture.[2, *, V2i.Zero .. V2i.II] |> ignore<SubTextureLevel>
        texture.[2, 3, V2i.Zero .. V2i.II] |> ignore<SubTextureImage>
        texture.[2, *, 0 .. 1] |> ignore<SubTextureLevel>
        texture.[2, 3, 0 .. 1] |> ignore<SubTextureImage>

        
        // TextureRange slices
        range.[*,*]         |> ignore<TextureRange>
        range.[1..2,3..4]   |> ignore<TextureRange>
        range.[1,*]         |> ignore<TextureLevel>
        range.[1,3..4]      |> ignore<TextureLevel>
        range.[1..2,3]      |> ignore<TextureLayer>
        range.[*,3]         |> ignore<TextureLayer>
        range.[1,2]         |> ignore<TextureImage>
        range.[2, *, V3i.Zero .. V3i.III] |> ignore<SubTextureLevel>
        range.[2, 3, V3i.Zero .. V3i.III] |> ignore<SubTextureImage>
        range.[2, *, V2i.Zero .. V2i.II] |> ignore<SubTextureLevel>
        range.[2, 3, V2i.Zero .. V2i.II] |> ignore<SubTextureImage>
        range.[2, *, 0 .. 1] |> ignore<SubTextureLevel>
        range.[2, 3, 0 .. 1] |> ignore<SubTextureImage>

        // TextureLevel slices
        level.[*]           |> ignore<TextureLevel>
        level.[3..4]        |> ignore<TextureLevel>
        level.[2]           |> ignore<TextureImage>
        
        level.[*, V3i.Zero .. V3i.III] |> ignore<SubTextureLevel>
        level.[3, V3i.Zero .. V3i.III] |> ignore<SubTextureImage>
        level.[*, V2i.Zero .. V2i.II] |> ignore<SubTextureLevel>
        level.[3, V2i.Zero .. V2i.II] |> ignore<SubTextureImage>
        level.[*, 0 .. 1] |> ignore<SubTextureLevel>
        level.[3, 0 .. 1] |> ignore<SubTextureImage>

        // TextureLayer slices
        layer.[*]           |> ignore<TextureLayer>
        layer.[3..4]        |> ignore<TextureLayer>
        layer.[2]           |> ignore<TextureImage>

[<AbstractClass; Sealed; Extension>]
type TextureSliceCommandStreamExtensions private() =

    static let cubeTargets =
        [|
            TextureTarget.TextureCubeMapPositiveX
            TextureTarget.TextureCubeMapNegativeX
            TextureTarget.TextureCubeMapPositiveY
            TextureTarget.TextureCubeMapNegativeY
            TextureTarget.TextureCubeMapPositiveZ
            TextureTarget.TextureCubeMapNegativeZ
        |]
    static let bindings =
        Dict.ofArray [|
            TextureTarget.Texture1D, GetPName.TextureBinding1D
            TextureTarget.Texture1DArray, GetPName.TextureBinding1DArray
            TextureTarget.Texture2D, GetPName.TextureBinding2D
            TextureTarget.Texture2DArray, GetPName.TextureBinding2DArray
            TextureTarget.TextureCubeMap, GetPName.TextureBindingCubeMap
            TextureTarget.Texture3D, GetPName.TextureBinding3D
        |]

    /// Copies the contents of the specified `PixelBuffer` to the given sub-range of the texture.
    /// Note that when copying to an Array texture the `PixelBuffer` is expected to have a matching
    /// number of layers (in its first otherwise unused dimension)
    [<Extension>]
    static member Copy(x : CommandStream, src : PixelBuffer, dst : SubTextureLevel) =
        let backend = x.BaseStream
        // Status: 
        //    implemented with/without using DSA
        //    using PixelBufferObject (core since OpenGL 2.1)
        let dstLevel = dst.TextureLevel

        let target = dstLevel.Texture.Target
        let tmp = APtr.temporary 1
        backend.GetIntegerv(APtr.constant bindings.[target], tmp)
        backend.BindTexture(target, dstLevel.Texture.Handle)
        backend.BindBuffer(BufferTargetARB.PixelUnpackBuffer, src.Handle)

        let target = dstLevel.Texture.Target
                
        match dstLevel.Texture.Dimension with
        | TextureDimension.Texture1D ->
            match dstLevel.Texture.Layers with
            | Some _ ->
                if src.Size.X <> dst.Size3D.X then failf "mismatching size: %A vs %A" src.Size.X dst.Size3D.X
                if src.Size.Y <> dstLevel.Layers then failf "mismatching layer count: %d vs %d" src.Size.Y dstLevel.Layers
                backend.TexSubImage2D(
                    target, dstLevel.Level, 
                    dst.Offset.X, dstLevel.BaseLayer,
                    uint32 dst.Size3D.X, uint32 dstLevel.Layers,
                    src.PixelFormat, src.PixelType,
                    0n   
                )
            | None ->
                if src.Size.X <> dst.Size3D.X then failf "mismatching size: %A vs %A" src.Size.X dst.Size3D.X
                fail "no 1D textures"
                //backend.TexSubImage1D(
                //    target, dstLevel.Level, 
                //    dst.Offset.X, 
                //    uint32 dst.Size3D.X, 
                //    src.PixelFormat, src.PixelType,
                //    0n   
                //)
                
        | TextureDimension.Texture2D ->
            match dstLevel.Texture.Layers with
            | Some _ ->
                if src.Size.XY <> dst.Size3D.XY then failf "mismatching size: %A vs %A" src.Size.XY dst.Size3D.XY
                if src.Size.Z <> dstLevel.Layers then failf "mismatching layer count: %d vs %d" src.Size.Z dstLevel.Layers
                backend.TexSubImage3D(
                    target, dstLevel.Level, 
                    dst.Offset.X, dstLevel.Size.Y - dst.Size3D.Y - dst.Offset.Y, dstLevel.BaseLayer,
                    uint32 dst.Size3D.X, uint32 dst.Size3D.Y, uint32 dstLevel.Layers,
                    src.PixelFormat, src.PixelType,
                    0n   
                )
            | None ->
                if src.Size.XY <> dst.Size3D.XY then failf "mismatching size: %A vs %A" src.Size.XY dst.Size3D.XY
                backend.TexSubImage2D(
                    target, dstLevel.Level, 
                    dst.Offset.X, dstLevel.Size.Y - dst.Size3D.Y - dst.Offset.Y,
                    uint32 dst.Size3D.X, uint32 dst.Size3D.Y,
                    src.PixelFormat, src.PixelType,
                    0n
                )
        
        | TextureDimension.Texture3D ->
            if src.Size <> dst.Size3D then failf "mismatching size: %A vs %A" src.Size dst.Size3D
            backend.TexSubImage3D(
                target, dstLevel.Level, 
                dst.Offset.X, dstLevel.Size.Y - dst.Size3D.Y - dst.Offset.Y, dst.Offset.Z,
                uint32 dst.Size3D.X, uint32 dst.Size3D.Y, uint32 dst.Size3D.Z,
                src.PixelFormat, src.PixelType,
                0n   
            )

        | TextureDimension.TextureCube ->
            if src.Size.XY <> dst.Size3D.XY then failf "mismatching size: %A vs %A" src.Size.XY dst.Size3D.XY
            if src.Size.Z <> dstLevel.Layers then failf "mismatching layer count: %d vs %d" src.Size.Z dstLevel.Layers

            match dstLevel.Texture.Layers with
            | Some _layers ->
                backend.TexSubImage3D(
                    target, dstLevel.Level, 
                    dst.Offset.X, dstLevel.Size.Y - dst.Size3D.Y - dst.Offset.Y, dstLevel.BaseLayer,
                    uint32 dst.Size3D.X, uint32 dst.Size3D.Y, uint32 dstLevel.Layers,
                    src.PixelFormat, src.PixelType,
                    0n   
                )
            | None ->
                let layerSize = nativeint src.SizeInBytes / nativeint dstLevel.Layers
                let mutable layer = dstLevel.BaseLayer
                let mutable srcOffset = 0n

                for i in 0 .. dstLevel.Layers - 1 do
                    let target = cubeTargets.[layer]

                    backend.TexSubImage2D(
                        target, dstLevel.Level,
                        dst.Offset.X, dstLevel.Size.Y - dst.Size3D.Y - dst.Offset.Y,
                        uint32 dst.Size3D.X, uint32 dst.Size3D.Y,
                        src.PixelFormat, src.PixelType,
                        srcOffset   
                    )

                    layer <- layer + 1
                    srcOffset <- srcOffset + layerSize

        | _ ->
            failwith "unreachable"


        backend.BindTexture(APtr.constant target, APtr.cast tmp)
        backend.BindBuffer(BufferTargetARB.PixelUnpackBuffer, 0u)

        
    [<Extension>]
    static member CreateTexture<'a when 'a :> ITexture>(this : Device, value : 'a) =
        match value :> ITexture with
            | :? Texture as t ->
                t.AddReference()
                t

            | :? PixTexture2d as t ->
                let l0 = t.PixImageMipMap.[0]

                let levels =
                    if t.TextureParams.wantMipMaps then 
                        if t.PixImageMipMap.LevelCount = 1 then  1 + int (floor (Fun.Log2(max (float l0.Size.X) (float l0.Size.Y))))
                        else t.PixImageMipMap.LevelCount
                    else
                        1

                let tex = this.CreateTexture2D(TextureFormat.ofPixFormat l0.PixFormat t.TextureParams, l0.Size, levels = levels)

                let pbos = 
                    Array.init (min levels t.PixImageMipMap.LevelCount) (fun level ->
                        let img = t.PixImageMipMap.[level]
                        let pbo = this.GetPixelBuffer(tex.Format, V3i(img.Size, 1))
                        pbo.Write(img)
                        pbo
                    )

                this.RunCommand (fun cmd ->
                    for i in 0 .. pbos.Length - 1 do
                        cmd.Copy(pbos.[i], tex.[i])

                    if pbos.Length = 1 && levels > 1 then
                        cmd.BaseStream.BindTexture(TextureTarget.Texture2D, tex.Handle)
                        cmd.BaseStream.GenerateMipmap(TextureTarget.Texture2D)
                        cmd.BaseStream.BindTexture(TextureTarget.Texture2D, 0u)
                )

                for p in pbos do p.Dispose()
                tex

            | :? PixTexture3d as t ->
                let img = t.PixVolume
                let tex = this.CreateTexture3D(TextureFormat.ofPixFormat img.PixFormat t.TextureParams, img.Size)

                use pbo = this.GetPixelBuffer(tex.Format, img.Size)
                pbo.Write(img)
                this.RunCommand (fun cmd -> cmd.Copy(pbo, tex.[0,0]))

                tex
                            
            | :? PixTextureCube as t ->
                let fx = t.PixImageCube.[CubeSide.PositiveX]
                let l0 = fx.[0]
                if l0.Size.X <> l0.Size.Y then failf "CubeMap is not square: %A" l0.Size

                let levels =
                    if t.TextureParams.wantMipMaps then 
                        if fx.LevelCount = 1 then  1 + int (floor (Fun.Log2(float l0.Size.X)))
                        else fx.LevelCount
                    else
                        1
                            
                let tex = this.CreateTextureCube(TextureFormat.ofPixFormat l0.PixFormat t.TextureParams, l0.Size.X, levels = levels)
                let pbos = 
                    let faces = [| CubeSide.PositiveX; CubeSide.PositiveY; CubeSide.PositiveZ; CubeSide.NegativeX; CubeSide.NegativeY; CubeSide.NegativeZ |]
                    Array.init (min levels fx.LevelCount) (fun level ->
                        let s = fx.[level].Size.X
                        let pbo = this.GetPixelBuffer(tex.Format, V3i(s, s, 6))

                        let data = faces |> Array.map (fun f -> t.PixImageCube.[f].[level])
                        pbo.Write data

                        pbo
                    )

                this.RunCommand (fun cmd ->
                    for i in 0 .. pbos.Length - 1 do
                        cmd.Copy(pbos.[i], tex.[i])

                    if levels <> pbos.Length then
                        cmd.BaseStream.BindTexture(TextureTarget.TextureCubeMap, tex.Handle)
                        cmd.BaseStream.GenerateMipmap(TextureTarget.TextureCubeMap)
                        cmd.BaseStream.BindTexture(TextureTarget.TextureCubeMap, 0u)
                )

                for p in pbos do p.Dispose()
                tex

            | :? FileTexture as t ->
                let img =
                    if System.IO.File.Exists t.FileName then
                        try PixImageSharp.Create t.FileName
                        with _ ->   
                            try PixImage.Create t.FileName
                            with _ -> failf "could not load image from %s" t.FileName
                    else
                        failf "texture file %s does not exist" t.FileName
                            

                let levels = 
                    if t.TextureParams.wantMipMaps then 1 + int (floor (Fun.Log2(max (float img.Size.X) (float img.Size.Y))))
                    else 1
                                
                let tex = this.CreateTexture2D(TextureFormat.ofPixFormat img.PixFormat t.TextureParams, img.Size, levels = levels)
                use pbo = this.GetPixelBuffer(tex.Format, V3i(img.Size, 1))
                pbo.Write(img)
                            
                this.RunCommand (fun cmd ->
                    cmd.Copy(pbo, tex.[0])

                    if levels > 1 then
                        cmd.BaseStream.BindTexture(TextureTarget.Texture2D, tex.Handle)
                        cmd.BaseStream.GenerateMipmap(TextureTarget.Texture2D)
                        cmd.BaseStream.BindTexture(TextureTarget.Texture2D, 0u)
                )

                tex
            | _ ->
                failf "bad texture: %A" value

    [<Extension>]
    static member GenerateMipMaps(x : CommandStream, tex : Texture) =
        if tex.Levels > 1 then
            let backend = x.BaseStream
            let target = tex.Target
            let tmp = APtr.temporary 1
            backend.GetIntegerv(APtr.constant bindings.[target], tmp)
            backend.BindTexture(target, tex.Handle)
            backend.GenerateMipmap(target)
            backend.BindTexture(APtr.constant target, APtr.cast tmp)
            
        ()
