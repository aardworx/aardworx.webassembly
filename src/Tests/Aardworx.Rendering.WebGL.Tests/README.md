# Aardworx.Rendering.WebGL.Tests

Browser-side test runner for the WebGL backend. Tests are listed in `Tests.fs`
and run on page load against a real `WebGLApplication`. Results are written to
`window.__aardworx_test_results__` for the Playwright harness in
`tests/playwright/tests/webgl.spec.ts`.

## Test runner contract

`TestResult` (see `TestRunner.fs`):

```fsharp
{ Name : string
  Passed : bool
  Skipped : bool
  DurationMs : float
  Error : string option
  Attachments : (string * string) list }   // (name, base64 data URL)
```

Tests can call `TestRunner.attach name dataUrl` from inside their body to add
artifacts (e.g. a rendered framebuffer + the expected reference) — these
appear under `attachments` in `window.__aardworx_test_results__` and are
forwarded to Playwright via `test.info().attach(...)`.

## Reference image bootstrap (`teapot reference render`)

The `teapot reference render` test renders a deterministic 256x256 lit scene
into an offscreen FBO and compares the resulting RGBA pixels to
`wwwroot/reference-teapot.png` (per-pixel L∞ ≤ 5 on each channel; pass if
fewer than 1% of pixels are out of tolerance). On failure both the actual and
expected images are attached as base64 PNG data URLs.

### One-time capture

If the reference PNG is missing (HTTP 404), or you want to refresh it after
intentional rendering changes, run the capture flow:

1. Start the dev server:
   ```
   dotnet run --project src/Tests/Aardworx.Rendering.WebGL.Tests/Aardworx.Rendering.WebGL.Tests.fsproj
   ```
2. Open `http://localhost:6010/?capture=true` in a browser.
3. The test renders into the FBO, encodes the pixels as a PNG via
   `canvas.toDataURL("image/png")`, and triggers a download of
   `reference-teapot.png` through a hidden `<a download>` link. The test
   itself reports as **Skipped** with a "captured reference" message.
4. Move the downloaded file into
   `src/Tests/Aardworx.Rendering.WebGL.Tests/wwwroot/reference-teapot.png`
   and commit it.
5. Reload `http://localhost:6010/` (no `?capture=true`). The test now does
   the real golden-image comparison.

### How it works

* Capture-mode detection: `window.location.search` is parsed via the
  existing `Location.GetQuery()` helper in `Browser.fs`. `?capture=true`,
  `?capture=1`, or a bare `?capture` flag all trigger capture.
* Reference fetch: `JSImage.tryLoad` loads the PNG; an offscreen 2D canvas
  + `getImageData(...)` extracts top-left-origin RGBA bytes (the WebGL
  backend's `ReadPixels` already applies `MirrorY`, so both sides agree on
  orientation).
* If the fetch returns `None` (404 / decode error), the test auto-falls
  back to capture mode — no manual flag required for the very first run.

## Running

```
dotnet build Aardworx.WebAssembly.sln
dotnet run --project src/Tests/Aardworx.Rendering.WebGL.Tests/Aardworx.Rendering.WebGL.Tests.fsproj
```

The Playwright spec drives the browser; do not run `npm test` without
chromium installed.
