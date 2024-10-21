namespace Aardworx.Rendering.WebGL

open Silk.NET.OpenGLES
open Aardvark.Rendering
open System
open Aardvark.Base

#nowarn "9"

/// Texture Limits.
type TextureLimits =
    {
        /// Maximum size of a 2D texture.
        MaxSize2d           : V2i
        /// Maximum size of a 3D texture.
        MaxSize3d           : V3i
        /// Maximum size of a cube texture.
        MaxSizeCube         : int
        /// Maximum number of layers in a texture array.
        MaxLayers           : int
        /// Maximum number of samples in a color texture.
        MaxColorSamples     : int
        /// Maximum number of samples in a depth texture.
        MaxDepthSamples     : int
        /// Maximum level-of-detail bias.
        MaxLodBias          : float
        /// Compressed texture formats.
        CompressedFormats   : Set<TextureFormat>
        /// Maximum number of texture bindings.
        MaxBindings         : int
    }
    
/// Buffer Limits.
type BufferLimits =
    {
        /// Minimum alignment for uniform buffer offsets.
        MinUniformBufferAlign       : int
        /// Maximum number of uniform buffer bindings.
        MaxUniformBufferBindings    : int
    }

/// Device features.
type DeviceFeatures =
    {
        /// Whether anisotropic texture filtering is supported.
        TextureFilterAnisotropic : bool
        
        /// Whether timer queries are supported.
        TimerQuery               : bool
    }

/// Device information.
type DeviceInformation =
    {
        /// Vendor.
        Vendor              : string
        
        /// Renderer.
        Renderer            : string
        
        /// Driver.
        Driver              : option<string>
        
        /// OpenGL ES version.
        Version             : Version
        
        /// GLSL version.
        GLSLVersion         : Version
        
        /// GLSL suffix.
        GLSLSuffix          : string
        
        /// Dedicated memory.
        Memory              : Mem

        /// Device features.
        Features            : DeviceFeatures

        /// Supported extensions.
        Extensions          : Set<string>
        
        /// Texture limits.
        TextureLimits       : TextureLimits
        
        /// Buffer limits.
        BufferLimits        : BufferLimits
    }

/// Device information module.
module DeviceInformation =
    
    /// Checks whether the given device information is from an Intel device.
    let isIntel (info : DeviceInformation) =
        info.Vendor.ToLower().Contains "intel" ||
        info.Renderer.ToLower().Contains "intel"

    /// formats the given device information.
    let toString (verbose : bool) (info : DeviceInformation) =
        String.concat "\r\n" [|
            sprintf "Device" 
            sprintf "  vendor:     %s" info.Vendor
            sprintf "  renderer:   %s" info.Renderer
            match info.Driver with
            | Some driver -> 
                sprintf "  driver:     %s" driver
            | None ->
                ()
            if info.Memory > Mem.Zero then
                sprintf "  Memory:     %A" info.Memory
            sprintf "OpenGL ES"
            sprintf "  Version:    %A" info.Version
            sprintf "  GLSL:       %A (%s)" info.GLSLVersion info.GLSLSuffix
            sprintf "  Extensions: %d" (Set.count info.Extensions)
            if verbose then
                for e in info.Extensions do
                    sprintf "    %s" e

            sprintf "  Features:"
            sprintf "    Anisotropic: %A" info.Features.TextureFilterAnisotropic
            sprintf "    TimerQuery:  %A" info.Features.TimerQuery

            sprintf "  Buffers:"
            sprintf "    Min UniformBuffer align: %d" info.BufferLimits.MinUniformBufferAlign
            sprintf "  Textures:"
            sprintf "    Max Size 2D:   %dx%d" info.TextureLimits.MaxSize2d.X info.TextureLimits.MaxSize2d.Y
            sprintf "    Max Size 3D:   %dx%dx%d" info.TextureLimits.MaxSize3d.X info.TextureLimits.MaxSize3d.Y info.TextureLimits.MaxSize3d.Z
            sprintf "    Max Size Cube: %dx%d" info.TextureLimits.MaxSizeCube info.TextureLimits.MaxSizeCube
            sprintf "    Max Layers:    %A" info.TextureLimits.MaxLayers
            sprintf "    Max Samples:   %A" info.TextureLimits.MaxColorSamples
            sprintf "    Max Bindings:  %d" info.TextureLimits.MaxBindings
            sprintf "    Compressed:    %d" (Set.count info.TextureLimits.CompressedFormats)
            if verbose then
                let mutable unknown = Set.empty
                for f in info.TextureLimits.CompressedFormats do
               
                    let name = string (unbox<GLEnum> (int f))
                    match Int32.TryParse name with
                    | true,v -> unknown <- Set.add v unknown //sprintf "      unknown(0x%08X)" v
                    | _ -> sprintf "      %s" name

                for chunk in Seq.chunkBySize 8 unknown do
                    sprintf "      %s" (chunk |> Seq.map (sprintf "0x%04X") |> String.concat ", ")


        |]

