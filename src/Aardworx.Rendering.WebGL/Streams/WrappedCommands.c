#include <emscripten.h>
#include <emscripten/html5.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <GLES3/gl3.h>
#include "../Native/WebGL.h"

typedef struct {
    int srcX0;
    int srcY0;
    int srcX1;
    int srcY1;
    int dstX0;
    int dstY0;
    int dstX1;
    int dstY1;
    GLenum mask;
    GLenum filter;
} glBlitFramebufferArgs;

EMSCRIPTEN_KEEPALIVE void _glBlitFramebuffer(glBlitFramebufferArgs* args) {
    glBlitFramebuffer(args->srcX0, args->srcY0, args->srcX1, args->srcY1, args->dstX0, args->dstY0, args->dstX1, args->dstY1, args->mask, args->filter);
}

EMSCRIPTEN_KEEPALIVE void _glClearBufferfi(GLenum buffer, int drawbuffer, int depth, int stencil) {
    glClearBufferfi(buffer, drawbuffer, *(float*)&depth, stencil);
}

typedef struct {
    GLenum target;
    int level;
    GLenum internalformat;
    uint32_t width;
    uint32_t height;
    uint32_t depth;
    int border;
    uint32_t imageSize;
    void* data;
} glCompressedTexImage3DArgs;

EMSCRIPTEN_KEEPALIVE void _glCompressedTexImage3D(glCompressedTexImage3DArgs* args) {
    glCompressedTexImage3D(args->target, args->level, args->internalformat, args->width, args->height, args->depth, args->border, args->imageSize, args->data);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    int zoffset;
    uint32_t width;
    uint32_t height;
    uint32_t depth;
    GLenum format;
    uint32_t imageSize;
    void* data;
} glCompressedTexSubImage3DArgs;

EMSCRIPTEN_KEEPALIVE void _glCompressedTexSubImage3D(glCompressedTexSubImage3DArgs* args) {
    glCompressedTexSubImage3D(args->target, args->level, args->xoffset, args->yoffset, args->zoffset, args->width, args->height, args->depth, args->format, args->imageSize, args->data);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    int zoffset;
    int x;
    int y;
    uint32_t width;
    uint32_t height;
} glCopyTexSubImage3DArgs;

EMSCRIPTEN_KEEPALIVE void _glCopyTexSubImage3D(glCopyTexSubImage3DArgs* args) {
    glCopyTexSubImage3D(args->target, args->level, args->xoffset, args->yoffset, args->zoffset, args->x, args->y, args->width, args->height);
}

typedef struct {
    uint32_t program;
    uint32_t index;
    uint32_t bufSize;
    uint32_t* length;
    uint32_t* size;
    GLenum* type;
    uint8_t* name;
} glGetTransformFeedbackVaryingArgs;

EMSCRIPTEN_KEEPALIVE void _glGetTransformFeedbackVarying(glGetTransformFeedbackVaryingArgs* args) {
    glGetTransformFeedbackVarying(args->program, args->index, args->bufSize, args->length, args->size, args->type, args->name);
}

typedef struct {
    GLenum target;
    uint32_t numAttachments;
    GLenum* attachments;
    int x;
    int y;
    uint32_t width;
    uint32_t height;
} glInvalidateSubFramebufferArgs;

EMSCRIPTEN_KEEPALIVE void _glInvalidateSubFramebuffer(glInvalidateSubFramebufferArgs* args) {
    glInvalidateSubFramebuffer(args->target, args->numAttachments, args->attachments, args->x, args->y, args->width, args->height);
}

EMSCRIPTEN_KEEPALIVE void _glSamplerParameterf(uint32_t sampler, GLenum pname, int param) {
    glSamplerParameterf(sampler, pname, *(float*)&param);
}

typedef struct {
    GLenum target;
    int level;
    GLenum internalformat;
    uint32_t width;
    uint32_t height;
    uint32_t depth;
    int border;
    GLenum format;
    GLenum type;
    void* pixels;
} glTexImage3DArgs;

