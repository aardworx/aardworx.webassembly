#include <emscripten.h>
#include <emscripten/html5.h>
#include <SDL/SDL_image.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdint.h>
#include "WebGL.h"

EMSCRIPTEN_KEEPALIVE void glGetBufferSubData(GLenum theTarget, GLintptr theOffset, GLsizeiptr theSize, void* theData)
{
    EM_ASM_(
    {
        Module.ctx.getBufferSubData($0, $1, HEAPU8.subarray($2, $2 + $3));
    }, theTarget, theOffset, theData, theSize);
}
EMSCRIPTEN_KEEPALIVE void glCommit()
{
    EM_ASM_(
    {
        Module.ctx.commit();
    });
}

EMSCRIPTEN_KEEPALIVE EMSCRIPTEN_WEBGL_CONTEXT_HANDLE emCreateContext(const char* id)
{
    EmscriptenWebGLContextAttributes attrs;
    emscripten_webgl_init_context_attributes(&attrs);
    attrs.alpha = 1;
    attrs.depth = 1;
    attrs.stencil = 1;
    attrs.antialias = 0;
    attrs.premultipliedAlpha = 1;
    attrs.preserveDrawingBuffer = 1;
    attrs.majorVersion = 2;
    attrs.minorVersion = 0;
    attrs.enableExtensionsByDefault = 1;
    attrs.powerPreference = EM_WEBGL_POWER_PREFERENCE_HIGH_PERFORMANCE;
    attrs.failIfMajorPerformanceCaveat = 0;
    //attrs.explicitSwapControl = 1;
    //attrs.renderViaOffscreenBackBuffer = 1;
    EMSCRIPTEN_WEBGL_CONTEXT_HANDLE glContext = emscripten_webgl_create_context(id, &attrs);

    return glContext; 
}

EMSCRIPTEN_KEEPALIVE int emEnableExtension(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE ctx, const char* name) {
    return emscripten_webgl_enable_extension(ctx, name);
}

EMSCRIPTEN_KEEPALIVE int emSwapBuffers() {
    return emscripten_webgl_commit_frame();
}


EMSCRIPTEN_KEEPALIVE int emDestroyContext(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE handle)
{
    return emscripten_webgl_destroy_context(handle);
}

EMSCRIPTEN_KEEPALIVE int emMakeCurrent(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE ctx) {
    return emscripten_webgl_make_context_current(ctx);
}

EMSCRIPTEN_KEEPALIVE int emIsCurrent(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE ctx) {
    return emscripten_webgl_get_current_context() == ctx;  
}


EMSCRIPTEN_KEEPALIVE EMSCRIPTEN_WEBGL_CONTEXT_HANDLE emGetCurrent() {
    return emscripten_webgl_get_current_context();
}



EMSCRIPTEN_KEEPALIVE GLboolean isNextInstance(DrawElementsIndirectCommand* c0, DrawElementsIndirectCommand* c1) {
    return
        c0->firstIndex == c1->firstIndex && 
        c0->baseVertex == c1->baseVertex && 
        c0->count == c1->count &&
        c0->baseInstance + c0->instanceCount == c1->baseInstance;
}

