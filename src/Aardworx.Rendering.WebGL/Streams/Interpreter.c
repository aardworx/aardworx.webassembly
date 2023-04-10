#include <emscripten.h>
#include <emscripten/html5.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <GLES3/gl3.h>
#include "../Native/WebGL.h"

typedef enum {
    Uniform2iv = 0,
    Uniform2ivI = 1,
    Uniform3f = 2,
    Uniform3fI = 3,
    Uniform3fv = 4,
    Uniform3fvI = 5,
    Uniform3i = 6,
    Uniform3iI = 7,
    Uniform3iv = 8,
    Uniform3ivI = 9,
    Uniform4f = 10,
    Uniform4fI = 11,
    Uniform4fv = 12,
    Uniform4fvI = 13,
    Uniform4i = 14,
    Uniform4iI = 15,
    Uniform4iv = 16,
    Uniform4ivI = 17,
    UniformMatrix2fv = 18,
    UniformMatrix2fvI = 19,
    UniformMatrix3fv = 20,
    UniformMatrix3fvI = 21,
    UniformMatrix4fv = 22,
    UniformMatrix4fvI = 23,
    UseProgram = 24,
    UseProgramI = 25,
    ValidateProgram = 26,
    ValidateProgramI = 27,
    VertexAttrib1f = 28,
    VertexAttrib1fI = 29,
    VertexAttrib1fv = 30,
    VertexAttrib1fvI = 31,
    VertexAttrib2f = 32,
    VertexAttrib2fI = 33,
    VertexAttrib2fv = 34,
    VertexAttrib2fvI = 35,
    VertexAttrib3f = 36,
    VertexAttrib3fI = 37,
    VertexAttrib3fv = 38,
    VertexAttrib3fvI = 39,
    VertexAttrib4f = 40,
    VertexAttrib4fI = 41,
    VertexAttrib4fv = 42,
    VertexAttrib4fvI = 43,
    VertexAttribPointer = 44,
    VertexAttribPointerI = 45,
    Viewport = 46,
    ViewportI = 47,
    TexImage2D = 48,
    TexImage2DI = 49,
    TexParameterf = 50,
    TexParameterfI = 51,
    TexParameterfv = 52,
    TexParameterfvI = 53,
    TexParameteri = 54,
    TexParameteriI = 55,
    TexParameteriv = 56,
    TexParameterivI = 57,
    TexSubImage2D = 58,
    TexSubImage2DI = 59,
    Uniform1f = 60,
    Uniform1fI = 61,
    Uniform1fv = 62,
    Uniform1fvI = 63,
    Uniform1i = 64,
    Uniform1iI = 65,
    Uniform1iv = 66,
    Uniform1ivI = 67,
    Uniform2f = 68,
    Uniform2fI = 69,
    Uniform2fv = 70,
    Uniform2fvI = 71,
    Uniform2i = 72,
    Uniform2iI = 73,
    ReadPixels = 74,
    ReadPixelsI = 75,
    ReleaseShaderCompiler = 76,
    RenderbufferStorage = 77,
    RenderbufferStorageI = 78,
    SampleCoverage = 79,
    SampleCoverageI = 80,
    Scissor = 81,
    ScissorI = 82,
    ShaderBinary = 83,
    ShaderBinaryI = 84,
    ShaderSource = 85,
    ShaderSourceI = 86,
    StencilFunc = 87,
    StencilFuncI = 88,
    StencilFuncSeparate = 89,
    StencilFuncSeparateI = 90,
    StencilMask = 91,
    StencilMaskI = 92,
    StencilMaskSeparate = 93,
    StencilMaskSeparateI = 94,
    StencilOp = 95,
    StencilOpI = 96,
    StencilOpSeparate = 97,
    StencilOpSeparateI = 98,
    GetShaderInfoLog = 99,
    GetShaderInfoLogI = 100,
    GetShaderPrecisionFormat = 101,
    GetShaderPrecisionFormatI = 102,
    GetShaderSource = 103,
    GetShaderSourceI = 104,
    GetString = 105,
    GetStringI = 106,
    GetTexParameterfv = 107,
    GetTexParameterfvI = 108,
    GetTexParameteriv = 109,
    GetTexParameterivI = 110,
    GetUniformfv = 111,
    GetUniformfvI = 112,
    GetUniformiv = 113,
    GetUniformivI = 114,
    GetUniformLocation = 115,
    GetUniformLocationI = 116,
    GetVertexAttribfv = 117,
    GetVertexAttribfvI = 118,
    GetVertexAttribiv = 119,
    GetVertexAttribivI = 120,
    GetVertexAttribPointerv = 121,
    GetVertexAttribPointervI = 122,
    Hint = 123,
    HintI = 124,
    IsBuffer = 125,
    IsBufferI = 126,
    IsEnabled = 127,
    IsEnabledI = 128,
    IsFramebuffer = 129,
    IsFramebufferI = 130,
    IsProgram = 131,
    IsProgramI = 132,
    IsRenderbuffer = 133,
    IsRenderbufferI = 134,
    IsShader = 135,
    IsShaderI = 136,
    IsTexture = 137,
    IsTextureI = 138,
    LineWidth = 139,
    LineWidthI = 140,
    LinkProgram = 141,
    LinkProgramI = 142,
    PixelStorei = 143,
    PixelStoreiI = 144,
    PolygonOffset = 145,
    PolygonOffsetI = 146,
    GetActiveUniform = 147,
    GetActiveUniformI = 148,
    GetAttachedShaders = 149,
    GetAttachedShadersI = 150,
    GetAttribLocation = 151,
    GetAttribLocationI = 152,
    GetBooleanv = 153,
    GetBooleanvI = 154,
    GetBufferParameteriv = 155,
    GetBufferParameterivI = 156,
    GetError = 157,
    GetErrorI = 158,
    GetFloatv = 159,
    GetFloatvI = 160,
    GetFramebufferAttachmentParameteriv = 161,
    GetFramebufferAttachmentParameterivI = 162,
    GetIntegerv = 163,
    GetIntegervI = 164,
    GetProgramiv = 165,
    GetProgramivI = 166,
    GetProgramInfoLog = 167,
    GetProgramInfoLogI = 168,
    GetRenderbufferParameteriv = 169,
    GetRenderbufferParameterivI = 170,
    GetShaderiv = 171,
    GetShaderivI = 172,
    GetActiveAttrib = 173,
    GetActiveAttribI = 174,
    DeleteBuffers = 175,
    DeleteBuffersI = 176,
    DeleteFramebuffers = 177,
    DeleteFramebuffersI = 178,
    DeleteProgram = 179,
    DeleteProgramI = 180,
    DeleteRenderbuffers = 181,
    DeleteRenderbuffersI = 182,
    DeleteShader = 183,
    DeleteShaderI = 184,
    DeleteTextures = 185,
    DeleteTexturesI = 186,
    DepthFunc = 187,
    DepthFuncI = 188,
    DepthMask = 189,
    DepthMaskI = 190,
    DepthRangef = 191,
    DepthRangefI = 192,
    DetachShader = 193,
    DetachShaderI = 194,
    Disable = 195,
    DisableI = 196,
    DisableVertexAttribArray = 197,
    DisableVertexAttribArrayI = 198,
    DrawArrays = 199,
    DrawArraysI = 200,
    DrawElements = 201,
    DrawElementsI = 202,
    Enable = 203,
    EnableI = 204,
    EnableVertexAttribArray = 205,
    EnableVertexAttribArrayI = 206,
    Finish = 207,
    Flush = 208,
    FramebufferRenderbuffer = 209,
    FramebufferRenderbufferI = 210,
    FramebufferTexture2D = 211,
    FramebufferTexture2DI = 212,
    FrontFace = 213,
    FrontFaceI = 214,
    GenBuffers = 215,
    GenBuffersI = 216,
    GenerateMipmap = 217,
    GenerateMipmapI = 218,
    GenFramebuffers = 219,
    GenFramebuffersI = 220,
    GenRenderbuffers = 221,
    GenRenderbuffersI = 222,
    GenTextures = 223,
    GenTexturesI = 224,
    BlendFuncSeparate = 225,
    BlendFuncSeparateI = 226,
    BufferData = 227,
    BufferDataI = 228,
    BufferSubData = 229,
    BufferSubDataI = 230,
    CheckFramebufferStatus = 231,
    CheckFramebufferStatusI = 232,
    Clear = 233,
    ClearI = 234,
    ClearColor = 235,
    ClearColorI = 236,
    ClearDepthf = 237,
    ClearDepthfI = 238,
    ClearStencil = 239,
    ClearStencilI = 240,
    ColorMask = 241,
    ColorMaskI = 242,
    CompileShader = 243,
    CompileShaderI = 244,
    CompressedTexImage2D = 245,
    CompressedTexImage2DI = 246,
    CompressedTexSubImage2D = 247,
    CompressedTexSubImage2DI = 248,
    CopyTexImage2D = 249,
    CopyTexImage2DI = 250,
    CopyTexSubImage2D = 251,
    CopyTexSubImage2DI = 252,
    CreateProgram = 253,
    CreateProgramI = 254,
    CreateShader = 255,
    CreateShaderI = 256,
    CullFace = 257,
    CullFaceI = 258,
    TransformFeedbackVaryings = 259,
    TransformFeedbackVaryingsI = 260,
    Uniform1ui = 261,
    Uniform1uiI = 262,
    Uniform1uiv = 263,
    Uniform1uivI = 264,
    Uniform2ui = 265,
    Uniform2uiI = 266,
    Uniform2uiv = 267,
    Uniform2uivI = 268,
    Uniform3ui = 269,
    Uniform3uiI = 270,
    Uniform3uiv = 271,
    Uniform3uivI = 272,
    Uniform4ui = 273,
    Uniform4uiI = 274,
    Uniform4uiv = 275,
    Uniform4uivI = 276,
    UniformBlockBinding = 277,
    UniformBlockBindingI = 278,
    UniformMatrix2x3fv = 279,
    UniformMatrix2x3fvI = 280,
    UniformMatrix2x4fv = 281,
    UniformMatrix2x4fvI = 282,
    UniformMatrix3x2fv = 283,
    UniformMatrix3x2fvI = 284,
    UniformMatrix3x4fv = 285,
    UniformMatrix3x4fvI = 286,
    UniformMatrix4x2fv = 287,
    UniformMatrix4x2fvI = 288,
    UniformMatrix4x3fv = 289,
    UniformMatrix4x3fvI = 290,
    VertexAttribDivisor = 291,
    VertexAttribDivisorI = 292,
    VertexAttribI4i = 293,
    VertexAttribI4iI = 294,
    VertexAttribI4ui = 295,
    VertexAttribI4uiI = 296,
    VertexAttribI4iv = 297,
    VertexAttribI4ivI = 298,
    VertexAttribI4uiv = 299,
    VertexAttribI4uivI = 300,
    VertexAttribIPointer = 301,
    VertexAttribIPointerI = 302,
    WaitSync = 303,
    WaitSyncI = 304,
    ActiveTexture = 305,
    ActiveTextureI = 306,
    AttachShader = 307,
    AttachShaderI = 308,
    BindAttribLocation = 309,
    BindAttribLocationI = 310,
    BindBuffer = 311,
    BindBufferI = 312,
    BindFramebuffer = 313,
    BindFramebufferI = 314,
    BindRenderbuffer = 315,
    BindRenderbufferI = 316,
    BindTexture = 317,
    BindTextureI = 318,
    BlendColor = 319,
    BlendColorI = 320,
    BlendEquation = 321,
    BlendEquationI = 322,
    BlendEquationSeparate = 323,
    BlendEquationSeparateI = 324,
    BlendFunc = 325,
    BlendFuncI = 326,
    SamplerParameteri = 327,
    SamplerParameteriI = 328,
    SamplerParameteriv = 329,
    SamplerParameterivI = 330,
    SamplerParameterf = 331,
    SamplerParameterfI = 332,
    SamplerParameterfv = 333,
    SamplerParameterfvI = 334,
    TexImage3D = 335,
    TexImage3DI = 336,
    TexStorage2D = 337,
    TexStorage2DI = 338,
    TexStorage3D = 339,
    TexStorage3DI = 340,
    TexSubImage3D = 341,
    TexSubImage3DI = 342,
    GetTransformFeedbackVarying = 343,
    GetTransformFeedbackVaryingI = 344,
    GetUniformuiv = 345,
    GetUniformuivI = 346,
    GetUniformBlockIndex = 347,
    GetUniformBlockIndexI = 348,
    GetUniformIndices = 349,
    GetUniformIndicesI = 350,
    GetVertexAttribIiv = 351,
    GetVertexAttribIivI = 352,
    GetVertexAttribIuiv = 353,
    GetVertexAttribIuivI = 354,
    InvalidateFramebuffer = 355,
    InvalidateFramebufferI = 356,
    InvalidateSubFramebuffer = 357,
    InvalidateSubFramebufferI = 358,
    IsQuery = 359,
    IsQueryI = 360,
    IsSampler = 361,
    IsSamplerI = 362,
    IsSync = 363,
    IsSyncI = 364,
    IsTransformFeedback = 365,
    IsTransformFeedbackI = 366,
    IsVertexArray = 367,
    IsVertexArrayI = 368,
    PauseTransformFeedback = 369,
    ProgramBinary = 370,
    ProgramBinaryI = 371,
    ProgramParameteri = 372,
    ProgramParameteriI = 373,
    ReadBuffer = 374,
    ReadBufferI = 375,
    RenderbufferStorageMultisample = 376,
    RenderbufferStorageMultisampleI = 377,
    ResumeTransformFeedback = 378,
    GetInteger64v = 379,
    GetInteger64vI = 380,
    GetInteger64i_v = 381,
    GetInteger64i_vI = 382,
    GetInternalformativ = 383,
    GetInternalformativI = 384,
    GetProgramBinary = 385,
    GetProgramBinaryI = 386,
    GetQueryiv = 387,
    GetQueryivI = 388,
    GetQueryObjectuiv = 389,
    GetQueryObjectuivI = 390,
    GetSamplerParameteriv = 391,
    GetSamplerParameterivI = 392,
    GetSamplerParameterfv = 393,
    GetSamplerParameterfvI = 394,
    GetStringi = 395,
    GetStringiI = 396,
    GetSynciv = 397,
    GetSyncivI = 398,
    DrawElementsInstanced = 399,
    DrawElementsInstancedI = 400,
    DrawRangeElements = 401,
    DrawRangeElementsI = 402,
    EndQuery = 403,
    EndQueryI = 404,
    EndTransformFeedback = 405,
    FenceSync = 406,
    FenceSyncI = 407,
    FramebufferTextureLayer = 408,
    FramebufferTextureLayerI = 409,
    GenQueries = 410,
    GenQueriesI = 411,
    GenSamplers = 412,
    GenSamplersI = 413,
    GenTransformFeedbacks = 414,
    GenTransformFeedbacksI = 415,
    GenVertexArrays = 416,
    GenVertexArraysI = 417,
    GetActiveUniformBlockiv = 418,
    GetActiveUniformBlockivI = 419,
    GetActiveUniformBlockName = 420,
    GetActiveUniformBlockNameI = 421,
    GetActiveUniformsiv = 422,
    GetActiveUniformsivI = 423,
    GetBufferParameteri64v = 424,
    GetBufferParameteri64vI = 425,
    GetFragDataLocation = 426,
    GetFragDataLocationI = 427,
    GetIntegeri_v = 428,
    GetIntegeri_vI = 429,
    BindBufferBase = 430,
    BindBufferBaseI = 431,
    BindBufferRange = 432,
    BindBufferRangeI = 433,
    BindSampler = 434,
    BindSamplerI = 435,
    BindTransformFeedback = 436,
    BindTransformFeedbackI = 437,
    BindVertexArray = 438,
    BindVertexArrayI = 439,
    BlitFramebuffer = 440,
    BlitFramebufferI = 441,
    ClearBufferiv = 442,
    ClearBufferivI = 443,
    ClearBufferuiv = 444,
    ClearBufferuivI = 445,
    ClearBufferfv = 446,
    ClearBufferfvI = 447,
    ClearBufferfi = 448,
    ClearBufferfiI = 449,
    ClientWaitSync = 450,
    ClientWaitSyncI = 451,
    CompressedTexImage3D = 452,
    CompressedTexImage3DI = 453,
    CompressedTexSubImage3D = 454,
    CompressedTexSubImage3DI = 455,
    CopyBufferSubData = 456,
    CopyBufferSubDataI = 457,
    CopyTexSubImage3D = 458,
    CopyTexSubImage3DI = 459,
    DeleteQueries = 460,
    DeleteQueriesI = 461,
    DeleteSamplers = 462,
    DeleteSamplersI = 463,
    DeleteSync = 464,
    DeleteSyncI = 465,
    DeleteTransformFeedbacks = 466,
    DeleteTransformFeedbacksI = 467,
    DeleteVertexArrays = 468,
    DeleteVertexArraysI = 469,
    DrawArraysInstanced = 470,
    DrawArraysInstancedI = 471,
    DrawBuffers = 472,
    DrawBuffersI = 473,
    BeginQuery = 474,
    BeginQueryI = 475,
    BeginTransformFeedback = 476,
    BeginTransformFeedbackI = 477,
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
        case GetActiveAttrib:
            glGetActiveAttrib(*(GLuint*)code, *(GLuint*)(code + 4), *(GLsizei*)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
            break;
        case GetActiveAttribI:
            glGetActiveAttrib(**(GLuint**)code, **(GLuint**)(code + 4), **(GLsizei**)(code + 8), *(GLsizei **)(code + 12), *(GLint **)(code + 16), *(GLenum **)(code + 20), *(GLchar **)(code + 24));
            code += 28;
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
