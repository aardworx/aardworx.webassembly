// One-shot bootstrap: launch headed Chromium, hit ?capture=true, save the
// generated PNG into the test project's wwwroot. Run via:
//   npx tsx scripts/capture-reference.ts
// or:
//   npx playwright test scripts/capture-reference.ts (after wiring as a test)
//
// We keep this as a standalone script so the dev server is started by the
// caller (or via npm run capture) — no Playwright test runner required.

import { chromium } from '@playwright/test';
import * as fs from 'fs';
import * as path from 'path';

const REF_PATH = path.resolve(
    __dirname,
    '../../../src/Tests/Aardworx.Rendering.WebGL.Tests/wwwroot/reference-teapot.png'
);

async function main() {
    const browser = await chromium.launch({
        headless: false,
        args: ['--disk-cache-size=0', '--media-cache-size=0'],
    });
    const ctx = await browser.newContext({ acceptDownloads: true });
    await ctx.route('**/*', route => route.continue({ headers: { ...route.request().headers(), 'Cache-Control': 'no-cache, no-store, must-revalidate' } }));
    const page = await ctx.newPage();

    page.on('console', msg => console.log(`[browser ${msg.type()}]`, msg.text()));
    page.on('pageerror', err => console.error('[browser error]', err.message));

    console.log('navigating with ?capture=true');
    await page.goto('http://localhost:6010/?capture=true');

    // Race: either the page triggers a real download via <a download>, or it
    // just stashes the data URL on window.__aardworx_capture_url__.
    const downloadPromise = page.waitForEvent('download', { timeout: 90_000 }).catch(() => null);
    const dataUrlPromise = page.waitForFunction(
        () => typeof (window as any).__aardworx_capture_url__ === 'string',
        null,
        { timeout: 90_000 }
    ).catch(() => null);

    const winner = await Promise.race([downloadPromise, dataUrlPromise]);

    if (winner && 'saveAs' in (winner as any)) {
        const dl = winner as Awaited<ReturnType<typeof page.waitForEvent>>;
        // @ts-ignore — Download type
        await dl.saveAs(REF_PATH);
        console.log('saved download →', REF_PATH);
    } else {
        const dataUrl: string = await page.evaluate(() => (window as any).__aardworx_capture_url__);
        if (!dataUrl) {
            throw new Error('no download and no __aardworx_capture_url__');
        }
        const m = /^data:image\/png;base64,(.+)$/.exec(dataUrl);
        if (!m) throw new Error('unexpected data URL prefix');
        fs.mkdirSync(path.dirname(REF_PATH), { recursive: true });
        fs.writeFileSync(REF_PATH, Buffer.from(m[1], 'base64'));
        console.log('wrote data URL →', REF_PATH, fs.statSync(REF_PATH).size, 'bytes');
    }

    await browser.close();
}

main().catch(err => {
    console.error(err);
    process.exit(1);
});
