namespace EmdawnTest

open System
open System.Runtime.InteropServices

/// WebGPU opaque handle types
[<Struct>]
type WGPUInstance = WGPUInstance of nativeint

[<Struct>]
type WGPUAdapter = WGPUAdapter of nativeint

[<Struct>]
type WGPUDevice = WGPUDevice of nativeint

[<Struct>]
type WGPUBuffer = WGPUBuffer of nativeint

[<Struct>]
type WGPUQueue = WGPUQueue of nativeint

/// WebGPU enums
type WGPUPowerPreference =
    | Undefined = 0
    | LowPower = 1
    | HighPerformance = 2

type WGPUBackendType =
    | Undefined = 0
    | Null = 1
    | WebGPU = 2
    | D3D11 = 3
    | D3D12 = 4
    | Metal = 5
    | Vulkan = 6
    | OpenGL = 7
    | OpenGLES = 8

/// WebGPU descriptor structures
[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type WGPUInstanceDescriptor =
    {
        nextInChain : nativeint
    }

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type WGPURequestAdapterOptions =
    {
        nextInChain : nativeint
        powerPreference : WGPUPowerPreference
        backendType : WGPUBackendType
        forceFallbackAdapter : int
    }

/// Raw P/Invoke declarations for WebGPU functions
module WebGPURaw =

    [<DllImport("webgpu", CallingConvention = CallingConvention.Cdecl)>]
    extern WGPUInstance wgpuCreateInstance(WGPUInstanceDescriptor& descriptor)

    [<DllImport("webgpu", CallingConvention = CallingConvention.Cdecl)>]
    extern void wgpuInstanceRelease(WGPUInstance instance)

    [<DllImport("webgpu", CallingConvention = CallingConvention.Cdecl)>]
    extern void wgpuAdapterRelease(WGPUAdapter adapter)

    [<DllImport("webgpu", CallingConvention = CallingConvention.Cdecl)>]
    extern void wgpuDeviceRelease(WGPUDevice device)

    [<DllImport("webgpu", CallingConvention = CallingConvention.Cdecl)>]
    extern WGPUQueue wgpuDeviceGetQueue(WGPUDevice device)

/// Higher-level F# wrapper for WebGPU
module WebGPU =

    let createInstance() =
        let mutable descriptor = { nextInChain = 0n }
        WebGPURaw.wgpuCreateInstance(&descriptor)

    let releaseInstance (instance: WGPUInstance) =
        WebGPURaw.wgpuInstanceRelease(instance)

    let testWebGPU() =
        printfn "Testing WebGPU integration..."
        try
            let instance = createInstance()
            let (WGPUInstance handle) = instance
            printfn "Created WebGPU instance: 0x%08x" handle

            if handle <> 0n then
                releaseInstance(instance)
                printfn "Released WebGPU instance"
                true
            else
                printfn "Failed to create WebGPU instance (got null)"
                false
        with ex ->
            printfn "Exception during WebGPU test: %s" ex.Message
            false
