### 1.2.2
* `IRuntime.ReadPixels(IFramebuffer, ...)` no longer churns a fresh PBO per call:
  - reads ≤ 4 KiB (1-pixel picks, small thumbnails) skip the PBO entirely and `glReadPixels` directly into the pinned destination — saves 6+ GL calls per readback
  - larger reads use a Device-cached scratch PBO that's lazily created and only ever resized upward (was: GenBuffer / BufferData / DeleteBuffer every call)
* macOS Safari (Metal-bridge IPC) hit hardest by the per-call overhead; same-Mac numbers post-fix: 1×1 pick 0.28 ms (Safari) / 0.40 ms (Chrome), 256×256 0.87 ms / 1.09 ms — Safari now actually faster than Chrome on Mac
* added `readpixels benchmark` test that measures and reports timings per readback size; useful for spotting regressions across browsers

### 1.2.1
* fixed `JSImage.tryLoad` never resolving on load failure: the `<img>` error handler was registered as `oneror` (typo) instead of `onerror`, so 404s and decode errors hung forever
* fixed `WorkerContext.Terminate` throwing `JSException`: the host-side `window.workers.terminate` JS function it dispatches to was never defined; added it alongside the other `window.workers.*` shims
* added in-browser test coverage for the JS interop and Web Worker APIs (DOM mutation, `JsObj.Evaluate`, `Window.Location`, `JSImage` round-trip, `Worker.start` / send / receive / echo / dispose)

### 1.2.0
* updated to Aardvark.Rendering 5.6.4 / FShade 5.7.3 / Aardvark.Dom 1.1.0
* migrated all shader code to explicit float32/V*f types (FShade no longer silently lowers double to float)
* fixed shader output type table: every color TextureFormat (Rgba8 etc.) now maps to V*f instead of V*d, matching FShade 5.7's strict typing
* added C3f/C4f to vertex attribute lookup tables (needed for primitives that emit per-vertex float colors)
* fixed Y-orientation bug in `IRuntime.ReadPixels(IFramebuffer, ...)`: the previous `TransformedPixImage(MirrorY)` returned a strided view, leaving `.Volume.Data` bottom-up; replaced with an in-place row swap so the byte array is top-down as callers expect
* fixed `IRuntime.ReadPixels` PBO size: was hardcoded to 16 bytes (1 pixel), now uses the actual readback size — large reads previously triggered `INVALID_OPERATION: readPixels: buffer is not large enough`
* implemented `IRuntime.Download(IBackendTexture, ...)` via the FBO + glReadPixels trick (WebGL has no glGetTexImage); supports color-renderable 2D / 2D-array slice / cubemap face. 3D volumes, compressed, depth, and stencil downloads remain unimplemented (those have no clean WebGL path)
* updated remainder of the WebGL backend for the Aardvark.Rendering 5.6 API surface (uint64 buffer/texture handles + `IBufferRange`, `DrawCallInfo[]` instead of list, `OutputDescription` PascalCase fields, `voption` from `TryGetUniform`/`TryGetAttribute`, `Range1f` for shader depth range, new `IRuntime` abstracts for debug labels / micromaps / position fetch / invocation reorder, `Image.create` parameter order, `BufferView` `normalized` ctor arg, `INativeBuffer.Use`, `Cursor.ResizeH/ResizeV` rename, etc.)
* added `Aardvark.FontProvider` 0.1.1 explicit dependency (no longer transitive from Rendering.Text)
* updated `aardpack 2.0.7`, `adaptify 1.3.7`, `fshadeaot 5.7.3` (the old `fshadeaot 5.2.15` targeted net6 and would not run)
* suppressed harmless MSB3277 build noise from upstream Aardvark.Base.dll's baked System.Text.Json 10.0 reference via `Directory.Build.props`
* added Playwright + in-browser test harness under `src/Tests/Aardworx.Rendering.WebGL.Tests/` and `tests/playwright/`; covers buffer roundtrip / buffer copy / texture upload-readback / framebuffer clear-readback / a deterministic teapot golden-image render

### 1.1.14
* fixed false leaking-resource warnings in certain scenarios

### 1.1.13
* updated Aardvark.Dom and fixed build issues

### 1.1.12
* fixed MultiDrawArrays/Elements with FirstIndex 

### 1.1.11
* relaxed BlendMode constraints for integer formats

### 1.1.10
* various bugfixes

### 1.1.9
* updated DOM packages
* rendering updates

### 1.1.8
* net8.0 updates

### 1.1.7
* added simple WebXR bindings

### 1.1.6
* public WrappedCommands

### 1.1.5
* fixed glTexImage3d Call

### 1.1.4
* flag for controlling GC.Collect() before each worker message

### 1.1.3
* forced GC.Collect() before each worker message

### 1.1.2
* removed Tewr.BlazorWorker dependency

### 1.1.1
* new worker implementation

### 1.1.0
* upgraded to net7.0

### 1.0.0-prerelease0020
* volume-alloc fixes

### 1.0.0-prerelease0019
* backported volume bugfixes

### 1.0.0-prerelease0018
* BindVertexArray(0) after executing FragmentProgram

### 1.0.0-prerelease0017
* JSImage and JSTexture

### 1.0.0-prerelease0016
* WebGLApplication CommandStreamMode now respected by all CompileRender overloads

### 1.0.0-prerelease0015
* added Javascript fragments and optimized UB uploads

### 1.0.0-prerelease0014
* fixed `GetEffectInterface` 

### 1.0.0-prerelease0013
* updated packages

### 1.0.0-prerelease0012
* disabled GS simulation for now

### 1.0.0-prerelease0011
* `GetEffectInterface` now also uses shader-cache

### 1.0.0-prerelease0010
* fixed channel issue

### 1.0.0-prerelease0009
* fixed MultiDrawIndexed

### 1.0.0-prerelease0008
* RenderPasses

### 1.0.0-prerelease0007
* made logging of update-code optional

### 1.0.0-prerelease0006
* upgraded to latest packages

### 1.0.0-prerelease0005
* inlined FXAA shader utilities (maybe helping)

### 1.0.0-prerelease0004
* fixed handling of multiple messages in one emit

### 1.0.0-prerelease0003
* improved Sg.Active performance (no structural dependency on the program)

### 1.0.0-prerelease0002
* removed debug prints from sampler creation
* WebGL targets now preserve ExtraLDFlags

### 1.0.0-prerelease0001
* initial version