EMSCRIPTEN_KEEPALIVE void glMultiDrawArrays(GLenum mode, DrawElementsIndirectCommand* indirect, int count, VertexBufferBindingInfo* bindingInfo) {
    GLboolean changed = 0;
    VertexAttribInfo* infos = bindingInfo->bindings;
    int attribCount = bindingInfo->bindingCount;

    int i = 0;
    DrawElementsIndirectCommand* last;
    int lastInstance = 0;

    while(i < count) {
        int instanceCount = indirect[i].instanceCount;
        int baseInstance = indirect[i].baseInstance;
        int firstIndex = indirect[i].firstIndex;
        int baseVertex = indirect[i].baseVertex;
        int count = indirect[i].count;

        last = &indirect[i];


        i++;
        // compact adjacent instances
        while(i < count && isNextInstance(last, &indirect[i])) {
            instanceCount += indirect[i].instanceCount;
            last = &indirect[i];
            i++;
        }

        if (instanceCount > 0) {

            if (baseInstance != lastInstance) {
                lastInstance = baseInstance;

                // shift instance-buffers
                for (int b = 0; b < attribCount; b++) {
                    int div = infos[b].divisor;
                    if (div != 0) {
                        int bi = baseInstance / div;
                        int index = infos[b].index;
                        glBindBuffer(GL_ARRAY_BUFFER, infos[b].buffer);
                        if (infos[b].integer) glVertexAttribIPointer(index, infos[b].size, infos[b].type, infos[b].stride, (void*)(infos[b].offset + bi * infos[b].stride));
                        else glVertexAttribPointer(index, infos[b].size, infos[b].type, infos[b].normalized, infos[b].stride, (void*)(infos[b].offset + bi * infos[b].stride));
                        changed = 1;
                    }
                }
                glBindBuffer(GL_ARRAY_BUFFER, 0);
            }

            // draw
            if (instanceCount > 1) {
                glDrawArraysInstanced(mode, firstIndex, count, instanceCount);
            }
            else {
                glDrawArrays(mode, firstIndex, count);
            }

        }

    }

    // restore original bindings
    if (changed) {
        for (int b = 0; b < attribCount; b++) {
            if (infos[b].divisor != 0) {
                int index = infos[b].index;
                glBindBuffer(GL_ARRAY_BUFFER, infos[b].buffer);
                if (infos[b].integer) glVertexAttribIPointer(index, infos[b].size, infos[b].type, infos[b].stride, (void*)infos[b].offset);
                else glVertexAttribPointer(index, infos[b].size, infos[b].type, infos[b].normalized, infos[b].stride, (void*)infos[b].offset);
            }
        }
        glBindBuffer(GL_ARRAY_BUFFER, 0);
    }
}

EMSCRIPTEN_KEEPALIVE void glMultiDrawArraysIndirect(GLenum mode, uint32_t indirectBuffer, int count, VertexBufferBindingInfo* bindingInfo) {
    DrawElementsIndirectCommand* indirect = (DrawElementsIndirectCommand*)malloc(sizeof(DrawElementsIndirectCommand) * count);
    glBindBuffer(GL_PIXEL_PACK_BUFFER, indirectBuffer);
    glGetBufferSubData(GL_PIXEL_PACK_BUFFER, 0, count * sizeof(DrawElementsIndirectCommand), (void*)indirect);
    glBindBuffer(GL_PIXEL_PACK_BUFFER, 0);
    glMultiDrawArrays(mode, indirect, count, bindingInfo);
    free(indirect);
}


