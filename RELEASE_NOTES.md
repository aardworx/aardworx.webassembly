### 1.2.6
* picked up Aardvark.Dom 1.1.7 (MSAA picking, per-object snap radius, plain-float pick encoding). Validated end-to-end on WebGL — desktop GL and iOS Safari both pick correctly with `Samples 4`.
* `IRuntime.ReadPixels` PBO path was sizing the scratch PBO via `img.Array.Length` (element count) instead of bytes — broke for any non-byte format. Aardvark.Dom's pick reads `Rgba32f` 33×33 = 17,424 bytes; old code allocated a 4,356-byte PBO and `glReadPixels` errored with `INVALID_OPERATION: readPixels: buffer is not large enough for dimensions`. Fixed to `System.Buffer.ByteLength(img.Array)`.
* `IRuntime.Clear` color-attachment dispatch was 2-way (float / signed-int — both routed through `glClearBufferfv` / `glClearBufferiv`). WebGL is strict about clear-value vs attachment signedness, so any clear of an unsigned-integer color attachment threw `GL_INVALID_OPERATION: glClearBufferiv: No defined conversion between clear value and attachment format`. Made it a 3-way `Choice` (float / signed-int / unsigned-int) selecting the matching `glClearBufferfv` / `glClearBufferiv` / `glClearBufferuiv` call based on `TextureFormat.isIntegerFormat` + `TextureFormat.isSigned`.
* added `R/Rg/Rgb/Rgba 32ui` to `Pixelbuffer.toPixelFormatAndType` (mapping `RedInteger`…`RgbaInteger` + `UnsignedInt`) and `R/Rg/Rgb/Rgba 32i` + `32ui` rows to `TextureFormatVisitor` so `PixImage<int32>` / `PixImage<uint32>` can be allocated for downloads of those formats.
* corrected unsigned-integer attachment shader-output type-mapping in `Shader.fs`: `Rgba32ui`/`Rgba16ui`/`Rgba8ui`/`Rgb32ui`/`Rgb16ui`/`Rgb8ui` now map to `typeof<V4ui>`/`typeof<V3ui>` (was `typeof<V4i>` / `typeof<V3i>`), so FShade emits `out uvec4` matching the actual buffer format. WebGL rejects `out ivec4 → unsigned-integer attachment` mismatches.
* updated `Examples/Dom`: replaced the orbit playground with a three-teapot pixel-snap demo (Red/Green/Blue at radii 0/8/16). Tap a teapot to plant a yellow arrow along the surface normal. Blue uses mode B (`PickViewPosition`) to exercise the FinalB pick path on WebGL. Bound the dev server to `0.0.0.0` so the demo is reachable from a phone over Tailscale/LAN.

### 1.2.5
* fix: shader-output type for the `"Normals"` framebuffer attachment was hardcoded to `V3d`, regardless of the actual texture format. Every other attachment derives its type from `TextureFormat.toShaderType`, but `Normals` was special-cased — so a fragment writing `[<Normal>] : V3f` to an `Rgba32f` G-buffer died with `[FShade] cannot convert Normals value from V3f to V3d` (FShade has no V3f→V3d converter). Removed the special case in both `shaderType` helpers (`AssembleModule` / `CreateProgram` paths). `Normals` now uses the texture format like everything else, and FShade's built-in V3f→V4f widening (appending `1.0f`) handles the typical `Rgba32f` G-buffer naturally.

### 1.2.4
* implemented several `IRuntime` members that were previously `NotImplementedException` stubs:
  - `Clear(IBackendTexture, ClearValues)` — attaches the texture to a temporary FBO and routes through the right `glClearBuffer*` family (integer vs float color, depth-only vs depth+stencil). Mirrors the Aardvark GL backend's contract: clears level 0 / slice 0.
  - `Copy(IBackendTexture, ...)` with explicit slice/level ranges — iterates per (slice, level) and reuses the existing single-image `BlitFramebuffer` path. WebGL2 has no `glCopyImageSubData`, so per-image FBO blit is the only option.
  - `DownloadAsync(IBackendBuffer, ...)` — performs the download eagerly (WebGL is single-threaded JS — no meaningful async path) and returns a no-op thunk that satisfies the `unit -> unit` waiter contract.
  - `CreateOcclusionQuery(precise)` — wraps WebGL2's `ANY_SAMPLES_PASSED{,_CONSERVATIVE}` query target. `IsPrecise` returns `false` regardless because WebGL2 does not expose `SAMPLES_PASSED` (the exact sample count) — only the boolean any-samples-passed variants.
  - `CreateTimeQuery()` — wraps `GL_EXT_disjoint_timer_query_webgl2` `TIME_ELAPSED`. Throws if the extension isn't supported by the current context (common on iOS Safari and some Linux drivers; check `device.Info.Features.TimerQuery` first).
* added tests covering each: `texture clear (color)`, `texture copy (slice/level)`, `buffer downloadAsync`, `occlusion query`, `time query` (auto-skipped when the GPU lacks the timer extension).

### 1.2.3
* fix: `IRuntime.ReadPixels` row-flip cast `img.Array :?> byte[]`, which throws `InvalidCastException` for any non-byte format (e.g. Aardvark.Dom's Rgba32f pick buffer). Caller swallowed the exception, so picking silently returned no hits since 1.2.2. Replaced with `Buffer.BlockCopy` which operates on raw bytes regardless of element type.
* added `framebuffer clear+readback (Rgba32f)` test to cover non-byte readback and prevent the regression from coming back.

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
