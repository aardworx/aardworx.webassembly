#include <emscripten.h>

EMSCRIPTEN_KEEPALIVE void installScript(const char* script)
{
    EM_ASM_({
        if (typeof document != "undefined") {
            const s = document.createElement("script");
            s.type = "application/javascript";
            s.innerText = UTF8ToString($0);
            document.head.appendChild(s);
        }
        else {
			const b = new Blob([UTF8ToString($0)], { type: "application/javascript" });
			const u = URL.createObjectURL(b);
            importScripts(u);
        }
    }, script);
}

EMSCRIPTEN_KEEPALIVE void installStyle(const char* script)
{
    EM_ASM_({
        const s = document.createElement("style");
        s.type = "text/css";
        s.innerText = UTF8ToString($0);
        document.head.appendChild(s);
    }, script);
}


EMSCRIPTEN_KEEPALIVE void emBegin(const char* line) {
    EM_ASM_(
    {
        console.group(UTF8ToString($0));
    }, line);
}



EMSCRIPTEN_KEEPALIVE void emEnd() {
    EM_ASM_(
    {
        console.groupEnd();
    });
}

EMSCRIPTEN_KEEPALIVE void emDebug(const char* line) {
    EM_ASM_(
    {
        console.debug(UTF8ToString($0));
    }, line);
}
EMSCRIPTEN_KEEPALIVE void emLog(const char* line) {
    EM_ASM_(
    {
        console.log(UTF8ToString($0));
    }, line);
}

EMSCRIPTEN_KEEPALIVE void emWarn(const char* line) {
    EM_ASM_(
    {
        console.warn(UTF8ToString($0));
    }, line);
}

EMSCRIPTEN_KEEPALIVE void emError(const char* line) {
    EM_ASM_(
    {
        console.error(UTF8ToString($0));
    }, line);
}
