# Aardworx WebGL Playwright runner

Headless harness that boots the F# Blazor WebAssembly test page
(`src/Tests/Aardworx.Rendering.WebGL.Tests`) and asserts the per-test pass/fail
flags it exposes on `window`.

## Setup

```bash
cd tests/playwright
npm install
npx playwright install chromium
```

## Run

```bash
npm test                # headless chromium
npm run test:headed     # see the page (handy when debugging tests)
```

The `webServer` block in `playwright.config.ts` runs
`dotnet run --project ../../src/Tests/Aardworx.Rendering.WebGL.Tests/...` and
waits for `http://localhost:6010` before invoking the spec.

## What the spec does

1. Navigates to `/`.
2. Polls (up to 60 s) for `window.__aardworx_test_summary__.done === true`.
3. Reads `window.__aardworx_test_results__` (one entry per test:
   `{ name, passed, skipped, durationMs, error }`).
4. Emits a soft `expect` per non-skipped result and attaches the full JSON to
   the Playwright report.