EMSCRIPTEN_KEEPALIVE void glMultiDrawElements(GLenum mode, DrawElementsIndirectCommand* indirect, int count, GLenum indexType, VertexBufferBindingInfo* bindingInfo) {
    GLboolean changed = 0;
    VertexAttribInfo* infos = bindingInfo->bindings;
    int attribCount = bindingInfo->bindingCount;
    int indexSize = 4;

    switch (indexType) 
    {
        case GL_UNSIGNED_BYTE:
            indexSize = 1;
            break;
        case GL_UNSIGNED_SHORT:
            indexSize = 2;
            break;
        case GL_UNSIGNED_INT:
            indexSize = 4;
            break;
        default:
            indexSize = 4;
            break;
    }


    int i = 0;
    DrawElementsIndirectCommand* last;
    int lastInstance = 0;

    while(i < count) {
        int instanceCount = indirect[i].instanceCount;
        int baseInstance = indirect[i].baseInstance;
        int firstIndex = indirect[i].firstIndex;
        int baseVertex = indirect[i].baseVertex;
        int count = indirect[i].count;

    
        //printf("%d/%d: %d, %d, %d, %d, %d\n", i, count, instanceCount, baseInstance, firstIndex, baseVertex, count);
        
        last = &indirect[i];
        i++;
        // compact adjacent instances
        while(i < count && isNextInstance(last, &indirect[i])) {
            instanceCount += indirect[i].instanceCount;
            last = &indirect[i];
            i++;
        }

        if (instanceCount > 0) {
            // shift instance-buffers
            if (baseInstance != lastInstance) {
                lastInstance = baseInstance;
                for (int b = 0; b < attribCount; b++) {
                    int div = infos[b].divisor;
                    if (div != 0) {
                        int bi = baseInstance / div;
                        int index = infos[b].index;
                        glBindBuffer(GL_ARRAY_BUFFER, infos[b].buffer);
                        if (infos[b].integer) glVertexAttribIPointer(index, infos[b].size, infos[b].type, infos[b].stride, (void*)(infos[b].offset + bi * infos[b].stride));
                        else glVertexAttribPointer(index, infos[b].size, infos[b].type, infos[b].normalized, infos[b].stride, (void*)(infos[b].offset + bi * infos[b].stride));
                        changed = 1;
                    }
                }
                glBindBuffer(GL_ARRAY_BUFFER, 0);
            }

            // draw
            if (instanceCount > 1) {
                glDrawElementsInstanced(mode, count, indexType, (void*)(bindingInfo->indexOffset + firstIndex * indexSize), instanceCount);
            }
            else {
                glDrawElements(mode, count, indexType, (void*)(bindingInfo->indexOffset + firstIndex * indexSize));
            }

        }

    }

    // restore original bindings
    if (changed) {
        for (int b = 0; b < attribCount; b++) {
            if (infos[b].divisor != 0) {
                int index = infos[b].index;
                glBindBuffer(GL_ARRAY_BUFFER, infos[b].buffer);
                if (infos[b].integer) glVertexAttribIPointer(index, infos[b].size, infos[b].type, infos[b].stride, (void*)infos[b].offset);
                else glVertexAttribPointer(index, infos[b].size, infos[b].type, infos[b].normalized, infos[b].stride, (void*)infos[b].offset);
            }
        }
        glBindBuffer(GL_ARRAY_BUFFER, 0);
    }
}

EMSCRIPTEN_KEEPALIVE void glMultiDrawElementsIndirect(GLenum mode, uint32_t indirectBuffer, int count, GLenum indexType, VertexBufferBindingInfo* bindingInfo) {
    DrawElementsIndirectCommand* indirect = (DrawElementsIndirectCommand*)malloc(sizeof(DrawElementsIndirectCommand) * count);
    glBindBuffer(GL_PIXEL_PACK_BUFFER, indirectBuffer);
    glGetBufferSubData(GL_PIXEL_PACK_BUFFER, 0, count * sizeof(DrawElementsIndirectCommand), (void*)indirect);
    glBindBuffer(GL_PIXEL_PACK_BUFFER, 0);
    glMultiDrawElements(mode, indirect, count, indexType, bindingInfo);
    free(indirect);
}

EMSCRIPTEN_KEEPALIVE void glTexSubImage2DJSImage(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint width, GLint height, GLenum format, GLenum type, int image) {
    EM_ASM_({
        var handle = ($0 < 0 || !aardvark.imageHandles[$0]) ? null : aardvark.imageHandles[$0];
        var image = $0;
        var target = $1;
        var level = $2;
        var xoffset = $3;
        var yoffset = $4;
        var width = $5;
        var height = $6;
        var format = $7;
        var type = $8;
        var gl = Module.ctx;
        gl.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, handle);
    }, image, target, level, xoffset, yoffset, width, height, format, type);
}


EMSCRIPTEN_KEEPALIVE void* emGetProcAddress(const char* name) {
    if (strcmp(name, "glGetBufferSubData") == 0) return (void*)glGetBufferSubData;
    if (strcmp(name, "glMultiDrawArrays") == 0)return (void*)glMultiDrawArrays;
    if (strcmp(name, "glMultiDrawArraysIndirect") == 0)return (void*)glMultiDrawArraysIndirect;
    if (strcmp(name, "glMultiDrawElements") == 0)return (void*)glMultiDrawElements;
    if (strcmp(name, "glMultiDrawElementsIndirect") == 0)return (void*)glMultiDrawElementsIndirect;
    if (strcmp(name, "glCommit") == 0) return (void*)glCommit;
    if (strcmp(name, "glTexSubImage2DJSImage") == 0) return (void*)glTexSubImage2DJSImage;
    return emscripten_webgl_get_proc_address(name);
}