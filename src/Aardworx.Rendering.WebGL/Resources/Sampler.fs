namespace Aardworx.Rendering.WebGL

open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardvark.Base
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open FSharp.Data.Adaptive

type Sampler(device : Device, handle : uint32, destroy : unit -> unit) =
    inherit Resource(device, "Sampler", handle, 0L)

    override x.Destroy(gl : GL) =
        gl.DeleteSampler handle
        destroy()

    new(device : Device, handle : uint32) =
        new Sampler(device, handle, id)

[<AbstractClass; Sealed; Extension>]
type DeviceSamplerExtensions private() =

    static let wrapMode =
        LookupTable.lookupTable [
            WrapMode.Border,        int TextureWrapMode.ClampToBorder
            WrapMode.Mirror,        int TextureWrapMode.MirroredRepeat
            WrapMode.MirrorOnce,    int GLEnum.MirroredRepeat
            WrapMode.Clamp,         int TextureWrapMode.ClampToEdge
            WrapMode.Wrap,          int TextureWrapMode.Repeat
        ]

    static let minFilter =
        LookupTable.lookupTable [
            struct (ValueNone, FilterMode.Point),                      int TextureMinFilter.Nearest
            struct (ValueSome FilterMode.Point, FilterMode.Point),     int TextureMinFilter.NearestMipmapNearest
            struct (ValueSome FilterMode.Linear, FilterMode.Point),    int TextureMinFilter.NearestMipmapLinear
            
            struct (ValueNone, FilterMode.Linear),                     int TextureMinFilter.Linear
            struct (ValueSome FilterMode.Point, FilterMode.Linear),    int TextureMinFilter.LinearMipmapNearest
            struct (ValueSome FilterMode.Linear, FilterMode.Linear),   int TextureMinFilter.LinearMipmapLinear
           
        ]
        
    static let magFilter =
        LookupTable.lookupTable [
            FilterMode.Linear, int TextureMagFilter.Linear
            FilterMode.Point, int TextureMagFilter.Nearest
        ]
        
    static let compareFunc =
        LookupTable.lookupTable [
            ComparisonFunction.Always, int DepthFunction.Always
            ComparisonFunction.Equal, int DepthFunction.Equal
            ComparisonFunction.Greater, int DepthFunction.Greater
            ComparisonFunction.GreaterOrEqual, int DepthFunction.Gequal
            ComparisonFunction.Less, int DepthFunction.Less
            ComparisonFunction.LessOrEqual, int DepthFunction.Lequal
            ComparisonFunction.Never, int DepthFunction.Never
            ComparisonFunction.NotEqual, int DepthFunction.Notequal
        ]

    static let cache = ConditionalWeakTable<Device, Dict<SamplerState, Sampler>>()

    static let getOrCreate (device : Device) (s : SamplerState) (creator : (unit -> unit) -> SamplerState -> Sampler) =
        let dict = 
            match cache.TryGetValue device with
            | true, c -> c
            | _ ->
                let c = Dict()
                cache.Add(device, c)
                c
        let sam = 
            dict.GetOrCreate(s, fun s ->
                creator (fun () -> dict.Remove s |> ignore) s
            )

        if sam.TryAddReference() then 
            sam
        else 
            let res = creator (fun () -> dict.Remove s |> ignore) s
            dict.[s] <- res
            res



    [<Extension>]
    static member CreateSampler(device : Device, state : SamplerState) =
        getOrCreate device state (fun destroy state ->
            let handle =
                device.Run (fun gl ->
                    let handle = gl.GenSampler()

                    Log.debug "TextureWrapS"
                    gl.SamplerParameter(handle, SamplerParameterI.TextureWrapS, wrapMode state.AddressU)
                    
                    Log.debug "TextureWrapT"
                    gl.SamplerParameter(handle, SamplerParameterI.TextureWrapT, wrapMode state.AddressV)
                    
                    Log.debug "TextureWrapR"
                    gl.SamplerParameter(handle, SamplerParameterI.TextureWrapR, wrapMode state.AddressW)
                    
                    
                    if state.Comparison <> ComparisonFunction.Always then
                        Log.debug "TextureCompareFunc %A" state.Comparison
                        gl.SamplerParameter(handle, SamplerParameterI.TextureCompareFunc, compareFunc state.Comparison)
                    
                        Log.debug "TextureCompareMode"
                        gl.SamplerParameter(handle, SamplerParameterI.TextureCompareMode, int TextureCompareMode.CompareRefToTexture)

                    Log.debug "TextureMinFilter"
                    gl.SamplerParameter(
                        handle, SamplerParameterI.TextureMinFilter, 
                        minFilter (struct(state.Filter.MipmapMode, state.Filter.Minification))
                    )
                
                    Log.debug "TextureMagFilter"
                    gl.SamplerParameter(
                        handle, SamplerParameterI.TextureMagFilter, 
                        magFilter state.Filter.Magnification
                    )
                    if state.BorderColor <> C4f.Black then failf "cannot set borderColor to: %A" state.BorderColor
                    if state.MipLodBias <> 0.0f then failf "cannot set MipLodBias: %f" state.MipLodBias
                    //Log.debug "TextureBorderColor"
                    //gl.SamplerParameter(
                    //    handle, SamplerParameterF.TextureBorderColor, 
                    //    ReadOnlySpan [| state.BorderColor.R; state.BorderColor.G; state.BorderColor.B; state.BorderColor.A |]
                    //)

                
                    //Log.debug "TextureLodBias"
                    //gl.SamplerParameter(
                    //    handle, SamplerParameterF.TextureLodBias,
                    //    float32 state.MipLodBias
                    //)
                    
                    Log.debug "TextureMinLod"
                    gl.SamplerParameter(
                        handle, SamplerParameterF.TextureMinLod,
                        float32 state.MinLod
                    )

                    Log.debug "TextureMaxLod"
                    gl.SamplerParameter(
                        handle, SamplerParameterF.TextureMaxLod,
                        float32 state.MaxLod
                    )

                    if device.Info.Features.TextureFilterAnisotropic then
                        Log.debug "TextureMaxAnisotropy"
                        gl.SamplerParameter(
                            handle, SamplerParameterF.TextureMaxAnisotropy, 
                            float32 state.MaxAnisotropy
                        )

                    handle
                )

            new Sampler(device, handle, destroy)
        )
        
    [<Extension>]
    static member CreateSampler(device : Device, state : FShade.SamplerState) =
        device.CreateSampler(state.SamplerState)

    [<Extension>]
    static member SetSampler(this : CommandStream, index : int, sampler : Sampler) =
        this.BaseStream.BindSampler(uint32 index, sampler.Handle)
        
    [<Extension>]
    static member SetSampler(this : CommandStream, index : int, sampler : aval<Sampler>) =
        this.BaseStream.BindSampler(APtr.constant (uint32 index), sampler |> APtr.mapVal (fun s -> s.Handle))
        
    [<Extension>]
    static member SetSampler(this : CommandStream, index : int, sampler : ares<Sampler>) =
        this.SetSampler(index, this.Acquire sampler)