/// Device serves as the central object for interacting with the WebGL API.
type Device(ctx : WebGLContext, debug : bool) as this =
    static let mutable lastDevice : Device = Unchecked.defaultof<_>

    static let versionRx =
        System.Text.RegularExpressions.Regex @"^OpenGL[ \t]*ES[ \t]*GLSL[ \t]*ES[ \t]*([0-9]+)\.([0-9]+)(?:\.([0-9]+))?"

    let gl = ctx.GL
    do lastDevice <- this

    // read device information
    let info =
        ctx.MakeCurrent()
        try
            gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1)
            gl.PixelStore(PixelStoreParameter.PackAlignment, 1)

            let extensions =
                let cnt = gl.GetInteger(GetPName.NumExtensions)
                Array.init cnt (fun i ->
                    gl.GetStringS(StringName.Extensions, uint32 i)
                )
                |> Set.ofArray


            let version = Version(gl.GetInteger(GetPName.MajorVersion), gl.GetInteger(GetPName.MinorVersion))
            
            if version < Version(3,0) then
                let msg = sprintf "insufficient OpenGLES Version %A (at least 3.0 currently required)." version
                printfn "%s" msg
                fail msg
            
            let compressedFormats =
                let cnt = gl.GetInteger GetPName.NumCompressedTextureFormats
                let arr = Array.zeroCreate<int> cnt
                use pArr = fixed arr
                gl.GetInteger(GetPName.CompressedTextureFormats, pArr)
                arr |> Array.map unbox<TextureFormat> |> Set.ofArray



            let glsl = gl.GetStringS(StringName.ShadingLanguageVersion)
            let m = versionRx.Match glsl
            let glslVersion =
                let major = int m.Groups.[1].Value
                let mutable minor = int m.Groups.[2].Value
                let mutable build = if m.Groups.[3].Success then int m.Groups.[3].Value else -1
                let mutable revision = -1

                if minor >= 10 then
                    revision <- build
                    build <- minor % 10
                    minor <- minor / 10

                if build <= 0 then Version(major, minor)
                elif revision <= 0 then Version(major, minor, build)
                else Version(major, minor, build, revision)

            let glslSuffix = glsl.Substring(m.Length).Trim(' ', '-', '\t')


            let dedicatedMemory = Mem.Zero
            
            let UNMASKED_VENDOR_WEBGL = unbox<StringName> 0x9245
            let UNMASKED_RENDERER_WEBGL = unbox<StringName> 0x9246

            //vendor:     Google Inc. (NVIDIA)
            //renderer:   ANGLE (NVIDIA, NVIDIA GeForce RTX 2070 with Max-Q Design Direct3D11 vs_5_0 ps_5_0, D3D11-30.0.14.7141)

            //[ \t]*\,[ \t]*([^\) \t]+)[ \t]*\)$
            let angleRx = System.Text.RegularExpressions.Regex @"^ANGLE[ \t]+\((.*)\)$"

            let timerQuery =
                if Set.contains "GL_EXT_disjoint_timer_query_webgl2" extensions then
                    ctx.EnableExtension "GL_EXT_disjoint_timer_query_webgl2"
                else
                    false

            let vendor, renderer, driver = 
                if Set.contains "WEBGL_debug_renderer_info" extensions && ctx.EnableExtension "WEBGL_debug_renderer_info" then 
                    let renderer = gl.GetStringS UNMASKED_RENDERER_WEBGL
                    let m = angleRx.Match renderer
                    if m.Success then
                        let parts = m.Groups.[1].Value.Split [|','|] |> Array.map (fun s -> s.Trim())
                        match parts with
                        | [|renderer; driver|] ->
                            gl.GetStringS UNMASKED_VENDOR_WEBGL, renderer, Some driver
                        | [|vendor; renderer; driver|] ->
                            vendor, renderer, Some driver
                        | _ ->
                            gl.GetStringS UNMASKED_VENDOR_WEBGL, m.Groups.[1].Value, None
                    else
                        gl.GetStringS UNMASKED_VENDOR_WEBGL, renderer, None
                else 
                    gl.GetStringS StringName.Vendor, gl.GetStringS StringName.Renderer, None
            //
            //
            // let checkFormats =
            //     [
            //         TextureFormat.Rgba8
            //         TextureFormat.Depth24Stencil8
            //         TextureFormat.DepthComponent32
            //         TextureFormat.Rgb32i
            //         TextureFormat.Rgb32ui
            //         TextureFormat.Rgb32f
            //         TextureFormat.Rgba32i
            //         TextureFormat.Rgba32ui
            //         TextureFormat.Rgba32f
            //     ]
            //
            // for fmt in checkFormats do
            //     let sams = Array.zeroCreate<int> 8
            //     gl.GetInternalformat(GLEnum.Renderbuffer, unbox<InternalFormat> (int fmt), InternalFormatPName.Samples, 8u, System.Span<int>(sams))
            //     printfn "%A: %A" fmt (sams |> Array.filter (fun v -> v <> 0))
            //
            {
                Vendor = vendor
                Renderer = renderer
                Driver = driver
                Version = version
                GLSLVersion = glslVersion
                GLSLSuffix = glslSuffix
                Memory = dedicatedMemory
                Extensions = extensions
                Features =
                    {
                        TextureFilterAnisotropic = Set.contains "EXT_texture_filter_anisotropic" extensions
                        TimerQuery = timerQuery
                    }
                TextureLimits =
                    {
                        MaxSize2d = V2i(gl.GetInteger GetPName.MaxTextureSize)
                        MaxSize3d = V3i(gl.GetInteger GetPName.Max3DTextureSize)
                        MaxSizeCube = gl.GetInteger GetPName.MaxCubeMapTextureSize
                        MaxLayers = gl.GetInteger GetPName.MaxArrayTextureLayers
                        MaxColorSamples = gl.GetInteger (unbox<GetPName> 36183)
                        MaxDepthSamples = gl.GetInteger (unbox<GetPName> 36183)
                        MaxLodBias = gl.GetFloat GetPName.MaxTextureLodBias |> float
                        CompressedFormats = compressedFormats
                        MaxBindings = gl.GetInteger GetPName.MaxTextureImageUnits
                    }
                BufferLimits =
                    {
                        MinUniformBufferAlign = gl.GetInteger GetPName.UniformBufferOffsetAlignment
                        MaxUniformBufferBindings = gl.GetInteger GetPName.MaxUniformBufferBindings
                    }
            }
        finally
            ctx.ReleaseCurrent()

    let mutable inRun = false
    let mutable currentCall : option<string> = None
    let mutable isDisposed = false
    let onDispose = Event<unit>()
    let pending = System.Collections.Generic.Dictionary<obj, GL -> unit>()
    let mutable runtime : IRuntime = Unchecked.defaultof<_>


    new(ctx) = Device(ctx, false)

    static member internal UnsafeLastDevice = lastDevice

    /// The WebGL context.
    member x.Context = ctx
    
    /// Device Information.
    member x.Info = info
    
    /// Is the Device in debug mode.
    member x.Debug = debug
    
    /// Is the Device disposed.
    member x.IsDisposed = isDisposed

    /// Event that is triggered when the device is disposed.
    member x.OnDispose : FSharp.Control.IEvent<unit> = onDispose.Publish

    /// invoked when a resource is created.
    member x.ResourceCreated(_name : string) =
        ()

    /// invoked when a resource is destroyed.
    member x.ResourceDestroyed(_name : string) =
        ()

    /// adds an action that will be executed the next time the device runs a command.
    member x.AddPending (key : 'a, action : GL -> unit) =
        pending.[key :> obj] <- action
        ()

    /// gets or sets the current Call (used for debugging).
    member x.CurrentCall
        with get() = currentCall
        and internal set c = currentCall <- c

    /// synchronously runs an action on the virtual device-thread.
    member x.Run<'a>(action : GL -> 'a) : 'a =
        if inRun then 
            // for p in pending.Values do p gl
            // pending.Clear()
            action gl
        else
            inRun <- true
            try
                let o = WebGLContext.Current
                if o <> ctx then
                    ctx.MakeCurrent()
                    try
                        // for p in pending.Values do p gl
                        // pending.Clear()
                        action gl
                    finally
                        if isNull o then ctx.ReleaseCurrent()
                        else o.MakeCurrent()
                else
                    // for p in pending.Values do p gl
                    // pending.Clear()
                    action gl
            finally
                inRun <- false

    /// runs all pending actions.
    member x.RunPending() =
        x.Run (fun gl ->
            for p in pending.Values do p gl
            pending.Clear()
        )

    /// Disposes the device.
    member x.Dispose() =
        if not isDisposed then
            isDisposed <- true
            onDispose.Trigger()
            ctx.Dispose()
            pending.Clear()

    /// Gets/Sets the runtime.
    member x.Runtime
        with get() = runtime
        and internal set r = runtime <- r