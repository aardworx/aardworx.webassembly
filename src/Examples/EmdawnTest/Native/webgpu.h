#ifndef WEBGPU_H_
#define WEBGPU_H_

#include <stdint.h>
#include <stddef.h>

#ifdef __cplusplus
extern "C" {
#endif

// Opaque handle types
typedef struct WGPUInstanceImpl* WGPUInstance;
typedef struct WGPUAdapterImpl* WGPUAdapter;
typedef struct WGPUDeviceImpl* WGPUDevice;
typedef struct WGPUBufferImpl* WGPUBuffer;
typedef struct WGPUTextureImpl* WGPUTexture;
typedef struct WGPUQueueImpl* WGPUQueue;

// Basic types
typedef uint32_t WGPUBool;
typedef uint64_t WGPUFlags;

// ChainedStruct for extensibility
typedef struct WGPUChainedStruct {
    struct WGPUChainedStruct const * next;
    uint32_t sType;
} WGPUChainedStruct;

// String view
typedef struct WGPUStringView {
    char const * data;
    size_t length;
} WGPUStringView;

// Enums
typedef enum WGPUPowerPreference {
    WGPUPowerPreference_Undefined = 0x00000000,
    WGPUPowerPreference_LowPower = 0x00000001,
    WGPUPowerPreference_HighPerformance = 0x00000002,
} WGPUPowerPreference;

typedef enum WGPUBackendType {
    WGPUBackendType_Undefined = 0x00000000,
    WGPUBackendType_Null = 0x00000001,
    WGPUBackendType_WebGPU = 0x00000002,
    WGPUBackendType_D3D11 = 0x00000003,
    WGPUBackendType_D3D12 = 0x00000004,
    WGPUBackendType_Metal = 0x00000005,
    WGPUBackendType_Vulkan = 0x00000006,
    WGPUBackendType_OpenGL = 0x00000007,
    WGPUBackendType_OpenGLES = 0x00000008,
} WGPUBackendType;

typedef enum WGPUFeatureName {
    WGPUFeatureName_DepthClipControl = 0x00000001,
    WGPUFeatureName_Depth32FloatStencil8 = 0x00000002,
    WGPUFeatureName_TimestampQuery = 0x00000003,
} WGPUFeatureName;

typedef enum WGPURequestAdapterStatus {
    WGPURequestAdapterStatus_Success = 0x00000000,
    WGPURequestAdapterStatus_Unavailable = 0x00000001,
    WGPURequestAdapterStatus_Error = 0x00000002,
    WGPURequestAdapterStatus_Unknown = 0x00000003,
} WGPURequestAdapterStatus;

typedef enum WGPURequestDeviceStatus {
    WGPURequestDeviceStatus_Success = 0x00000000,
    WGPURequestDeviceStatus_Error = 0x00000001,
    WGPURequestDeviceStatus_Unknown = 0x00000002,
} WGPURequestDeviceStatus;

typedef enum WGPUBufferUsage {
    WGPUBufferUsage_None = 0x00000000,
    WGPUBufferUsage_MapRead = 0x00000001,
    WGPUBufferUsage_MapWrite = 0x00000002,
    WGPUBufferUsage_CopySrc = 0x00000004,
    WGPUBufferUsage_CopyDst = 0x00000008,
    WGPUBufferUsage_Index = 0x00000010,
    WGPUBufferUsage_Vertex = 0x00000020,
    WGPUBufferUsage_Uniform = 0x00000040,
    WGPUBufferUsage_Storage = 0x00000080,
    WGPUBufferUsage_Indirect = 0x00000100,
    WGPUBufferUsage_QueryResolve = 0x00000200,
} WGPUBufferUsage;

// Callback types
typedef void (*WGPURequestAdapterCallback)(WGPURequestAdapterStatus status, WGPUAdapter adapter, char const * message, void * userdata);
typedef void (*WGPURequestDeviceCallback)(WGPURequestDeviceStatus status, WGPUDevice device, char const * message, void * userdata);

// Callback info structures
typedef struct WGPURequestAdapterCallbackInfo {
    WGPUChainedStruct const * nextInChain;
    WGPURequestAdapterCallback callback;
    void * userdata;
} WGPURequestAdapterCallbackInfo;

typedef struct WGPURequestDeviceCallbackInfo {
    WGPUChainedStruct const * nextInChain;
    WGPURequestDeviceCallback callback;
    void * userdata;
} WGPURequestDeviceCallbackInfo;

// Descriptor structures
typedef struct WGPUInstanceDescriptor {
    WGPUChainedStruct const * nextInChain;
} WGPUInstanceDescriptor;

typedef struct WGPURequestAdapterOptions {
    WGPUChainedStruct const * nextInChain;
    WGPUPowerPreference powerPreference;
    WGPUBackendType backendType;
    WGPUBool forceFallbackAdapter;
} WGPURequestAdapterOptions;

typedef struct WGPUDeviceDescriptor {
    WGPUChainedStruct const * nextInChain;
    WGPUStringView label;
    size_t requiredFeatureCount;
    WGPUFeatureName const * requiredFeatures;
} WGPUDeviceDescriptor;

typedef struct WGPUBufferDescriptor {
    WGPUChainedStruct const * nextInChain;
    WGPUStringView label;
    WGPUFlags usage;
    uint64_t size;
    WGPUBool mappedAtCreation;
} WGPUBufferDescriptor;

// Core function declarations
WGPUInstance wgpuCreateInstance(WGPUInstanceDescriptor const * descriptor);
void wgpuInstanceRequestAdapter(WGPUInstance instance, WGPURequestAdapterOptions const * options, WGPURequestAdapterCallbackInfo callbackInfo);
void wgpuAdapterRequestDevice(WGPUAdapter adapter, WGPUDeviceDescriptor const * descriptor, WGPURequestDeviceCallbackInfo callbackInfo);
WGPUQueue wgpuDeviceGetQueue(WGPUDevice device);
WGPUBuffer wgpuDeviceCreateBuffer(WGPUDevice device, WGPUBufferDescriptor const * descriptor);

// Reference counting
void wgpuInstanceRelease(WGPUInstance instance);
void wgpuAdapterRelease(WGPUAdapter adapter);
void wgpuDeviceRelease(WGPUDevice device);
void wgpuBufferRelease(WGPUBuffer buffer);

#ifdef __cplusplus
} // extern "C"
#endif

#endif // WEBGPU_H_
