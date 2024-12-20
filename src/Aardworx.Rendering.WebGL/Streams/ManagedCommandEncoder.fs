// ===============================================================================
//                    AUTO GENERATED FILE (see Generator.fsx)
// ===============================================================================

namespace Aardworx.Rendering.WebGL.Streams
open System.Runtime.InteropServices
open System.Security
open Silk.NET.OpenGLES
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open FSharp.Data.Adaptive
open Microsoft.FSharp.NativeInterop
open Aardvark.Base

#nowarn "9"


type internal GLFunctionPointers() =
    [<DefaultValue>]
    val mutable public glBeginQuery : nativeint
    [<DefaultValue>]
    val mutable public glBeginTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glBindBufferBase : nativeint
    [<DefaultValue>]
    val mutable public glBindBufferRange : nativeint
    [<DefaultValue>]
    val mutable public glBindSampler : nativeint
    [<DefaultValue>]
    val mutable public glBindTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glBindVertexArray : nativeint
    [<DefaultValue>]
    val mutable public glBlitFramebuffer : nativeint
    [<DefaultValue>]
    val mutable public glClearBufferiv : nativeint
    [<DefaultValue>]
    val mutable public glClearBufferuiv : nativeint
    [<DefaultValue>]
    val mutable public glClearBufferfv : nativeint
    [<DefaultValue>]
    val mutable public glClearBufferfi : nativeint
    [<DefaultValue>]
    val mutable public glClientWaitSync : nativeint
    [<DefaultValue>]
    val mutable public glCompressedTexImage3D : nativeint
    [<DefaultValue>]
    val mutable public glCompressedTexSubImage3D : nativeint
    [<DefaultValue>]
    val mutable public glCopyBufferSubData : nativeint
    [<DefaultValue>]
    val mutable public glCopyTexSubImage3D : nativeint
    [<DefaultValue>]
    val mutable public glDeleteQueries : nativeint
    [<DefaultValue>]
    val mutable public glDeleteSamplers : nativeint
    [<DefaultValue>]
    val mutable public glDeleteSync : nativeint
    [<DefaultValue>]
    val mutable public glDeleteTransformFeedbacks : nativeint
    [<DefaultValue>]
    val mutable public glDeleteVertexArrays : nativeint
    [<DefaultValue>]
    val mutable public glDrawArraysInstanced : nativeint
    [<DefaultValue>]
    val mutable public glDrawBuffers : nativeint
    [<DefaultValue>]
    val mutable public glDrawElementsInstanced : nativeint
    [<DefaultValue>]
    val mutable public glDrawRangeElements : nativeint
    [<DefaultValue>]
    val mutable public glEndQuery : nativeint
    [<DefaultValue>]
    val mutable public glEndTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glFenceSync : nativeint
    [<DefaultValue>]
    val mutable public glFramebufferTextureLayer : nativeint
    [<DefaultValue>]
    val mutable public glGenQueries : nativeint
    [<DefaultValue>]
    val mutable public glGenSamplers : nativeint
    [<DefaultValue>]
    val mutable public glGenTransformFeedbacks : nativeint
    [<DefaultValue>]
    val mutable public glGenVertexArrays : nativeint
    [<DefaultValue>]
    val mutable public glGetActiveUniformBlockiv : nativeint
    [<DefaultValue>]
    val mutable public glGetActiveUniformBlockName : nativeint
    [<DefaultValue>]
    val mutable public glGetActiveUniformsiv : nativeint
    [<DefaultValue>]
    val mutable public glGetBufferParameteri64v : nativeint
    [<DefaultValue>]
    val mutable public glGetFragDataLocation : nativeint
    [<DefaultValue>]
    val mutable public glGetIntegeri_v : nativeint
    [<DefaultValue>]
    val mutable public glGetInteger64v : nativeint
    [<DefaultValue>]
    val mutable public glGetInteger64i_v : nativeint
    [<DefaultValue>]
    val mutable public glGetInternalformativ : nativeint
    [<DefaultValue>]
    val mutable public glGetProgramBinary : nativeint
    [<DefaultValue>]
    val mutable public glGetQueryiv : nativeint
    [<DefaultValue>]
    val mutable public glGetQueryObjectuiv : nativeint
    [<DefaultValue>]
    val mutable public glGetSamplerParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glGetSamplerParameterfv : nativeint
    [<DefaultValue>]
    val mutable public glGetStringi : nativeint
    [<DefaultValue>]
    val mutable public glGetSynciv : nativeint
    [<DefaultValue>]
    val mutable public glGetTransformFeedbackVarying : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformuiv : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformBlockIndex : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformIndices : nativeint
    [<DefaultValue>]
    val mutable public glGetVertexAttribIiv : nativeint
    [<DefaultValue>]
    val mutable public glGetVertexAttribIuiv : nativeint
    [<DefaultValue>]
    val mutable public glInvalidateFramebuffer : nativeint
    [<DefaultValue>]
    val mutable public glInvalidateSubFramebuffer : nativeint
    [<DefaultValue>]
    val mutable public glIsQuery : nativeint
    [<DefaultValue>]
    val mutable public glIsSampler : nativeint
    [<DefaultValue>]
    val mutable public glIsSync : nativeint
    [<DefaultValue>]
    val mutable public glIsTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glIsVertexArray : nativeint
    [<DefaultValue>]
    val mutable public glPauseTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glProgramBinary : nativeint
    [<DefaultValue>]
    val mutable public glProgramParameteri : nativeint
    [<DefaultValue>]
    val mutable public glReadBuffer : nativeint
    [<DefaultValue>]
    val mutable public glRenderbufferStorageMultisample : nativeint
    [<DefaultValue>]
    val mutable public glResumeTransformFeedback : nativeint
    [<DefaultValue>]
    val mutable public glSamplerParameteri : nativeint
    [<DefaultValue>]
    val mutable public glSamplerParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glSamplerParameterf : nativeint
    [<DefaultValue>]
    val mutable public glSamplerParameterfv : nativeint
    [<DefaultValue>]
    val mutable public glTexImage3D : nativeint
    [<DefaultValue>]
    val mutable public glTexStorage2D : nativeint
    [<DefaultValue>]
    val mutable public glTexStorage3D : nativeint
    [<DefaultValue>]
    val mutable public glTexSubImage3D : nativeint
    [<DefaultValue>]
    val mutable public glTransformFeedbackVaryings : nativeint
    [<DefaultValue>]
    val mutable public glUniform1ui : nativeint
    [<DefaultValue>]
    val mutable public glUniform1uiv : nativeint
    [<DefaultValue>]
    val mutable public glUniform2ui : nativeint
    [<DefaultValue>]
    val mutable public glUniform2uiv : nativeint
    [<DefaultValue>]
    val mutable public glUniform3ui : nativeint
    [<DefaultValue>]
    val mutable public glUniform3uiv : nativeint
    [<DefaultValue>]
    val mutable public glUniform4ui : nativeint
    [<DefaultValue>]
    val mutable public glUniform4uiv : nativeint
    [<DefaultValue>]
    val mutable public glUniformBlockBinding : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix2x3fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix2x4fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix3x2fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix3x4fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix4x2fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix4x3fv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribDivisor : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribI4i : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribI4ui : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribI4iv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribI4uiv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribIPointer : nativeint
    [<DefaultValue>]
    val mutable public glWaitSync : nativeint
    [<DefaultValue>]
    val mutable public glActiveTexture : nativeint
    [<DefaultValue>]
    val mutable public glAttachShader : nativeint
    [<DefaultValue>]
    val mutable public glBindAttribLocation : nativeint
    [<DefaultValue>]
    val mutable public glBindBuffer : nativeint
    [<DefaultValue>]
    val mutable public glBindFramebuffer : nativeint
    [<DefaultValue>]
    val mutable public glBindRenderbuffer : nativeint
    [<DefaultValue>]
    val mutable public glBindTexture : nativeint
    [<DefaultValue>]
    val mutable public glBlendColor : nativeint
    [<DefaultValue>]
    val mutable public glBlendEquation : nativeint
    [<DefaultValue>]
    val mutable public glBlendEquationSeparate : nativeint
    [<DefaultValue>]
    val mutable public glBlendFunc : nativeint
    [<DefaultValue>]
    val mutable public glBlendFuncSeparate : nativeint
    [<DefaultValue>]
    val mutable public glBufferData : nativeint
    [<DefaultValue>]
    val mutable public glBufferSubData : nativeint
    [<DefaultValue>]
    val mutable public glCheckFramebufferStatus : nativeint
    [<DefaultValue>]
    val mutable public glClear : nativeint
    [<DefaultValue>]
    val mutable public glClearColor : nativeint
    [<DefaultValue>]
    val mutable public glClearDepthf : nativeint
    [<DefaultValue>]
    val mutable public glClearStencil : nativeint
    [<DefaultValue>]
    val mutable public glColorMask : nativeint
    [<DefaultValue>]
    val mutable public glCompileShader : nativeint
    [<DefaultValue>]
    val mutable public glCompressedTexImage2D : nativeint
    [<DefaultValue>]
    val mutable public glCompressedTexSubImage2D : nativeint
    [<DefaultValue>]
    val mutable public glCopyTexImage2D : nativeint
    [<DefaultValue>]
    val mutable public glCopyTexSubImage2D : nativeint
    [<DefaultValue>]
    val mutable public glCreateProgram : nativeint
    [<DefaultValue>]
    val mutable public glCreateShader : nativeint
    [<DefaultValue>]
    val mutable public glCullFace : nativeint
    [<DefaultValue>]
    val mutable public glDeleteBuffers : nativeint
    [<DefaultValue>]
    val mutable public glDeleteFramebuffers : nativeint
    [<DefaultValue>]
    val mutable public glDeleteProgram : nativeint
    [<DefaultValue>]
    val mutable public glDeleteRenderbuffers : nativeint
    [<DefaultValue>]
    val mutable public glDeleteShader : nativeint
    [<DefaultValue>]
    val mutable public glDeleteTextures : nativeint
    [<DefaultValue>]
    val mutable public glDepthFunc : nativeint
    [<DefaultValue>]
    val mutable public glDepthMask : nativeint
    [<DefaultValue>]
    val mutable public glDepthRangef : nativeint
    [<DefaultValue>]
    val mutable public glDetachShader : nativeint
    [<DefaultValue>]
    val mutable public glDisable : nativeint
    [<DefaultValue>]
    val mutable public glDisableVertexAttribArray : nativeint
    [<DefaultValue>]
    val mutable public glDrawArrays : nativeint
    [<DefaultValue>]
    val mutable public glDrawElements : nativeint
    [<DefaultValue>]
    val mutable public glEnable : nativeint
    [<DefaultValue>]
    val mutable public glEnableVertexAttribArray : nativeint
    [<DefaultValue>]
    val mutable public glFinish : nativeint
    [<DefaultValue>]
    val mutable public glFlush : nativeint
    [<DefaultValue>]
    val mutable public glFramebufferRenderbuffer : nativeint
    [<DefaultValue>]
    val mutable public glFramebufferTexture2D : nativeint
    [<DefaultValue>]
    val mutable public glFrontFace : nativeint
    [<DefaultValue>]
    val mutable public glGenBuffers : nativeint
    [<DefaultValue>]
    val mutable public glGenerateMipmap : nativeint
    [<DefaultValue>]
    val mutable public glGenFramebuffers : nativeint
    [<DefaultValue>]
    val mutable public glGenRenderbuffers : nativeint
    [<DefaultValue>]
    val mutable public glGenTextures : nativeint
    [<DefaultValue>]
    val mutable public glGetActiveAttrib : nativeint
    [<DefaultValue>]
    val mutable public glGetActiveUniform : nativeint
    [<DefaultValue>]
    val mutable public glGetAttachedShaders : nativeint
    [<DefaultValue>]
    val mutable public glGetAttribLocation : nativeint
    [<DefaultValue>]
    val mutable public glGetBooleanv : nativeint
    [<DefaultValue>]
    val mutable public glGetBufferParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glGetError : nativeint
    [<DefaultValue>]
    val mutable public glGetFloatv : nativeint
    [<DefaultValue>]
    val mutable public glGetFramebufferAttachmentParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glGetIntegerv : nativeint
    [<DefaultValue>]
    val mutable public glGetProgramiv : nativeint
    [<DefaultValue>]
    val mutable public glGetProgramInfoLog : nativeint
    [<DefaultValue>]
    val mutable public glGetRenderbufferParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glGetShaderiv : nativeint
    [<DefaultValue>]
    val mutable public glGetShaderInfoLog : nativeint
    [<DefaultValue>]
    val mutable public glGetShaderPrecisionFormat : nativeint
    [<DefaultValue>]
    val mutable public glGetShaderSource : nativeint
    [<DefaultValue>]
    val mutable public glGetString : nativeint
    [<DefaultValue>]
    val mutable public glGetTexParameterfv : nativeint
    [<DefaultValue>]
    val mutable public glGetTexParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformfv : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformiv : nativeint
    [<DefaultValue>]
    val mutable public glGetUniformLocation : nativeint
    [<DefaultValue>]
    val mutable public glGetVertexAttribfv : nativeint
    [<DefaultValue>]
    val mutable public glGetVertexAttribiv : nativeint
    [<DefaultValue>]
    val mutable public glGetVertexAttribPointerv : nativeint
    [<DefaultValue>]
    val mutable public glHint : nativeint
    [<DefaultValue>]
    val mutable public glIsBuffer : nativeint
    [<DefaultValue>]
    val mutable public glIsEnabled : nativeint
    [<DefaultValue>]
    val mutable public glIsFramebuffer : nativeint
    [<DefaultValue>]
    val mutable public glIsProgram : nativeint
    [<DefaultValue>]
    val mutable public glIsRenderbuffer : nativeint
    [<DefaultValue>]
    val mutable public glIsShader : nativeint
    [<DefaultValue>]
    val mutable public glIsTexture : nativeint
    [<DefaultValue>]
    val mutable public glLineWidth : nativeint
    [<DefaultValue>]
    val mutable public glLinkProgram : nativeint
    [<DefaultValue>]
    val mutable public glPixelStorei : nativeint
    [<DefaultValue>]
    val mutable public glPolygonOffset : nativeint
    [<DefaultValue>]
    val mutable public glReadPixels : nativeint
    [<DefaultValue>]
    val mutable public glReleaseShaderCompiler : nativeint
    [<DefaultValue>]
    val mutable public glRenderbufferStorage : nativeint
    [<DefaultValue>]
    val mutable public glSampleCoverage : nativeint
    [<DefaultValue>]
    val mutable public glScissor : nativeint
    [<DefaultValue>]
    val mutable public glShaderBinary : nativeint
    [<DefaultValue>]
    val mutable public glShaderSource : nativeint
    [<DefaultValue>]
    val mutable public glStencilFunc : nativeint
    [<DefaultValue>]
    val mutable public glStencilFuncSeparate : nativeint
    [<DefaultValue>]
    val mutable public glStencilMask : nativeint
    [<DefaultValue>]
    val mutable public glStencilMaskSeparate : nativeint
    [<DefaultValue>]
    val mutable public glStencilOp : nativeint
    [<DefaultValue>]
    val mutable public glStencilOpSeparate : nativeint
    [<DefaultValue>]
    val mutable public glTexImage2D : nativeint
    [<DefaultValue>]
    val mutable public glTexParameterf : nativeint
    [<DefaultValue>]
    val mutable public glTexParameterfv : nativeint
    [<DefaultValue>]
    val mutable public glTexParameteri : nativeint
    [<DefaultValue>]
    val mutable public glTexParameteriv : nativeint
    [<DefaultValue>]
    val mutable public glTexSubImage2D : nativeint
    [<DefaultValue>]
    val mutable public glUniform1f : nativeint
    [<DefaultValue>]
    val mutable public glUniform1fv : nativeint
    [<DefaultValue>]
    val mutable public glUniform1i : nativeint
    [<DefaultValue>]
    val mutable public glUniform1iv : nativeint
    [<DefaultValue>]
    val mutable public glUniform2f : nativeint
    [<DefaultValue>]
    val mutable public glUniform2fv : nativeint
    [<DefaultValue>]
    val mutable public glUniform2i : nativeint
    [<DefaultValue>]
    val mutable public glUniform2iv : nativeint
    [<DefaultValue>]
    val mutable public glUniform3f : nativeint
    [<DefaultValue>]
    val mutable public glUniform3fv : nativeint
    [<DefaultValue>]
    val mutable public glUniform3i : nativeint
    [<DefaultValue>]
    val mutable public glUniform3iv : nativeint
    [<DefaultValue>]
    val mutable public glUniform4f : nativeint
    [<DefaultValue>]
    val mutable public glUniform4fv : nativeint
    [<DefaultValue>]
    val mutable public glUniform4i : nativeint
    [<DefaultValue>]
    val mutable public glUniform4iv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix2fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix3fv : nativeint
    [<DefaultValue>]
    val mutable public glUniformMatrix4fv : nativeint
    [<DefaultValue>]
    val mutable public glUseProgram : nativeint
    [<DefaultValue>]
    val mutable public glValidateProgram : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib1f : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib1fv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib2f : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib2fv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib3f : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib3fv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib4f : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttrib4fv : nativeint
    [<DefaultValue>]
    val mutable public glVertexAttribPointer : nativeint
    [<DefaultValue>]
    val mutable public glViewport : nativeint
    [<DefaultValue>]
    val mutable public glGetBufferSubData : nativeint
    [<DefaultValue>]
    val mutable public glMultiDrawArraysIndirect : nativeint
    [<DefaultValue>]
    val mutable public glMultiDrawArrays : nativeint
    [<DefaultValue>]
    val mutable public glMultiDrawElementsIndirect : nativeint
    [<DefaultValue>]
    val mutable public glMultiDrawElements : nativeint
    [<DefaultValue>]
    val mutable public glCommit : nativeint
    [<DefaultValue>]
    val mutable public glTexSubImage2DJSImage : nativeint


module internal GLFunctionPointers =
    let private cache = System.Collections.Concurrent.ConcurrentDictionary<Silk.NET.Core.Contexts.INativeContext, GLFunctionPointers>()
    let private getProc1 (ctx : Silk.NET.Core.Contexts.INativeContext) (name : string) =
        let mutable res = 0n
        if ctx.TryGetProcAddress(name, &res) then res
        else 0n

    let private getProc (ctx : Silk.NET.Core.Contexts.INativeContext) (names : list<string>) =
        names |> List.tryPick (fun n -> let p = getProc1 ctx n in if p <> 0n then Some p else None) |> Option.defaultValue 0n

    let private load (ctx : Silk.NET.Core.Contexts.INativeContext) =
        let res = GLFunctionPointers()
        res.glBeginQuery <- getProc ctx ["glBeginQuery"; "glBeginQuery"; "glBeginQueryARB"]
        res.glBeginTransformFeedback <- getProc ctx ["glBeginTransformFeedback"; "glBeginTransformFeedback"; "glBeginTransformFeedbackEXT"; "glBeginTransformFeedbackNV"]
        res.glBindBufferBase <- getProc ctx ["glBindBufferBase"; "glBindBufferBase"; "glBindBufferBaseEXT"; "glBindBufferBaseNV"]
        res.glBindBufferRange <- getProc ctx ["glBindBufferRange"; "glBindBufferRange"; "glBindBufferRangeEXT"; "glBindBufferRangeNV"]
        res.glBindSampler <- getProc ctx ["glBindSampler"]
        res.glBindTransformFeedback <- getProc ctx ["glBindTransformFeedback"]
        res.glBindVertexArray <- getProc ctx ["glBindVertexArray"; "glBindVertexArray"; "glBindVertexArrayOES"]
        res.glBlitFramebuffer <- getProc ctx ["glBlitFramebuffer"; "glBlitFramebuffer"; "glBlitFramebufferEXT"; "glBlitFramebufferNV"]
        res.glClearBufferiv <- getProc ctx ["glClearBufferiv"]
        res.glClearBufferuiv <- getProc ctx ["glClearBufferuiv"]
        res.glClearBufferfv <- getProc ctx ["glClearBufferfv"]
        res.glClearBufferfi <- getProc ctx ["glClearBufferfi"]
        res.glClientWaitSync <- getProc ctx ["glClientWaitSync"; "glClientWaitSync"; "glClientWaitSyncAPPLE"]
        res.glCompressedTexImage3D <- getProc ctx ["glCompressedTexImage3D"; "glCompressedTexImage3D"; "glCompressedTexImage3DARB"]
        res.glCompressedTexSubImage3D <- getProc ctx ["glCompressedTexSubImage3D"; "glCompressedTexSubImage3D"; "glCompressedTexSubImage3DARB"]
        res.glCopyBufferSubData <- getProc ctx ["glCopyBufferSubData"; "glCopyBufferSubData"; "glCopyBufferSubDataNV"]
        res.glCopyTexSubImage3D <- getProc ctx ["glCopyTexSubImage3D"; "glCopyTexSubImage3D"; "glCopyTexSubImage3DEXT"]
        res.glDeleteQueries <- getProc ctx ["glDeleteQueries"; "glDeleteQueries"; "glDeleteQueriesARB"]
        res.glDeleteSamplers <- getProc ctx ["glDeleteSamplers"]
        res.glDeleteSync <- getProc ctx ["glDeleteSync"; "glDeleteSync"; "glDeleteSyncAPPLE"]
        res.glDeleteTransformFeedbacks <- getProc ctx ["glDeleteTransformFeedbacks"; "glDeleteTransformFeedbacks"; "glDeleteTransformFeedbacksNV"]
        res.glDeleteVertexArrays <- getProc ctx ["glDeleteVertexArrays"; "glDeleteVertexArrays"; "glDeleteVertexArraysAPPLE"; "glDeleteVertexArraysOES"]
        res.glDrawArraysInstanced <- getProc ctx ["glDrawArraysInstanced"; "glDrawArraysInstanced"; "glDrawArraysInstancedANGLE"; "glDrawArraysInstancedARB"; "glDrawArraysInstancedEXT"; "glDrawArraysInstancedNV"]
        res.glDrawBuffers <- getProc ctx ["glDrawBuffers"; "glDrawBuffers"; "glDrawBuffersARB"; "glDrawBuffersATI"; "glDrawBuffersEXT"]
        res.glDrawElementsInstanced <- getProc ctx ["glDrawElementsInstanced"; "glDrawElementsInstanced"; "glDrawElementsInstancedANGLE"; "glDrawElementsInstancedARB"; "glDrawElementsInstancedEXT"; "glDrawElementsInstancedNV"]
        res.glDrawRangeElements <- getProc ctx ["glDrawRangeElements"; "glDrawRangeElements"; "glDrawRangeElementsEXT"]
        res.glEndQuery <- getProc ctx ["glEndQuery"; "glEndQuery"; "glEndQueryARB"]
        res.glEndTransformFeedback <- getProc ctx ["glEndTransformFeedback"; "glEndTransformFeedback"; "glEndTransformFeedbackEXT"; "glEndTransformFeedbackNV"]
        res.glFenceSync <- getProc ctx ["glFenceSync"; "glFenceSync"; "glFenceSyncAPPLE"]
        res.glFramebufferTextureLayer <- getProc ctx ["glFramebufferTextureLayer"; "glFramebufferTextureLayer"; "glFramebufferTextureLayerARB"; "glFramebufferTextureLayerEXT"]
        res.glGenQueries <- getProc ctx ["glGenQueries"; "glGenQueries"; "glGenQueriesARB"]
        res.glGenSamplers <- getProc ctx ["glGenSamplers"]
        res.glGenTransformFeedbacks <- getProc ctx ["glGenTransformFeedbacks"; "glGenTransformFeedbacks"; "glGenTransformFeedbacksNV"]
        res.glGenVertexArrays <- getProc ctx ["glGenVertexArrays"; "glGenVertexArrays"; "glGenVertexArraysAPPLE"; "glGenVertexArraysOES"]
        res.glGetActiveUniformBlockiv <- getProc ctx ["glGetActiveUniformBlockiv"]
        res.glGetActiveUniformBlockName <- getProc ctx ["glGetActiveUniformBlockName"]
        res.glGetActiveUniformsiv <- getProc ctx ["glGetActiveUniformsiv"]
        res.glGetBufferParameteri64v <- getProc ctx ["glGetBufferParameteri64v"]
        res.glGetFragDataLocation <- getProc ctx ["glGetFragDataLocation"; "glGetFragDataLocation"; "glGetFragDataLocationEXT"]
        res.glGetIntegeri_v <- getProc ctx ["glGetIntegeri_v"; "glGetIntegerIndexedvEXT"; "glGetIntegeri_v"]
        res.glGetInteger64v <- getProc ctx ["glGetInteger64v"; "glGetInteger64v"; "glGetInteger64vAPPLE"; "glGetInteger64vEXT"]
        res.glGetInteger64i_v <- getProc ctx ["glGetInteger64i_v"]
        res.glGetInternalformativ <- getProc ctx ["glGetInternalformativ"]
        res.glGetProgramBinary <- getProc ctx ["glGetProgramBinary"; "glGetProgramBinary"; "glGetProgramBinaryOES"]
        res.glGetQueryiv <- getProc ctx ["glGetQueryiv"; "glGetQueryiv"; "glGetQueryivARB"]
        res.glGetQueryObjectuiv <- getProc ctx ["glGetQueryObjectuiv"; "glGetQueryObjectuiv"; "glGetQueryObjectuivARB"]
        res.glGetSamplerParameteriv <- getProc ctx ["glGetSamplerParameteriv"]
        res.glGetSamplerParameterfv <- getProc ctx ["glGetSamplerParameterfv"]
        res.glGetStringi <- getProc ctx ["glGetStringi"]
        res.glGetSynciv <- getProc ctx ["glGetSynciv"; "glGetSynciv"; "glGetSyncivAPPLE"]
        res.glGetTransformFeedbackVarying <- getProc ctx ["glGetTransformFeedbackVarying"; "glGetTransformFeedbackVarying"; "glGetTransformFeedbackVaryingEXT"]
        res.glGetUniformuiv <- getProc ctx ["glGetUniformuiv"; "glGetUniformuiv"; "glGetUniformuivEXT"]
        res.glGetUniformBlockIndex <- getProc ctx ["glGetUniformBlockIndex"]
        res.glGetUniformIndices <- getProc ctx ["glGetUniformIndices"]
        res.glGetVertexAttribIiv <- getProc ctx ["glGetVertexAttribIiv"; "glGetVertexAttribIiv"; "glGetVertexAttribIivEXT"]
        res.glGetVertexAttribIuiv <- getProc ctx ["glGetVertexAttribIuiv"; "glGetVertexAttribIuiv"; "glGetVertexAttribIuivEXT"]
        res.glInvalidateFramebuffer <- getProc ctx ["glInvalidateFramebuffer"]
        res.glInvalidateSubFramebuffer <- getProc ctx ["glInvalidateSubFramebuffer"]
        res.glIsQuery <- getProc ctx ["glIsQuery"; "glIsQuery"; "glIsQueryARB"]
        res.glIsSampler <- getProc ctx ["glIsSampler"]
        res.glIsSync <- getProc ctx ["glIsSync"; "glIsSync"; "glIsSyncAPPLE"]
        res.glIsTransformFeedback <- getProc ctx ["glIsTransformFeedback"; "glIsTransformFeedback"; "glIsTransformFeedbackNV"]
        res.glIsVertexArray <- getProc ctx ["glIsVertexArray"; "glIsVertexArray"; "glIsVertexArrayAPPLE"; "glIsVertexArrayOES"]
        res.glPauseTransformFeedback <- getProc ctx ["glPauseTransformFeedback"; "glPauseTransformFeedback"; "glPauseTransformFeedbackNV"]
        res.glProgramBinary <- getProc ctx ["glProgramBinary"; "glProgramBinary"; "glProgramBinaryOES"]
        res.glProgramParameteri <- getProc ctx ["glProgramParameteri"; "glProgramParameteri"; "glProgramParameteriARB"; "glProgramParameteriEXT"]
        res.glReadBuffer <- getProc ctx ["glReadBuffer"]
        res.glRenderbufferStorageMultisample <- getProc ctx ["glRenderbufferStorageMultisample"; "glRenderbufferStorageMultisample"; "glRenderbufferStorageMultisampleEXT"; "glRenderbufferStorageMultisampleNV"]
        res.glResumeTransformFeedback <- getProc ctx ["glResumeTransformFeedback"; "glResumeTransformFeedback"; "glResumeTransformFeedbackNV"]
        res.glSamplerParameteri <- getProc ctx ["glSamplerParameteri"]
        res.glSamplerParameteriv <- getProc ctx ["glSamplerParameteriv"]
        res.glSamplerParameterf <- getProc ctx ["glSamplerParameterf"]
        res.glSamplerParameterfv <- getProc ctx ["glSamplerParameterfv"]
        res.glTexImage3D <- getProc ctx ["glTexImage3D"; "glTexImage3D"; "glTexImage3DEXT"]
        res.glTexStorage2D <- getProc ctx ["glTexStorage2D"; "glTexStorage2D"; "glTexStorage2DEXT"]
        res.glTexStorage3D <- getProc ctx ["glTexStorage3D"; "glTexStorage3D"; "glTexStorage3DEXT"]
        res.glTexSubImage3D <- getProc ctx ["glTexSubImage3D"; "glTexSubImage3D"; "glTexSubImage3DEXT"]
        res.glTransformFeedbackVaryings <- getProc ctx ["glTransformFeedbackVaryings"; "glTransformFeedbackVaryings"; "glTransformFeedbackVaryingsEXT"]
        res.glUniform1ui <- getProc ctx ["glUniform1ui"; "glUniform1ui"; "glUniform1uiEXT"]
        res.glUniform1uiv <- getProc ctx ["glUniform1uiv"; "glUniform1uiv"; "glUniform1uivEXT"]
        res.glUniform2ui <- getProc ctx ["glUniform2ui"; "glUniform2ui"; "glUniform2uiEXT"]
        res.glUniform2uiv <- getProc ctx ["glUniform2uiv"; "glUniform2uiv"; "glUniform2uivEXT"]
        res.glUniform3ui <- getProc ctx ["glUniform3ui"; "glUniform3ui"; "glUniform3uiEXT"]
        res.glUniform3uiv <- getProc ctx ["glUniform3uiv"; "glUniform3uiv"; "glUniform3uivEXT"]
        res.glUniform4ui <- getProc ctx ["glUniform4ui"; "glUniform4ui"; "glUniform4uiEXT"]
        res.glUniform4uiv <- getProc ctx ["glUniform4uiv"; "glUniform4uiv"; "glUniform4uivEXT"]
        res.glUniformBlockBinding <- getProc ctx ["glUniformBlockBinding"]
        res.glUniformMatrix2x3fv <- getProc ctx ["glUniformMatrix2x3fv"; "glUniformMatrix2x3fv"; "glUniformMatrix2x3fvNV"]
        res.glUniformMatrix2x4fv <- getProc ctx ["glUniformMatrix2x4fv"; "glUniformMatrix2x4fv"; "glUniformMatrix2x4fvNV"]
        res.glUniformMatrix3x2fv <- getProc ctx ["glUniformMatrix3x2fv"; "glUniformMatrix3x2fv"; "glUniformMatrix3x2fvNV"]
        res.glUniformMatrix3x4fv <- getProc ctx ["glUniformMatrix3x4fv"; "glUniformMatrix3x4fv"; "glUniformMatrix3x4fvNV"]
        res.glUniformMatrix4x2fv <- getProc ctx ["glUniformMatrix4x2fv"; "glUniformMatrix4x2fv"; "glUniformMatrix4x2fvNV"]
        res.glUniformMatrix4x3fv <- getProc ctx ["glUniformMatrix4x3fv"; "glUniformMatrix4x3fv"; "glUniformMatrix4x3fvNV"]
        res.glVertexAttribDivisor <- getProc ctx ["glVertexAttribDivisor"; "glVertexAttribDivisor"; "glVertexAttribDivisorANGLE"; "glVertexAttribDivisorARB"; "glVertexAttribDivisorEXT"; "glVertexAttribDivisorNV"]
        res.glVertexAttribI4i <- getProc ctx ["glVertexAttribI4i"; "glVertexAttribI4i"; "glVertexAttribI4iEXT"]
        res.glVertexAttribI4ui <- getProc ctx ["glVertexAttribI4ui"; "glVertexAttribI4ui"; "glVertexAttribI4uiEXT"]
        res.glVertexAttribI4iv <- getProc ctx ["glVertexAttribI4iv"; "glVertexAttribI4iv"; "glVertexAttribI4ivEXT"]
        res.glVertexAttribI4uiv <- getProc ctx ["glVertexAttribI4uiv"; "glVertexAttribI4uiv"; "glVertexAttribI4uivEXT"]
        res.glVertexAttribIPointer <- getProc ctx ["glVertexAttribIPointer"; "glVertexAttribIPointer"; "glVertexAttribIPointerEXT"]
        res.glWaitSync <- getProc ctx ["glWaitSync"; "glWaitSync"; "glWaitSyncAPPLE"]
        res.glActiveTexture <- getProc ctx ["glActiveTexture"; "glActiveTexture"; "glActiveTextureARB"]
        res.glAttachShader <- getProc ctx ["glAttachShader"; "glAttachObjectARB"; "glAttachShader"]
        res.glBindAttribLocation <- getProc ctx ["glBindAttribLocation"; "glBindAttribLocation"; "glBindAttribLocationARB"]
        res.glBindBuffer <- getProc ctx ["glBindBuffer"; "glBindBuffer"; "glBindBufferARB"]
        res.glBindFramebuffer <- getProc ctx ["glBindFramebuffer"]
        res.glBindRenderbuffer <- getProc ctx ["glBindRenderbuffer"]
        res.glBindTexture <- getProc ctx ["glBindTexture"; "glBindTexture"; "glBindTextureEXT"]
        res.glBlendColor <- getProc ctx ["glBlendColor"; "glBlendColor"; "glBlendColorEXT"]
        res.glBlendEquation <- getProc ctx ["glBlendEquation"; "glBlendEquation"; "glBlendEquationEXT"]
        res.glBlendEquationSeparate <- getProc ctx ["glBlendEquationSeparate"; "glBlendEquationSeparate"; "glBlendEquationSeparateEXT"]
        res.glBlendFunc <- getProc ctx ["glBlendFunc"]
        res.glBlendFuncSeparate <- getProc ctx ["glBlendFuncSeparate"; "glBlendFuncSeparate"; "glBlendFuncSeparateEXT"; "glBlendFuncSeparateINGR"]
        res.glBufferData <- getProc ctx ["glBufferData"; "glBufferData"; "glBufferDataARB"]
        res.glBufferSubData <- getProc ctx ["glBufferSubData"; "glBufferSubData"; "glBufferSubDataARB"]
        res.glCheckFramebufferStatus <- getProc ctx ["glCheckFramebufferStatus"; "glCheckFramebufferStatus"; "glCheckFramebufferStatusEXT"]
        res.glClear <- getProc ctx ["glClear"]
        res.glClearColor <- getProc ctx ["glClearColor"]
        res.glClearDepthf <- getProc ctx ["glClearDepthf"; "glClearDepthf"; "glClearDepthfOES"]
        res.glClearStencil <- getProc ctx ["glClearStencil"]
        res.glColorMask <- getProc ctx ["glColorMask"]
        res.glCompileShader <- getProc ctx ["glCompileShader"; "glCompileShader"; "glCompileShaderARB"]
        res.glCompressedTexImage2D <- getProc ctx ["glCompressedTexImage2D"; "glCompressedTexImage2D"; "glCompressedTexImage2DARB"]
        res.glCompressedTexSubImage2D <- getProc ctx ["glCompressedTexSubImage2D"; "glCompressedTexSubImage2D"; "glCompressedTexSubImage2DARB"]
        res.glCopyTexImage2D <- getProc ctx ["glCopyTexImage2D"; "glCopyTexImage2D"; "glCopyTexImage2DEXT"]
        res.glCopyTexSubImage2D <- getProc ctx ["glCopyTexSubImage2D"; "glCopyTexSubImage2D"; "glCopyTexSubImage2DEXT"]
        res.glCreateProgram <- getProc ctx ["glCreateProgram"; "glCreateProgram"; "glCreateProgramObjectARB"]
        res.glCreateShader <- getProc ctx ["glCreateShader"; "glCreateShader"; "glCreateShaderObjectARB"]
        res.glCullFace <- getProc ctx ["glCullFace"]
        res.glDeleteBuffers <- getProc ctx ["glDeleteBuffers"; "glDeleteBuffers"; "glDeleteBuffersARB"]
        res.glDeleteFramebuffers <- getProc ctx ["glDeleteFramebuffers"; "glDeleteFramebuffers"; "glDeleteFramebuffersEXT"]
        res.glDeleteProgram <- getProc ctx ["glDeleteProgram"]
        res.glDeleteRenderbuffers <- getProc ctx ["glDeleteRenderbuffers"; "glDeleteRenderbuffers"; "glDeleteRenderbuffersEXT"]
        res.glDeleteShader <- getProc ctx ["glDeleteShader"]
        res.glDeleteTextures <- getProc ctx ["glDeleteTextures"]
        res.glDepthFunc <- getProc ctx ["glDepthFunc"]
        res.glDepthMask <- getProc ctx ["glDepthMask"]
        res.glDepthRangef <- getProc ctx ["glDepthRangef"; "glDepthRangef"; "glDepthRangefOES"]
        res.glDetachShader <- getProc ctx ["glDetachShader"; "glDetachObjectARB"; "glDetachShader"]
        res.glDisable <- getProc ctx ["glDisable"]
        res.glDisableVertexAttribArray <- getProc ctx ["glDisableVertexAttribArray"; "glDisableVertexAttribArray"; "glDisableVertexAttribArrayARB"]
        res.glDrawArrays <- getProc ctx ["glDrawArrays"; "glDrawArrays"; "glDrawArraysEXT"]
        res.glDrawElements <- getProc ctx ["glDrawElements"]
        res.glEnable <- getProc ctx ["glEnable"]
        res.glEnableVertexAttribArray <- getProc ctx ["glEnableVertexAttribArray"; "glEnableVertexAttribArray"; "glEnableVertexAttribArrayARB"]
        res.glFinish <- getProc ctx ["glFinish"]
        res.glFlush <- getProc ctx ["glFlush"]
        res.glFramebufferRenderbuffer <- getProc ctx ["glFramebufferRenderbuffer"; "glFramebufferRenderbuffer"; "glFramebufferRenderbufferEXT"]
        res.glFramebufferTexture2D <- getProc ctx ["glFramebufferTexture2D"; "glFramebufferTexture2D"; "glFramebufferTexture2DEXT"]
        res.glFrontFace <- getProc ctx ["glFrontFace"]
        res.glGenBuffers <- getProc ctx ["glGenBuffers"; "glGenBuffers"; "glGenBuffersARB"]
        res.glGenerateMipmap <- getProc ctx ["glGenerateMipmap"; "glGenerateMipmap"; "glGenerateMipmapEXT"]
        res.glGenFramebuffers <- getProc ctx ["glGenFramebuffers"; "glGenFramebuffers"; "glGenFramebuffersEXT"]
        res.glGenRenderbuffers <- getProc ctx ["glGenRenderbuffers"; "glGenRenderbuffers"; "glGenRenderbuffersEXT"]
        res.glGenTextures <- getProc ctx ["glGenTextures"]
        res.glGetActiveAttrib <- getProc ctx ["glGetActiveAttrib"; "glGetActiveAttrib"; "glGetActiveAttribARB"]
        res.glGetActiveUniform <- getProc ctx ["glGetActiveUniform"; "glGetActiveUniform"; "glGetActiveUniformARB"]
        res.glGetAttachedShaders <- getProc ctx ["glGetAttachedShaders"]
        res.glGetAttribLocation <- getProc ctx ["glGetAttribLocation"; "glGetAttribLocation"; "glGetAttribLocationARB"]
        res.glGetBooleanv <- getProc ctx ["glGetBooleanv"]
        res.glGetBufferParameteriv <- getProc ctx ["glGetBufferParameteriv"; "glGetBufferParameteriv"; "glGetBufferParameterivARB"]
        res.glGetError <- getProc ctx ["glGetError"]
        res.glGetFloatv <- getProc ctx ["glGetFloatv"]
        res.glGetFramebufferAttachmentParameteriv <- getProc ctx ["glGetFramebufferAttachmentParameteriv"; "glGetFramebufferAttachmentParameteriv"; "glGetFramebufferAttachmentParameterivEXT"]
        res.glGetIntegerv <- getProc ctx ["glGetIntegerv"]
        res.glGetProgramiv <- getProc ctx ["glGetProgramiv"]
        res.glGetProgramInfoLog <- getProc ctx ["glGetProgramInfoLog"]
        res.glGetRenderbufferParameteriv <- getProc ctx ["glGetRenderbufferParameteriv"; "glGetRenderbufferParameteriv"; "glGetRenderbufferParameterivEXT"]
        res.glGetShaderiv <- getProc ctx ["glGetShaderiv"]
        res.glGetShaderInfoLog <- getProc ctx ["glGetShaderInfoLog"]
        res.glGetShaderPrecisionFormat <- getProc ctx ["glGetShaderPrecisionFormat"]
        res.glGetShaderSource <- getProc ctx ["glGetShaderSource"; "glGetShaderSource"; "glGetShaderSourceARB"]
        res.glGetString <- getProc ctx ["glGetString"]
        res.glGetTexParameterfv <- getProc ctx ["glGetTexParameterfv"]
        res.glGetTexParameteriv <- getProc ctx ["glGetTexParameteriv"]
        res.glGetUniformfv <- getProc ctx ["glGetUniformfv"; "glGetUniformfv"; "glGetUniformfvARB"]
        res.glGetUniformiv <- getProc ctx ["glGetUniformiv"; "glGetUniformiv"; "glGetUniformivARB"]
        res.glGetUniformLocation <- getProc ctx ["glGetUniformLocation"; "glGetUniformLocation"; "glGetUniformLocationARB"]
        res.glGetVertexAttribfv <- getProc ctx ["glGetVertexAttribfv"; "glGetVertexAttribfv"; "glGetVertexAttribfvARB"; "glGetVertexAttribfvNV"]
        res.glGetVertexAttribiv <- getProc ctx ["glGetVertexAttribiv"; "glGetVertexAttribiv"; "glGetVertexAttribivARB"; "glGetVertexAttribivNV"]
        res.glGetVertexAttribPointerv <- getProc ctx ["glGetVertexAttribPointerv"; "glGetVertexAttribPointerv"; "glGetVertexAttribPointervARB"; "glGetVertexAttribPointervNV"]
        res.glHint <- getProc ctx ["glHint"]
        res.glIsBuffer <- getProc ctx ["glIsBuffer"; "glIsBuffer"; "glIsBufferARB"]
        res.glIsEnabled <- getProc ctx ["glIsEnabled"]
        res.glIsFramebuffer <- getProc ctx ["glIsFramebuffer"; "glIsFramebuffer"; "glIsFramebufferEXT"]
        res.glIsProgram <- getProc ctx ["glIsProgram"]
        res.glIsRenderbuffer <- getProc ctx ["glIsRenderbuffer"; "glIsRenderbuffer"; "glIsRenderbufferEXT"]
        res.glIsShader <- getProc ctx ["glIsShader"]
        res.glIsTexture <- getProc ctx ["glIsTexture"]
        res.glLineWidth <- getProc ctx ["glLineWidth"]
        res.glLinkProgram <- getProc ctx ["glLinkProgram"; "glLinkProgram"; "glLinkProgramARB"]
        res.glPixelStorei <- getProc ctx ["glPixelStorei"]
        res.glPolygonOffset <- getProc ctx ["glPolygonOffset"]
        res.glReadPixels <- getProc ctx ["glReadPixels"]
        res.glReleaseShaderCompiler <- getProc ctx ["glReleaseShaderCompiler"]
        res.glRenderbufferStorage <- getProc ctx ["glRenderbufferStorage"; "glRenderbufferStorage"; "glRenderbufferStorageEXT"]
        res.glSampleCoverage <- getProc ctx ["glSampleCoverage"; "glSampleCoverage"; "glSampleCoverageARB"]
        res.glScissor <- getProc ctx ["glScissor"]
        res.glShaderBinary <- getProc ctx ["glShaderBinary"]
        res.glShaderSource <- getProc ctx ["glShaderSource"; "glShaderSource"; "glShaderSourceARB"]
        res.glStencilFunc <- getProc ctx ["glStencilFunc"]
        res.glStencilFuncSeparate <- getProc ctx ["glStencilFuncSeparate"]
        res.glStencilMask <- getProc ctx ["glStencilMask"]
        res.glStencilMaskSeparate <- getProc ctx ["glStencilMaskSeparate"]
        res.glStencilOp <- getProc ctx ["glStencilOp"]
        res.glStencilOpSeparate <- getProc ctx ["glStencilOpSeparate"; "glStencilOpSeparate"; "glStencilOpSeparateATI"]
        res.glTexImage2D <- getProc ctx ["glTexImage2D"]
        res.glTexParameterf <- getProc ctx ["glTexParameterf"]
        res.glTexParameterfv <- getProc ctx ["glTexParameterfv"]
        res.glTexParameteri <- getProc ctx ["glTexParameteri"]
        res.glTexParameteriv <- getProc ctx ["glTexParameteriv"]
        res.glTexSubImage2D <- getProc ctx ["glTexSubImage2D"; "glTexSubImage2D"; "glTexSubImage2DEXT"]
        res.glUniform1f <- getProc ctx ["glUniform1f"; "glUniform1f"; "glUniform1fARB"]
        res.glUniform1fv <- getProc ctx ["glUniform1fv"; "glUniform1fv"; "glUniform1fvARB"]
        res.glUniform1i <- getProc ctx ["glUniform1i"; "glUniform1i"; "glUniform1iARB"]
        res.glUniform1iv <- getProc ctx ["glUniform1iv"; "glUniform1iv"; "glUniform1ivARB"]
        res.glUniform2f <- getProc ctx ["glUniform2f"; "glUniform2f"; "glUniform2fARB"]
        res.glUniform2fv <- getProc ctx ["glUniform2fv"; "glUniform2fv"; "glUniform2fvARB"]
        res.glUniform2i <- getProc ctx ["glUniform2i"; "glUniform2i"; "glUniform2iARB"]
        res.glUniform2iv <- getProc ctx ["glUniform2iv"; "glUniform2iv"; "glUniform2ivARB"]
        res.glUniform3f <- getProc ctx ["glUniform3f"; "glUniform3f"; "glUniform3fARB"]
        res.glUniform3fv <- getProc ctx ["glUniform3fv"; "glUniform3fv"; "glUniform3fvARB"]
        res.glUniform3i <- getProc ctx ["glUniform3i"; "glUniform3i"; "glUniform3iARB"]
        res.glUniform3iv <- getProc ctx ["glUniform3iv"; "glUniform3iv"; "glUniform3ivARB"]
        res.glUniform4f <- getProc ctx ["glUniform4f"; "glUniform4f"; "glUniform4fARB"]
        res.glUniform4fv <- getProc ctx ["glUniform4fv"; "glUniform4fv"; "glUniform4fvARB"]
        res.glUniform4i <- getProc ctx ["glUniform4i"; "glUniform4i"; "glUniform4iARB"]
        res.glUniform4iv <- getProc ctx ["glUniform4iv"; "glUniform4iv"; "glUniform4ivARB"]
        res.glUniformMatrix2fv <- getProc ctx ["glUniformMatrix2fv"; "glUniformMatrix2fv"; "glUniformMatrix2fvARB"]
        res.glUniformMatrix3fv <- getProc ctx ["glUniformMatrix3fv"; "glUniformMatrix3fv"; "glUniformMatrix3fvARB"]
        res.glUniformMatrix4fv <- getProc ctx ["glUniformMatrix4fv"; "glUniformMatrix4fv"; "glUniformMatrix4fvARB"]
        res.glUseProgram <- getProc ctx ["glUseProgram"; "glUseProgram"; "glUseProgramObjectARB"]
        res.glValidateProgram <- getProc ctx ["glValidateProgram"; "glValidateProgram"; "glValidateProgramARB"]
        res.glVertexAttrib1f <- getProc ctx ["glVertexAttrib1f"; "glVertexAttrib1f"; "glVertexAttrib1fARB"; "glVertexAttrib1fNV"]
        res.glVertexAttrib1fv <- getProc ctx ["glVertexAttrib1fv"; "glVertexAttrib1fv"; "glVertexAttrib1fvARB"; "glVertexAttrib1fvNV"]
        res.glVertexAttrib2f <- getProc ctx ["glVertexAttrib2f"; "glVertexAttrib2f"; "glVertexAttrib2fARB"; "glVertexAttrib2fNV"]
        res.glVertexAttrib2fv <- getProc ctx ["glVertexAttrib2fv"; "glVertexAttrib2fv"; "glVertexAttrib2fvARB"; "glVertexAttrib2fvNV"]
        res.glVertexAttrib3f <- getProc ctx ["glVertexAttrib3f"; "glVertexAttrib3f"; "glVertexAttrib3fARB"; "glVertexAttrib3fNV"]
        res.glVertexAttrib3fv <- getProc ctx ["glVertexAttrib3fv"; "glVertexAttrib3fv"; "glVertexAttrib3fvARB"; "glVertexAttrib3fvNV"]
        res.glVertexAttrib4f <- getProc ctx ["glVertexAttrib4f"; "glVertexAttrib4f"; "glVertexAttrib4fARB"; "glVertexAttrib4fNV"]
        res.glVertexAttrib4fv <- getProc ctx ["glVertexAttrib4fv"; "glVertexAttrib4fv"; "glVertexAttrib4fvARB"; "glVertexAttrib4fvNV"]
        res.glVertexAttribPointer <- getProc ctx ["glVertexAttribPointer"; "glVertexAttribPointer"; "glVertexAttribPointerARB"]
        res.glViewport <- getProc ctx ["glViewport"]
        res.glGetBufferSubData <- getProc ctx ["glGetBufferSubData"; "glGetBufferSubData"; "glGetBufferSubDataARB"]
        res.glMultiDrawArraysIndirect <- getProc ctx ["glMultiDrawArraysIndirect"; "glMultiDrawArraysIndirect"; "glMultiDrawArraysIndirectAMD"; "glMultiDrawArraysIndirectEXT"]
        res.glMultiDrawArrays <- getProc ctx ["glMultiDrawArrays"; "glMultiDrawArrays"; "glMultiDrawArraysEXT"]
        res.glMultiDrawElementsIndirect <- getProc ctx ["glMultiDrawElementsIndirect"; "glMultiDrawElementsIndirect"; "glMultiDrawElementsIndirectAMD"; "glMultiDrawElementsIndirectEXT"]
        res.glMultiDrawElements <- getProc ctx ["glMultiDrawElements"; "glMultiDrawElements"; "glMultiDrawElementsEXT"]
        res.glCommit <- getProc ctx ["glCommit"]
        res.glTexSubImage2DJSImage <- getProc ctx ["glTexSubImage2DJSImage"]
        res

    let get (ctx : Silk.NET.Core.Contexts.INativeContext) =
        cache.GetOrAdd(ctx, fun ctx ->
            load ctx
        )

module internal GLDelegates =
    [<SuppressUnmanagedCodeSecurity>]
    type glBeginQueryDelegate = delegate of ``target`` : QueryTarget * ``id`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBeginTransformFeedbackDelegate = delegate of ``primitiveMode`` : PrimitiveType -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindBufferBaseDelegate = delegate of ``target`` : BufferTargetARB * ``index`` : uint32 * ``buffer`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindBufferRangeDelegate = delegate of ``target`` : BufferTargetARB * ``index`` : uint32 * ``buffer`` : uint32 * ``offset`` : nativeint * ``size`` : unativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindSamplerDelegate = delegate of ``unit`` : uint32 * ``sampler`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindTransformFeedbackDelegate = delegate of ``target`` : BindTransformFeedbackTarget * ``id`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindVertexArrayDelegate = delegate of ``array`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlitFramebufferDelegate = delegate of ``srcX0`` : int * ``srcY0`` : int * ``srcX1`` : int * ``srcY1`` : int * ``dstX0`` : int * ``dstY0`` : int * ``dstX1`` : int * ``dstY1`` : int * ``mask`` : ClearBufferMask * ``filter`` : BlitFramebufferFilter -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearBufferivDelegate = delegate of ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearBufferuivDelegate = delegate of ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearBufferfvDelegate = delegate of ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearBufferfiDelegate = delegate of ``buffer`` : BufferKind * ``drawbuffer`` : int * ``depth`` : float32 * ``stencil`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClientWaitSyncDelegate = delegate of ``sync`` : nativeint * ``flags`` : SyncObjectMask * ``timeout`` : uint64 -> GLEnum
    [<SuppressUnmanagedCodeSecurity>]
    type glCompressedTexImage3DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``border`` : int * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCompressedTexSubImage3DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``format`` : InternalFormat * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCopyBufferSubDataDelegate = delegate of ``readTarget`` : CopyBufferSubDataTarget * ``writeTarget`` : CopyBufferSubDataTarget * ``readOffset`` : nativeint * ``writeOffset`` : nativeint * ``size`` : unativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCopyTexSubImage3DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteQueriesDelegate = delegate of ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteSamplersDelegate = delegate of ``count`` : uint32 * ``samplers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteSyncDelegate = delegate of ``sync`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteTransformFeedbacksDelegate = delegate of ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteVertexArraysDelegate = delegate of ``n`` : uint32 * ``arrays`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawArraysInstancedDelegate = delegate of ``mode`` : PrimitiveType * ``first`` : int * ``count`` : uint32 * ``instancecount`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawBuffersDelegate = delegate of ``n`` : uint32 * ``bufs`` : nativeptr<GLEnum> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawElementsInstancedDelegate = delegate of ``mode`` : PrimitiveType * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint * ``instancecount`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawRangeElementsDelegate = delegate of ``mode`` : PrimitiveType * ``start`` : uint32 * ``end`` : uint32 * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glEndQueryDelegate = delegate of ``target`` : QueryTarget -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glEndTransformFeedbackDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFenceSyncDelegate = delegate of ``condition`` : SyncCondition * ``flags`` : SyncBehaviorFlags -> nativeint
    [<SuppressUnmanagedCodeSecurity>]
    type glFramebufferTextureLayerDelegate = delegate of ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``texture`` : uint32 * ``level`` : int * ``layer`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenQueriesDelegate = delegate of ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenSamplersDelegate = delegate of ``count`` : uint32 * ``samplers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenTransformFeedbacksDelegate = delegate of ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenVertexArraysDelegate = delegate of ``n`` : uint32 * ``arrays`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetActiveUniformBlockivDelegate = delegate of ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``pname`` : UniformBlockPName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetActiveUniformBlockNameDelegate = delegate of ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``uniformBlockName`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetActiveUniformsivDelegate = delegate of ``program`` : uint32 * ``uniformCount`` : uint32 * ``uniformIndices`` : nativeptr<uint32> * ``pname`` : UniformPName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetBufferParameteri64vDelegate = delegate of ``target`` : BufferTargetARB * ``pname`` : BufferPNameARB * ``params`` : nativeptr<int64> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetFragDataLocationDelegate = delegate of ``program`` : uint32 * ``name`` : nativeptr<uint8> -> int
    [<SuppressUnmanagedCodeSecurity>]
    type glGetIntegeri_vDelegate = delegate of ``target`` : GetPName * ``index`` : uint32 * ``data`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetInteger64vDelegate = delegate of ``pname`` : GetPName * ``data`` : nativeptr<int64> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetInteger64i_vDelegate = delegate of ``target`` : GetPName * ``index`` : uint32 * ``data`` : nativeptr<int64> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetInternalformativDelegate = delegate of ``target`` : TextureTarget * ``internalformat`` : InternalFormat * ``pname`` : InternalFormatPName * ``count`` : uint32 * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetProgramBinaryDelegate = delegate of ``program`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``binaryFormat`` : nativeptr<GLEnum> * ``binary`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetQueryivDelegate = delegate of ``target`` : QueryTarget * ``pname`` : QueryParameterName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetQueryObjectuivDelegate = delegate of ``id`` : uint32 * ``pname`` : QueryObjectParameterName * ``params`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetSamplerParameterivDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetSamplerParameterfvDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``params`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetStringiDelegate = delegate of ``name`` : StringName * ``index`` : uint32 -> nativeptr<uint8>
    [<SuppressUnmanagedCodeSecurity>]
    type glGetSyncivDelegate = delegate of ``sync`` : nativeint * ``pname`` : SyncParameterName * ``count`` : uint32 * ``length`` : nativeptr<uint32> * ``values`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetTransformFeedbackVaryingDelegate = delegate of ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<uint32> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformuivDelegate = delegate of ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformBlockIndexDelegate = delegate of ``program`` : uint32 * ``uniformBlockName`` : nativeptr<uint8> -> uint32
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformIndicesDelegate = delegate of ``program`` : uint32 * ``uniformCount`` : uint32 * ``uniformNames`` : nativeptr<nativeptr<uint8>> * ``uniformIndices`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetVertexAttribIivDelegate = delegate of ``index`` : uint32 * ``pname`` : VertexAttribEnum * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetVertexAttribIuivDelegate = delegate of ``index`` : uint32 * ``pname`` : VertexAttribEnum * ``params`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glInvalidateFramebufferDelegate = delegate of ``target`` : FramebufferTarget * ``numAttachments`` : uint32 * ``attachments`` : nativeptr<GLEnum> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glInvalidateSubFramebufferDelegate = delegate of ``target`` : FramebufferTarget * ``numAttachments`` : uint32 * ``attachments`` : nativeptr<GLEnum> * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glIsQueryDelegate = delegate of ``id`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsSamplerDelegate = delegate of ``sampler`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsSyncDelegate = delegate of ``sync`` : nativeint -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsTransformFeedbackDelegate = delegate of ``id`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsVertexArrayDelegate = delegate of ``array`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glPauseTransformFeedbackDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glProgramBinaryDelegate = delegate of ``program`` : uint32 * ``binaryFormat`` : GLEnum * ``binary`` : nativeint * ``length`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glProgramParameteriDelegate = delegate of ``program`` : uint32 * ``pname`` : ProgramParameterPName * ``value`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glReadBufferDelegate = delegate of ``src`` : ReadBufferMode -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glRenderbufferStorageMultisampleDelegate = delegate of ``target`` : RenderbufferTarget * ``samples`` : uint32 * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glResumeTransformFeedbackDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glSamplerParameteriDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``param`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glSamplerParameterivDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``param`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glSamplerParameterfDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``param`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glSamplerParameterfvDelegate = delegate of ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``param`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexImage3DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``border`` : int * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexStorage2DDelegate = delegate of ``target`` : TextureTarget * ``levels`` : uint32 * ``internalformat`` : SizedInternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexStorage3DDelegate = delegate of ``target`` : TextureTarget * ``levels`` : uint32 * ``internalformat`` : SizedInternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexSubImage3DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTransformFeedbackVaryingsDelegate = delegate of ``program`` : uint32 * ``count`` : uint32 * ``varyings`` : nativeptr<nativeptr<uint8>> * ``bufferMode`` : TransformFeedbackBufferMode -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1uiDelegate = delegate of ``location`` : int * ``v0`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1uivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2uiDelegate = delegate of ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2uivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3uiDelegate = delegate of ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 * ``v2`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3uivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4uiDelegate = delegate of ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 * ``v2`` : uint32 * ``v3`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4uivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformBlockBindingDelegate = delegate of ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``uniformBlockBinding`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix2x3fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix2x4fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix3x2fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix3x4fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix4x2fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix4x3fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribDivisorDelegate = delegate of ``index`` : uint32 * ``divisor`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribI4iDelegate = delegate of ``index`` : uint32 * ``x`` : int * ``y`` : int * ``z`` : int * ``w`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribI4uiDelegate = delegate of ``index`` : uint32 * ``x`` : uint32 * ``y`` : uint32 * ``z`` : uint32 * ``w`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribI4ivDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribI4uivDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribIPointerDelegate = delegate of ``index`` : uint32 * ``size`` : int * ``type`` : VertexAttribIType * ``stride`` : uint32 * ``pointer`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glWaitSyncDelegate = delegate of ``sync`` : nativeint * ``flags`` : SyncBehaviorFlags * ``timeout`` : uint64 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glActiveTextureDelegate = delegate of ``texture`` : TextureUnit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glAttachShaderDelegate = delegate of ``program`` : uint32 * ``shader`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindAttribLocationDelegate = delegate of ``program`` : uint32 * ``index`` : uint32 * ``name`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindBufferDelegate = delegate of ``target`` : BufferTargetARB * ``buffer`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindFramebufferDelegate = delegate of ``target`` : FramebufferTarget * ``framebuffer`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindRenderbufferDelegate = delegate of ``target`` : RenderbufferTarget * ``renderbuffer`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBindTextureDelegate = delegate of ``target`` : TextureTarget * ``texture`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlendColorDelegate = delegate of ``red`` : float32 * ``green`` : float32 * ``blue`` : float32 * ``alpha`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlendEquationDelegate = delegate of ``mode`` : BlendEquationModeEXT -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlendEquationSeparateDelegate = delegate of ``modeRGB`` : BlendEquationModeEXT * ``modeAlpha`` : BlendEquationModeEXT -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlendFuncDelegate = delegate of ``sfactor`` : BlendingFactor * ``dfactor`` : BlendingFactor -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBlendFuncSeparateDelegate = delegate of ``sfactorRGB`` : BlendingFactor * ``dfactorRGB`` : BlendingFactor * ``sfactorAlpha`` : BlendingFactor * ``dfactorAlpha`` : BlendingFactor -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBufferDataDelegate = delegate of ``target`` : BufferTargetARB * ``size`` : unativeint * ``data`` : nativeint * ``usage`` : BufferUsageARB -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glBufferSubDataDelegate = delegate of ``target`` : BufferTargetARB * ``offset`` : nativeint * ``size`` : unativeint * ``data`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCheckFramebufferStatusDelegate = delegate of ``target`` : FramebufferTarget -> GLEnum
    [<SuppressUnmanagedCodeSecurity>]
    type glClearDelegate = delegate of ``mask`` : ClearBufferMask -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearColorDelegate = delegate of ``red`` : float32 * ``green`` : float32 * ``blue`` : float32 * ``alpha`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearDepthfDelegate = delegate of ``d`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glClearStencilDelegate = delegate of ``s`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glColorMaskDelegate = delegate of ``red`` : Boolean * ``green`` : Boolean * ``blue`` : Boolean * ``alpha`` : Boolean -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCompileShaderDelegate = delegate of ``shader`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCompressedTexImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCompressedTexSubImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : InternalFormat * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCopyTexImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCopyTexSubImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCreateProgramDelegate = delegate of unit -> uint32
    [<SuppressUnmanagedCodeSecurity>]
    type glCreateShaderDelegate = delegate of ``type`` : ShaderType -> uint32
    [<SuppressUnmanagedCodeSecurity>]
    type glCullFaceDelegate = delegate of ``mode`` : CullFaceMode -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteBuffersDelegate = delegate of ``n`` : uint32 * ``buffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteFramebuffersDelegate = delegate of ``n`` : uint32 * ``framebuffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteProgramDelegate = delegate of ``program`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteRenderbuffersDelegate = delegate of ``n`` : uint32 * ``renderbuffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteShaderDelegate = delegate of ``shader`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDeleteTexturesDelegate = delegate of ``n`` : uint32 * ``textures`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDepthFuncDelegate = delegate of ``func`` : DepthFunction -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDepthMaskDelegate = delegate of ``flag`` : Boolean -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDepthRangefDelegate = delegate of ``n`` : float32 * ``f`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDetachShaderDelegate = delegate of ``program`` : uint32 * ``shader`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDisableDelegate = delegate of ``cap`` : EnableCap -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDisableVertexAttribArrayDelegate = delegate of ``index`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawArraysDelegate = delegate of ``mode`` : PrimitiveType * ``first`` : int * ``count`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glDrawElementsDelegate = delegate of ``mode`` : PrimitiveType * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glEnableDelegate = delegate of ``cap`` : EnableCap -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glEnableVertexAttribArrayDelegate = delegate of ``index`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFinishDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFlushDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFramebufferRenderbufferDelegate = delegate of ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``renderbuffertarget`` : RenderbufferTarget * ``renderbuffer`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFramebufferTexture2DDelegate = delegate of ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``textarget`` : TextureTarget * ``texture`` : uint32 * ``level`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glFrontFaceDelegate = delegate of ``mode`` : FrontFaceDirection -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenBuffersDelegate = delegate of ``n`` : uint32 * ``buffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenerateMipmapDelegate = delegate of ``target`` : TextureTarget -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenFramebuffersDelegate = delegate of ``n`` : uint32 * ``framebuffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenRenderbuffersDelegate = delegate of ``n`` : uint32 * ``renderbuffers`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGenTexturesDelegate = delegate of ``n`` : uint32 * ``textures`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetActiveAttribDelegate = delegate of ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<int> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetActiveUniformDelegate = delegate of ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<int> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetAttachedShadersDelegate = delegate of ``program`` : uint32 * ``maxCount`` : uint32 * ``count`` : nativeptr<uint32> * ``shaders`` : nativeptr<uint32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetAttribLocationDelegate = delegate of ``program`` : uint32 * ``name`` : nativeptr<uint8> -> int
    [<SuppressUnmanagedCodeSecurity>]
    type glGetBooleanvDelegate = delegate of ``pname`` : GetPName * ``data`` : nativeptr<Boolean> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetBufferParameterivDelegate = delegate of ``target`` : BufferTargetARB * ``pname`` : BufferPNameARB * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetErrorDelegate = delegate of unit -> GLEnum
    [<SuppressUnmanagedCodeSecurity>]
    type glGetFloatvDelegate = delegate of ``pname`` : GetPName * ``data`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetFramebufferAttachmentParameterivDelegate = delegate of ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``pname`` : FramebufferAttachmentParameterName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetIntegervDelegate = delegate of ``pname`` : GetPName * ``data`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetProgramivDelegate = delegate of ``program`` : uint32 * ``pname`` : ProgramPropertyARB * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetProgramInfoLogDelegate = delegate of ``program`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``infoLog`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetRenderbufferParameterivDelegate = delegate of ``target`` : RenderbufferTarget * ``pname`` : RenderbufferParameterName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetShaderivDelegate = delegate of ``shader`` : uint32 * ``pname`` : ShaderParameterName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetShaderInfoLogDelegate = delegate of ``shader`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``infoLog`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetShaderPrecisionFormatDelegate = delegate of ``shadertype`` : ShaderType * ``precisiontype`` : PrecisionType * ``range`` : nativeptr<int> * ``precision`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetShaderSourceDelegate = delegate of ``shader`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``source`` : nativeptr<uint8> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetStringDelegate = delegate of ``name`` : StringName -> nativeptr<uint8>
    [<SuppressUnmanagedCodeSecurity>]
    type glGetTexParameterfvDelegate = delegate of ``target`` : TextureTarget * ``pname`` : GetTextureParameter * ``params`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetTexParameterivDelegate = delegate of ``target`` : TextureTarget * ``pname`` : GetTextureParameter * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformfvDelegate = delegate of ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformivDelegate = delegate of ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetUniformLocationDelegate = delegate of ``program`` : uint32 * ``name`` : nativeptr<uint8> -> int
    [<SuppressUnmanagedCodeSecurity>]
    type glGetVertexAttribfvDelegate = delegate of ``index`` : uint32 * ``pname`` : VertexAttribPropertyARB * ``params`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetVertexAttribivDelegate = delegate of ``index`` : uint32 * ``pname`` : VertexAttribPropertyARB * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetVertexAttribPointervDelegate = delegate of ``index`` : uint32 * ``pname`` : VertexAttribPointerPropertyARB * ``pointer`` : nativeptr<nativeint> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glHintDelegate = delegate of ``target`` : HintTarget * ``mode`` : HintMode -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glIsBufferDelegate = delegate of ``buffer`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsEnabledDelegate = delegate of ``cap`` : EnableCap -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsFramebufferDelegate = delegate of ``framebuffer`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsProgramDelegate = delegate of ``program`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsRenderbufferDelegate = delegate of ``renderbuffer`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsShaderDelegate = delegate of ``shader`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glIsTextureDelegate = delegate of ``texture`` : uint32 -> Boolean
    [<SuppressUnmanagedCodeSecurity>]
    type glLineWidthDelegate = delegate of ``width`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glLinkProgramDelegate = delegate of ``program`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glPixelStoreiDelegate = delegate of ``pname`` : PixelStoreParameter * ``param`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glPolygonOffsetDelegate = delegate of ``factor`` : float32 * ``units`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glReadPixelsDelegate = delegate of ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glReleaseShaderCompilerDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glRenderbufferStorageDelegate = delegate of ``target`` : RenderbufferTarget * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glSampleCoverageDelegate = delegate of ``value`` : float32 * ``invert`` : Boolean -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glScissorDelegate = delegate of ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glShaderBinaryDelegate = delegate of ``count`` : uint32 * ``shaders`` : nativeptr<uint32> * ``binaryFormat`` : ShaderBinaryFormat * ``binary`` : nativeint * ``length`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glShaderSourceDelegate = delegate of ``shader`` : uint32 * ``count`` : uint32 * ``string`` : nativeptr<nativeptr<uint8>> * ``length`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilFuncDelegate = delegate of ``func`` : StencilFunction * ``ref`` : int * ``mask`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilFuncSeparateDelegate = delegate of ``face`` : StencilFaceDirection * ``func`` : StencilFunction * ``ref`` : int * ``mask`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilMaskDelegate = delegate of ``mask`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilMaskSeparateDelegate = delegate of ``face`` : StencilFaceDirection * ``mask`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilOpDelegate = delegate of ``fail`` : StencilOp * ``zfail`` : StencilOp * ``zpass`` : StencilOp -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glStencilOpSeparateDelegate = delegate of ``face`` : StencilFaceDirection * ``sfail`` : StencilOp * ``dpfail`` : StencilOp * ``dppass`` : StencilOp -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexParameterfDelegate = delegate of ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``param`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexParameterfvDelegate = delegate of ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``params`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexParameteriDelegate = delegate of ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``param`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexParameterivDelegate = delegate of ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``params`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexSubImage2DDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1fDelegate = delegate of ``location`` : int * ``v0`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1iDelegate = delegate of ``location`` : int * ``v0`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform1ivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2fDelegate = delegate of ``location`` : int * ``v0`` : float32 * ``v1`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2iDelegate = delegate of ``location`` : int * ``v0`` : int * ``v1`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform2ivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3fDelegate = delegate of ``location`` : int * ``v0`` : float32 * ``v1`` : float32 * ``v2`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3iDelegate = delegate of ``location`` : int * ``v0`` : int * ``v1`` : int * ``v2`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform3ivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4fDelegate = delegate of ``location`` : int * ``v0`` : float32 * ``v1`` : float32 * ``v2`` : float32 * ``v3`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4iDelegate = delegate of ``location`` : int * ``v0`` : int * ``v1`` : int * ``v2`` : int * ``v3`` : int -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniform4ivDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix2fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix3fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUniformMatrix4fvDelegate = delegate of ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glUseProgramDelegate = delegate of ``program`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glValidateProgramDelegate = delegate of ``program`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib1fDelegate = delegate of ``index`` : uint32 * ``x`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib1fvDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib2fDelegate = delegate of ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib2fvDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib3fDelegate = delegate of ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 * ``z`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib3fvDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib4fDelegate = delegate of ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 * ``z`` : float32 * ``w`` : float32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttrib4fvDelegate = delegate of ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glVertexAttribPointerDelegate = delegate of ``index`` : uint32 * ``size`` : int * ``type`` : VertexAttribPointerType * ``normalized`` : Boolean * ``stride`` : uint32 * ``pointer`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glViewportDelegate = delegate of ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glGetBufferSubDataDelegate = delegate of ``target`` : BufferTargetARB * ``offset`` : nativeint * ``size`` : unativeint * ``dst`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glMultiDrawArraysIndirectDelegate = delegate of ``mode`` : PrimitiveType * ``indirectBuffer`` : uint32 * ``count`` : int * ``bindingInfo`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glMultiDrawArraysDelegate = delegate of ``mode`` : PrimitiveType * ``indirect`` : nativeint * ``count`` : int * ``bindingInfo`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glMultiDrawElementsIndirectDelegate = delegate of ``mode`` : PrimitiveType * ``indirectBuffer`` : uint32 * ``count`` : int * ``indexType`` : DrawElementsType * ``bindingInfo`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glMultiDrawElementsDelegate = delegate of ``mode`` : PrimitiveType * ``indirect`` : nativeint * ``count`` : int * ``indexType`` : DrawElementsType * ``bindingInfo`` : nativeint -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glCommitDelegate = delegate of unit -> unit
    [<SuppressUnmanagedCodeSecurity>]
    type glTexSubImage2DJSImageDelegate = delegate of ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : int * ``height`` : int * ``format`` : PixelFormat * ``typ`` : PixelType * ``imgHandle`` : int -> unit
    type GLDelegates() =
        [<DefaultValue>]
        val mutable public glBeginQuery : glBeginQueryDelegate
        [<DefaultValue>]
        val mutable public glBeginTransformFeedback : glBeginTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glBindBufferBase : glBindBufferBaseDelegate
        [<DefaultValue>]
        val mutable public glBindBufferRange : glBindBufferRangeDelegate
        [<DefaultValue>]
        val mutable public glBindSampler : glBindSamplerDelegate
        [<DefaultValue>]
        val mutable public glBindTransformFeedback : glBindTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glBindVertexArray : glBindVertexArrayDelegate
        [<DefaultValue>]
        val mutable public glBlitFramebuffer : glBlitFramebufferDelegate
        [<DefaultValue>]
        val mutable public glClearBufferiv : glClearBufferivDelegate
        [<DefaultValue>]
        val mutable public glClearBufferuiv : glClearBufferuivDelegate
        [<DefaultValue>]
        val mutable public glClearBufferfv : glClearBufferfvDelegate
        [<DefaultValue>]
        val mutable public glClearBufferfi : glClearBufferfiDelegate
        [<DefaultValue>]
        val mutable public glClientWaitSync : glClientWaitSyncDelegate
        [<DefaultValue>]
        val mutable public glCompressedTexImage3D : glCompressedTexImage3DDelegate
        [<DefaultValue>]
        val mutable public glCompressedTexSubImage3D : glCompressedTexSubImage3DDelegate
        [<DefaultValue>]
        val mutable public glCopyBufferSubData : glCopyBufferSubDataDelegate
        [<DefaultValue>]
        val mutable public glCopyTexSubImage3D : glCopyTexSubImage3DDelegate
        [<DefaultValue>]
        val mutable public glDeleteQueries : glDeleteQueriesDelegate
        [<DefaultValue>]
        val mutable public glDeleteSamplers : glDeleteSamplersDelegate
        [<DefaultValue>]
        val mutable public glDeleteSync : glDeleteSyncDelegate
        [<DefaultValue>]
        val mutable public glDeleteTransformFeedbacks : glDeleteTransformFeedbacksDelegate
        [<DefaultValue>]
        val mutable public glDeleteVertexArrays : glDeleteVertexArraysDelegate
        [<DefaultValue>]
        val mutable public glDrawArraysInstanced : glDrawArraysInstancedDelegate
        [<DefaultValue>]
        val mutable public glDrawBuffers : glDrawBuffersDelegate
        [<DefaultValue>]
        val mutable public glDrawElementsInstanced : glDrawElementsInstancedDelegate
        [<DefaultValue>]
        val mutable public glDrawRangeElements : glDrawRangeElementsDelegate
        [<DefaultValue>]
        val mutable public glEndQuery : glEndQueryDelegate
        [<DefaultValue>]
        val mutable public glEndTransformFeedback : glEndTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glFenceSync : glFenceSyncDelegate
        [<DefaultValue>]
        val mutable public glFramebufferTextureLayer : glFramebufferTextureLayerDelegate
        [<DefaultValue>]
        val mutable public glGenQueries : glGenQueriesDelegate
        [<DefaultValue>]
        val mutable public glGenSamplers : glGenSamplersDelegate
        [<DefaultValue>]
        val mutable public glGenTransformFeedbacks : glGenTransformFeedbacksDelegate
        [<DefaultValue>]
        val mutable public glGenVertexArrays : glGenVertexArraysDelegate
        [<DefaultValue>]
        val mutable public glGetActiveUniformBlockiv : glGetActiveUniformBlockivDelegate
        [<DefaultValue>]
        val mutable public glGetActiveUniformBlockName : glGetActiveUniformBlockNameDelegate
        [<DefaultValue>]
        val mutable public glGetActiveUniformsiv : glGetActiveUniformsivDelegate
        [<DefaultValue>]
        val mutable public glGetBufferParameteri64v : glGetBufferParameteri64vDelegate
        [<DefaultValue>]
        val mutable public glGetFragDataLocation : glGetFragDataLocationDelegate
        [<DefaultValue>]
        val mutable public glGetIntegeri_v : glGetIntegeri_vDelegate
        [<DefaultValue>]
        val mutable public glGetInteger64v : glGetInteger64vDelegate
        [<DefaultValue>]
        val mutable public glGetInteger64i_v : glGetInteger64i_vDelegate
        [<DefaultValue>]
        val mutable public glGetInternalformativ : glGetInternalformativDelegate
        [<DefaultValue>]
        val mutable public glGetProgramBinary : glGetProgramBinaryDelegate
        [<DefaultValue>]
        val mutable public glGetQueryiv : glGetQueryivDelegate
        [<DefaultValue>]
        val mutable public glGetQueryObjectuiv : glGetQueryObjectuivDelegate
        [<DefaultValue>]
        val mutable public glGetSamplerParameteriv : glGetSamplerParameterivDelegate
        [<DefaultValue>]
        val mutable public glGetSamplerParameterfv : glGetSamplerParameterfvDelegate
        [<DefaultValue>]
        val mutable public glGetStringi : glGetStringiDelegate
        [<DefaultValue>]
        val mutable public glGetSynciv : glGetSyncivDelegate
        [<DefaultValue>]
        val mutable public glGetTransformFeedbackVarying : glGetTransformFeedbackVaryingDelegate
        [<DefaultValue>]
        val mutable public glGetUniformuiv : glGetUniformuivDelegate
        [<DefaultValue>]
        val mutable public glGetUniformBlockIndex : glGetUniformBlockIndexDelegate
        [<DefaultValue>]
        val mutable public glGetUniformIndices : glGetUniformIndicesDelegate
        [<DefaultValue>]
        val mutable public glGetVertexAttribIiv : glGetVertexAttribIivDelegate
        [<DefaultValue>]
        val mutable public glGetVertexAttribIuiv : glGetVertexAttribIuivDelegate
        [<DefaultValue>]
        val mutable public glInvalidateFramebuffer : glInvalidateFramebufferDelegate
        [<DefaultValue>]
        val mutable public glInvalidateSubFramebuffer : glInvalidateSubFramebufferDelegate
        [<DefaultValue>]
        val mutable public glIsQuery : glIsQueryDelegate
        [<DefaultValue>]
        val mutable public glIsSampler : glIsSamplerDelegate
        [<DefaultValue>]
        val mutable public glIsSync : glIsSyncDelegate
        [<DefaultValue>]
        val mutable public glIsTransformFeedback : glIsTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glIsVertexArray : glIsVertexArrayDelegate
        [<DefaultValue>]
        val mutable public glPauseTransformFeedback : glPauseTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glProgramBinary : glProgramBinaryDelegate
        [<DefaultValue>]
        val mutable public glProgramParameteri : glProgramParameteriDelegate
        [<DefaultValue>]
        val mutable public glReadBuffer : glReadBufferDelegate
        [<DefaultValue>]
        val mutable public glRenderbufferStorageMultisample : glRenderbufferStorageMultisampleDelegate
        [<DefaultValue>]
        val mutable public glResumeTransformFeedback : glResumeTransformFeedbackDelegate
        [<DefaultValue>]
        val mutable public glSamplerParameteri : glSamplerParameteriDelegate
        [<DefaultValue>]
        val mutable public glSamplerParameteriv : glSamplerParameterivDelegate
        [<DefaultValue>]
        val mutable public glSamplerParameterf : glSamplerParameterfDelegate
        [<DefaultValue>]
        val mutable public glSamplerParameterfv : glSamplerParameterfvDelegate
        [<DefaultValue>]
        val mutable public glTexImage3D : glTexImage3DDelegate
        [<DefaultValue>]
        val mutable public glTexStorage2D : glTexStorage2DDelegate
        [<DefaultValue>]
        val mutable public glTexStorage3D : glTexStorage3DDelegate
        [<DefaultValue>]
        val mutable public glTexSubImage3D : glTexSubImage3DDelegate
        [<DefaultValue>]
        val mutable public glTransformFeedbackVaryings : glTransformFeedbackVaryingsDelegate
        [<DefaultValue>]
        val mutable public glUniform1ui : glUniform1uiDelegate
        [<DefaultValue>]
        val mutable public glUniform1uiv : glUniform1uivDelegate
        [<DefaultValue>]
        val mutable public glUniform2ui : glUniform2uiDelegate
        [<DefaultValue>]
        val mutable public glUniform2uiv : glUniform2uivDelegate
        [<DefaultValue>]
        val mutable public glUniform3ui : glUniform3uiDelegate
        [<DefaultValue>]
        val mutable public glUniform3uiv : glUniform3uivDelegate
        [<DefaultValue>]
        val mutable public glUniform4ui : glUniform4uiDelegate
        [<DefaultValue>]
        val mutable public glUniform4uiv : glUniform4uivDelegate
        [<DefaultValue>]
        val mutable public glUniformBlockBinding : glUniformBlockBindingDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix2x3fv : glUniformMatrix2x3fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix2x4fv : glUniformMatrix2x4fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix3x2fv : glUniformMatrix3x2fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix3x4fv : glUniformMatrix3x4fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix4x2fv : glUniformMatrix4x2fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix4x3fv : glUniformMatrix4x3fvDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribDivisor : glVertexAttribDivisorDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribI4i : glVertexAttribI4iDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribI4ui : glVertexAttribI4uiDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribI4iv : glVertexAttribI4ivDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribI4uiv : glVertexAttribI4uivDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribIPointer : glVertexAttribIPointerDelegate
        [<DefaultValue>]
        val mutable public glWaitSync : glWaitSyncDelegate
        [<DefaultValue>]
        val mutable public glActiveTexture : glActiveTextureDelegate
        [<DefaultValue>]
        val mutable public glAttachShader : glAttachShaderDelegate
        [<DefaultValue>]
        val mutable public glBindAttribLocation : glBindAttribLocationDelegate
        [<DefaultValue>]
        val mutable public glBindBuffer : glBindBufferDelegate
        [<DefaultValue>]
        val mutable public glBindFramebuffer : glBindFramebufferDelegate
        [<DefaultValue>]
        val mutable public glBindRenderbuffer : glBindRenderbufferDelegate
        [<DefaultValue>]
        val mutable public glBindTexture : glBindTextureDelegate
        [<DefaultValue>]
        val mutable public glBlendColor : glBlendColorDelegate
        [<DefaultValue>]
        val mutable public glBlendEquation : glBlendEquationDelegate
        [<DefaultValue>]
        val mutable public glBlendEquationSeparate : glBlendEquationSeparateDelegate
        [<DefaultValue>]
        val mutable public glBlendFunc : glBlendFuncDelegate
        [<DefaultValue>]
        val mutable public glBlendFuncSeparate : glBlendFuncSeparateDelegate
        [<DefaultValue>]
        val mutable public glBufferData : glBufferDataDelegate
        [<DefaultValue>]
        val mutable public glBufferSubData : glBufferSubDataDelegate
        [<DefaultValue>]
        val mutable public glCheckFramebufferStatus : glCheckFramebufferStatusDelegate
        [<DefaultValue>]
        val mutable public glClear : glClearDelegate
        [<DefaultValue>]
        val mutable public glClearColor : glClearColorDelegate
        [<DefaultValue>]
        val mutable public glClearDepthf : glClearDepthfDelegate
        [<DefaultValue>]
        val mutable public glClearStencil : glClearStencilDelegate
        [<DefaultValue>]
        val mutable public glColorMask : glColorMaskDelegate
        [<DefaultValue>]
        val mutable public glCompileShader : glCompileShaderDelegate
        [<DefaultValue>]
        val mutable public glCompressedTexImage2D : glCompressedTexImage2DDelegate
        [<DefaultValue>]
        val mutable public glCompressedTexSubImage2D : glCompressedTexSubImage2DDelegate
        [<DefaultValue>]
        val mutable public glCopyTexImage2D : glCopyTexImage2DDelegate
        [<DefaultValue>]
        val mutable public glCopyTexSubImage2D : glCopyTexSubImage2DDelegate
        [<DefaultValue>]
        val mutable public glCreateProgram : glCreateProgramDelegate
        [<DefaultValue>]
        val mutable public glCreateShader : glCreateShaderDelegate
        [<DefaultValue>]
        val mutable public glCullFace : glCullFaceDelegate
        [<DefaultValue>]
        val mutable public glDeleteBuffers : glDeleteBuffersDelegate
        [<DefaultValue>]
        val mutable public glDeleteFramebuffers : glDeleteFramebuffersDelegate
        [<DefaultValue>]
        val mutable public glDeleteProgram : glDeleteProgramDelegate
        [<DefaultValue>]
        val mutable public glDeleteRenderbuffers : glDeleteRenderbuffersDelegate
        [<DefaultValue>]
        val mutable public glDeleteShader : glDeleteShaderDelegate
        [<DefaultValue>]
        val mutable public glDeleteTextures : glDeleteTexturesDelegate
        [<DefaultValue>]
        val mutable public glDepthFunc : glDepthFuncDelegate
        [<DefaultValue>]
        val mutable public glDepthMask : glDepthMaskDelegate
        [<DefaultValue>]
        val mutable public glDepthRangef : glDepthRangefDelegate
        [<DefaultValue>]
        val mutable public glDetachShader : glDetachShaderDelegate
        [<DefaultValue>]
        val mutable public glDisable : glDisableDelegate
        [<DefaultValue>]
        val mutable public glDisableVertexAttribArray : glDisableVertexAttribArrayDelegate
        [<DefaultValue>]
        val mutable public glDrawArrays : glDrawArraysDelegate
        [<DefaultValue>]
        val mutable public glDrawElements : glDrawElementsDelegate
        [<DefaultValue>]
        val mutable public glEnable : glEnableDelegate
        [<DefaultValue>]
        val mutable public glEnableVertexAttribArray : glEnableVertexAttribArrayDelegate
        [<DefaultValue>]
        val mutable public glFinish : glFinishDelegate
        [<DefaultValue>]
        val mutable public glFlush : glFlushDelegate
        [<DefaultValue>]
        val mutable public glFramebufferRenderbuffer : glFramebufferRenderbufferDelegate
        [<DefaultValue>]
        val mutable public glFramebufferTexture2D : glFramebufferTexture2DDelegate
        [<DefaultValue>]
        val mutable public glFrontFace : glFrontFaceDelegate
        [<DefaultValue>]
        val mutable public glGenBuffers : glGenBuffersDelegate
        [<DefaultValue>]
        val mutable public glGenerateMipmap : glGenerateMipmapDelegate
        [<DefaultValue>]
        val mutable public glGenFramebuffers : glGenFramebuffersDelegate
        [<DefaultValue>]
        val mutable public glGenRenderbuffers : glGenRenderbuffersDelegate
        [<DefaultValue>]
        val mutable public glGenTextures : glGenTexturesDelegate
        [<DefaultValue>]
        val mutable public glGetActiveAttrib : glGetActiveAttribDelegate
        [<DefaultValue>]
        val mutable public glGetActiveUniform : glGetActiveUniformDelegate
        [<DefaultValue>]
        val mutable public glGetAttachedShaders : glGetAttachedShadersDelegate
        [<DefaultValue>]
        val mutable public glGetAttribLocation : glGetAttribLocationDelegate
        [<DefaultValue>]
        val mutable public glGetBooleanv : glGetBooleanvDelegate
        [<DefaultValue>]
        val mutable public glGetBufferParameteriv : glGetBufferParameterivDelegate
        [<DefaultValue>]
        val mutable public glGetError : glGetErrorDelegate
        [<DefaultValue>]
        val mutable public glGetFloatv : glGetFloatvDelegate
        [<DefaultValue>]
        val mutable public glGetFramebufferAttachmentParameteriv : glGetFramebufferAttachmentParameterivDelegate
        [<DefaultValue>]
        val mutable public glGetIntegerv : glGetIntegervDelegate
        [<DefaultValue>]
        val mutable public glGetProgramiv : glGetProgramivDelegate
        [<DefaultValue>]
        val mutable public glGetProgramInfoLog : glGetProgramInfoLogDelegate
        [<DefaultValue>]
        val mutable public glGetRenderbufferParameteriv : glGetRenderbufferParameterivDelegate
        [<DefaultValue>]
        val mutable public glGetShaderiv : glGetShaderivDelegate
        [<DefaultValue>]
        val mutable public glGetShaderInfoLog : glGetShaderInfoLogDelegate
        [<DefaultValue>]
        val mutable public glGetShaderPrecisionFormat : glGetShaderPrecisionFormatDelegate
        [<DefaultValue>]
        val mutable public glGetShaderSource : glGetShaderSourceDelegate
        [<DefaultValue>]
        val mutable public glGetString : glGetStringDelegate
        [<DefaultValue>]
        val mutable public glGetTexParameterfv : glGetTexParameterfvDelegate
        [<DefaultValue>]
        val mutable public glGetTexParameteriv : glGetTexParameterivDelegate
        [<DefaultValue>]
        val mutable public glGetUniformfv : glGetUniformfvDelegate
        [<DefaultValue>]
        val mutable public glGetUniformiv : glGetUniformivDelegate
        [<DefaultValue>]
        val mutable public glGetUniformLocation : glGetUniformLocationDelegate
        [<DefaultValue>]
        val mutable public glGetVertexAttribfv : glGetVertexAttribfvDelegate
        [<DefaultValue>]
        val mutable public glGetVertexAttribiv : glGetVertexAttribivDelegate
        [<DefaultValue>]
        val mutable public glGetVertexAttribPointerv : glGetVertexAttribPointervDelegate
        [<DefaultValue>]
        val mutable public glHint : glHintDelegate
        [<DefaultValue>]
        val mutable public glIsBuffer : glIsBufferDelegate
        [<DefaultValue>]
        val mutable public glIsEnabled : glIsEnabledDelegate
        [<DefaultValue>]
        val mutable public glIsFramebuffer : glIsFramebufferDelegate
        [<DefaultValue>]
        val mutable public glIsProgram : glIsProgramDelegate
        [<DefaultValue>]
        val mutable public glIsRenderbuffer : glIsRenderbufferDelegate
        [<DefaultValue>]
        val mutable public glIsShader : glIsShaderDelegate
        [<DefaultValue>]
        val mutable public glIsTexture : glIsTextureDelegate
        [<DefaultValue>]
        val mutable public glLineWidth : glLineWidthDelegate
        [<DefaultValue>]
        val mutable public glLinkProgram : glLinkProgramDelegate
        [<DefaultValue>]
        val mutable public glPixelStorei : glPixelStoreiDelegate
        [<DefaultValue>]
        val mutable public glPolygonOffset : glPolygonOffsetDelegate
        [<DefaultValue>]
        val mutable public glReadPixels : glReadPixelsDelegate
        [<DefaultValue>]
        val mutable public glReleaseShaderCompiler : glReleaseShaderCompilerDelegate
        [<DefaultValue>]
        val mutable public glRenderbufferStorage : glRenderbufferStorageDelegate
        [<DefaultValue>]
        val mutable public glSampleCoverage : glSampleCoverageDelegate
        [<DefaultValue>]
        val mutable public glScissor : glScissorDelegate
        [<DefaultValue>]
        val mutable public glShaderBinary : glShaderBinaryDelegate
        [<DefaultValue>]
        val mutable public glShaderSource : glShaderSourceDelegate
        [<DefaultValue>]
        val mutable public glStencilFunc : glStencilFuncDelegate
        [<DefaultValue>]
        val mutable public glStencilFuncSeparate : glStencilFuncSeparateDelegate
        [<DefaultValue>]
        val mutable public glStencilMask : glStencilMaskDelegate
        [<DefaultValue>]
        val mutable public glStencilMaskSeparate : glStencilMaskSeparateDelegate
        [<DefaultValue>]
        val mutable public glStencilOp : glStencilOpDelegate
        [<DefaultValue>]
        val mutable public glStencilOpSeparate : glStencilOpSeparateDelegate
        [<DefaultValue>]
        val mutable public glTexImage2D : glTexImage2DDelegate
        [<DefaultValue>]
        val mutable public glTexParameterf : glTexParameterfDelegate
        [<DefaultValue>]
        val mutable public glTexParameterfv : glTexParameterfvDelegate
        [<DefaultValue>]
        val mutable public glTexParameteri : glTexParameteriDelegate
        [<DefaultValue>]
        val mutable public glTexParameteriv : glTexParameterivDelegate
        [<DefaultValue>]
        val mutable public glTexSubImage2D : glTexSubImage2DDelegate
        [<DefaultValue>]
        val mutable public glUniform1f : glUniform1fDelegate
        [<DefaultValue>]
        val mutable public glUniform1fv : glUniform1fvDelegate
        [<DefaultValue>]
        val mutable public glUniform1i : glUniform1iDelegate
        [<DefaultValue>]
        val mutable public glUniform1iv : glUniform1ivDelegate
        [<DefaultValue>]
        val mutable public glUniform2f : glUniform2fDelegate
        [<DefaultValue>]
        val mutable public glUniform2fv : glUniform2fvDelegate
        [<DefaultValue>]
        val mutable public glUniform2i : glUniform2iDelegate
        [<DefaultValue>]
        val mutable public glUniform2iv : glUniform2ivDelegate
        [<DefaultValue>]
        val mutable public glUniform3f : glUniform3fDelegate
        [<DefaultValue>]
        val mutable public glUniform3fv : glUniform3fvDelegate
        [<DefaultValue>]
        val mutable public glUniform3i : glUniform3iDelegate
        [<DefaultValue>]
        val mutable public glUniform3iv : glUniform3ivDelegate
        [<DefaultValue>]
        val mutable public glUniform4f : glUniform4fDelegate
        [<DefaultValue>]
        val mutable public glUniform4fv : glUniform4fvDelegate
        [<DefaultValue>]
        val mutable public glUniform4i : glUniform4iDelegate
        [<DefaultValue>]
        val mutable public glUniform4iv : glUniform4ivDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix2fv : glUniformMatrix2fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix3fv : glUniformMatrix3fvDelegate
        [<DefaultValue>]
        val mutable public glUniformMatrix4fv : glUniformMatrix4fvDelegate
        [<DefaultValue>]
        val mutable public glUseProgram : glUseProgramDelegate
        [<DefaultValue>]
        val mutable public glValidateProgram : glValidateProgramDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib1f : glVertexAttrib1fDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib1fv : glVertexAttrib1fvDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib2f : glVertexAttrib2fDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib2fv : glVertexAttrib2fvDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib3f : glVertexAttrib3fDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib3fv : glVertexAttrib3fvDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib4f : glVertexAttrib4fDelegate
        [<DefaultValue>]
        val mutable public glVertexAttrib4fv : glVertexAttrib4fvDelegate
        [<DefaultValue>]
        val mutable public glVertexAttribPointer : glVertexAttribPointerDelegate
        [<DefaultValue>]
        val mutable public glViewport : glViewportDelegate
        [<DefaultValue>]
        val mutable public glGetBufferSubData : glGetBufferSubDataDelegate
        [<DefaultValue>]
        val mutable public glMultiDrawArraysIndirect : glMultiDrawArraysIndirectDelegate
        [<DefaultValue>]
        val mutable public glMultiDrawArrays : glMultiDrawArraysDelegate
        [<DefaultValue>]
        val mutable public glMultiDrawElementsIndirect : glMultiDrawElementsIndirectDelegate
        [<DefaultValue>]
        val mutable public glMultiDrawElements : glMultiDrawElementsDelegate
        [<DefaultValue>]
        val mutable public glCommit : glCommitDelegate
        [<DefaultValue>]
        val mutable public glTexSubImage2DJSImage : glTexSubImage2DJSImageDelegate
    let private cache = System.Collections.Concurrent.ConcurrentDictionary<Silk.NET.Core.Contexts.INativeContext, GLDelegates>()
    let private getDelegate<'t> (ptr : nativeint) =
        if ptr = 0n then Unchecked.defaultof<'t>
        else Marshal.GetDelegateForFunctionPointer(ptr, typeof<'t>) |> unbox<'t>
    let get (ctx : Silk.NET.Core.Contexts.INativeContext) =
        cache.GetOrAdd(ctx, fun ctx ->
            let ptrs = GLFunctionPointers.get ctx
            let res = GLDelegates()
            res.glBeginQuery <- if ptrs.glBeginQuery = 0n then glBeginQueryDelegate(fun _ _ -> failwith "glBeginQuery not found") else getDelegate<glBeginQueryDelegate> ptrs.glBeginQuery
            res.glBeginTransformFeedback <- if ptrs.glBeginTransformFeedback = 0n then glBeginTransformFeedbackDelegate(fun _ -> failwith "glBeginTransformFeedback not found") else getDelegate<glBeginTransformFeedbackDelegate> ptrs.glBeginTransformFeedback
            res.glBindBufferBase <- if ptrs.glBindBufferBase = 0n then glBindBufferBaseDelegate(fun _ _ _ -> failwith "glBindBufferBase not found") else getDelegate<glBindBufferBaseDelegate> ptrs.glBindBufferBase
            res.glBindBufferRange <- if ptrs.glBindBufferRange = 0n then glBindBufferRangeDelegate(fun _ _ _ _ _ -> failwith "glBindBufferRange not found") else getDelegate<glBindBufferRangeDelegate> ptrs.glBindBufferRange
            res.glBindSampler <- if ptrs.glBindSampler = 0n then glBindSamplerDelegate(fun _ _ -> failwith "glBindSampler not found") else getDelegate<glBindSamplerDelegate> ptrs.glBindSampler
            res.glBindTransformFeedback <- if ptrs.glBindTransformFeedback = 0n then glBindTransformFeedbackDelegate(fun _ _ -> failwith "glBindTransformFeedback not found") else getDelegate<glBindTransformFeedbackDelegate> ptrs.glBindTransformFeedback
            res.glBindVertexArray <- if ptrs.glBindVertexArray = 0n then glBindVertexArrayDelegate(fun _ -> failwith "glBindVertexArray not found") else getDelegate<glBindVertexArrayDelegate> ptrs.glBindVertexArray
            res.glBlitFramebuffer <- if ptrs.glBlitFramebuffer = 0n then glBlitFramebufferDelegate(fun _ _ _ _ _ _ _ _ _ _ -> failwith "glBlitFramebuffer not found") else getDelegate<glBlitFramebufferDelegate> ptrs.glBlitFramebuffer
            res.glClearBufferiv <- if ptrs.glClearBufferiv = 0n then glClearBufferivDelegate(fun _ _ _ -> failwith "glClearBufferiv not found") else getDelegate<glClearBufferivDelegate> ptrs.glClearBufferiv
            res.glClearBufferuiv <- if ptrs.glClearBufferuiv = 0n then glClearBufferuivDelegate(fun _ _ _ -> failwith "glClearBufferuiv not found") else getDelegate<glClearBufferuivDelegate> ptrs.glClearBufferuiv
            res.glClearBufferfv <- if ptrs.glClearBufferfv = 0n then glClearBufferfvDelegate(fun _ _ _ -> failwith "glClearBufferfv not found") else getDelegate<glClearBufferfvDelegate> ptrs.glClearBufferfv
            res.glClearBufferfi <- if ptrs.glClearBufferfi = 0n then glClearBufferfiDelegate(fun _ _ _ _ -> failwith "glClearBufferfi not found") else getDelegate<glClearBufferfiDelegate> ptrs.glClearBufferfi
            res.glClientWaitSync <- if ptrs.glClientWaitSync = 0n then glClientWaitSyncDelegate(fun _ _ _ -> failwith "glClientWaitSync not found") else getDelegate<glClientWaitSyncDelegate> ptrs.glClientWaitSync
            res.glCompressedTexImage3D <- if ptrs.glCompressedTexImage3D = 0n then glCompressedTexImage3DDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glCompressedTexImage3D not found") else getDelegate<glCompressedTexImage3DDelegate> ptrs.glCompressedTexImage3D
            res.glCompressedTexSubImage3D <- if ptrs.glCompressedTexSubImage3D = 0n then glCompressedTexSubImage3DDelegate(fun _ _ _ _ _ _ _ _ _ _ _ -> failwith "glCompressedTexSubImage3D not found") else getDelegate<glCompressedTexSubImage3DDelegate> ptrs.glCompressedTexSubImage3D
            res.glCopyBufferSubData <- if ptrs.glCopyBufferSubData = 0n then glCopyBufferSubDataDelegate(fun _ _ _ _ _ -> failwith "glCopyBufferSubData not found") else getDelegate<glCopyBufferSubDataDelegate> ptrs.glCopyBufferSubData
            res.glCopyTexSubImage3D <- if ptrs.glCopyTexSubImage3D = 0n then glCopyTexSubImage3DDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glCopyTexSubImage3D not found") else getDelegate<glCopyTexSubImage3DDelegate> ptrs.glCopyTexSubImage3D
            res.glDeleteQueries <- if ptrs.glDeleteQueries = 0n then glDeleteQueriesDelegate(fun _ _ -> failwith "glDeleteQueries not found") else getDelegate<glDeleteQueriesDelegate> ptrs.glDeleteQueries
            res.glDeleteSamplers <- if ptrs.glDeleteSamplers = 0n then glDeleteSamplersDelegate(fun _ _ -> failwith "glDeleteSamplers not found") else getDelegate<glDeleteSamplersDelegate> ptrs.glDeleteSamplers
            res.glDeleteSync <- if ptrs.glDeleteSync = 0n then glDeleteSyncDelegate(fun _ -> failwith "glDeleteSync not found") else getDelegate<glDeleteSyncDelegate> ptrs.glDeleteSync
            res.glDeleteTransformFeedbacks <- if ptrs.glDeleteTransformFeedbacks = 0n then glDeleteTransformFeedbacksDelegate(fun _ _ -> failwith "glDeleteTransformFeedbacks not found") else getDelegate<glDeleteTransformFeedbacksDelegate> ptrs.glDeleteTransformFeedbacks
            res.glDeleteVertexArrays <- if ptrs.glDeleteVertexArrays = 0n then glDeleteVertexArraysDelegate(fun _ _ -> failwith "glDeleteVertexArrays not found") else getDelegate<glDeleteVertexArraysDelegate> ptrs.glDeleteVertexArrays
            res.glDrawArraysInstanced <- if ptrs.glDrawArraysInstanced = 0n then glDrawArraysInstancedDelegate(fun _ _ _ _ -> failwith "glDrawArraysInstanced not found") else getDelegate<glDrawArraysInstancedDelegate> ptrs.glDrawArraysInstanced
            res.glDrawBuffers <- if ptrs.glDrawBuffers = 0n then glDrawBuffersDelegate(fun _ _ -> failwith "glDrawBuffers not found") else getDelegate<glDrawBuffersDelegate> ptrs.glDrawBuffers
            res.glDrawElementsInstanced <- if ptrs.glDrawElementsInstanced = 0n then glDrawElementsInstancedDelegate(fun _ _ _ _ _ -> failwith "glDrawElementsInstanced not found") else getDelegate<glDrawElementsInstancedDelegate> ptrs.glDrawElementsInstanced
            res.glDrawRangeElements <- if ptrs.glDrawRangeElements = 0n then glDrawRangeElementsDelegate(fun _ _ _ _ _ _ -> failwith "glDrawRangeElements not found") else getDelegate<glDrawRangeElementsDelegate> ptrs.glDrawRangeElements
            res.glEndQuery <- if ptrs.glEndQuery = 0n then glEndQueryDelegate(fun _ -> failwith "glEndQuery not found") else getDelegate<glEndQueryDelegate> ptrs.glEndQuery
            res.glEndTransformFeedback <- if ptrs.glEndTransformFeedback = 0n then glEndTransformFeedbackDelegate(fun () -> failwith "glEndTransformFeedback not found") else getDelegate<glEndTransformFeedbackDelegate> ptrs.glEndTransformFeedback
            res.glFenceSync <- if ptrs.glFenceSync = 0n then glFenceSyncDelegate(fun _ _ -> failwith "glFenceSync not found") else getDelegate<glFenceSyncDelegate> ptrs.glFenceSync
            res.glFramebufferTextureLayer <- if ptrs.glFramebufferTextureLayer = 0n then glFramebufferTextureLayerDelegate(fun _ _ _ _ _ -> failwith "glFramebufferTextureLayer not found") else getDelegate<glFramebufferTextureLayerDelegate> ptrs.glFramebufferTextureLayer
            res.glGenQueries <- if ptrs.glGenQueries = 0n then glGenQueriesDelegate(fun _ _ -> failwith "glGenQueries not found") else getDelegate<glGenQueriesDelegate> ptrs.glGenQueries
            res.glGenSamplers <- if ptrs.glGenSamplers = 0n then glGenSamplersDelegate(fun _ _ -> failwith "glGenSamplers not found") else getDelegate<glGenSamplersDelegate> ptrs.glGenSamplers
            res.glGenTransformFeedbacks <- if ptrs.glGenTransformFeedbacks = 0n then glGenTransformFeedbacksDelegate(fun _ _ -> failwith "glGenTransformFeedbacks not found") else getDelegate<glGenTransformFeedbacksDelegate> ptrs.glGenTransformFeedbacks
            res.glGenVertexArrays <- if ptrs.glGenVertexArrays = 0n then glGenVertexArraysDelegate(fun _ _ -> failwith "glGenVertexArrays not found") else getDelegate<glGenVertexArraysDelegate> ptrs.glGenVertexArrays
            res.glGetActiveUniformBlockiv <- if ptrs.glGetActiveUniformBlockiv = 0n then glGetActiveUniformBlockivDelegate(fun _ _ _ _ -> failwith "glGetActiveUniformBlockiv not found") else getDelegate<glGetActiveUniformBlockivDelegate> ptrs.glGetActiveUniformBlockiv
            res.glGetActiveUniformBlockName <- if ptrs.glGetActiveUniformBlockName = 0n then glGetActiveUniformBlockNameDelegate(fun _ _ _ _ _ -> failwith "glGetActiveUniformBlockName not found") else getDelegate<glGetActiveUniformBlockNameDelegate> ptrs.glGetActiveUniformBlockName
            res.glGetActiveUniformsiv <- if ptrs.glGetActiveUniformsiv = 0n then glGetActiveUniformsivDelegate(fun _ _ _ _ _ -> failwith "glGetActiveUniformsiv not found") else getDelegate<glGetActiveUniformsivDelegate> ptrs.glGetActiveUniformsiv
            res.glGetBufferParameteri64v <- if ptrs.glGetBufferParameteri64v = 0n then glGetBufferParameteri64vDelegate(fun _ _ _ -> failwith "glGetBufferParameteri64v not found") else getDelegate<glGetBufferParameteri64vDelegate> ptrs.glGetBufferParameteri64v
            res.glGetFragDataLocation <- if ptrs.glGetFragDataLocation = 0n then glGetFragDataLocationDelegate(fun _ _ -> failwith "glGetFragDataLocation not found") else getDelegate<glGetFragDataLocationDelegate> ptrs.glGetFragDataLocation
            res.glGetIntegeri_v <- if ptrs.glGetIntegeri_v = 0n then glGetIntegeri_vDelegate(fun _ _ _ -> failwith "glGetIntegeri_v not found") else getDelegate<glGetIntegeri_vDelegate> ptrs.glGetIntegeri_v
            res.glGetInteger64v <- if ptrs.glGetInteger64v = 0n then glGetInteger64vDelegate(fun _ _ -> failwith "glGetInteger64v not found") else getDelegate<glGetInteger64vDelegate> ptrs.glGetInteger64v
            res.glGetInteger64i_v <- if ptrs.glGetInteger64i_v = 0n then glGetInteger64i_vDelegate(fun _ _ _ -> failwith "glGetInteger64i_v not found") else getDelegate<glGetInteger64i_vDelegate> ptrs.glGetInteger64i_v
            res.glGetInternalformativ <- if ptrs.glGetInternalformativ = 0n then glGetInternalformativDelegate(fun _ _ _ _ _ -> failwith "glGetInternalformativ not found") else getDelegate<glGetInternalformativDelegate> ptrs.glGetInternalformativ
            res.glGetProgramBinary <- if ptrs.glGetProgramBinary = 0n then glGetProgramBinaryDelegate(fun _ _ _ _ _ -> failwith "glGetProgramBinary not found") else getDelegate<glGetProgramBinaryDelegate> ptrs.glGetProgramBinary
            res.glGetQueryiv <- if ptrs.glGetQueryiv = 0n then glGetQueryivDelegate(fun _ _ _ -> failwith "glGetQueryiv not found") else getDelegate<glGetQueryivDelegate> ptrs.glGetQueryiv
            res.glGetQueryObjectuiv <- if ptrs.glGetQueryObjectuiv = 0n then glGetQueryObjectuivDelegate(fun _ _ _ -> failwith "glGetQueryObjectuiv not found") else getDelegate<glGetQueryObjectuivDelegate> ptrs.glGetQueryObjectuiv
            res.glGetSamplerParameteriv <- if ptrs.glGetSamplerParameteriv = 0n then glGetSamplerParameterivDelegate(fun _ _ _ -> failwith "glGetSamplerParameteriv not found") else getDelegate<glGetSamplerParameterivDelegate> ptrs.glGetSamplerParameteriv
            res.glGetSamplerParameterfv <- if ptrs.glGetSamplerParameterfv = 0n then glGetSamplerParameterfvDelegate(fun _ _ _ -> failwith "glGetSamplerParameterfv not found") else getDelegate<glGetSamplerParameterfvDelegate> ptrs.glGetSamplerParameterfv
            res.glGetStringi <- if ptrs.glGetStringi = 0n then glGetStringiDelegate(fun _ _ -> failwith "glGetStringi not found") else getDelegate<glGetStringiDelegate> ptrs.glGetStringi
            res.glGetSynciv <- if ptrs.glGetSynciv = 0n then glGetSyncivDelegate(fun _ _ _ _ _ -> failwith "glGetSynciv not found") else getDelegate<glGetSyncivDelegate> ptrs.glGetSynciv
            res.glGetTransformFeedbackVarying <- if ptrs.glGetTransformFeedbackVarying = 0n then glGetTransformFeedbackVaryingDelegate(fun _ _ _ _ _ _ _ -> failwith "glGetTransformFeedbackVarying not found") else getDelegate<glGetTransformFeedbackVaryingDelegate> ptrs.glGetTransformFeedbackVarying
            res.glGetUniformuiv <- if ptrs.glGetUniformuiv = 0n then glGetUniformuivDelegate(fun _ _ _ -> failwith "glGetUniformuiv not found") else getDelegate<glGetUniformuivDelegate> ptrs.glGetUniformuiv
            res.glGetUniformBlockIndex <- if ptrs.glGetUniformBlockIndex = 0n then glGetUniformBlockIndexDelegate(fun _ _ -> failwith "glGetUniformBlockIndex not found") else getDelegate<glGetUniformBlockIndexDelegate> ptrs.glGetUniformBlockIndex
            res.glGetUniformIndices <- if ptrs.glGetUniformIndices = 0n then glGetUniformIndicesDelegate(fun _ _ _ _ -> failwith "glGetUniformIndices not found") else getDelegate<glGetUniformIndicesDelegate> ptrs.glGetUniformIndices
            res.glGetVertexAttribIiv <- if ptrs.glGetVertexAttribIiv = 0n then glGetVertexAttribIivDelegate(fun _ _ _ -> failwith "glGetVertexAttribIiv not found") else getDelegate<glGetVertexAttribIivDelegate> ptrs.glGetVertexAttribIiv
            res.glGetVertexAttribIuiv <- if ptrs.glGetVertexAttribIuiv = 0n then glGetVertexAttribIuivDelegate(fun _ _ _ -> failwith "glGetVertexAttribIuiv not found") else getDelegate<glGetVertexAttribIuivDelegate> ptrs.glGetVertexAttribIuiv
            res.glInvalidateFramebuffer <- if ptrs.glInvalidateFramebuffer = 0n then glInvalidateFramebufferDelegate(fun _ _ _ -> failwith "glInvalidateFramebuffer not found") else getDelegate<glInvalidateFramebufferDelegate> ptrs.glInvalidateFramebuffer
            res.glInvalidateSubFramebuffer <- if ptrs.glInvalidateSubFramebuffer = 0n then glInvalidateSubFramebufferDelegate(fun _ _ _ _ _ _ _ -> failwith "glInvalidateSubFramebuffer not found") else getDelegate<glInvalidateSubFramebufferDelegate> ptrs.glInvalidateSubFramebuffer
            res.glIsQuery <- if ptrs.glIsQuery = 0n then glIsQueryDelegate(fun _ -> failwith "glIsQuery not found") else getDelegate<glIsQueryDelegate> ptrs.glIsQuery
            res.glIsSampler <- if ptrs.glIsSampler = 0n then glIsSamplerDelegate(fun _ -> failwith "glIsSampler not found") else getDelegate<glIsSamplerDelegate> ptrs.glIsSampler
            res.glIsSync <- if ptrs.glIsSync = 0n then glIsSyncDelegate(fun _ -> failwith "glIsSync not found") else getDelegate<glIsSyncDelegate> ptrs.glIsSync
            res.glIsTransformFeedback <- if ptrs.glIsTransformFeedback = 0n then glIsTransformFeedbackDelegate(fun _ -> failwith "glIsTransformFeedback not found") else getDelegate<glIsTransformFeedbackDelegate> ptrs.glIsTransformFeedback
            res.glIsVertexArray <- if ptrs.glIsVertexArray = 0n then glIsVertexArrayDelegate(fun _ -> failwith "glIsVertexArray not found") else getDelegate<glIsVertexArrayDelegate> ptrs.glIsVertexArray
            res.glPauseTransformFeedback <- if ptrs.glPauseTransformFeedback = 0n then glPauseTransformFeedbackDelegate(fun () -> failwith "glPauseTransformFeedback not found") else getDelegate<glPauseTransformFeedbackDelegate> ptrs.glPauseTransformFeedback
            res.glProgramBinary <- if ptrs.glProgramBinary = 0n then glProgramBinaryDelegate(fun _ _ _ _ -> failwith "glProgramBinary not found") else getDelegate<glProgramBinaryDelegate> ptrs.glProgramBinary
            res.glProgramParameteri <- if ptrs.glProgramParameteri = 0n then glProgramParameteriDelegate(fun _ _ _ -> failwith "glProgramParameteri not found") else getDelegate<glProgramParameteriDelegate> ptrs.glProgramParameteri
            res.glReadBuffer <- if ptrs.glReadBuffer = 0n then glReadBufferDelegate(fun _ -> failwith "glReadBuffer not found") else getDelegate<glReadBufferDelegate> ptrs.glReadBuffer
            res.glRenderbufferStorageMultisample <- if ptrs.glRenderbufferStorageMultisample = 0n then glRenderbufferStorageMultisampleDelegate(fun _ _ _ _ _ -> failwith "glRenderbufferStorageMultisample not found") else getDelegate<glRenderbufferStorageMultisampleDelegate> ptrs.glRenderbufferStorageMultisample
            res.glResumeTransformFeedback <- if ptrs.glResumeTransformFeedback = 0n then glResumeTransformFeedbackDelegate(fun () -> failwith "glResumeTransformFeedback not found") else getDelegate<glResumeTransformFeedbackDelegate> ptrs.glResumeTransformFeedback
            res.glSamplerParameteri <- if ptrs.glSamplerParameteri = 0n then glSamplerParameteriDelegate(fun _ _ _ -> failwith "glSamplerParameteri not found") else getDelegate<glSamplerParameteriDelegate> ptrs.glSamplerParameteri
            res.glSamplerParameteriv <- if ptrs.glSamplerParameteriv = 0n then glSamplerParameterivDelegate(fun _ _ _ -> failwith "glSamplerParameteriv not found") else getDelegate<glSamplerParameterivDelegate> ptrs.glSamplerParameteriv
            res.glSamplerParameterf <- if ptrs.glSamplerParameterf = 0n then glSamplerParameterfDelegate(fun _ _ _ -> failwith "glSamplerParameterf not found") else getDelegate<glSamplerParameterfDelegate> ptrs.glSamplerParameterf
            res.glSamplerParameterfv <- if ptrs.glSamplerParameterfv = 0n then glSamplerParameterfvDelegate(fun _ _ _ -> failwith "glSamplerParameterfv not found") else getDelegate<glSamplerParameterfvDelegate> ptrs.glSamplerParameterfv
            res.glTexImage3D <- if ptrs.glTexImage3D = 0n then glTexImage3DDelegate(fun _ _ _ _ _ _ _ _ _ _ -> failwith "glTexImage3D not found") else getDelegate<glTexImage3DDelegate> ptrs.glTexImage3D
            res.glTexStorage2D <- if ptrs.glTexStorage2D = 0n then glTexStorage2DDelegate(fun _ _ _ _ _ -> failwith "glTexStorage2D not found") else getDelegate<glTexStorage2DDelegate> ptrs.glTexStorage2D
            res.glTexStorage3D <- if ptrs.glTexStorage3D = 0n then glTexStorage3DDelegate(fun _ _ _ _ _ _ -> failwith "glTexStorage3D not found") else getDelegate<glTexStorage3DDelegate> ptrs.glTexStorage3D
            res.glTexSubImage3D <- if ptrs.glTexSubImage3D = 0n then glTexSubImage3DDelegate(fun _ _ _ _ _ _ _ _ _ _ _ -> failwith "glTexSubImage3D not found") else getDelegate<glTexSubImage3DDelegate> ptrs.glTexSubImage3D
            res.glTransformFeedbackVaryings <- if ptrs.glTransformFeedbackVaryings = 0n then glTransformFeedbackVaryingsDelegate(fun _ _ _ _ -> failwith "glTransformFeedbackVaryings not found") else getDelegate<glTransformFeedbackVaryingsDelegate> ptrs.glTransformFeedbackVaryings
            res.glUniform1ui <- if ptrs.glUniform1ui = 0n then glUniform1uiDelegate(fun _ _ -> failwith "glUniform1ui not found") else getDelegate<glUniform1uiDelegate> ptrs.glUniform1ui
            res.glUniform1uiv <- if ptrs.glUniform1uiv = 0n then glUniform1uivDelegate(fun _ _ _ -> failwith "glUniform1uiv not found") else getDelegate<glUniform1uivDelegate> ptrs.glUniform1uiv
            res.glUniform2ui <- if ptrs.glUniform2ui = 0n then glUniform2uiDelegate(fun _ _ _ -> failwith "glUniform2ui not found") else getDelegate<glUniform2uiDelegate> ptrs.glUniform2ui
            res.glUniform2uiv <- if ptrs.glUniform2uiv = 0n then glUniform2uivDelegate(fun _ _ _ -> failwith "glUniform2uiv not found") else getDelegate<glUniform2uivDelegate> ptrs.glUniform2uiv
            res.glUniform3ui <- if ptrs.glUniform3ui = 0n then glUniform3uiDelegate(fun _ _ _ _ -> failwith "glUniform3ui not found") else getDelegate<glUniform3uiDelegate> ptrs.glUniform3ui
            res.glUniform3uiv <- if ptrs.glUniform3uiv = 0n then glUniform3uivDelegate(fun _ _ _ -> failwith "glUniform3uiv not found") else getDelegate<glUniform3uivDelegate> ptrs.glUniform3uiv
            res.glUniform4ui <- if ptrs.glUniform4ui = 0n then glUniform4uiDelegate(fun _ _ _ _ _ -> failwith "glUniform4ui not found") else getDelegate<glUniform4uiDelegate> ptrs.glUniform4ui
            res.glUniform4uiv <- if ptrs.glUniform4uiv = 0n then glUniform4uivDelegate(fun _ _ _ -> failwith "glUniform4uiv not found") else getDelegate<glUniform4uivDelegate> ptrs.glUniform4uiv
            res.glUniformBlockBinding <- if ptrs.glUniformBlockBinding = 0n then glUniformBlockBindingDelegate(fun _ _ _ -> failwith "glUniformBlockBinding not found") else getDelegate<glUniformBlockBindingDelegate> ptrs.glUniformBlockBinding
            res.glUniformMatrix2x3fv <- if ptrs.glUniformMatrix2x3fv = 0n then glUniformMatrix2x3fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix2x3fv not found") else getDelegate<glUniformMatrix2x3fvDelegate> ptrs.glUniformMatrix2x3fv
            res.glUniformMatrix2x4fv <- if ptrs.glUniformMatrix2x4fv = 0n then glUniformMatrix2x4fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix2x4fv not found") else getDelegate<glUniformMatrix2x4fvDelegate> ptrs.glUniformMatrix2x4fv
            res.glUniformMatrix3x2fv <- if ptrs.glUniformMatrix3x2fv = 0n then glUniformMatrix3x2fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix3x2fv not found") else getDelegate<glUniformMatrix3x2fvDelegate> ptrs.glUniformMatrix3x2fv
            res.glUniformMatrix3x4fv <- if ptrs.glUniformMatrix3x4fv = 0n then glUniformMatrix3x4fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix3x4fv not found") else getDelegate<glUniformMatrix3x4fvDelegate> ptrs.glUniformMatrix3x4fv
            res.glUniformMatrix4x2fv <- if ptrs.glUniformMatrix4x2fv = 0n then glUniformMatrix4x2fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix4x2fv not found") else getDelegate<glUniformMatrix4x2fvDelegate> ptrs.glUniformMatrix4x2fv
            res.glUniformMatrix4x3fv <- if ptrs.glUniformMatrix4x3fv = 0n then glUniformMatrix4x3fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix4x3fv not found") else getDelegate<glUniformMatrix4x3fvDelegate> ptrs.glUniformMatrix4x3fv
            res.glVertexAttribDivisor <- if ptrs.glVertexAttribDivisor = 0n then glVertexAttribDivisorDelegate(fun _ _ -> failwith "glVertexAttribDivisor not found") else getDelegate<glVertexAttribDivisorDelegate> ptrs.glVertexAttribDivisor
            res.glVertexAttribI4i <- if ptrs.glVertexAttribI4i = 0n then glVertexAttribI4iDelegate(fun _ _ _ _ _ -> failwith "glVertexAttribI4i not found") else getDelegate<glVertexAttribI4iDelegate> ptrs.glVertexAttribI4i
            res.glVertexAttribI4ui <- if ptrs.glVertexAttribI4ui = 0n then glVertexAttribI4uiDelegate(fun _ _ _ _ _ -> failwith "glVertexAttribI4ui not found") else getDelegate<glVertexAttribI4uiDelegate> ptrs.glVertexAttribI4ui
            res.glVertexAttribI4iv <- if ptrs.glVertexAttribI4iv = 0n then glVertexAttribI4ivDelegate(fun _ _ -> failwith "glVertexAttribI4iv not found") else getDelegate<glVertexAttribI4ivDelegate> ptrs.glVertexAttribI4iv
            res.glVertexAttribI4uiv <- if ptrs.glVertexAttribI4uiv = 0n then glVertexAttribI4uivDelegate(fun _ _ -> failwith "glVertexAttribI4uiv not found") else getDelegate<glVertexAttribI4uivDelegate> ptrs.glVertexAttribI4uiv
            res.glVertexAttribIPointer <- if ptrs.glVertexAttribIPointer = 0n then glVertexAttribIPointerDelegate(fun _ _ _ _ _ -> failwith "glVertexAttribIPointer not found") else getDelegate<glVertexAttribIPointerDelegate> ptrs.glVertexAttribIPointer
            res.glWaitSync <- if ptrs.glWaitSync = 0n then glWaitSyncDelegate(fun _ _ _ -> failwith "glWaitSync not found") else getDelegate<glWaitSyncDelegate> ptrs.glWaitSync
            res.glActiveTexture <- if ptrs.glActiveTexture = 0n then glActiveTextureDelegate(fun _ -> failwith "glActiveTexture not found") else getDelegate<glActiveTextureDelegate> ptrs.glActiveTexture
            res.glAttachShader <- if ptrs.glAttachShader = 0n then glAttachShaderDelegate(fun _ _ -> failwith "glAttachShader not found") else getDelegate<glAttachShaderDelegate> ptrs.glAttachShader
            res.glBindAttribLocation <- if ptrs.glBindAttribLocation = 0n then glBindAttribLocationDelegate(fun _ _ _ -> failwith "glBindAttribLocation not found") else getDelegate<glBindAttribLocationDelegate> ptrs.glBindAttribLocation
            res.glBindBuffer <- if ptrs.glBindBuffer = 0n then glBindBufferDelegate(fun _ _ -> failwith "glBindBuffer not found") else getDelegate<glBindBufferDelegate> ptrs.glBindBuffer
            res.glBindFramebuffer <- if ptrs.glBindFramebuffer = 0n then glBindFramebufferDelegate(fun _ _ -> failwith "glBindFramebuffer not found") else getDelegate<glBindFramebufferDelegate> ptrs.glBindFramebuffer
            res.glBindRenderbuffer <- if ptrs.glBindRenderbuffer = 0n then glBindRenderbufferDelegate(fun _ _ -> failwith "glBindRenderbuffer not found") else getDelegate<glBindRenderbufferDelegate> ptrs.glBindRenderbuffer
            res.glBindTexture <- if ptrs.glBindTexture = 0n then glBindTextureDelegate(fun _ _ -> failwith "glBindTexture not found") else getDelegate<glBindTextureDelegate> ptrs.glBindTexture
            res.glBlendColor <- if ptrs.glBlendColor = 0n then glBlendColorDelegate(fun _ _ _ _ -> failwith "glBlendColor not found") else getDelegate<glBlendColorDelegate> ptrs.glBlendColor
            res.glBlendEquation <- if ptrs.glBlendEquation = 0n then glBlendEquationDelegate(fun _ -> failwith "glBlendEquation not found") else getDelegate<glBlendEquationDelegate> ptrs.glBlendEquation
            res.glBlendEquationSeparate <- if ptrs.glBlendEquationSeparate = 0n then glBlendEquationSeparateDelegate(fun _ _ -> failwith "glBlendEquationSeparate not found") else getDelegate<glBlendEquationSeparateDelegate> ptrs.glBlendEquationSeparate
            res.glBlendFunc <- if ptrs.glBlendFunc = 0n then glBlendFuncDelegate(fun _ _ -> failwith "glBlendFunc not found") else getDelegate<glBlendFuncDelegate> ptrs.glBlendFunc
            res.glBlendFuncSeparate <- if ptrs.glBlendFuncSeparate = 0n then glBlendFuncSeparateDelegate(fun _ _ _ _ -> failwith "glBlendFuncSeparate not found") else getDelegate<glBlendFuncSeparateDelegate> ptrs.glBlendFuncSeparate
            res.glBufferData <- if ptrs.glBufferData = 0n then glBufferDataDelegate(fun _ _ _ _ -> failwith "glBufferData not found") else getDelegate<glBufferDataDelegate> ptrs.glBufferData
            res.glBufferSubData <- if ptrs.glBufferSubData = 0n then glBufferSubDataDelegate(fun _ _ _ _ -> failwith "glBufferSubData not found") else getDelegate<glBufferSubDataDelegate> ptrs.glBufferSubData
            res.glCheckFramebufferStatus <- if ptrs.glCheckFramebufferStatus = 0n then glCheckFramebufferStatusDelegate(fun _ -> failwith "glCheckFramebufferStatus not found") else getDelegate<glCheckFramebufferStatusDelegate> ptrs.glCheckFramebufferStatus
            res.glClear <- if ptrs.glClear = 0n then glClearDelegate(fun _ -> failwith "glClear not found") else getDelegate<glClearDelegate> ptrs.glClear
            res.glClearColor <- if ptrs.glClearColor = 0n then glClearColorDelegate(fun _ _ _ _ -> failwith "glClearColor not found") else getDelegate<glClearColorDelegate> ptrs.glClearColor
            res.glClearDepthf <- if ptrs.glClearDepthf = 0n then glClearDepthfDelegate(fun _ -> failwith "glClearDepthf not found") else getDelegate<glClearDepthfDelegate> ptrs.glClearDepthf
            res.glClearStencil <- if ptrs.glClearStencil = 0n then glClearStencilDelegate(fun _ -> failwith "glClearStencil not found") else getDelegate<glClearStencilDelegate> ptrs.glClearStencil
            res.glColorMask <- if ptrs.glColorMask = 0n then glColorMaskDelegate(fun _ _ _ _ -> failwith "glColorMask not found") else getDelegate<glColorMaskDelegate> ptrs.glColorMask
            res.glCompileShader <- if ptrs.glCompileShader = 0n then glCompileShaderDelegate(fun _ -> failwith "glCompileShader not found") else getDelegate<glCompileShaderDelegate> ptrs.glCompileShader
            res.glCompressedTexImage2D <- if ptrs.glCompressedTexImage2D = 0n then glCompressedTexImage2DDelegate(fun _ _ _ _ _ _ _ _ -> failwith "glCompressedTexImage2D not found") else getDelegate<glCompressedTexImage2DDelegate> ptrs.glCompressedTexImage2D
            res.glCompressedTexSubImage2D <- if ptrs.glCompressedTexSubImage2D = 0n then glCompressedTexSubImage2DDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glCompressedTexSubImage2D not found") else getDelegate<glCompressedTexSubImage2DDelegate> ptrs.glCompressedTexSubImage2D
            res.glCopyTexImage2D <- if ptrs.glCopyTexImage2D = 0n then glCopyTexImage2DDelegate(fun _ _ _ _ _ _ _ _ -> failwith "glCopyTexImage2D not found") else getDelegate<glCopyTexImage2DDelegate> ptrs.glCopyTexImage2D
            res.glCopyTexSubImage2D <- if ptrs.glCopyTexSubImage2D = 0n then glCopyTexSubImage2DDelegate(fun _ _ _ _ _ _ _ _ -> failwith "glCopyTexSubImage2D not found") else getDelegate<glCopyTexSubImage2DDelegate> ptrs.glCopyTexSubImage2D
            res.glCreateProgram <- if ptrs.glCreateProgram = 0n then glCreateProgramDelegate(fun () -> failwith "glCreateProgram not found") else getDelegate<glCreateProgramDelegate> ptrs.glCreateProgram
            res.glCreateShader <- if ptrs.glCreateShader = 0n then glCreateShaderDelegate(fun _ -> failwith "glCreateShader not found") else getDelegate<glCreateShaderDelegate> ptrs.glCreateShader
            res.glCullFace <- if ptrs.glCullFace = 0n then glCullFaceDelegate(fun _ -> failwith "glCullFace not found") else getDelegate<glCullFaceDelegate> ptrs.glCullFace
            res.glDeleteBuffers <- if ptrs.glDeleteBuffers = 0n then glDeleteBuffersDelegate(fun _ _ -> failwith "glDeleteBuffers not found") else getDelegate<glDeleteBuffersDelegate> ptrs.glDeleteBuffers
            res.glDeleteFramebuffers <- if ptrs.glDeleteFramebuffers = 0n then glDeleteFramebuffersDelegate(fun _ _ -> failwith "glDeleteFramebuffers not found") else getDelegate<glDeleteFramebuffersDelegate> ptrs.glDeleteFramebuffers
            res.glDeleteProgram <- if ptrs.glDeleteProgram = 0n then glDeleteProgramDelegate(fun _ -> failwith "glDeleteProgram not found") else getDelegate<glDeleteProgramDelegate> ptrs.glDeleteProgram
            res.glDeleteRenderbuffers <- if ptrs.glDeleteRenderbuffers = 0n then glDeleteRenderbuffersDelegate(fun _ _ -> failwith "glDeleteRenderbuffers not found") else getDelegate<glDeleteRenderbuffersDelegate> ptrs.glDeleteRenderbuffers
            res.glDeleteShader <- if ptrs.glDeleteShader = 0n then glDeleteShaderDelegate(fun _ -> failwith "glDeleteShader not found") else getDelegate<glDeleteShaderDelegate> ptrs.glDeleteShader
            res.glDeleteTextures <- if ptrs.glDeleteTextures = 0n then glDeleteTexturesDelegate(fun _ _ -> failwith "glDeleteTextures not found") else getDelegate<glDeleteTexturesDelegate> ptrs.glDeleteTextures
            res.glDepthFunc <- if ptrs.glDepthFunc = 0n then glDepthFuncDelegate(fun _ -> failwith "glDepthFunc not found") else getDelegate<glDepthFuncDelegate> ptrs.glDepthFunc
            res.glDepthMask <- if ptrs.glDepthMask = 0n then glDepthMaskDelegate(fun _ -> failwith "glDepthMask not found") else getDelegate<glDepthMaskDelegate> ptrs.glDepthMask
            res.glDepthRangef <- if ptrs.glDepthRangef = 0n then glDepthRangefDelegate(fun _ _ -> failwith "glDepthRangef not found") else getDelegate<glDepthRangefDelegate> ptrs.glDepthRangef
            res.glDetachShader <- if ptrs.glDetachShader = 0n then glDetachShaderDelegate(fun _ _ -> failwith "glDetachShader not found") else getDelegate<glDetachShaderDelegate> ptrs.glDetachShader
            res.glDisable <- if ptrs.glDisable = 0n then glDisableDelegate(fun _ -> failwith "glDisable not found") else getDelegate<glDisableDelegate> ptrs.glDisable
            res.glDisableVertexAttribArray <- if ptrs.glDisableVertexAttribArray = 0n then glDisableVertexAttribArrayDelegate(fun _ -> failwith "glDisableVertexAttribArray not found") else getDelegate<glDisableVertexAttribArrayDelegate> ptrs.glDisableVertexAttribArray
            res.glDrawArrays <- if ptrs.glDrawArrays = 0n then glDrawArraysDelegate(fun _ _ _ -> failwith "glDrawArrays not found") else getDelegate<glDrawArraysDelegate> ptrs.glDrawArrays
            res.glDrawElements <- if ptrs.glDrawElements = 0n then glDrawElementsDelegate(fun _ _ _ _ -> failwith "glDrawElements not found") else getDelegate<glDrawElementsDelegate> ptrs.glDrawElements
            res.glEnable <- if ptrs.glEnable = 0n then glEnableDelegate(fun _ -> failwith "glEnable not found") else getDelegate<glEnableDelegate> ptrs.glEnable
            res.glEnableVertexAttribArray <- if ptrs.glEnableVertexAttribArray = 0n then glEnableVertexAttribArrayDelegate(fun _ -> failwith "glEnableVertexAttribArray not found") else getDelegate<glEnableVertexAttribArrayDelegate> ptrs.glEnableVertexAttribArray
            res.glFinish <- if ptrs.glFinish = 0n then glFinishDelegate(fun () -> failwith "glFinish not found") else getDelegate<glFinishDelegate> ptrs.glFinish
            res.glFlush <- if ptrs.glFlush = 0n then glFlushDelegate(fun () -> failwith "glFlush not found") else getDelegate<glFlushDelegate> ptrs.glFlush
            res.glFramebufferRenderbuffer <- if ptrs.glFramebufferRenderbuffer = 0n then glFramebufferRenderbufferDelegate(fun _ _ _ _ -> failwith "glFramebufferRenderbuffer not found") else getDelegate<glFramebufferRenderbufferDelegate> ptrs.glFramebufferRenderbuffer
            res.glFramebufferTexture2D <- if ptrs.glFramebufferTexture2D = 0n then glFramebufferTexture2DDelegate(fun _ _ _ _ _ -> failwith "glFramebufferTexture2D not found") else getDelegate<glFramebufferTexture2DDelegate> ptrs.glFramebufferTexture2D
            res.glFrontFace <- if ptrs.glFrontFace = 0n then glFrontFaceDelegate(fun _ -> failwith "glFrontFace not found") else getDelegate<glFrontFaceDelegate> ptrs.glFrontFace
            res.glGenBuffers <- if ptrs.glGenBuffers = 0n then glGenBuffersDelegate(fun _ _ -> failwith "glGenBuffers not found") else getDelegate<glGenBuffersDelegate> ptrs.glGenBuffers
            res.glGenerateMipmap <- if ptrs.glGenerateMipmap = 0n then glGenerateMipmapDelegate(fun _ -> failwith "glGenerateMipmap not found") else getDelegate<glGenerateMipmapDelegate> ptrs.glGenerateMipmap
            res.glGenFramebuffers <- if ptrs.glGenFramebuffers = 0n then glGenFramebuffersDelegate(fun _ _ -> failwith "glGenFramebuffers not found") else getDelegate<glGenFramebuffersDelegate> ptrs.glGenFramebuffers
            res.glGenRenderbuffers <- if ptrs.glGenRenderbuffers = 0n then glGenRenderbuffersDelegate(fun _ _ -> failwith "glGenRenderbuffers not found") else getDelegate<glGenRenderbuffersDelegate> ptrs.glGenRenderbuffers
            res.glGenTextures <- if ptrs.glGenTextures = 0n then glGenTexturesDelegate(fun _ _ -> failwith "glGenTextures not found") else getDelegate<glGenTexturesDelegate> ptrs.glGenTextures
            res.glGetActiveAttrib <- if ptrs.glGetActiveAttrib = 0n then glGetActiveAttribDelegate(fun _ _ _ _ _ _ _ -> failwith "glGetActiveAttrib not found") else getDelegate<glGetActiveAttribDelegate> ptrs.glGetActiveAttrib
            res.glGetActiveUniform <- if ptrs.glGetActiveUniform = 0n then glGetActiveUniformDelegate(fun _ _ _ _ _ _ _ -> failwith "glGetActiveUniform not found") else getDelegate<glGetActiveUniformDelegate> ptrs.glGetActiveUniform
            res.glGetAttachedShaders <- if ptrs.glGetAttachedShaders = 0n then glGetAttachedShadersDelegate(fun _ _ _ _ -> failwith "glGetAttachedShaders not found") else getDelegate<glGetAttachedShadersDelegate> ptrs.glGetAttachedShaders
            res.glGetAttribLocation <- if ptrs.glGetAttribLocation = 0n then glGetAttribLocationDelegate(fun _ _ -> failwith "glGetAttribLocation not found") else getDelegate<glGetAttribLocationDelegate> ptrs.glGetAttribLocation
            res.glGetBooleanv <- if ptrs.glGetBooleanv = 0n then glGetBooleanvDelegate(fun _ _ -> failwith "glGetBooleanv not found") else getDelegate<glGetBooleanvDelegate> ptrs.glGetBooleanv
            res.glGetBufferParameteriv <- if ptrs.glGetBufferParameteriv = 0n then glGetBufferParameterivDelegate(fun _ _ _ -> failwith "glGetBufferParameteriv not found") else getDelegate<glGetBufferParameterivDelegate> ptrs.glGetBufferParameteriv
            res.glGetError <- if ptrs.glGetError = 0n then glGetErrorDelegate(fun () -> failwith "glGetError not found") else getDelegate<glGetErrorDelegate> ptrs.glGetError
            res.glGetFloatv <- if ptrs.glGetFloatv = 0n then glGetFloatvDelegate(fun _ _ -> failwith "glGetFloatv not found") else getDelegate<glGetFloatvDelegate> ptrs.glGetFloatv
            res.glGetFramebufferAttachmentParameteriv <- if ptrs.glGetFramebufferAttachmentParameteriv = 0n then glGetFramebufferAttachmentParameterivDelegate(fun _ _ _ _ -> failwith "glGetFramebufferAttachmentParameteriv not found") else getDelegate<glGetFramebufferAttachmentParameterivDelegate> ptrs.glGetFramebufferAttachmentParameteriv
            res.glGetIntegerv <- if ptrs.glGetIntegerv = 0n then glGetIntegervDelegate(fun _ _ -> failwith "glGetIntegerv not found") else getDelegate<glGetIntegervDelegate> ptrs.glGetIntegerv
            res.glGetProgramiv <- if ptrs.glGetProgramiv = 0n then glGetProgramivDelegate(fun _ _ _ -> failwith "glGetProgramiv not found") else getDelegate<glGetProgramivDelegate> ptrs.glGetProgramiv
            res.glGetProgramInfoLog <- if ptrs.glGetProgramInfoLog = 0n then glGetProgramInfoLogDelegate(fun _ _ _ _ -> failwith "glGetProgramInfoLog not found") else getDelegate<glGetProgramInfoLogDelegate> ptrs.glGetProgramInfoLog
            res.glGetRenderbufferParameteriv <- if ptrs.glGetRenderbufferParameteriv = 0n then glGetRenderbufferParameterivDelegate(fun _ _ _ -> failwith "glGetRenderbufferParameteriv not found") else getDelegate<glGetRenderbufferParameterivDelegate> ptrs.glGetRenderbufferParameteriv
            res.glGetShaderiv <- if ptrs.glGetShaderiv = 0n then glGetShaderivDelegate(fun _ _ _ -> failwith "glGetShaderiv not found") else getDelegate<glGetShaderivDelegate> ptrs.glGetShaderiv
            res.glGetShaderInfoLog <- if ptrs.glGetShaderInfoLog = 0n then glGetShaderInfoLogDelegate(fun _ _ _ _ -> failwith "glGetShaderInfoLog not found") else getDelegate<glGetShaderInfoLogDelegate> ptrs.glGetShaderInfoLog
            res.glGetShaderPrecisionFormat <- if ptrs.glGetShaderPrecisionFormat = 0n then glGetShaderPrecisionFormatDelegate(fun _ _ _ _ -> failwith "glGetShaderPrecisionFormat not found") else getDelegate<glGetShaderPrecisionFormatDelegate> ptrs.glGetShaderPrecisionFormat
            res.glGetShaderSource <- if ptrs.glGetShaderSource = 0n then glGetShaderSourceDelegate(fun _ _ _ _ -> failwith "glGetShaderSource not found") else getDelegate<glGetShaderSourceDelegate> ptrs.glGetShaderSource
            res.glGetString <- if ptrs.glGetString = 0n then glGetStringDelegate(fun _ -> failwith "glGetString not found") else getDelegate<glGetStringDelegate> ptrs.glGetString
            res.glGetTexParameterfv <- if ptrs.glGetTexParameterfv = 0n then glGetTexParameterfvDelegate(fun _ _ _ -> failwith "glGetTexParameterfv not found") else getDelegate<glGetTexParameterfvDelegate> ptrs.glGetTexParameterfv
            res.glGetTexParameteriv <- if ptrs.glGetTexParameteriv = 0n then glGetTexParameterivDelegate(fun _ _ _ -> failwith "glGetTexParameteriv not found") else getDelegate<glGetTexParameterivDelegate> ptrs.glGetTexParameteriv
            res.glGetUniformfv <- if ptrs.glGetUniformfv = 0n then glGetUniformfvDelegate(fun _ _ _ -> failwith "glGetUniformfv not found") else getDelegate<glGetUniformfvDelegate> ptrs.glGetUniformfv
            res.glGetUniformiv <- if ptrs.glGetUniformiv = 0n then glGetUniformivDelegate(fun _ _ _ -> failwith "glGetUniformiv not found") else getDelegate<glGetUniformivDelegate> ptrs.glGetUniformiv
            res.glGetUniformLocation <- if ptrs.glGetUniformLocation = 0n then glGetUniformLocationDelegate(fun _ _ -> failwith "glGetUniformLocation not found") else getDelegate<glGetUniformLocationDelegate> ptrs.glGetUniformLocation
            res.glGetVertexAttribfv <- if ptrs.glGetVertexAttribfv = 0n then glGetVertexAttribfvDelegate(fun _ _ _ -> failwith "glGetVertexAttribfv not found") else getDelegate<glGetVertexAttribfvDelegate> ptrs.glGetVertexAttribfv
            res.glGetVertexAttribiv <- if ptrs.glGetVertexAttribiv = 0n then glGetVertexAttribivDelegate(fun _ _ _ -> failwith "glGetVertexAttribiv not found") else getDelegate<glGetVertexAttribivDelegate> ptrs.glGetVertexAttribiv
            res.glGetVertexAttribPointerv <- if ptrs.glGetVertexAttribPointerv = 0n then glGetVertexAttribPointervDelegate(fun _ _ _ -> failwith "glGetVertexAttribPointerv not found") else getDelegate<glGetVertexAttribPointervDelegate> ptrs.glGetVertexAttribPointerv
            res.glHint <- if ptrs.glHint = 0n then glHintDelegate(fun _ _ -> failwith "glHint not found") else getDelegate<glHintDelegate> ptrs.glHint
            res.glIsBuffer <- if ptrs.glIsBuffer = 0n then glIsBufferDelegate(fun _ -> failwith "glIsBuffer not found") else getDelegate<glIsBufferDelegate> ptrs.glIsBuffer
            res.glIsEnabled <- if ptrs.glIsEnabled = 0n then glIsEnabledDelegate(fun _ -> failwith "glIsEnabled not found") else getDelegate<glIsEnabledDelegate> ptrs.glIsEnabled
            res.glIsFramebuffer <- if ptrs.glIsFramebuffer = 0n then glIsFramebufferDelegate(fun _ -> failwith "glIsFramebuffer not found") else getDelegate<glIsFramebufferDelegate> ptrs.glIsFramebuffer
            res.glIsProgram <- if ptrs.glIsProgram = 0n then glIsProgramDelegate(fun _ -> failwith "glIsProgram not found") else getDelegate<glIsProgramDelegate> ptrs.glIsProgram
            res.glIsRenderbuffer <- if ptrs.glIsRenderbuffer = 0n then glIsRenderbufferDelegate(fun _ -> failwith "glIsRenderbuffer not found") else getDelegate<glIsRenderbufferDelegate> ptrs.glIsRenderbuffer
            res.glIsShader <- if ptrs.glIsShader = 0n then glIsShaderDelegate(fun _ -> failwith "glIsShader not found") else getDelegate<glIsShaderDelegate> ptrs.glIsShader
            res.glIsTexture <- if ptrs.glIsTexture = 0n then glIsTextureDelegate(fun _ -> failwith "glIsTexture not found") else getDelegate<glIsTextureDelegate> ptrs.glIsTexture
            res.glLineWidth <- if ptrs.glLineWidth = 0n then glLineWidthDelegate(fun _ -> failwith "glLineWidth not found") else getDelegate<glLineWidthDelegate> ptrs.glLineWidth
            res.glLinkProgram <- if ptrs.glLinkProgram = 0n then glLinkProgramDelegate(fun _ -> failwith "glLinkProgram not found") else getDelegate<glLinkProgramDelegate> ptrs.glLinkProgram
            res.glPixelStorei <- if ptrs.glPixelStorei = 0n then glPixelStoreiDelegate(fun _ _ -> failwith "glPixelStorei not found") else getDelegate<glPixelStoreiDelegate> ptrs.glPixelStorei
            res.glPolygonOffset <- if ptrs.glPolygonOffset = 0n then glPolygonOffsetDelegate(fun _ _ -> failwith "glPolygonOffset not found") else getDelegate<glPolygonOffsetDelegate> ptrs.glPolygonOffset
            res.glReadPixels <- if ptrs.glReadPixels = 0n then glReadPixelsDelegate(fun _ _ _ _ _ _ _ -> failwith "glReadPixels not found") else getDelegate<glReadPixelsDelegate> ptrs.glReadPixels
            res.glReleaseShaderCompiler <- if ptrs.glReleaseShaderCompiler = 0n then glReleaseShaderCompilerDelegate(fun () -> failwith "glReleaseShaderCompiler not found") else getDelegate<glReleaseShaderCompilerDelegate> ptrs.glReleaseShaderCompiler
            res.glRenderbufferStorage <- if ptrs.glRenderbufferStorage = 0n then glRenderbufferStorageDelegate(fun _ _ _ _ -> failwith "glRenderbufferStorage not found") else getDelegate<glRenderbufferStorageDelegate> ptrs.glRenderbufferStorage
            res.glSampleCoverage <- if ptrs.glSampleCoverage = 0n then glSampleCoverageDelegate(fun _ _ -> failwith "glSampleCoverage not found") else getDelegate<glSampleCoverageDelegate> ptrs.glSampleCoverage
            res.glScissor <- if ptrs.glScissor = 0n then glScissorDelegate(fun _ _ _ _ -> failwith "glScissor not found") else getDelegate<glScissorDelegate> ptrs.glScissor
            res.glShaderBinary <- if ptrs.glShaderBinary = 0n then glShaderBinaryDelegate(fun _ _ _ _ _ -> failwith "glShaderBinary not found") else getDelegate<glShaderBinaryDelegate> ptrs.glShaderBinary
            res.glShaderSource <- if ptrs.glShaderSource = 0n then glShaderSourceDelegate(fun _ _ _ _ -> failwith "glShaderSource not found") else getDelegate<glShaderSourceDelegate> ptrs.glShaderSource
            res.glStencilFunc <- if ptrs.glStencilFunc = 0n then glStencilFuncDelegate(fun _ _ _ -> failwith "glStencilFunc not found") else getDelegate<glStencilFuncDelegate> ptrs.glStencilFunc
            res.glStencilFuncSeparate <- if ptrs.glStencilFuncSeparate = 0n then glStencilFuncSeparateDelegate(fun _ _ _ _ -> failwith "glStencilFuncSeparate not found") else getDelegate<glStencilFuncSeparateDelegate> ptrs.glStencilFuncSeparate
            res.glStencilMask <- if ptrs.glStencilMask = 0n then glStencilMaskDelegate(fun _ -> failwith "glStencilMask not found") else getDelegate<glStencilMaskDelegate> ptrs.glStencilMask
            res.glStencilMaskSeparate <- if ptrs.glStencilMaskSeparate = 0n then glStencilMaskSeparateDelegate(fun _ _ -> failwith "glStencilMaskSeparate not found") else getDelegate<glStencilMaskSeparateDelegate> ptrs.glStencilMaskSeparate
            res.glStencilOp <- if ptrs.glStencilOp = 0n then glStencilOpDelegate(fun _ _ _ -> failwith "glStencilOp not found") else getDelegate<glStencilOpDelegate> ptrs.glStencilOp
            res.glStencilOpSeparate <- if ptrs.glStencilOpSeparate = 0n then glStencilOpSeparateDelegate(fun _ _ _ _ -> failwith "glStencilOpSeparate not found") else getDelegate<glStencilOpSeparateDelegate> ptrs.glStencilOpSeparate
            res.glTexImage2D <- if ptrs.glTexImage2D = 0n then glTexImage2DDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glTexImage2D not found") else getDelegate<glTexImage2DDelegate> ptrs.glTexImage2D
            res.glTexParameterf <- if ptrs.glTexParameterf = 0n then glTexParameterfDelegate(fun _ _ _ -> failwith "glTexParameterf not found") else getDelegate<glTexParameterfDelegate> ptrs.glTexParameterf
            res.glTexParameterfv <- if ptrs.glTexParameterfv = 0n then glTexParameterfvDelegate(fun _ _ _ -> failwith "glTexParameterfv not found") else getDelegate<glTexParameterfvDelegate> ptrs.glTexParameterfv
            res.glTexParameteri <- if ptrs.glTexParameteri = 0n then glTexParameteriDelegate(fun _ _ _ -> failwith "glTexParameteri not found") else getDelegate<glTexParameteriDelegate> ptrs.glTexParameteri
            res.glTexParameteriv <- if ptrs.glTexParameteriv = 0n then glTexParameterivDelegate(fun _ _ _ -> failwith "glTexParameteriv not found") else getDelegate<glTexParameterivDelegate> ptrs.glTexParameteriv
            res.glTexSubImage2D <- if ptrs.glTexSubImage2D = 0n then glTexSubImage2DDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glTexSubImage2D not found") else getDelegate<glTexSubImage2DDelegate> ptrs.glTexSubImage2D
            res.glUniform1f <- if ptrs.glUniform1f = 0n then glUniform1fDelegate(fun _ _ -> failwith "glUniform1f not found") else getDelegate<glUniform1fDelegate> ptrs.glUniform1f
            res.glUniform1fv <- if ptrs.glUniform1fv = 0n then glUniform1fvDelegate(fun _ _ _ -> failwith "glUniform1fv not found") else getDelegate<glUniform1fvDelegate> ptrs.glUniform1fv
            res.glUniform1i <- if ptrs.glUniform1i = 0n then glUniform1iDelegate(fun _ _ -> failwith "glUniform1i not found") else getDelegate<glUniform1iDelegate> ptrs.glUniform1i
            res.glUniform1iv <- if ptrs.glUniform1iv = 0n then glUniform1ivDelegate(fun _ _ _ -> failwith "glUniform1iv not found") else getDelegate<glUniform1ivDelegate> ptrs.glUniform1iv
            res.glUniform2f <- if ptrs.glUniform2f = 0n then glUniform2fDelegate(fun _ _ _ -> failwith "glUniform2f not found") else getDelegate<glUniform2fDelegate> ptrs.glUniform2f
            res.glUniform2fv <- if ptrs.glUniform2fv = 0n then glUniform2fvDelegate(fun _ _ _ -> failwith "glUniform2fv not found") else getDelegate<glUniform2fvDelegate> ptrs.glUniform2fv
            res.glUniform2i <- if ptrs.glUniform2i = 0n then glUniform2iDelegate(fun _ _ _ -> failwith "glUniform2i not found") else getDelegate<glUniform2iDelegate> ptrs.glUniform2i
            res.glUniform2iv <- if ptrs.glUniform2iv = 0n then glUniform2ivDelegate(fun _ _ _ -> failwith "glUniform2iv not found") else getDelegate<glUniform2ivDelegate> ptrs.glUniform2iv
            res.glUniform3f <- if ptrs.glUniform3f = 0n then glUniform3fDelegate(fun _ _ _ _ -> failwith "glUniform3f not found") else getDelegate<glUniform3fDelegate> ptrs.glUniform3f
            res.glUniform3fv <- if ptrs.glUniform3fv = 0n then glUniform3fvDelegate(fun _ _ _ -> failwith "glUniform3fv not found") else getDelegate<glUniform3fvDelegate> ptrs.glUniform3fv
            res.glUniform3i <- if ptrs.glUniform3i = 0n then glUniform3iDelegate(fun _ _ _ _ -> failwith "glUniform3i not found") else getDelegate<glUniform3iDelegate> ptrs.glUniform3i
            res.glUniform3iv <- if ptrs.glUniform3iv = 0n then glUniform3ivDelegate(fun _ _ _ -> failwith "glUniform3iv not found") else getDelegate<glUniform3ivDelegate> ptrs.glUniform3iv
            res.glUniform4f <- if ptrs.glUniform4f = 0n then glUniform4fDelegate(fun _ _ _ _ _ -> failwith "glUniform4f not found") else getDelegate<glUniform4fDelegate> ptrs.glUniform4f
            res.glUniform4fv <- if ptrs.glUniform4fv = 0n then glUniform4fvDelegate(fun _ _ _ -> failwith "glUniform4fv not found") else getDelegate<glUniform4fvDelegate> ptrs.glUniform4fv
            res.glUniform4i <- if ptrs.glUniform4i = 0n then glUniform4iDelegate(fun _ _ _ _ _ -> failwith "glUniform4i not found") else getDelegate<glUniform4iDelegate> ptrs.glUniform4i
            res.glUniform4iv <- if ptrs.glUniform4iv = 0n then glUniform4ivDelegate(fun _ _ _ -> failwith "glUniform4iv not found") else getDelegate<glUniform4ivDelegate> ptrs.glUniform4iv
            res.glUniformMatrix2fv <- if ptrs.glUniformMatrix2fv = 0n then glUniformMatrix2fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix2fv not found") else getDelegate<glUniformMatrix2fvDelegate> ptrs.glUniformMatrix2fv
            res.glUniformMatrix3fv <- if ptrs.glUniformMatrix3fv = 0n then glUniformMatrix3fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix3fv not found") else getDelegate<glUniformMatrix3fvDelegate> ptrs.glUniformMatrix3fv
            res.glUniformMatrix4fv <- if ptrs.glUniformMatrix4fv = 0n then glUniformMatrix4fvDelegate(fun _ _ _ _ -> failwith "glUniformMatrix4fv not found") else getDelegate<glUniformMatrix4fvDelegate> ptrs.glUniformMatrix4fv
            res.glUseProgram <- if ptrs.glUseProgram = 0n then glUseProgramDelegate(fun _ -> failwith "glUseProgram not found") else getDelegate<glUseProgramDelegate> ptrs.glUseProgram
            res.glValidateProgram <- if ptrs.glValidateProgram = 0n then glValidateProgramDelegate(fun _ -> failwith "glValidateProgram not found") else getDelegate<glValidateProgramDelegate> ptrs.glValidateProgram
            res.glVertexAttrib1f <- if ptrs.glVertexAttrib1f = 0n then glVertexAttrib1fDelegate(fun _ _ -> failwith "glVertexAttrib1f not found") else getDelegate<glVertexAttrib1fDelegate> ptrs.glVertexAttrib1f
            res.glVertexAttrib1fv <- if ptrs.glVertexAttrib1fv = 0n then glVertexAttrib1fvDelegate(fun _ _ -> failwith "glVertexAttrib1fv not found") else getDelegate<glVertexAttrib1fvDelegate> ptrs.glVertexAttrib1fv
            res.glVertexAttrib2f <- if ptrs.glVertexAttrib2f = 0n then glVertexAttrib2fDelegate(fun _ _ _ -> failwith "glVertexAttrib2f not found") else getDelegate<glVertexAttrib2fDelegate> ptrs.glVertexAttrib2f
            res.glVertexAttrib2fv <- if ptrs.glVertexAttrib2fv = 0n then glVertexAttrib2fvDelegate(fun _ _ -> failwith "glVertexAttrib2fv not found") else getDelegate<glVertexAttrib2fvDelegate> ptrs.glVertexAttrib2fv
            res.glVertexAttrib3f <- if ptrs.glVertexAttrib3f = 0n then glVertexAttrib3fDelegate(fun _ _ _ _ -> failwith "glVertexAttrib3f not found") else getDelegate<glVertexAttrib3fDelegate> ptrs.glVertexAttrib3f
            res.glVertexAttrib3fv <- if ptrs.glVertexAttrib3fv = 0n then glVertexAttrib3fvDelegate(fun _ _ -> failwith "glVertexAttrib3fv not found") else getDelegate<glVertexAttrib3fvDelegate> ptrs.glVertexAttrib3fv
            res.glVertexAttrib4f <- if ptrs.glVertexAttrib4f = 0n then glVertexAttrib4fDelegate(fun _ _ _ _ _ -> failwith "glVertexAttrib4f not found") else getDelegate<glVertexAttrib4fDelegate> ptrs.glVertexAttrib4f
            res.glVertexAttrib4fv <- if ptrs.glVertexAttrib4fv = 0n then glVertexAttrib4fvDelegate(fun _ _ -> failwith "glVertexAttrib4fv not found") else getDelegate<glVertexAttrib4fvDelegate> ptrs.glVertexAttrib4fv
            res.glVertexAttribPointer <- if ptrs.glVertexAttribPointer = 0n then glVertexAttribPointerDelegate(fun _ _ _ _ _ _ -> failwith "glVertexAttribPointer not found") else getDelegate<glVertexAttribPointerDelegate> ptrs.glVertexAttribPointer
            res.glViewport <- if ptrs.glViewport = 0n then glViewportDelegate(fun _ _ _ _ -> failwith "glViewport not found") else getDelegate<glViewportDelegate> ptrs.glViewport
            res.glGetBufferSubData <- if ptrs.glGetBufferSubData = 0n then glGetBufferSubDataDelegate(fun _ _ _ _ -> failwith "glGetBufferSubData not found") else getDelegate<glGetBufferSubDataDelegate> ptrs.glGetBufferSubData
            res.glMultiDrawArraysIndirect <- if ptrs.glMultiDrawArraysIndirect = 0n then glMultiDrawArraysIndirectDelegate(fun _ _ _ _ -> failwith "glMultiDrawArraysIndirect not found") else getDelegate<glMultiDrawArraysIndirectDelegate> ptrs.glMultiDrawArraysIndirect
            res.glMultiDrawArrays <- if ptrs.glMultiDrawArrays = 0n then glMultiDrawArraysDelegate(fun _ _ _ _ -> failwith "glMultiDrawArrays not found") else getDelegate<glMultiDrawArraysDelegate> ptrs.glMultiDrawArrays
            res.glMultiDrawElementsIndirect <- if ptrs.glMultiDrawElementsIndirect = 0n then glMultiDrawElementsIndirectDelegate(fun _ _ _ _ _ -> failwith "glMultiDrawElementsIndirect not found") else getDelegate<glMultiDrawElementsIndirectDelegate> ptrs.glMultiDrawElementsIndirect
            res.glMultiDrawElements <- if ptrs.glMultiDrawElements = 0n then glMultiDrawElementsDelegate(fun _ _ _ _ _ -> failwith "glMultiDrawElements not found") else getDelegate<glMultiDrawElementsDelegate> ptrs.glMultiDrawElements
            res.glCommit <- if ptrs.glCommit = 0n then glCommitDelegate(fun () -> failwith "glCommit not found") else getDelegate<glCommitDelegate> ptrs.glCommit
            res.glTexSubImage2DJSImage <- if ptrs.glTexSubImage2DJSImage = 0n then glTexSubImage2DJSImageDelegate(fun _ _ _ _ _ _ _ _ _ -> failwith "glTexSubImage2DJSImage not found") else getDelegate<glTexSubImage2DJSImageDelegate> ptrs.glTexSubImage2DJSImage
            res
        )
type ManagedCommandEncoder(device : Device) =
    inherit CommandEncoder(device)
    let gl = GLDelegates.get device.Context
    let mutable currentGL = Unchecked.defaultof<_>
    let commands = System.Collections.Generic.List<unit -> unit>()
    let mutable stack : list<int64> = []

    override x.Destroy() =
        commands.Clear()

    override x.Clear() =
        commands.Clear()

    override x.Perform gl =
        currentGL <- gl
        stack <- []
        for i in 0 .. commands.Count - 1 do commands.[i] ()

    override x.Custom(action : GL -> unit) =
        commands.Add (fun () -> action currentGL)

    override x.Push (location : nativeptr<'a>) =
        commands.Add <| fun () -> 
            if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack
            else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack
    
    override x.Pop (location : nativeptr<'a>) =
        commands.Add <| fun () -> 
            let h = List.head stack
            stack <- List.tail stack
            if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)
            else NativePtr.write location (Unchecked.reinterpret (int h))
    
    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = 
        let location = x.Use location
        let table =
            cases |> Map.ofList |> Map.map (fun _ action -> 
                let res = new ManagedCommandEncoder(device)
                x.AddNested res
                action (res :> CommandEncoder)
                res
            )
        let def = 
            let res = new ManagedCommandEncoder(device)
            x.AddNested res
            fallback (res :> CommandEncoder)
            res
        commands.Add <| fun () ->
            let v = location.Value
            match Map.tryFind v table with
            | Some c -> c.UnsafeRunSynchronously currentGL
            | None -> def.UnsafeRunSynchronously currentGL
    
    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) =
        commands.Add <| fun () -> Marshal.Copy(src, dst, size)
    
    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        commands.Add <| fun () -> Marshal.Copy(src.Pointer, dst.Pointer, size.Value)
    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        commands.Add <| fun () -> Marshal.Copy(src.Pointer, dst.Value, size.Value)
    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        commands.Add <| fun () -> Marshal.Copy(src.Value, dst.Pointer, size.Value)
    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        commands.Add <| fun () -> Marshal.Copy(src.Value, dst.Value, size.Value)
    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) =
        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>
        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>
        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>
        commands.Add <| fun () -> NativePtr.write res (NativePtr.read a + NativePtr.read b)
    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) =
        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>
        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>
        let c = x.Use(c).Pointer |> NativePtr.ofNativeInt<nativeint>
        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>
        commands.Add <| fun () -> NativePtr.write res (NativePtr.read a + NativePtr.read b * NativePtr.read c)
    override x.Bgra(values : aptr<byte>, count : aptr<int>) = 
        let values = x.Use(values).Pointer |> NativePtr.ofNativeInt<byte>
        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>
        commands.Add <| fun () ->
            let mutable off = 0
            for i in 0 .. NativePtr.read count - 1 do
                let t = NativePtr.get values off
                NativePtr.set values off (NativePtr.get values (off + 2))
                NativePtr.set values (off + 2) t
                off <- off + 4
    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = 
        let src = x.Use(src).Pointer |> NativePtr.ofNativeInt<byte>
        let dst = x.Use(dst).Pointer |> NativePtr.ofNativeInt<byte>
        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>
        commands.Add <| fun () ->
            let mutable off = 0
            for i in 0 .. NativePtr.read count - 1 do
                NativePtr.set dst (off + 2) (NativePtr.get src (off+0))
                NativePtr.set dst (off + 1) (NativePtr.get src (off+1))
                NativePtr.set dst (off + 0) (NativePtr.get src (off+2))
                NativePtr.set dst (off + 3) (NativePtr.get src (off+3))
                off <- off + 4
    override this.Call(func : aptr<nativeint>) =
        let tDel = DelegateType.Get([], typeof<unit>)
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [|  |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [|  |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>) =
        let tDel = DelegateType.Get([typeof<'a>], typeof<unit>)
        let arg0 = this.Use arg0
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        let arg6 = this.Use arg6
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj |] |> ignore
            )


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>, arg7 : aptr<'h>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>; typeof<'h>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        let arg6 = this.Use arg6
        let arg7 = this.Use arg7
        if func.IsVolatile then
            let func = this.Use func
            commands.Add (fun () ->
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj; arg7.Value :> obj |] |> ignore
            )
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            commands.Add (fun () ->
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj; arg7.Value :> obj |] |> ignore
            )


    override this.BeginQuery(``target`` : QueryTarget, ``id`` : uint32) = 
        commands.Add <| fun () -> gl.glBeginQuery.Invoke(``target``, ``id``)
    override this.BeginQuery(``target`` : aptr<QueryTarget>, ``id`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``id`` = this.Use ``id``
        commands.Add <| fun () -> gl.glBeginQuery.Invoke(``target``.Value, ``id``.Value)
    override this.BeginTransformFeedback(``primitiveMode`` : PrimitiveType) = 
        commands.Add <| fun () -> gl.glBeginTransformFeedback.Invoke(``primitiveMode``)
    override this.BeginTransformFeedback(``primitiveMode`` : aptr<PrimitiveType>) = 
        let ``primitiveMode`` = this.Use ``primitiveMode``
        commands.Add <| fun () -> gl.glBeginTransformFeedback.Invoke(``primitiveMode``.Value)
    override this.BindBufferBase(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32) = 
        commands.Add <| fun () -> gl.glBindBufferBase.Invoke(``target``, ``index``, ``buffer``)
    override this.BindBufferBase(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``buffer`` = this.Use ``buffer``
        commands.Add <| fun () -> gl.glBindBufferBase.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value)
    override this.BindBufferRange(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32, ``offset`` : nativeint, ``size`` : unativeint) = 
        commands.Add <| fun () -> gl.glBindBufferRange.Invoke(``target``, ``index``, ``buffer``, ``offset``, ``size``)
    override this.BindBufferRange(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``buffer`` = this.Use ``buffer``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        commands.Add <| fun () -> gl.glBindBufferRange.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value, ``offset``.Value, ``size``.Value)
    override this.BindSampler(``unit`` : uint32, ``sampler`` : uint32) = 
        commands.Add <| fun () -> gl.glBindSampler.Invoke(``unit``, ``sampler``)
    override this.BindSampler(``unit`` : aptr<uint32>, ``sampler`` : aptr<uint32>) = 
        let ``unit`` = this.Use ``unit``
        let ``sampler`` = this.Use ``sampler``
        commands.Add <| fun () -> gl.glBindSampler.Invoke(``unit``.Value, ``sampler``.Value)
    override this.BindTransformFeedback(``target`` : BindTransformFeedbackTarget, ``id`` : uint32) = 
        commands.Add <| fun () -> gl.glBindTransformFeedback.Invoke(``target``, ``id``)
    override this.BindTransformFeedback(``target`` : aptr<BindTransformFeedbackTarget>, ``id`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``id`` = this.Use ``id``
        commands.Add <| fun () -> gl.glBindTransformFeedback.Invoke(``target``.Value, ``id``.Value)
    override this.BindVertexArray(``array`` : uint32) = 
        commands.Add <| fun () -> gl.glBindVertexArray.Invoke(``array``)
    override this.BindVertexArray(``array`` : aptr<uint32>) = 
        let ``array`` = this.Use ``array``
        commands.Add <| fun () -> gl.glBindVertexArray.Invoke(``array``.Value)
    override this.BlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) = 
        commands.Add <| fun () -> WrappedCommands.glBlitFramebuffer(``srcX0``, ``srcY0``, ``srcX1``, ``srcY1``, ``dstX0``, ``dstY0``, ``dstX1``, ``dstY1``, ``mask``, ``filter``)
    override this.BlitFramebuffer(``srcX0`` : aptr<int>, ``srcY0`` : aptr<int>, ``srcX1`` : aptr<int>, ``srcY1`` : aptr<int>, ``dstX0`` : aptr<int>, ``dstY0`` : aptr<int>, ``dstX1`` : aptr<int>, ``dstY1`` : aptr<int>, ``mask`` : aptr<ClearBufferMask>, ``filter`` : aptr<BlitFramebufferFilter>) = 
        let ``srcX0`` = this.Use ``srcX0``
        let ``srcY0`` = this.Use ``srcY0``
        let ``srcX1`` = this.Use ``srcX1``
        let ``srcY1`` = this.Use ``srcY1``
        let ``dstX0`` = this.Use ``dstX0``
        let ``dstY0`` = this.Use ``dstY0``
        let ``dstX1`` = this.Use ``dstX1``
        let ``dstY1`` = this.Use ``dstY1``
        let ``mask`` = this.Use ``mask``
        let ``filter`` = this.Use ``filter``
        commands.Add <| fun () -> WrappedCommands.glBlitFramebuffer(``srcX0``.Value, ``srcY0``.Value, ``srcX1``.Value, ``srcY1``.Value, ``dstX0``.Value, ``dstY0``.Value, ``dstX1``.Value, ``dstY1``.Value, ``mask``.Value, ``filter``.Value)
    override this.ClearBufferiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glClearBufferiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<int>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glClearBufferiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferuiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glClearBufferuiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferuiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<uint32>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glClearBufferuiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferfv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glClearBufferfv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferfv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<float32>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glClearBufferfv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) = 
        commands.Add <| fun () -> WrappedCommands.glClearBufferfi(``buffer``, ``drawbuffer``, ``depth``, ``stencil``)
    override this.ClearBufferfi(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``depth`` : aptr<float32>, ``stencil`` : aptr<int>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``depth`` = this.Use ``depth``
        let ``stencil`` = this.Use ``stencil``
        commands.Add <| fun () -> WrappedCommands.glClearBufferfi(``buffer``.Value, ``drawbuffer``.Value, ``depth``.Value, ``stencil``.Value)
    override this.ClientWaitSync(``sync`` : nativeint, ``flags`` : SyncObjectMask, ``timeout`` : uint64, ``returnValue`` : nativeptr<GLEnum>) = 
        commands.Add <| fun () -> gl.glClientWaitSync.Invoke(``sync``, ``flags``, ``timeout``) |> NativePtr.write returnValue
    override this.ClientWaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncObjectMask>, ``timeout`` : aptr<uint64>, ``returnValue`` : aptr<GLEnum>) = 
        let ``sync`` = this.Use ``sync``
        let ``flags`` = this.Use ``flags``
        let ``timeout`` = this.Use ``timeout``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glClientWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glCompressedTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``imageSize``, ``data``)
    override this.CompressedTexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``border`` = this.Use ``border``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> WrappedCommands.glCompressedTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glCompressedTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``imageSize``, ``data``)
    override this.CompressedTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``format`` = this.Use ``format``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> WrappedCommands.glCompressedTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CopyBufferSubData(``readTarget`` : CopyBufferSubDataTarget, ``writeTarget`` : CopyBufferSubDataTarget, ``readOffset`` : nativeint, ``writeOffset`` : nativeint, ``size`` : unativeint) = 
        commands.Add <| fun () -> gl.glCopyBufferSubData.Invoke(``readTarget``, ``writeTarget``, ``readOffset``, ``writeOffset``, ``size``)
    override this.CopyBufferSubData(``readTarget`` : aptr<CopyBufferSubDataTarget>, ``writeTarget`` : aptr<CopyBufferSubDataTarget>, ``readOffset`` : aptr<nativeint>, ``writeOffset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``readTarget`` = this.Use ``readTarget``
        let ``writeTarget`` = this.Use ``writeTarget``
        let ``readOffset`` = this.Use ``readOffset``
        let ``writeOffset`` = this.Use ``writeOffset``
        let ``size`` = this.Use ``size``
        commands.Add <| fun () -> gl.glCopyBufferSubData.Invoke(``readTarget``.Value, ``writeTarget``.Value, ``readOffset``.Value, ``writeOffset``.Value, ``size``.Value)
    override this.CopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> WrappedCommands.glCopyTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``x``, ``y``, ``width``, ``height``)
    override this.CopyTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> WrappedCommands.glCopyTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.DeleteQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteQueries.Invoke(``n``, ``ids``)
    override this.DeleteQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        commands.Add <| fun () -> gl.glDeleteQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.DeleteSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteSamplers.Invoke(``count``, ``samplers``)
    override this.DeleteSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``samplers`` = this.Use ``samplers``
        commands.Add <| fun () -> gl.glDeleteSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
    override this.DeleteSync(``sync`` : nativeint) = 
        commands.Add <| fun () -> gl.glDeleteSync.Invoke(``sync``)
    override this.DeleteSync(``sync`` : aptr<nativeint>) = 
        let ``sync`` = this.Use ``sync``
        commands.Add <| fun () -> gl.glDeleteSync.Invoke(``sync``.Value)
    override this.DeleteTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteTransformFeedbacks.Invoke(``n``, ``ids``)
    override this.DeleteTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        commands.Add <| fun () -> gl.glDeleteTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.DeleteVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteVertexArrays.Invoke(``n``, ``arrays``)
    override this.DeleteVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``arrays`` = this.Use ``arrays``
        commands.Add <| fun () -> gl.glDeleteVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
    override this.DrawArraysInstanced(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32, ``instancecount`` : uint32) = 
        commands.Add <| fun () -> gl.glDrawArraysInstanced.Invoke(``mode``, ``first``, ``count``, ``instancecount``)
    override this.DrawArraysInstanced(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>, ``instancecount`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``first`` = this.Use ``first``
        let ``count`` = this.Use ``count``
        let ``instancecount`` = this.Use ``instancecount``
        commands.Add <| fun () -> gl.glDrawArraysInstanced.Invoke(``mode``.Value, ``first``.Value, ``count``.Value, ``instancecount``.Value)
    override this.DrawBuffers(``n`` : uint32, ``bufs`` : nativeptr<GLEnum>) = 
        commands.Add <| fun () -> gl.glDrawBuffers.Invoke(``n``, ``bufs``)
    override this.DrawBuffers(``n`` : aptr<uint32>, ``bufs`` : aptr<nativeptr<GLEnum>>) = 
        let ``n`` = this.Use ``n``
        let ``bufs`` = this.Use ``bufs``
        commands.Add <| fun () -> gl.glDrawBuffers.Invoke(``n``.Value, ``bufs``.Value)
    override this.DrawElementsInstanced(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint, ``instancecount`` : uint32) = 
        commands.Add <| fun () -> gl.glDrawElementsInstanced.Invoke(``mode``, ``count``, ``type``, ``indices``, ``instancecount``)
    override this.DrawElementsInstanced(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>, ``instancecount`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        let ``instancecount`` = this.Use ``instancecount``
        commands.Add <| fun () -> gl.glDrawElementsInstanced.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value, ``instancecount``.Value)
    override this.DrawRangeElements(``mode`` : PrimitiveType, ``start`` : uint32, ``end`` : uint32, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        commands.Add <| fun () -> gl.glDrawRangeElements.Invoke(``mode``, ``start``, ``end``, ``count``, ``type``, ``indices``)
    override this.DrawRangeElements(``mode`` : aptr<PrimitiveType>, ``start`` : aptr<uint32>, ``end`` : aptr<uint32>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``start`` = this.Use ``start``
        let ``end`` = this.Use ``end``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        commands.Add <| fun () -> gl.glDrawRangeElements.Invoke(``mode``.Value, ``start``.Value, ``end``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
    override this.EndQuery(``target`` : QueryTarget) = 
        commands.Add <| fun () -> gl.glEndQuery.Invoke(``target``)
    override this.EndQuery(``target`` : aptr<QueryTarget>) = 
        let ``target`` = this.Use ``target``
        commands.Add <| fun () -> gl.glEndQuery.Invoke(``target``.Value)
    override this.EndTransformFeedback() = 
        commands.Add <| fun () -> gl.glEndTransformFeedback.Invoke()
    override this.FenceSync(``condition`` : SyncCondition, ``flags`` : SyncBehaviorFlags, ``returnValue`` : nativeptr<nativeint>) = 
        commands.Add <| fun () -> gl.glFenceSync.Invoke(``condition``, ``flags``) |> NativePtr.write returnValue
    override this.FenceSync(``condition`` : aptr<SyncCondition>, ``flags`` : aptr<SyncBehaviorFlags>, ``returnValue`` : aptr<nativeint>) = 
        let ``condition`` = this.Use ``condition``
        let ``flags`` = this.Use ``flags``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glFenceSync.Invoke(``condition``.Value, ``flags``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.FramebufferTextureLayer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``texture`` : uint32, ``level`` : int, ``layer`` : int) = 
        commands.Add <| fun () -> gl.glFramebufferTextureLayer.Invoke(``target``, ``attachment``, ``texture``, ``level``, ``layer``)
    override this.FramebufferTextureLayer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>, ``layer`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``texture`` = this.Use ``texture``
        let ``level`` = this.Use ``level``
        let ``layer`` = this.Use ``layer``
        commands.Add <| fun () -> gl.glFramebufferTextureLayer.Invoke(``target``.Value, ``attachment``.Value, ``texture``.Value, ``level``.Value, ``layer``.Value)
    override this.GenQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenQueries.Invoke(``n``, ``ids``)
    override this.GenQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        commands.Add <| fun () -> gl.glGenQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.GenSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenSamplers.Invoke(``count``, ``samplers``)
    override this.GenSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``samplers`` = this.Use ``samplers``
        commands.Add <| fun () -> gl.glGenSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
    override this.GenTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenTransformFeedbacks.Invoke(``n``, ``ids``)
    override this.GenTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        commands.Add <| fun () -> gl.glGenTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.GenVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenVertexArrays.Invoke(``n``, ``arrays``)
    override this.GenVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``arrays`` = this.Use ``arrays``
        commands.Add <| fun () -> gl.glGenVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
    override this.GetActiveUniformBlockiv(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``pname`` : UniformBlockPName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetActiveUniformBlockiv.Invoke(``program``, ``uniformBlockIndex``, ``pname``, ``params``)
    override this.GetActiveUniformBlockiv(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``pname`` : aptr<UniformBlockPName>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetActiveUniformBlockiv.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetActiveUniformBlockName(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``uniformBlockName`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> gl.glGetActiveUniformBlockName.Invoke(``program``, ``uniformBlockIndex``, ``bufSize``, ``length``, ``uniformBlockName``)
    override this.GetActiveUniformBlockName(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``uniformBlockName`` = this.Use ``uniformBlockName``
        commands.Add <| fun () -> gl.glGetActiveUniformBlockName.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``uniformBlockName``.Pointer)
    override this.GetActiveUniformsiv(``program`` : uint32, ``uniformCount`` : uint32, ``uniformIndices`` : nativeptr<uint32>, ``pname`` : UniformPName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetActiveUniformsiv.Invoke(``program``, ``uniformCount``, ``uniformIndices``, ``pname``, ``params``)
    override this.GetActiveUniformsiv(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformIndices`` : aptr<uint32>, ``pname`` : aptr<UniformPName>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``uniformCount`` = this.Use ``uniformCount``
        let ``uniformIndices`` = this.Use ``uniformIndices``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetActiveUniformsiv.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformIndices``.Pointer, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetBufferParameteri64v(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int64>) = 
        commands.Add <| fun () -> gl.glGetBufferParameteri64v.Invoke(``target``, ``pname``, ``params``)
    override this.GetBufferParameteri64v(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int64>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetBufferParameteri64v.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetFragDataLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetFragDataLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetFragDataLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetFragDataLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetIntegeri_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetIntegeri_v.Invoke(``target``, ``index``, ``data``)
    override this.GetIntegeri_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetIntegeri_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInteger64v(``pname`` : GetPName, ``data`` : nativeptr<int64>) = 
        commands.Add <| fun () -> gl.glGetInteger64v.Invoke(``pname``, ``data``)
    override this.GetInteger64v(``pname`` : aptr<GetPName>, ``data`` : aptr<int64>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetInteger64v.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInteger64i_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int64>) = 
        commands.Add <| fun () -> gl.glGetInteger64i_v.Invoke(``target``, ``index``, ``data``)
    override this.GetInteger64i_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int64>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetInteger64i_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInternalformativ(``target`` : TextureTarget, ``internalformat`` : InternalFormat, ``pname`` : InternalFormatPName, ``count`` : uint32, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetInternalformativ.Invoke(``target``, ``internalformat``, ``pname``, ``count``, ``params``)
    override this.GetInternalformativ(``target`` : aptr<TextureTarget>, ``internalformat`` : aptr<InternalFormat>, ``pname`` : aptr<InternalFormatPName>, ``count`` : aptr<uint32>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``internalformat`` = this.Use ``internalformat``
        let ``pname`` = this.Use ``pname``
        let ``count`` = this.Use ``count``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetInternalformativ.Invoke(``target``.Value, ``internalformat``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetProgramBinary(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``binaryFormat`` : nativeptr<GLEnum>, ``binary`` : nativeint) = 
        commands.Add <| fun () -> gl.glGetProgramBinary.Invoke(``program``, ``bufSize``, ``length``, ``binaryFormat``, ``binary``)
    override this.GetProgramBinary(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T5>) = 
        let ``program`` = this.Use ``program``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        commands.Add <| fun () -> gl.glGetProgramBinary.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``binaryFormat``.Pointer, ``binary``.Pointer)
    override this.GetQueryiv(``target`` : QueryTarget, ``pname`` : QueryParameterName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetQueryiv.Invoke(``target``, ``pname``, ``params``)
    override this.GetQueryiv(``target`` : aptr<QueryTarget>, ``pname`` : aptr<QueryParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetQueryiv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetQueryObjectuiv(``id`` : uint32, ``pname`` : QueryObjectParameterName, ``params`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetQueryObjectuiv.Invoke(``id``, ``pname``, ``params``)
    override this.GetQueryObjectuiv(``id`` : aptr<uint32>, ``pname`` : aptr<QueryObjectParameterName>, ``params`` : aptr<uint32>) = 
        let ``id`` = this.Use ``id``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetQueryObjectuiv.Invoke(``id``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetSamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetSamplerParameteriv.Invoke(``sampler``, ``pname``, ``params``)
    override this.GetSamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``params`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetSamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``params`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glGetSamplerParameterfv.Invoke(``sampler``, ``pname``, ``params``)
    override this.GetSamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``params`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetStringi(``name`` : StringName, ``index`` : uint32, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        commands.Add <| fun () -> gl.glGetStringi.Invoke(``name``, ``index``) |> NativePtr.write returnValue
    override this.GetStringi(``name`` : aptr<StringName>, ``index`` : aptr<uint32>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        let ``name`` = this.Use ``name``
        let ``index`` = this.Use ``index``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetStringi.Invoke(``name``.Value, ``index``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetSynciv(``sync`` : nativeint, ``pname`` : SyncParameterName, ``count`` : uint32, ``length`` : nativeptr<uint32>, ``values`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetSynciv.Invoke(``sync``, ``pname``, ``count``, ``length``, ``values``)
    override this.GetSynciv(``sync`` : aptr<nativeint>, ``pname`` : aptr<SyncParameterName>, ``count`` : aptr<uint32>, ``length`` : aptr<uint32>, ``values`` : aptr<int>) = 
        let ``sync`` = this.Use ``sync``
        let ``pname`` = this.Use ``pname``
        let ``count`` = this.Use ``count``
        let ``length`` = this.Use ``length``
        let ``values`` = this.Use ``values``
        commands.Add <| fun () -> gl.glGetSynciv.Invoke(``sync``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``values``.Pointer)
    override this.GetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> WrappedCommands.glGetTransformFeedbackVarying(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetTransformFeedbackVarying(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<uint32>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        commands.Add <| fun () -> WrappedCommands.glGetTransformFeedbackVarying(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetUniformuiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetUniformuiv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformuiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetUniformuiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformBlockIndex(``program`` : uint32, ``uniformBlockName`` : nativeptr<uint8>, ``returnValue`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetUniformBlockIndex.Invoke(``program``, ``uniformBlockName``) |> NativePtr.write returnValue
    override this.GetUniformBlockIndex(``program`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>, ``returnValue`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockName`` = this.Use ``uniformBlockName``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetUniformBlockIndex.Invoke(``program``.Value, NativePtr.ofNativeInt ``uniformBlockName``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetUniformIndices(``program`` : uint32, ``uniformCount`` : uint32, ``uniformNames`` : nativeptr<nativeptr<uint8>>, ``uniformIndices`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetUniformIndices.Invoke(``program``, ``uniformCount``, ``uniformNames``, ``uniformIndices``)
    override this.GetUniformIndices(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformNames`` : aptr<nativeptr<uint8>>, ``uniformIndices`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformCount`` = this.Use ``uniformCount``
        let ``uniformNames`` = this.Use ``uniformNames``
        let ``uniformIndices`` = this.Use ``uniformIndices``
        commands.Add <| fun () -> gl.glGetUniformIndices.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformNames``.Pointer, NativePtr.ofNativeInt ``uniformIndices``.Pointer)
    override this.GetVertexAttribIiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetVertexAttribIiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribIiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetVertexAttribIiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribIuiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetVertexAttribIuiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribIuiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetVertexAttribIuiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.InvalidateFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>) = 
        commands.Add <| fun () -> gl.glInvalidateFramebuffer.Invoke(``target``, ``numAttachments``, ``attachments``)
    override this.InvalidateFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>) = 
        let ``target`` = this.Use ``target``
        let ``numAttachments`` = this.Use ``numAttachments``
        let ``attachments`` = this.Use ``attachments``
        commands.Add <| fun () -> gl.glInvalidateFramebuffer.Invoke(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer)
    override this.InvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> WrappedCommands.glInvalidateSubFramebuffer(``target``, ``numAttachments``, ``attachments``, ``x``, ``y``, ``width``, ``height``)
    override this.InvalidateSubFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``numAttachments`` = this.Use ``numAttachments``
        let ``attachments`` = this.Use ``attachments``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> WrappedCommands.glInvalidateSubFramebuffer(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.IsQuery(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsQuery.Invoke(``id``) |> NativePtr.write returnValue
    override this.IsQuery(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``id`` = this.Use ``id``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsQuery.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsSampler(``sampler`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsSampler.Invoke(``sampler``) |> NativePtr.write returnValue
    override this.IsSampler(``sampler`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsSampler.Invoke(``sampler``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsSync(``sync`` : nativeint, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsSync.Invoke(``sync``) |> NativePtr.write returnValue
    override this.IsSync(``sync`` : aptr<nativeint>, ``returnValue`` : aptr<Boolean>) = 
        let ``sync`` = this.Use ``sync``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsSync.Invoke(``sync``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsTransformFeedback(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsTransformFeedback.Invoke(``id``) |> NativePtr.write returnValue
    override this.IsTransformFeedback(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``id`` = this.Use ``id``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsTransformFeedback.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsVertexArray(``array`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsVertexArray.Invoke(``array``) |> NativePtr.write returnValue
    override this.IsVertexArray(``array`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``array`` = this.Use ``array``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsVertexArray.Invoke(``array``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.PauseTransformFeedback() = 
        commands.Add <| fun () -> gl.glPauseTransformFeedback.Invoke()
    override this.ProgramBinary(``program`` : uint32, ``binaryFormat`` : GLEnum, ``binary`` : nativeint, ``length`` : uint32) = 
        commands.Add <| fun () -> gl.glProgramBinary.Invoke(``program``, ``binaryFormat``, ``binary``, ``length``)
    override this.ProgramBinary(``program`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T3>, ``length`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        let ``length`` = this.Use ``length``
        commands.Add <| fun () -> gl.glProgramBinary.Invoke(``program``.Value, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
    override this.ProgramParameteri(``program`` : uint32, ``pname`` : ProgramParameterPName, ``value`` : int) = 
        commands.Add <| fun () -> gl.glProgramParameteri.Invoke(``program``, ``pname``, ``value``)
    override this.ProgramParameteri(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramParameterPName>, ``value`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``pname`` = this.Use ``pname``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glProgramParameteri.Invoke(``program``.Value, ``pname``.Value, ``value``.Value)
    override this.ReadBuffer(``src`` : ReadBufferMode) = 
        commands.Add <| fun () -> gl.glReadBuffer.Invoke(``src``)
    override this.ReadBuffer(``src`` : aptr<ReadBufferMode>) = 
        let ``src`` = this.Use ``src``
        commands.Add <| fun () -> gl.glReadBuffer.Invoke(``src``.Value)
    override this.RenderbufferStorageMultisample(``target`` : RenderbufferTarget, ``samples`` : uint32, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> gl.glRenderbufferStorageMultisample.Invoke(``target``, ``samples``, ``internalformat``, ``width``, ``height``)
    override this.RenderbufferStorageMultisample(``target`` : aptr<RenderbufferTarget>, ``samples`` : aptr<uint32>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``samples`` = this.Use ``samples``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> gl.glRenderbufferStorageMultisample.Invoke(``target``.Value, ``samples``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.ResumeTransformFeedback() = 
        commands.Add <| fun () -> gl.glResumeTransformFeedback.Invoke()
    override this.SamplerParameteri(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : int) = 
        commands.Add <| fun () -> gl.glSamplerParameteri.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameteri(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> gl.glSamplerParameteri.Invoke(``sampler``.Value, ``pname``.Value, ``param``.Value)
    override this.SamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glSamplerParameteriv.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> gl.glSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
    override this.SamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glSamplerParameterf(``sampler``, ``pname``, ``param``)
    override this.SamplerParameterf(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> WrappedCommands.glSamplerParameterf(``sampler``.Value, ``pname``.Value, ``param``.Value)
    override this.SamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glSamplerParameterfv.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> gl.glSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
    override this.TexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``format``, ``type``, ``pixels``)
    override this.TexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``border`` = this.Use ``border``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        commands.Add <| fun () -> WrappedCommands.glTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TexStorage2D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> gl.glTexStorage2D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``)
    override this.TexStorage2D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``levels`` = this.Use ``levels``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> gl.glTexStorage2D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.TexStorage3D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32) = 
        commands.Add <| fun () -> gl.glTexStorage3D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``, ``depth``)
    override this.TexStorage3D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``levels`` = this.Use ``levels``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        commands.Add <| fun () -> gl.glTexStorage3D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value)
    override this.TexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``type``, ``pixels``)
    override this.TexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        commands.Add <| fun () -> WrappedCommands.glTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TransformFeedbackVaryings(``program`` : uint32, ``count`` : uint32, ``varyings`` : nativeptr<nativeptr<uint8>>, ``bufferMode`` : TransformFeedbackBufferMode) = 
        commands.Add <| fun () -> gl.glTransformFeedbackVaryings.Invoke(``program``, ``count``, ``varyings``, ``bufferMode``)
    override this.TransformFeedbackVaryings(``program`` : aptr<uint32>, ``count`` : aptr<uint32>, ``varyings`` : aptr<nativeptr<uint8>>, ``bufferMode`` : aptr<TransformFeedbackBufferMode>) = 
        let ``program`` = this.Use ``program``
        let ``count`` = this.Use ``count``
        let ``varyings`` = this.Use ``varyings``
        let ``bufferMode`` = this.Use ``bufferMode``
        commands.Add <| fun () -> gl.glTransformFeedbackVaryings.Invoke(``program``.Value, ``count``.Value, NativePtr.ofNativeInt ``varyings``.Pointer, ``bufferMode``.Value)
    override this.Uniform1ui(``location`` : int, ``v0`` : uint32) = 
        commands.Add <| fun () -> gl.glUniform1ui.Invoke(``location``, ``v0``)
    override this.Uniform1ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        commands.Add <| fun () -> gl.glUniform1ui.Invoke(``location``.Value, ``v0``.Value)
    override this.Uniform1uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glUniform1uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform1uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32) = 
        commands.Add <| fun () -> gl.glUniform2ui.Invoke(``location``, ``v0``, ``v1``)
    override this.Uniform2ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        commands.Add <| fun () -> gl.glUniform2ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glUniform2uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform2uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32) = 
        commands.Add <| fun () -> gl.glUniform3ui.Invoke(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        commands.Add <| fun () -> gl.glUniform3ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glUniform3uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform3uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32, ``v3`` : uint32) = 
        commands.Add <| fun () -> gl.glUniform4ui.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>, ``v3`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        commands.Add <| fun () -> gl.glUniform4ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glUniform4uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform4uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformBlockBinding(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``uniformBlockBinding`` : uint32) = 
        commands.Add <| fun () -> gl.glUniformBlockBinding.Invoke(``program``, ``uniformBlockIndex``, ``uniformBlockBinding``)
    override this.UniformBlockBinding(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``uniformBlockBinding`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``uniformBlockBinding`` = this.Use ``uniformBlockBinding``
        commands.Add <| fun () -> gl.glUniformBlockBinding.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``uniformBlockBinding``.Value)
    override this.UniformMatrix2x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix2x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix2x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix2x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix2x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix2x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix3x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix3x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix3x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix3x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix4x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix4x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix4x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix4x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.VertexAttribDivisor(``index`` : uint32, ``divisor`` : uint32) = 
        commands.Add <| fun () -> gl.glVertexAttribDivisor.Invoke(``index``, ``divisor``)
    override this.VertexAttribDivisor(``index`` : aptr<uint32>, ``divisor`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``divisor`` = this.Use ``divisor``
        commands.Add <| fun () -> gl.glVertexAttribDivisor.Invoke(``index``.Value, ``divisor``.Value)
    override this.VertexAttribI4i(``index`` : uint32, ``x`` : int, ``y`` : int, ``z`` : int, ``w`` : int) = 
        commands.Add <| fun () -> gl.glVertexAttribI4i.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttribI4i(``index`` : aptr<uint32>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``z`` : aptr<int>, ``w`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        commands.Add <| fun () -> gl.glVertexAttribI4i.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttribI4ui(``index`` : uint32, ``x`` : uint32, ``y`` : uint32, ``z`` : uint32, ``w`` : uint32) = 
        commands.Add <| fun () -> gl.glVertexAttribI4ui.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttribI4ui(``index`` : aptr<uint32>, ``x`` : aptr<uint32>, ``y`` : aptr<uint32>, ``z`` : aptr<uint32>, ``w`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        commands.Add <| fun () -> gl.glVertexAttribI4ui.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttribI4iv(``index`` : uint32, ``v`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glVertexAttribI4iv.Invoke(``index``, ``v``)
    override this.VertexAttribI4iv(``index`` : aptr<uint32>, ``v`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttribI4iv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribI4uiv(``index`` : uint32, ``v`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glVertexAttribI4uiv.Invoke(``index``, ``v``)
    override this.VertexAttribI4uiv(``index`` : aptr<uint32>, ``v`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttribI4uiv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribIPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribIType, ``stride`` : uint32, ``pointer`` : nativeint) = 
        commands.Add <| fun () -> gl.glVertexAttribIPointer.Invoke(``index``, ``size``, ``type``, ``stride``, ``pointer``)
    override this.VertexAttribIPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribIType>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T5>) = 
        let ``index`` = this.Use ``index``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``stride`` = this.Use ``stride``
        let ``pointer`` = this.Use ``pointer``
        commands.Add <| fun () -> gl.glVertexAttribIPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``stride``.Value, ``pointer``.Pointer)
    override this.WaitSync(``sync`` : nativeint, ``flags`` : SyncBehaviorFlags, ``timeout`` : uint64) = 
        commands.Add <| fun () -> gl.glWaitSync.Invoke(``sync``, ``flags``, ``timeout``)
    override this.WaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncBehaviorFlags>, ``timeout`` : aptr<uint64>) = 
        let ``sync`` = this.Use ``sync``
        let ``flags`` = this.Use ``flags``
        let ``timeout`` = this.Use ``timeout``
        commands.Add <| fun () -> gl.glWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value)
    override this.ActiveTexture(``texture`` : TextureUnit) = 
        commands.Add <| fun () -> gl.glActiveTexture.Invoke(``texture``)
    override this.ActiveTexture(``texture`` : aptr<TextureUnit>) = 
        let ``texture`` = this.Use ``texture``
        commands.Add <| fun () -> gl.glActiveTexture.Invoke(``texture``.Value)
    override this.AttachShader(``program`` : uint32, ``shader`` : uint32) = 
        commands.Add <| fun () -> gl.glAttachShader.Invoke(``program``, ``shader``)
    override this.AttachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``shader`` = this.Use ``shader``
        commands.Add <| fun () -> gl.glAttachShader.Invoke(``program``.Value, ``shader``.Value)
    override this.BindAttribLocation(``program`` : uint32, ``index`` : uint32, ``name`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> gl.glBindAttribLocation.Invoke(``program``, ``index``, ``name``)
    override this.BindAttribLocation(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``name`` = this.Use ``name``
        commands.Add <| fun () -> gl.glBindAttribLocation.Invoke(``program``.Value, ``index``.Value, NativePtr.ofNativeInt ``name``.Pointer)
    override this.BindBuffer(``target`` : BufferTargetARB, ``buffer`` : uint32) = 
        commands.Add <| fun () -> gl.glBindBuffer.Invoke(``target``, ``buffer``)
    override this.BindBuffer(``target`` : aptr<BufferTargetARB>, ``buffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``buffer`` = this.Use ``buffer``
        commands.Add <| fun () -> gl.glBindBuffer.Invoke(``target``.Value, ``buffer``.Value)
    override this.BindFramebuffer(``target`` : FramebufferTarget, ``framebuffer`` : uint32) = 
        commands.Add <| fun () -> gl.glBindFramebuffer.Invoke(``target``, ``framebuffer``)
    override this.BindFramebuffer(``target`` : aptr<FramebufferTarget>, ``framebuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``framebuffer`` = this.Use ``framebuffer``
        commands.Add <| fun () -> gl.glBindFramebuffer.Invoke(``target``.Value, ``framebuffer``.Value)
    override this.BindRenderbuffer(``target`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        commands.Add <| fun () -> gl.glBindRenderbuffer.Invoke(``target``, ``renderbuffer``)
    override this.BindRenderbuffer(``target`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``renderbuffer`` = this.Use ``renderbuffer``
        commands.Add <| fun () -> gl.glBindRenderbuffer.Invoke(``target``.Value, ``renderbuffer``.Value)
    override this.BindTexture(``target`` : TextureTarget, ``texture`` : uint32) = 
        commands.Add <| fun () -> gl.glBindTexture.Invoke(``target``, ``texture``)
    override this.BindTexture(``target`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``texture`` = this.Use ``texture``
        commands.Add <| fun () -> gl.glBindTexture.Invoke(``target``.Value, ``texture``.Value)
    override this.BlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glBlendColor(``red``, ``green``, ``blue``, ``alpha``)
    override this.BlendColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        commands.Add <| fun () -> WrappedCommands.glBlendColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.BlendEquation(``mode`` : BlendEquationModeEXT) = 
        commands.Add <| fun () -> gl.glBlendEquation.Invoke(``mode``)
    override this.BlendEquation(``mode`` : aptr<BlendEquationModeEXT>) = 
        let ``mode`` = this.Use ``mode``
        commands.Add <| fun () -> gl.glBlendEquation.Invoke(``mode``.Value)
    override this.BlendEquationSeparate(``modeRGB`` : BlendEquationModeEXT, ``modeAlpha`` : BlendEquationModeEXT) = 
        commands.Add <| fun () -> gl.glBlendEquationSeparate.Invoke(``modeRGB``, ``modeAlpha``)
    override this.BlendEquationSeparate(``modeRGB`` : aptr<BlendEquationModeEXT>, ``modeAlpha`` : aptr<BlendEquationModeEXT>) = 
        let ``modeRGB`` = this.Use ``modeRGB``
        let ``modeAlpha`` = this.Use ``modeAlpha``
        commands.Add <| fun () -> gl.glBlendEquationSeparate.Invoke(``modeRGB``.Value, ``modeAlpha``.Value)
    override this.BlendFunc(``sfactor`` : BlendingFactor, ``dfactor`` : BlendingFactor) = 
        commands.Add <| fun () -> gl.glBlendFunc.Invoke(``sfactor``, ``dfactor``)
    override this.BlendFunc(``sfactor`` : aptr<BlendingFactor>, ``dfactor`` : aptr<BlendingFactor>) = 
        let ``sfactor`` = this.Use ``sfactor``
        let ``dfactor`` = this.Use ``dfactor``
        commands.Add <| fun () -> gl.glBlendFunc.Invoke(``sfactor``.Value, ``dfactor``.Value)
    override this.BlendFuncSeparate(``sfactorRGB`` : BlendingFactor, ``dfactorRGB`` : BlendingFactor, ``sfactorAlpha`` : BlendingFactor, ``dfactorAlpha`` : BlendingFactor) = 
        commands.Add <| fun () -> gl.glBlendFuncSeparate.Invoke(``sfactorRGB``, ``dfactorRGB``, ``sfactorAlpha``, ``dfactorAlpha``)
    override this.BlendFuncSeparate(``sfactorRGB`` : aptr<BlendingFactor>, ``dfactorRGB`` : aptr<BlendingFactor>, ``sfactorAlpha`` : aptr<BlendingFactor>, ``dfactorAlpha`` : aptr<BlendingFactor>) = 
        let ``sfactorRGB`` = this.Use ``sfactorRGB``
        let ``dfactorRGB`` = this.Use ``dfactorRGB``
        let ``sfactorAlpha`` = this.Use ``sfactorAlpha``
        let ``dfactorAlpha`` = this.Use ``dfactorAlpha``
        commands.Add <| fun () -> gl.glBlendFuncSeparate.Invoke(``sfactorRGB``.Value, ``dfactorRGB``.Value, ``sfactorAlpha``.Value, ``dfactorAlpha``.Value)
    override this.BufferData(``target`` : BufferTargetARB, ``size`` : unativeint, ``data`` : nativeint, ``usage`` : BufferUsageARB) = 
        commands.Add <| fun () -> gl.glBufferData.Invoke(``target``, ``size``, ``data``, ``usage``)
    override this.BufferData(``target`` : aptr<BufferTargetARB>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>, ``usage`` : aptr<BufferUsageARB>) = 
        let ``target`` = this.Use ``target``
        let ``size`` = this.Use ``size``
        let ``data`` = this.Use ``data``
        let ``usage`` = this.Use ``usage``
        commands.Add <| fun () -> gl.glBufferData.Invoke(``target``.Value, ``size``.Value, ``data``.Value, ``usage``.Value)
    override this.BufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``data`` : nativeint) = 
        commands.Add <| fun () -> gl.glBufferSubData.Invoke(``target``, ``offset``, ``size``, ``data``)
    override this.BufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``data``.Value)
    override this.CheckFramebufferStatus(``target`` : FramebufferTarget, ``returnValue`` : nativeptr<GLEnum>) = 
        commands.Add <| fun () -> gl.glCheckFramebufferStatus.Invoke(``target``) |> NativePtr.write returnValue
    override this.CheckFramebufferStatus(``target`` : aptr<FramebufferTarget>, ``returnValue`` : aptr<GLEnum>) = 
        let ``target`` = this.Use ``target``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glCheckFramebufferStatus.Invoke(``target``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.Clear(``mask`` : ClearBufferMask) = 
        commands.Add <| fun () -> gl.glClear.Invoke(``mask``)
    override this.Clear(``mask`` : aptr<ClearBufferMask>) = 
        let ``mask`` = this.Use ``mask``
        commands.Add <| fun () -> gl.glClear.Invoke(``mask``.Value)
    override this.ClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glClearColor(``red``, ``green``, ``blue``, ``alpha``)
    override this.ClearColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        commands.Add <| fun () -> WrappedCommands.glClearColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.ClearDepthf(``d`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glClearDepthf(``d``)
    override this.ClearDepthf(``d`` : aptr<float32>) = 
        let ``d`` = this.Use ``d``
        commands.Add <| fun () -> WrappedCommands.glClearDepthf(``d``.Value)
    override this.ClearStencil(``s`` : int) = 
        commands.Add <| fun () -> gl.glClearStencil.Invoke(``s``)
    override this.ClearStencil(``s`` : aptr<int>) = 
        let ``s`` = this.Use ``s``
        commands.Add <| fun () -> gl.glClearStencil.Invoke(``s``.Value)
    override this.ColorMask(``red`` : Boolean, ``green`` : Boolean, ``blue`` : Boolean, ``alpha`` : Boolean) = 
        commands.Add <| fun () -> gl.glColorMask.Invoke(``red``, ``green``, ``blue``, ``alpha``)
    override this.ColorMask(``red`` : aptr<Boolean>, ``green`` : aptr<Boolean>, ``blue`` : aptr<Boolean>, ``alpha`` : aptr<Boolean>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        commands.Add <| fun () -> gl.glColorMask.Invoke(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.CompileShader(``shader`` : uint32) = 
        commands.Add <| fun () -> gl.glCompileShader.Invoke(``shader``)
    override this.CompileShader(``shader`` : aptr<uint32>) = 
        let ``shader`` = this.Use ``shader``
        commands.Add <| fun () -> gl.glCompileShader.Invoke(``shader``.Value)
    override this.CompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glCompressedTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``imageSize``, ``data``)
    override this.CompressedTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> WrappedCommands.glCompressedTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glCompressedTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``imageSize``, ``data``)
    override this.CompressedTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> WrappedCommands.glCompressedTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) = 
        commands.Add <| fun () -> WrappedCommands.glCopyTexImage2D(``target``, ``level``, ``internalformat``, ``x``, ``y``, ``width``, ``height``, ``border``)
    override this.CopyTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        commands.Add <| fun () -> WrappedCommands.glCopyTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``border``.Value)
    override this.CopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> WrappedCommands.glCopyTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``x``, ``y``, ``width``, ``height``)
    override this.CopyTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> WrappedCommands.glCopyTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.CreateProgram(``returnValue`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glCreateProgram.Invoke() |> NativePtr.write returnValue
    override this.CreateProgram(``returnValue`` : aptr<uint32>) = 
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glCreateProgram.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CreateShader(``type`` : ShaderType, ``returnValue`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glCreateShader.Invoke(``type``) |> NativePtr.write returnValue
    override this.CreateShader(``type`` : aptr<ShaderType>, ``returnValue`` : aptr<uint32>) = 
        let ``type`` = this.Use ``type``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glCreateShader.Invoke(``type``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CullFace(``mode`` : CullFaceMode) = 
        commands.Add <| fun () -> gl.glCullFace.Invoke(``mode``)
    override this.CullFace(``mode`` : aptr<CullFaceMode>) = 
        let ``mode`` = this.Use ``mode``
        commands.Add <| fun () -> gl.glCullFace.Invoke(``mode``.Value)
    override this.DeleteBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteBuffers.Invoke(``n``, ``buffers``)
    override this.DeleteBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``buffers`` = this.Use ``buffers``
        commands.Add <| fun () -> gl.glDeleteBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
    override this.DeleteFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteFramebuffers.Invoke(``n``, ``framebuffers``)
    override this.DeleteFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``framebuffers`` = this.Use ``framebuffers``
        commands.Add <| fun () -> gl.glDeleteFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
    override this.DeleteProgram(``program`` : uint32) = 
        commands.Add <| fun () -> gl.glDeleteProgram.Invoke(``program``)
    override this.DeleteProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        commands.Add <| fun () -> gl.glDeleteProgram.Invoke(``program``.Value)
    override this.DeleteRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteRenderbuffers.Invoke(``n``, ``renderbuffers``)
    override this.DeleteRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``renderbuffers`` = this.Use ``renderbuffers``
        commands.Add <| fun () -> gl.glDeleteRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
    override this.DeleteShader(``shader`` : uint32) = 
        commands.Add <| fun () -> gl.glDeleteShader.Invoke(``shader``)
    override this.DeleteShader(``shader`` : aptr<uint32>) = 
        let ``shader`` = this.Use ``shader``
        commands.Add <| fun () -> gl.glDeleteShader.Invoke(``shader``.Value)
    override this.DeleteTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glDeleteTextures.Invoke(``n``, ``textures``)
    override this.DeleteTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``textures`` = this.Use ``textures``
        commands.Add <| fun () -> gl.glDeleteTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
    override this.DepthFunc(``func`` : DepthFunction) = 
        commands.Add <| fun () -> gl.glDepthFunc.Invoke(``func``)
    override this.DepthFunc(``func`` : aptr<DepthFunction>) = 
        let ``func`` = this.Use ``func``
        commands.Add <| fun () -> gl.glDepthFunc.Invoke(``func``.Value)
    override this.DepthMask(``flag`` : Boolean) = 
        commands.Add <| fun () -> gl.glDepthMask.Invoke(``flag``)
    override this.DepthMask(``flag`` : aptr<Boolean>) = 
        let ``flag`` = this.Use ``flag``
        commands.Add <| fun () -> gl.glDepthMask.Invoke(``flag``.Value)
    override this.DepthRangef(``n`` : float32, ``f`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glDepthRangef(``n``, ``f``)
    override this.DepthRangef(``n`` : aptr<float32>, ``f`` : aptr<float32>) = 
        let ``n`` = this.Use ``n``
        let ``f`` = this.Use ``f``
        commands.Add <| fun () -> WrappedCommands.glDepthRangef(``n``.Value, ``f``.Value)
    override this.DetachShader(``program`` : uint32, ``shader`` : uint32) = 
        commands.Add <| fun () -> gl.glDetachShader.Invoke(``program``, ``shader``)
    override this.DetachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``shader`` = this.Use ``shader``
        commands.Add <| fun () -> gl.glDetachShader.Invoke(``program``.Value, ``shader``.Value)
    override this.Disable(``cap`` : EnableCap) = 
        commands.Add <| fun () -> gl.glDisable.Invoke(``cap``)
    override this.Disable(``cap`` : aptr<EnableCap>) = 
        let ``cap`` = this.Use ``cap``
        commands.Add <| fun () -> gl.glDisable.Invoke(``cap``.Value)
    override this.DisableVertexAttribArray(``index`` : uint32) = 
        commands.Add <| fun () -> gl.glDisableVertexAttribArray.Invoke(``index``)
    override this.DisableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        commands.Add <| fun () -> gl.glDisableVertexAttribArray.Invoke(``index``.Value)
    override this.DrawArrays(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32) = 
        commands.Add <| fun () -> gl.glDrawArrays.Invoke(``mode``, ``first``, ``count``)
    override this.DrawArrays(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``first`` = this.Use ``first``
        let ``count`` = this.Use ``count``
        commands.Add <| fun () -> gl.glDrawArrays.Invoke(``mode``.Value, ``first``.Value, ``count``.Value)
    override this.DrawElements(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        commands.Add <| fun () -> gl.glDrawElements.Invoke(``mode``, ``count``, ``type``, ``indices``)
    override this.DrawElements(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        commands.Add <| fun () -> gl.glDrawElements.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
    override this.Enable(``cap`` : EnableCap) = 
        commands.Add <| fun () -> gl.glEnable.Invoke(``cap``)
    override this.Enable(``cap`` : aptr<EnableCap>) = 
        let ``cap`` = this.Use ``cap``
        commands.Add <| fun () -> gl.glEnable.Invoke(``cap``.Value)
    override this.EnableVertexAttribArray(``index`` : uint32) = 
        commands.Add <| fun () -> gl.glEnableVertexAttribArray.Invoke(``index``)
    override this.EnableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        commands.Add <| fun () -> gl.glEnableVertexAttribArray.Invoke(``index``.Value)
    override this.Finish() = 
        commands.Add <| fun () -> gl.glFinish.Invoke()
    override this.Flush() = 
        commands.Add <| fun () -> gl.glFlush.Invoke()
    override this.FramebufferRenderbuffer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``renderbuffertarget`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        commands.Add <| fun () -> gl.glFramebufferRenderbuffer.Invoke(``target``, ``attachment``, ``renderbuffertarget``, ``renderbuffer``)
    override this.FramebufferRenderbuffer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``renderbuffertarget`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``renderbuffertarget`` = this.Use ``renderbuffertarget``
        let ``renderbuffer`` = this.Use ``renderbuffer``
        commands.Add <| fun () -> gl.glFramebufferRenderbuffer.Invoke(``target``.Value, ``attachment``.Value, ``renderbuffertarget``.Value, ``renderbuffer``.Value)
    override this.FramebufferTexture2D(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``textarget`` : TextureTarget, ``texture`` : uint32, ``level`` : int) = 
        commands.Add <| fun () -> gl.glFramebufferTexture2D.Invoke(``target``, ``attachment``, ``textarget``, ``texture``, ``level``)
    override this.FramebufferTexture2D(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``textarget`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``textarget`` = this.Use ``textarget``
        let ``texture`` = this.Use ``texture``
        let ``level`` = this.Use ``level``
        commands.Add <| fun () -> gl.glFramebufferTexture2D.Invoke(``target``.Value, ``attachment``.Value, ``textarget``.Value, ``texture``.Value, ``level``.Value)
    override this.FrontFace(``mode`` : FrontFaceDirection) = 
        commands.Add <| fun () -> gl.glFrontFace.Invoke(``mode``)
    override this.FrontFace(``mode`` : aptr<FrontFaceDirection>) = 
        let ``mode`` = this.Use ``mode``
        commands.Add <| fun () -> gl.glFrontFace.Invoke(``mode``.Value)
    override this.GenBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenBuffers.Invoke(``n``, ``buffers``)
    override this.GenBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``buffers`` = this.Use ``buffers``
        commands.Add <| fun () -> gl.glGenBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
    override this.GenerateMipmap(``target`` : TextureTarget) = 
        commands.Add <| fun () -> gl.glGenerateMipmap.Invoke(``target``)
    override this.GenerateMipmap(``target`` : aptr<TextureTarget>) = 
        let ``target`` = this.Use ``target``
        commands.Add <| fun () -> gl.glGenerateMipmap.Invoke(``target``.Value)
    override this.GenFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenFramebuffers.Invoke(``n``, ``framebuffers``)
    override this.GenFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``framebuffers`` = this.Use ``framebuffers``
        commands.Add <| fun () -> gl.glGenFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
    override this.GenRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenRenderbuffers.Invoke(``n``, ``renderbuffers``)
    override this.GenRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``renderbuffers`` = this.Use ``renderbuffers``
        commands.Add <| fun () -> gl.glGenRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
    override this.GenTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGenTextures.Invoke(``n``, ``textures``)
    override this.GenTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``textures`` = this.Use ``textures``
        commands.Add <| fun () -> gl.glGenTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
    override this.GetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> WrappedCommands.glGetActiveAttrib(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetActiveAttrib(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        commands.Add <| fun () -> WrappedCommands.glGetActiveAttrib(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> WrappedCommands.glGetActiveUniform(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetActiveUniform(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        commands.Add <| fun () -> WrappedCommands.glGetActiveUniform(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetAttachedShaders(``program`` : uint32, ``maxCount`` : uint32, ``count`` : nativeptr<uint32>, ``shaders`` : nativeptr<uint32>) = 
        commands.Add <| fun () -> gl.glGetAttachedShaders.Invoke(``program``, ``maxCount``, ``count``, ``shaders``)
    override this.GetAttachedShaders(``program`` : aptr<uint32>, ``maxCount`` : aptr<uint32>, ``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``maxCount`` = this.Use ``maxCount``
        let ``count`` = this.Use ``count``
        let ``shaders`` = this.Use ``shaders``
        commands.Add <| fun () -> gl.glGetAttachedShaders.Invoke(``program``.Value, ``maxCount``.Value, NativePtr.ofNativeInt ``count``.Pointer, NativePtr.ofNativeInt ``shaders``.Pointer)
    override this.GetAttribLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetAttribLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetAttribLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetAttribLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetBooleanv(``pname`` : GetPName, ``data`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glGetBooleanv.Invoke(``pname``, ``data``)
    override this.GetBooleanv(``pname`` : aptr<GetPName>, ``data`` : aptr<Boolean>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetBooleanv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetBufferParameteriv(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetBufferParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetBufferParameteriv(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetBufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetError(``returnValue`` : nativeptr<GLEnum>) = 
        commands.Add <| fun () -> gl.glGetError.Invoke() |> NativePtr.write returnValue
    override this.GetError(``returnValue`` : aptr<GLEnum>) = 
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetError.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetFloatv(``pname`` : GetPName, ``data`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glGetFloatv.Invoke(``pname``, ``data``)
    override this.GetFloatv(``pname`` : aptr<GetPName>, ``data`` : aptr<float32>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetFloatv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetFramebufferAttachmentParameteriv(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``pname`` : FramebufferAttachmentParameterName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``, ``attachment``, ``pname``, ``params``)
    override this.GetFramebufferAttachmentParameteriv(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``pname`` : aptr<FramebufferAttachmentParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``.Value, ``attachment``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetIntegerv(``pname`` : GetPName, ``data`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetIntegerv.Invoke(``pname``, ``data``)
    override this.GetIntegerv(``pname`` : aptr<GetPName>, ``data`` : aptr<int>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        commands.Add <| fun () -> gl.glGetIntegerv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetProgramiv(``program`` : uint32, ``pname`` : ProgramPropertyARB, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetProgramiv.Invoke(``program``, ``pname``, ``params``)
    override this.GetProgramiv(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramPropertyARB>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetProgramiv.Invoke(``program``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetProgramInfoLog(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> gl.glGetProgramInfoLog.Invoke(``program``, ``bufSize``, ``length``, ``infoLog``)
    override this.GetProgramInfoLog(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``infoLog`` = this.Use ``infoLog``
        commands.Add <| fun () -> gl.glGetProgramInfoLog.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
    override this.GetRenderbufferParameteriv(``target`` : RenderbufferTarget, ``pname`` : RenderbufferParameterName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetRenderbufferParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetRenderbufferParameteriv(``target`` : aptr<RenderbufferTarget>, ``pname`` : aptr<RenderbufferParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetRenderbufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetShaderiv(``shader`` : uint32, ``pname`` : ShaderParameterName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetShaderiv.Invoke(``shader``, ``pname``, ``params``)
    override this.GetShaderiv(``shader`` : aptr<uint32>, ``pname`` : aptr<ShaderParameterName>, ``params`` : aptr<int>) = 
        let ``shader`` = this.Use ``shader``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetShaderiv.Invoke(``shader``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetShaderInfoLog(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> gl.glGetShaderInfoLog.Invoke(``shader``, ``bufSize``, ``length``, ``infoLog``)
    override this.GetShaderInfoLog(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``shader`` = this.Use ``shader``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``infoLog`` = this.Use ``infoLog``
        commands.Add <| fun () -> gl.glGetShaderInfoLog.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
    override this.GetShaderPrecisionFormat(``shadertype`` : ShaderType, ``precisiontype`` : PrecisionType, ``range`` : nativeptr<int>, ``precision`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetShaderPrecisionFormat.Invoke(``shadertype``, ``precisiontype``, ``range``, ``precision``)
    override this.GetShaderPrecisionFormat(``shadertype`` : aptr<ShaderType>, ``precisiontype`` : aptr<PrecisionType>, ``range`` : aptr<int>, ``precision`` : aptr<int>) = 
        let ``shadertype`` = this.Use ``shadertype``
        let ``precisiontype`` = this.Use ``precisiontype``
        let ``range`` = this.Use ``range``
        let ``precision`` = this.Use ``precision``
        commands.Add <| fun () -> gl.glGetShaderPrecisionFormat.Invoke(``shadertype``.Value, ``precisiontype``.Value, NativePtr.ofNativeInt ``range``.Pointer, NativePtr.ofNativeInt ``precision``.Pointer)
    override this.GetShaderSource(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``source`` : nativeptr<uint8>) = 
        commands.Add <| fun () -> gl.glGetShaderSource.Invoke(``shader``, ``bufSize``, ``length``, ``source``)
    override this.GetShaderSource(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``source`` : aptr<uint8>) = 
        let ``shader`` = this.Use ``shader``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``source`` = this.Use ``source``
        commands.Add <| fun () -> gl.glGetShaderSource.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``source``.Pointer)
    override this.GetString(``name`` : StringName, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        commands.Add <| fun () -> gl.glGetString.Invoke(``name``) |> NativePtr.write returnValue
    override this.GetString(``name`` : aptr<StringName>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetString.Invoke(``name``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetTexParameterfv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glGetTexParameterfv.Invoke(``target``, ``pname``, ``params``)
    override this.GetTexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetTexParameteriv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetTexParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetTexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformfv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glGetUniformfv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformfv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<float32>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetUniformfv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetUniformiv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetUniformiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetUniformLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetUniformLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glGetUniformLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetVertexAttribfv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glGetVertexAttribfv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribfv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetVertexAttribfv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribiv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glGetVertexAttribiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glGetVertexAttribiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribPointerv(``index`` : uint32, ``pname`` : VertexAttribPointerPropertyARB, ``pointer`` : nativeptr<nativeint>) = 
        commands.Add <| fun () -> gl.glGetVertexAttribPointerv.Invoke(``index``, ``pname``, ``pointer``)
    override this.GetVertexAttribPointerv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPointerPropertyARB>, ``pointer`` : aptr<nativeint>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``pointer`` = this.Use ``pointer``
        commands.Add <| fun () -> gl.glGetVertexAttribPointerv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``pointer``.Pointer)
    override this.Hint(``target`` : HintTarget, ``mode`` : HintMode) = 
        commands.Add <| fun () -> gl.glHint.Invoke(``target``, ``mode``)
    override this.Hint(``target`` : aptr<HintTarget>, ``mode`` : aptr<HintMode>) = 
        let ``target`` = this.Use ``target``
        let ``mode`` = this.Use ``mode``
        commands.Add <| fun () -> gl.glHint.Invoke(``target``.Value, ``mode``.Value)
    override this.IsBuffer(``buffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsBuffer.Invoke(``buffer``) |> NativePtr.write returnValue
    override this.IsBuffer(``buffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsBuffer.Invoke(``buffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsEnabled(``cap`` : EnableCap, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsEnabled.Invoke(``cap``) |> NativePtr.write returnValue
    override this.IsEnabled(``cap`` : aptr<EnableCap>, ``returnValue`` : aptr<Boolean>) = 
        let ``cap`` = this.Use ``cap``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsEnabled.Invoke(``cap``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsFramebuffer(``framebuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsFramebuffer.Invoke(``framebuffer``) |> NativePtr.write returnValue
    override this.IsFramebuffer(``framebuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``framebuffer`` = this.Use ``framebuffer``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsFramebuffer.Invoke(``framebuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsProgram(``program`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsProgram.Invoke(``program``) |> NativePtr.write returnValue
    override this.IsProgram(``program`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``program`` = this.Use ``program``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsProgram.Invoke(``program``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsRenderbuffer(``renderbuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsRenderbuffer.Invoke(``renderbuffer``) |> NativePtr.write returnValue
    override this.IsRenderbuffer(``renderbuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``renderbuffer`` = this.Use ``renderbuffer``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsRenderbuffer.Invoke(``renderbuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsShader(``shader`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsShader.Invoke(``shader``) |> NativePtr.write returnValue
    override this.IsShader(``shader`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``shader`` = this.Use ``shader``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsShader.Invoke(``shader``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsTexture(``texture`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        commands.Add <| fun () -> gl.glIsTexture.Invoke(``texture``) |> NativePtr.write returnValue
    override this.IsTexture(``texture`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``texture`` = this.Use ``texture``
        let ``returnValue`` = this.Use ``returnValue``
        commands.Add <| fun () -> gl.glIsTexture.Invoke(``texture``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.LineWidth(``width`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glLineWidth(``width``)
    override this.LineWidth(``width`` : aptr<float32>) = 
        let ``width`` = this.Use ``width``
        commands.Add <| fun () -> WrappedCommands.glLineWidth(``width``.Value)
    override this.LinkProgram(``program`` : uint32) = 
        commands.Add <| fun () -> gl.glLinkProgram.Invoke(``program``)
    override this.LinkProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        commands.Add <| fun () -> gl.glLinkProgram.Invoke(``program``.Value)
    override this.PixelStorei(``pname`` : PixelStoreParameter, ``param`` : int) = 
        commands.Add <| fun () -> gl.glPixelStorei.Invoke(``pname``, ``param``)
    override this.PixelStorei(``pname`` : aptr<PixelStoreParameter>, ``param`` : aptr<int>) = 
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> gl.glPixelStorei.Invoke(``pname``.Value, ``param``.Value)
    override this.PolygonOffset(``factor`` : float32, ``units`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glPolygonOffset(``factor``, ``units``)
    override this.PolygonOffset(``factor`` : aptr<float32>, ``units`` : aptr<float32>) = 
        let ``factor`` = this.Use ``factor``
        let ``units`` = this.Use ``units``
        commands.Add <| fun () -> WrappedCommands.glPolygonOffset(``factor``.Value, ``units``.Value)
    override this.ReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glReadPixels(``x``, ``y``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
    override this.ReadPixels(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<'T7>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        commands.Add <| fun () -> WrappedCommands.glReadPixels(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Pointer)
    override this.ReleaseShaderCompiler() = 
        commands.Add <| fun () -> gl.glReleaseShaderCompiler.Invoke()
    override this.RenderbufferStorage(``target`` : RenderbufferTarget, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> gl.glRenderbufferStorage.Invoke(``target``, ``internalformat``, ``width``, ``height``)
    override this.RenderbufferStorage(``target`` : aptr<RenderbufferTarget>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> gl.glRenderbufferStorage.Invoke(``target``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.SampleCoverage(``value`` : float32, ``invert`` : Boolean) = 
        commands.Add <| fun () -> WrappedCommands.glSampleCoverage(``value``, ``invert``)
    override this.SampleCoverage(``value`` : aptr<float32>, ``invert`` : aptr<Boolean>) = 
        let ``value`` = this.Use ``value``
        let ``invert`` = this.Use ``invert``
        commands.Add <| fun () -> WrappedCommands.glSampleCoverage(``value``.Value, ``invert``.Value)
    override this.Scissor(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> gl.glScissor.Invoke(``x``, ``y``, ``width``, ``height``)
    override this.Scissor(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> gl.glScissor.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.ShaderBinary(``count`` : uint32, ``shaders`` : nativeptr<uint32>, ``binaryFormat`` : ShaderBinaryFormat, ``binary`` : nativeint, ``length`` : uint32) = 
        commands.Add <| fun () -> gl.glShaderBinary.Invoke(``count``, ``shaders``, ``binaryFormat``, ``binary``, ``length``)
    override this.ShaderBinary(``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>, ``binaryFormat`` : aptr<ShaderBinaryFormat>, ``binary`` : aptr<'T4>, ``length`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``shaders`` = this.Use ``shaders``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        let ``length`` = this.Use ``length``
        commands.Add <| fun () -> gl.glShaderBinary.Invoke(``count``.Value, NativePtr.ofNativeInt ``shaders``.Pointer, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
    override this.ShaderSource(``shader`` : uint32, ``count`` : uint32, ``string`` : nativeptr<nativeptr<uint8>>, ``length`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glShaderSource.Invoke(``shader``, ``count``, ``string``, ``length``)
    override this.ShaderSource(``shader`` : aptr<uint32>, ``count`` : aptr<uint32>, ``string`` : aptr<nativeptr<uint8>>, ``length`` : aptr<int>) = 
        let ``shader`` = this.Use ``shader``
        let ``count`` = this.Use ``count``
        let ``string`` = this.Use ``string``
        let ``length`` = this.Use ``length``
        commands.Add <| fun () -> gl.glShaderSource.Invoke(``shader``.Value, ``count``.Value, NativePtr.ofNativeInt ``string``.Pointer, NativePtr.ofNativeInt ``length``.Pointer)
    override this.StencilFunc(``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        commands.Add <| fun () -> gl.glStencilFunc.Invoke(``func``, ``ref``, ``mask``)
    override this.StencilFunc(``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``func`` = this.Use ``func``
        let ``ref`` = this.Use ``ref``
        let ``mask`` = this.Use ``mask``
        commands.Add <| fun () -> gl.glStencilFunc.Invoke(``func``.Value, ``ref``.Value, ``mask``.Value)
    override this.StencilFuncSeparate(``face`` : StencilFaceDirection, ``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        commands.Add <| fun () -> gl.glStencilFuncSeparate.Invoke(``face``, ``func``, ``ref``, ``mask``)
    override this.StencilFuncSeparate(``face`` : aptr<StencilFaceDirection>, ``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``face`` = this.Use ``face``
        let ``func`` = this.Use ``func``
        let ``ref`` = this.Use ``ref``
        let ``mask`` = this.Use ``mask``
        commands.Add <| fun () -> gl.glStencilFuncSeparate.Invoke(``face``.Value, ``func``.Value, ``ref``.Value, ``mask``.Value)
    override this.StencilMask(``mask`` : uint32) = 
        commands.Add <| fun () -> gl.glStencilMask.Invoke(``mask``)
    override this.StencilMask(``mask`` : aptr<uint32>) = 
        let ``mask`` = this.Use ``mask``
        commands.Add <| fun () -> gl.glStencilMask.Invoke(``mask``.Value)
    override this.StencilMaskSeparate(``face`` : StencilFaceDirection, ``mask`` : uint32) = 
        commands.Add <| fun () -> gl.glStencilMaskSeparate.Invoke(``face``, ``mask``)
    override this.StencilMaskSeparate(``face`` : aptr<StencilFaceDirection>, ``mask`` : aptr<uint32>) = 
        let ``face`` = this.Use ``face``
        let ``mask`` = this.Use ``mask``
        commands.Add <| fun () -> gl.glStencilMaskSeparate.Invoke(``face``.Value, ``mask``.Value)
    override this.StencilOp(``fail`` : StencilOp, ``zfail`` : StencilOp, ``zpass`` : StencilOp) = 
        commands.Add <| fun () -> gl.glStencilOp.Invoke(``fail``, ``zfail``, ``zpass``)
    override this.StencilOp(``fail`` : aptr<StencilOp>, ``zfail`` : aptr<StencilOp>, ``zpass`` : aptr<StencilOp>) = 
        let ``fail`` = this.Use ``fail``
        let ``zfail`` = this.Use ``zfail``
        let ``zpass`` = this.Use ``zpass``
        commands.Add <| fun () -> gl.glStencilOp.Invoke(``fail``.Value, ``zfail``.Value, ``zpass``.Value)
    override this.StencilOpSeparate(``face`` : StencilFaceDirection, ``sfail`` : StencilOp, ``dpfail`` : StencilOp, ``dppass`` : StencilOp) = 
        commands.Add <| fun () -> gl.glStencilOpSeparate.Invoke(``face``, ``sfail``, ``dpfail``, ``dppass``)
    override this.StencilOpSeparate(``face`` : aptr<StencilFaceDirection>, ``sfail`` : aptr<StencilOp>, ``dpfail`` : aptr<StencilOp>, ``dppass`` : aptr<StencilOp>) = 
        let ``face`` = this.Use ``face``
        let ``sfail`` = this.Use ``sfail``
        let ``dpfail`` = this.Use ``dpfail``
        let ``dppass`` = this.Use ``dppass``
        commands.Add <| fun () -> gl.glStencilOpSeparate.Invoke(``face``.Value, ``sfail``.Value, ``dpfail``.Value, ``dppass``.Value)
    override this.TexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``format``, ``type``, ``pixels``)
    override this.TexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        commands.Add <| fun () -> WrappedCommands.glTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glTexParameterf(``target``, ``pname``, ``param``)
    override this.TexParameterf(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> WrappedCommands.glTexParameterf(``target``.Value, ``pname``.Value, ``param``.Value)
    override this.TexParameterfv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glTexParameterfv.Invoke(``target``, ``pname``, ``params``)
    override this.TexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.TexParameteri(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : int) = 
        commands.Add <| fun () -> gl.glTexParameteri.Invoke(``target``, ``pname``, ``param``)
    override this.TexParameteri(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        commands.Add <| fun () -> gl.glTexParameteri.Invoke(``target``.Value, ``pname``.Value, ``param``.Value)
    override this.TexParameteriv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glTexParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.TexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        commands.Add <| fun () -> gl.glTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.TexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        commands.Add <| fun () -> WrappedCommands.glTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
    override this.TexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        commands.Add <| fun () -> WrappedCommands.glTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.Uniform1f(``location`` : int, ``v0`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glUniform1f(``location``, ``v0``)
    override this.Uniform1f(``location`` : aptr<int>, ``v0`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        commands.Add <| fun () -> WrappedCommands.glUniform1f(``location``.Value, ``v0``.Value)
    override this.Uniform1fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniform1fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform1fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform1i(``location`` : int, ``v0`` : int) = 
        commands.Add <| fun () -> gl.glUniform1i.Invoke(``location``, ``v0``)
    override this.Uniform1i(``location`` : aptr<int>, ``v0`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        commands.Add <| fun () -> gl.glUniform1i.Invoke(``location``.Value, ``v0``.Value)
    override this.Uniform1iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glUniform1iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform1iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glUniform2f(``location``, ``v0``, ``v1``)
    override this.Uniform2f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        commands.Add <| fun () -> WrappedCommands.glUniform2f(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniform2fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform2fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2i(``location`` : int, ``v0`` : int, ``v1`` : int) = 
        commands.Add <| fun () -> gl.glUniform2i.Invoke(``location``, ``v0``, ``v1``)
    override this.Uniform2i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        commands.Add <| fun () -> gl.glUniform2i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glUniform2iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform2iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glUniform3f(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        commands.Add <| fun () -> WrappedCommands.glUniform3f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniform3fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform3fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int) = 
        commands.Add <| fun () -> gl.glUniform3i.Invoke(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        commands.Add <| fun () -> gl.glUniform3i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glUniform3iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform3iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glUniform4f(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>, ``v3`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        commands.Add <| fun () -> WrappedCommands.glUniform4f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniform4fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform4fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int, ``v3`` : int) = 
        commands.Add <| fun () -> gl.glUniform4i.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>, ``v3`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        commands.Add <| fun () -> gl.glUniform4i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        commands.Add <| fun () -> gl.glUniform4iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniform4iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glUniformMatrix4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        commands.Add <| fun () -> gl.glUniformMatrix4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UseProgram(``program`` : uint32) = 
        commands.Add <| fun () -> gl.glUseProgram.Invoke(``program``)
    override this.UseProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        commands.Add <| fun () -> gl.glUseProgram.Invoke(``program``.Value)
    override this.ValidateProgram(``program`` : uint32) = 
        commands.Add <| fun () -> gl.glValidateProgram.Invoke(``program``)
    override this.ValidateProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        commands.Add <| fun () -> gl.glValidateProgram.Invoke(``program``.Value)
    override this.VertexAttrib1f(``index`` : uint32, ``x`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib1f(``index``, ``x``)
    override this.VertexAttrib1f(``index`` : aptr<uint32>, ``x`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib1f(``index``.Value, ``x``.Value)
    override this.VertexAttrib1fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glVertexAttrib1fv.Invoke(``index``, ``v``)
    override this.VertexAttrib1fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttrib1fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib2f(``index``, ``x``, ``y``)
    override this.VertexAttrib2f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib2f(``index``.Value, ``x``.Value, ``y``.Value)
    override this.VertexAttrib2fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glVertexAttrib2fv.Invoke(``index``, ``v``)
    override this.VertexAttrib2fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttrib2fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib3f(``index``, ``x``, ``y``, ``z``)
    override this.VertexAttrib3f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib3f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value)
    override this.VertexAttrib3fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glVertexAttrib3fv.Invoke(``index``, ``v``)
    override this.VertexAttrib3fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttrib3fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) = 
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib4f(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttrib4f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>, ``w`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        commands.Add <| fun () -> WrappedCommands.glVertexAttrib4f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttrib4fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        commands.Add <| fun () -> gl.glVertexAttrib4fv.Invoke(``index``, ``v``)
    override this.VertexAttrib4fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        commands.Add <| fun () -> gl.glVertexAttrib4fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribPointerType, ``normalized`` : Boolean, ``stride`` : uint32, ``pointer`` : nativeint) = 
        commands.Add <| fun () -> gl.glVertexAttribPointer.Invoke(``index``, ``size``, ``type``, ``normalized``, ``stride``, ``pointer``)
    override this.VertexAttribPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribPointerType>, ``normalized`` : aptr<Boolean>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T6>) = 
        let ``index`` = this.Use ``index``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``normalized`` = this.Use ``normalized``
        let ``stride`` = this.Use ``stride``
        let ``pointer`` = this.Use ``pointer``
        commands.Add <| fun () -> gl.glVertexAttribPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``normalized``.Value, ``stride``.Value, ``pointer``.Pointer)
    override this.Viewport(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        commands.Add <| fun () -> gl.glViewport.Invoke(``x``, ``y``, ``width``, ``height``)
    override this.Viewport(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        commands.Add <| fun () -> gl.glViewport.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.GetBufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``dst`` : nativeint) = 
        commands.Add <| fun () -> gl.glGetBufferSubData.Invoke(``target``, ``offset``, ``size``, ``dst``)
    override this.GetBufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``dst`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        let ``dst`` = this.Use ``dst``
        commands.Add <| fun () -> gl.glGetBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``dst``.Value)
    override this.MultiDrawArraysIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``bindingInfo`` : nativeint) = 
        commands.Add <| fun () -> gl.glMultiDrawArraysIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``bindingInfo``)
    override this.MultiDrawArraysIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirectBuffer`` = this.Use ``indirectBuffer``
        let ``count`` = this.Use ``count``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        commands.Add <| fun () -> gl.glMultiDrawArraysIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``bindingInfo``.Value)
    override this.MultiDrawArrays(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``bindingInfo`` : nativeint) = 
        commands.Add <| fun () -> gl.glMultiDrawArrays.Invoke(``mode``, ``indirect``, ``count``, ``bindingInfo``)
    override this.MultiDrawArrays(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirect`` = this.Use ``indirect``
        let ``count`` = this.Use ``count``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        commands.Add <| fun () -> gl.glMultiDrawArrays.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``bindingInfo``.Value)
    override this.MultiDrawElementsIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        commands.Add <| fun () -> gl.glMultiDrawElementsIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``indexType``, ``bindingInfo``)
    override this.MultiDrawElementsIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirectBuffer`` = this.Use ``indirectBuffer``
        let ``count`` = this.Use ``count``
        let ``indexType`` = this.Use ``indexType``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        commands.Add <| fun () -> gl.glMultiDrawElementsIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
    override this.MultiDrawElements(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        commands.Add <| fun () -> gl.glMultiDrawElements.Invoke(``mode``, ``indirect``, ``count``, ``indexType``, ``bindingInfo``)
    override this.MultiDrawElements(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirect`` = this.Use ``indirect``
        let ``count`` = this.Use ``count``
        let ``indexType`` = this.Use ``indexType``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        commands.Add <| fun () -> gl.glMultiDrawElements.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
    override this.Commit() = 
        commands.Add <| fun () -> gl.glCommit.Invoke()
    override this.TexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) = 
        commands.Add <| fun () -> WrappedCommands.glTexSubImage2DJSImage(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``typ``, ``imgHandle``)
    override this.TexSubImage2DJSImage(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<int>, ``height`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``typ`` : aptr<PixelType>, ``imgHandle`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``typ`` = this.Use ``typ``
        let ``imgHandle`` = this.Use ``imgHandle``
        commands.Add <| fun () -> WrappedCommands.glTexSubImage2DJSImage(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``typ``.Value, ``imgHandle``.Value)
type internal ImmediateCommandEncoder(device : Device, currentGL : GL) =
    inherit CommandEncoder(device)
    let gl = GLDelegates.get device.Context
    let mutable stack : list<int64> = []
    let mutable pointers = System.Collections.Generic.HashSet<AdaptivePointer>()

    member private x.Acquire(r : AdaptivePointer) =
        if pointers.Add r then r.Acquire()
        if not r.IsConstant then r.Update AdaptiveToken.Top

    override x.Begin() =
        stack <- []

    override x.End() =
        stack <- []
        x.Dispose()

    override x.Destroy() =
        if pointers.Count > 0 then 
             for p in pointers do p.Release()
             pointers <- System.Collections.Generic.HashSet<AdaptivePointer>()

    override x.Clear() =
        if pointers.Count > 0 then 
             for p in pointers do p.Release()
             pointers <- System.Collections.Generic.HashSet<AdaptivePointer>()

    override x.Perform gl =
        ()

    override x.Custom(action : GL -> unit) =
        action currentGL

    override x.Push (location : nativeptr<'a>) =
        if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack
        else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack
    
    override x.Pop (location : nativeptr<'a>) =
        let h = List.head stack
        stack <- List.tail stack
        if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)
        else NativePtr.write location (Unchecked.reinterpret (int h))
    
    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = 
        x.Acquire location
        let v = location.Value
        match cases |> List.tryPick (fun (vi, c) -> if vi = v then Some c else None)  with
        | Some c -> c (x :> CommandEncoder)
        | None -> fallback (x :> CommandEncoder)
    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) =
        Marshal.Copy(src, dst, size)
    
    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) =
        x.Acquire src; x.Acquire dst; x.Acquire size
        Marshal.Copy(src.Pointer, dst.Pointer, size.Value)
    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        x.Acquire src; x.Acquire dst; x.Acquire size
        Marshal.Copy(src.Pointer, dst.Value, size.Value)
    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) =
        x.Acquire src; x.Acquire dst; x.Acquire size
        Marshal.Copy(src.Value, dst.Pointer, size.Value)
    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        x.Acquire src; x.Acquire dst; x.Acquire size
        Marshal.Copy(src.Value, dst.Value, size.Value)
    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) =
        x.Acquire a; x.Acquire b; x.Acquire res
        let pa = a.Pointer |> NativePtr.ofNativeInt<nativeint>
        let pb = b.Pointer |> NativePtr.ofNativeInt<nativeint>
        let pr = res.Pointer |> NativePtr.ofNativeInt<nativeint>
        NativePtr.write pr (NativePtr.read pa + NativePtr.read pb)
    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) =
        x.Acquire a; x.Acquire b; x.Acquire c; x.Acquire res
        let pa = a.Pointer |> NativePtr.ofNativeInt<nativeint>
        let pb = b.Pointer |> NativePtr.ofNativeInt<nativeint>
        let pc = c.Pointer |> NativePtr.ofNativeInt<nativeint>
        let pr = res.Pointer |> NativePtr.ofNativeInt<nativeint>
        NativePtr.write pr (NativePtr.read pa + NativePtr.read pb * NativePtr.read pc)
    override x.Bgra(values : aptr<byte>, count : aptr<int>) = 
        x.Acquire(values); x.Acquire(count)
        let values = values.Pointer |> NativePtr.ofNativeInt<byte>
        let count = count.Pointer |> NativePtr.ofNativeInt<int>
        let mutable off = 0
        for i in 0 .. NativePtr.read count - 1 do
            let t = NativePtr.get values off
            NativePtr.set values off (NativePtr.get values (off + 2))
            NativePtr.set values (off + 2) t
            off <- off + 4
    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = 
        x.Acquire(src); x.Acquire(dst); x.Acquire(count)
        let src = src.Pointer |> NativePtr.ofNativeInt<byte>
        let dst = dst.Pointer |> NativePtr.ofNativeInt<byte>
        let count = count.Pointer |> NativePtr.ofNativeInt<int>
        let mutable off = 0
        for i in 0 .. NativePtr.read count - 1 do
            NativePtr.set dst (off + 2) (NativePtr.get src (off+0))
            NativePtr.set dst (off + 1) (NativePtr.get src (off+1))
            NativePtr.set dst (off + 0) (NativePtr.get src (off+2))
            NativePtr.set dst (off + 3) (NativePtr.get src (off+3))
            off <- off + 4
    override this.Call(func : aptr<nativeint>) =
        let tDel = DelegateType.Get([], typeof<unit>)
        this.Acquire func
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [|  |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>) =
        let tDel = DelegateType.Get([typeof<'a>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        this.Acquire arg3
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        this.Acquire arg3
        this.Acquire arg4
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        this.Acquire arg3
        this.Acquire arg4
        this.Acquire arg5
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        this.Acquire arg3
        this.Acquire arg4
        this.Acquire arg5
        this.Acquire arg6
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj |] |> ignore


    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>, arg7 : aptr<'h>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>; typeof<'h>], typeof<unit>)
        this.Acquire func
        this.Acquire arg0
        this.Acquire arg1
        this.Acquire arg2
        this.Acquire arg3
        this.Acquire arg4
        this.Acquire arg5
        this.Acquire arg6
        this.Acquire arg7
        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
        del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj; arg7.Value :> obj |] |> ignore


    override this.BeginQuery(``target`` : QueryTarget, ``id`` : uint32) = 
        gl.glBeginQuery.Invoke(``target``, ``id``)
    override this.BeginQuery(``target`` : aptr<QueryTarget>, ``id`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``id``
        gl.glBeginQuery.Invoke(``target``.Value, ``id``.Value)
    override this.BeginTransformFeedback(``primitiveMode`` : PrimitiveType) = 
        gl.glBeginTransformFeedback.Invoke(``primitiveMode``)
    override this.BeginTransformFeedback(``primitiveMode`` : aptr<PrimitiveType>) = 
        this.Acquire ``primitiveMode``
        gl.glBeginTransformFeedback.Invoke(``primitiveMode``.Value)
    override this.BindBufferBase(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32) = 
        gl.glBindBufferBase.Invoke(``target``, ``index``, ``buffer``)
    override this.BindBufferBase(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``index``
        this.Acquire ``buffer``
        gl.glBindBufferBase.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value)
    override this.BindBufferRange(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32, ``offset`` : nativeint, ``size`` : unativeint) = 
        gl.glBindBufferRange.Invoke(``target``, ``index``, ``buffer``, ``offset``, ``size``)
    override this.BindBufferRange(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        this.Acquire ``target``
        this.Acquire ``index``
        this.Acquire ``buffer``
        this.Acquire ``offset``
        this.Acquire ``size``
        gl.glBindBufferRange.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value, ``offset``.Value, ``size``.Value)
    override this.BindSampler(``unit`` : uint32, ``sampler`` : uint32) = 
        gl.glBindSampler.Invoke(``unit``, ``sampler``)
    override this.BindSampler(``unit`` : aptr<uint32>, ``sampler`` : aptr<uint32>) = 
        this.Acquire ``unit``
        this.Acquire ``sampler``
        gl.glBindSampler.Invoke(``unit``.Value, ``sampler``.Value)
    override this.BindTransformFeedback(``target`` : BindTransformFeedbackTarget, ``id`` : uint32) = 
        gl.glBindTransformFeedback.Invoke(``target``, ``id``)
    override this.BindTransformFeedback(``target`` : aptr<BindTransformFeedbackTarget>, ``id`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``id``
        gl.glBindTransformFeedback.Invoke(``target``.Value, ``id``.Value)
    override this.BindVertexArray(``array`` : uint32) = 
        gl.glBindVertexArray.Invoke(``array``)
    override this.BindVertexArray(``array`` : aptr<uint32>) = 
        this.Acquire ``array``
        gl.glBindVertexArray.Invoke(``array``.Value)
    override this.BlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) = 
        WrappedCommands.glBlitFramebuffer(``srcX0``, ``srcY0``, ``srcX1``, ``srcY1``, ``dstX0``, ``dstY0``, ``dstX1``, ``dstY1``, ``mask``, ``filter``)
    override this.BlitFramebuffer(``srcX0`` : aptr<int>, ``srcY0`` : aptr<int>, ``srcX1`` : aptr<int>, ``srcY1`` : aptr<int>, ``dstX0`` : aptr<int>, ``dstY0`` : aptr<int>, ``dstX1`` : aptr<int>, ``dstY1`` : aptr<int>, ``mask`` : aptr<ClearBufferMask>, ``filter`` : aptr<BlitFramebufferFilter>) = 
        this.Acquire ``srcX0``
        this.Acquire ``srcY0``
        this.Acquire ``srcX1``
        this.Acquire ``srcY1``
        this.Acquire ``dstX0``
        this.Acquire ``dstY0``
        this.Acquire ``dstX1``
        this.Acquire ``dstY1``
        this.Acquire ``mask``
        this.Acquire ``filter``
        WrappedCommands.glBlitFramebuffer(``srcX0``.Value, ``srcY0``.Value, ``srcX1``.Value, ``srcY1``.Value, ``dstX0``.Value, ``dstY0``.Value, ``dstX1``.Value, ``dstY1``.Value, ``mask``.Value, ``filter``.Value)
    override this.ClearBufferiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<int>) = 
        gl.glClearBufferiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<int>) = 
        this.Acquire ``buffer``
        this.Acquire ``drawbuffer``
        this.Acquire ``value``
        gl.glClearBufferiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferuiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<uint32>) = 
        gl.glClearBufferuiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferuiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<uint32>) = 
        this.Acquire ``buffer``
        this.Acquire ``drawbuffer``
        this.Acquire ``value``
        gl.glClearBufferuiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferfv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<float32>) = 
        gl.glClearBufferfv.Invoke(``buffer``, ``drawbuffer``, ``value``)
    override this.ClearBufferfv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<float32>) = 
        this.Acquire ``buffer``
        this.Acquire ``drawbuffer``
        this.Acquire ``value``
        gl.glClearBufferfv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.ClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) = 
        WrappedCommands.glClearBufferfi(``buffer``, ``drawbuffer``, ``depth``, ``stencil``)
    override this.ClearBufferfi(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``depth`` : aptr<float32>, ``stencil`` : aptr<int>) = 
        this.Acquire ``buffer``
        this.Acquire ``drawbuffer``
        this.Acquire ``depth``
        this.Acquire ``stencil``
        WrappedCommands.glClearBufferfi(``buffer``.Value, ``drawbuffer``.Value, ``depth``.Value, ``stencil``.Value)
    override this.ClientWaitSync(``sync`` : nativeint, ``flags`` : SyncObjectMask, ``timeout`` : uint64, ``returnValue`` : nativeptr<GLEnum>) = 
        gl.glClientWaitSync.Invoke(``sync``, ``flags``, ``timeout``) |> NativePtr.write returnValue
    override this.ClientWaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncObjectMask>, ``timeout`` : aptr<uint64>, ``returnValue`` : aptr<GLEnum>) = 
        this.Acquire ``sync``
        this.Acquire ``flags``
        this.Acquire ``timeout``
        this.Acquire ``returnValue``
        gl.glClientWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        WrappedCommands.glCompressedTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``imageSize``, ``data``)
    override this.CompressedTexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``depth``
        this.Acquire ``border``
        this.Acquire ``imageSize``
        this.Acquire ``data``
        WrappedCommands.glCompressedTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        WrappedCommands.glCompressedTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``imageSize``, ``data``)
    override this.CompressedTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``zoffset``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``depth``
        this.Acquire ``format``
        this.Acquire ``imageSize``
        this.Acquire ``data``
        WrappedCommands.glCompressedTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CopyBufferSubData(``readTarget`` : CopyBufferSubDataTarget, ``writeTarget`` : CopyBufferSubDataTarget, ``readOffset`` : nativeint, ``writeOffset`` : nativeint, ``size`` : unativeint) = 
        gl.glCopyBufferSubData.Invoke(``readTarget``, ``writeTarget``, ``readOffset``, ``writeOffset``, ``size``)
    override this.CopyBufferSubData(``readTarget`` : aptr<CopyBufferSubDataTarget>, ``writeTarget`` : aptr<CopyBufferSubDataTarget>, ``readOffset`` : aptr<nativeint>, ``writeOffset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        this.Acquire ``readTarget``
        this.Acquire ``writeTarget``
        this.Acquire ``readOffset``
        this.Acquire ``writeOffset``
        this.Acquire ``size``
        gl.glCopyBufferSubData.Invoke(``readTarget``.Value, ``writeTarget``.Value, ``readOffset``.Value, ``writeOffset``.Value, ``size``.Value)
    override this.CopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        WrappedCommands.glCopyTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``x``, ``y``, ``width``, ``height``)
    override this.CopyTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``zoffset``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        WrappedCommands.glCopyTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.DeleteQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        gl.glDeleteQueries.Invoke(``n``, ``ids``)
    override this.DeleteQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``ids``
        gl.glDeleteQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.DeleteSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        gl.glDeleteSamplers.Invoke(``count``, ``samplers``)
    override this.DeleteSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        this.Acquire ``count``
        this.Acquire ``samplers``
        gl.glDeleteSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
    override this.DeleteSync(``sync`` : nativeint) = 
        gl.glDeleteSync.Invoke(``sync``)
    override this.DeleteSync(``sync`` : aptr<nativeint>) = 
        this.Acquire ``sync``
        gl.glDeleteSync.Invoke(``sync``.Value)
    override this.DeleteTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        gl.glDeleteTransformFeedbacks.Invoke(``n``, ``ids``)
    override this.DeleteTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``ids``
        gl.glDeleteTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.DeleteVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        gl.glDeleteVertexArrays.Invoke(``n``, ``arrays``)
    override this.DeleteVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``arrays``
        gl.glDeleteVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
    override this.DrawArraysInstanced(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32, ``instancecount`` : uint32) = 
        gl.glDrawArraysInstanced.Invoke(``mode``, ``first``, ``count``, ``instancecount``)
    override this.DrawArraysInstanced(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>, ``instancecount`` : aptr<uint32>) = 
        this.Acquire ``mode``
        this.Acquire ``first``
        this.Acquire ``count``
        this.Acquire ``instancecount``
        gl.glDrawArraysInstanced.Invoke(``mode``.Value, ``first``.Value, ``count``.Value, ``instancecount``.Value)
    override this.DrawBuffers(``n`` : uint32, ``bufs`` : nativeptr<GLEnum>) = 
        gl.glDrawBuffers.Invoke(``n``, ``bufs``)
    override this.DrawBuffers(``n`` : aptr<uint32>, ``bufs`` : aptr<nativeptr<GLEnum>>) = 
        this.Acquire ``n``
        this.Acquire ``bufs``
        gl.glDrawBuffers.Invoke(``n``.Value, ``bufs``.Value)
    override this.DrawElementsInstanced(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint, ``instancecount`` : uint32) = 
        gl.glDrawElementsInstanced.Invoke(``mode``, ``count``, ``type``, ``indices``, ``instancecount``)
    override this.DrawElementsInstanced(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>, ``instancecount`` : aptr<uint32>) = 
        this.Acquire ``mode``
        this.Acquire ``count``
        this.Acquire ``type``
        this.Acquire ``indices``
        this.Acquire ``instancecount``
        gl.glDrawElementsInstanced.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value, ``instancecount``.Value)
    override this.DrawRangeElements(``mode`` : PrimitiveType, ``start`` : uint32, ``end`` : uint32, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        gl.glDrawRangeElements.Invoke(``mode``, ``start``, ``end``, ``count``, ``type``, ``indices``)
    override this.DrawRangeElements(``mode`` : aptr<PrimitiveType>, ``start`` : aptr<uint32>, ``end`` : aptr<uint32>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``start``
        this.Acquire ``end``
        this.Acquire ``count``
        this.Acquire ``type``
        this.Acquire ``indices``
        gl.glDrawRangeElements.Invoke(``mode``.Value, ``start``.Value, ``end``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
    override this.EndQuery(``target`` : QueryTarget) = 
        gl.glEndQuery.Invoke(``target``)
    override this.EndQuery(``target`` : aptr<QueryTarget>) = 
        this.Acquire ``target``
        gl.glEndQuery.Invoke(``target``.Value)
    override this.EndTransformFeedback() = 
        gl.glEndTransformFeedback.Invoke()
    override this.FenceSync(``condition`` : SyncCondition, ``flags`` : SyncBehaviorFlags, ``returnValue`` : nativeptr<nativeint>) = 
        gl.glFenceSync.Invoke(``condition``, ``flags``) |> NativePtr.write returnValue
    override this.FenceSync(``condition`` : aptr<SyncCondition>, ``flags`` : aptr<SyncBehaviorFlags>, ``returnValue`` : aptr<nativeint>) = 
        this.Acquire ``condition``
        this.Acquire ``flags``
        this.Acquire ``returnValue``
        gl.glFenceSync.Invoke(``condition``.Value, ``flags``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.FramebufferTextureLayer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``texture`` : uint32, ``level`` : int, ``layer`` : int) = 
        gl.glFramebufferTextureLayer.Invoke(``target``, ``attachment``, ``texture``, ``level``, ``layer``)
    override this.FramebufferTextureLayer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>, ``layer`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``attachment``
        this.Acquire ``texture``
        this.Acquire ``level``
        this.Acquire ``layer``
        gl.glFramebufferTextureLayer.Invoke(``target``.Value, ``attachment``.Value, ``texture``.Value, ``level``.Value, ``layer``.Value)
    override this.GenQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        gl.glGenQueries.Invoke(``n``, ``ids``)
    override this.GenQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``ids``
        gl.glGenQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.GenSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        gl.glGenSamplers.Invoke(``count``, ``samplers``)
    override this.GenSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        this.Acquire ``count``
        this.Acquire ``samplers``
        gl.glGenSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
    override this.GenTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        gl.glGenTransformFeedbacks.Invoke(``n``, ``ids``)
    override this.GenTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``ids``
        gl.glGenTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
    override this.GenVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        gl.glGenVertexArrays.Invoke(``n``, ``arrays``)
    override this.GenVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``arrays``
        gl.glGenVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
    override this.GetActiveUniformBlockiv(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``pname`` : UniformBlockPName, ``params`` : nativeptr<int>) = 
        gl.glGetActiveUniformBlockiv.Invoke(``program``, ``uniformBlockIndex``, ``pname``, ``params``)
    override this.GetActiveUniformBlockiv(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``pname`` : aptr<UniformBlockPName>, ``params`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``uniformBlockIndex``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetActiveUniformBlockiv.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetActiveUniformBlockName(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``uniformBlockName`` : nativeptr<uint8>) = 
        gl.glGetActiveUniformBlockName.Invoke(``program``, ``uniformBlockIndex``, ``bufSize``, ``length``, ``uniformBlockName``)
    override this.GetActiveUniformBlockName(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``uniformBlockIndex``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``uniformBlockName``
        gl.glGetActiveUniformBlockName.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``uniformBlockName``.Pointer)
    override this.GetActiveUniformsiv(``program`` : uint32, ``uniformCount`` : uint32, ``uniformIndices`` : nativeptr<uint32>, ``pname`` : UniformPName, ``params`` : nativeptr<int>) = 
        gl.glGetActiveUniformsiv.Invoke(``program``, ``uniformCount``, ``uniformIndices``, ``pname``, ``params``)
    override this.GetActiveUniformsiv(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformIndices`` : aptr<uint32>, ``pname`` : aptr<UniformPName>, ``params`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``uniformCount``
        this.Acquire ``uniformIndices``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetActiveUniformsiv.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformIndices``.Pointer, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetBufferParameteri64v(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int64>) = 
        gl.glGetBufferParameteri64v.Invoke(``target``, ``pname``, ``params``)
    override this.GetBufferParameteri64v(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int64>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetBufferParameteri64v.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetFragDataLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        gl.glGetFragDataLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetFragDataLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``name``
        this.Acquire ``returnValue``
        gl.glGetFragDataLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetIntegeri_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int>) = 
        gl.glGetIntegeri_v.Invoke(``target``, ``index``, ``data``)
    override this.GetIntegeri_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``index``
        this.Acquire ``data``
        gl.glGetIntegeri_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInteger64v(``pname`` : GetPName, ``data`` : nativeptr<int64>) = 
        gl.glGetInteger64v.Invoke(``pname``, ``data``)
    override this.GetInteger64v(``pname`` : aptr<GetPName>, ``data`` : aptr<int64>) = 
        this.Acquire ``pname``
        this.Acquire ``data``
        gl.glGetInteger64v.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInteger64i_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int64>) = 
        gl.glGetInteger64i_v.Invoke(``target``, ``index``, ``data``)
    override this.GetInteger64i_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int64>) = 
        this.Acquire ``target``
        this.Acquire ``index``
        this.Acquire ``data``
        gl.glGetInteger64i_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetInternalformativ(``target`` : TextureTarget, ``internalformat`` : InternalFormat, ``pname`` : InternalFormatPName, ``count`` : uint32, ``params`` : nativeptr<int>) = 
        gl.glGetInternalformativ.Invoke(``target``, ``internalformat``, ``pname``, ``count``, ``params``)
    override this.GetInternalformativ(``target`` : aptr<TextureTarget>, ``internalformat`` : aptr<InternalFormat>, ``pname`` : aptr<InternalFormatPName>, ``count`` : aptr<uint32>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``internalformat``
        this.Acquire ``pname``
        this.Acquire ``count``
        this.Acquire ``params``
        gl.glGetInternalformativ.Invoke(``target``.Value, ``internalformat``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetProgramBinary(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``binaryFormat`` : nativeptr<GLEnum>, ``binary`` : nativeint) = 
        gl.glGetProgramBinary.Invoke(``program``, ``bufSize``, ``length``, ``binaryFormat``, ``binary``)
    override this.GetProgramBinary(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T5>) = 
        this.Acquire ``program``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``binaryFormat``
        this.Acquire ``binary``
        gl.glGetProgramBinary.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``binaryFormat``.Pointer, ``binary``.Pointer)
    override this.GetQueryiv(``target`` : QueryTarget, ``pname`` : QueryParameterName, ``params`` : nativeptr<int>) = 
        gl.glGetQueryiv.Invoke(``target``, ``pname``, ``params``)
    override this.GetQueryiv(``target`` : aptr<QueryTarget>, ``pname`` : aptr<QueryParameterName>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetQueryiv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetQueryObjectuiv(``id`` : uint32, ``pname`` : QueryObjectParameterName, ``params`` : nativeptr<uint32>) = 
        gl.glGetQueryObjectuiv.Invoke(``id``, ``pname``, ``params``)
    override this.GetQueryObjectuiv(``id`` : aptr<uint32>, ``pname`` : aptr<QueryObjectParameterName>, ``params`` : aptr<uint32>) = 
        this.Acquire ``id``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetQueryObjectuiv.Invoke(``id``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetSamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``params`` : nativeptr<int>) = 
        gl.glGetSamplerParameteriv.Invoke(``sampler``, ``pname``, ``params``)
    override this.GetSamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``params`` : aptr<int>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetSamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``params`` : nativeptr<float32>) = 
        gl.glGetSamplerParameterfv.Invoke(``sampler``, ``pname``, ``params``)
    override this.GetSamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``params`` : aptr<float32>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetStringi(``name`` : StringName, ``index`` : uint32, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        gl.glGetStringi.Invoke(``name``, ``index``) |> NativePtr.write returnValue
    override this.GetStringi(``name`` : aptr<StringName>, ``index`` : aptr<uint32>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        this.Acquire ``name``
        this.Acquire ``index``
        this.Acquire ``returnValue``
        gl.glGetStringi.Invoke(``name``.Value, ``index``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetSynciv(``sync`` : nativeint, ``pname`` : SyncParameterName, ``count`` : uint32, ``length`` : nativeptr<uint32>, ``values`` : nativeptr<int>) = 
        gl.glGetSynciv.Invoke(``sync``, ``pname``, ``count``, ``length``, ``values``)
    override this.GetSynciv(``sync`` : aptr<nativeint>, ``pname`` : aptr<SyncParameterName>, ``count`` : aptr<uint32>, ``length`` : aptr<uint32>, ``values`` : aptr<int>) = 
        this.Acquire ``sync``
        this.Acquire ``pname``
        this.Acquire ``count``
        this.Acquire ``length``
        this.Acquire ``values``
        gl.glGetSynciv.Invoke(``sync``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``values``.Pointer)
    override this.GetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        WrappedCommands.glGetTransformFeedbackVarying(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetTransformFeedbackVarying(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<uint32>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``index``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``size``
        this.Acquire ``type``
        this.Acquire ``name``
        WrappedCommands.glGetTransformFeedbackVarying(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetUniformuiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<uint32>) = 
        gl.glGetUniformuiv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformuiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``location``
        this.Acquire ``params``
        gl.glGetUniformuiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformBlockIndex(``program`` : uint32, ``uniformBlockName`` : nativeptr<uint8>, ``returnValue`` : nativeptr<uint32>) = 
        gl.glGetUniformBlockIndex.Invoke(``program``, ``uniformBlockName``) |> NativePtr.write returnValue
    override this.GetUniformBlockIndex(``program`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>, ``returnValue`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``uniformBlockName``
        this.Acquire ``returnValue``
        gl.glGetUniformBlockIndex.Invoke(``program``.Value, NativePtr.ofNativeInt ``uniformBlockName``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetUniformIndices(``program`` : uint32, ``uniformCount`` : uint32, ``uniformNames`` : nativeptr<nativeptr<uint8>>, ``uniformIndices`` : nativeptr<uint32>) = 
        gl.glGetUniformIndices.Invoke(``program``, ``uniformCount``, ``uniformNames``, ``uniformIndices``)
    override this.GetUniformIndices(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformNames`` : aptr<nativeptr<uint8>>, ``uniformIndices`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``uniformCount``
        this.Acquire ``uniformNames``
        this.Acquire ``uniformIndices``
        gl.glGetUniformIndices.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformNames``.Pointer, NativePtr.ofNativeInt ``uniformIndices``.Pointer)
    override this.GetVertexAttribIiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<int>) = 
        gl.glGetVertexAttribIiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribIiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<int>) = 
        this.Acquire ``index``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetVertexAttribIiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribIuiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<uint32>) = 
        gl.glGetVertexAttribIuiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribIuiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<uint32>) = 
        this.Acquire ``index``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetVertexAttribIuiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.InvalidateFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>) = 
        gl.glInvalidateFramebuffer.Invoke(``target``, ``numAttachments``, ``attachments``)
    override this.InvalidateFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>) = 
        this.Acquire ``target``
        this.Acquire ``numAttachments``
        this.Acquire ``attachments``
        gl.glInvalidateFramebuffer.Invoke(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer)
    override this.InvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        WrappedCommands.glInvalidateSubFramebuffer(``target``, ``numAttachments``, ``attachments``, ``x``, ``y``, ``width``, ``height``)
    override this.InvalidateSubFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``numAttachments``
        this.Acquire ``attachments``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        WrappedCommands.glInvalidateSubFramebuffer(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.IsQuery(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsQuery.Invoke(``id``) |> NativePtr.write returnValue
    override this.IsQuery(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``id``
        this.Acquire ``returnValue``
        gl.glIsQuery.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsSampler(``sampler`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsSampler.Invoke(``sampler``) |> NativePtr.write returnValue
    override this.IsSampler(``sampler`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``sampler``
        this.Acquire ``returnValue``
        gl.glIsSampler.Invoke(``sampler``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsSync(``sync`` : nativeint, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsSync.Invoke(``sync``) |> NativePtr.write returnValue
    override this.IsSync(``sync`` : aptr<nativeint>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``sync``
        this.Acquire ``returnValue``
        gl.glIsSync.Invoke(``sync``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsTransformFeedback(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsTransformFeedback.Invoke(``id``) |> NativePtr.write returnValue
    override this.IsTransformFeedback(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``id``
        this.Acquire ``returnValue``
        gl.glIsTransformFeedback.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsVertexArray(``array`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsVertexArray.Invoke(``array``) |> NativePtr.write returnValue
    override this.IsVertexArray(``array`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``array``
        this.Acquire ``returnValue``
        gl.glIsVertexArray.Invoke(``array``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.PauseTransformFeedback() = 
        gl.glPauseTransformFeedback.Invoke()
    override this.ProgramBinary(``program`` : uint32, ``binaryFormat`` : GLEnum, ``binary`` : nativeint, ``length`` : uint32) = 
        gl.glProgramBinary.Invoke(``program``, ``binaryFormat``, ``binary``, ``length``)
    override this.ProgramBinary(``program`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T3>, ``length`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``binaryFormat``
        this.Acquire ``binary``
        this.Acquire ``length``
        gl.glProgramBinary.Invoke(``program``.Value, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
    override this.ProgramParameteri(``program`` : uint32, ``pname`` : ProgramParameterPName, ``value`` : int) = 
        gl.glProgramParameteri.Invoke(``program``, ``pname``, ``value``)
    override this.ProgramParameteri(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramParameterPName>, ``value`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``pname``
        this.Acquire ``value``
        gl.glProgramParameteri.Invoke(``program``.Value, ``pname``.Value, ``value``.Value)
    override this.ReadBuffer(``src`` : ReadBufferMode) = 
        gl.glReadBuffer.Invoke(``src``)
    override this.ReadBuffer(``src`` : aptr<ReadBufferMode>) = 
        this.Acquire ``src``
        gl.glReadBuffer.Invoke(``src``.Value)
    override this.RenderbufferStorageMultisample(``target`` : RenderbufferTarget, ``samples`` : uint32, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        gl.glRenderbufferStorageMultisample.Invoke(``target``, ``samples``, ``internalformat``, ``width``, ``height``)
    override this.RenderbufferStorageMultisample(``target`` : aptr<RenderbufferTarget>, ``samples`` : aptr<uint32>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``samples``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        gl.glRenderbufferStorageMultisample.Invoke(``target``.Value, ``samples``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.ResumeTransformFeedback() = 
        gl.glResumeTransformFeedback.Invoke()
    override this.SamplerParameteri(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : int) = 
        gl.glSamplerParameteri.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameteri(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``param``
        gl.glSamplerParameteri.Invoke(``sampler``.Value, ``pname``.Value, ``param``.Value)
    override this.SamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : nativeptr<int>) = 
        gl.glSamplerParameteriv.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``param``
        gl.glSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
    override this.SamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) = 
        WrappedCommands.glSamplerParameterf(``sampler``, ``pname``, ``param``)
    override this.SamplerParameterf(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``param``
        WrappedCommands.glSamplerParameterf(``sampler``.Value, ``pname``.Value, ``param``.Value)
    override this.SamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : nativeptr<float32>) = 
        gl.glSamplerParameterfv.Invoke(``sampler``, ``pname``, ``param``)
    override this.SamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        this.Acquire ``sampler``
        this.Acquire ``pname``
        this.Acquire ``param``
        gl.glSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
    override this.TexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        WrappedCommands.glTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``format``, ``type``, ``pixels``)
    override this.TexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``depth``
        this.Acquire ``border``
        this.Acquire ``format``
        this.Acquire ``type``
        this.Acquire ``pixels``
        WrappedCommands.glTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TexStorage2D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        gl.glTexStorage2D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``)
    override this.TexStorage2D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``levels``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        gl.glTexStorage2D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.TexStorage3D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32) = 
        gl.glTexStorage3D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``, ``depth``)
    override this.TexStorage3D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``levels``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``depth``
        gl.glTexStorage3D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value)
    override this.TexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        WrappedCommands.glTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``type``, ``pixels``)
    override this.TexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``zoffset``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``depth``
        this.Acquire ``format``
        this.Acquire ``type``
        this.Acquire ``pixels``
        WrappedCommands.glTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TransformFeedbackVaryings(``program`` : uint32, ``count`` : uint32, ``varyings`` : nativeptr<nativeptr<uint8>>, ``bufferMode`` : TransformFeedbackBufferMode) = 
        gl.glTransformFeedbackVaryings.Invoke(``program``, ``count``, ``varyings``, ``bufferMode``)
    override this.TransformFeedbackVaryings(``program`` : aptr<uint32>, ``count`` : aptr<uint32>, ``varyings`` : aptr<nativeptr<uint8>>, ``bufferMode`` : aptr<TransformFeedbackBufferMode>) = 
        this.Acquire ``program``
        this.Acquire ``count``
        this.Acquire ``varyings``
        this.Acquire ``bufferMode``
        gl.glTransformFeedbackVaryings.Invoke(``program``.Value, ``count``.Value, NativePtr.ofNativeInt ``varyings``.Pointer, ``bufferMode``.Value)
    override this.Uniform1ui(``location`` : int, ``v0`` : uint32) = 
        gl.glUniform1ui.Invoke(``location``, ``v0``)
    override this.Uniform1ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        gl.glUniform1ui.Invoke(``location``.Value, ``v0``.Value)
    override this.Uniform1uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        gl.glUniform1uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform1uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32) = 
        gl.glUniform2ui.Invoke(``location``, ``v0``, ``v1``)
    override this.Uniform2ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        gl.glUniform2ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        gl.glUniform2uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform2uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32) = 
        gl.glUniform3ui.Invoke(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        gl.glUniform3ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        gl.glUniform3uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform3uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32, ``v3`` : uint32) = 
        gl.glUniform4ui.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>, ``v3`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        this.Acquire ``v3``
        gl.glUniform4ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        gl.glUniform4uiv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform4uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformBlockBinding(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``uniformBlockBinding`` : uint32) = 
        gl.glUniformBlockBinding.Invoke(``program``, ``uniformBlockIndex``, ``uniformBlockBinding``)
    override this.UniformBlockBinding(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``uniformBlockBinding`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``uniformBlockIndex``
        this.Acquire ``uniformBlockBinding``
        gl.glUniformBlockBinding.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``uniformBlockBinding``.Value)
    override this.UniformMatrix2x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix2x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix2x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix2x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix2x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix2x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix3x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix3x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix3x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix3x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix4x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix4x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix4x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix4x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.VertexAttribDivisor(``index`` : uint32, ``divisor`` : uint32) = 
        gl.glVertexAttribDivisor.Invoke(``index``, ``divisor``)
    override this.VertexAttribDivisor(``index`` : aptr<uint32>, ``divisor`` : aptr<uint32>) = 
        this.Acquire ``index``
        this.Acquire ``divisor``
        gl.glVertexAttribDivisor.Invoke(``index``.Value, ``divisor``.Value)
    override this.VertexAttribI4i(``index`` : uint32, ``x`` : int, ``y`` : int, ``z`` : int, ``w`` : int) = 
        gl.glVertexAttribI4i.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttribI4i(``index`` : aptr<uint32>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``z`` : aptr<int>, ``w`` : aptr<int>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``z``
        this.Acquire ``w``
        gl.glVertexAttribI4i.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttribI4ui(``index`` : uint32, ``x`` : uint32, ``y`` : uint32, ``z`` : uint32, ``w`` : uint32) = 
        gl.glVertexAttribI4ui.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttribI4ui(``index`` : aptr<uint32>, ``x`` : aptr<uint32>, ``y`` : aptr<uint32>, ``z`` : aptr<uint32>, ``w`` : aptr<uint32>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``z``
        this.Acquire ``w``
        gl.glVertexAttribI4ui.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttribI4iv(``index`` : uint32, ``v`` : nativeptr<int>) = 
        gl.glVertexAttribI4iv.Invoke(``index``, ``v``)
    override this.VertexAttribI4iv(``index`` : aptr<uint32>, ``v`` : aptr<int>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttribI4iv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribI4uiv(``index`` : uint32, ``v`` : nativeptr<uint32>) = 
        gl.glVertexAttribI4uiv.Invoke(``index``, ``v``)
    override this.VertexAttribI4uiv(``index`` : aptr<uint32>, ``v`` : aptr<uint32>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttribI4uiv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribIPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribIType, ``stride`` : uint32, ``pointer`` : nativeint) = 
        gl.glVertexAttribIPointer.Invoke(``index``, ``size``, ``type``, ``stride``, ``pointer``)
    override this.VertexAttribIPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribIType>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T5>) = 
        this.Acquire ``index``
        this.Acquire ``size``
        this.Acquire ``type``
        this.Acquire ``stride``
        this.Acquire ``pointer``
        gl.glVertexAttribIPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``stride``.Value, ``pointer``.Pointer)
    override this.WaitSync(``sync`` : nativeint, ``flags`` : SyncBehaviorFlags, ``timeout`` : uint64) = 
        gl.glWaitSync.Invoke(``sync``, ``flags``, ``timeout``)
    override this.WaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncBehaviorFlags>, ``timeout`` : aptr<uint64>) = 
        this.Acquire ``sync``
        this.Acquire ``flags``
        this.Acquire ``timeout``
        gl.glWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value)
    override this.ActiveTexture(``texture`` : TextureUnit) = 
        gl.glActiveTexture.Invoke(``texture``)
    override this.ActiveTexture(``texture`` : aptr<TextureUnit>) = 
        this.Acquire ``texture``
        gl.glActiveTexture.Invoke(``texture``.Value)
    override this.AttachShader(``program`` : uint32, ``shader`` : uint32) = 
        gl.glAttachShader.Invoke(``program``, ``shader``)
    override this.AttachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``shader``
        gl.glAttachShader.Invoke(``program``.Value, ``shader``.Value)
    override this.BindAttribLocation(``program`` : uint32, ``index`` : uint32, ``name`` : nativeptr<uint8>) = 
        gl.glBindAttribLocation.Invoke(``program``, ``index``, ``name``)
    override this.BindAttribLocation(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``name`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``index``
        this.Acquire ``name``
        gl.glBindAttribLocation.Invoke(``program``.Value, ``index``.Value, NativePtr.ofNativeInt ``name``.Pointer)
    override this.BindBuffer(``target`` : BufferTargetARB, ``buffer`` : uint32) = 
        gl.glBindBuffer.Invoke(``target``, ``buffer``)
    override this.BindBuffer(``target`` : aptr<BufferTargetARB>, ``buffer`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``buffer``
        gl.glBindBuffer.Invoke(``target``.Value, ``buffer``.Value)
    override this.BindFramebuffer(``target`` : FramebufferTarget, ``framebuffer`` : uint32) = 
        gl.glBindFramebuffer.Invoke(``target``, ``framebuffer``)
    override this.BindFramebuffer(``target`` : aptr<FramebufferTarget>, ``framebuffer`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``framebuffer``
        gl.glBindFramebuffer.Invoke(``target``.Value, ``framebuffer``.Value)
    override this.BindRenderbuffer(``target`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        gl.glBindRenderbuffer.Invoke(``target``, ``renderbuffer``)
    override this.BindRenderbuffer(``target`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``renderbuffer``
        gl.glBindRenderbuffer.Invoke(``target``.Value, ``renderbuffer``.Value)
    override this.BindTexture(``target`` : TextureTarget, ``texture`` : uint32) = 
        gl.glBindTexture.Invoke(``target``, ``texture``)
    override this.BindTexture(``target`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``texture``
        gl.glBindTexture.Invoke(``target``.Value, ``texture``.Value)
    override this.BlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        WrappedCommands.glBlendColor(``red``, ``green``, ``blue``, ``alpha``)
    override this.BlendColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        this.Acquire ``red``
        this.Acquire ``green``
        this.Acquire ``blue``
        this.Acquire ``alpha``
        WrappedCommands.glBlendColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.BlendEquation(``mode`` : BlendEquationModeEXT) = 
        gl.glBlendEquation.Invoke(``mode``)
    override this.BlendEquation(``mode`` : aptr<BlendEquationModeEXT>) = 
        this.Acquire ``mode``
        gl.glBlendEquation.Invoke(``mode``.Value)
    override this.BlendEquationSeparate(``modeRGB`` : BlendEquationModeEXT, ``modeAlpha`` : BlendEquationModeEXT) = 
        gl.glBlendEquationSeparate.Invoke(``modeRGB``, ``modeAlpha``)
    override this.BlendEquationSeparate(``modeRGB`` : aptr<BlendEquationModeEXT>, ``modeAlpha`` : aptr<BlendEquationModeEXT>) = 
        this.Acquire ``modeRGB``
        this.Acquire ``modeAlpha``
        gl.glBlendEquationSeparate.Invoke(``modeRGB``.Value, ``modeAlpha``.Value)
    override this.BlendFunc(``sfactor`` : BlendingFactor, ``dfactor`` : BlendingFactor) = 
        gl.glBlendFunc.Invoke(``sfactor``, ``dfactor``)
    override this.BlendFunc(``sfactor`` : aptr<BlendingFactor>, ``dfactor`` : aptr<BlendingFactor>) = 
        this.Acquire ``sfactor``
        this.Acquire ``dfactor``
        gl.glBlendFunc.Invoke(``sfactor``.Value, ``dfactor``.Value)
    override this.BlendFuncSeparate(``sfactorRGB`` : BlendingFactor, ``dfactorRGB`` : BlendingFactor, ``sfactorAlpha`` : BlendingFactor, ``dfactorAlpha`` : BlendingFactor) = 
        gl.glBlendFuncSeparate.Invoke(``sfactorRGB``, ``dfactorRGB``, ``sfactorAlpha``, ``dfactorAlpha``)
    override this.BlendFuncSeparate(``sfactorRGB`` : aptr<BlendingFactor>, ``dfactorRGB`` : aptr<BlendingFactor>, ``sfactorAlpha`` : aptr<BlendingFactor>, ``dfactorAlpha`` : aptr<BlendingFactor>) = 
        this.Acquire ``sfactorRGB``
        this.Acquire ``dfactorRGB``
        this.Acquire ``sfactorAlpha``
        this.Acquire ``dfactorAlpha``
        gl.glBlendFuncSeparate.Invoke(``sfactorRGB``.Value, ``dfactorRGB``.Value, ``sfactorAlpha``.Value, ``dfactorAlpha``.Value)
    override this.BufferData(``target`` : BufferTargetARB, ``size`` : unativeint, ``data`` : nativeint, ``usage`` : BufferUsageARB) = 
        gl.glBufferData.Invoke(``target``, ``size``, ``data``, ``usage``)
    override this.BufferData(``target`` : aptr<BufferTargetARB>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>, ``usage`` : aptr<BufferUsageARB>) = 
        this.Acquire ``target``
        this.Acquire ``size``
        this.Acquire ``data``
        this.Acquire ``usage``
        gl.glBufferData.Invoke(``target``.Value, ``size``.Value, ``data``.Value, ``usage``.Value)
    override this.BufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``data`` : nativeint) = 
        gl.glBufferSubData.Invoke(``target``, ``offset``, ``size``, ``data``)
    override this.BufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``offset``
        this.Acquire ``size``
        this.Acquire ``data``
        gl.glBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``data``.Value)
    override this.CheckFramebufferStatus(``target`` : FramebufferTarget, ``returnValue`` : nativeptr<GLEnum>) = 
        gl.glCheckFramebufferStatus.Invoke(``target``) |> NativePtr.write returnValue
    override this.CheckFramebufferStatus(``target`` : aptr<FramebufferTarget>, ``returnValue`` : aptr<GLEnum>) = 
        this.Acquire ``target``
        this.Acquire ``returnValue``
        gl.glCheckFramebufferStatus.Invoke(``target``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.Clear(``mask`` : ClearBufferMask) = 
        gl.glClear.Invoke(``mask``)
    override this.Clear(``mask`` : aptr<ClearBufferMask>) = 
        this.Acquire ``mask``
        gl.glClear.Invoke(``mask``.Value)
    override this.ClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        WrappedCommands.glClearColor(``red``, ``green``, ``blue``, ``alpha``)
    override this.ClearColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        this.Acquire ``red``
        this.Acquire ``green``
        this.Acquire ``blue``
        this.Acquire ``alpha``
        WrappedCommands.glClearColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.ClearDepthf(``d`` : float32) = 
        WrappedCommands.glClearDepthf(``d``)
    override this.ClearDepthf(``d`` : aptr<float32>) = 
        this.Acquire ``d``
        WrappedCommands.glClearDepthf(``d``.Value)
    override this.ClearStencil(``s`` : int) = 
        gl.glClearStencil.Invoke(``s``)
    override this.ClearStencil(``s`` : aptr<int>) = 
        this.Acquire ``s``
        gl.glClearStencil.Invoke(``s``.Value)
    override this.ColorMask(``red`` : Boolean, ``green`` : Boolean, ``blue`` : Boolean, ``alpha`` : Boolean) = 
        gl.glColorMask.Invoke(``red``, ``green``, ``blue``, ``alpha``)
    override this.ColorMask(``red`` : aptr<Boolean>, ``green`` : aptr<Boolean>, ``blue`` : aptr<Boolean>, ``alpha`` : aptr<Boolean>) = 
        this.Acquire ``red``
        this.Acquire ``green``
        this.Acquire ``blue``
        this.Acquire ``alpha``
        gl.glColorMask.Invoke(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
    override this.CompileShader(``shader`` : uint32) = 
        gl.glCompileShader.Invoke(``shader``)
    override this.CompileShader(``shader`` : aptr<uint32>) = 
        this.Acquire ``shader``
        gl.glCompileShader.Invoke(``shader``.Value)
    override this.CompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        WrappedCommands.glCompressedTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``imageSize``, ``data``)
    override this.CompressedTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``border``
        this.Acquire ``imageSize``
        this.Acquire ``data``
        WrappedCommands.glCompressedTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        WrappedCommands.glCompressedTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``imageSize``, ``data``)
    override this.CompressedTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``format``
        this.Acquire ``imageSize``
        this.Acquire ``data``
        WrappedCommands.glCompressedTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
    override this.CopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) = 
        WrappedCommands.glCopyTexImage2D(``target``, ``level``, ``internalformat``, ``x``, ``y``, ``width``, ``height``, ``border``)
    override this.CopyTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``internalformat``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``border``
        WrappedCommands.glCopyTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``border``.Value)
    override this.CopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        WrappedCommands.glCopyTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``x``, ``y``, ``width``, ``height``)
    override this.CopyTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        WrappedCommands.glCopyTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.CreateProgram(``returnValue`` : nativeptr<uint32>) = 
        gl.glCreateProgram.Invoke() |> NativePtr.write returnValue
    override this.CreateProgram(``returnValue`` : aptr<uint32>) = 
        this.Acquire ``returnValue``
        gl.glCreateProgram.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CreateShader(``type`` : ShaderType, ``returnValue`` : nativeptr<uint32>) = 
        gl.glCreateShader.Invoke(``type``) |> NativePtr.write returnValue
    override this.CreateShader(``type`` : aptr<ShaderType>, ``returnValue`` : aptr<uint32>) = 
        this.Acquire ``type``
        this.Acquire ``returnValue``
        gl.glCreateShader.Invoke(``type``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.CullFace(``mode`` : CullFaceMode) = 
        gl.glCullFace.Invoke(``mode``)
    override this.CullFace(``mode`` : aptr<CullFaceMode>) = 
        this.Acquire ``mode``
        gl.glCullFace.Invoke(``mode``.Value)
    override this.DeleteBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        gl.glDeleteBuffers.Invoke(``n``, ``buffers``)
    override this.DeleteBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``buffers``
        gl.glDeleteBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
    override this.DeleteFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        gl.glDeleteFramebuffers.Invoke(``n``, ``framebuffers``)
    override this.DeleteFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``framebuffers``
        gl.glDeleteFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
    override this.DeleteProgram(``program`` : uint32) = 
        gl.glDeleteProgram.Invoke(``program``)
    override this.DeleteProgram(``program`` : aptr<uint32>) = 
        this.Acquire ``program``
        gl.glDeleteProgram.Invoke(``program``.Value)
    override this.DeleteRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        gl.glDeleteRenderbuffers.Invoke(``n``, ``renderbuffers``)
    override this.DeleteRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``renderbuffers``
        gl.glDeleteRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
    override this.DeleteShader(``shader`` : uint32) = 
        gl.glDeleteShader.Invoke(``shader``)
    override this.DeleteShader(``shader`` : aptr<uint32>) = 
        this.Acquire ``shader``
        gl.glDeleteShader.Invoke(``shader``.Value)
    override this.DeleteTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        gl.glDeleteTextures.Invoke(``n``, ``textures``)
    override this.DeleteTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``textures``
        gl.glDeleteTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
    override this.DepthFunc(``func`` : DepthFunction) = 
        gl.glDepthFunc.Invoke(``func``)
    override this.DepthFunc(``func`` : aptr<DepthFunction>) = 
        this.Acquire ``func``
        gl.glDepthFunc.Invoke(``func``.Value)
    override this.DepthMask(``flag`` : Boolean) = 
        gl.glDepthMask.Invoke(``flag``)
    override this.DepthMask(``flag`` : aptr<Boolean>) = 
        this.Acquire ``flag``
        gl.glDepthMask.Invoke(``flag``.Value)
    override this.DepthRangef(``n`` : float32, ``f`` : float32) = 
        WrappedCommands.glDepthRangef(``n``, ``f``)
    override this.DepthRangef(``n`` : aptr<float32>, ``f`` : aptr<float32>) = 
        this.Acquire ``n``
        this.Acquire ``f``
        WrappedCommands.glDepthRangef(``n``.Value, ``f``.Value)
    override this.DetachShader(``program`` : uint32, ``shader`` : uint32) = 
        gl.glDetachShader.Invoke(``program``, ``shader``)
    override this.DetachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``shader``
        gl.glDetachShader.Invoke(``program``.Value, ``shader``.Value)
    override this.Disable(``cap`` : EnableCap) = 
        gl.glDisable.Invoke(``cap``)
    override this.Disable(``cap`` : aptr<EnableCap>) = 
        this.Acquire ``cap``
        gl.glDisable.Invoke(``cap``.Value)
    override this.DisableVertexAttribArray(``index`` : uint32) = 
        gl.glDisableVertexAttribArray.Invoke(``index``)
    override this.DisableVertexAttribArray(``index`` : aptr<uint32>) = 
        this.Acquire ``index``
        gl.glDisableVertexAttribArray.Invoke(``index``.Value)
    override this.DrawArrays(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32) = 
        gl.glDrawArrays.Invoke(``mode``, ``first``, ``count``)
    override this.DrawArrays(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>) = 
        this.Acquire ``mode``
        this.Acquire ``first``
        this.Acquire ``count``
        gl.glDrawArrays.Invoke(``mode``.Value, ``first``.Value, ``count``.Value)
    override this.DrawElements(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        gl.glDrawElements.Invoke(``mode``, ``count``, ``type``, ``indices``)
    override this.DrawElements(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``count``
        this.Acquire ``type``
        this.Acquire ``indices``
        gl.glDrawElements.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
    override this.Enable(``cap`` : EnableCap) = 
        gl.glEnable.Invoke(``cap``)
    override this.Enable(``cap`` : aptr<EnableCap>) = 
        this.Acquire ``cap``
        gl.glEnable.Invoke(``cap``.Value)
    override this.EnableVertexAttribArray(``index`` : uint32) = 
        gl.glEnableVertexAttribArray.Invoke(``index``)
    override this.EnableVertexAttribArray(``index`` : aptr<uint32>) = 
        this.Acquire ``index``
        gl.glEnableVertexAttribArray.Invoke(``index``.Value)
    override this.Finish() = 
        gl.glFinish.Invoke()
    override this.Flush() = 
        gl.glFlush.Invoke()
    override this.FramebufferRenderbuffer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``renderbuffertarget`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        gl.glFramebufferRenderbuffer.Invoke(``target``, ``attachment``, ``renderbuffertarget``, ``renderbuffer``)
    override this.FramebufferRenderbuffer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``renderbuffertarget`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``attachment``
        this.Acquire ``renderbuffertarget``
        this.Acquire ``renderbuffer``
        gl.glFramebufferRenderbuffer.Invoke(``target``.Value, ``attachment``.Value, ``renderbuffertarget``.Value, ``renderbuffer``.Value)
    override this.FramebufferTexture2D(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``textarget`` : TextureTarget, ``texture`` : uint32, ``level`` : int) = 
        gl.glFramebufferTexture2D.Invoke(``target``, ``attachment``, ``textarget``, ``texture``, ``level``)
    override this.FramebufferTexture2D(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``textarget`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``attachment``
        this.Acquire ``textarget``
        this.Acquire ``texture``
        this.Acquire ``level``
        gl.glFramebufferTexture2D.Invoke(``target``.Value, ``attachment``.Value, ``textarget``.Value, ``texture``.Value, ``level``.Value)
    override this.FrontFace(``mode`` : FrontFaceDirection) = 
        gl.glFrontFace.Invoke(``mode``)
    override this.FrontFace(``mode`` : aptr<FrontFaceDirection>) = 
        this.Acquire ``mode``
        gl.glFrontFace.Invoke(``mode``.Value)
    override this.GenBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        gl.glGenBuffers.Invoke(``n``, ``buffers``)
    override this.GenBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``buffers``
        gl.glGenBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
    override this.GenerateMipmap(``target`` : TextureTarget) = 
        gl.glGenerateMipmap.Invoke(``target``)
    override this.GenerateMipmap(``target`` : aptr<TextureTarget>) = 
        this.Acquire ``target``
        gl.glGenerateMipmap.Invoke(``target``.Value)
    override this.GenFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        gl.glGenFramebuffers.Invoke(``n``, ``framebuffers``)
    override this.GenFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``framebuffers``
        gl.glGenFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
    override this.GenRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        gl.glGenRenderbuffers.Invoke(``n``, ``renderbuffers``)
    override this.GenRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``renderbuffers``
        gl.glGenRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
    override this.GenTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        gl.glGenTextures.Invoke(``n``, ``textures``)
    override this.GenTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        this.Acquire ``n``
        this.Acquire ``textures``
        gl.glGenTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
    override this.GetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        WrappedCommands.glGetActiveAttrib(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetActiveAttrib(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``index``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``size``
        this.Acquire ``type``
        this.Acquire ``name``
        WrappedCommands.glGetActiveAttrib(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        WrappedCommands.glGetActiveUniform(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
    override this.GetActiveUniform(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``index``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``size``
        this.Acquire ``type``
        this.Acquire ``name``
        WrappedCommands.glGetActiveUniform(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
    override this.GetAttachedShaders(``program`` : uint32, ``maxCount`` : uint32, ``count`` : nativeptr<uint32>, ``shaders`` : nativeptr<uint32>) = 
        gl.glGetAttachedShaders.Invoke(``program``, ``maxCount``, ``count``, ``shaders``)
    override this.GetAttachedShaders(``program`` : aptr<uint32>, ``maxCount`` : aptr<uint32>, ``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>) = 
        this.Acquire ``program``
        this.Acquire ``maxCount``
        this.Acquire ``count``
        this.Acquire ``shaders``
        gl.glGetAttachedShaders.Invoke(``program``.Value, ``maxCount``.Value, NativePtr.ofNativeInt ``count``.Pointer, NativePtr.ofNativeInt ``shaders``.Pointer)
    override this.GetAttribLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        gl.glGetAttribLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetAttribLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``name``
        this.Acquire ``returnValue``
        gl.glGetAttribLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetBooleanv(``pname`` : GetPName, ``data`` : nativeptr<Boolean>) = 
        gl.glGetBooleanv.Invoke(``pname``, ``data``)
    override this.GetBooleanv(``pname`` : aptr<GetPName>, ``data`` : aptr<Boolean>) = 
        this.Acquire ``pname``
        this.Acquire ``data``
        gl.glGetBooleanv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetBufferParameteriv(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int>) = 
        gl.glGetBufferParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetBufferParameteriv(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetBufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetError(``returnValue`` : nativeptr<GLEnum>) = 
        gl.glGetError.Invoke() |> NativePtr.write returnValue
    override this.GetError(``returnValue`` : aptr<GLEnum>) = 
        this.Acquire ``returnValue``
        gl.glGetError.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetFloatv(``pname`` : GetPName, ``data`` : nativeptr<float32>) = 
        gl.glGetFloatv.Invoke(``pname``, ``data``)
    override this.GetFloatv(``pname`` : aptr<GetPName>, ``data`` : aptr<float32>) = 
        this.Acquire ``pname``
        this.Acquire ``data``
        gl.glGetFloatv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetFramebufferAttachmentParameteriv(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``pname`` : FramebufferAttachmentParameterName, ``params`` : nativeptr<int>) = 
        gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``, ``attachment``, ``pname``, ``params``)
    override this.GetFramebufferAttachmentParameteriv(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``pname`` : aptr<FramebufferAttachmentParameterName>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``attachment``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``.Value, ``attachment``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetIntegerv(``pname`` : GetPName, ``data`` : nativeptr<int>) = 
        gl.glGetIntegerv.Invoke(``pname``, ``data``)
    override this.GetIntegerv(``pname`` : aptr<GetPName>, ``data`` : aptr<int>) = 
        this.Acquire ``pname``
        this.Acquire ``data``
        gl.glGetIntegerv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
    override this.GetProgramiv(``program`` : uint32, ``pname`` : ProgramPropertyARB, ``params`` : nativeptr<int>) = 
        gl.glGetProgramiv.Invoke(``program``, ``pname``, ``params``)
    override this.GetProgramiv(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramPropertyARB>, ``params`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetProgramiv.Invoke(``program``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetProgramInfoLog(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        gl.glGetProgramInfoLog.Invoke(``program``, ``bufSize``, ``length``, ``infoLog``)
    override this.GetProgramInfoLog(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        this.Acquire ``program``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``infoLog``
        gl.glGetProgramInfoLog.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
    override this.GetRenderbufferParameteriv(``target`` : RenderbufferTarget, ``pname`` : RenderbufferParameterName, ``params`` : nativeptr<int>) = 
        gl.glGetRenderbufferParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetRenderbufferParameteriv(``target`` : aptr<RenderbufferTarget>, ``pname`` : aptr<RenderbufferParameterName>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetRenderbufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetShaderiv(``shader`` : uint32, ``pname`` : ShaderParameterName, ``params`` : nativeptr<int>) = 
        gl.glGetShaderiv.Invoke(``shader``, ``pname``, ``params``)
    override this.GetShaderiv(``shader`` : aptr<uint32>, ``pname`` : aptr<ShaderParameterName>, ``params`` : aptr<int>) = 
        this.Acquire ``shader``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetShaderiv.Invoke(``shader``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetShaderInfoLog(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        gl.glGetShaderInfoLog.Invoke(``shader``, ``bufSize``, ``length``, ``infoLog``)
    override this.GetShaderInfoLog(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        this.Acquire ``shader``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``infoLog``
        gl.glGetShaderInfoLog.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
    override this.GetShaderPrecisionFormat(``shadertype`` : ShaderType, ``precisiontype`` : PrecisionType, ``range`` : nativeptr<int>, ``precision`` : nativeptr<int>) = 
        gl.glGetShaderPrecisionFormat.Invoke(``shadertype``, ``precisiontype``, ``range``, ``precision``)
    override this.GetShaderPrecisionFormat(``shadertype`` : aptr<ShaderType>, ``precisiontype`` : aptr<PrecisionType>, ``range`` : aptr<int>, ``precision`` : aptr<int>) = 
        this.Acquire ``shadertype``
        this.Acquire ``precisiontype``
        this.Acquire ``range``
        this.Acquire ``precision``
        gl.glGetShaderPrecisionFormat.Invoke(``shadertype``.Value, ``precisiontype``.Value, NativePtr.ofNativeInt ``range``.Pointer, NativePtr.ofNativeInt ``precision``.Pointer)
    override this.GetShaderSource(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``source`` : nativeptr<uint8>) = 
        gl.glGetShaderSource.Invoke(``shader``, ``bufSize``, ``length``, ``source``)
    override this.GetShaderSource(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``source`` : aptr<uint8>) = 
        this.Acquire ``shader``
        this.Acquire ``bufSize``
        this.Acquire ``length``
        this.Acquire ``source``
        gl.glGetShaderSource.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``source``.Pointer)
    override this.GetString(``name`` : StringName, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        gl.glGetString.Invoke(``name``) |> NativePtr.write returnValue
    override this.GetString(``name`` : aptr<StringName>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        this.Acquire ``name``
        this.Acquire ``returnValue``
        gl.glGetString.Invoke(``name``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetTexParameterfv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<float32>) = 
        gl.glGetTexParameterfv.Invoke(``target``, ``pname``, ``params``)
    override this.GetTexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<float32>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetTexParameteriv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<int>) = 
        gl.glGetTexParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.GetTexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformfv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<float32>) = 
        gl.glGetUniformfv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformfv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<float32>) = 
        this.Acquire ``program``
        this.Acquire ``location``
        this.Acquire ``params``
        gl.glGetUniformfv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<int>) = 
        gl.glGetUniformiv.Invoke(``program``, ``location``, ``params``)
    override this.GetUniformiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``location``
        this.Acquire ``params``
        gl.glGetUniformiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetUniformLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        gl.glGetUniformLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
    override this.GetUniformLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        this.Acquire ``program``
        this.Acquire ``name``
        this.Acquire ``returnValue``
        gl.glGetUniformLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.GetVertexAttribfv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<float32>) = 
        gl.glGetVertexAttribfv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribfv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetVertexAttribfv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribiv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<int>) = 
        gl.glGetVertexAttribiv.Invoke(``index``, ``pname``, ``params``)
    override this.GetVertexAttribiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<int>) = 
        this.Acquire ``index``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glGetVertexAttribiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.GetVertexAttribPointerv(``index`` : uint32, ``pname`` : VertexAttribPointerPropertyARB, ``pointer`` : nativeptr<nativeint>) = 
        gl.glGetVertexAttribPointerv.Invoke(``index``, ``pname``, ``pointer``)
    override this.GetVertexAttribPointerv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPointerPropertyARB>, ``pointer`` : aptr<nativeint>) = 
        this.Acquire ``index``
        this.Acquire ``pname``
        this.Acquire ``pointer``
        gl.glGetVertexAttribPointerv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``pointer``.Pointer)
    override this.Hint(``target`` : HintTarget, ``mode`` : HintMode) = 
        gl.glHint.Invoke(``target``, ``mode``)
    override this.Hint(``target`` : aptr<HintTarget>, ``mode`` : aptr<HintMode>) = 
        this.Acquire ``target``
        this.Acquire ``mode``
        gl.glHint.Invoke(``target``.Value, ``mode``.Value)
    override this.IsBuffer(``buffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsBuffer.Invoke(``buffer``) |> NativePtr.write returnValue
    override this.IsBuffer(``buffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``buffer``
        this.Acquire ``returnValue``
        gl.glIsBuffer.Invoke(``buffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsEnabled(``cap`` : EnableCap, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsEnabled.Invoke(``cap``) |> NativePtr.write returnValue
    override this.IsEnabled(``cap`` : aptr<EnableCap>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``cap``
        this.Acquire ``returnValue``
        gl.glIsEnabled.Invoke(``cap``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsFramebuffer(``framebuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsFramebuffer.Invoke(``framebuffer``) |> NativePtr.write returnValue
    override this.IsFramebuffer(``framebuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``framebuffer``
        this.Acquire ``returnValue``
        gl.glIsFramebuffer.Invoke(``framebuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsProgram(``program`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsProgram.Invoke(``program``) |> NativePtr.write returnValue
    override this.IsProgram(``program`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``program``
        this.Acquire ``returnValue``
        gl.glIsProgram.Invoke(``program``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsRenderbuffer(``renderbuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsRenderbuffer.Invoke(``renderbuffer``) |> NativePtr.write returnValue
    override this.IsRenderbuffer(``renderbuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``renderbuffer``
        this.Acquire ``returnValue``
        gl.glIsRenderbuffer.Invoke(``renderbuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsShader(``shader`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsShader.Invoke(``shader``) |> NativePtr.write returnValue
    override this.IsShader(``shader`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``shader``
        this.Acquire ``returnValue``
        gl.glIsShader.Invoke(``shader``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.IsTexture(``texture`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        gl.glIsTexture.Invoke(``texture``) |> NativePtr.write returnValue
    override this.IsTexture(``texture`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        this.Acquire ``texture``
        this.Acquire ``returnValue``
        gl.glIsTexture.Invoke(``texture``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
    override this.LineWidth(``width`` : float32) = 
        WrappedCommands.glLineWidth(``width``)
    override this.LineWidth(``width`` : aptr<float32>) = 
        this.Acquire ``width``
        WrappedCommands.glLineWidth(``width``.Value)
    override this.LinkProgram(``program`` : uint32) = 
        gl.glLinkProgram.Invoke(``program``)
    override this.LinkProgram(``program`` : aptr<uint32>) = 
        this.Acquire ``program``
        gl.glLinkProgram.Invoke(``program``.Value)
    override this.PixelStorei(``pname`` : PixelStoreParameter, ``param`` : int) = 
        gl.glPixelStorei.Invoke(``pname``, ``param``)
    override this.PixelStorei(``pname`` : aptr<PixelStoreParameter>, ``param`` : aptr<int>) = 
        this.Acquire ``pname``
        this.Acquire ``param``
        gl.glPixelStorei.Invoke(``pname``.Value, ``param``.Value)
    override this.PolygonOffset(``factor`` : float32, ``units`` : float32) = 
        WrappedCommands.glPolygonOffset(``factor``, ``units``)
    override this.PolygonOffset(``factor`` : aptr<float32>, ``units`` : aptr<float32>) = 
        this.Acquire ``factor``
        this.Acquire ``units``
        WrappedCommands.glPolygonOffset(``factor``.Value, ``units``.Value)
    override this.ReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        WrappedCommands.glReadPixels(``x``, ``y``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
    override this.ReadPixels(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<'T7>) = 
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``format``
        this.Acquire ``type``
        this.Acquire ``pixels``
        WrappedCommands.glReadPixels(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Pointer)
    override this.ReleaseShaderCompiler() = 
        gl.glReleaseShaderCompiler.Invoke()
    override this.RenderbufferStorage(``target`` : RenderbufferTarget, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        gl.glRenderbufferStorage.Invoke(``target``, ``internalformat``, ``width``, ``height``)
    override this.RenderbufferStorage(``target`` : aptr<RenderbufferTarget>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``target``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        gl.glRenderbufferStorage.Invoke(``target``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
    override this.SampleCoverage(``value`` : float32, ``invert`` : Boolean) = 
        WrappedCommands.glSampleCoverage(``value``, ``invert``)
    override this.SampleCoverage(``value`` : aptr<float32>, ``invert`` : aptr<Boolean>) = 
        this.Acquire ``value``
        this.Acquire ``invert``
        WrappedCommands.glSampleCoverage(``value``.Value, ``invert``.Value)
    override this.Scissor(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        gl.glScissor.Invoke(``x``, ``y``, ``width``, ``height``)
    override this.Scissor(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        gl.glScissor.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.ShaderBinary(``count`` : uint32, ``shaders`` : nativeptr<uint32>, ``binaryFormat`` : ShaderBinaryFormat, ``binary`` : nativeint, ``length`` : uint32) = 
        gl.glShaderBinary.Invoke(``count``, ``shaders``, ``binaryFormat``, ``binary``, ``length``)
    override this.ShaderBinary(``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>, ``binaryFormat`` : aptr<ShaderBinaryFormat>, ``binary`` : aptr<'T4>, ``length`` : aptr<uint32>) = 
        this.Acquire ``count``
        this.Acquire ``shaders``
        this.Acquire ``binaryFormat``
        this.Acquire ``binary``
        this.Acquire ``length``
        gl.glShaderBinary.Invoke(``count``.Value, NativePtr.ofNativeInt ``shaders``.Pointer, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
    override this.ShaderSource(``shader`` : uint32, ``count`` : uint32, ``string`` : nativeptr<nativeptr<uint8>>, ``length`` : nativeptr<int>) = 
        gl.glShaderSource.Invoke(``shader``, ``count``, ``string``, ``length``)
    override this.ShaderSource(``shader`` : aptr<uint32>, ``count`` : aptr<uint32>, ``string`` : aptr<nativeptr<uint8>>, ``length`` : aptr<int>) = 
        this.Acquire ``shader``
        this.Acquire ``count``
        this.Acquire ``string``
        this.Acquire ``length``
        gl.glShaderSource.Invoke(``shader``.Value, ``count``.Value, NativePtr.ofNativeInt ``string``.Pointer, NativePtr.ofNativeInt ``length``.Pointer)
    override this.StencilFunc(``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        gl.glStencilFunc.Invoke(``func``, ``ref``, ``mask``)
    override this.StencilFunc(``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        this.Acquire ``func``
        this.Acquire ``ref``
        this.Acquire ``mask``
        gl.glStencilFunc.Invoke(``func``.Value, ``ref``.Value, ``mask``.Value)
    override this.StencilFuncSeparate(``face`` : StencilFaceDirection, ``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        gl.glStencilFuncSeparate.Invoke(``face``, ``func``, ``ref``, ``mask``)
    override this.StencilFuncSeparate(``face`` : aptr<StencilFaceDirection>, ``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        this.Acquire ``face``
        this.Acquire ``func``
        this.Acquire ``ref``
        this.Acquire ``mask``
        gl.glStencilFuncSeparate.Invoke(``face``.Value, ``func``.Value, ``ref``.Value, ``mask``.Value)
    override this.StencilMask(``mask`` : uint32) = 
        gl.glStencilMask.Invoke(``mask``)
    override this.StencilMask(``mask`` : aptr<uint32>) = 
        this.Acquire ``mask``
        gl.glStencilMask.Invoke(``mask``.Value)
    override this.StencilMaskSeparate(``face`` : StencilFaceDirection, ``mask`` : uint32) = 
        gl.glStencilMaskSeparate.Invoke(``face``, ``mask``)
    override this.StencilMaskSeparate(``face`` : aptr<StencilFaceDirection>, ``mask`` : aptr<uint32>) = 
        this.Acquire ``face``
        this.Acquire ``mask``
        gl.glStencilMaskSeparate.Invoke(``face``.Value, ``mask``.Value)
    override this.StencilOp(``fail`` : StencilOp, ``zfail`` : StencilOp, ``zpass`` : StencilOp) = 
        gl.glStencilOp.Invoke(``fail``, ``zfail``, ``zpass``)
    override this.StencilOp(``fail`` : aptr<StencilOp>, ``zfail`` : aptr<StencilOp>, ``zpass`` : aptr<StencilOp>) = 
        this.Acquire ``fail``
        this.Acquire ``zfail``
        this.Acquire ``zpass``
        gl.glStencilOp.Invoke(``fail``.Value, ``zfail``.Value, ``zpass``.Value)
    override this.StencilOpSeparate(``face`` : StencilFaceDirection, ``sfail`` : StencilOp, ``dpfail`` : StencilOp, ``dppass`` : StencilOp) = 
        gl.glStencilOpSeparate.Invoke(``face``, ``sfail``, ``dpfail``, ``dppass``)
    override this.StencilOpSeparate(``face`` : aptr<StencilFaceDirection>, ``sfail`` : aptr<StencilOp>, ``dpfail`` : aptr<StencilOp>, ``dppass`` : aptr<StencilOp>) = 
        this.Acquire ``face``
        this.Acquire ``sfail``
        this.Acquire ``dpfail``
        this.Acquire ``dppass``
        gl.glStencilOpSeparate.Invoke(``face``.Value, ``sfail``.Value, ``dpfail``.Value, ``dppass``.Value)
    override this.TexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        WrappedCommands.glTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``format``, ``type``, ``pixels``)
    override this.TexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``internalformat``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``border``
        this.Acquire ``format``
        this.Acquire ``type``
        this.Acquire ``pixels``
        WrappedCommands.glTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.TexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) = 
        WrappedCommands.glTexParameterf(``target``, ``pname``, ``param``)
    override this.TexParameterf(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<float32>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``param``
        WrappedCommands.glTexParameterf(``target``.Value, ``pname``.Value, ``param``.Value)
    override this.TexParameterfv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<float32>) = 
        gl.glTexParameterfv.Invoke(``target``, ``pname``, ``params``)
    override this.TexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<float32>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.TexParameteri(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : int) = 
        gl.glTexParameteri.Invoke(``target``, ``pname``, ``param``)
    override this.TexParameteri(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``param``
        gl.glTexParameteri.Invoke(``target``.Value, ``pname``.Value, ``param``.Value)
    override this.TexParameteriv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<int>) = 
        gl.glTexParameteriv.Invoke(``target``, ``pname``, ``params``)
    override this.TexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``pname``
        this.Acquire ``params``
        gl.glTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
    override this.TexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        WrappedCommands.glTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
    override this.TexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``format``
        this.Acquire ``type``
        this.Acquire ``pixels``
        WrappedCommands.glTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
    override this.Uniform1f(``location`` : int, ``v0`` : float32) = 
        WrappedCommands.glUniform1f(``location``, ``v0``)
    override this.Uniform1f(``location`` : aptr<int>, ``v0`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        WrappedCommands.glUniform1f(``location``.Value, ``v0``.Value)
    override this.Uniform1fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        gl.glUniform1fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform1fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform1i(``location`` : int, ``v0`` : int) = 
        gl.glUniform1i.Invoke(``location``, ``v0``)
    override this.Uniform1i(``location`` : aptr<int>, ``v0`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        gl.glUniform1i.Invoke(``location``.Value, ``v0``.Value)
    override this.Uniform1iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        gl.glUniform1iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform1iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform1iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) = 
        WrappedCommands.glUniform2f(``location``, ``v0``, ``v1``)
    override this.Uniform2f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        WrappedCommands.glUniform2f(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        gl.glUniform2fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform2fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform2i(``location`` : int, ``v0`` : int, ``v1`` : int) = 
        gl.glUniform2i.Invoke(``location``, ``v0``, ``v1``)
    override this.Uniform2i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        gl.glUniform2i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
    override this.Uniform2iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        gl.glUniform2iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform2iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform2iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) = 
        WrappedCommands.glUniform3f(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        WrappedCommands.glUniform3f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        gl.glUniform3fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform3fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform3i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int) = 
        gl.glUniform3i.Invoke(``location``, ``v0``, ``v1``, ``v2``)
    override this.Uniform3i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        gl.glUniform3i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
    override this.Uniform3iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        gl.glUniform3iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform3iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform3iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) = 
        WrappedCommands.glUniform4f(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>, ``v3`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        this.Acquire ``v3``
        WrappedCommands.glUniform4f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        gl.glUniform4fv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform4fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.Uniform4i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int, ``v3`` : int) = 
        gl.glUniform4i.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
    override this.Uniform4i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>, ``v3`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``v0``
        this.Acquire ``v1``
        this.Acquire ``v2``
        this.Acquire ``v3``
        gl.glUniform4i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
    override this.Uniform4iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        gl.glUniform4iv.Invoke(``location``, ``count``, ``value``)
    override this.Uniform4iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``value``
        gl.glUniform4iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UniformMatrix4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        gl.glUniformMatrix4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
    override this.UniformMatrix4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        this.Acquire ``location``
        this.Acquire ``count``
        this.Acquire ``transpose``
        this.Acquire ``value``
        gl.glUniformMatrix4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
    override this.UseProgram(``program`` : uint32) = 
        gl.glUseProgram.Invoke(``program``)
    override this.UseProgram(``program`` : aptr<uint32>) = 
        this.Acquire ``program``
        gl.glUseProgram.Invoke(``program``.Value)
    override this.ValidateProgram(``program`` : uint32) = 
        gl.glValidateProgram.Invoke(``program``)
    override this.ValidateProgram(``program`` : aptr<uint32>) = 
        this.Acquire ``program``
        gl.glValidateProgram.Invoke(``program``.Value)
    override this.VertexAttrib1f(``index`` : uint32, ``x`` : float32) = 
        WrappedCommands.glVertexAttrib1f(``index``, ``x``)
    override this.VertexAttrib1f(``index`` : aptr<uint32>, ``x`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        WrappedCommands.glVertexAttrib1f(``index``.Value, ``x``.Value)
    override this.VertexAttrib1fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        gl.glVertexAttrib1fv.Invoke(``index``, ``v``)
    override this.VertexAttrib1fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttrib1fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) = 
        WrappedCommands.glVertexAttrib2f(``index``, ``x``, ``y``)
    override this.VertexAttrib2f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        this.Acquire ``y``
        WrappedCommands.glVertexAttrib2f(``index``.Value, ``x``.Value, ``y``.Value)
    override this.VertexAttrib2fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        gl.glVertexAttrib2fv.Invoke(``index``, ``v``)
    override this.VertexAttrib2fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttrib2fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) = 
        WrappedCommands.glVertexAttrib3f(``index``, ``x``, ``y``, ``z``)
    override this.VertexAttrib3f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``z``
        WrappedCommands.glVertexAttrib3f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value)
    override this.VertexAttrib3fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        gl.glVertexAttrib3fv.Invoke(``index``, ``v``)
    override this.VertexAttrib3fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttrib3fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) = 
        WrappedCommands.glVertexAttrib4f(``index``, ``x``, ``y``, ``z``, ``w``)
    override this.VertexAttrib4f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>, ``w`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``z``
        this.Acquire ``w``
        WrappedCommands.glVertexAttrib4f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
    override this.VertexAttrib4fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        gl.glVertexAttrib4fv.Invoke(``index``, ``v``)
    override this.VertexAttrib4fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        this.Acquire ``index``
        this.Acquire ``v``
        gl.glVertexAttrib4fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
    override this.VertexAttribPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribPointerType, ``normalized`` : Boolean, ``stride`` : uint32, ``pointer`` : nativeint) = 
        gl.glVertexAttribPointer.Invoke(``index``, ``size``, ``type``, ``normalized``, ``stride``, ``pointer``)
    override this.VertexAttribPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribPointerType>, ``normalized`` : aptr<Boolean>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T6>) = 
        this.Acquire ``index``
        this.Acquire ``size``
        this.Acquire ``type``
        this.Acquire ``normalized``
        this.Acquire ``stride``
        this.Acquire ``pointer``
        gl.glVertexAttribPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``normalized``.Value, ``stride``.Value, ``pointer``.Pointer)
    override this.Viewport(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        gl.glViewport.Invoke(``x``, ``y``, ``width``, ``height``)
    override this.Viewport(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        this.Acquire ``x``
        this.Acquire ``y``
        this.Acquire ``width``
        this.Acquire ``height``
        gl.glViewport.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
    override this.GetBufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``dst`` : nativeint) = 
        gl.glGetBufferSubData.Invoke(``target``, ``offset``, ``size``, ``dst``)
    override this.GetBufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``dst`` : aptr<nativeint>) = 
        this.Acquire ``target``
        this.Acquire ``offset``
        this.Acquire ``size``
        this.Acquire ``dst``
        gl.glGetBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``dst``.Value)
    override this.MultiDrawArraysIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``bindingInfo`` : nativeint) = 
        gl.glMultiDrawArraysIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``bindingInfo``)
    override this.MultiDrawArraysIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``indirectBuffer``
        this.Acquire ``count``
        this.Acquire ``bindingInfo``
        gl.glMultiDrawArraysIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``bindingInfo``.Value)
    override this.MultiDrawArrays(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``bindingInfo`` : nativeint) = 
        gl.glMultiDrawArrays.Invoke(``mode``, ``indirect``, ``count``, ``bindingInfo``)
    override this.MultiDrawArrays(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``indirect``
        this.Acquire ``count``
        this.Acquire ``bindingInfo``
        gl.glMultiDrawArrays.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``bindingInfo``.Value)
    override this.MultiDrawElementsIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        gl.glMultiDrawElementsIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``indexType``, ``bindingInfo``)
    override this.MultiDrawElementsIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``indirectBuffer``
        this.Acquire ``count``
        this.Acquire ``indexType``
        this.Acquire ``bindingInfo``
        gl.glMultiDrawElementsIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
    override this.MultiDrawElements(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        gl.glMultiDrawElements.Invoke(``mode``, ``indirect``, ``count``, ``indexType``, ``bindingInfo``)
    override this.MultiDrawElements(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        this.Acquire ``mode``
        this.Acquire ``indirect``
        this.Acquire ``count``
        this.Acquire ``indexType``
        this.Acquire ``bindingInfo``
        gl.glMultiDrawElements.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
    override this.Commit() = 
        gl.glCommit.Invoke()
    override this.TexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) = 
        WrappedCommands.glTexSubImage2DJSImage(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``typ``, ``imgHandle``)
    override this.TexSubImage2DJSImage(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<int>, ``height`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``typ`` : aptr<PixelType>, ``imgHandle`` : aptr<int>) = 
        this.Acquire ``target``
        this.Acquire ``level``
        this.Acquire ``xoffset``
        this.Acquire ``yoffset``
        this.Acquire ``width``
        this.Acquire ``height``
        this.Acquire ``format``
        this.Acquire ``typ``
        this.Acquire ``imgHandle``
        WrappedCommands.glTexSubImage2DJSImage(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``typ``.Value, ``imgHandle``.Value)
type DebugCommandEncoder(device : Device) =
    inherit CommandEncoder(device)
    let gl = GLDelegates.get device.Context
    let mutable currentGL = Unchecked.defaultof<GL>
    let mutable stack : list<int64> = []
    let commands = System.Collections.Generic.List<(unit -> string) * (unit -> unit)>()

    [<DefaultValue; System.ThreadStatic>]
    static val mutable private InCommandStream : bool

    override x.Destroy() =
        commands.Clear()

    override x.Clear() =
        commands.Clear()

    override x.Custom(action : GL -> unit) =
        commands.Add ((fun () -> "custom"), (fun () -> action currentGL))

    override x.Push (location : nativeptr<'a>) =
        let run() = 
            if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack
            else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack
        let name() =
            sprintf "Push(%A)" location
        commands.Add (name, run)
    
    override x.Pop (location : nativeptr<'a>) =
        let run() = 
            let h = List.head stack
            stack <- List.tail stack
            if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)
            else NativePtr.write location (Unchecked.reinterpret (int h))
        let name() =
            sprintf "Pop(%A)" location
        commands.Add (name, run)
    
    override x.Perform gl =
        let o = DebugCommandEncoder.InCommandStream
        if not o then
            DebugCommandEncoder.InCommandStream <- true
        currentGL <- gl
        stack <- []
        for i in 0 .. commands.Count - 1 do
            let (getName, run) = commands.[i]
            let name = getName()
            if not (System.String.IsNullOrWhiteSpace name) then Aardvark.Base.Report.Debug("{0}", name)
            device.CurrentCall <- Some name
            run()
            let err = gl.GetError()
            if err <> GLEnum.NoError then
                Aardvark.Base.Report.Debug("ERROR: {0}", err)
        if not o then
            DebugCommandEncoder.InCommandStream <- false

    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = 
        let location = x.Use location
        let table =
            cases |> Map.ofList |> Map.map (fun _ action -> 
                let res = new DebugCommandEncoder(device)
                x.AddNested res
                action (res :> CommandEncoder)
                res
            )
        let def = 
            let res = new DebugCommandEncoder(device)
            x.AddNested res
            fallback (res :> CommandEncoder)
            res
        let run() =
            let v = location.Value
            match Map.tryFind v table with
            | Some c -> c.UnsafeRunSynchronously currentGL
            | None -> def.UnsafeRunSynchronously currentGL
        let name() =
            ""
        commands.Add(name, run)
    
    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) =
        let run() = Marshal.Copy(src, dst, size)
        let name() = sprintf "memcpy(%A, %A, %A)" src dst size
        commands.Add(name, run)
    
    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        let run() = Marshal.Copy(src.Pointer, dst.Pointer, size.Value)
        let name() = sprintf "memcpy(%A, %A, %A)" src.Pointer dst.Pointer size.Value
        commands.Add(name, run)
    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        let run() = Marshal.Copy(src.Pointer, dst.Value, size.Value)
        let name() = sprintf "memcpy(%A, %A, %A)" src.Pointer dst.Value size.Value
        commands.Add(name, run)
    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        let run() = Marshal.Copy(src.Value, dst.Pointer, size.Value)
        let name() = sprintf "memcpy(%A, %A, %A)" src.Value dst.Pointer size.Value
        commands.Add(name, run)
    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        let src = x.Use src
        let dst = x.Use dst
        let size = x.Use size
        let run() = Marshal.Copy(src.Value, dst.Value, size.Value)
        let name() = sprintf "memcpy(%A, %A, %A)" src.Value dst.Value size.Value
        commands.Add(name, run)
    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) =
        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>
        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>
        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>
        let run() = NativePtr.write res (NativePtr.read a + NativePtr.read b)
        let name() = sprintf "add(%A, %A, %A)" a b res
        commands.Add(name, run)
    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) =
        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>
        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>
        let c = x.Use(c).Pointer |> NativePtr.ofNativeInt<nativeint>
        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>
        let run() = NativePtr.write res (NativePtr.read a + NativePtr.read b * NativePtr.read c)
        let name() = sprintf "mad(%A, %A, %A, %A)" a b c res
        commands.Add(name, run)
    override x.Bgra(values : aptr<byte>, count : aptr<int>) = 
        let values = x.Use(values).Pointer |> NativePtr.ofNativeInt<byte>
        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>
        let run() =
            let mutable off = 0
            for i in 0 .. NativePtr.read count - 1 do
                let t = NativePtr.get values off
                NativePtr.set values off (NativePtr.get values (off + 2))
                NativePtr.set values (off + 2) t
                off <- off + 4
        commands.Add((fun () -> "bgra"), run)
    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = 
        let src = x.Use(src).Pointer |> NativePtr.ofNativeInt<byte>
        let dst = x.Use(dst).Pointer |> NativePtr.ofNativeInt<byte>
        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>
        let run() =
            let mutable off = 0
            for i in 0 .. NativePtr.read count - 1 do
                NativePtr.set dst (off + 2) (NativePtr.get src (off+0))
                NativePtr.set dst (off + 1) (NativePtr.get src (off+1))
                NativePtr.set dst (off + 0) (NativePtr.get src (off+2))
                NativePtr.set dst (off + 3) (NativePtr.get src (off+3))
                off <- off + 4
        commands.Add((fun () -> "copyBgra"), run)
    override this.Call(func : aptr<nativeint>) =
        let tDel = DelegateType.Get([], typeof<unit>)
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X()" func.Value 
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [|  |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X()" func.Value 
            let run() =
                del.DynamicInvoke [|  |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>) =
        let tDel = DelegateType.Get([typeof<'a>], typeof<unit>)
        let arg0 = this.Use arg0
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A)" func.Value arg0.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A)" func.Value arg0.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A)" func.Value arg0.Value arg1.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A)" func.Value arg0.Value arg1.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        let arg6 = this.Use arg6
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value arg6.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value arg6.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>, arg7 : aptr<'h>) =
        let tDel = DelegateType.Get([typeof<'a>; typeof<'b>; typeof<'c>; typeof<'d>; typeof<'e>; typeof<'f>; typeof<'g>; typeof<'h>], typeof<unit>)
        let arg0 = this.Use arg0
        let arg1 = this.Use arg1
        let arg2 = this.Use arg2
        let arg3 = this.Use arg3
        let arg4 = this.Use arg4
        let arg5 = this.Use arg5
        let arg6 = this.Use arg6
        let arg7 = this.Use arg7
        if func.IsVolatile then
            let func = this.Use func
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value arg6.Value arg7.Value
            let run() =
                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj; arg7.Value :> obj |] |> ignore
            commands.Add(name, run)
        else
            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)
            let name() =
                sprintf "%016X(%A, %A, %A, %A, %A, %A, %A, %A)" func.Value arg0.Value arg1.Value arg2.Value arg3.Value arg4.Value arg5.Value arg6.Value arg7.Value
            let run() =
                del.DynamicInvoke [| arg0.Value :> obj; arg1.Value :> obj; arg2.Value :> obj; arg3.Value :> obj; arg4.Value :> obj; arg5.Value :> obj; arg6.Value :> obj; arg7.Value :> obj |] |> ignore
            commands.Add(name, run)

    override this.BeginQuery(``target`` : QueryTarget, ``id`` : uint32) = 
        let run() = gl.glBeginQuery.Invoke(``target``, ``id``)
        let name() = sprintf "BeginQuery(%A, %A)" ``target`` ``id``
        commands.Add((name, run))
    override this.BeginQuery(``target`` : aptr<QueryTarget>, ``id`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``id`` = this.Use ``id``
        let run() = gl.glBeginQuery.Invoke(``target``.Value, ``id``.Value)
        let name() = sprintf "BeginQuery(%A, %A)" (``target``.Value) (``id``.Value)
        commands.Add((name, run))
    override this.BeginTransformFeedback(``primitiveMode`` : PrimitiveType) = 
        let run() = gl.glBeginTransformFeedback.Invoke(``primitiveMode``)
        let name() = sprintf "BeginTransformFeedback(%A)" ``primitiveMode``
        commands.Add((name, run))
    override this.BeginTransformFeedback(``primitiveMode`` : aptr<PrimitiveType>) = 
        let ``primitiveMode`` = this.Use ``primitiveMode``
        let run() = gl.glBeginTransformFeedback.Invoke(``primitiveMode``.Value)
        let name() = sprintf "BeginTransformFeedback(%A)" (``primitiveMode``.Value)
        commands.Add((name, run))
    override this.BindBufferBase(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32) = 
        let run() = gl.glBindBufferBase.Invoke(``target``, ``index``, ``buffer``)
        let name() = sprintf "BindBufferBase(%A, %A, %A)" ``target`` ``index`` ``buffer``
        commands.Add((name, run))
    override this.BindBufferBase(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``buffer`` = this.Use ``buffer``
        let run() = gl.glBindBufferBase.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value)
        let name() = sprintf "BindBufferBase(%A, %A, %A)" (``target``.Value) (``index``.Value) (``buffer``.Value)
        commands.Add((name, run))
    override this.BindBufferRange(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32, ``offset`` : nativeint, ``size`` : unativeint) = 
        let run() = gl.glBindBufferRange.Invoke(``target``, ``index``, ``buffer``, ``offset``, ``size``)
        let name() = sprintf "BindBufferRange(%A, %A, %A, %A, %A)" ``target`` ``index`` ``buffer`` ``offset`` ``size``
        commands.Add((name, run))
    override this.BindBufferRange(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``buffer`` = this.Use ``buffer``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        let run() = gl.glBindBufferRange.Invoke(``target``.Value, ``index``.Value, ``buffer``.Value, ``offset``.Value, ``size``.Value)
        let name() = sprintf "BindBufferRange(%A, %A, %A, %A, %A)" (``target``.Value) (``index``.Value) (``buffer``.Value) (``offset``.Value) (``size``.Value)
        commands.Add((name, run))
    override this.BindSampler(``unit`` : uint32, ``sampler`` : uint32) = 
        let run() = gl.glBindSampler.Invoke(``unit``, ``sampler``)
        let name() = sprintf "BindSampler(%A, %A)" ``unit`` ``sampler``
        commands.Add((name, run))
    override this.BindSampler(``unit`` : aptr<uint32>, ``sampler`` : aptr<uint32>) = 
        let ``unit`` = this.Use ``unit``
        let ``sampler`` = this.Use ``sampler``
        let run() = gl.glBindSampler.Invoke(``unit``.Value, ``sampler``.Value)
        let name() = sprintf "BindSampler(%A, %A)" (``unit``.Value) (``sampler``.Value)
        commands.Add((name, run))
    override this.BindTransformFeedback(``target`` : BindTransformFeedbackTarget, ``id`` : uint32) = 
        let run() = gl.glBindTransformFeedback.Invoke(``target``, ``id``)
        let name() = sprintf "BindTransformFeedback(%A, %A)" ``target`` ``id``
        commands.Add((name, run))
    override this.BindTransformFeedback(``target`` : aptr<BindTransformFeedbackTarget>, ``id`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``id`` = this.Use ``id``
        let run() = gl.glBindTransformFeedback.Invoke(``target``.Value, ``id``.Value)
        let name() = sprintf "BindTransformFeedback(%A, %A)" (``target``.Value) (``id``.Value)
        commands.Add((name, run))
    override this.BindVertexArray(``array`` : uint32) = 
        let run() = gl.glBindVertexArray.Invoke(``array``)
        let name() = sprintf "BindVertexArray(%A)" ``array``
        commands.Add((name, run))
    override this.BindVertexArray(``array`` : aptr<uint32>) = 
        let ``array`` = this.Use ``array``
        let run() = gl.glBindVertexArray.Invoke(``array``.Value)
        let name() = sprintf "BindVertexArray(%A)" (``array``.Value)
        commands.Add((name, run))
    override this.BlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) = 
        let run() = WrappedCommands.glBlitFramebuffer(``srcX0``, ``srcY0``, ``srcX1``, ``srcY1``, ``dstX0``, ``dstY0``, ``dstX1``, ``dstY1``, ``mask``, ``filter``)
        let name() = sprintf "BlitFramebuffer(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" ``srcX0`` ``srcY0`` ``srcX1`` ``srcY1`` ``dstX0`` ``dstY0`` ``dstX1`` ``dstY1`` ``mask`` ``filter``
        commands.Add((name, run))
    override this.BlitFramebuffer(``srcX0`` : aptr<int>, ``srcY0`` : aptr<int>, ``srcX1`` : aptr<int>, ``srcY1`` : aptr<int>, ``dstX0`` : aptr<int>, ``dstY0`` : aptr<int>, ``dstX1`` : aptr<int>, ``dstY1`` : aptr<int>, ``mask`` : aptr<ClearBufferMask>, ``filter`` : aptr<BlitFramebufferFilter>) = 
        let ``srcX0`` = this.Use ``srcX0``
        let ``srcY0`` = this.Use ``srcY0``
        let ``srcX1`` = this.Use ``srcX1``
        let ``srcY1`` = this.Use ``srcY1``
        let ``dstX0`` = this.Use ``dstX0``
        let ``dstY0`` = this.Use ``dstY0``
        let ``dstX1`` = this.Use ``dstX1``
        let ``dstY1`` = this.Use ``dstY1``
        let ``mask`` = this.Use ``mask``
        let ``filter`` = this.Use ``filter``
        let run() = WrappedCommands.glBlitFramebuffer(``srcX0``.Value, ``srcY0``.Value, ``srcX1``.Value, ``srcY1``.Value, ``dstX0``.Value, ``dstY0``.Value, ``dstX1``.Value, ``dstY1``.Value, ``mask``.Value, ``filter``.Value)
        let name() = sprintf "BlitFramebuffer(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" (``srcX0``.Value) (``srcY0``.Value) (``srcX1``.Value) (``srcY1``.Value) (``dstX0``.Value) (``dstY0``.Value) (``dstX1``.Value) (``dstY1``.Value) (``mask``.Value) (``filter``.Value)
        commands.Add((name, run))
    override this.ClearBufferiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<int>) = 
        let run() = gl.glClearBufferiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
        let name() = sprintf "ClearBufferiv(%A, %A, %A)" ``buffer`` ``drawbuffer`` ``value``
        commands.Add((name, run))
    override this.ClearBufferiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<int>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        let run() = gl.glClearBufferiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "ClearBufferiv(%A, %A, %A)" (``buffer``.Value) (``drawbuffer``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.ClearBufferuiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<uint32>) = 
        let run() = gl.glClearBufferuiv.Invoke(``buffer``, ``drawbuffer``, ``value``)
        let name() = sprintf "ClearBufferuiv(%A, %A, %A)" ``buffer`` ``drawbuffer`` ``value``
        commands.Add((name, run))
    override this.ClearBufferuiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<uint32>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        let run() = gl.glClearBufferuiv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "ClearBufferuiv(%A, %A, %A)" (``buffer``.Value) (``drawbuffer``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.ClearBufferfv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<float32>) = 
        let run() = gl.glClearBufferfv.Invoke(``buffer``, ``drawbuffer``, ``value``)
        let name() = sprintf "ClearBufferfv(%A, %A, %A)" ``buffer`` ``drawbuffer`` ``value``
        commands.Add((name, run))
    override this.ClearBufferfv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<float32>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``value`` = this.Use ``value``
        let run() = gl.glClearBufferfv.Invoke(``buffer``.Value, ``drawbuffer``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "ClearBufferfv(%A, %A, %A)" (``buffer``.Value) (``drawbuffer``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.ClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) = 
        let run() = WrappedCommands.glClearBufferfi(``buffer``, ``drawbuffer``, ``depth``, ``stencil``)
        let name() = sprintf "ClearBufferfi(%A, %A, %A, %A)" ``buffer`` ``drawbuffer`` ``depth`` ``stencil``
        commands.Add((name, run))
    override this.ClearBufferfi(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``depth`` : aptr<float32>, ``stencil`` : aptr<int>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``drawbuffer`` = this.Use ``drawbuffer``
        let ``depth`` = this.Use ``depth``
        let ``stencil`` = this.Use ``stencil``
        let run() = WrappedCommands.glClearBufferfi(``buffer``.Value, ``drawbuffer``.Value, ``depth``.Value, ``stencil``.Value)
        let name() = sprintf "ClearBufferfi(%A, %A, %A, %A)" (``buffer``.Value) (``drawbuffer``.Value) (``depth``.Value) (``stencil``.Value)
        commands.Add((name, run))
    override this.ClientWaitSync(``sync`` : nativeint, ``flags`` : SyncObjectMask, ``timeout`` : uint64, ``returnValue`` : nativeptr<GLEnum>) = 
        let run() = gl.glClientWaitSync.Invoke(``sync``, ``flags``, ``timeout``) |> NativePtr.write returnValue
        let name() = sprintf "ClientWaitSync(%A, %A, %A)" ``sync`` ``flags`` ``timeout``
        commands.Add((name, run))
    override this.ClientWaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncObjectMask>, ``timeout`` : aptr<uint64>, ``returnValue`` : aptr<GLEnum>) = 
        let ``sync`` = this.Use ``sync``
        let ``flags`` = this.Use ``flags``
        let ``timeout`` = this.Use ``timeout``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glClientWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "ClientWaitSync(%A, %A, %A)" (``sync``.Value) (``flags``.Value) (``timeout``.Value)
        commands.Add((name, run))
    override this.CompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        let run() = WrappedCommands.glCompressedTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``imageSize``, ``data``)
        let name() = sprintf "CompressedTexImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``internalformat`` ``width`` ``height`` ``depth`` ``border`` ``imageSize`` ``data``
        commands.Add((name, run))
    override this.CompressedTexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``border`` = this.Use ``border``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        let run() = WrappedCommands.glCompressedTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
        let name() = sprintf "CompressedTexImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value) (``depth``.Value) (``border``.Value) (``imageSize``.Value) (``data``.Value)
        commands.Add((name, run))
    override this.CompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        let run() = WrappedCommands.glCompressedTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``imageSize``, ``data``)
        let name() = sprintf "CompressedTexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``zoffset`` ``width`` ``height`` ``depth`` ``format`` ``imageSize`` ``data``
        commands.Add((name, run))
    override this.CompressedTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``format`` = this.Use ``format``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        let run() = WrappedCommands.glCompressedTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
        let name() = sprintf "CompressedTexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``zoffset``.Value) (``width``.Value) (``height``.Value) (``depth``.Value) (``format``.Value) (``imageSize``.Value) (``data``.Value)
        commands.Add((name, run))
    override this.CopyBufferSubData(``readTarget`` : CopyBufferSubDataTarget, ``writeTarget`` : CopyBufferSubDataTarget, ``readOffset`` : nativeint, ``writeOffset`` : nativeint, ``size`` : unativeint) = 
        let run() = gl.glCopyBufferSubData.Invoke(``readTarget``, ``writeTarget``, ``readOffset``, ``writeOffset``, ``size``)
        let name() = sprintf "CopyBufferSubData(%A, %A, %A, %A, %A)" ``readTarget`` ``writeTarget`` ``readOffset`` ``writeOffset`` ``size``
        commands.Add((name, run))
    override this.CopyBufferSubData(``readTarget`` : aptr<CopyBufferSubDataTarget>, ``writeTarget`` : aptr<CopyBufferSubDataTarget>, ``readOffset`` : aptr<nativeint>, ``writeOffset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``readTarget`` = this.Use ``readTarget``
        let ``writeTarget`` = this.Use ``writeTarget``
        let ``readOffset`` = this.Use ``readOffset``
        let ``writeOffset`` = this.Use ``writeOffset``
        let ``size`` = this.Use ``size``
        let run() = gl.glCopyBufferSubData.Invoke(``readTarget``.Value, ``writeTarget``.Value, ``readOffset``.Value, ``writeOffset``.Value, ``size``.Value)
        let name() = sprintf "CopyBufferSubData(%A, %A, %A, %A, %A)" (``readTarget``.Value) (``writeTarget``.Value) (``readOffset``.Value) (``writeOffset``.Value) (``size``.Value)
        commands.Add((name, run))
    override this.CopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        let run() = WrappedCommands.glCopyTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``x``, ``y``, ``width``, ``height``)
        let name() = sprintf "CopyTexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``zoffset`` ``x`` ``y`` ``width`` ``height``
        commands.Add((name, run))
    override this.CopyTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = WrappedCommands.glCopyTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "CopyTexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``zoffset``.Value) (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.DeleteQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteQueries.Invoke(``n``, ``ids``)
        let name() = sprintf "DeleteQueries(%A, %A)" ``n`` ``ids``
        commands.Add((name, run))
    override this.DeleteQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        let run() = gl.glDeleteQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
        let name() = sprintf "DeleteQueries(%A, %A)" (``n``.Value) (``ids``.Pointer)
        commands.Add((name, run))
    override this.DeleteSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteSamplers.Invoke(``count``, ``samplers``)
        let name() = sprintf "DeleteSamplers(%A, %A)" ``count`` ``samplers``
        commands.Add((name, run))
    override this.DeleteSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``samplers`` = this.Use ``samplers``
        let run() = gl.glDeleteSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
        let name() = sprintf "DeleteSamplers(%A, %A)" (``count``.Value) (``samplers``.Pointer)
        commands.Add((name, run))
    override this.DeleteSync(``sync`` : nativeint) = 
        let run() = gl.glDeleteSync.Invoke(``sync``)
        let name() = sprintf "DeleteSync(%A)" ``sync``
        commands.Add((name, run))
    override this.DeleteSync(``sync`` : aptr<nativeint>) = 
        let ``sync`` = this.Use ``sync``
        let run() = gl.glDeleteSync.Invoke(``sync``.Value)
        let name() = sprintf "DeleteSync(%A)" (``sync``.Value)
        commands.Add((name, run))
    override this.DeleteTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteTransformFeedbacks.Invoke(``n``, ``ids``)
        let name() = sprintf "DeleteTransformFeedbacks(%A, %A)" ``n`` ``ids``
        commands.Add((name, run))
    override this.DeleteTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        let run() = gl.glDeleteTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
        let name() = sprintf "DeleteTransformFeedbacks(%A, %A)" (``n``.Value) (``ids``.Pointer)
        commands.Add((name, run))
    override this.DeleteVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteVertexArrays.Invoke(``n``, ``arrays``)
        let name() = sprintf "DeleteVertexArrays(%A, %A)" ``n`` ``arrays``
        commands.Add((name, run))
    override this.DeleteVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``arrays`` = this.Use ``arrays``
        let run() = gl.glDeleteVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
        let name() = sprintf "DeleteVertexArrays(%A, %A)" (``n``.Value) (``arrays``.Pointer)
        commands.Add((name, run))
    override this.DrawArraysInstanced(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32, ``instancecount`` : uint32) = 
        let run() = gl.glDrawArraysInstanced.Invoke(``mode``, ``first``, ``count``, ``instancecount``)
        let name() = sprintf "DrawArraysInstanced(%A, %A, %A, %A)" ``mode`` ``first`` ``count`` ``instancecount``
        commands.Add((name, run))
    override this.DrawArraysInstanced(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>, ``instancecount`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``first`` = this.Use ``first``
        let ``count`` = this.Use ``count``
        let ``instancecount`` = this.Use ``instancecount``
        let run() = gl.glDrawArraysInstanced.Invoke(``mode``.Value, ``first``.Value, ``count``.Value, ``instancecount``.Value)
        let name() = sprintf "DrawArraysInstanced(%A, %A, %A, %A)" (``mode``.Value) (``first``.Value) (``count``.Value) (``instancecount``.Value)
        commands.Add((name, run))
    override this.DrawBuffers(``n`` : uint32, ``bufs`` : nativeptr<GLEnum>) = 
        let run() = gl.glDrawBuffers.Invoke(``n``, ``bufs``)
        let name() = sprintf "DrawBuffers(%A, %A)" ``n`` ``bufs``
        commands.Add((name, run))
    override this.DrawBuffers(``n`` : aptr<uint32>, ``bufs`` : aptr<nativeptr<GLEnum>>) = 
        let ``n`` = this.Use ``n``
        let ``bufs`` = this.Use ``bufs``
        let run() = gl.glDrawBuffers.Invoke(``n``.Value, ``bufs``.Value)
        let name() = sprintf "DrawBuffers(%A, %A)" (``n``.Value) (``bufs``.Value)
        commands.Add((name, run))
    override this.DrawElementsInstanced(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint, ``instancecount`` : uint32) = 
        let run() = gl.glDrawElementsInstanced.Invoke(``mode``, ``count``, ``type``, ``indices``, ``instancecount``)
        let name() = sprintf "DrawElementsInstanced(%A, %A, %A, %A, %A)" ``mode`` ``count`` ``type`` ``indices`` ``instancecount``
        commands.Add((name, run))
    override this.DrawElementsInstanced(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>, ``instancecount`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        let ``instancecount`` = this.Use ``instancecount``
        let run() = gl.glDrawElementsInstanced.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value, ``instancecount``.Value)
        let name() = sprintf "DrawElementsInstanced(%A, %A, %A, %A, %A)" (``mode``.Value) (``count``.Value) (``type``.Value) (``indices``.Value) (``instancecount``.Value)
        commands.Add((name, run))
    override this.DrawRangeElements(``mode`` : PrimitiveType, ``start`` : uint32, ``end`` : uint32, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        let run() = gl.glDrawRangeElements.Invoke(``mode``, ``start``, ``end``, ``count``, ``type``, ``indices``)
        let name() = sprintf "DrawRangeElements(%A, %A, %A, %A, %A, %A)" ``mode`` ``start`` ``end`` ``count`` ``type`` ``indices``
        commands.Add((name, run))
    override this.DrawRangeElements(``mode`` : aptr<PrimitiveType>, ``start`` : aptr<uint32>, ``end`` : aptr<uint32>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``start`` = this.Use ``start``
        let ``end`` = this.Use ``end``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        let run() = gl.glDrawRangeElements.Invoke(``mode``.Value, ``start``.Value, ``end``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
        let name() = sprintf "DrawRangeElements(%A, %A, %A, %A, %A, %A)" (``mode``.Value) (``start``.Value) (``end``.Value) (``count``.Value) (``type``.Value) (``indices``.Value)
        commands.Add((name, run))
    override this.EndQuery(``target`` : QueryTarget) = 
        let run() = gl.glEndQuery.Invoke(``target``)
        let name() = sprintf "EndQuery(%A)" ``target``
        commands.Add((name, run))
    override this.EndQuery(``target`` : aptr<QueryTarget>) = 
        let ``target`` = this.Use ``target``
        let run() = gl.glEndQuery.Invoke(``target``.Value)
        let name() = sprintf "EndQuery(%A)" (``target``.Value)
        commands.Add((name, run))
    override this.EndTransformFeedback() = 
        let run() = gl.glEndTransformFeedback.Invoke()
        commands.Add(((fun () -> "EndTransformFeedback"), run))
    override this.FenceSync(``condition`` : SyncCondition, ``flags`` : SyncBehaviorFlags, ``returnValue`` : nativeptr<nativeint>) = 
        let run() = gl.glFenceSync.Invoke(``condition``, ``flags``) |> NativePtr.write returnValue
        let name() = sprintf "FenceSync(%A, %A)" ``condition`` ``flags``
        commands.Add((name, run))
    override this.FenceSync(``condition`` : aptr<SyncCondition>, ``flags`` : aptr<SyncBehaviorFlags>, ``returnValue`` : aptr<nativeint>) = 
        let ``condition`` = this.Use ``condition``
        let ``flags`` = this.Use ``flags``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glFenceSync.Invoke(``condition``.Value, ``flags``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "FenceSync(%A, %A)" (``condition``.Value) (``flags``.Value)
        commands.Add((name, run))
    override this.FramebufferTextureLayer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``texture`` : uint32, ``level`` : int, ``layer`` : int) = 
        let run() = gl.glFramebufferTextureLayer.Invoke(``target``, ``attachment``, ``texture``, ``level``, ``layer``)
        let name() = sprintf "FramebufferTextureLayer(%A, %A, %A, %A, %A)" ``target`` ``attachment`` ``texture`` ``level`` ``layer``
        commands.Add((name, run))
    override this.FramebufferTextureLayer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>, ``layer`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``texture`` = this.Use ``texture``
        let ``level`` = this.Use ``level``
        let ``layer`` = this.Use ``layer``
        let run() = gl.glFramebufferTextureLayer.Invoke(``target``.Value, ``attachment``.Value, ``texture``.Value, ``level``.Value, ``layer``.Value)
        let name() = sprintf "FramebufferTextureLayer(%A, %A, %A, %A, %A)" (``target``.Value) (``attachment``.Value) (``texture``.Value) (``level``.Value) (``layer``.Value)
        commands.Add((name, run))
    override this.GenQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        let run() = gl.glGenQueries.Invoke(``n``, ``ids``)
        let name() = sprintf "GenQueries(%A, %A)" ``n`` ``ids``
        commands.Add((name, run))
    override this.GenQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        let run() = gl.glGenQueries.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
        let name() = sprintf "GenQueries(%A, %A)" (``n``.Value) (``ids``.Pointer)
        commands.Add((name, run))
    override this.GenSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        let run() = gl.glGenSamplers.Invoke(``count``, ``samplers``)
        let name() = sprintf "GenSamplers(%A, %A)" ``count`` ``samplers``
        commands.Add((name, run))
    override this.GenSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``samplers`` = this.Use ``samplers``
        let run() = gl.glGenSamplers.Invoke(``count``.Value, NativePtr.ofNativeInt ``samplers``.Pointer)
        let name() = sprintf "GenSamplers(%A, %A)" (``count``.Value) (``samplers``.Pointer)
        commands.Add((name, run))
    override this.GenTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        let run() = gl.glGenTransformFeedbacks.Invoke(``n``, ``ids``)
        let name() = sprintf "GenTransformFeedbacks(%A, %A)" ``n`` ``ids``
        commands.Add((name, run))
    override this.GenTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``ids`` = this.Use ``ids``
        let run() = gl.glGenTransformFeedbacks.Invoke(``n``.Value, NativePtr.ofNativeInt ``ids``.Pointer)
        let name() = sprintf "GenTransformFeedbacks(%A, %A)" (``n``.Value) (``ids``.Pointer)
        commands.Add((name, run))
    override this.GenVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        let run() = gl.glGenVertexArrays.Invoke(``n``, ``arrays``)
        let name() = sprintf "GenVertexArrays(%A, %A)" ``n`` ``arrays``
        commands.Add((name, run))
    override this.GenVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``arrays`` = this.Use ``arrays``
        let run() = gl.glGenVertexArrays.Invoke(``n``.Value, NativePtr.ofNativeInt ``arrays``.Pointer)
        let name() = sprintf "GenVertexArrays(%A, %A)" (``n``.Value) (``arrays``.Pointer)
        commands.Add((name, run))
    override this.GetActiveUniformBlockiv(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``pname`` : UniformBlockPName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetActiveUniformBlockiv.Invoke(``program``, ``uniformBlockIndex``, ``pname``, ``params``)
        let name() = sprintf "GetActiveUniformBlockiv(%A, %A, %A, %A)" ``program`` ``uniformBlockIndex`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetActiveUniformBlockiv(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``pname`` : aptr<UniformBlockPName>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetActiveUniformBlockiv.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetActiveUniformBlockiv(%A, %A, %A, %A)" (``program``.Value) (``uniformBlockIndex``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetActiveUniformBlockName(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``uniformBlockName`` : nativeptr<uint8>) = 
        let run() = gl.glGetActiveUniformBlockName.Invoke(``program``, ``uniformBlockIndex``, ``bufSize``, ``length``, ``uniformBlockName``)
        let name() = sprintf "GetActiveUniformBlockName(%A, %A, %A, %A, %A)" ``program`` ``uniformBlockIndex`` ``bufSize`` ``length`` ``uniformBlockName``
        commands.Add((name, run))
    override this.GetActiveUniformBlockName(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``uniformBlockName`` = this.Use ``uniformBlockName``
        let run() = gl.glGetActiveUniformBlockName.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``uniformBlockName``.Pointer)
        let name() = sprintf "GetActiveUniformBlockName(%A, %A, %A, %A, %A)" (``program``.Value) (``uniformBlockIndex``.Value) (``bufSize``.Value) (``length``.Pointer) (``uniformBlockName``.Pointer)
        commands.Add((name, run))
    override this.GetActiveUniformsiv(``program`` : uint32, ``uniformCount`` : uint32, ``uniformIndices`` : nativeptr<uint32>, ``pname`` : UniformPName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetActiveUniformsiv.Invoke(``program``, ``uniformCount``, ``uniformIndices``, ``pname``, ``params``)
        let name() = sprintf "GetActiveUniformsiv(%A, %A, %A, %A, %A)" ``program`` ``uniformCount`` ``uniformIndices`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetActiveUniformsiv(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformIndices`` : aptr<uint32>, ``pname`` : aptr<UniformPName>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``uniformCount`` = this.Use ``uniformCount``
        let ``uniformIndices`` = this.Use ``uniformIndices``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetActiveUniformsiv.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformIndices``.Pointer, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetActiveUniformsiv(%A, %A, %A, %A, %A)" (``program``.Value) (``uniformCount``.Value) (``uniformIndices``.Pointer) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetBufferParameteri64v(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int64>) = 
        let run() = gl.glGetBufferParameteri64v.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetBufferParameteri64v(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetBufferParameteri64v(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int64>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetBufferParameteri64v.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetBufferParameteri64v(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetFragDataLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        let run() = gl.glGetFragDataLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
        let name() = sprintf "GetFragDataLocation(%A, %A)" ``program`` ``name``
        commands.Add((name, run))
    override this.GetFragDataLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetFragDataLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetFragDataLocation(%A, %A)" (``program``.Value) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetIntegeri_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int>) = 
        let run() = gl.glGetIntegeri_v.Invoke(``target``, ``index``, ``data``)
        let name() = sprintf "GetIntegeri_v(%A, %A, %A)" ``target`` ``index`` ``data``
        commands.Add((name, run))
    override this.GetIntegeri_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetIntegeri_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetIntegeri_v(%A, %A, %A)" (``target``.Value) (``index``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetInteger64v(``pname`` : GetPName, ``data`` : nativeptr<int64>) = 
        let run() = gl.glGetInteger64v.Invoke(``pname``, ``data``)
        let name() = sprintf "GetInteger64v(%A, %A)" ``pname`` ``data``
        commands.Add((name, run))
    override this.GetInteger64v(``pname`` : aptr<GetPName>, ``data`` : aptr<int64>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetInteger64v.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetInteger64v(%A, %A)" (``pname``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetInteger64i_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int64>) = 
        let run() = gl.glGetInteger64i_v.Invoke(``target``, ``index``, ``data``)
        let name() = sprintf "GetInteger64i_v(%A, %A, %A)" ``target`` ``index`` ``data``
        commands.Add((name, run))
    override this.GetInteger64i_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int64>) = 
        let ``target`` = this.Use ``target``
        let ``index`` = this.Use ``index``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetInteger64i_v.Invoke(``target``.Value, ``index``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetInteger64i_v(%A, %A, %A)" (``target``.Value) (``index``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetInternalformativ(``target`` : TextureTarget, ``internalformat`` : InternalFormat, ``pname`` : InternalFormatPName, ``count`` : uint32, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetInternalformativ.Invoke(``target``, ``internalformat``, ``pname``, ``count``, ``params``)
        let name() = sprintf "GetInternalformativ(%A, %A, %A, %A, %A)" ``target`` ``internalformat`` ``pname`` ``count`` ``params``
        commands.Add((name, run))
    override this.GetInternalformativ(``target`` : aptr<TextureTarget>, ``internalformat`` : aptr<InternalFormat>, ``pname`` : aptr<InternalFormatPName>, ``count`` : aptr<uint32>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``internalformat`` = this.Use ``internalformat``
        let ``pname`` = this.Use ``pname``
        let ``count`` = this.Use ``count``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetInternalformativ.Invoke(``target``.Value, ``internalformat``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetInternalformativ(%A, %A, %A, %A, %A)" (``target``.Value) (``internalformat``.Value) (``pname``.Value) (``count``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetProgramBinary(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``binaryFormat`` : nativeptr<GLEnum>, ``binary`` : nativeint) = 
        let run() = gl.glGetProgramBinary.Invoke(``program``, ``bufSize``, ``length``, ``binaryFormat``, ``binary``)
        let name() = sprintf "GetProgramBinary(%A, %A, %A, %A, %A)" ``program`` ``bufSize`` ``length`` ``binaryFormat`` ``binary``
        commands.Add((name, run))
    override this.GetProgramBinary(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T5>) = 
        let ``program`` = this.Use ``program``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        let run() = gl.glGetProgramBinary.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``binaryFormat``.Pointer, ``binary``.Pointer)
        let name() = sprintf "GetProgramBinary(%A, %A, %A, %A, %A)" (``program``.Value) (``bufSize``.Value) (``length``.Pointer) (``binaryFormat``.Pointer) (``binary``.Pointer)
        commands.Add((name, run))
    override this.GetQueryiv(``target`` : QueryTarget, ``pname`` : QueryParameterName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetQueryiv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetQueryiv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetQueryiv(``target`` : aptr<QueryTarget>, ``pname`` : aptr<QueryParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetQueryiv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetQueryiv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetQueryObjectuiv(``id`` : uint32, ``pname`` : QueryObjectParameterName, ``params`` : nativeptr<uint32>) = 
        let run() = gl.glGetQueryObjectuiv.Invoke(``id``, ``pname``, ``params``)
        let name() = sprintf "GetQueryObjectuiv(%A, %A, %A)" ``id`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetQueryObjectuiv(``id`` : aptr<uint32>, ``pname`` : aptr<QueryObjectParameterName>, ``params`` : aptr<uint32>) = 
        let ``id`` = this.Use ``id``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetQueryObjectuiv.Invoke(``id``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetQueryObjectuiv(%A, %A, %A)" (``id``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetSamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetSamplerParameteriv.Invoke(``sampler``, ``pname``, ``params``)
        let name() = sprintf "GetSamplerParameteriv(%A, %A, %A)" ``sampler`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetSamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``params`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetSamplerParameteriv(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetSamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``params`` : nativeptr<float32>) = 
        let run() = gl.glGetSamplerParameterfv.Invoke(``sampler``, ``pname``, ``params``)
        let name() = sprintf "GetSamplerParameterfv(%A, %A, %A)" ``sampler`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetSamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``params`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetSamplerParameterfv(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetStringi(``name`` : StringName, ``index`` : uint32, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        let run() = gl.glGetStringi.Invoke(``name``, ``index``) |> NativePtr.write returnValue
        let name() = sprintf "GetStringi(%A, %A)" ``name`` ``index``
        commands.Add((name, run))
    override this.GetStringi(``name`` : aptr<StringName>, ``index`` : aptr<uint32>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        let ``name`` = this.Use ``name``
        let ``index`` = this.Use ``index``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetStringi.Invoke(``name``.Value, ``index``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetStringi(%A, %A)" (``name``.Value) (``index``.Value)
        commands.Add((name, run))
    override this.GetSynciv(``sync`` : nativeint, ``pname`` : SyncParameterName, ``count`` : uint32, ``length`` : nativeptr<uint32>, ``values`` : nativeptr<int>) = 
        let run() = gl.glGetSynciv.Invoke(``sync``, ``pname``, ``count``, ``length``, ``values``)
        let name() = sprintf "GetSynciv(%A, %A, %A, %A, %A)" ``sync`` ``pname`` ``count`` ``length`` ``values``
        commands.Add((name, run))
    override this.GetSynciv(``sync`` : aptr<nativeint>, ``pname`` : aptr<SyncParameterName>, ``count`` : aptr<uint32>, ``length`` : aptr<uint32>, ``values`` : aptr<int>) = 
        let ``sync`` = this.Use ``sync``
        let ``pname`` = this.Use ``pname``
        let ``count`` = this.Use ``count``
        let ``length`` = this.Use ``length``
        let ``values`` = this.Use ``values``
        let run() = gl.glGetSynciv.Invoke(``sync``.Value, ``pname``.Value, ``count``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``values``.Pointer)
        let name() = sprintf "GetSynciv(%A, %A, %A, %A, %A)" (``sync``.Value) (``pname``.Value) (``count``.Value) (``length``.Pointer) (``values``.Pointer)
        commands.Add((name, run))
    override this.GetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        let run() = WrappedCommands.glGetTransformFeedbackVarying(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
        let name() = sprintf "GetTransformFeedbackVarying(%A, %A, %A, %A, %A, %A, %A)" ``program`` ``index`` ``bufSize`` ``length`` ``size`` ``type`` ``name``
        commands.Add((name, run))
    override this.GetTransformFeedbackVarying(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<uint32>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        let run() = WrappedCommands.glGetTransformFeedbackVarying(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
        let name() = sprintf "GetTransformFeedbackVarying(%A, %A, %A, %A, %A, %A, %A)" (``program``.Value) (``index``.Value) (``bufSize``.Value) (``length``.Pointer) (``size``.Pointer) (``type``.Pointer) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetUniformuiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<uint32>) = 
        let run() = gl.glGetUniformuiv.Invoke(``program``, ``location``, ``params``)
        let name() = sprintf "GetUniformuiv(%A, %A, %A)" ``program`` ``location`` ``params``
        commands.Add((name, run))
    override this.GetUniformuiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetUniformuiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetUniformuiv(%A, %A, %A)" (``program``.Value) (``location``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetUniformBlockIndex(``program`` : uint32, ``uniformBlockName`` : nativeptr<uint8>, ``returnValue`` : nativeptr<uint32>) = 
        let run() = gl.glGetUniformBlockIndex.Invoke(``program``, ``uniformBlockName``) |> NativePtr.write returnValue
        let name() = sprintf "GetUniformBlockIndex(%A, %A)" ``program`` ``uniformBlockName``
        commands.Add((name, run))
    override this.GetUniformBlockIndex(``program`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>, ``returnValue`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockName`` = this.Use ``uniformBlockName``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetUniformBlockIndex.Invoke(``program``.Value, NativePtr.ofNativeInt ``uniformBlockName``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetUniformBlockIndex(%A, %A)" (``program``.Value) (``uniformBlockName``.Pointer)
        commands.Add((name, run))
    override this.GetUniformIndices(``program`` : uint32, ``uniformCount`` : uint32, ``uniformNames`` : nativeptr<nativeptr<uint8>>, ``uniformIndices`` : nativeptr<uint32>) = 
        let run() = gl.glGetUniformIndices.Invoke(``program``, ``uniformCount``, ``uniformNames``, ``uniformIndices``)
        let name() = sprintf "GetUniformIndices(%A, %A, %A, %A)" ``program`` ``uniformCount`` ``uniformNames`` ``uniformIndices``
        commands.Add((name, run))
    override this.GetUniformIndices(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformNames`` : aptr<nativeptr<uint8>>, ``uniformIndices`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformCount`` = this.Use ``uniformCount``
        let ``uniformNames`` = this.Use ``uniformNames``
        let ``uniformIndices`` = this.Use ``uniformIndices``
        let run() = gl.glGetUniformIndices.Invoke(``program``.Value, ``uniformCount``.Value, NativePtr.ofNativeInt ``uniformNames``.Pointer, NativePtr.ofNativeInt ``uniformIndices``.Pointer)
        let name() = sprintf "GetUniformIndices(%A, %A, %A, %A)" (``program``.Value) (``uniformCount``.Value) (``uniformNames``.Pointer) (``uniformIndices``.Pointer)
        commands.Add((name, run))
    override this.GetVertexAttribIiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetVertexAttribIiv.Invoke(``index``, ``pname``, ``params``)
        let name() = sprintf "GetVertexAttribIiv(%A, %A, %A)" ``index`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetVertexAttribIiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetVertexAttribIiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetVertexAttribIiv(%A, %A, %A)" (``index``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetVertexAttribIuiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<uint32>) = 
        let run() = gl.glGetVertexAttribIuiv.Invoke(``index``, ``pname``, ``params``)
        let name() = sprintf "GetVertexAttribIuiv(%A, %A, %A)" ``index`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetVertexAttribIuiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetVertexAttribIuiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetVertexAttribIuiv(%A, %A, %A)" (``index``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.InvalidateFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>) = 
        let run() = gl.glInvalidateFramebuffer.Invoke(``target``, ``numAttachments``, ``attachments``)
        let name() = sprintf "InvalidateFramebuffer(%A, %A, %A)" ``target`` ``numAttachments`` ``attachments``
        commands.Add((name, run))
    override this.InvalidateFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>) = 
        let ``target`` = this.Use ``target``
        let ``numAttachments`` = this.Use ``numAttachments``
        let ``attachments`` = this.Use ``attachments``
        let run() = gl.glInvalidateFramebuffer.Invoke(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer)
        let name() = sprintf "InvalidateFramebuffer(%A, %A, %A)" (``target``.Value) (``numAttachments``.Value) (``attachments``.Pointer)
        commands.Add((name, run))
    override this.InvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        let run() = WrappedCommands.glInvalidateSubFramebuffer(``target``, ``numAttachments``, ``attachments``, ``x``, ``y``, ``width``, ``height``)
        let name() = sprintf "InvalidateSubFramebuffer(%A, %A, %A, %A, %A, %A, %A)" ``target`` ``numAttachments`` ``attachments`` ``x`` ``y`` ``width`` ``height``
        commands.Add((name, run))
    override this.InvalidateSubFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``numAttachments`` = this.Use ``numAttachments``
        let ``attachments`` = this.Use ``attachments``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = WrappedCommands.glInvalidateSubFramebuffer(``target``.Value, ``numAttachments``.Value, NativePtr.ofNativeInt ``attachments``.Pointer, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "InvalidateSubFramebuffer(%A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``numAttachments``.Value) (``attachments``.Pointer) (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.IsQuery(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsQuery.Invoke(``id``) |> NativePtr.write returnValue
        let name() = sprintf "IsQuery(%A)" ``id``
        commands.Add((name, run))
    override this.IsQuery(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``id`` = this.Use ``id``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsQuery.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsQuery(%A)" (``id``.Value)
        commands.Add((name, run))
    override this.IsSampler(``sampler`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsSampler.Invoke(``sampler``) |> NativePtr.write returnValue
        let name() = sprintf "IsSampler(%A)" ``sampler``
        commands.Add((name, run))
    override this.IsSampler(``sampler`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsSampler.Invoke(``sampler``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsSampler(%A)" (``sampler``.Value)
        commands.Add((name, run))
    override this.IsSync(``sync`` : nativeint, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsSync.Invoke(``sync``) |> NativePtr.write returnValue
        let name() = sprintf "IsSync(%A)" ``sync``
        commands.Add((name, run))
    override this.IsSync(``sync`` : aptr<nativeint>, ``returnValue`` : aptr<Boolean>) = 
        let ``sync`` = this.Use ``sync``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsSync.Invoke(``sync``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsSync(%A)" (``sync``.Value)
        commands.Add((name, run))
    override this.IsTransformFeedback(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsTransformFeedback.Invoke(``id``) |> NativePtr.write returnValue
        let name() = sprintf "IsTransformFeedback(%A)" ``id``
        commands.Add((name, run))
    override this.IsTransformFeedback(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``id`` = this.Use ``id``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsTransformFeedback.Invoke(``id``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsTransformFeedback(%A)" (``id``.Value)
        commands.Add((name, run))
    override this.IsVertexArray(``array`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsVertexArray.Invoke(``array``) |> NativePtr.write returnValue
        let name() = sprintf "IsVertexArray(%A)" ``array``
        commands.Add((name, run))
    override this.IsVertexArray(``array`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``array`` = this.Use ``array``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsVertexArray.Invoke(``array``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsVertexArray(%A)" (``array``.Value)
        commands.Add((name, run))
    override this.PauseTransformFeedback() = 
        let run() = gl.glPauseTransformFeedback.Invoke()
        commands.Add(((fun () -> "PauseTransformFeedback"), run))
    override this.ProgramBinary(``program`` : uint32, ``binaryFormat`` : GLEnum, ``binary`` : nativeint, ``length`` : uint32) = 
        let run() = gl.glProgramBinary.Invoke(``program``, ``binaryFormat``, ``binary``, ``length``)
        let name() = sprintf "ProgramBinary(%A, %A, %A, %A)" ``program`` ``binaryFormat`` ``binary`` ``length``
        commands.Add((name, run))
    override this.ProgramBinary(``program`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<'T3>, ``length`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        let ``length`` = this.Use ``length``
        let run() = gl.glProgramBinary.Invoke(``program``.Value, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
        let name() = sprintf "ProgramBinary(%A, %A, %A, %A)" (``program``.Value) (``binaryFormat``.Value) (``binary``.Pointer) (``length``.Value)
        commands.Add((name, run))
    override this.ProgramParameteri(``program`` : uint32, ``pname`` : ProgramParameterPName, ``value`` : int) = 
        let run() = gl.glProgramParameteri.Invoke(``program``, ``pname``, ``value``)
        let name() = sprintf "ProgramParameteri(%A, %A, %A)" ``program`` ``pname`` ``value``
        commands.Add((name, run))
    override this.ProgramParameteri(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramParameterPName>, ``value`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``pname`` = this.Use ``pname``
        let ``value`` = this.Use ``value``
        let run() = gl.glProgramParameteri.Invoke(``program``.Value, ``pname``.Value, ``value``.Value)
        let name() = sprintf "ProgramParameteri(%A, %A, %A)" (``program``.Value) (``pname``.Value) (``value``.Value)
        commands.Add((name, run))
    override this.ReadBuffer(``src`` : ReadBufferMode) = 
        let run() = gl.glReadBuffer.Invoke(``src``)
        let name() = sprintf "ReadBuffer(%A)" ``src``
        commands.Add((name, run))
    override this.ReadBuffer(``src`` : aptr<ReadBufferMode>) = 
        let ``src`` = this.Use ``src``
        let run() = gl.glReadBuffer.Invoke(``src``.Value)
        let name() = sprintf "ReadBuffer(%A)" (``src``.Value)
        commands.Add((name, run))
    override this.RenderbufferStorageMultisample(``target`` : RenderbufferTarget, ``samples`` : uint32, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        let run() = gl.glRenderbufferStorageMultisample.Invoke(``target``, ``samples``, ``internalformat``, ``width``, ``height``)
        let name() = sprintf "RenderbufferStorageMultisample(%A, %A, %A, %A, %A)" ``target`` ``samples`` ``internalformat`` ``width`` ``height``
        commands.Add((name, run))
    override this.RenderbufferStorageMultisample(``target`` : aptr<RenderbufferTarget>, ``samples`` : aptr<uint32>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``samples`` = this.Use ``samples``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = gl.glRenderbufferStorageMultisample.Invoke(``target``.Value, ``samples``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "RenderbufferStorageMultisample(%A, %A, %A, %A, %A)" (``target``.Value) (``samples``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.ResumeTransformFeedback() = 
        let run() = gl.glResumeTransformFeedback.Invoke()
        commands.Add(((fun () -> "ResumeTransformFeedback"), run))
    override this.SamplerParameteri(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : int) = 
        let run() = gl.glSamplerParameteri.Invoke(``sampler``, ``pname``, ``param``)
        let name() = sprintf "SamplerParameteri(%A, %A, %A)" ``sampler`` ``pname`` ``param``
        commands.Add((name, run))
    override this.SamplerParameteri(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = gl.glSamplerParameteri.Invoke(``sampler``.Value, ``pname``.Value, ``param``.Value)
        let name() = sprintf "SamplerParameteri(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``param``.Value)
        commands.Add((name, run))
    override this.SamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : nativeptr<int>) = 
        let run() = gl.glSamplerParameteriv.Invoke(``sampler``, ``pname``, ``param``)
        let name() = sprintf "SamplerParameteriv(%A, %A, %A)" ``sampler`` ``pname`` ``param``
        commands.Add((name, run))
    override this.SamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = gl.glSamplerParameteriv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
        let name() = sprintf "SamplerParameteriv(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``param``.Pointer)
        commands.Add((name, run))
    override this.SamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) = 
        let run() = WrappedCommands.glSamplerParameterf(``sampler``, ``pname``, ``param``)
        let name() = sprintf "SamplerParameterf(%A, %A, %A)" ``sampler`` ``pname`` ``param``
        commands.Add((name, run))
    override this.SamplerParameterf(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = WrappedCommands.glSamplerParameterf(``sampler``.Value, ``pname``.Value, ``param``.Value)
        let name() = sprintf "SamplerParameterf(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``param``.Value)
        commands.Add((name, run))
    override this.SamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : nativeptr<float32>) = 
        let run() = gl.glSamplerParameterfv.Invoke(``sampler``, ``pname``, ``param``)
        let name() = sprintf "SamplerParameterfv(%A, %A, %A)" ``sampler`` ``pname`` ``param``
        commands.Add((name, run))
    override this.SamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler`` = this.Use ``sampler``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = gl.glSamplerParameterfv.Invoke(``sampler``.Value, ``pname``.Value, NativePtr.ofNativeInt ``param``.Pointer)
        let name() = sprintf "SamplerParameterfv(%A, %A, %A)" (``sampler``.Value) (``pname``.Value) (``param``.Pointer)
        commands.Add((name, run))
    override this.TexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        let run() = WrappedCommands.glTexImage3D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``depth``, ``border``, ``format``, ``type``, ``pixels``)
        let name() = sprintf "TexImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``internalformat`` ``width`` ``height`` ``depth`` ``border`` ``format`` ``type`` ``pixels``
        commands.Add((name, run))
    override this.TexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``border`` = this.Use ``border``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        let run() = WrappedCommands.glTexImage3D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
        let name() = sprintf "TexImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value) (``depth``.Value) (``border``.Value) (``format``.Value) (``type``.Value) (``pixels``.Value)
        commands.Add((name, run))
    override this.TexStorage2D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        let run() = gl.glTexStorage2D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``)
        let name() = sprintf "TexStorage2D(%A, %A, %A, %A, %A)" ``target`` ``levels`` ``internalformat`` ``width`` ``height``
        commands.Add((name, run))
    override this.TexStorage2D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``levels`` = this.Use ``levels``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = gl.glTexStorage2D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "TexStorage2D(%A, %A, %A, %A, %A)" (``target``.Value) (``levels``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.TexStorage3D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32) = 
        let run() = gl.glTexStorage3D.Invoke(``target``, ``levels``, ``internalformat``, ``width``, ``height``, ``depth``)
        let name() = sprintf "TexStorage3D(%A, %A, %A, %A, %A, %A)" ``target`` ``levels`` ``internalformat`` ``width`` ``height`` ``depth``
        commands.Add((name, run))
    override this.TexStorage3D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``levels`` = this.Use ``levels``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let run() = gl.glTexStorage3D.Invoke(``target``.Value, ``levels``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``depth``.Value)
        let name() = sprintf "TexStorage3D(%A, %A, %A, %A, %A, %A)" (``target``.Value) (``levels``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value) (``depth``.Value)
        commands.Add((name, run))
    override this.TexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        let run() = WrappedCommands.glTexSubImage3D(``target``, ``level``, ``xoffset``, ``yoffset``, ``zoffset``, ``width``, ``height``, ``depth``, ``format``, ``type``, ``pixels``)
        let name() = sprintf "TexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``zoffset`` ``width`` ``height`` ``depth`` ``format`` ``type`` ``pixels``
        commands.Add((name, run))
    override this.TexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``zoffset`` = this.Use ``zoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``depth`` = this.Use ``depth``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        let run() = WrappedCommands.glTexSubImage3D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``zoffset``.Value, ``width``.Value, ``height``.Value, ``depth``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
        let name() = sprintf "TexSubImage3D(%A, %A, %A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``zoffset``.Value) (``width``.Value) (``height``.Value) (``depth``.Value) (``format``.Value) (``type``.Value) (``pixels``.Value)
        commands.Add((name, run))
    override this.TransformFeedbackVaryings(``program`` : uint32, ``count`` : uint32, ``varyings`` : nativeptr<nativeptr<uint8>>, ``bufferMode`` : TransformFeedbackBufferMode) = 
        let run() = gl.glTransformFeedbackVaryings.Invoke(``program``, ``count``, ``varyings``, ``bufferMode``)
        let name() = sprintf "TransformFeedbackVaryings(%A, %A, %A, %A)" ``program`` ``count`` ``varyings`` ``bufferMode``
        commands.Add((name, run))
    override this.TransformFeedbackVaryings(``program`` : aptr<uint32>, ``count`` : aptr<uint32>, ``varyings`` : aptr<nativeptr<uint8>>, ``bufferMode`` : aptr<TransformFeedbackBufferMode>) = 
        let ``program`` = this.Use ``program``
        let ``count`` = this.Use ``count``
        let ``varyings`` = this.Use ``varyings``
        let ``bufferMode`` = this.Use ``bufferMode``
        let run() = gl.glTransformFeedbackVaryings.Invoke(``program``.Value, ``count``.Value, NativePtr.ofNativeInt ``varyings``.Pointer, ``bufferMode``.Value)
        let name() = sprintf "TransformFeedbackVaryings(%A, %A, %A, %A)" (``program``.Value) (``count``.Value) (``varyings``.Pointer) (``bufferMode``.Value)
        commands.Add((name, run))
    override this.Uniform1ui(``location`` : int, ``v0`` : uint32) = 
        let run() = gl.glUniform1ui.Invoke(``location``, ``v0``)
        let name() = sprintf "Uniform1ui(%A, %A)" ``location`` ``v0``
        commands.Add((name, run))
    override this.Uniform1ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let run() = gl.glUniform1ui.Invoke(``location``.Value, ``v0``.Value)
        let name() = sprintf "Uniform1ui(%A, %A)" (``location``.Value) (``v0``.Value)
        commands.Add((name, run))
    override this.Uniform1uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        let run() = gl.glUniform1uiv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform1uiv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform1uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform1uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform1uiv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform2ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32) = 
        let run() = gl.glUniform2ui.Invoke(``location``, ``v0``, ``v1``)
        let name() = sprintf "Uniform2ui(%A, %A, %A)" ``location`` ``v0`` ``v1``
        commands.Add((name, run))
    override this.Uniform2ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let run() = gl.glUniform2ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
        let name() = sprintf "Uniform2ui(%A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value)
        commands.Add((name, run))
    override this.Uniform2uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        let run() = gl.glUniform2uiv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform2uiv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform2uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform2uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform2uiv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform3ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32) = 
        let run() = gl.glUniform3ui.Invoke(``location``, ``v0``, ``v1``, ``v2``)
        let name() = sprintf "Uniform3ui(%A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2``
        commands.Add((name, run))
    override this.Uniform3ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let run() = gl.glUniform3ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
        let name() = sprintf "Uniform3ui(%A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value)
        commands.Add((name, run))
    override this.Uniform3uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        let run() = gl.glUniform3uiv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform3uiv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform3uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform3uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform3uiv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform4ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32, ``v3`` : uint32) = 
        let run() = gl.glUniform4ui.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
        let name() = sprintf "Uniform4ui(%A, %A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2`` ``v3``
        commands.Add((name, run))
    override this.Uniform4ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>, ``v3`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        let run() = gl.glUniform4ui.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
        let name() = sprintf "Uniform4ui(%A, %A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value) (``v3``.Value)
        commands.Add((name, run))
    override this.Uniform4uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        let run() = gl.glUniform4uiv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform4uiv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform4uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform4uiv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform4uiv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformBlockBinding(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``uniformBlockBinding`` : uint32) = 
        let run() = gl.glUniformBlockBinding.Invoke(``program``, ``uniformBlockIndex``, ``uniformBlockBinding``)
        let name() = sprintf "UniformBlockBinding(%A, %A, %A)" ``program`` ``uniformBlockIndex`` ``uniformBlockBinding``
        commands.Add((name, run))
    override this.UniformBlockBinding(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``uniformBlockBinding`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``uniformBlockIndex`` = this.Use ``uniformBlockIndex``
        let ``uniformBlockBinding`` = this.Use ``uniformBlockBinding``
        let run() = gl.glUniformBlockBinding.Invoke(``program``.Value, ``uniformBlockIndex``.Value, ``uniformBlockBinding``.Value)
        let name() = sprintf "UniformBlockBinding(%A, %A, %A)" (``program``.Value) (``uniformBlockIndex``.Value) (``uniformBlockBinding``.Value)
        commands.Add((name, run))
    override this.UniformMatrix2x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix2x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix2x3fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix2x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix2x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix2x3fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix2x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix2x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix2x4fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix2x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix2x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix2x4fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix3x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix3x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix3x2fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix3x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix3x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix3x2fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix3x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix3x4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix3x4fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix3x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix3x4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix3x4fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix4x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix4x2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix4x2fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix4x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix4x2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix4x2fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix4x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix4x3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix4x3fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix4x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix4x3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix4x3fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.VertexAttribDivisor(``index`` : uint32, ``divisor`` : uint32) = 
        let run() = gl.glVertexAttribDivisor.Invoke(``index``, ``divisor``)
        let name() = sprintf "VertexAttribDivisor(%A, %A)" ``index`` ``divisor``
        commands.Add((name, run))
    override this.VertexAttribDivisor(``index`` : aptr<uint32>, ``divisor`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``divisor`` = this.Use ``divisor``
        let run() = gl.glVertexAttribDivisor.Invoke(``index``.Value, ``divisor``.Value)
        let name() = sprintf "VertexAttribDivisor(%A, %A)" (``index``.Value) (``divisor``.Value)
        commands.Add((name, run))
    override this.VertexAttribI4i(``index`` : uint32, ``x`` : int, ``y`` : int, ``z`` : int, ``w`` : int) = 
        let run() = gl.glVertexAttribI4i.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
        let name() = sprintf "VertexAttribI4i(%A, %A, %A, %A, %A)" ``index`` ``x`` ``y`` ``z`` ``w``
        commands.Add((name, run))
    override this.VertexAttribI4i(``index`` : aptr<uint32>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``z`` : aptr<int>, ``w`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        let run() = gl.glVertexAttribI4i.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
        let name() = sprintf "VertexAttribI4i(%A, %A, %A, %A, %A)" (``index``.Value) (``x``.Value) (``y``.Value) (``z``.Value) (``w``.Value)
        commands.Add((name, run))
    override this.VertexAttribI4ui(``index`` : uint32, ``x`` : uint32, ``y`` : uint32, ``z`` : uint32, ``w`` : uint32) = 
        let run() = gl.glVertexAttribI4ui.Invoke(``index``, ``x``, ``y``, ``z``, ``w``)
        let name() = sprintf "VertexAttribI4ui(%A, %A, %A, %A, %A)" ``index`` ``x`` ``y`` ``z`` ``w``
        commands.Add((name, run))
    override this.VertexAttribI4ui(``index`` : aptr<uint32>, ``x`` : aptr<uint32>, ``y`` : aptr<uint32>, ``z`` : aptr<uint32>, ``w`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        let run() = gl.glVertexAttribI4ui.Invoke(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
        let name() = sprintf "VertexAttribI4ui(%A, %A, %A, %A, %A)" (``index``.Value) (``x``.Value) (``y``.Value) (``z``.Value) (``w``.Value)
        commands.Add((name, run))
    override this.VertexAttribI4iv(``index`` : uint32, ``v`` : nativeptr<int>) = 
        let run() = gl.glVertexAttribI4iv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttribI4iv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttribI4iv(``index`` : aptr<uint32>, ``v`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttribI4iv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttribI4iv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttribI4uiv(``index`` : uint32, ``v`` : nativeptr<uint32>) = 
        let run() = gl.glVertexAttribI4uiv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttribI4uiv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttribI4uiv(``index`` : aptr<uint32>, ``v`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttribI4uiv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttribI4uiv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttribIPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribIType, ``stride`` : uint32, ``pointer`` : nativeint) = 
        let run() = gl.glVertexAttribIPointer.Invoke(``index``, ``size``, ``type``, ``stride``, ``pointer``)
        let name() = sprintf "VertexAttribIPointer(%A, %A, %A, %A, %A)" ``index`` ``size`` ``type`` ``stride`` ``pointer``
        commands.Add((name, run))
    override this.VertexAttribIPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribIType>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T5>) = 
        let ``index`` = this.Use ``index``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``stride`` = this.Use ``stride``
        let ``pointer`` = this.Use ``pointer``
        let run() = gl.glVertexAttribIPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``stride``.Value, ``pointer``.Pointer)
        let name() = sprintf "VertexAttribIPointer(%A, %A, %A, %A, %A)" (``index``.Value) (``size``.Value) (``type``.Value) (``stride``.Value) (``pointer``.Pointer)
        commands.Add((name, run))
    override this.WaitSync(``sync`` : nativeint, ``flags`` : SyncBehaviorFlags, ``timeout`` : uint64) = 
        let run() = gl.glWaitSync.Invoke(``sync``, ``flags``, ``timeout``)
        let name() = sprintf "WaitSync(%A, %A, %A)" ``sync`` ``flags`` ``timeout``
        commands.Add((name, run))
    override this.WaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncBehaviorFlags>, ``timeout`` : aptr<uint64>) = 
        let ``sync`` = this.Use ``sync``
        let ``flags`` = this.Use ``flags``
        let ``timeout`` = this.Use ``timeout``
        let run() = gl.glWaitSync.Invoke(``sync``.Value, ``flags``.Value, ``timeout``.Value)
        let name() = sprintf "WaitSync(%A, %A, %A)" (``sync``.Value) (``flags``.Value) (``timeout``.Value)
        commands.Add((name, run))
    override this.ActiveTexture(``texture`` : TextureUnit) = 
        let run() = gl.glActiveTexture.Invoke(``texture``)
        let name() = sprintf "ActiveTexture(%A)" ``texture``
        commands.Add((name, run))
    override this.ActiveTexture(``texture`` : aptr<TextureUnit>) = 
        let ``texture`` = this.Use ``texture``
        let run() = gl.glActiveTexture.Invoke(``texture``.Value)
        let name() = sprintf "ActiveTexture(%A)" (``texture``.Value)
        commands.Add((name, run))
    override this.AttachShader(``program`` : uint32, ``shader`` : uint32) = 
        let run() = gl.glAttachShader.Invoke(``program``, ``shader``)
        let name() = sprintf "AttachShader(%A, %A)" ``program`` ``shader``
        commands.Add((name, run))
    override this.AttachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``shader`` = this.Use ``shader``
        let run() = gl.glAttachShader.Invoke(``program``.Value, ``shader``.Value)
        let name() = sprintf "AttachShader(%A, %A)" (``program``.Value) (``shader``.Value)
        commands.Add((name, run))
    override this.BindAttribLocation(``program`` : uint32, ``index`` : uint32, ``name`` : nativeptr<uint8>) = 
        let run() = gl.glBindAttribLocation.Invoke(``program``, ``index``, ``name``)
        let name() = sprintf "BindAttribLocation(%A, %A, %A)" ``program`` ``index`` ``name``
        commands.Add((name, run))
    override this.BindAttribLocation(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``name`` = this.Use ``name``
        let run() = gl.glBindAttribLocation.Invoke(``program``.Value, ``index``.Value, NativePtr.ofNativeInt ``name``.Pointer)
        let name() = sprintf "BindAttribLocation(%A, %A, %A)" (``program``.Value) (``index``.Value) (``name``.Pointer)
        commands.Add((name, run))
    override this.BindBuffer(``target`` : BufferTargetARB, ``buffer`` : uint32) = 
        let run() = gl.glBindBuffer.Invoke(``target``, ``buffer``)
        let name() = sprintf "BindBuffer(%A, %A)" ``target`` ``buffer``
        commands.Add((name, run))
    override this.BindBuffer(``target`` : aptr<BufferTargetARB>, ``buffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``buffer`` = this.Use ``buffer``
        let run() = gl.glBindBuffer.Invoke(``target``.Value, ``buffer``.Value)
        let name() = sprintf "BindBuffer(%A, %A)" (``target``.Value) (``buffer``.Value)
        commands.Add((name, run))
    override this.BindFramebuffer(``target`` : FramebufferTarget, ``framebuffer`` : uint32) = 
        let run() = gl.glBindFramebuffer.Invoke(``target``, ``framebuffer``)
        let name() = sprintf "BindFramebuffer(%A, %A)" ``target`` ``framebuffer``
        commands.Add((name, run))
    override this.BindFramebuffer(``target`` : aptr<FramebufferTarget>, ``framebuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``framebuffer`` = this.Use ``framebuffer``
        let run() = gl.glBindFramebuffer.Invoke(``target``.Value, ``framebuffer``.Value)
        let name() = sprintf "BindFramebuffer(%A, %A)" (``target``.Value) (``framebuffer``.Value)
        commands.Add((name, run))
    override this.BindRenderbuffer(``target`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        let run() = gl.glBindRenderbuffer.Invoke(``target``, ``renderbuffer``)
        let name() = sprintf "BindRenderbuffer(%A, %A)" ``target`` ``renderbuffer``
        commands.Add((name, run))
    override this.BindRenderbuffer(``target`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``renderbuffer`` = this.Use ``renderbuffer``
        let run() = gl.glBindRenderbuffer.Invoke(``target``.Value, ``renderbuffer``.Value)
        let name() = sprintf "BindRenderbuffer(%A, %A)" (``target``.Value) (``renderbuffer``.Value)
        commands.Add((name, run))
    override this.BindTexture(``target`` : TextureTarget, ``texture`` : uint32) = 
        let run() = gl.glBindTexture.Invoke(``target``, ``texture``)
        let name() = sprintf "BindTexture(%A, %A)" ``target`` ``texture``
        commands.Add((name, run))
    override this.BindTexture(``target`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``texture`` = this.Use ``texture``
        let run() = gl.glBindTexture.Invoke(``target``.Value, ``texture``.Value)
        let name() = sprintf "BindTexture(%A, %A)" (``target``.Value) (``texture``.Value)
        commands.Add((name, run))
    override this.BlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        let run() = WrappedCommands.glBlendColor(``red``, ``green``, ``blue``, ``alpha``)
        let name() = sprintf "BlendColor(%A, %A, %A, %A)" ``red`` ``green`` ``blue`` ``alpha``
        commands.Add((name, run))
    override this.BlendColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        let run() = WrappedCommands.glBlendColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
        let name() = sprintf "BlendColor(%A, %A, %A, %A)" (``red``.Value) (``green``.Value) (``blue``.Value) (``alpha``.Value)
        commands.Add((name, run))
    override this.BlendEquation(``mode`` : BlendEquationModeEXT) = 
        let run() = gl.glBlendEquation.Invoke(``mode``)
        let name() = sprintf "BlendEquation(%A)" ``mode``
        commands.Add((name, run))
    override this.BlendEquation(``mode`` : aptr<BlendEquationModeEXT>) = 
        let ``mode`` = this.Use ``mode``
        let run() = gl.glBlendEquation.Invoke(``mode``.Value)
        let name() = sprintf "BlendEquation(%A)" (``mode``.Value)
        commands.Add((name, run))
    override this.BlendEquationSeparate(``modeRGB`` : BlendEquationModeEXT, ``modeAlpha`` : BlendEquationModeEXT) = 
        let run() = gl.glBlendEquationSeparate.Invoke(``modeRGB``, ``modeAlpha``)
        let name() = sprintf "BlendEquationSeparate(%A, %A)" ``modeRGB`` ``modeAlpha``
        commands.Add((name, run))
    override this.BlendEquationSeparate(``modeRGB`` : aptr<BlendEquationModeEXT>, ``modeAlpha`` : aptr<BlendEquationModeEXT>) = 
        let ``modeRGB`` = this.Use ``modeRGB``
        let ``modeAlpha`` = this.Use ``modeAlpha``
        let run() = gl.glBlendEquationSeparate.Invoke(``modeRGB``.Value, ``modeAlpha``.Value)
        let name() = sprintf "BlendEquationSeparate(%A, %A)" (``modeRGB``.Value) (``modeAlpha``.Value)
        commands.Add((name, run))
    override this.BlendFunc(``sfactor`` : BlendingFactor, ``dfactor`` : BlendingFactor) = 
        let run() = gl.glBlendFunc.Invoke(``sfactor``, ``dfactor``)
        let name() = sprintf "BlendFunc(%A, %A)" ``sfactor`` ``dfactor``
        commands.Add((name, run))
    override this.BlendFunc(``sfactor`` : aptr<BlendingFactor>, ``dfactor`` : aptr<BlendingFactor>) = 
        let ``sfactor`` = this.Use ``sfactor``
        let ``dfactor`` = this.Use ``dfactor``
        let run() = gl.glBlendFunc.Invoke(``sfactor``.Value, ``dfactor``.Value)
        let name() = sprintf "BlendFunc(%A, %A)" (``sfactor``.Value) (``dfactor``.Value)
        commands.Add((name, run))
    override this.BlendFuncSeparate(``sfactorRGB`` : BlendingFactor, ``dfactorRGB`` : BlendingFactor, ``sfactorAlpha`` : BlendingFactor, ``dfactorAlpha`` : BlendingFactor) = 
        let run() = gl.glBlendFuncSeparate.Invoke(``sfactorRGB``, ``dfactorRGB``, ``sfactorAlpha``, ``dfactorAlpha``)
        let name() = sprintf "BlendFuncSeparate(%A, %A, %A, %A)" ``sfactorRGB`` ``dfactorRGB`` ``sfactorAlpha`` ``dfactorAlpha``
        commands.Add((name, run))
    override this.BlendFuncSeparate(``sfactorRGB`` : aptr<BlendingFactor>, ``dfactorRGB`` : aptr<BlendingFactor>, ``sfactorAlpha`` : aptr<BlendingFactor>, ``dfactorAlpha`` : aptr<BlendingFactor>) = 
        let ``sfactorRGB`` = this.Use ``sfactorRGB``
        let ``dfactorRGB`` = this.Use ``dfactorRGB``
        let ``sfactorAlpha`` = this.Use ``sfactorAlpha``
        let ``dfactorAlpha`` = this.Use ``dfactorAlpha``
        let run() = gl.glBlendFuncSeparate.Invoke(``sfactorRGB``.Value, ``dfactorRGB``.Value, ``sfactorAlpha``.Value, ``dfactorAlpha``.Value)
        let name() = sprintf "BlendFuncSeparate(%A, %A, %A, %A)" (``sfactorRGB``.Value) (``dfactorRGB``.Value) (``sfactorAlpha``.Value) (``dfactorAlpha``.Value)
        commands.Add((name, run))
    override this.BufferData(``target`` : BufferTargetARB, ``size`` : unativeint, ``data`` : nativeint, ``usage`` : BufferUsageARB) = 
        let run() = gl.glBufferData.Invoke(``target``, ``size``, ``data``, ``usage``)
        let name() = sprintf "BufferData(%A, %A, %A, %A)" ``target`` ``size`` ``data`` ``usage``
        commands.Add((name, run))
    override this.BufferData(``target`` : aptr<BufferTargetARB>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>, ``usage`` : aptr<BufferUsageARB>) = 
        let ``target`` = this.Use ``target``
        let ``size`` = this.Use ``size``
        let ``data`` = this.Use ``data``
        let ``usage`` = this.Use ``usage``
        let run() = gl.glBufferData.Invoke(``target``.Value, ``size``.Value, ``data``.Value, ``usage``.Value)
        let name() = sprintf "BufferData(%A, %A, %A, %A)" (``target``.Value) (``size``.Value) (``data``.Value) (``usage``.Value)
        commands.Add((name, run))
    override this.BufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``data`` : nativeint) = 
        let run() = gl.glBufferSubData.Invoke(``target``, ``offset``, ``size``, ``data``)
        let name() = sprintf "BufferSubData(%A, %A, %A, %A)" ``target`` ``offset`` ``size`` ``data``
        commands.Add((name, run))
    override this.BufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        let ``data`` = this.Use ``data``
        let run() = gl.glBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``data``.Value)
        let name() = sprintf "BufferSubData(%A, %A, %A, %A)" (``target``.Value) (``offset``.Value) (``size``.Value) (``data``.Value)
        commands.Add((name, run))
    override this.CheckFramebufferStatus(``target`` : FramebufferTarget, ``returnValue`` : nativeptr<GLEnum>) = 
        let run() = gl.glCheckFramebufferStatus.Invoke(``target``) |> NativePtr.write returnValue
        let name() = sprintf "CheckFramebufferStatus(%A)" ``target``
        commands.Add((name, run))
    override this.CheckFramebufferStatus(``target`` : aptr<FramebufferTarget>, ``returnValue`` : aptr<GLEnum>) = 
        let ``target`` = this.Use ``target``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glCheckFramebufferStatus.Invoke(``target``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "CheckFramebufferStatus(%A)" (``target``.Value)
        commands.Add((name, run))
    override this.Clear(``mask`` : ClearBufferMask) = 
        let run() = gl.glClear.Invoke(``mask``)
        let name() = sprintf "Clear(%A)" ``mask``
        commands.Add((name, run))
    override this.Clear(``mask`` : aptr<ClearBufferMask>) = 
        let ``mask`` = this.Use ``mask``
        let run() = gl.glClear.Invoke(``mask``.Value)
        let name() = sprintf "Clear(%A)" (``mask``.Value)
        commands.Add((name, run))
    override this.ClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        let run() = WrappedCommands.glClearColor(``red``, ``green``, ``blue``, ``alpha``)
        let name() = sprintf "ClearColor(%A, %A, %A, %A)" ``red`` ``green`` ``blue`` ``alpha``
        commands.Add((name, run))
    override this.ClearColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        let run() = WrappedCommands.glClearColor(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
        let name() = sprintf "ClearColor(%A, %A, %A, %A)" (``red``.Value) (``green``.Value) (``blue``.Value) (``alpha``.Value)
        commands.Add((name, run))
    override this.ClearDepthf(``d`` : float32) = 
        let run() = WrappedCommands.glClearDepthf(``d``)
        let name() = sprintf "ClearDepthf(%A)" ``d``
        commands.Add((name, run))
    override this.ClearDepthf(``d`` : aptr<float32>) = 
        let ``d`` = this.Use ``d``
        let run() = WrappedCommands.glClearDepthf(``d``.Value)
        let name() = sprintf "ClearDepthf(%A)" (``d``.Value)
        commands.Add((name, run))
    override this.ClearStencil(``s`` : int) = 
        let run() = gl.glClearStencil.Invoke(``s``)
        let name() = sprintf "ClearStencil(%A)" ``s``
        commands.Add((name, run))
    override this.ClearStencil(``s`` : aptr<int>) = 
        let ``s`` = this.Use ``s``
        let run() = gl.glClearStencil.Invoke(``s``.Value)
        let name() = sprintf "ClearStencil(%A)" (``s``.Value)
        commands.Add((name, run))
    override this.ColorMask(``red`` : Boolean, ``green`` : Boolean, ``blue`` : Boolean, ``alpha`` : Boolean) = 
        let run() = gl.glColorMask.Invoke(``red``, ``green``, ``blue``, ``alpha``)
        let name() = sprintf "ColorMask(%A, %A, %A, %A)" ``red`` ``green`` ``blue`` ``alpha``
        commands.Add((name, run))
    override this.ColorMask(``red`` : aptr<Boolean>, ``green`` : aptr<Boolean>, ``blue`` : aptr<Boolean>, ``alpha`` : aptr<Boolean>) = 
        let ``red`` = this.Use ``red``
        let ``green`` = this.Use ``green``
        let ``blue`` = this.Use ``blue``
        let ``alpha`` = this.Use ``alpha``
        let run() = gl.glColorMask.Invoke(``red``.Value, ``green``.Value, ``blue``.Value, ``alpha``.Value)
        let name() = sprintf "ColorMask(%A, %A, %A, %A)" (``red``.Value) (``green``.Value) (``blue``.Value) (``alpha``.Value)
        commands.Add((name, run))
    override this.CompileShader(``shader`` : uint32) = 
        let run() = gl.glCompileShader.Invoke(``shader``)
        let name() = sprintf "CompileShader(%A)" ``shader``
        commands.Add((name, run))
    override this.CompileShader(``shader`` : aptr<uint32>) = 
        let ``shader`` = this.Use ``shader``
        let run() = gl.glCompileShader.Invoke(``shader``.Value)
        let name() = sprintf "CompileShader(%A)" (``shader``.Value)
        commands.Add((name, run))
    override this.CompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        let run() = WrappedCommands.glCompressedTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``imageSize``, ``data``)
        let name() = sprintf "CompressedTexImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``internalformat`` ``width`` ``height`` ``border`` ``imageSize`` ``data``
        commands.Add((name, run))
    override this.CompressedTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        let run() = WrappedCommands.glCompressedTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``imageSize``.Value, ``data``.Value)
        let name() = sprintf "CompressedTexImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value) (``border``.Value) (``imageSize``.Value) (``data``.Value)
        commands.Add((name, run))
    override this.CompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        let run() = WrappedCommands.glCompressedTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``imageSize``, ``data``)
        let name() = sprintf "CompressedTexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``width`` ``height`` ``format`` ``imageSize`` ``data``
        commands.Add((name, run))
    override this.CompressedTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``imageSize`` = this.Use ``imageSize``
        let ``data`` = this.Use ``data``
        let run() = WrappedCommands.glCompressedTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``imageSize``.Value, ``data``.Value)
        let name() = sprintf "CompressedTexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``width``.Value) (``height``.Value) (``format``.Value) (``imageSize``.Value) (``data``.Value)
        commands.Add((name, run))
    override this.CopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) = 
        let run() = WrappedCommands.glCopyTexImage2D(``target``, ``level``, ``internalformat``, ``x``, ``y``, ``width``, ``height``, ``border``)
        let name() = sprintf "CopyTexImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``internalformat`` ``x`` ``y`` ``width`` ``height`` ``border``
        commands.Add((name, run))
    override this.CopyTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        let run() = WrappedCommands.glCopyTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``border``.Value)
        let name() = sprintf "CopyTexImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``internalformat``.Value) (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value) (``border``.Value)
        commands.Add((name, run))
    override this.CopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        let run() = WrappedCommands.glCopyTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``x``, ``y``, ``width``, ``height``)
        let name() = sprintf "CopyTexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``x`` ``y`` ``width`` ``height``
        commands.Add((name, run))
    override this.CopyTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = WrappedCommands.glCopyTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "CopyTexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.CreateProgram(``returnValue`` : nativeptr<uint32>) = 
        let run() = gl.glCreateProgram.Invoke() |> NativePtr.write returnValue
        let name() = sprintf "CreateProgram()" 
        commands.Add((name, run))
    override this.CreateProgram(``returnValue`` : aptr<uint32>) = 
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glCreateProgram.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "CreateProgram()" 
        commands.Add((name, run))
    override this.CreateShader(``type`` : ShaderType, ``returnValue`` : nativeptr<uint32>) = 
        let run() = gl.glCreateShader.Invoke(``type``) |> NativePtr.write returnValue
        let name() = sprintf "CreateShader(%A)" ``type``
        commands.Add((name, run))
    override this.CreateShader(``type`` : aptr<ShaderType>, ``returnValue`` : aptr<uint32>) = 
        let ``type`` = this.Use ``type``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glCreateShader.Invoke(``type``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "CreateShader(%A)" (``type``.Value)
        commands.Add((name, run))
    override this.CullFace(``mode`` : CullFaceMode) = 
        let run() = gl.glCullFace.Invoke(``mode``)
        let name() = sprintf "CullFace(%A)" ``mode``
        commands.Add((name, run))
    override this.CullFace(``mode`` : aptr<CullFaceMode>) = 
        let ``mode`` = this.Use ``mode``
        let run() = gl.glCullFace.Invoke(``mode``.Value)
        let name() = sprintf "CullFace(%A)" (``mode``.Value)
        commands.Add((name, run))
    override this.DeleteBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteBuffers.Invoke(``n``, ``buffers``)
        let name() = sprintf "DeleteBuffers(%A, %A)" ``n`` ``buffers``
        commands.Add((name, run))
    override this.DeleteBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``buffers`` = this.Use ``buffers``
        let run() = gl.glDeleteBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
        let name() = sprintf "DeleteBuffers(%A, %A)" (``n``.Value) (``buffers``.Pointer)
        commands.Add((name, run))
    override this.DeleteFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteFramebuffers.Invoke(``n``, ``framebuffers``)
        let name() = sprintf "DeleteFramebuffers(%A, %A)" ``n`` ``framebuffers``
        commands.Add((name, run))
    override this.DeleteFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``framebuffers`` = this.Use ``framebuffers``
        let run() = gl.glDeleteFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
        let name() = sprintf "DeleteFramebuffers(%A, %A)" (``n``.Value) (``framebuffers``.Pointer)
        commands.Add((name, run))
    override this.DeleteProgram(``program`` : uint32) = 
        let run() = gl.glDeleteProgram.Invoke(``program``)
        let name() = sprintf "DeleteProgram(%A)" ``program``
        commands.Add((name, run))
    override this.DeleteProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let run() = gl.glDeleteProgram.Invoke(``program``.Value)
        let name() = sprintf "DeleteProgram(%A)" (``program``.Value)
        commands.Add((name, run))
    override this.DeleteRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteRenderbuffers.Invoke(``n``, ``renderbuffers``)
        let name() = sprintf "DeleteRenderbuffers(%A, %A)" ``n`` ``renderbuffers``
        commands.Add((name, run))
    override this.DeleteRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``renderbuffers`` = this.Use ``renderbuffers``
        let run() = gl.glDeleteRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
        let name() = sprintf "DeleteRenderbuffers(%A, %A)" (``n``.Value) (``renderbuffers``.Pointer)
        commands.Add((name, run))
    override this.DeleteShader(``shader`` : uint32) = 
        let run() = gl.glDeleteShader.Invoke(``shader``)
        let name() = sprintf "DeleteShader(%A)" ``shader``
        commands.Add((name, run))
    override this.DeleteShader(``shader`` : aptr<uint32>) = 
        let ``shader`` = this.Use ``shader``
        let run() = gl.glDeleteShader.Invoke(``shader``.Value)
        let name() = sprintf "DeleteShader(%A)" (``shader``.Value)
        commands.Add((name, run))
    override this.DeleteTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        let run() = gl.glDeleteTextures.Invoke(``n``, ``textures``)
        let name() = sprintf "DeleteTextures(%A, %A)" ``n`` ``textures``
        commands.Add((name, run))
    override this.DeleteTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``textures`` = this.Use ``textures``
        let run() = gl.glDeleteTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
        let name() = sprintf "DeleteTextures(%A, %A)" (``n``.Value) (``textures``.Pointer)
        commands.Add((name, run))
    override this.DepthFunc(``func`` : DepthFunction) = 
        let run() = gl.glDepthFunc.Invoke(``func``)
        let name() = sprintf "DepthFunc(%A)" ``func``
        commands.Add((name, run))
    override this.DepthFunc(``func`` : aptr<DepthFunction>) = 
        let ``func`` = this.Use ``func``
        let run() = gl.glDepthFunc.Invoke(``func``.Value)
        let name() = sprintf "DepthFunc(%A)" (``func``.Value)
        commands.Add((name, run))
    override this.DepthMask(``flag`` : Boolean) = 
        let run() = gl.glDepthMask.Invoke(``flag``)
        let name() = sprintf "DepthMask(%A)" ``flag``
        commands.Add((name, run))
    override this.DepthMask(``flag`` : aptr<Boolean>) = 
        let ``flag`` = this.Use ``flag``
        let run() = gl.glDepthMask.Invoke(``flag``.Value)
        let name() = sprintf "DepthMask(%A)" (``flag``.Value)
        commands.Add((name, run))
    override this.DepthRangef(``n`` : float32, ``f`` : float32) = 
        let run() = WrappedCommands.glDepthRangef(``n``, ``f``)
        let name() = sprintf "DepthRangef(%A, %A)" ``n`` ``f``
        commands.Add((name, run))
    override this.DepthRangef(``n`` : aptr<float32>, ``f`` : aptr<float32>) = 
        let ``n`` = this.Use ``n``
        let ``f`` = this.Use ``f``
        let run() = WrappedCommands.glDepthRangef(``n``.Value, ``f``.Value)
        let name() = sprintf "DepthRangef(%A, %A)" (``n``.Value) (``f``.Value)
        commands.Add((name, run))
    override this.DetachShader(``program`` : uint32, ``shader`` : uint32) = 
        let run() = gl.glDetachShader.Invoke(``program``, ``shader``)
        let name() = sprintf "DetachShader(%A, %A)" ``program`` ``shader``
        commands.Add((name, run))
    override this.DetachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``shader`` = this.Use ``shader``
        let run() = gl.glDetachShader.Invoke(``program``.Value, ``shader``.Value)
        let name() = sprintf "DetachShader(%A, %A)" (``program``.Value) (``shader``.Value)
        commands.Add((name, run))
    override this.Disable(``cap`` : EnableCap) = 
        let run() = gl.glDisable.Invoke(``cap``)
        let name() = sprintf "Disable(%A)" ``cap``
        commands.Add((name, run))
    override this.Disable(``cap`` : aptr<EnableCap>) = 
        let ``cap`` = this.Use ``cap``
        let run() = gl.glDisable.Invoke(``cap``.Value)
        let name() = sprintf "Disable(%A)" (``cap``.Value)
        commands.Add((name, run))
    override this.DisableVertexAttribArray(``index`` : uint32) = 
        let run() = gl.glDisableVertexAttribArray.Invoke(``index``)
        let name() = sprintf "DisableVertexAttribArray(%A)" ``index``
        commands.Add((name, run))
    override this.DisableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let run() = gl.glDisableVertexAttribArray.Invoke(``index``.Value)
        let name() = sprintf "DisableVertexAttribArray(%A)" (``index``.Value)
        commands.Add((name, run))
    override this.DrawArrays(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32) = 
        let run() = gl.glDrawArrays.Invoke(``mode``, ``first``, ``count``)
        let name() = sprintf "DrawArrays(%A, %A, %A)" ``mode`` ``first`` ``count``
        commands.Add((name, run))
    override this.DrawArrays(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>) = 
        let ``mode`` = this.Use ``mode``
        let ``first`` = this.Use ``first``
        let ``count`` = this.Use ``count``
        let run() = gl.glDrawArrays.Invoke(``mode``.Value, ``first``.Value, ``count``.Value)
        let name() = sprintf "DrawArrays(%A, %A, %A)" (``mode``.Value) (``first``.Value) (``count``.Value)
        commands.Add((name, run))
    override this.DrawElements(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        let run() = gl.glDrawElements.Invoke(``mode``, ``count``, ``type``, ``indices``)
        let name() = sprintf "DrawElements(%A, %A, %A, %A)" ``mode`` ``count`` ``type`` ``indices``
        commands.Add((name, run))
    override this.DrawElements(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``count`` = this.Use ``count``
        let ``type`` = this.Use ``type``
        let ``indices`` = this.Use ``indices``
        let run() = gl.glDrawElements.Invoke(``mode``.Value, ``count``.Value, ``type``.Value, ``indices``.Value)
        let name() = sprintf "DrawElements(%A, %A, %A, %A)" (``mode``.Value) (``count``.Value) (``type``.Value) (``indices``.Value)
        commands.Add((name, run))
    override this.Enable(``cap`` : EnableCap) = 
        let run() = gl.glEnable.Invoke(``cap``)
        let name() = sprintf "Enable(%A)" ``cap``
        commands.Add((name, run))
    override this.Enable(``cap`` : aptr<EnableCap>) = 
        let ``cap`` = this.Use ``cap``
        let run() = gl.glEnable.Invoke(``cap``.Value)
        let name() = sprintf "Enable(%A)" (``cap``.Value)
        commands.Add((name, run))
    override this.EnableVertexAttribArray(``index`` : uint32) = 
        let run() = gl.glEnableVertexAttribArray.Invoke(``index``)
        let name() = sprintf "EnableVertexAttribArray(%A)" ``index``
        commands.Add((name, run))
    override this.EnableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index`` = this.Use ``index``
        let run() = gl.glEnableVertexAttribArray.Invoke(``index``.Value)
        let name() = sprintf "EnableVertexAttribArray(%A)" (``index``.Value)
        commands.Add((name, run))
    override this.Finish() = 
        let run() = gl.glFinish.Invoke()
        commands.Add(((fun () -> "Finish"), run))
    override this.Flush() = 
        let run() = gl.glFlush.Invoke()
        commands.Add(((fun () -> "Flush"), run))
    override this.FramebufferRenderbuffer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``renderbuffertarget`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        let run() = gl.glFramebufferRenderbuffer.Invoke(``target``, ``attachment``, ``renderbuffertarget``, ``renderbuffer``)
        let name() = sprintf "FramebufferRenderbuffer(%A, %A, %A, %A)" ``target`` ``attachment`` ``renderbuffertarget`` ``renderbuffer``
        commands.Add((name, run))
    override this.FramebufferRenderbuffer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``renderbuffertarget`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``renderbuffertarget`` = this.Use ``renderbuffertarget``
        let ``renderbuffer`` = this.Use ``renderbuffer``
        let run() = gl.glFramebufferRenderbuffer.Invoke(``target``.Value, ``attachment``.Value, ``renderbuffertarget``.Value, ``renderbuffer``.Value)
        let name() = sprintf "FramebufferRenderbuffer(%A, %A, %A, %A)" (``target``.Value) (``attachment``.Value) (``renderbuffertarget``.Value) (``renderbuffer``.Value)
        commands.Add((name, run))
    override this.FramebufferTexture2D(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``textarget`` : TextureTarget, ``texture`` : uint32, ``level`` : int) = 
        let run() = gl.glFramebufferTexture2D.Invoke(``target``, ``attachment``, ``textarget``, ``texture``, ``level``)
        let name() = sprintf "FramebufferTexture2D(%A, %A, %A, %A, %A)" ``target`` ``attachment`` ``textarget`` ``texture`` ``level``
        commands.Add((name, run))
    override this.FramebufferTexture2D(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``textarget`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``textarget`` = this.Use ``textarget``
        let ``texture`` = this.Use ``texture``
        let ``level`` = this.Use ``level``
        let run() = gl.glFramebufferTexture2D.Invoke(``target``.Value, ``attachment``.Value, ``textarget``.Value, ``texture``.Value, ``level``.Value)
        let name() = sprintf "FramebufferTexture2D(%A, %A, %A, %A, %A)" (``target``.Value) (``attachment``.Value) (``textarget``.Value) (``texture``.Value) (``level``.Value)
        commands.Add((name, run))
    override this.FrontFace(``mode`` : FrontFaceDirection) = 
        let run() = gl.glFrontFace.Invoke(``mode``)
        let name() = sprintf "FrontFace(%A)" ``mode``
        commands.Add((name, run))
    override this.FrontFace(``mode`` : aptr<FrontFaceDirection>) = 
        let ``mode`` = this.Use ``mode``
        let run() = gl.glFrontFace.Invoke(``mode``.Value)
        let name() = sprintf "FrontFace(%A)" (``mode``.Value)
        commands.Add((name, run))
    override this.GenBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        let run() = gl.glGenBuffers.Invoke(``n``, ``buffers``)
        let name() = sprintf "GenBuffers(%A, %A)" ``n`` ``buffers``
        commands.Add((name, run))
    override this.GenBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``buffers`` = this.Use ``buffers``
        let run() = gl.glGenBuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``buffers``.Pointer)
        let name() = sprintf "GenBuffers(%A, %A)" (``n``.Value) (``buffers``.Pointer)
        commands.Add((name, run))
    override this.GenerateMipmap(``target`` : TextureTarget) = 
        let run() = gl.glGenerateMipmap.Invoke(``target``)
        let name() = sprintf "GenerateMipmap(%A)" ``target``
        commands.Add((name, run))
    override this.GenerateMipmap(``target`` : aptr<TextureTarget>) = 
        let ``target`` = this.Use ``target``
        let run() = gl.glGenerateMipmap.Invoke(``target``.Value)
        let name() = sprintf "GenerateMipmap(%A)" (``target``.Value)
        commands.Add((name, run))
    override this.GenFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        let run() = gl.glGenFramebuffers.Invoke(``n``, ``framebuffers``)
        let name() = sprintf "GenFramebuffers(%A, %A)" ``n`` ``framebuffers``
        commands.Add((name, run))
    override this.GenFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``framebuffers`` = this.Use ``framebuffers``
        let run() = gl.glGenFramebuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``framebuffers``.Pointer)
        let name() = sprintf "GenFramebuffers(%A, %A)" (``n``.Value) (``framebuffers``.Pointer)
        commands.Add((name, run))
    override this.GenRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        let run() = gl.glGenRenderbuffers.Invoke(``n``, ``renderbuffers``)
        let name() = sprintf "GenRenderbuffers(%A, %A)" ``n`` ``renderbuffers``
        commands.Add((name, run))
    override this.GenRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``renderbuffers`` = this.Use ``renderbuffers``
        let run() = gl.glGenRenderbuffers.Invoke(``n``.Value, NativePtr.ofNativeInt ``renderbuffers``.Pointer)
        let name() = sprintf "GenRenderbuffers(%A, %A)" (``n``.Value) (``renderbuffers``.Pointer)
        commands.Add((name, run))
    override this.GenTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        let run() = gl.glGenTextures.Invoke(``n``, ``textures``)
        let name() = sprintf "GenTextures(%A, %A)" ``n`` ``textures``
        commands.Add((name, run))
    override this.GenTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n`` = this.Use ``n``
        let ``textures`` = this.Use ``textures``
        let run() = gl.glGenTextures.Invoke(``n``.Value, NativePtr.ofNativeInt ``textures``.Pointer)
        let name() = sprintf "GenTextures(%A, %A)" (``n``.Value) (``textures``.Pointer)
        commands.Add((name, run))
    override this.GetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        let run() = WrappedCommands.glGetActiveAttrib(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
        let name() = sprintf "GetActiveAttrib(%A, %A, %A, %A, %A, %A, %A)" ``program`` ``index`` ``bufSize`` ``length`` ``size`` ``type`` ``name``
        commands.Add((name, run))
    override this.GetActiveAttrib(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        let run() = WrappedCommands.glGetActiveAttrib(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
        let name() = sprintf "GetActiveAttrib(%A, %A, %A, %A, %A, %A, %A)" (``program``.Value) (``index``.Value) (``bufSize``.Value) (``length``.Pointer) (``size``.Pointer) (``type``.Pointer) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        let run() = WrappedCommands.glGetActiveUniform(``program``, ``index``, ``bufSize``, ``length``, ``size``, ``type``, ``name``)
        let name() = sprintf "GetActiveUniform(%A, %A, %A, %A, %A, %A, %A)" ``program`` ``index`` ``bufSize`` ``length`` ``size`` ``type`` ``name``
        commands.Add((name, run))
    override this.GetActiveUniform(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``index`` = this.Use ``index``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``name`` = this.Use ``name``
        let run() = WrappedCommands.glGetActiveUniform(``program``.Value, ``index``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``size``.Pointer, NativePtr.ofNativeInt ``type``.Pointer, NativePtr.ofNativeInt ``name``.Pointer)
        let name() = sprintf "GetActiveUniform(%A, %A, %A, %A, %A, %A, %A)" (``program``.Value) (``index``.Value) (``bufSize``.Value) (``length``.Pointer) (``size``.Pointer) (``type``.Pointer) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetAttachedShaders(``program`` : uint32, ``maxCount`` : uint32, ``count`` : nativeptr<uint32>, ``shaders`` : nativeptr<uint32>) = 
        let run() = gl.glGetAttachedShaders.Invoke(``program``, ``maxCount``, ``count``, ``shaders``)
        let name() = sprintf "GetAttachedShaders(%A, %A, %A, %A)" ``program`` ``maxCount`` ``count`` ``shaders``
        commands.Add((name, run))
    override this.GetAttachedShaders(``program`` : aptr<uint32>, ``maxCount`` : aptr<uint32>, ``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let ``maxCount`` = this.Use ``maxCount``
        let ``count`` = this.Use ``count``
        let ``shaders`` = this.Use ``shaders``
        let run() = gl.glGetAttachedShaders.Invoke(``program``.Value, ``maxCount``.Value, NativePtr.ofNativeInt ``count``.Pointer, NativePtr.ofNativeInt ``shaders``.Pointer)
        let name() = sprintf "GetAttachedShaders(%A, %A, %A, %A)" (``program``.Value) (``maxCount``.Value) (``count``.Pointer) (``shaders``.Pointer)
        commands.Add((name, run))
    override this.GetAttribLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        let run() = gl.glGetAttribLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
        let name() = sprintf "GetAttribLocation(%A, %A)" ``program`` ``name``
        commands.Add((name, run))
    override this.GetAttribLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetAttribLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetAttribLocation(%A, %A)" (``program``.Value) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetBooleanv(``pname`` : GetPName, ``data`` : nativeptr<Boolean>) = 
        let run() = gl.glGetBooleanv.Invoke(``pname``, ``data``)
        let name() = sprintf "GetBooleanv(%A, %A)" ``pname`` ``data``
        commands.Add((name, run))
    override this.GetBooleanv(``pname`` : aptr<GetPName>, ``data`` : aptr<Boolean>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetBooleanv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetBooleanv(%A, %A)" (``pname``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetBufferParameteriv(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetBufferParameteriv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetBufferParameteriv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetBufferParameteriv(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetBufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetBufferParameteriv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetError(``returnValue`` : nativeptr<GLEnum>) = 
        let run() = gl.glGetError.Invoke() |> NativePtr.write returnValue
        let name() = sprintf "GetError()" 
        commands.Add((name, run))
    override this.GetError(``returnValue`` : aptr<GLEnum>) = 
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetError.Invoke() |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetError()" 
        commands.Add((name, run))
    override this.GetFloatv(``pname`` : GetPName, ``data`` : nativeptr<float32>) = 
        let run() = gl.glGetFloatv.Invoke(``pname``, ``data``)
        let name() = sprintf "GetFloatv(%A, %A)" ``pname`` ``data``
        commands.Add((name, run))
    override this.GetFloatv(``pname`` : aptr<GetPName>, ``data`` : aptr<float32>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetFloatv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetFloatv(%A, %A)" (``pname``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetFramebufferAttachmentParameteriv(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``pname`` : FramebufferAttachmentParameterName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``, ``attachment``, ``pname``, ``params``)
        let name() = sprintf "GetFramebufferAttachmentParameteriv(%A, %A, %A, %A)" ``target`` ``attachment`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetFramebufferAttachmentParameteriv(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``pname`` : aptr<FramebufferAttachmentParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``attachment`` = this.Use ``attachment``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetFramebufferAttachmentParameteriv.Invoke(``target``.Value, ``attachment``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetFramebufferAttachmentParameteriv(%A, %A, %A, %A)" (``target``.Value) (``attachment``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetIntegerv(``pname`` : GetPName, ``data`` : nativeptr<int>) = 
        let run() = gl.glGetIntegerv.Invoke(``pname``, ``data``)
        let name() = sprintf "GetIntegerv(%A, %A)" ``pname`` ``data``
        commands.Add((name, run))
    override this.GetIntegerv(``pname`` : aptr<GetPName>, ``data`` : aptr<int>) = 
        let ``pname`` = this.Use ``pname``
        let ``data`` = this.Use ``data``
        let run() = gl.glGetIntegerv.Invoke(``pname``.Value, NativePtr.ofNativeInt ``data``.Pointer)
        let name() = sprintf "GetIntegerv(%A, %A)" (``pname``.Value) (``data``.Pointer)
        commands.Add((name, run))
    override this.GetProgramiv(``program`` : uint32, ``pname`` : ProgramPropertyARB, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetProgramiv.Invoke(``program``, ``pname``, ``params``)
        let name() = sprintf "GetProgramiv(%A, %A, %A)" ``program`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetProgramiv(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramPropertyARB>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetProgramiv.Invoke(``program``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetProgramiv(%A, %A, %A)" (``program``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetProgramInfoLog(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        let run() = gl.glGetProgramInfoLog.Invoke(``program``, ``bufSize``, ``length``, ``infoLog``)
        let name() = sprintf "GetProgramInfoLog(%A, %A, %A, %A)" ``program`` ``bufSize`` ``length`` ``infoLog``
        commands.Add((name, run))
    override this.GetProgramInfoLog(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``program`` = this.Use ``program``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``infoLog`` = this.Use ``infoLog``
        let run() = gl.glGetProgramInfoLog.Invoke(``program``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
        let name() = sprintf "GetProgramInfoLog(%A, %A, %A, %A)" (``program``.Value) (``bufSize``.Value) (``length``.Pointer) (``infoLog``.Pointer)
        commands.Add((name, run))
    override this.GetRenderbufferParameteriv(``target`` : RenderbufferTarget, ``pname`` : RenderbufferParameterName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetRenderbufferParameteriv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetRenderbufferParameteriv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetRenderbufferParameteriv(``target`` : aptr<RenderbufferTarget>, ``pname`` : aptr<RenderbufferParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetRenderbufferParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetRenderbufferParameteriv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetShaderiv(``shader`` : uint32, ``pname`` : ShaderParameterName, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetShaderiv.Invoke(``shader``, ``pname``, ``params``)
        let name() = sprintf "GetShaderiv(%A, %A, %A)" ``shader`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetShaderiv(``shader`` : aptr<uint32>, ``pname`` : aptr<ShaderParameterName>, ``params`` : aptr<int>) = 
        let ``shader`` = this.Use ``shader``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetShaderiv.Invoke(``shader``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetShaderiv(%A, %A, %A)" (``shader``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetShaderInfoLog(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        let run() = gl.glGetShaderInfoLog.Invoke(``shader``, ``bufSize``, ``length``, ``infoLog``)
        let name() = sprintf "GetShaderInfoLog(%A, %A, %A, %A)" ``shader`` ``bufSize`` ``length`` ``infoLog``
        commands.Add((name, run))
    override this.GetShaderInfoLog(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``shader`` = this.Use ``shader``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``infoLog`` = this.Use ``infoLog``
        let run() = gl.glGetShaderInfoLog.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``infoLog``.Pointer)
        let name() = sprintf "GetShaderInfoLog(%A, %A, %A, %A)" (``shader``.Value) (``bufSize``.Value) (``length``.Pointer) (``infoLog``.Pointer)
        commands.Add((name, run))
    override this.GetShaderPrecisionFormat(``shadertype`` : ShaderType, ``precisiontype`` : PrecisionType, ``range`` : nativeptr<int>, ``precision`` : nativeptr<int>) = 
        let run() = gl.glGetShaderPrecisionFormat.Invoke(``shadertype``, ``precisiontype``, ``range``, ``precision``)
        let name() = sprintf "GetShaderPrecisionFormat(%A, %A, %A, %A)" ``shadertype`` ``precisiontype`` ``range`` ``precision``
        commands.Add((name, run))
    override this.GetShaderPrecisionFormat(``shadertype`` : aptr<ShaderType>, ``precisiontype`` : aptr<PrecisionType>, ``range`` : aptr<int>, ``precision`` : aptr<int>) = 
        let ``shadertype`` = this.Use ``shadertype``
        let ``precisiontype`` = this.Use ``precisiontype``
        let ``range`` = this.Use ``range``
        let ``precision`` = this.Use ``precision``
        let run() = gl.glGetShaderPrecisionFormat.Invoke(``shadertype``.Value, ``precisiontype``.Value, NativePtr.ofNativeInt ``range``.Pointer, NativePtr.ofNativeInt ``precision``.Pointer)
        let name() = sprintf "GetShaderPrecisionFormat(%A, %A, %A, %A)" (``shadertype``.Value) (``precisiontype``.Value) (``range``.Pointer) (``precision``.Pointer)
        commands.Add((name, run))
    override this.GetShaderSource(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``source`` : nativeptr<uint8>) = 
        let run() = gl.glGetShaderSource.Invoke(``shader``, ``bufSize``, ``length``, ``source``)
        let name() = sprintf "GetShaderSource(%A, %A, %A, %A)" ``shader`` ``bufSize`` ``length`` ``source``
        commands.Add((name, run))
    override this.GetShaderSource(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``source`` : aptr<uint8>) = 
        let ``shader`` = this.Use ``shader``
        let ``bufSize`` = this.Use ``bufSize``
        let ``length`` = this.Use ``length``
        let ``source`` = this.Use ``source``
        let run() = gl.glGetShaderSource.Invoke(``shader``.Value, ``bufSize``.Value, NativePtr.ofNativeInt ``length``.Pointer, NativePtr.ofNativeInt ``source``.Pointer)
        let name() = sprintf "GetShaderSource(%A, %A, %A, %A)" (``shader``.Value) (``bufSize``.Value) (``length``.Pointer) (``source``.Pointer)
        commands.Add((name, run))
    override this.GetString(``name`` : StringName, ``returnValue`` : nativeptr<nativeptr<uint8>>) = 
        let run() = gl.glGetString.Invoke(``name``) |> NativePtr.write returnValue
        let name() = sprintf "GetString(%A)" ``name``
        commands.Add((name, run))
    override this.GetString(``name`` : aptr<StringName>, ``returnValue`` : aptr<nativeptr<uint8>>) = 
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetString.Invoke(``name``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetString(%A)" (``name``.Value)
        commands.Add((name, run))
    override this.GetTexParameterfv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<float32>) = 
        let run() = gl.glGetTexParameterfv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetTexParameterfv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetTexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetTexParameterfv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetTexParameteriv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetTexParameteriv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "GetTexParameteriv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetTexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetTexParameteriv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetUniformfv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<float32>) = 
        let run() = gl.glGetUniformfv.Invoke(``program``, ``location``, ``params``)
        let name() = sprintf "GetUniformfv(%A, %A, %A)" ``program`` ``location`` ``params``
        commands.Add((name, run))
    override this.GetUniformfv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<float32>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetUniformfv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetUniformfv(%A, %A, %A)" (``program``.Value) (``location``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetUniformiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetUniformiv.Invoke(``program``, ``location``, ``params``)
        let name() = sprintf "GetUniformiv(%A, %A, %A)" ``program`` ``location`` ``params``
        commands.Add((name, run))
    override this.GetUniformiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``location`` = this.Use ``location``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetUniformiv.Invoke(``program``.Value, ``location``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetUniformiv(%A, %A, %A)" (``program``.Value) (``location``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetUniformLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) = 
        let run() = gl.glGetUniformLocation.Invoke(``program``, ``name``) |> NativePtr.write returnValue
        let name() = sprintf "GetUniformLocation(%A, %A)" ``program`` ``name``
        commands.Add((name, run))
    override this.GetUniformLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) = 
        let ``program`` = this.Use ``program``
        let ``name`` = this.Use ``name``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glGetUniformLocation.Invoke(``program``.Value, NativePtr.ofNativeInt ``name``.Pointer) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "GetUniformLocation(%A, %A)" (``program``.Value) (``name``.Pointer)
        commands.Add((name, run))
    override this.GetVertexAttribfv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<float32>) = 
        let run() = gl.glGetVertexAttribfv.Invoke(``index``, ``pname``, ``params``)
        let name() = sprintf "GetVertexAttribfv(%A, %A, %A)" ``index`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetVertexAttribfv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetVertexAttribfv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetVertexAttribfv(%A, %A, %A)" (``index``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetVertexAttribiv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<int>) = 
        let run() = gl.glGetVertexAttribiv.Invoke(``index``, ``pname``, ``params``)
        let name() = sprintf "GetVertexAttribiv(%A, %A, %A)" ``index`` ``pname`` ``params``
        commands.Add((name, run))
    override this.GetVertexAttribiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<int>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glGetVertexAttribiv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "GetVertexAttribiv(%A, %A, %A)" (``index``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.GetVertexAttribPointerv(``index`` : uint32, ``pname`` : VertexAttribPointerPropertyARB, ``pointer`` : nativeptr<nativeint>) = 
        let run() = gl.glGetVertexAttribPointerv.Invoke(``index``, ``pname``, ``pointer``)
        let name() = sprintf "GetVertexAttribPointerv(%A, %A, %A)" ``index`` ``pname`` ``pointer``
        commands.Add((name, run))
    override this.GetVertexAttribPointerv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPointerPropertyARB>, ``pointer`` : aptr<nativeint>) = 
        let ``index`` = this.Use ``index``
        let ``pname`` = this.Use ``pname``
        let ``pointer`` = this.Use ``pointer``
        let run() = gl.glGetVertexAttribPointerv.Invoke(``index``.Value, ``pname``.Value, NativePtr.ofNativeInt ``pointer``.Pointer)
        let name() = sprintf "GetVertexAttribPointerv(%A, %A, %A)" (``index``.Value) (``pname``.Value) (``pointer``.Pointer)
        commands.Add((name, run))
    override this.Hint(``target`` : HintTarget, ``mode`` : HintMode) = 
        let run() = gl.glHint.Invoke(``target``, ``mode``)
        let name() = sprintf "Hint(%A, %A)" ``target`` ``mode``
        commands.Add((name, run))
    override this.Hint(``target`` : aptr<HintTarget>, ``mode`` : aptr<HintMode>) = 
        let ``target`` = this.Use ``target``
        let ``mode`` = this.Use ``mode``
        let run() = gl.glHint.Invoke(``target``.Value, ``mode``.Value)
        let name() = sprintf "Hint(%A, %A)" (``target``.Value) (``mode``.Value)
        commands.Add((name, run))
    override this.IsBuffer(``buffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsBuffer.Invoke(``buffer``) |> NativePtr.write returnValue
        let name() = sprintf "IsBuffer(%A)" ``buffer``
        commands.Add((name, run))
    override this.IsBuffer(``buffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``buffer`` = this.Use ``buffer``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsBuffer.Invoke(``buffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsBuffer(%A)" (``buffer``.Value)
        commands.Add((name, run))
    override this.IsEnabled(``cap`` : EnableCap, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsEnabled.Invoke(``cap``) |> NativePtr.write returnValue
        let name() = sprintf "IsEnabled(%A)" ``cap``
        commands.Add((name, run))
    override this.IsEnabled(``cap`` : aptr<EnableCap>, ``returnValue`` : aptr<Boolean>) = 
        let ``cap`` = this.Use ``cap``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsEnabled.Invoke(``cap``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsEnabled(%A)" (``cap``.Value)
        commands.Add((name, run))
    override this.IsFramebuffer(``framebuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsFramebuffer.Invoke(``framebuffer``) |> NativePtr.write returnValue
        let name() = sprintf "IsFramebuffer(%A)" ``framebuffer``
        commands.Add((name, run))
    override this.IsFramebuffer(``framebuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``framebuffer`` = this.Use ``framebuffer``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsFramebuffer.Invoke(``framebuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsFramebuffer(%A)" (``framebuffer``.Value)
        commands.Add((name, run))
    override this.IsProgram(``program`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsProgram.Invoke(``program``) |> NativePtr.write returnValue
        let name() = sprintf "IsProgram(%A)" ``program``
        commands.Add((name, run))
    override this.IsProgram(``program`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``program`` = this.Use ``program``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsProgram.Invoke(``program``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsProgram(%A)" (``program``.Value)
        commands.Add((name, run))
    override this.IsRenderbuffer(``renderbuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsRenderbuffer.Invoke(``renderbuffer``) |> NativePtr.write returnValue
        let name() = sprintf "IsRenderbuffer(%A)" ``renderbuffer``
        commands.Add((name, run))
    override this.IsRenderbuffer(``renderbuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``renderbuffer`` = this.Use ``renderbuffer``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsRenderbuffer.Invoke(``renderbuffer``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsRenderbuffer(%A)" (``renderbuffer``.Value)
        commands.Add((name, run))
    override this.IsShader(``shader`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsShader.Invoke(``shader``) |> NativePtr.write returnValue
        let name() = sprintf "IsShader(%A)" ``shader``
        commands.Add((name, run))
    override this.IsShader(``shader`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``shader`` = this.Use ``shader``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsShader.Invoke(``shader``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsShader(%A)" (``shader``.Value)
        commands.Add((name, run))
    override this.IsTexture(``texture`` : uint32, ``returnValue`` : nativeptr<Boolean>) = 
        let run() = gl.glIsTexture.Invoke(``texture``) |> NativePtr.write returnValue
        let name() = sprintf "IsTexture(%A)" ``texture``
        commands.Add((name, run))
    override this.IsTexture(``texture`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) = 
        let ``texture`` = this.Use ``texture``
        let ``returnValue`` = this.Use ``returnValue``
        let run() = gl.glIsTexture.Invoke(``texture``.Value) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)
        let name() = sprintf "IsTexture(%A)" (``texture``.Value)
        commands.Add((name, run))
    override this.LineWidth(``width`` : float32) = 
        let run() = WrappedCommands.glLineWidth(``width``)
        let name() = sprintf "LineWidth(%A)" ``width``
        commands.Add((name, run))
    override this.LineWidth(``width`` : aptr<float32>) = 
        let ``width`` = this.Use ``width``
        let run() = WrappedCommands.glLineWidth(``width``.Value)
        let name() = sprintf "LineWidth(%A)" (``width``.Value)
        commands.Add((name, run))
    override this.LinkProgram(``program`` : uint32) = 
        let run() = gl.glLinkProgram.Invoke(``program``)
        let name() = sprintf "LinkProgram(%A)" ``program``
        commands.Add((name, run))
    override this.LinkProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let run() = gl.glLinkProgram.Invoke(``program``.Value)
        let name() = sprintf "LinkProgram(%A)" (``program``.Value)
        commands.Add((name, run))
    override this.PixelStorei(``pname`` : PixelStoreParameter, ``param`` : int) = 
        let run() = gl.glPixelStorei.Invoke(``pname``, ``param``)
        let name() = sprintf "PixelStorei(%A, %A)" ``pname`` ``param``
        commands.Add((name, run))
    override this.PixelStorei(``pname`` : aptr<PixelStoreParameter>, ``param`` : aptr<int>) = 
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = gl.glPixelStorei.Invoke(``pname``.Value, ``param``.Value)
        let name() = sprintf "PixelStorei(%A, %A)" (``pname``.Value) (``param``.Value)
        commands.Add((name, run))
    override this.PolygonOffset(``factor`` : float32, ``units`` : float32) = 
        let run() = WrappedCommands.glPolygonOffset(``factor``, ``units``)
        let name() = sprintf "PolygonOffset(%A, %A)" ``factor`` ``units``
        commands.Add((name, run))
    override this.PolygonOffset(``factor`` : aptr<float32>, ``units`` : aptr<float32>) = 
        let ``factor`` = this.Use ``factor``
        let ``units`` = this.Use ``units``
        let run() = WrappedCommands.glPolygonOffset(``factor``.Value, ``units``.Value)
        let name() = sprintf "PolygonOffset(%A, %A)" (``factor``.Value) (``units``.Value)
        commands.Add((name, run))
    override this.ReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        let run() = WrappedCommands.glReadPixels(``x``, ``y``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
        let name() = sprintf "ReadPixels(%A, %A, %A, %A, %A, %A, %A)" ``x`` ``y`` ``width`` ``height`` ``format`` ``type`` ``pixels``
        commands.Add((name, run))
    override this.ReadPixels(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<'T7>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        let run() = WrappedCommands.glReadPixels(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Pointer)
        let name() = sprintf "ReadPixels(%A, %A, %A, %A, %A, %A, %A)" (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value) (``format``.Value) (``type``.Value) (``pixels``.Pointer)
        commands.Add((name, run))
    override this.ReleaseShaderCompiler() = 
        let run() = gl.glReleaseShaderCompiler.Invoke()
        commands.Add(((fun () -> "ReleaseShaderCompiler"), run))
    override this.RenderbufferStorage(``target`` : RenderbufferTarget, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        let run() = gl.glRenderbufferStorage.Invoke(``target``, ``internalformat``, ``width``, ``height``)
        let name() = sprintf "RenderbufferStorage(%A, %A, %A, %A)" ``target`` ``internalformat`` ``width`` ``height``
        commands.Add((name, run))
    override this.RenderbufferStorage(``target`` : aptr<RenderbufferTarget>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target`` = this.Use ``target``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = gl.glRenderbufferStorage.Invoke(``target``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "RenderbufferStorage(%A, %A, %A, %A)" (``target``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.SampleCoverage(``value`` : float32, ``invert`` : Boolean) = 
        let run() = WrappedCommands.glSampleCoverage(``value``, ``invert``)
        let name() = sprintf "SampleCoverage(%A, %A)" ``value`` ``invert``
        commands.Add((name, run))
    override this.SampleCoverage(``value`` : aptr<float32>, ``invert`` : aptr<Boolean>) = 
        let ``value`` = this.Use ``value``
        let ``invert`` = this.Use ``invert``
        let run() = WrappedCommands.glSampleCoverage(``value``.Value, ``invert``.Value)
        let name() = sprintf "SampleCoverage(%A, %A)" (``value``.Value) (``invert``.Value)
        commands.Add((name, run))
    override this.Scissor(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        let run() = gl.glScissor.Invoke(``x``, ``y``, ``width``, ``height``)
        let name() = sprintf "Scissor(%A, %A, %A, %A)" ``x`` ``y`` ``width`` ``height``
        commands.Add((name, run))
    override this.Scissor(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = gl.glScissor.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "Scissor(%A, %A, %A, %A)" (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.ShaderBinary(``count`` : uint32, ``shaders`` : nativeptr<uint32>, ``binaryFormat`` : ShaderBinaryFormat, ``binary`` : nativeint, ``length`` : uint32) = 
        let run() = gl.glShaderBinary.Invoke(``count``, ``shaders``, ``binaryFormat``, ``binary``, ``length``)
        let name() = sprintf "ShaderBinary(%A, %A, %A, %A, %A)" ``count`` ``shaders`` ``binaryFormat`` ``binary`` ``length``
        commands.Add((name, run))
    override this.ShaderBinary(``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>, ``binaryFormat`` : aptr<ShaderBinaryFormat>, ``binary`` : aptr<'T4>, ``length`` : aptr<uint32>) = 
        let ``count`` = this.Use ``count``
        let ``shaders`` = this.Use ``shaders``
        let ``binaryFormat`` = this.Use ``binaryFormat``
        let ``binary`` = this.Use ``binary``
        let ``length`` = this.Use ``length``
        let run() = gl.glShaderBinary.Invoke(``count``.Value, NativePtr.ofNativeInt ``shaders``.Pointer, ``binaryFormat``.Value, ``binary``.Pointer, ``length``.Value)
        let name() = sprintf "ShaderBinary(%A, %A, %A, %A, %A)" (``count``.Value) (``shaders``.Pointer) (``binaryFormat``.Value) (``binary``.Pointer) (``length``.Value)
        commands.Add((name, run))
    override this.ShaderSource(``shader`` : uint32, ``count`` : uint32, ``string`` : nativeptr<nativeptr<uint8>>, ``length`` : nativeptr<int>) = 
        let run() = gl.glShaderSource.Invoke(``shader``, ``count``, ``string``, ``length``)
        let name() = sprintf "ShaderSource(%A, %A, %A, %A)" ``shader`` ``count`` ``string`` ``length``
        commands.Add((name, run))
    override this.ShaderSource(``shader`` : aptr<uint32>, ``count`` : aptr<uint32>, ``string`` : aptr<nativeptr<uint8>>, ``length`` : aptr<int>) = 
        let ``shader`` = this.Use ``shader``
        let ``count`` = this.Use ``count``
        let ``string`` = this.Use ``string``
        let ``length`` = this.Use ``length``
        let run() = gl.glShaderSource.Invoke(``shader``.Value, ``count``.Value, NativePtr.ofNativeInt ``string``.Pointer, NativePtr.ofNativeInt ``length``.Pointer)
        let name() = sprintf "ShaderSource(%A, %A, %A, %A)" (``shader``.Value) (``count``.Value) (``string``.Pointer) (``length``.Pointer)
        commands.Add((name, run))
    override this.StencilFunc(``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        let run() = gl.glStencilFunc.Invoke(``func``, ``ref``, ``mask``)
        let name() = sprintf "StencilFunc(%A, %A, %A)" ``func`` ``ref`` ``mask``
        commands.Add((name, run))
    override this.StencilFunc(``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``func`` = this.Use ``func``
        let ``ref`` = this.Use ``ref``
        let ``mask`` = this.Use ``mask``
        let run() = gl.glStencilFunc.Invoke(``func``.Value, ``ref``.Value, ``mask``.Value)
        let name() = sprintf "StencilFunc(%A, %A, %A)" (``func``.Value) (``ref``.Value) (``mask``.Value)
        commands.Add((name, run))
    override this.StencilFuncSeparate(``face`` : StencilFaceDirection, ``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        let run() = gl.glStencilFuncSeparate.Invoke(``face``, ``func``, ``ref``, ``mask``)
        let name() = sprintf "StencilFuncSeparate(%A, %A, %A, %A)" ``face`` ``func`` ``ref`` ``mask``
        commands.Add((name, run))
    override this.StencilFuncSeparate(``face`` : aptr<StencilFaceDirection>, ``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``face`` = this.Use ``face``
        let ``func`` = this.Use ``func``
        let ``ref`` = this.Use ``ref``
        let ``mask`` = this.Use ``mask``
        let run() = gl.glStencilFuncSeparate.Invoke(``face``.Value, ``func``.Value, ``ref``.Value, ``mask``.Value)
        let name() = sprintf "StencilFuncSeparate(%A, %A, %A, %A)" (``face``.Value) (``func``.Value) (``ref``.Value) (``mask``.Value)
        commands.Add((name, run))
    override this.StencilMask(``mask`` : uint32) = 
        let run() = gl.glStencilMask.Invoke(``mask``)
        let name() = sprintf "StencilMask(%A)" ``mask``
        commands.Add((name, run))
    override this.StencilMask(``mask`` : aptr<uint32>) = 
        let ``mask`` = this.Use ``mask``
        let run() = gl.glStencilMask.Invoke(``mask``.Value)
        let name() = sprintf "StencilMask(%A)" (``mask``.Value)
        commands.Add((name, run))
    override this.StencilMaskSeparate(``face`` : StencilFaceDirection, ``mask`` : uint32) = 
        let run() = gl.glStencilMaskSeparate.Invoke(``face``, ``mask``)
        let name() = sprintf "StencilMaskSeparate(%A, %A)" ``face`` ``mask``
        commands.Add((name, run))
    override this.StencilMaskSeparate(``face`` : aptr<StencilFaceDirection>, ``mask`` : aptr<uint32>) = 
        let ``face`` = this.Use ``face``
        let ``mask`` = this.Use ``mask``
        let run() = gl.glStencilMaskSeparate.Invoke(``face``.Value, ``mask``.Value)
        let name() = sprintf "StencilMaskSeparate(%A, %A)" (``face``.Value) (``mask``.Value)
        commands.Add((name, run))
    override this.StencilOp(``fail`` : StencilOp, ``zfail`` : StencilOp, ``zpass`` : StencilOp) = 
        let run() = gl.glStencilOp.Invoke(``fail``, ``zfail``, ``zpass``)
        let name() = sprintf "StencilOp(%A, %A, %A)" ``fail`` ``zfail`` ``zpass``
        commands.Add((name, run))
    override this.StencilOp(``fail`` : aptr<StencilOp>, ``zfail`` : aptr<StencilOp>, ``zpass`` : aptr<StencilOp>) = 
        let ``fail`` = this.Use ``fail``
        let ``zfail`` = this.Use ``zfail``
        let ``zpass`` = this.Use ``zpass``
        let run() = gl.glStencilOp.Invoke(``fail``.Value, ``zfail``.Value, ``zpass``.Value)
        let name() = sprintf "StencilOp(%A, %A, %A)" (``fail``.Value) (``zfail``.Value) (``zpass``.Value)
        commands.Add((name, run))
    override this.StencilOpSeparate(``face`` : StencilFaceDirection, ``sfail`` : StencilOp, ``dpfail`` : StencilOp, ``dppass`` : StencilOp) = 
        let run() = gl.glStencilOpSeparate.Invoke(``face``, ``sfail``, ``dpfail``, ``dppass``)
        let name() = sprintf "StencilOpSeparate(%A, %A, %A, %A)" ``face`` ``sfail`` ``dpfail`` ``dppass``
        commands.Add((name, run))
    override this.StencilOpSeparate(``face`` : aptr<StencilFaceDirection>, ``sfail`` : aptr<StencilOp>, ``dpfail`` : aptr<StencilOp>, ``dppass`` : aptr<StencilOp>) = 
        let ``face`` = this.Use ``face``
        let ``sfail`` = this.Use ``sfail``
        let ``dpfail`` = this.Use ``dpfail``
        let ``dppass`` = this.Use ``dppass``
        let run() = gl.glStencilOpSeparate.Invoke(``face``.Value, ``sfail``.Value, ``dpfail``.Value, ``dppass``.Value)
        let name() = sprintf "StencilOpSeparate(%A, %A, %A, %A)" (``face``.Value) (``sfail``.Value) (``dpfail``.Value) (``dppass``.Value)
        commands.Add((name, run))
    override this.TexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        let run() = WrappedCommands.glTexImage2D(``target``, ``level``, ``internalformat``, ``width``, ``height``, ``border``, ``format``, ``type``, ``pixels``)
        let name() = sprintf "TexImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``internalformat`` ``width`` ``height`` ``border`` ``format`` ``type`` ``pixels``
        commands.Add((name, run))
    override this.TexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``internalformat`` = this.Use ``internalformat``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``border`` = this.Use ``border``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        let run() = WrappedCommands.glTexImage2D(``target``.Value, ``level``.Value, ``internalformat``.Value, ``width``.Value, ``height``.Value, ``border``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
        let name() = sprintf "TexImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``internalformat``.Value) (``width``.Value) (``height``.Value) (``border``.Value) (``format``.Value) (``type``.Value) (``pixels``.Value)
        commands.Add((name, run))
    override this.TexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) = 
        let run() = WrappedCommands.glTexParameterf(``target``, ``pname``, ``param``)
        let name() = sprintf "TexParameterf(%A, %A, %A)" ``target`` ``pname`` ``param``
        commands.Add((name, run))
    override this.TexParameterf(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = WrappedCommands.glTexParameterf(``target``.Value, ``pname``.Value, ``param``.Value)
        let name() = sprintf "TexParameterf(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``param``.Value)
        commands.Add((name, run))
    override this.TexParameterfv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<float32>) = 
        let run() = gl.glTexParameterfv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "TexParameterfv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.TexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<float32>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glTexParameterfv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "TexParameterfv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.TexParameteri(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : int) = 
        let run() = gl.glTexParameteri.Invoke(``target``, ``pname``, ``param``)
        let name() = sprintf "TexParameteri(%A, %A, %A)" ``target`` ``pname`` ``param``
        commands.Add((name, run))
    override this.TexParameteri(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``param`` = this.Use ``param``
        let run() = gl.glTexParameteri.Invoke(``target``.Value, ``pname``.Value, ``param``.Value)
        let name() = sprintf "TexParameteri(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``param``.Value)
        commands.Add((name, run))
    override this.TexParameteriv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<int>) = 
        let run() = gl.glTexParameteriv.Invoke(``target``, ``pname``, ``params``)
        let name() = sprintf "TexParameteriv(%A, %A, %A)" ``target`` ``pname`` ``params``
        commands.Add((name, run))
    override this.TexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``pname`` = this.Use ``pname``
        let ``params`` = this.Use ``params``
        let run() = gl.glTexParameteriv.Invoke(``target``.Value, ``pname``.Value, NativePtr.ofNativeInt ``params``.Pointer)
        let name() = sprintf "TexParameteriv(%A, %A, %A)" (``target``.Value) (``pname``.Value) (``params``.Pointer)
        commands.Add((name, run))
    override this.TexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        let run() = WrappedCommands.glTexSubImage2D(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``type``, ``pixels``)
        let name() = sprintf "TexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``width`` ``height`` ``format`` ``type`` ``pixels``
        commands.Add((name, run))
    override this.TexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``type`` = this.Use ``type``
        let ``pixels`` = this.Use ``pixels``
        let run() = WrappedCommands.glTexSubImage2D(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``type``.Value, ``pixels``.Value)
        let name() = sprintf "TexSubImage2D(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``width``.Value) (``height``.Value) (``format``.Value) (``type``.Value) (``pixels``.Value)
        commands.Add((name, run))
    override this.Uniform1f(``location`` : int, ``v0`` : float32) = 
        let run() = WrappedCommands.glUniform1f(``location``, ``v0``)
        let name() = sprintf "Uniform1f(%A, %A)" ``location`` ``v0``
        commands.Add((name, run))
    override this.Uniform1f(``location`` : aptr<int>, ``v0`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let run() = WrappedCommands.glUniform1f(``location``.Value, ``v0``.Value)
        let name() = sprintf "Uniform1f(%A, %A)" (``location``.Value) (``v0``.Value)
        commands.Add((name, run))
    override this.Uniform1fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniform1fv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform1fv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform1fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform1fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform1fv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform1i(``location`` : int, ``v0`` : int) = 
        let run() = gl.glUniform1i.Invoke(``location``, ``v0``)
        let name() = sprintf "Uniform1i(%A, %A)" ``location`` ``v0``
        commands.Add((name, run))
    override this.Uniform1i(``location`` : aptr<int>, ``v0`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let run() = gl.glUniform1i.Invoke(``location``.Value, ``v0``.Value)
        let name() = sprintf "Uniform1i(%A, %A)" (``location``.Value) (``v0``.Value)
        commands.Add((name, run))
    override this.Uniform1iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        let run() = gl.glUniform1iv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform1iv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform1iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform1iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform1iv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) = 
        let run() = WrappedCommands.glUniform2f(``location``, ``v0``, ``v1``)
        let name() = sprintf "Uniform2f(%A, %A, %A)" ``location`` ``v0`` ``v1``
        commands.Add((name, run))
    override this.Uniform2f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let run() = WrappedCommands.glUniform2f(``location``.Value, ``v0``.Value, ``v1``.Value)
        let name() = sprintf "Uniform2f(%A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value)
        commands.Add((name, run))
    override this.Uniform2fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniform2fv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform2fv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform2fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform2fv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform2i(``location`` : int, ``v0`` : int, ``v1`` : int) = 
        let run() = gl.glUniform2i.Invoke(``location``, ``v0``, ``v1``)
        let name() = sprintf "Uniform2i(%A, %A, %A)" ``location`` ``v0`` ``v1``
        commands.Add((name, run))
    override this.Uniform2i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let run() = gl.glUniform2i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value)
        let name() = sprintf "Uniform2i(%A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value)
        commands.Add((name, run))
    override this.Uniform2iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        let run() = gl.glUniform2iv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform2iv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform2iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform2iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform2iv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) = 
        let run() = WrappedCommands.glUniform3f(``location``, ``v0``, ``v1``, ``v2``)
        let name() = sprintf "Uniform3f(%A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2``
        commands.Add((name, run))
    override this.Uniform3f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let run() = WrappedCommands.glUniform3f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
        let name() = sprintf "Uniform3f(%A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value)
        commands.Add((name, run))
    override this.Uniform3fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniform3fv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform3fv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform3fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform3fv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform3i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int) = 
        let run() = gl.glUniform3i.Invoke(``location``, ``v0``, ``v1``, ``v2``)
        let name() = sprintf "Uniform3i(%A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2``
        commands.Add((name, run))
    override this.Uniform3i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let run() = gl.glUniform3i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value)
        let name() = sprintf "Uniform3i(%A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value)
        commands.Add((name, run))
    override this.Uniform3iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        let run() = gl.glUniform3iv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform3iv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform3iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform3iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform3iv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) = 
        let run() = WrappedCommands.glUniform4f(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
        let name() = sprintf "Uniform4f(%A, %A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2`` ``v3``
        commands.Add((name, run))
    override this.Uniform4f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>, ``v3`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        let run() = WrappedCommands.glUniform4f(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
        let name() = sprintf "Uniform4f(%A, %A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value) (``v3``.Value)
        commands.Add((name, run))
    override this.Uniform4fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniform4fv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform4fv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform4fv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform4fv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.Uniform4i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int, ``v3`` : int) = 
        let run() = gl.glUniform4i.Invoke(``location``, ``v0``, ``v1``, ``v2``, ``v3``)
        let name() = sprintf "Uniform4i(%A, %A, %A, %A, %A)" ``location`` ``v0`` ``v1`` ``v2`` ``v3``
        commands.Add((name, run))
    override this.Uniform4i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>, ``v3`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``v0`` = this.Use ``v0``
        let ``v1`` = this.Use ``v1``
        let ``v2`` = this.Use ``v2``
        let ``v3`` = this.Use ``v3``
        let run() = gl.glUniform4i.Invoke(``location``.Value, ``v0``.Value, ``v1``.Value, ``v2``.Value, ``v3``.Value)
        let name() = sprintf "Uniform4i(%A, %A, %A, %A, %A)" (``location``.Value) (``v0``.Value) (``v1``.Value) (``v2``.Value) (``v3``.Value)
        commands.Add((name, run))
    override this.Uniform4iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        let run() = gl.glUniform4iv.Invoke(``location``, ``count``, ``value``)
        let name() = sprintf "Uniform4iv(%A, %A, %A)" ``location`` ``count`` ``value``
        commands.Add((name, run))
    override this.Uniform4iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniform4iv.Invoke(``location``.Value, ``count``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "Uniform4iv(%A, %A, %A)" (``location``.Value) (``count``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix2fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix2fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix2fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix2fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix3fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix3fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix3fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix3fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UniformMatrix4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        let run() = gl.glUniformMatrix4fv.Invoke(``location``, ``count``, ``transpose``, ``value``)
        let name() = sprintf "UniformMatrix4fv(%A, %A, %A, %A)" ``location`` ``count`` ``transpose`` ``value``
        commands.Add((name, run))
    override this.UniformMatrix4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location`` = this.Use ``location``
        let ``count`` = this.Use ``count``
        let ``transpose`` = this.Use ``transpose``
        let ``value`` = this.Use ``value``
        let run() = gl.glUniformMatrix4fv.Invoke(``location``.Value, ``count``.Value, ``transpose``.Value, NativePtr.ofNativeInt ``value``.Pointer)
        let name() = sprintf "UniformMatrix4fv(%A, %A, %A, %A)" (``location``.Value) (``count``.Value) (``transpose``.Value) (``value``.Pointer)
        commands.Add((name, run))
    override this.UseProgram(``program`` : uint32) = 
        let run() = gl.glUseProgram.Invoke(``program``)
        let name() = sprintf "UseProgram(%A)" ``program``
        commands.Add((name, run))
    override this.UseProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let run() = gl.glUseProgram.Invoke(``program``.Value)
        let name() = sprintf "UseProgram(%A)" (``program``.Value)
        commands.Add((name, run))
    override this.ValidateProgram(``program`` : uint32) = 
        let run() = gl.glValidateProgram.Invoke(``program``)
        let name() = sprintf "ValidateProgram(%A)" ``program``
        commands.Add((name, run))
    override this.ValidateProgram(``program`` : aptr<uint32>) = 
        let ``program`` = this.Use ``program``
        let run() = gl.glValidateProgram.Invoke(``program``.Value)
        let name() = sprintf "ValidateProgram(%A)" (``program``.Value)
        commands.Add((name, run))
    override this.VertexAttrib1f(``index`` : uint32, ``x`` : float32) = 
        let run() = WrappedCommands.glVertexAttrib1f(``index``, ``x``)
        let name() = sprintf "VertexAttrib1f(%A, %A)" ``index`` ``x``
        commands.Add((name, run))
    override this.VertexAttrib1f(``index`` : aptr<uint32>, ``x`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let run() = WrappedCommands.glVertexAttrib1f(``index``.Value, ``x``.Value)
        let name() = sprintf "VertexAttrib1f(%A, %A)" (``index``.Value) (``x``.Value)
        commands.Add((name, run))
    override this.VertexAttrib1fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        let run() = gl.glVertexAttrib1fv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttrib1fv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttrib1fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttrib1fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttrib1fv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) = 
        let run() = WrappedCommands.glVertexAttrib2f(``index``, ``x``, ``y``)
        let name() = sprintf "VertexAttrib2f(%A, %A, %A)" ``index`` ``x`` ``y``
        commands.Add((name, run))
    override this.VertexAttrib2f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let run() = WrappedCommands.glVertexAttrib2f(``index``.Value, ``x``.Value, ``y``.Value)
        let name() = sprintf "VertexAttrib2f(%A, %A, %A)" (``index``.Value) (``x``.Value) (``y``.Value)
        commands.Add((name, run))
    override this.VertexAttrib2fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        let run() = gl.glVertexAttrib2fv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttrib2fv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttrib2fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttrib2fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttrib2fv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) = 
        let run() = WrappedCommands.glVertexAttrib3f(``index``, ``x``, ``y``, ``z``)
        let name() = sprintf "VertexAttrib3f(%A, %A, %A, %A)" ``index`` ``x`` ``y`` ``z``
        commands.Add((name, run))
    override this.VertexAttrib3f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let run() = WrappedCommands.glVertexAttrib3f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value)
        let name() = sprintf "VertexAttrib3f(%A, %A, %A, %A)" (``index``.Value) (``x``.Value) (``y``.Value) (``z``.Value)
        commands.Add((name, run))
    override this.VertexAttrib3fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        let run() = gl.glVertexAttrib3fv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttrib3fv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttrib3fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttrib3fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttrib3fv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) = 
        let run() = WrappedCommands.glVertexAttrib4f(``index``, ``x``, ``y``, ``z``, ``w``)
        let name() = sprintf "VertexAttrib4f(%A, %A, %A, %A, %A)" ``index`` ``x`` ``y`` ``z`` ``w``
        commands.Add((name, run))
    override this.VertexAttrib4f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>, ``w`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``z`` = this.Use ``z``
        let ``w`` = this.Use ``w``
        let run() = WrappedCommands.glVertexAttrib4f(``index``.Value, ``x``.Value, ``y``.Value, ``z``.Value, ``w``.Value)
        let name() = sprintf "VertexAttrib4f(%A, %A, %A, %A, %A)" (``index``.Value) (``x``.Value) (``y``.Value) (``z``.Value) (``w``.Value)
        commands.Add((name, run))
    override this.VertexAttrib4fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        let run() = gl.glVertexAttrib4fv.Invoke(``index``, ``v``)
        let name() = sprintf "VertexAttrib4fv(%A, %A)" ``index`` ``v``
        commands.Add((name, run))
    override this.VertexAttrib4fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index`` = this.Use ``index``
        let ``v`` = this.Use ``v``
        let run() = gl.glVertexAttrib4fv.Invoke(``index``.Value, NativePtr.ofNativeInt ``v``.Pointer)
        let name() = sprintf "VertexAttrib4fv(%A, %A)" (``index``.Value) (``v``.Pointer)
        commands.Add((name, run))
    override this.VertexAttribPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribPointerType, ``normalized`` : Boolean, ``stride`` : uint32, ``pointer`` : nativeint) = 
        let run() = gl.glVertexAttribPointer.Invoke(``index``, ``size``, ``type``, ``normalized``, ``stride``, ``pointer``)
        let name() = sprintf "VertexAttribPointer(%A, %A, %A, %A, %A, %A)" ``index`` ``size`` ``type`` ``normalized`` ``stride`` ``pointer``
        commands.Add((name, run))
    override this.VertexAttribPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribPointerType>, ``normalized`` : aptr<Boolean>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<'T6>) = 
        let ``index`` = this.Use ``index``
        let ``size`` = this.Use ``size``
        let ``type`` = this.Use ``type``
        let ``normalized`` = this.Use ``normalized``
        let ``stride`` = this.Use ``stride``
        let ``pointer`` = this.Use ``pointer``
        let run() = gl.glVertexAttribPointer.Invoke(``index``.Value, ``size``.Value, ``type``.Value, ``normalized``.Value, ``stride``.Value, ``pointer``.Pointer)
        let name() = sprintf "VertexAttribPointer(%A, %A, %A, %A, %A, %A)" (``index``.Value) (``size``.Value) (``type``.Value) (``normalized``.Value) (``stride``.Value) (``pointer``.Pointer)
        commands.Add((name, run))
    override this.Viewport(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        let run() = gl.glViewport.Invoke(``x``, ``y``, ``width``, ``height``)
        let name() = sprintf "Viewport(%A, %A, %A, %A)" ``x`` ``y`` ``width`` ``height``
        commands.Add((name, run))
    override this.Viewport(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x`` = this.Use ``x``
        let ``y`` = this.Use ``y``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let run() = gl.glViewport.Invoke(``x``.Value, ``y``.Value, ``width``.Value, ``height``.Value)
        let name() = sprintf "Viewport(%A, %A, %A, %A)" (``x``.Value) (``y``.Value) (``width``.Value) (``height``.Value)
        commands.Add((name, run))
    override this.GetBufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``dst`` : nativeint) = 
        let run() = gl.glGetBufferSubData.Invoke(``target``, ``offset``, ``size``, ``dst``)
        let name() = sprintf "GetBufferSubData(%A, %A, %A, %A)" ``target`` ``offset`` ``size`` ``dst``
        commands.Add((name, run))
    override this.GetBufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``dst`` : aptr<nativeint>) = 
        let ``target`` = this.Use ``target``
        let ``offset`` = this.Use ``offset``
        let ``size`` = this.Use ``size``
        let ``dst`` = this.Use ``dst``
        let run() = gl.glGetBufferSubData.Invoke(``target``.Value, ``offset``.Value, ``size``.Value, ``dst``.Value)
        let name() = sprintf "GetBufferSubData(%A, %A, %A, %A)" (``target``.Value) (``offset``.Value) (``size``.Value) (``dst``.Value)
        commands.Add((name, run))
    override this.MultiDrawArraysIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``bindingInfo`` : nativeint) = 
        let run() = gl.glMultiDrawArraysIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``bindingInfo``)
        let name() = sprintf "MultiDrawArraysIndirect(%A, %A, %A, %A)" ``mode`` ``indirectBuffer`` ``count`` ``bindingInfo``
        commands.Add((name, run))
    override this.MultiDrawArraysIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirectBuffer`` = this.Use ``indirectBuffer``
        let ``count`` = this.Use ``count``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        let run() = gl.glMultiDrawArraysIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``bindingInfo``.Value)
        let name() = sprintf "MultiDrawArraysIndirect(%A, %A, %A, %A)" (``mode``.Value) (``indirectBuffer``.Value) (``count``.Value) (``bindingInfo``.Value)
        commands.Add((name, run))
    override this.MultiDrawArrays(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``bindingInfo`` : nativeint) = 
        let run() = gl.glMultiDrawArrays.Invoke(``mode``, ``indirect``, ``count``, ``bindingInfo``)
        let name() = sprintf "MultiDrawArrays(%A, %A, %A, %A)" ``mode`` ``indirect`` ``count`` ``bindingInfo``
        commands.Add((name, run))
    override this.MultiDrawArrays(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirect`` = this.Use ``indirect``
        let ``count`` = this.Use ``count``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        let run() = gl.glMultiDrawArrays.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``bindingInfo``.Value)
        let name() = sprintf "MultiDrawArrays(%A, %A, %A, %A)" (``mode``.Value) (``indirect``.Value) (``count``.Value) (``bindingInfo``.Value)
        commands.Add((name, run))
    override this.MultiDrawElementsIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        let run() = gl.glMultiDrawElementsIndirect.Invoke(``mode``, ``indirectBuffer``, ``count``, ``indexType``, ``bindingInfo``)
        let name() = sprintf "MultiDrawElementsIndirect(%A, %A, %A, %A, %A)" ``mode`` ``indirectBuffer`` ``count`` ``indexType`` ``bindingInfo``
        commands.Add((name, run))
    override this.MultiDrawElementsIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirectBuffer`` = this.Use ``indirectBuffer``
        let ``count`` = this.Use ``count``
        let ``indexType`` = this.Use ``indexType``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        let run() = gl.glMultiDrawElementsIndirect.Invoke(``mode``.Value, ``indirectBuffer``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
        let name() = sprintf "MultiDrawElementsIndirect(%A, %A, %A, %A, %A)" (``mode``.Value) (``indirectBuffer``.Value) (``count``.Value) (``indexType``.Value) (``bindingInfo``.Value)
        commands.Add((name, run))
    override this.MultiDrawElements(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        let run() = gl.glMultiDrawElements.Invoke(``mode``, ``indirect``, ``count``, ``indexType``, ``bindingInfo``)
        let name() = sprintf "MultiDrawElements(%A, %A, %A, %A, %A)" ``mode`` ``indirect`` ``count`` ``indexType`` ``bindingInfo``
        commands.Add((name, run))
    override this.MultiDrawElements(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode`` = this.Use ``mode``
        let ``indirect`` = this.Use ``indirect``
        let ``count`` = this.Use ``count``
        let ``indexType`` = this.Use ``indexType``
        let ``bindingInfo`` = this.Use ``bindingInfo``
        let run() = gl.glMultiDrawElements.Invoke(``mode``.Value, ``indirect``.Value, ``count``.Value, ``indexType``.Value, ``bindingInfo``.Value)
        let name() = sprintf "MultiDrawElements(%A, %A, %A, %A, %A)" (``mode``.Value) (``indirect``.Value) (``count``.Value) (``indexType``.Value) (``bindingInfo``.Value)
        commands.Add((name, run))
    override this.Commit() = 
        let run() = gl.glCommit.Invoke()
        commands.Add(((fun () -> "Commit"), run))
    override this.TexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) = 
        let run() = WrappedCommands.glTexSubImage2DJSImage(``target``, ``level``, ``xoffset``, ``yoffset``, ``width``, ``height``, ``format``, ``typ``, ``imgHandle``)
        let name() = sprintf "TexSubImage2DJSImage(%A, %A, %A, %A, %A, %A, %A, %A, %A)" ``target`` ``level`` ``xoffset`` ``yoffset`` ``width`` ``height`` ``format`` ``typ`` ``imgHandle``
        commands.Add((name, run))
    override this.TexSubImage2DJSImage(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<int>, ``height`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``typ`` : aptr<PixelType>, ``imgHandle`` : aptr<int>) = 
        let ``target`` = this.Use ``target``
        let ``level`` = this.Use ``level``
        let ``xoffset`` = this.Use ``xoffset``
        let ``yoffset`` = this.Use ``yoffset``
        let ``width`` = this.Use ``width``
        let ``height`` = this.Use ``height``
        let ``format`` = this.Use ``format``
        let ``typ`` = this.Use ``typ``
        let ``imgHandle`` = this.Use ``imgHandle``
        let run() = WrappedCommands.glTexSubImage2DJSImage(``target``.Value, ``level``.Value, ``xoffset``.Value, ``yoffset``.Value, ``width``.Value, ``height``.Value, ``format``.Value, ``typ``.Value, ``imgHandle``.Value)
        let name() = sprintf "TexSubImage2DJSImage(%A, %A, %A, %A, %A, %A, %A, %A, %A)" (``target``.Value) (``level``.Value) (``xoffset``.Value) (``yoffset``.Value) (``width``.Value) (``height``.Value) (``format``.Value) (``typ``.Value) (``imgHandle``.Value)
        commands.Add((name, run))