EMSCRIPTEN_KEEPALIVE void _glTexImage3D(glTexImage3DArgs* args) {
    glTexImage3D(args->target, args->level, args->internalformat, args->width, args->height, args->depth, args->border, args->format, args->type, args->pixels);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    int zoffset;
    uint32_t width;
    uint32_t height;
    uint32_t depth;
    GLenum format;
    GLenum type;
    void* pixels;
} glTexSubImage3DArgs;

EMSCRIPTEN_KEEPALIVE void _glTexSubImage3D(glTexSubImage3DArgs* args) {
    glTexSubImage3D(args->target, args->level, args->xoffset, args->yoffset, args->zoffset, args->width, args->height, args->depth, args->format, args->type, args->pixels);
}

EMSCRIPTEN_KEEPALIVE void _glBlendColor(int red, int green, int blue, int alpha) {
    glBlendColor(*(float*)&red, *(float*)&green, *(float*)&blue, *(float*)&alpha);
}

EMSCRIPTEN_KEEPALIVE void _glClearColor(int red, int green, int blue, int alpha) {
    glClearColor(*(float*)&red, *(float*)&green, *(float*)&blue, *(float*)&alpha);
}

EMSCRIPTEN_KEEPALIVE void _glClearDepthf(int d) {
    glClearDepthf(*(float*)&d);
}

typedef struct {
    GLenum target;
    int level;
    GLenum internalformat;
    uint32_t width;
    uint32_t height;
    int border;
    uint32_t imageSize;
    void* data;
} glCompressedTexImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glCompressedTexImage2D(glCompressedTexImage2DArgs* args) {
    glCompressedTexImage2D(args->target, args->level, args->internalformat, args->width, args->height, args->border, args->imageSize, args->data);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    uint32_t width;
    uint32_t height;
    GLenum format;
    uint32_t imageSize;
    void* data;
} glCompressedTexSubImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glCompressedTexSubImage2D(glCompressedTexSubImage2DArgs* args) {
    glCompressedTexSubImage2D(args->target, args->level, args->xoffset, args->yoffset, args->width, args->height, args->format, args->imageSize, args->data);
}

typedef struct {
    GLenum target;
    int level;
    GLenum internalformat;
    int x;
    int y;
    uint32_t width;
    uint32_t height;
    int border;
} glCopyTexImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glCopyTexImage2D(glCopyTexImage2DArgs* args) {
    glCopyTexImage2D(args->target, args->level, args->internalformat, args->x, args->y, args->width, args->height, args->border);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    int x;
    int y;
    uint32_t width;
    uint32_t height;
} glCopyTexSubImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glCopyTexSubImage2D(glCopyTexSubImage2DArgs* args) {
    glCopyTexSubImage2D(args->target, args->level, args->xoffset, args->yoffset, args->x, args->y, args->width, args->height);
}

EMSCRIPTEN_KEEPALIVE void _glDepthRangef(int n, int f) {
    glDepthRangef(*(float*)&n, *(float*)&f);
}

typedef struct {
    uint32_t program;
    uint32_t index;
    uint32_t bufSize;
    uint32_t* length;
    int* size;
    GLenum* type;
    uint8_t* name;
} glGetActiveAttribArgs;

EMSCRIPTEN_KEEPALIVE void _glGetActiveAttrib(glGetActiveAttribArgs* args) {
    glGetActiveAttrib(args->program, args->index, args->bufSize, args->length, args->size, args->type, args->name);
}

typedef struct {
    uint32_t program;
    uint32_t index;
    uint32_t bufSize;
    uint32_t* length;
    int* size;
    GLenum* type;
    uint8_t* name;
} glGetActiveUniformArgs;

EMSCRIPTEN_KEEPALIVE void _glGetActiveUniform(glGetActiveUniformArgs* args) {
    glGetActiveUniform(args->program, args->index, args->bufSize, args->length, args->size, args->type, args->name);
}

