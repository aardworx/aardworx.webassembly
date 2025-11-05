// Minimal WebGPU JavaScript stub library for testing --js-library integration
// This is a test stub - not a real implementation

mergeInto(LibraryManager.library, {
  wgpuCreateInstance: function(descriptor) {
    console.log('[WebGPU Stub] wgpuCreateInstance called');
    // Return a dummy handle (non-zero pointer)
    return 1;
  },

  wgpuInstanceRequestAdapter: function(instance, options, callbackInfo) {
    console.log('[WebGPU Stub] wgpuInstanceRequestAdapter called');
    // In a real implementation, this would be async
    // For now, just log that it was called
  },

  wgpuAdapterRequestDevice: function(adapter, descriptor, callbackInfo) {
    console.log('[WebGPU Stub] wgpuAdapterRequestDevice called');
  },

  wgpuDeviceGetQueue: function(device) {
    console.log('[WebGPU Stub] wgpuDeviceGetQueue called');
    return 1;
  },

  wgpuDeviceCreateBuffer: function(device, descriptor) {
    console.log('[WebGPU Stub] wgpuDeviceCreateBuffer called');
    return 1;
  },

  wgpuInstanceRelease: function(instance) {
    console.log('[WebGPU Stub] wgpuInstanceRelease called');
  },

  wgpuAdapterRelease: function(adapter) {
    console.log('[WebGPU Stub] wgpuAdapterRelease called');
  },

  wgpuDeviceRelease: function(device) {
    console.log('[WebGPU Stub] wgpuDeviceRelease called');
  },

  wgpuBufferRelease: function(buffer) {
    console.log('[WebGPU Stub] wgpuBufferRelease called');
  }
});
