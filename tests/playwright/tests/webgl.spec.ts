import { test, expect } from '@playwright/test';

interface TestAttachment {
    name: string;
    dataUrl: string;
}

interface TestResult {
    name: string;
    passed: boolean;
    skipped: boolean;
    durationMs: number;
    error: string | null;
    attachments?: TestAttachment[];
}

interface TestSummary {
    total?: number;
    passed?: number;
    failed?: number;
    skipped?: number;
    done: boolean;
    fatal?: string;
}

declare global {
    interface Window {
        __aardworx_test_results__?: TestResult[];
        __aardworx_test_summary__?: TestSummary;
    }
}

test('WebGL backend tests run in browser', async ({ page }) => {
    page.on('console', msg => {
        // Helpful when debugging — drop into stdout.
        // eslint-disable-next-line no-console
        console.log(`[browser ${msg.type()}] ${msg.text()}`);
    });
    page.on('pageerror', err => {
        // eslint-disable-next-line no-console
        console.log(`[browser error] ${err.message}`);
    });

    await page.goto('/');

    // Wait up to 60s for the F# entry point to set window.__aardworx_test_summary__.done = true
    await page.waitForFunction(
        () => !!window.__aardworx_test_summary__ && window.__aardworx_test_summary__.done === true,
        undefined,
        { timeout: 60_000 }
    );

    const summary = await page.evaluate(() => window.__aardworx_test_summary__);
    const results = (await page.evaluate(() => window.__aardworx_test_results__)) ?? [];

    await test.info().attach('summary', {
        body: JSON.stringify(summary, null, 2),
        contentType: 'application/json',
    });
    await test.info().attach('results', {
        body: JSON.stringify(results, null, 2),
        contentType: 'application/json',
    });

    if (summary?.fatal) {
        throw new Error(`Fatal error in test page: ${summary.fatal}`);
    }

    expect.soft(results.length, 'no test results were emitted').toBeGreaterThan(0);

    for (const r of results) {
        for (const a of r.attachments ?? []) {
            // dataUrl looks like "data:image/png;base64,...."
            const m = /^data:([^;]+);base64,(.*)$/.exec(a.dataUrl);
            if (m) {
                await test.info().attach(`${r.name} — ${a.name}`, {
                    body: Buffer.from(m[2], 'base64'),
                    contentType: m[1],
                });
            } else {
                await test.info().attach(`${r.name} — ${a.name}`, {
                    body: a.dataUrl,
                    contentType: 'text/plain',
                });
            }
        }
        if (r.skipped) {
            // log but don't fail on skipped/pending tests
            // eslint-disable-next-line no-console
            console.log(`[skipped] ${r.name}: ${r.error ?? ''}`);
            continue;
        }
        const message = r.error ? `${r.name}\n${r.error}` : r.name;
        expect.soft(r.passed, message).toBe(true);
    }
});