EMSCRIPTEN_KEEPALIVE void _glLineWidth(int width) {
    glLineWidth(*(float*)&width);
}

EMSCRIPTEN_KEEPALIVE void _glPolygonOffset(int factor, int units) {
    glPolygonOffset(*(float*)&factor, *(float*)&units);
}

typedef struct {
    int x;
    int y;
    uint32_t width;
    uint32_t height;
    GLenum format;
    GLenum type;
    void* pixels;
} glReadPixelsArgs;

EMSCRIPTEN_KEEPALIVE void _glReadPixels(glReadPixelsArgs* args) {
    glReadPixels(args->x, args->y, args->width, args->height, args->format, args->type, args->pixels);
}

EMSCRIPTEN_KEEPALIVE void _glSampleCoverage(int value, GLenum invert) {
    glSampleCoverage(*(float*)&value, invert);
}

typedef struct {
    GLenum target;
    int level;
    GLenum internalformat;
    uint32_t width;
    uint32_t height;
    int border;
    GLenum format;
    GLenum type;
    void* pixels;
} glTexImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glTexImage2D(glTexImage2DArgs* args) {
    glTexImage2D(args->target, args->level, args->internalformat, args->width, args->height, args->border, args->format, args->type, args->pixels);
}

EMSCRIPTEN_KEEPALIVE void _glTexParameterf(GLenum target, GLenum pname, int param) {
    glTexParameterf(target, pname, *(float*)&param);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    uint32_t width;
    uint32_t height;
    GLenum format;
    GLenum type;
    void* pixels;
} glTexSubImage2DArgs;

EMSCRIPTEN_KEEPALIVE void _glTexSubImage2D(glTexSubImage2DArgs* args) {
    glTexSubImage2D(args->target, args->level, args->xoffset, args->yoffset, args->width, args->height, args->format, args->type, args->pixels);
}

EMSCRIPTEN_KEEPALIVE void _glUniform1f(int location, int v0) {
    glUniform1f(location, *(float*)&v0);
}

EMSCRIPTEN_KEEPALIVE void _glUniform2f(int location, int v0, int v1) {
    glUniform2f(location, *(float*)&v0, *(float*)&v1);
}

EMSCRIPTEN_KEEPALIVE void _glUniform3f(int location, int v0, int v1, int v2) {
    glUniform3f(location, *(float*)&v0, *(float*)&v1, *(float*)&v2);
}

EMSCRIPTEN_KEEPALIVE void _glUniform4f(int location, int v0, int v1, int v2, int v3) {
    glUniform4f(location, *(float*)&v0, *(float*)&v1, *(float*)&v2, *(float*)&v3);
}

EMSCRIPTEN_KEEPALIVE void _glVertexAttrib1f(uint32_t index, int x) {
    glVertexAttrib1f(index, *(float*)&x);
}

EMSCRIPTEN_KEEPALIVE void _glVertexAttrib2f(uint32_t index, int x, int y) {
    glVertexAttrib2f(index, *(float*)&x, *(float*)&y);
}

EMSCRIPTEN_KEEPALIVE void _glVertexAttrib3f(uint32_t index, int x, int y, int z) {
    glVertexAttrib3f(index, *(float*)&x, *(float*)&y, *(float*)&z);
}

EMSCRIPTEN_KEEPALIVE void _glVertexAttrib4f(uint32_t index, int x, int y, int z, int w) {
    glVertexAttrib4f(index, *(float*)&x, *(float*)&y, *(float*)&z, *(float*)&w);
}

typedef struct {
    GLenum target;
    int level;
    int xoffset;
    int yoffset;
    int width;
    int height;
    GLenum format;
    GLenum typ;
    int imgHandle;
} glTexSubImage2DJSImageArgs;

EMSCRIPTEN_KEEPALIVE void _glTexSubImage2DJSImage(glTexSubImage2DJSImageArgs* args) {
    glTexSubImage2DJSImage(args->target, args->level, args->xoffset, args->yoffset, args->width, args->height, args->format, args->typ, args->imgHandle);
}

