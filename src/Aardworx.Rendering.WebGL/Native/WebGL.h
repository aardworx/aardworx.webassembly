
#ifndef _WEBGL_H
#define _WEBGL_H
#include <emscripten.h>
#include <emscripten/html5.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <GLES3/gl3.h>

void glGetBufferSubData(GLenum theTarget, GLintptr theOffset, GLsizeiptr theSize, void* theData);

void glCommit();


typedef struct {
    int32_t index;
    int32_t offset;
    int32_t size;
    int32_t stride;
    GLenum type;
    int32_t normalized;
    int32_t integer;
    int32_t buffer;
    int32_t divisor;
} VertexAttribInfo;

// {
//     IndexType           : DrawElementsType
//     IndexOffset         : nativeint
//     IndexElementSize    : nativeint
//     BindingCount        : int
//     Bindings            : nativeptr<VertexAttribInfo>
// }

typedef struct {
    GLenum indexType;
    intptr_t indexOffset;
    intptr_t indexElementSize;
    int32_t bindingCount;
    VertexAttribInfo* bindings;
} VertexBufferBindingInfo;

typedef struct {
    uint32_t  count;
    uint32_t  instanceCount;
    uint32_t  firstIndex;
    uint32_t  baseInstance;
    uint32_t  baseVertex;
} DrawElementsIndirectCommand;


void glMultiDrawArraysIndirect(GLenum mode, uint32_t indirectBuffer, int count, VertexBufferBindingInfo* bindingInfo);
void glMultiDrawArrays(GLenum mode, DrawElementsIndirectCommand* indirectBuffer, int count, VertexBufferBindingInfo* bindingInfo);

void glMultiDrawElementsIndirect(GLenum mode, uint32_t indirectBuffer, int count, GLenum indexType, VertexBufferBindingInfo* bindingInfo);
void glMultiDrawElements(GLenum mode, DrawElementsIndirectCommand* indirectBuffer, int count, GLenum indexType, VertexBufferBindingInfo* bindingInfo);

void glTexSubImage2DJSImage(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint width, GLint height, GLenum format, GLenum type, int image);


#endif