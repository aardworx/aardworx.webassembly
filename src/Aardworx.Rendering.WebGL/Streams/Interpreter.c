#include <emscripten.h>
#include <emscripten/html5.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <GLES3/gl3.h>
#include "../Native/WebGL.h"

typedef enum {
    BeginQuery = 0,
    BeginQueryI = 1,
    BeginTransformFeedback = 2,
    BeginTransformFeedbackI = 3,
    BindBufferBase = 4,
    BindBufferBaseI = 5,
    BindBufferRange = 6,
    BindBufferRangeI = 7,
    BindSampler = 8,
    BindSamplerI = 9,
    BindTransformFeedback = 10,
    BindTransformFeedbackI = 11,
    BindVertexArray = 12,
    BindVertexArrayI = 13,
    BlitFramebuffer = 14,
    BlitFramebufferI = 15,
    ClearBufferiv = 16,
    ClearBufferivI = 17,
    ClearBufferuiv = 18,
    ClearBufferuivI = 19,
    ClearBufferfv = 20,
    ClearBufferfvI = 21,
    ClearBufferfi = 22,
    ClearBufferfiI = 23,
    ClientWaitSync = 24,
    ClientWaitSyncI = 25,
    CompressedTexImage3D = 26,
    CompressedTexImage3DI = 27,
    CompressedTexSubImage3D = 28,
    CompressedTexSubImage3DI = 29,
    CopyBufferSubData = 30,
    CopyBufferSubDataI = 31,
    CopyTexSubImage3D = 32,
    CopyTexSubImage3DI = 33,
    DeleteQueries = 34,
    DeleteQueriesI = 35,
    DeleteSamplers = 36,
    DeleteSamplersI = 37,
    DeleteSync = 38,
    DeleteSyncI = 39,
    DeleteTransformFeedbacks = 40,
    DeleteTransformFeedbacksI = 41,
    DeleteVertexArrays = 42,
    DeleteVertexArraysI = 43,
    DrawArraysInstanced = 44,
    DrawArraysInstancedI = 45,
    DrawBuffers = 46,
    DrawBuffersI = 47,
    DrawElementsInstanced = 48,
    DrawElementsInstancedI = 49,
    DrawRangeElements = 50,
    DrawRangeElementsI = 51,
    EndQuery = 52,
    EndQueryI = 53,
    EndTransformFeedback = 54,
    FenceSync = 55,
    FenceSyncI = 56,
    FramebufferTextureLayer = 57,
    FramebufferTextureLayerI = 58,
    GenQueries = 59,
    GenQueriesI = 60,
    GenSamplers = 61,
    GenSamplersI = 62,
    GenTransformFeedbacks = 63,
    GenTransformFeedbacksI = 64,
    GenVertexArrays = 65,
    GenVertexArraysI = 66,
    GetActiveUniformBlockiv = 67,
    GetActiveUniformBlockivI = 68,
    GetActiveUniformBlockName = 69,
    GetActiveUniformBlockNameI = 70,
    GetActiveUniformsiv = 71,
    GetActiveUniformsivI = 72,
    GetBufferParameteri64v = 73,
    GetBufferParameteri64vI = 74,
    GetFragDataLocation = 75,
    GetFragDataLocationI = 76,
    GetIntegeri_v = 77,
    GetIntegeri_vI = 78,
    GetInteger64v = 79,
    GetInteger64vI = 80,
    GetInteger64i_v = 81,
    GetInteger64i_vI = 82,
    GetInternalformativ = 83,
    GetInternalformativI = 84,
    GetProgramBinary = 85,
    GetProgramBinaryI = 86,
    GetQueryiv = 87,
    GetQueryivI = 88,
    GetQueryObjectuiv = 89,
    GetQueryObjectuivI = 90,
    GetSamplerParameteriv = 91,
    GetSamplerParameterivI = 92,
    GetSamplerParameterfv = 93,
    GetSamplerParameterfvI = 94,
    GetStringi = 95,
    GetStringiI = 96,
    GetSynciv = 97,
    GetSyncivI = 98,
    GetTransformFeedbackVarying = 99,
    GetTransformFeedbackVaryingI = 100,
    GetUniformuiv = 101,
    GetUniformuivI = 102,
    GetUniformBlockIndex = 103,
    GetUniformBlockIndexI = 104,
    GetUniformIndices = 105,
    GetUniformIndicesI = 106,
    GetVertexAttribIiv = 107,
    GetVertexAttribIivI = 108,
    GetVertexAttribIuiv = 109,
    GetVertexAttribIuivI = 110,
    InvalidateFramebuffer = 111,
    InvalidateFramebufferI = 112,
    InvalidateSubFramebuffer = 113,
    InvalidateSubFramebufferI = 114,
    IsQuery = 115,
    IsQueryI = 116,
    IsSampler = 117,
    IsSamplerI = 118,
    IsSync = 119,
    IsSyncI = 120,
    IsTransformFeedback = 121,
    IsTransformFeedbackI = 122,
    IsVertexArray = 123,
    IsVertexArrayI = 124,
    PauseTransformFeedback = 125,
    ProgramBinary = 126,
    ProgramBinaryI = 127,
    ProgramParameteri = 128,
    ProgramParameteriI = 129,
    ReadBuffer = 130,
    ReadBufferI = 131,
    RenderbufferStorageMultisample = 132,
    RenderbufferStorageMultisampleI = 133,
    ResumeTransformFeedback = 134,
    SamplerParameteri = 135,
    SamplerParameteriI = 136,
    SamplerParameteriv = 137,
    SamplerParameterivI = 138,
    SamplerParameterf = 139,
    SamplerParameterfI = 140,
    SamplerParameterfv = 141,
    SamplerParameterfvI = 142,
    TexImage3D = 143,
    TexImage3DI = 144,
    TexStorage2D = 145,
    TexStorage2DI = 146,
    TexStorage3D = 147,
    TexStorage3DI = 148,
    TexSubImage3D = 149,
    TexSubImage3DI = 150,
    TransformFeedbackVaryings = 151,
    TransformFeedbackVaryingsI = 152,
    Uniform1ui = 153,
    Uniform1uiI = 154,
    Uniform1uiv = 155,
    Uniform1uivI = 156,
    Uniform2ui = 157,
    Uniform2uiI = 158,
    Uniform2uiv = 159,
    Uniform2uivI = 160,
    Uniform3ui = 161,
    Uniform3uiI = 162,
    Uniform3uiv = 163,
    Uniform3uivI = 164,
    Uniform4ui = 165,
    Uniform4uiI = 166,
    Uniform4uiv = 167,
    Uniform4uivI = 168,
    UniformBlockBinding = 169,
    UniformBlockBindingI = 170,
    UniformMatrix2x3fv = 171,
    UniformMatrix2x3fvI = 172,
    UniformMatrix2x4fv = 173,
    UniformMatrix2x4fvI = 174,
    UniformMatrix3x2fv = 175,
    UniformMatrix3x2fvI = 176,
    UniformMatrix3x4fv = 177,
    UniformMatrix3x4fvI = 178,
    UniformMatrix4x2fv = 179,
    UniformMatrix4x2fvI = 180,
    UniformMatrix4x3fv = 181,
    UniformMatrix4x3fvI = 182,
    VertexAttribDivisor = 183,
    VertexAttribDivisorI = 184,
    VertexAttribI4i = 185,
    VertexAttribI4iI = 186,
    VertexAttribI4ui = 187,
    VertexAttribI4uiI = 188,
    VertexAttribI4iv = 189,
    VertexAttribI4ivI = 190,
    VertexAttribI4uiv = 191,
    VertexAttribI4uivI = 192,
    VertexAttribIPointer = 193,
    VertexAttribIPointerI = 194,
    WaitSync = 195,
    WaitSyncI = 196,
    ActiveTexture = 197,
    ActiveTextureI = 198,
    AttachShader = 199,
    AttachShaderI = 200,
    BindAttribLocation = 201,
    BindAttribLocationI = 202,
    BindBuffer = 203,
    BindBufferI = 204,
    BindFramebuffer = 205,
    BindFramebufferI = 206,
    BindRenderbuffer = 207,
    BindRenderbufferI = 208,
    BindTexture = 209,
    BindTextureI = 210,
    BlendColor = 211,
    BlendColorI = 212,
    BlendEquation = 213,
    BlendEquationI = 214,
    BlendEquationSeparate = 215,
    BlendEquationSeparateI = 216,
    BlendFunc = 217,
    BlendFuncI = 218,
    BlendFuncSeparate = 219,
    BlendFuncSeparateI = 220,
    BufferData = 221,
    BufferDataI = 222,
    BufferSubData = 223,
    BufferSubDataI = 224,
    CheckFramebufferStatus = 225,
    CheckFramebufferStatusI = 226,
    Clear = 227,
    ClearI = 228,
    ClearColor = 229,
    ClearColorI = 230,
    ClearDepthf = 231,
    ClearDepthfI = 232,
    ClearStencil = 233,
    ClearStencilI = 234,
    ColorMask = 235,
    ColorMaskI = 236,
    CompileShader = 237,
    CompileShaderI = 238,
    CompressedTexImage2D = 239,
    CompressedTexImage2DI = 240,
    CompressedTexSubImage2D = 241,
    CompressedTexSubImage2DI = 242,
    CopyTexImage2D = 243,
    CopyTexImage2DI = 244,
    CopyTexSubImage2D = 245,
    CopyTexSubImage2DI = 246,
    CreateProgram = 247,
    CreateProgramI = 248,
    CreateShader = 249,
    CreateShaderI = 250,
    CullFace = 251,
    CullFaceI = 252,
    DeleteBuffers = 253,
    DeleteBuffersI = 254,
    DeleteFramebuffers = 255,
    DeleteFramebuffersI = 256,
    DeleteProgram = 257,
    DeleteProgramI = 258,
    DeleteRenderbuffers = 259,
    DeleteRenderbuffersI = 260,
    DeleteShader = 261,
    DeleteShaderI = 262,
    DeleteTextures = 263,
    DeleteTexturesI = 264,
    DepthFunc = 265,
    DepthFuncI = 266,
    DepthMask = 267,
    DepthMaskI = 268,
    DepthRangef = 269,
    DepthRangefI = 270,
    DetachShader = 271,
    DetachShaderI = 272,
    Disable = 273,
    DisableI = 274,
    DisableVertexAttribArray = 275,
    DisableVertexAttribArrayI = 276,
    DrawArrays = 277,
    DrawArraysI = 278,
    DrawElements = 279,
    DrawElementsI = 280,
    Enable = 281,
    EnableI = 282,
    EnableVertexAttribArray = 283,
    EnableVertexAttribArrayI = 284,
    Finish = 285,
    Flush = 286,
    FramebufferRenderbuffer = 287,
    FramebufferRenderbufferI = 288,
    FramebufferTexture2D = 289,
    FramebufferTexture2DI = 290,
    FrontFace = 291,
    FrontFaceI = 292,
    GenBuffers = 293,
    GenBuffersI = 294,
    GenerateMipmap = 295,
    GenerateMipmapI = 296,
    GenFramebuffers = 297,
    GenFramebuffersI = 298,
    GenRenderbuffers = 299,
    GenRenderbuffersI = 300,
    GenTextures = 301,
    GenTexturesI = 302,
    GetActiveAttrib = 303,
    GetActiveAttribI = 304,
    GetActiveUniform = 305,
    GetActiveUniformI = 306,
    GetAttachedShaders = 307,
    GetAttachedShadersI = 308,
    GetAttribLocation = 309,
    GetAttribLocationI = 310,
    GetBooleanv = 311,
    GetBooleanvI = 312,
    GetBufferParameteriv = 313,
    GetBufferParameterivI = 314,
    GetError = 315,
    GetErrorI = 316,
    GetFloatv = 317,
    GetFloatvI = 318,
    GetFramebufferAttachmentParameteriv = 319,
    GetFramebufferAttachmentParameterivI = 320,
    GetIntegerv = 321,
    GetIntegervI = 322,
    GetProgramiv = 323,
    GetProgramivI = 324,
    GetProgramInfoLog = 325,
    GetProgramInfoLogI = 326,
    GetRenderbufferParameteriv = 327,
    GetRenderbufferParameterivI = 328,
    GetShaderiv = 329,
    GetShaderivI = 330,
    GetShaderInfoLog = 331,
    GetShaderInfoLogI = 332,
    GetShaderPrecisionFormat = 333,
    GetShaderPrecisionFormatI = 334,
    GetShaderSource = 335,
    GetShaderSourceI = 336,
    GetString = 337,
    GetStringI = 338,
    GetTexParameterfv = 339,
    GetTexParameterfvI = 340,
    GetTexParameteriv = 341,
    GetTexParameterivI = 342,
    GetUniformfv = 343,
    GetUniformfvI = 344,
    GetUniformiv = 345,
    GetUniformivI = 346,
    GetUniformLocation = 347,
    GetUniformLocationI = 348,
    GetVertexAttribfv = 349,
    GetVertexAttribfvI = 350,
    GetVertexAttribiv = 351,
    GetVertexAttribivI = 352,
    GetVertexAttribPointerv = 353,
    GetVertexAttribPointervI = 354,
    Hint = 355,
    HintI = 356,
    IsBuffer = 357,
    IsBufferI = 358,
    IsEnabled = 359,
    IsEnabledI = 360,
    IsFramebuffer = 361,
    IsFramebufferI = 362,
    IsProgram = 363,
    IsProgramI = 364,
    IsRenderbuffer = 365,
    IsRenderbufferI = 366,
    IsShader = 367,
    IsShaderI = 368,
    IsTexture = 369,
    IsTextureI = 370,
    LineWidth = 371,
    LineWidthI = 372,
    LinkProgram = 373,
    LinkProgramI = 374,
    PixelStorei = 375,
    PixelStoreiI = 376,
    PolygonOffset = 377,
    PolygonOffsetI = 378,
    ReadPixels = 379,
    ReadPixelsI = 380,
    ReleaseShaderCompiler = 381,
    RenderbufferStorage = 382,
    RenderbufferStorageI = 383,
    SampleCoverage = 384,
    SampleCoverageI = 385,
    Scissor = 386,
    ScissorI = 387,
    ShaderBinary = 388,
    ShaderBinaryI = 389,
    ShaderSource = 390,
    ShaderSourceI = 391,
    StencilFunc = 392,
    StencilFuncI = 393,
    StencilFuncSeparate = 394,
    StencilFuncSeparateI = 395,
    StencilMask = 396,
    StencilMaskI = 397,
    StencilMaskSeparate = 398,
    StencilMaskSeparateI = 399,
    StencilOp = 400,
    StencilOpI = 401,
    StencilOpSeparate = 402,
    StencilOpSeparateI = 403,
    TexImage2D = 404,
    TexImage2DI = 405,
    TexParameterf = 406,
    TexParameterfI = 407,
    TexParameterfv = 408,
    TexParameterfvI = 409,
    TexParameteri = 410,
    TexParameteriI = 411,
    TexParameteriv = 412,
    TexParameterivI = 413,
    TexSubImage2D = 414,
    TexSubImage2DI = 415,
    Uniform1f = 416,
    Uniform1fI = 417,
    Uniform1fv = 418,
    Uniform1fvI = 419,
    Uniform1i = 420,
    Uniform1iI = 421,
    Uniform1iv = 422,
    Uniform1ivI = 423,
    Uniform2f = 424,
    Uniform2fI = 425,
    Uniform2fv = 426,
    Uniform2fvI = 427,
    Uniform2i = 428,
    Uniform2iI = 429,
    Uniform2iv = 430,
    Uniform2ivI = 431,
    Uniform3f = 432,
    Uniform3fI = 433,
    Uniform3fv = 434,
    Uniform3fvI = 435,
    Uniform3i = 436,
    Uniform3iI = 437,
    Uniform3iv = 438,
    Uniform3ivI = 439,
    Uniform4f = 440,
    Uniform4fI = 441,
    Uniform4fv = 442,
    Uniform4fvI = 443,
    Uniform4i = 444,
    Uniform4iI = 445,
    Uniform4iv = 446,
    Uniform4ivI = 447,
    UniformMatrix2fv = 448,
    UniformMatrix2fvI = 449,
    UniformMatrix3fv = 450,
    UniformMatrix3fvI = 451,
    UniformMatrix4fv = 452,
    UniformMatrix4fvI = 453,
    UseProgram = 454,
    UseProgramI = 455,
    ValidateProgram = 456,
    ValidateProgramI = 457,
    VertexAttrib1f = 458,
    VertexAttrib1fI = 459,
    VertexAttrib1fv = 460,
    VertexAttrib1fvI = 461,
    VertexAttrib2f = 462,
    VertexAttrib2fI = 463,
    VertexAttrib2fv = 464,
    VertexAttrib2fvI = 465,
    VertexAttrib3f = 466,
    VertexAttrib3fI = 467,
    VertexAttrib3fv = 468,
    VertexAttrib3fvI = 469,
    VertexAttrib4f = 470,
    VertexAttrib4fI = 471,
    VertexAttrib4fv = 472,
    VertexAttrib4fvI = 473,
    VertexAttribPointer = 474,
    VertexAttribPointerI = 475,
    Viewport = 476,
    ViewportI = 477,
    GetBufferSubData = 478,
    GetBufferSubDataI = 479,
    MultiDrawArraysIndirect = 480,
    MultiDrawArraysIndirectI = 481,
    MultiDrawArrays = 482,
    MultiDrawArraysI = 483,
    MultiDrawElementsIndirect = 484,
    MultiDrawElementsIndirectI = 485,
    MultiDrawElements = 486,
    MultiDrawElementsI = 487,
    Commit = 488,
    TexSubImage2DJSImage = 489,
    TexSubImage2DJSImageI = 490,
    CopyDD = 512,
    CopyDI = 513,
    CopyID = 514,
    CopyII = 515,
    Add = 516,
    Mad = 517,
    Copy = 518,
    Custom = 519,
    Switch = 520,
    Jmp = 521,
    Log = 522,
    Push1 = 523,
    Pop1 = 524,
    Push2 = 525,
    Pop2 = 526,
    Push4 = 527,
    Pop4 = 528,
    Push8 = 529,
    Pop8 = 530,
    Bgra = 531,
    CopyBgra = 532,
} OpCode;
int emInterpret(intptr_t code, int length, intptr_t stack) {
    intptr_t e = code + length;
    int value, cnt;
    int temp1, temp2;
    while(code < e) {
        OpCode op = (OpCode)(*(uint16_t*)code);
        code += 2;
        switch(op) {
        case BeginQuery:
            glBeginQuery(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BeginQueryI:
            glBeginQuery(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BeginTransformFeedback:
            glBeginTransformFeedback(*(GLenum*)code);
            code += 4;
            break;
        case BeginTransformFeedbackI:
            glBeginTransformFeedback(**(GLenum**)code);
            code += 4;
            break;
        case BindBufferBase:
            glBindBufferBase(*(GLenum*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8));
            code += 12;
            break;
        case BindBufferBaseI:
            glBindBufferBase(**(GLenum**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8));
            code += 12;
            break;
        case BindBufferRange:
            glBindBufferRange(*(GLenum*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8), *(GLintptr*)(code + 12), *(GLsizeiptr*)(code + 16));
            code += 20;
            break;
        case BindBufferRangeI:
            glBindBufferRange(**(GLenum**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8), **(GLintptr**)(code + 12), **(GLsizeiptr**)(code + 16));
            code += 20;
            break;
        case BindSampler:
            glBindSampler(*(GLuint*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindSamplerI:
            glBindSampler(**(GLuint**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindTransformFeedback:
            glBindTransformFeedback(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindTransformFeedbackI:
            glBindTransformFeedback(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindVertexArray:
            glBindVertexArray(*(GLuint*)code);
            code += 4;
            break;
        case BindVertexArrayI:
            glBindVertexArray(**(GLuint**)code);
            code += 4;
            break;
        case BlitFramebuffer:
            glBlitFramebuffer(*(GLint*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLint*)(code + 20), *(GLint*)(code + 24), *(GLint*)(code + 28), *(GLbitfield*)(code + 32), *(GLenum*)(code + 36));
            code += 40;
            break;
        case BlitFramebufferI:
            glBlitFramebuffer(**(GLint**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLint**)(code + 20), **(GLint**)(code + 24), **(GLint**)(code + 28), **(GLbitfield**)(code + 32), **(GLenum**)(code + 36));
            code += 40;
            break;
        case ClearBufferiv:
            glClearBufferiv(*(GLenum*)code, *(GLint*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case ClearBufferivI:
            glClearBufferiv(**(GLenum**)code, **(GLint**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case ClearBufferuiv:
            glClearBufferuiv(*(GLenum*)code, *(GLint*)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case ClearBufferuivI:
            glClearBufferuiv(**(GLenum**)code, **(GLint**)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case ClearBufferfv:
            glClearBufferfv(*(GLenum*)code, *(GLint*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case ClearBufferfvI:
            glClearBufferfv(**(GLenum**)code, **(GLint**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case ClearBufferfi:
            glClearBufferfi(*(GLenum*)code, *(GLint*)(code + 4), *(GLfloat*)(code + 8), *(GLint*)(code + 12));
            code += 16;
            break;
        case ClearBufferfiI:
            glClearBufferfi(**(GLenum**)code, **(GLint**)(code + 4), **(GLfloat**)(code + 8), **(GLint**)(code + 12));
            code += 16;
            break;
        case ClientWaitSync:
            *(GLenum*)(code + 12) = glClientWaitSync(*(GLsync*)code, *(GLbitfield*)(code + 4), *(GLuint64*)(code + 8));
            code += 16;
            break;
        case ClientWaitSyncI:
            *(GLenum*)(code + 12) = glClientWaitSync(**(GLsync**)code, **(GLbitfield**)(code + 4), **(GLuint64**)(code + 8));
            code += 16;
            break;
        case CompressedTexImage3D:
            glCompressedTexImage3D(*(GLenum*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16), *(GLsizei*)(code + 20), *(GLint*)(code + 24), *(GLsizei*)(code + 28), *(const void **)(code + 32));
            code += 36;
            break;
        case CompressedTexImage3DI:
            glCompressedTexImage3D(**(GLenum**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16), **(GLsizei**)(code + 20), **(GLint**)(code + 24), **(GLsizei**)(code + 28), **(const void ***)(code + 32));
            code += 36;
            break;
        case CompressedTexSubImage3D:
            glCompressedTexSubImage3D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLsizei*)(code + 20), *(GLsizei*)(code + 24), *(GLsizei*)(code + 28), *(GLenum*)(code + 32), *(GLsizei*)(code + 36), *(const void **)(code + 40));
            code += 44;
            break;
        case CompressedTexSubImage3DI:
            glCompressedTexSubImage3D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLsizei**)(code + 20), **(GLsizei**)(code + 24), **(GLsizei**)(code + 28), **(GLenum**)(code + 32), **(GLsizei**)(code + 36), **(const void ***)(code + 40));
            code += 44;
            break;
        case CopyBufferSubData:
            glCopyBufferSubData(*(GLenum*)code, *(GLenum*)(code + 4), *(GLintptr*)(code + 8), *(GLintptr*)(code + 12), *(GLsizeiptr*)(code + 16));
            code += 20;
            break;
        case CopyBufferSubDataI:
            glCopyBufferSubData(**(GLenum**)code, **(GLenum**)(code + 4), **(GLintptr**)(code + 8), **(GLintptr**)(code + 12), **(GLsizeiptr**)(code + 16));
            code += 20;
            break;
        case CopyTexSubImage3D:
            glCopyTexSubImage3D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLint*)(code + 20), *(GLint*)(code + 24), *(GLsizei*)(code + 28), *(GLsizei*)(code + 32));
            code += 36;
            break;
        case CopyTexSubImage3DI:
            glCopyTexSubImage3D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLint**)(code + 20), **(GLint**)(code + 24), **(GLsizei**)(code + 28), **(GLsizei**)(code + 32));
            code += 36;
            break;
        case DeleteQueries:
            glDeleteQueries(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteQueriesI:
            glDeleteQueries(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteSamplers:
            glDeleteSamplers(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteSamplersI:
            glDeleteSamplers(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteSync:
            glDeleteSync(*(GLsync*)code);
            code += 4;
            break;
        case DeleteSyncI:
            glDeleteSync(**(GLsync**)code);
            code += 4;
            break;
        case DeleteTransformFeedbacks:
            glDeleteTransformFeedbacks(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteTransformFeedbacksI:
            glDeleteTransformFeedbacks(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteVertexArrays:
            glDeleteVertexArrays(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteVertexArraysI:
            glDeleteVertexArrays(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DrawArraysInstanced:
            glDrawArraysInstanced(*(GLenum*)code, *(GLint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei*)(code + 12));
            code += 16;
            break;
        case DrawArraysInstancedI:
            glDrawArraysInstanced(**(GLenum**)code, **(GLint**)(code + 4), **(GLsizei**)(code + 8), **(GLsizei**)(code + 12));
            code += 16;
            break;
        case DrawBuffers:
            glDrawBuffers(*(GLsizei*)code, *(const GLenum **)(code + 4));
            code += 8;
            break;
        case DrawBuffersI:
            glDrawBuffers(**(GLsizei**)code, **(const GLenum ***)(code + 4));
            code += 8;
            break;
        case DrawElementsInstanced:
            glDrawElementsInstanced(*(GLenum*)code, *(GLsizei*)(code + 4), *(GLenum*)(code + 8), *(const void **)(code + 12), *(GLsizei*)(code + 16));
            code += 20;
            break;
        case DrawElementsInstancedI:
            glDrawElementsInstanced(**(GLenum**)code, **(GLsizei**)(code + 4), **(GLenum**)(code + 8), **(const void ***)(code + 12), **(GLsizei**)(code + 16));
            code += 20;
            break;
        case DrawRangeElements:
            glDrawRangeElements(*(GLenum*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8), *(GLsizei*)(code + 12), *(GLenum*)(code + 16), *(const void **)(code + 20));
            code += 24;
            break;
        case DrawRangeElementsI:
            glDrawRangeElements(**(GLenum**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8), **(GLsizei**)(code + 12), **(GLenum**)(code + 16), **(const void ***)(code + 20));
            code += 24;
            break;
        case EndQuery:
            glEndQuery(*(GLenum*)code);
            code += 4;
            break;
        case EndQueryI:
            glEndQuery(**(GLenum**)code);
            code += 4;
            break;
        case EndTransformFeedback:
            glEndTransformFeedback();
            code += 0;
            break;
        case FenceSync:
            *(GLsync*)(code + 8) = glFenceSync(*(GLenum*)code, *(GLbitfield*)(code + 4));
            code += 12;
            break;
        case FenceSyncI:
            *(GLsync*)(code + 8) = glFenceSync(**(GLenum**)code, **(GLbitfield**)(code + 4));
            code += 12;
            break;
        case FramebufferTextureLayer:
            glFramebufferTextureLayer(*(GLenum*)code, *(GLenum*)(code + 4), *(GLuint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16));
            code += 20;
            break;
        case FramebufferTextureLayerI:
            glFramebufferTextureLayer(**(GLenum**)code, **(GLenum**)(code + 4), **(GLuint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16));
            code += 20;
            break;
        case GenQueries:
            glGenQueries(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenQueriesI:
            glGenQueries(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenSamplers:
            glGenSamplers(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenSamplersI:
            glGenSamplers(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenTransformFeedbacks:
            glGenTransformFeedbacks(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenTransformFeedbacksI:
            glGenTransformFeedbacks(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenVertexArrays:
            glGenVertexArrays(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenVertexArraysI:
            glGenVertexArrays(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GetActiveUniformBlockiv:
            glGetActiveUniformBlockiv(*(GLuint*)code, *(GLuint*)(code + 4), *(GLenum*)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetActiveUniformBlockivI:
            glGetActiveUniformBlockiv(**(GLuint**)code, **(GLuint**)(code + 4), **(GLenum**)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetActiveUniformBlockName:
            glGetActiveUniformBlockName(*(GLuint*)code, *(GLuint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLchar **)(code + 16));
            code += 20;
            break;
        case GetActiveUniformBlockNameI:
            glGetActiveUniformBlockName(**(GLuint**)code, **(GLuint**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLchar **)(code + 16));
            code += 20;
            break;
        case GetActiveUniformsiv:
            glGetActiveUniformsiv(*(GLuint*)code, *(GLsizei*)(code + 4), *(const GLuint **)(code + 8), *(GLenum*)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetActiveUniformsivI:
            glGetActiveUniformsiv(**(GLuint**)code, **(GLsizei**)(code + 4), *(const GLuint **)(code + 8), **(GLenum**)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetBufferParameteri64v:
            glGetBufferParameteri64v(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint64 **)(code + 8));
            code += 12;
            break;
        case GetBufferParameteri64vI:
            glGetBufferParameteri64v(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint64 **)(code + 8));
            code += 12;
            break;
        case GetFragDataLocation:
            *(int*)(code + 8) = glGetFragDataLocation(*(GLuint*)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetFragDataLocationI:
            *(int*)(code + 8) = glGetFragDataLocation(**(GLuint**)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetIntegeri_v:
            glGetIntegeri_v(*(GLenum*)code, *(GLuint*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetIntegeri_vI:
            glGetIntegeri_v(**(GLenum**)code, **(GLuint**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetInteger64v:
            glGetInteger64v(*(GLenum*)code, *(GLint64 **)(code + 4));
            code += 8;
            break;
        case GetInteger64vI:
            glGetInteger64v(**(GLenum**)code, *(GLint64 **)(code + 4));
            code += 8;
            break;
        case GetInteger64i_v:
            glGetInteger64i_v(*(GLenum*)code, *(GLuint*)(code + 4), *(GLint64 **)(code + 8));
            code += 12;
            break;
        case GetInteger64i_vI:
            glGetInteger64i_v(**(GLenum**)code, **(GLuint**)(code + 4), *(GLint64 **)(code + 8));
            code += 12;
            break;
        case GetInternalformativ:
            glGetInternalformativ(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetInternalformativI:
            glGetInternalformativ(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetProgramBinary:
            glGetProgramBinary(*(GLuint*)code, *(GLsizei*)(code + 4), *(GLsizei **)(code + 8), *(GLenum **)(code + 12), *(void **)(code + 16));
            code += 20;
            break;
        case GetProgramBinaryI:
            glGetProgramBinary(**(GLuint**)code, **(GLsizei**)(code + 4), *(GLsizei **)(code + 8), *(GLenum **)(code + 12), *(void **)(code + 16));
            code += 20;
            break;
        case GetQueryiv:
            glGetQueryiv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetQueryivI:
            glGetQueryiv(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetQueryObjectuiv:
            glGetQueryObjectuiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case GetQueryObjectuivI:
            glGetQueryObjectuiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case GetSamplerParameteriv:
            glGetSamplerParameteriv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetSamplerParameterivI:
            glGetSamplerParameteriv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetSamplerParameterfv:
            glGetSamplerParameterfv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetSamplerParameterfvI:
            glGetSamplerParameterfv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetStringi:
            *(const GLubyte**)(code + 8) = glGetStringi(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 12;
            break;
        case GetStringiI:
            *(const GLubyte**)(code + 8) = glGetStringi(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 12;
            break;
        case GetSynciv:
            glGetSynciv(*(GLsync*)code, *(GLenum*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetSyncivI:
            glGetSynciv(**(GLsync**)code, **(GLenum**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16));
            code += 20;
            break;
        case GetTransformFeedbackVarying:
            glGetTransformFeedbackVarying(*(GLuint*)code, *(GLuint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLsizei **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetTransformFeedbackVaryingI:
            glGetTransformFeedbackVarying(**(GLuint**)code, **(GLuint**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLsizei **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetUniformuiv:
            glGetUniformuiv(*(GLuint*)code, *(GLint*)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case GetUniformuivI:
            glGetUniformuiv(**(GLuint**)code, **(GLint**)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case GetUniformBlockIndex:
            *(uint32_t*)(code + 8) = glGetUniformBlockIndex(*(GLuint*)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetUniformBlockIndexI:
            *(uint32_t*)(code + 8) = glGetUniformBlockIndex(**(GLuint**)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetUniformIndices:
            glGetUniformIndices(*(GLuint*)code, *(GLsizei*)(code + 4), *(const GLchar *const**)(code + 8), *(GLuint **)(code + 12));
            code += 16;
            break;
        case GetUniformIndicesI:
            glGetUniformIndices(**(GLuint**)code, **(GLsizei**)(code + 4), *(const GLchar *const**)(code + 8), *(GLuint **)(code + 12));
            code += 16;
            break;
        case GetVertexAttribIiv:
            glGetVertexAttribIiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribIivI:
            glGetVertexAttribIiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribIuiv:
            glGetVertexAttribIuiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribIuivI:
            glGetVertexAttribIuiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLuint **)(code + 8));
            code += 12;
            break;
        case InvalidateFramebuffer:
            glInvalidateFramebuffer(*(GLenum*)code, *(GLsizei*)(code + 4), *(const GLenum **)(code + 8));
            code += 12;
            break;
        case InvalidateFramebufferI:
            glInvalidateFramebuffer(**(GLenum**)code, **(GLsizei**)(code + 4), *(const GLenum **)(code + 8));
            code += 12;
            break;
        case InvalidateSubFramebuffer:
            glInvalidateSubFramebuffer(*(GLenum*)code, *(GLsizei*)(code + 4), *(const GLenum **)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLsizei*)(code + 20), *(GLsizei*)(code + 24));
            code += 28;
            break;
        case InvalidateSubFramebufferI:
            glInvalidateSubFramebuffer(**(GLenum**)code, **(GLsizei**)(code + 4), *(const GLenum **)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLsizei**)(code + 20), **(GLsizei**)(code + 24));
            code += 28;
            break;
        case IsQuery:
            *(int*)(code + 4) = glIsQuery(*(GLuint*)code);
            code += 8;
            break;
        case IsQueryI:
            *(int*)(code + 4) = glIsQuery(**(GLuint**)code);
            code += 8;
            break;
        case IsSampler:
            *(int*)(code + 4) = glIsSampler(*(GLuint*)code);
            code += 8;
            break;
        case IsSamplerI:
            *(int*)(code + 4) = glIsSampler(**(GLuint**)code);
            code += 8;
            break;
        case IsSync:
            *(int*)(code + 4) = glIsSync(*(GLsync*)code);
            code += 8;
            break;
        case IsSyncI:
            *(int*)(code + 4) = glIsSync(**(GLsync**)code);
            code += 8;
            break;
        case IsTransformFeedback:
            *(int*)(code + 4) = glIsTransformFeedback(*(GLuint*)code);
            code += 8;
            break;
        case IsTransformFeedbackI:
            *(int*)(code + 4) = glIsTransformFeedback(**(GLuint**)code);
            code += 8;
            break;
        case IsVertexArray:
            *(int*)(code + 4) = glIsVertexArray(*(GLuint*)code);
            code += 8;
            break;
        case IsVertexArrayI:
            *(int*)(code + 4) = glIsVertexArray(**(GLuint**)code);
            code += 8;
            break;
        case PauseTransformFeedback:
            glPauseTransformFeedback();
            code += 0;
            break;
        case ProgramBinary:
            glProgramBinary(*(GLuint*)code, *(GLenum*)(code + 4), *(const void **)(code + 8), *(GLint*)(code + 12));
            code += 16;
            break;
        case ProgramBinaryI:
            glProgramBinary(**(GLuint**)code, **(GLenum**)(code + 4), *(const void **)(code + 8), **(GLint**)(code + 12));
            code += 16;
            break;
        case ProgramParameteri:
            glProgramParameteri(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint*)(code + 8));
            code += 12;
            break;
        case ProgramParameteriI:
            glProgramParameteri(**(GLuint**)code, **(GLenum**)(code + 4), **(GLint**)(code + 8));
            code += 12;
            break;
        case ReadBuffer:
            glReadBuffer(*(GLenum*)code);
            code += 4;
            break;
        case ReadBufferI:
            glReadBuffer(**(GLenum**)code);
            code += 4;
            break;
        case RenderbufferStorageMultisample:
            glRenderbufferStorageMultisample(*(GLenum*)code, *(GLsizei*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16));
            code += 20;
            break;
        case RenderbufferStorageMultisampleI:
            glRenderbufferStorageMultisample(**(GLenum**)code, **(GLsizei**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16));
            code += 20;
            break;
        case ResumeTransformFeedback:
            glResumeTransformFeedback();
            code += 0;
            break;
        case SamplerParameteri:
            glSamplerParameteri(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint*)(code + 8));
            code += 12;
            break;
        case SamplerParameteriI:
            glSamplerParameteri(**(GLuint**)code, **(GLenum**)(code + 4), **(GLint**)(code + 8));
            code += 12;
            break;
        case SamplerParameteriv:
            glSamplerParameteriv(*(GLuint*)code, *(GLenum*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case SamplerParameterivI:
            glSamplerParameteriv(**(GLuint**)code, **(GLenum**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case SamplerParameterf:
            glSamplerParameterf(*(GLuint*)code, *(GLenum*)(code + 4), *(GLfloat*)(code + 8));
            code += 12;
            break;
        case SamplerParameterfI:
            glSamplerParameterf(**(GLuint**)code, **(GLenum**)(code + 4), **(GLfloat**)(code + 8));
            code += 12;
            break;
        case SamplerParameterfv:
            glSamplerParameterfv(*(GLuint*)code, *(GLenum*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case SamplerParameterfvI:
            glSamplerParameterfv(**(GLuint**)code, **(GLenum**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case TexImage3D:
            glTexImage3D(*(GLenum*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16), *(GLsizei*)(code + 20), *(GLint*)(code + 24), *(GLenum*)(code + 28), *(GLenum*)(code + 32), *(const void **)(code + 36));
            code += 40;
            break;
        case TexImage3DI:
            glTexImage3D(**(GLenum**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16), **(GLsizei**)(code + 20), **(GLint**)(code + 24), **(GLenum**)(code + 28), **(GLenum**)(code + 32), **(const void ***)(code + 36));
            code += 40;
            break;
        case TexStorage2D:
            glTexStorage2D(*(GLenum*)code, *(GLsizei*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16));
            code += 20;
            break;
        case TexStorage2DI:
            glTexStorage2D(**(GLenum**)code, **(GLsizei**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16));
            code += 20;
            break;
        case TexStorage3D:
            glTexStorage3D(*(GLenum*)code, *(GLsizei*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16), *(GLsizei*)(code + 20));
            code += 24;
            break;
        case TexStorage3DI:
            glTexStorage3D(**(GLenum**)code, **(GLsizei**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16), **(GLsizei**)(code + 20));
            code += 24;
            break;
        case TexSubImage3D:
            glTexSubImage3D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLsizei*)(code + 20), *(GLsizei*)(code + 24), *(GLsizei*)(code + 28), *(GLenum*)(code + 32), *(GLenum*)(code + 36), *(const void **)(code + 40));
            code += 44;
            break;
        case TexSubImage3DI:
            glTexSubImage3D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLsizei**)(code + 20), **(GLsizei**)(code + 24), **(GLsizei**)(code + 28), **(GLenum**)(code + 32), **(GLenum**)(code + 36), **(const void ***)(code + 40));
            code += 44;
            break;
        case TransformFeedbackVaryings:
            glTransformFeedbackVaryings(*(GLuint*)code, *(GLsizei*)(code + 4), *(const GLchar *const**)(code + 8), *(GLenum*)(code + 12));
            code += 16;
            break;
        case TransformFeedbackVaryingsI:
            glTransformFeedbackVaryings(**(GLuint**)code, **(GLsizei**)(code + 4), *(const GLchar *const**)(code + 8), **(GLenum**)(code + 12));
            code += 16;
            break;
        case Uniform1ui:
            glUniform1ui(*(GLint*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case Uniform1uiI:
            glUniform1ui(**(GLint**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case Uniform1uiv:
            glUniform1uiv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform1uivI:
            glUniform1uiv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform2ui:
            glUniform2ui(*(GLint*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8));
            code += 12;
            break;
        case Uniform2uiI:
            glUniform2ui(**(GLint**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8));
            code += 12;
            break;
        case Uniform2uiv:
            glUniform2uiv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform2uivI:
            glUniform2uiv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform3ui:
            glUniform3ui(*(GLint*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8), *(GLuint*)(code + 12));
            code += 16;
            break;
        case Uniform3uiI:
            glUniform3ui(**(GLint**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8), **(GLuint**)(code + 12));
            code += 16;
            break;
        case Uniform3uiv:
            glUniform3uiv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform3uivI:
            glUniform3uiv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform4ui:
            glUniform4ui(*(GLint*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8), *(GLuint*)(code + 12), *(GLuint*)(code + 16));
            code += 20;
            break;
        case Uniform4uiI:
            glUniform4ui(**(GLint**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8), **(GLuint**)(code + 12), **(GLuint**)(code + 16));
            code += 20;
            break;
        case Uniform4uiv:
            glUniform4uiv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case Uniform4uivI:
            glUniform4uiv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLuint **)(code + 8));
            code += 12;
            break;
        case UniformBlockBinding:
            glUniformBlockBinding(*(GLuint*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8));
            code += 12;
            break;
        case UniformBlockBindingI:
            glUniformBlockBinding(**(GLuint**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8));
            code += 12;
            break;
        case UniformMatrix2x3fv:
            glUniformMatrix2x3fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix2x3fvI:
            glUniformMatrix2x3fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix2x4fv:
            glUniformMatrix2x4fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix2x4fvI:
            glUniformMatrix2x4fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3x2fv:
            glUniformMatrix3x2fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3x2fvI:
            glUniformMatrix3x2fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3x4fv:
            glUniformMatrix3x4fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3x4fvI:
            glUniformMatrix3x4fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4x2fv:
            glUniformMatrix4x2fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4x2fvI:
            glUniformMatrix4x2fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4x3fv:
            glUniformMatrix4x3fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4x3fvI:
            glUniformMatrix4x3fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case VertexAttribDivisor:
            glVertexAttribDivisor(*(GLuint*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case VertexAttribDivisorI:
            glVertexAttribDivisor(**(GLuint**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case VertexAttribI4i:
            glVertexAttribI4i(*(GLuint*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16));
            code += 20;
            break;
        case VertexAttribI4iI:
            glVertexAttribI4i(**(GLuint**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16));
            code += 20;
            break;
        case VertexAttribI4ui:
            glVertexAttribI4ui(*(GLuint*)code, *(GLuint*)(code + 4), *(GLuint*)(code + 8), *(GLuint*)(code + 12), *(GLuint*)(code + 16));
            code += 20;
            break;
        case VertexAttribI4uiI:
            glVertexAttribI4ui(**(GLuint**)code, **(GLuint**)(code + 4), **(GLuint**)(code + 8), **(GLuint**)(code + 12), **(GLuint**)(code + 16));
            code += 20;
            break;
        case VertexAttribI4iv:
            glVertexAttribI4iv(*(GLuint*)code, *(const GLint **)(code + 4));
            code += 8;
            break;
        case VertexAttribI4ivI:
            glVertexAttribI4iv(**(GLuint**)code, *(const GLint **)(code + 4));
            code += 8;
            break;
        case VertexAttribI4uiv:
            glVertexAttribI4uiv(*(GLuint*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case VertexAttribI4uivI:
            glVertexAttribI4uiv(**(GLuint**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case VertexAttribIPointer:
            glVertexAttribIPointer(*(GLuint*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(const void **)(code + 16));
            code += 20;
            break;
        case VertexAttribIPointerI:
            glVertexAttribIPointer(**(GLuint**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), *(const void **)(code + 16));
            code += 20;
            break;
        case WaitSync:
            glWaitSync(*(GLsync*)code, *(GLbitfield*)(code + 4), *(GLuint64*)(code + 8));
            code += 12;
            break;
        case WaitSyncI:
            glWaitSync(**(GLsync**)code, **(GLbitfield**)(code + 4), **(GLuint64**)(code + 8));
            code += 12;
            break;
        case ActiveTexture:
            glActiveTexture(*(GLenum*)code);
            code += 4;
            break;
        case ActiveTextureI:
            glActiveTexture(**(GLenum**)code);
            code += 4;
            break;
        case AttachShader:
            glAttachShader(*(GLuint*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case AttachShaderI:
            glAttachShader(**(GLuint**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindAttribLocation:
            glBindAttribLocation(*(GLuint*)code, *(GLuint*)(code + 4), *(const GLchar **)(code + 8));
            code += 12;
            break;
        case BindAttribLocationI:
            glBindAttribLocation(**(GLuint**)code, **(GLuint**)(code + 4), *(const GLchar **)(code + 8));
            code += 12;
            break;
        case BindBuffer:
            glBindBuffer(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindBufferI:
            glBindBuffer(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindFramebuffer:
            glBindFramebuffer(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindFramebufferI:
            glBindFramebuffer(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindRenderbuffer:
            glBindRenderbuffer(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindRenderbufferI:
            glBindRenderbuffer(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BindTexture:
            glBindTexture(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case BindTextureI:
            glBindTexture(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case BlendColor:
            glBlendColor(*(GLfloat*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12));
            code += 16;
            break;
        case BlendColorI:
            glBlendColor(**(GLfloat**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12));
            code += 16;
            break;
        case BlendEquation:
            glBlendEquation(*(GLenum*)code);
            code += 4;
            break;
        case BlendEquationI:
            glBlendEquation(**(GLenum**)code);
            code += 4;
            break;
        case BlendEquationSeparate:
            glBlendEquationSeparate(*(GLenum*)code, *(GLenum*)(code + 4));
            code += 8;
            break;
        case BlendEquationSeparateI:
            glBlendEquationSeparate(**(GLenum**)code, **(GLenum**)(code + 4));
            code += 8;
            break;
        case BlendFunc:
            glBlendFunc(*(GLenum*)code, *(GLenum*)(code + 4));
            code += 8;
            break;
        case BlendFuncI:
            glBlendFunc(**(GLenum**)code, **(GLenum**)(code + 4));
            code += 8;
            break;
        case BlendFuncSeparate:
            glBlendFuncSeparate(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLenum*)(code + 12));
            code += 16;
            break;
        case BlendFuncSeparateI:
            glBlendFuncSeparate(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), **(GLenum**)(code + 12));
            code += 16;
            break;
        case BufferData:
            glBufferData(*(GLenum*)code, *(GLsizeiptr*)(code + 4), *(const void **)(code + 8), *(GLenum*)(code + 12));
            code += 16;
            break;
        case BufferDataI:
            glBufferData(**(GLenum**)code, **(GLsizeiptr**)(code + 4), **(const void ***)(code + 8), **(GLenum**)(code + 12));
            code += 16;
            break;
        case BufferSubData:
            glBufferSubData(*(GLenum*)code, *(GLintptr*)(code + 4), *(GLsizeiptr*)(code + 8), *(const void **)(code + 12));
            code += 16;
            break;
        case BufferSubDataI:
            glBufferSubData(**(GLenum**)code, **(GLintptr**)(code + 4), **(GLsizeiptr**)(code + 8), **(const void ***)(code + 12));
            code += 16;
            break;
        case CheckFramebufferStatus:
            *(GLenum*)(code + 4) = glCheckFramebufferStatus(*(GLenum*)code);
            code += 8;
            break;
        case CheckFramebufferStatusI:
            *(GLenum*)(code + 4) = glCheckFramebufferStatus(**(GLenum**)code);
            code += 8;
            break;
        case Clear:
            glClear(*(GLbitfield*)code);
            code += 4;
            break;
        case ClearI:
            glClear(**(GLbitfield**)code);
            code += 4;
            break;
        case ClearColor:
            glClearColor(*(GLfloat*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12));
            code += 16;
            break;
        case ClearColorI:
            glClearColor(**(GLfloat**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12));
            code += 16;
            break;
        case ClearDepthf:
            glClearDepthf(*(GLclampf*)code);
            code += 4;
            break;
        case ClearDepthfI:
            glClearDepthf(**(GLclampf**)code);
            code += 4;
            break;
        case ClearStencil:
            glClearStencil(*(GLint*)code);
            code += 4;
            break;
        case ClearStencilI:
            glClearStencil(**(GLint**)code);
            code += 4;
            break;
        case ColorMask:
            glColorMask(*(GLboolean*)code, *(GLboolean*)(code + 4), *(GLboolean*)(code + 8), *(GLboolean*)(code + 12));
            code += 16;
            break;
        case ColorMaskI:
            glColorMask(**(GLboolean**)code, **(GLboolean**)(code + 4), **(GLboolean**)(code + 8), **(GLboolean**)(code + 12));
            code += 16;
            break;
        case CompileShader:
            glCompileShader(*(GLuint*)code);
            code += 4;
            break;
        case CompileShaderI:
            glCompileShader(**(GLuint**)code);
            code += 4;
            break;
        case CompressedTexImage2D:
            glCompressedTexImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16), *(GLint*)(code + 20), *(GLsizei*)(code + 24), *(const void **)(code + 28));
            code += 32;
            break;
        case CompressedTexImage2DI:
            glCompressedTexImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16), **(GLint**)(code + 20), **(GLsizei**)(code + 24), **(const void ***)(code + 28));
            code += 32;
            break;
        case CompressedTexSubImage2D:
            glCompressedTexSubImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLsizei*)(code + 16), *(GLsizei*)(code + 20), *(GLenum*)(code + 24), *(GLsizei*)(code + 28), *(const void **)(code + 32));
            code += 36;
            break;
        case CompressedTexSubImage2DI:
            glCompressedTexSubImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLsizei**)(code + 16), **(GLsizei**)(code + 20), **(GLenum**)(code + 24), **(GLsizei**)(code + 28), **(const void ***)(code + 32));
            code += 36;
            break;
        case CopyTexImage2D:
            glCopyTexImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLsizei*)(code + 20), *(GLsizei*)(code + 24), *(GLint*)(code + 28));
            code += 32;
            break;
        case CopyTexImage2DI:
            glCopyTexImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLsizei**)(code + 20), **(GLsizei**)(code + 24), **(GLint**)(code + 28));
            code += 32;
            break;
        case CopyTexSubImage2D:
            glCopyTexSubImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16), *(GLint*)(code + 20), *(GLsizei*)(code + 24), *(GLsizei*)(code + 28));
            code += 32;
            break;
        case CopyTexSubImage2DI:
            glCopyTexSubImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16), **(GLint**)(code + 20), **(GLsizei**)(code + 24), **(GLsizei**)(code + 28));
            code += 32;
            break;
        case CreateProgram:
            *(uint32_t*)(code + 0) = glCreateProgram();
            code += 4;
            break;
        case CreateProgramI:
            *(uint32_t*)(code + 0) = glCreateProgram();
            code += 4;
            break;
        case CreateShader:
            *(uint32_t*)(code + 4) = glCreateShader(*(GLenum*)code);
            code += 8;
            break;
        case CreateShaderI:
            *(uint32_t*)(code + 4) = glCreateShader(**(GLenum**)code);
            code += 8;
            break;
        case CullFace:
            glCullFace(*(GLenum*)code);
            code += 4;
            break;
        case CullFaceI:
            glCullFace(**(GLenum**)code);
            code += 4;
            break;
        case DeleteBuffers:
            glDeleteBuffers(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteBuffersI:
            glDeleteBuffers(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteFramebuffers:
            glDeleteFramebuffers(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteFramebuffersI:
            glDeleteFramebuffers(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteProgram:
            glDeleteProgram(*(GLuint*)code);
            code += 4;
            break;
        case DeleteProgramI:
            glDeleteProgram(**(GLuint**)code);
            code += 4;
            break;
        case DeleteRenderbuffers:
            glDeleteRenderbuffers(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteRenderbuffersI:
            glDeleteRenderbuffers(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteShader:
            glDeleteShader(*(GLuint*)code);
            code += 4;
            break;
        case DeleteShaderI:
            glDeleteShader(**(GLuint**)code);
            code += 4;
            break;
        case DeleteTextures:
            glDeleteTextures(*(GLsizei*)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DeleteTexturesI:
            glDeleteTextures(**(GLsizei**)code, *(const GLuint **)(code + 4));
            code += 8;
            break;
        case DepthFunc:
            glDepthFunc(*(GLenum*)code);
            code += 4;
            break;
        case DepthFuncI:
            glDepthFunc(**(GLenum**)code);
            code += 4;
            break;
        case DepthMask:
            glDepthMask(*(GLboolean*)code);
            code += 4;
            break;
        case DepthMaskI:
            glDepthMask(**(GLboolean**)code);
            code += 4;
            break;
        case DepthRangef:
            glDepthRangef(*(GLclampf*)code, *(GLclampf*)(code + 4));
            code += 8;
            break;
        case DepthRangefI:
            glDepthRangef(**(GLclampf**)code, **(GLclampf**)(code + 4));
            code += 8;
            break;
        case DetachShader:
            glDetachShader(*(GLuint*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case DetachShaderI:
            glDetachShader(**(GLuint**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case Disable:
            glDisable(*(GLenum*)code);
            code += 4;
            break;
        case DisableI:
            glDisable(**(GLenum**)code);
            code += 4;
            break;
        case DisableVertexAttribArray:
            glDisableVertexAttribArray(*(GLuint*)code);
            code += 4;
            break;
        case DisableVertexAttribArrayI:
            glDisableVertexAttribArray(**(GLuint**)code);
            code += 4;
            break;
        case DrawArrays:
            glDrawArrays(*(GLenum*)code, *(GLint*)(code + 4), *(GLsizei*)(code + 8));
            code += 12;
            break;
        case DrawArraysI:
            glDrawArrays(**(GLenum**)code, **(GLint**)(code + 4), **(GLsizei**)(code + 8));
            code += 12;
            break;
        case DrawElements:
            glDrawElements(*(GLenum*)code, *(GLsizei*)(code + 4), *(GLenum*)(code + 8), *(const void **)(code + 12));
            code += 16;
            break;
        case DrawElementsI:
            glDrawElements(**(GLenum**)code, **(GLsizei**)(code + 4), **(GLenum**)(code + 8), **(const void ***)(code + 12));
            code += 16;
            break;
        case Enable:
            glEnable(*(GLenum*)code);
            code += 4;
            break;
        case EnableI:
            glEnable(**(GLenum**)code);
            code += 4;
            break;
        case EnableVertexAttribArray:
            glEnableVertexAttribArray(*(GLuint*)code);
            code += 4;
            break;
        case EnableVertexAttribArrayI:
            glEnableVertexAttribArray(**(GLuint**)code);
            code += 4;
            break;
        case Finish:
            glFinish();
            code += 0;
            break;
        case Flush:
            glFlush();
            code += 0;
            break;
        case FramebufferRenderbuffer:
            glFramebufferRenderbuffer(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLuint*)(code + 12));
            code += 16;
            break;
        case FramebufferRenderbufferI:
            glFramebufferRenderbuffer(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), **(GLuint**)(code + 12));
            code += 16;
            break;
        case FramebufferTexture2D:
            glFramebufferTexture2D(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLuint*)(code + 12), *(GLint*)(code + 16));
            code += 20;
            break;
        case FramebufferTexture2DI:
            glFramebufferTexture2D(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), **(GLuint**)(code + 12), **(GLint**)(code + 16));
            code += 20;
            break;
        case FrontFace:
            glFrontFace(*(GLenum*)code);
            code += 4;
            break;
        case FrontFaceI:
            glFrontFace(**(GLenum**)code);
            code += 4;
            break;
        case GenBuffers:
            glGenBuffers(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenBuffersI:
            glGenBuffers(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenerateMipmap:
            glGenerateMipmap(*(GLenum*)code);
            code += 4;
            break;
        case GenerateMipmapI:
            glGenerateMipmap(**(GLenum**)code);
            code += 4;
            break;
        case GenFramebuffers:
            glGenFramebuffers(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenFramebuffersI:
            glGenFramebuffers(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenRenderbuffers:
            glGenRenderbuffers(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenRenderbuffersI:
            glGenRenderbuffers(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenTextures:
            glGenTextures(*(GLsizei*)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GenTexturesI:
            glGenTextures(**(GLsizei**)code, *(GLuint **)(code + 4));
            code += 8;
            break;
        case GetActiveAttrib:
            glGetActiveAttrib(*(GLuint*)code, *(GLuint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetActiveAttribI:
            glGetActiveAttrib(**(GLuint**)code, **(GLuint**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetActiveUniform:
            glGetActiveUniform(*(GLuint*)code, *(GLuint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetActiveUniformI:
            glGetActiveUniform(**(GLuint**)code, **(GLuint**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetAttachedShaders:
            glGetAttachedShaders(*(GLuint*)code, *(GLsizei*)(code + 4), *(GLsizei **)(code + 8), *(GLuint **)(code + 12));
            code += 16;
            break;
        case GetAttachedShadersI:
            glGetAttachedShaders(**(GLuint**)code, **(GLsizei**)(code + 4), *(GLsizei **)(code + 8), *(GLuint **)(code + 12));
            code += 16;
            break;
        case GetAttribLocation:
            *(int*)(code + 8) = glGetAttribLocation(*(GLuint*)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetAttribLocationI:
            *(int*)(code + 8) = glGetAttribLocation(**(GLuint**)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetBooleanv:
            glGetBooleanv(*(GLenum*)code, *(GLboolean **)(code + 4));
            code += 8;
            break;
        case GetBooleanvI:
            glGetBooleanv(**(GLenum**)code, *(GLboolean **)(code + 4));
            code += 8;
            break;
        case GetBufferParameteriv:
            glGetBufferParameteriv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetBufferParameterivI:
            glGetBufferParameteriv(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetError:
            *(GLenum*)(code + 0) = glGetError();
            code += 4;
            break;
        case GetErrorI:
            *(GLenum*)(code + 0) = glGetError();
            code += 4;
            break;
        case GetFloatv:
            glGetFloatv(*(GLenum*)code, *(GLfloat **)(code + 4));
            code += 8;
            break;
        case GetFloatvI:
            glGetFloatv(**(GLenum**)code, *(GLfloat **)(code + 4));
            code += 8;
            break;
        case GetFramebufferAttachmentParameteriv:
            glGetFramebufferAttachmentParameteriv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetFramebufferAttachmentParameterivI:
            glGetFramebufferAttachmentParameteriv(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetIntegerv:
            glGetIntegerv(*(GLenum*)code, *(GLint **)(code + 4));
            code += 8;
            break;
        case GetIntegervI:
            glGetIntegerv(**(GLenum**)code, *(GLint **)(code + 4));
            code += 8;
            break;
        case GetProgramiv:
            glGetProgramiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetProgramivI:
            glGetProgramiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetProgramInfoLog:
            glGetProgramInfoLog(*(GLuint*)code, *(GLsizei*)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetProgramInfoLogI:
            glGetProgramInfoLog(**(GLuint**)code, **(GLsizei**)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetRenderbufferParameteriv:
            glGetRenderbufferParameteriv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetRenderbufferParameterivI:
            glGetRenderbufferParameteriv(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetShaderiv:
            glGetShaderiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetShaderivI:
            glGetShaderiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetShaderInfoLog:
            glGetShaderInfoLog(*(GLuint*)code, *(GLsizei*)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetShaderInfoLogI:
            glGetShaderInfoLog(**(GLuint**)code, **(GLsizei**)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetShaderPrecisionFormat:
            glGetShaderPrecisionFormat(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetShaderPrecisionFormatI:
            glGetShaderPrecisionFormat(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8), *(GLint **)(code + 12));
            code += 16;
            break;
        case GetShaderSource:
            glGetShaderSource(*(GLuint*)code, *(GLsizei*)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetShaderSourceI:
            glGetShaderSource(**(GLuint**)code, **(GLsizei**)(code + 4), *(GLsizei **)(code + 8), *(GLchar **)(code + 12));
            code += 16;
            break;
        case GetString:
            *(const GLubyte**)(code + 4) = glGetString(*(GLenum*)code);
            code += 8;
            break;
        case GetStringI:
            *(const GLubyte**)(code + 4) = glGetString(**(GLenum**)code);
            code += 8;
            break;
        case GetTexParameterfv:
            glGetTexParameterfv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetTexParameterfvI:
            glGetTexParameterfv(**(GLenum**)code, **(GLenum**)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetTexParameteriv:
            glGetTexParameteriv(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetTexParameterivI:
            glGetTexParameteriv(**(GLenum**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetUniformfv:
            glGetUniformfv(*(GLuint*)code, *(GLint*)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetUniformfvI:
            glGetUniformfv(**(GLuint**)code, **(GLint**)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetUniformiv:
            glGetUniformiv(*(GLuint*)code, *(GLint*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetUniformivI:
            glGetUniformiv(**(GLuint**)code, **(GLint**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetUniformLocation:
            *(int*)(code + 8) = glGetUniformLocation(*(GLuint*)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetUniformLocationI:
            *(int*)(code + 8) = glGetUniformLocation(**(GLuint**)code, *(const GLchar **)(code + 4));
            code += 12;
            break;
        case GetVertexAttribfv:
            glGetVertexAttribfv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribfvI:
            glGetVertexAttribfv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLfloat **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribiv:
            glGetVertexAttribiv(*(GLuint*)code, *(GLenum*)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribivI:
            glGetVertexAttribiv(**(GLuint**)code, **(GLenum**)(code + 4), *(GLint **)(code + 8));
            code += 12;
            break;
        case GetVertexAttribPointerv:
            glGetVertexAttribPointerv(*(GLuint*)code, *(GLenum*)(code + 4), *(void ***)(code + 8));
            code += 12;
            break;
        case GetVertexAttribPointervI:
            glGetVertexAttribPointerv(**(GLuint**)code, **(GLenum**)(code + 4), *(void ***)(code + 8));
            code += 12;
            break;
        case Hint:
            glHint(*(GLenum*)code, *(GLenum*)(code + 4));
            code += 8;
            break;
        case HintI:
            glHint(**(GLenum**)code, **(GLenum**)(code + 4));
            code += 8;
            break;
        case IsBuffer:
            *(int*)(code + 4) = glIsBuffer(*(GLuint*)code);
            code += 8;
            break;
        case IsBufferI:
            *(int*)(code + 4) = glIsBuffer(**(GLuint**)code);
            code += 8;
            break;
        case IsEnabled:
            *(int*)(code + 4) = glIsEnabled(*(GLenum*)code);
            code += 8;
            break;
        case IsEnabledI:
            *(int*)(code + 4) = glIsEnabled(**(GLenum**)code);
            code += 8;
            break;
        case IsFramebuffer:
            *(int*)(code + 4) = glIsFramebuffer(*(GLuint*)code);
            code += 8;
            break;
        case IsFramebufferI:
            *(int*)(code + 4) = glIsFramebuffer(**(GLuint**)code);
            code += 8;
            break;
        case IsProgram:
            *(int*)(code + 4) = glIsProgram(*(GLuint*)code);
            code += 8;
            break;
        case IsProgramI:
            *(int*)(code + 4) = glIsProgram(**(GLuint**)code);
            code += 8;
            break;
        case IsRenderbuffer:
            *(int*)(code + 4) = glIsRenderbuffer(*(GLuint*)code);
            code += 8;
            break;
        case IsRenderbufferI:
            *(int*)(code + 4) = glIsRenderbuffer(**(GLuint**)code);
            code += 8;
            break;
        case IsShader:
            *(int*)(code + 4) = glIsShader(*(GLuint*)code);
            code += 8;
            break;
        case IsShaderI:
            *(int*)(code + 4) = glIsShader(**(GLuint**)code);
            code += 8;
            break;
        case IsTexture:
            *(int*)(code + 4) = glIsTexture(*(GLuint*)code);
            code += 8;
            break;
        case IsTextureI:
            *(int*)(code + 4) = glIsTexture(**(GLuint**)code);
            code += 8;
            break;
        case LineWidth:
            glLineWidth(*(GLfloat*)code);
            code += 4;
            break;
        case LineWidthI:
            glLineWidth(**(GLfloat**)code);
            code += 4;
            break;
        case LinkProgram:
            glLinkProgram(*(GLuint*)code);
            code += 4;
            break;
        case LinkProgramI:
            glLinkProgram(**(GLuint**)code);
            code += 4;
            break;
        case PixelStorei:
            glPixelStorei(*(GLenum*)code, *(GLint*)(code + 4));
            code += 8;
            break;
        case PixelStoreiI:
            glPixelStorei(**(GLenum**)code, **(GLint**)(code + 4));
            code += 8;
            break;
        case PolygonOffset:
            glPolygonOffset(*(GLfloat*)code, *(GLfloat*)(code + 4));
            code += 8;
            break;
        case PolygonOffsetI:
            glPolygonOffset(**(GLfloat**)code, **(GLfloat**)(code + 4));
            code += 8;
            break;
        case ReadPixels:
            glReadPixels(*(GLint*)code, *(GLint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei*)(code + 12), *(GLenum*)(code + 16), *(GLenum*)(code + 20), *(void **)(code + 24));
            code += 28;
            break;
        case ReadPixelsI:
            glReadPixels(**(GLint**)code, **(GLint**)(code + 4), **(GLsizei**)(code + 8), **(GLsizei**)(code + 12), **(GLenum**)(code + 16), **(GLenum**)(code + 20), *(void **)(code + 24));
            code += 28;
            break;
        case ReleaseShaderCompiler:
            glReleaseShaderCompiler();
            code += 0;
            break;
        case RenderbufferStorage:
            glRenderbufferStorage(*(GLenum*)code, *(GLenum*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei*)(code + 12));
            code += 16;
            break;
        case RenderbufferStorageI:
            glRenderbufferStorage(**(GLenum**)code, **(GLenum**)(code + 4), **(GLsizei**)(code + 8), **(GLsizei**)(code + 12));
            code += 16;
            break;
        case SampleCoverage:
            glSampleCoverage(*(GLfloat*)code, *(GLboolean*)(code + 4));
            code += 8;
            break;
        case SampleCoverageI:
            glSampleCoverage(**(GLfloat**)code, **(GLboolean**)(code + 4));
            code += 8;
            break;
        case Scissor:
            glScissor(*(GLint*)code, *(GLint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei*)(code + 12));
            code += 16;
            break;
        case ScissorI:
            glScissor(**(GLint**)code, **(GLint**)(code + 4), **(GLsizei**)(code + 8), **(GLsizei**)(code + 12));
            code += 16;
            break;
        case ShaderBinary:
            glShaderBinary(*(GLsizei*)code, *(const GLuint **)(code + 4), *(GLenum*)(code + 8), *(const void **)(code + 12), *(GLsizei*)(code + 16));
            code += 20;
            break;
        case ShaderBinaryI:
            glShaderBinary(**(GLsizei**)code, *(const GLuint **)(code + 4), **(GLenum**)(code + 8), *(const void **)(code + 12), **(GLsizei**)(code + 16));
            code += 20;
            break;
        case ShaderSource:
            glShaderSource(*(GLuint*)code, *(GLsizei*)(code + 4), *(const GLchar ***)(code + 8), *(const GLint **)(code + 12));
            code += 16;
            break;
        case ShaderSourceI:
            glShaderSource(**(GLuint**)code, **(GLsizei**)(code + 4), *(const GLchar ***)(code + 8), *(const GLint **)(code + 12));
            code += 16;
            break;
        case StencilFunc:
            glStencilFunc(*(GLenum*)code, *(GLint*)(code + 4), *(GLuint*)(code + 8));
            code += 12;
            break;
        case StencilFuncI:
            glStencilFunc(**(GLenum**)code, **(GLint**)(code + 4), **(GLuint**)(code + 8));
            code += 12;
            break;
        case StencilFuncSeparate:
            glStencilFuncSeparate(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint*)(code + 8), *(GLuint*)(code + 12));
            code += 16;
            break;
        case StencilFuncSeparateI:
            glStencilFuncSeparate(**(GLenum**)code, **(GLenum**)(code + 4), **(GLint**)(code + 8), **(GLuint**)(code + 12));
            code += 16;
            break;
        case StencilMask:
            glStencilMask(*(GLuint*)code);
            code += 4;
            break;
        case StencilMaskI:
            glStencilMask(**(GLuint**)code);
            code += 4;
            break;
        case StencilMaskSeparate:
            glStencilMaskSeparate(*(GLenum*)code, *(GLuint*)(code + 4));
            code += 8;
            break;
        case StencilMaskSeparateI:
            glStencilMaskSeparate(**(GLenum**)code, **(GLuint**)(code + 4));
            code += 8;
            break;
        case StencilOp:
            glStencilOp(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8));
            code += 12;
            break;
        case StencilOpI:
            glStencilOp(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8));
            code += 12;
            break;
        case StencilOpSeparate:
            glStencilOpSeparate(*(GLenum*)code, *(GLenum*)(code + 4), *(GLenum*)(code + 8), *(GLenum*)(code + 12));
            code += 16;
            break;
        case StencilOpSeparateI:
            glStencilOpSeparate(**(GLenum**)code, **(GLenum**)(code + 4), **(GLenum**)(code + 8), **(GLenum**)(code + 12));
            code += 16;
            break;
        case TexImage2D:
            glTexImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLsizei*)(code + 12), *(GLsizei*)(code + 16), *(GLint*)(code + 20), *(GLenum*)(code + 24), *(GLenum*)(code + 28), *(const void **)(code + 32));
            code += 36;
            break;
        case TexImage2DI:
            glTexImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLsizei**)(code + 12), **(GLsizei**)(code + 16), **(GLint**)(code + 20), **(GLenum**)(code + 24), **(GLenum**)(code + 28), **(const void ***)(code + 32));
            code += 36;
            break;
        case TexParameterf:
            glTexParameterf(*(GLenum*)code, *(GLenum*)(code + 4), *(GLfloat*)(code + 8));
            code += 12;
            break;
        case TexParameterfI:
            glTexParameterf(**(GLenum**)code, **(GLenum**)(code + 4), **(GLfloat**)(code + 8));
            code += 12;
            break;
        case TexParameterfv:
            glTexParameterfv(*(GLenum*)code, *(GLenum*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case TexParameterfvI:
            glTexParameterfv(**(GLenum**)code, **(GLenum**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case TexParameteri:
            glTexParameteri(*(GLenum*)code, *(GLenum*)(code + 4), *(GLint*)(code + 8));
            code += 12;
            break;
        case TexParameteriI:
            glTexParameteri(**(GLenum**)code, **(GLenum**)(code + 4), **(GLint**)(code + 8));
            code += 12;
            break;
        case TexParameteriv:
            glTexParameteriv(*(GLenum*)code, *(GLenum*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case TexParameterivI:
            glTexParameteriv(**(GLenum**)code, **(GLenum**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case TexSubImage2D:
            glTexSubImage2D(*(GLenum*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLsizei*)(code + 16), *(GLsizei*)(code + 20), *(GLenum*)(code + 24), *(GLenum*)(code + 28), *(const void **)(code + 32));
            code += 36;
            break;
        case TexSubImage2DI:
            glTexSubImage2D(**(GLenum**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLsizei**)(code + 16), **(GLsizei**)(code + 20), **(GLenum**)(code + 24), **(GLenum**)(code + 28), **(const void ***)(code + 32));
            code += 36;
            break;
        case Uniform1f:
            glUniform1f(*(GLint*)code, *(GLfloat*)(code + 4));
            code += 8;
            break;
        case Uniform1fI:
            glUniform1f(**(GLint**)code, **(GLfloat**)(code + 4));
            code += 8;
            break;
        case Uniform1fv:
            glUniform1fv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform1fvI:
            glUniform1fv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform1i:
            glUniform1i(*(GLint*)code, *(GLint*)(code + 4));
            code += 8;
            break;
        case Uniform1iI:
            glUniform1i(**(GLint**)code, **(GLint**)(code + 4));
            code += 8;
            break;
        case Uniform1iv:
            glUniform1iv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform1ivI:
            glUniform1iv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform2f:
            glUniform2f(*(GLint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8));
            code += 12;
            break;
        case Uniform2fI:
            glUniform2f(**(GLint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8));
            code += 12;
            break;
        case Uniform2fv:
            glUniform2fv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform2fvI:
            glUniform2fv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform2i:
            glUniform2i(*(GLint*)code, *(GLint*)(code + 4), *(GLint*)(code + 8));
            code += 12;
            break;
        case Uniform2iI:
            glUniform2i(**(GLint**)code, **(GLint**)(code + 4), **(GLint**)(code + 8));
            code += 12;
            break;
        case Uniform2iv:
            glUniform2iv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform2ivI:
            glUniform2iv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform3f:
            glUniform3f(*(GLint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12));
            code += 16;
            break;
        case Uniform3fI:
            glUniform3f(**(GLint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12));
            code += 16;
            break;
        case Uniform3fv:
            glUniform3fv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform3fvI:
            glUniform3fv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform3i:
            glUniform3i(*(GLint*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12));
            code += 16;
            break;
        case Uniform3iI:
            glUniform3i(**(GLint**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12));
            code += 16;
            break;
        case Uniform3iv:
            glUniform3iv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform3ivI:
            glUniform3iv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform4f:
            glUniform4f(*(GLint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12), *(GLfloat*)(code + 16));
            code += 20;
            break;
        case Uniform4fI:
            glUniform4f(**(GLint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12), **(GLfloat**)(code + 16));
            code += 20;
            break;
        case Uniform4fv:
            glUniform4fv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform4fvI:
            glUniform4fv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLfloat **)(code + 8));
            code += 12;
            break;
        case Uniform4i:
            glUniform4i(*(GLint*)code, *(GLint*)(code + 4), *(GLint*)(code + 8), *(GLint*)(code + 12), *(GLint*)(code + 16));
            code += 20;
            break;
        case Uniform4iI:
            glUniform4i(**(GLint**)code, **(GLint**)(code + 4), **(GLint**)(code + 8), **(GLint**)(code + 12), **(GLint**)(code + 16));
            code += 20;
            break;
        case Uniform4iv:
            glUniform4iv(*(GLint*)code, *(GLsizei*)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case Uniform4ivI:
            glUniform4iv(**(GLint**)code, **(GLsizei**)(code + 4), *(const GLint **)(code + 8));
            code += 12;
            break;
        case UniformMatrix2fv:
            glUniformMatrix2fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix2fvI:
            glUniformMatrix2fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3fv:
            glUniformMatrix3fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix3fvI:
            glUniformMatrix3fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4fv:
            glUniformMatrix4fv(*(GLint*)code, *(GLsizei*)(code + 4), *(GLboolean*)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UniformMatrix4fvI:
            glUniformMatrix4fv(**(GLint**)code, **(GLsizei**)(code + 4), **(GLboolean**)(code + 8), *(const GLfloat **)(code + 12));
            code += 16;
            break;
        case UseProgram:
            glUseProgram(*(GLuint*)code);
            code += 4;
            break;
        case UseProgramI:
            glUseProgram(**(GLuint**)code);
            code += 4;
            break;
        case ValidateProgram:
            glValidateProgram(*(GLuint*)code);
            code += 4;
            break;
        case ValidateProgramI:
            glValidateProgram(**(GLuint**)code);
            code += 4;
            break;
        case VertexAttrib1f:
            glVertexAttrib1f(*(GLuint*)code, *(GLfloat*)(code + 4));
            code += 8;
            break;
        case VertexAttrib1fI:
            glVertexAttrib1f(**(GLuint**)code, **(GLfloat**)(code + 4));
            code += 8;
            break;
        case VertexAttrib1fv:
            glVertexAttrib1fv(*(GLuint*)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib1fvI:
            glVertexAttrib1fv(**(GLuint**)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib2f:
            glVertexAttrib2f(*(GLuint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8));
            code += 12;
            break;
        case VertexAttrib2fI:
            glVertexAttrib2f(**(GLuint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8));
            code += 12;
            break;
        case VertexAttrib2fv:
            glVertexAttrib2fv(*(GLuint*)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib2fvI:
            glVertexAttrib2fv(**(GLuint**)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib3f:
            glVertexAttrib3f(*(GLuint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12));
            code += 16;
            break;
        case VertexAttrib3fI:
            glVertexAttrib3f(**(GLuint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12));
            code += 16;
            break;
        case VertexAttrib3fv:
            glVertexAttrib3fv(*(GLuint*)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib3fvI:
            glVertexAttrib3fv(**(GLuint**)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib4f:
            glVertexAttrib4f(*(GLuint*)code, *(GLfloat*)(code + 4), *(GLfloat*)(code + 8), *(GLfloat*)(code + 12), *(GLfloat*)(code + 16));
            code += 20;
            break;
        case VertexAttrib4fI:
            glVertexAttrib4f(**(GLuint**)code, **(GLfloat**)(code + 4), **(GLfloat**)(code + 8), **(GLfloat**)(code + 12), **(GLfloat**)(code + 16));
            code += 20;
            break;
        case VertexAttrib4fv:
            glVertexAttrib4fv(*(GLuint*)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttrib4fvI:
            glVertexAttrib4fv(**(GLuint**)code, *(const GLfloat **)(code + 4));
            code += 8;
            break;
        case VertexAttribPointer:
            glVertexAttribPointer(*(GLuint*)code, *(GLint*)(code + 4), *(GLenum*)(code + 8), *(GLboolean*)(code + 12), *(GLsizei*)(code + 16), *(const void **)(code + 20));
            code += 24;
            break;
        case VertexAttribPointerI:
            glVertexAttribPointer(**(GLuint**)code, **(GLint**)(code + 4), **(GLenum**)(code + 8), **(GLboolean**)(code + 12), **(GLsizei**)(code + 16), *(const void **)(code + 20));
            code += 24;
            break;
        case Viewport:
            glViewport(*(GLint*)code, *(GLint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei*)(code + 12));
            code += 16;
            break;
        case ViewportI:
            glViewport(**(GLint**)code, **(GLint**)(code + 4), **(GLsizei**)(code + 8), **(GLsizei**)(code + 12));
            code += 16;
            break;
        case GetBufferSubData:
            glGetBufferSubData(*(GLenum*)code, *(GLintptr*)(code + 4), *(GLsizeiptr*)(code + 8), *(void**)(code + 12));
            code += 16;
            break;
        case GetBufferSubDataI:
            glGetBufferSubData(**(GLenum**)code, **(GLintptr**)(code + 4), **(GLsizeiptr**)(code + 8), **(void***)(code + 12));
            code += 16;
            break;
        case MultiDrawArraysIndirect:
            glMultiDrawArraysIndirect(*(GLenum*)code, *(GLuint*)(code + 4), *(int*)(code + 8), *(VertexBufferBindingInfo**)(code + 12));
            code += 16;
            break;
        case MultiDrawArraysIndirectI:
            glMultiDrawArraysIndirect(**(GLenum**)code, **(GLuint**)(code + 4), **(int**)(code + 8), **(VertexBufferBindingInfo***)(code + 12));
            code += 16;
            break;
        case MultiDrawArrays:
            glMultiDrawArrays(*(GLenum*)code, *(DrawElementsIndirectCommand**)(code + 4), *(int*)(code + 8), *(VertexBufferBindingInfo**)(code + 12));
            code += 16;
            break;
        case MultiDrawArraysI:
            glMultiDrawArrays(**(GLenum**)code, **(DrawElementsIndirectCommand***)(code + 4), **(int**)(code + 8), **(VertexBufferBindingInfo***)(code + 12));
            code += 16;
            break;
        case MultiDrawElementsIndirect:
            glMultiDrawElementsIndirect(*(GLenum*)code, *(GLuint*)(code + 4), *(int*)(code + 8), *(GLenum*)(code + 12), *(VertexBufferBindingInfo**)(code + 16));
            code += 20;
            break;
        case MultiDrawElementsIndirectI:
            glMultiDrawElementsIndirect(**(GLenum**)code, **(GLuint**)(code + 4), **(int**)(code + 8), **(GLenum**)(code + 12), **(VertexBufferBindingInfo***)(code + 16));
            code += 20;
            break;
        case MultiDrawElements:
            glMultiDrawElements(*(GLenum*)code, *(DrawElementsIndirectCommand**)(code + 4), *(int*)(code + 8), *(GLenum*)(code + 12), *(VertexBufferBindingInfo**)(code + 16));
            code += 20;
            break;
        case MultiDrawElementsI:
            glMultiDrawElements(**(GLenum**)code, **(DrawElementsIndirectCommand***)(code + 4), **(int**)(code + 8), **(GLenum**)(code + 12), **(VertexBufferBindingInfo***)(code + 16));
            code += 20;
            break;
        case Commit:
            glCommit();
            code += 0;
            break;
        case TexSubImage2DJSImage:
            glTexSubImage2DJSImage(*(GLenum*)code, *(int*)(code + 4), *(int*)(code + 8), *(int*)(code + 12), *(int*)(code + 16), *(int*)(code + 20), *(GLenum*)(code + 24), *(GLenum*)(code + 28), *(int*)(code + 32));
            code += 36;
            break;
        case TexSubImage2DJSImageI:
            glTexSubImage2DJSImage(**(GLenum**)code, **(int**)(code + 4), **(int**)(code + 8), **(int**)(code + 12), **(int**)(code + 16), **(int**)(code + 20), **(GLenum**)(code + 24), **(GLenum**)(code + 28), **(int**)(code + 32));
            code += 36;
            break;
        case CopyDD:
            memcpy(*(void**)(code+4), *(void**)code, **(size_t**)(code+8));
            code += 12;
            break;
        case CopyDI:
            memcpy(**(void***)(code+4), *(void**)code, **(size_t**)(code+8));
            code += 12;
            break;
        case CopyID:
            memcpy(**(void***)(code+4), *(void**)code, **(size_t**)(code+8));
            code += 12;
            break;
        case CopyII:
            memcpy(**(void***)(code+4), **(void***)code, **(size_t**)(code+8));
            code += 12;
            break;
        case Add:
            *(intptr_t*)(code+8) = *(intptr_t*)code + *(intptr_t*)(code+4);
            code += 12;
            break;
        case Mad:
            *(intptr_t*)(code+12) = *(intptr_t*)code + *(intptr_t*)(code+4) * *(intptr_t*)(code+8);
            code += 16;
            break;
        case Copy:
            memcpy(*(void**)(code+4), *(void**)code, **(size_t**)(code+8));
            code += 12;
            break;
        case Custom:
            ((void (*) (void))**(void***)code)();
            code += 4;
            break;
        case Switch:
            value = **(int**)code; code += 4;
            cnt = *(int*)code; code += 4;
            for(int i = 0; i < cnt; i++) {
                int v = *(int*)code; code += 4;
                int o = *(int*)code; code += 4;
                if (v == value) {
                    code += o;
                    break;
                }
            }
            break;
        case Jmp:
            code += *(intptr_t*)code;
            break;
        case Log:
            cnt = *(int*)code; code += 4;
            emscripten_log(EM_LOG_INFO, (const char*)code); code += cnt;
            break;
        case Push1:
            *(uint8_t*)stack = **(uint8_t**)code;
            code += 4; stack += 1;
            break;
        case Push2:
            *(uint16_t*)stack = **(uint16_t**)code;
            code += 4; stack += 2;
            break;
        case Push4:
            *(uint32_t*)stack = **(uint32_t**)code;
            code += 4; stack += 4;
            break;
        case Push8:
            *(uint64_t*)stack = **(uint64_t**)code;
            code += 4; stack += 8;
            break;
        case Pop1:
            stack -= 1;
            **(uint8_t**)code = *(uint8_t*)stack;
            code += 4;
            break;
        case Pop2:
            stack -= 2;
            **(uint16_t**)code = *(uint16_t*)stack;
            code += 4;
            break;
        case Pop4:
            stack -= 4;
            **(uint32_t**)code = *(uint32_t*)stack;
            code += 4;
            break;
        case Pop8:
            stack -= 8;
            **(uint64_t**)code = *(uint64_t*)stack;
            code += 4;
            break;
        case Bgra:
            value = *(intptr_t*)code; code += 4;
            cnt = **(int**)code; code += 4;
            temp1 = 0;
            for(int i = 0; i < cnt; i++) {
                 temp2 = ((char*)value)[temp1];
                 ((char*)value)[temp1] = ((char*)value)[temp1+2];
                 ((char*)value)[temp1+2] = (char)temp2;
                 temp1 += 4;
            }
            break;
        case CopyBgra:
            temp1 = *(intptr_t*)code; code += 4;
            temp2 = *(intptr_t*)code; code += 4;
            cnt = **(int**)code; code += 4;
            for(int i = 0, o = 0; i < cnt; i++, o+=4) {
                 ((char*)temp2)[o+2] = ((char*)temp1)[o];
                 ((char*)temp2)[o+1] = ((char*)temp1)[o+1];
                 ((char*)temp2)[o] = ((char*)temp1)[o+2];
                 ((char*)temp2)[o+3] = ((char*)temp1)[o+3];
            }
            break;
        default:
            EM_ASM_({ console.error("bad OP: ", $0); }, op);
            return -1;
        }
    }
    return 0;
}

typedef struct Fragment {
    struct Fragment* Prev;
    struct Fragment* Next;
    intptr_t Code;
    int      Length;
} Fragment;
int emRun(Fragment* frag, intptr_t stack) {
    while(frag) {
        if(emInterpret(frag->Code, frag->Length, stack) != 0) return -1;
        frag = frag->Next;
    }
    return 0;
}
