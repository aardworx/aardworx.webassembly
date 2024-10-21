// ===============================================================================
//                    AUTO GENERATED FILE (see Generator.fsx)
// ===============================================================================
namespace Aardworx.Rendering.WebGL.Streams

open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

[<AbstractClass>]
type CommandEncoder(device : Device) =
    inherit CommandEncoderBase(device)
    abstract Switch : location : aptr<int> * cases : list<int * (CommandEncoder -> unit)> * fallback : (CommandEncoder -> unit) -> unit
    abstract Call : func : aptr<nativeint> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> * arg3 : aptr<'d> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> * arg3 : aptr<'d> * arg4 : aptr<'e> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> * arg3 : aptr<'d> * arg4 : aptr<'e> * arg5 : aptr<'f> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> * arg3 : aptr<'d> * arg4 : aptr<'e> * arg5 : aptr<'f> * arg6 : aptr<'g> -> unit
    abstract Call : func : aptr<nativeint> * arg0 : aptr<'a> * arg1 : aptr<'b> * arg2 : aptr<'c> * arg3 : aptr<'d> * arg4 : aptr<'e> * arg5 : aptr<'f> * arg6 : aptr<'g> * arg7 : aptr<'h> -> unit
    abstract BeginQuery : ``target`` : QueryTarget * ``id`` : uint32 -> unit
    abstract BeginQuery : ``target`` : aptr<QueryTarget> * ``id`` : aptr<uint32> -> unit
    abstract BeginTransformFeedback : ``primitiveMode`` : PrimitiveType -> unit
    abstract BeginTransformFeedback : ``primitiveMode`` : aptr<PrimitiveType> -> unit
    abstract BindBufferBase : ``target`` : BufferTargetARB * ``index`` : uint32 * ``buffer`` : uint32 -> unit
    abstract BindBufferBase : ``target`` : aptr<BufferTargetARB> * ``index`` : aptr<uint32> * ``buffer`` : aptr<uint32> -> unit
    abstract BindBufferRange : ``target`` : BufferTargetARB * ``index`` : uint32 * ``buffer`` : uint32 * ``offset`` : nativeint * ``size`` : unativeint -> unit
    abstract BindBufferRange : ``target`` : aptr<BufferTargetARB> * ``index`` : aptr<uint32> * ``buffer`` : aptr<uint32> * ``offset`` : aptr<nativeint> * ``size`` : aptr<unativeint> -> unit
    abstract BindSampler : ``unit`` : uint32 * ``sampler`` : uint32 -> unit
    abstract BindSampler : ``unit`` : aptr<uint32> * ``sampler`` : aptr<uint32> -> unit
    abstract BindTransformFeedback : ``target`` : BindTransformFeedbackTarget * ``id`` : uint32 -> unit
    abstract BindTransformFeedback : ``target`` : aptr<BindTransformFeedbackTarget> * ``id`` : aptr<uint32> -> unit
    abstract BindVertexArray : ``array`` : uint32 -> unit
    abstract BindVertexArray : ``array`` : aptr<uint32> -> unit
    abstract BlitFramebuffer : ``srcX0`` : int * ``srcY0`` : int * ``srcX1`` : int * ``srcY1`` : int * ``dstX0`` : int * ``dstY0`` : int * ``dstX1`` : int * ``dstY1`` : int * ``mask`` : ClearBufferMask * ``filter`` : BlitFramebufferFilter -> unit
    abstract BlitFramebuffer : ``srcX0`` : aptr<int> * ``srcY0`` : aptr<int> * ``srcX1`` : aptr<int> * ``srcY1`` : aptr<int> * ``dstX0`` : aptr<int> * ``dstY0`` : aptr<int> * ``dstX1`` : aptr<int> * ``dstY1`` : aptr<int> * ``mask`` : aptr<ClearBufferMask> * ``filter`` : aptr<BlitFramebufferFilter> -> unit
    abstract ClearBufferiv : ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<int> -> unit
    abstract ClearBufferiv : ``buffer`` : aptr<BufferKind> * ``drawbuffer`` : aptr<int> * ``value`` : aptr<int> -> unit
    abstract ClearBufferuiv : ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<uint32> -> unit
    abstract ClearBufferuiv : ``buffer`` : aptr<BufferKind> * ``drawbuffer`` : aptr<int> * ``value`` : aptr<uint32> -> unit
    abstract ClearBufferfv : ``buffer`` : BufferKind * ``drawbuffer`` : int * ``value`` : nativeptr<float32> -> unit
    abstract ClearBufferfv : ``buffer`` : aptr<BufferKind> * ``drawbuffer`` : aptr<int> * ``value`` : aptr<float32> -> unit
    abstract ClearBufferfi : ``buffer`` : BufferKind * ``drawbuffer`` : int * ``depth`` : float32 * ``stencil`` : int -> unit
    abstract ClearBufferfi : ``buffer`` : aptr<BufferKind> * ``drawbuffer`` : aptr<int> * ``depth`` : aptr<float32> * ``stencil`` : aptr<int> -> unit
    abstract ClientWaitSync : ``sync`` : nativeint * ``flags`` : SyncObjectMask * ``timeout`` : uint64 * ``returnValue`` : nativeptr<GLEnum> -> unit
    abstract ClientWaitSync : ``sync`` : aptr<nativeint> * ``flags`` : aptr<SyncObjectMask> * ``timeout`` : aptr<uint64> * ``returnValue`` : aptr<GLEnum> -> unit
    abstract CompressedTexImage3D : ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``border`` : int * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    abstract CompressedTexImage3D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``depth`` : aptr<uint32> * ``border`` : aptr<int> * ``imageSize`` : aptr<uint32> * ``data`` : aptr<nativeint> -> unit
    abstract CompressedTexSubImage3D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``format`` : InternalFormat * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    abstract CompressedTexSubImage3D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``zoffset`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``depth`` : aptr<uint32> * ``format`` : aptr<InternalFormat> * ``imageSize`` : aptr<uint32> * ``data`` : aptr<nativeint> -> unit
    abstract CopyBufferSubData : ``readTarget`` : CopyBufferSubDataTarget * ``writeTarget`` : CopyBufferSubDataTarget * ``readOffset`` : nativeint * ``writeOffset`` : nativeint * ``size`` : unativeint -> unit
    abstract CopyBufferSubData : ``readTarget`` : aptr<CopyBufferSubDataTarget> * ``writeTarget`` : aptr<CopyBufferSubDataTarget> * ``readOffset`` : aptr<nativeint> * ``writeOffset`` : aptr<nativeint> * ``size`` : aptr<unativeint> -> unit
    abstract CopyTexSubImage3D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract CopyTexSubImage3D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``zoffset`` : aptr<int> * ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract DeleteQueries : ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    abstract DeleteQueries : ``n`` : aptr<uint32> * ``ids`` : aptr<uint32> -> unit
    abstract DeleteSamplers : ``count`` : uint32 * ``samplers`` : nativeptr<uint32> -> unit
    abstract DeleteSamplers : ``count`` : aptr<uint32> * ``samplers`` : aptr<uint32> -> unit
    abstract DeleteSync : ``sync`` : nativeint -> unit
    abstract DeleteSync : ``sync`` : aptr<nativeint> -> unit
    abstract DeleteTransformFeedbacks : ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    abstract DeleteTransformFeedbacks : ``n`` : aptr<uint32> * ``ids`` : aptr<uint32> -> unit
    abstract DeleteVertexArrays : ``n`` : uint32 * ``arrays`` : nativeptr<uint32> -> unit
    abstract DeleteVertexArrays : ``n`` : aptr<uint32> * ``arrays`` : aptr<uint32> -> unit
    abstract DrawArraysInstanced : ``mode`` : PrimitiveType * ``first`` : int * ``count`` : uint32 * ``instancecount`` : uint32 -> unit
    abstract DrawArraysInstanced : ``mode`` : aptr<PrimitiveType> * ``first`` : aptr<int> * ``count`` : aptr<uint32> * ``instancecount`` : aptr<uint32> -> unit
    abstract DrawBuffers : ``n`` : uint32 * ``bufs`` : nativeptr<GLEnum> -> unit
    abstract DrawBuffers : ``n`` : aptr<uint32> * ``bufs`` : aptr<nativeptr<GLEnum>> -> unit
    abstract DrawElementsInstanced : ``mode`` : PrimitiveType * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint * ``instancecount`` : uint32 -> unit
    abstract DrawElementsInstanced : ``mode`` : aptr<PrimitiveType> * ``count`` : aptr<uint32> * ``type`` : aptr<DrawElementsType> * ``indices`` : aptr<nativeint> * ``instancecount`` : aptr<uint32> -> unit
    abstract DrawRangeElements : ``mode`` : PrimitiveType * ``start`` : uint32 * ``end`` : uint32 * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint -> unit
    abstract DrawRangeElements : ``mode`` : aptr<PrimitiveType> * ``start`` : aptr<uint32> * ``end`` : aptr<uint32> * ``count`` : aptr<uint32> * ``type`` : aptr<DrawElementsType> * ``indices`` : aptr<nativeint> -> unit
    abstract EndQuery : ``target`` : QueryTarget -> unit
    abstract EndQuery : ``target`` : aptr<QueryTarget> -> unit
    abstract EndTransformFeedback : unit -> unit
    abstract FenceSync : ``condition`` : SyncCondition * ``flags`` : SyncBehaviorFlags * ``returnValue`` : nativeptr<nativeint> -> unit
    abstract FenceSync : ``condition`` : aptr<SyncCondition> * ``flags`` : aptr<SyncBehaviorFlags> * ``returnValue`` : aptr<nativeint> -> unit
    abstract FramebufferTextureLayer : ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``texture`` : uint32 * ``level`` : int * ``layer`` : int -> unit
    abstract FramebufferTextureLayer : ``target`` : aptr<FramebufferTarget> * ``attachment`` : aptr<FramebufferAttachment> * ``texture`` : aptr<uint32> * ``level`` : aptr<int> * ``layer`` : aptr<int> -> unit
    abstract GenQueries : ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    abstract GenQueries : ``n`` : aptr<uint32> * ``ids`` : aptr<uint32> -> unit
    abstract GenSamplers : ``count`` : uint32 * ``samplers`` : nativeptr<uint32> -> unit
    abstract GenSamplers : ``count`` : aptr<uint32> * ``samplers`` : aptr<uint32> -> unit
    abstract GenTransformFeedbacks : ``n`` : uint32 * ``ids`` : nativeptr<uint32> -> unit
    abstract GenTransformFeedbacks : ``n`` : aptr<uint32> * ``ids`` : aptr<uint32> -> unit
    abstract GenVertexArrays : ``n`` : uint32 * ``arrays`` : nativeptr<uint32> -> unit
    abstract GenVertexArrays : ``n`` : aptr<uint32> * ``arrays`` : aptr<uint32> -> unit
    abstract GetActiveUniformBlockiv : ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``pname`` : UniformBlockPName * ``params`` : nativeptr<int> -> unit
    abstract GetActiveUniformBlockiv : ``program`` : aptr<uint32> * ``uniformBlockIndex`` : aptr<uint32> * ``pname`` : aptr<UniformBlockPName> * ``params`` : aptr<int> -> unit
    abstract GetActiveUniformBlockName : ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``uniformBlockName`` : nativeptr<uint8> -> unit
    abstract GetActiveUniformBlockName : ``program`` : aptr<uint32> * ``uniformBlockIndex`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``uniformBlockName`` : aptr<uint8> -> unit
    abstract GetActiveUniformsiv : ``program`` : uint32 * ``uniformCount`` : uint32 * ``uniformIndices`` : nativeptr<uint32> * ``pname`` : UniformPName * ``params`` : nativeptr<int> -> unit
    abstract GetActiveUniformsiv : ``program`` : aptr<uint32> * ``uniformCount`` : aptr<uint32> * ``uniformIndices`` : aptr<uint32> * ``pname`` : aptr<UniformPName> * ``params`` : aptr<int> -> unit
    abstract GetBufferParameteri64v : ``target`` : BufferTargetARB * ``pname`` : BufferPNameARB * ``params`` : nativeptr<int64> -> unit
    abstract GetBufferParameteri64v : ``target`` : aptr<BufferTargetARB> * ``pname`` : aptr<BufferPNameARB> * ``params`` : aptr<int64> -> unit
    abstract GetFragDataLocation : ``program`` : uint32 * ``name`` : nativeptr<uint8> * ``returnValue`` : nativeptr<int> -> unit
    abstract GetFragDataLocation : ``program`` : aptr<uint32> * ``name`` : aptr<uint8> * ``returnValue`` : aptr<int> -> unit
    abstract GetIntegeri_v : ``target`` : GetPName * ``index`` : uint32 * ``data`` : nativeptr<int> -> unit
    abstract GetIntegeri_v : ``target`` : aptr<GetPName> * ``index`` : aptr<uint32> * ``data`` : aptr<int> -> unit
    abstract GetInteger64v : ``pname`` : GetPName * ``data`` : nativeptr<int64> -> unit
    abstract GetInteger64v : ``pname`` : aptr<GetPName> * ``data`` : aptr<int64> -> unit
    abstract GetInteger64i_v : ``target`` : GetPName * ``index`` : uint32 * ``data`` : nativeptr<int64> -> unit
    abstract GetInteger64i_v : ``target`` : aptr<GetPName> * ``index`` : aptr<uint32> * ``data`` : aptr<int64> -> unit
    abstract GetInternalformativ : ``target`` : TextureTarget * ``internalformat`` : InternalFormat * ``pname`` : InternalFormatPName * ``count`` : uint32 * ``params`` : nativeptr<int> -> unit
    abstract GetInternalformativ : ``target`` : aptr<TextureTarget> * ``internalformat`` : aptr<InternalFormat> * ``pname`` : aptr<InternalFormatPName> * ``count`` : aptr<uint32> * ``params`` : aptr<int> -> unit
    abstract GetProgramBinary : ``program`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``binaryFormat`` : nativeptr<GLEnum> * ``binary`` : nativeint -> unit
    abstract GetProgramBinary : ``program`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``binaryFormat`` : aptr<GLEnum> * ``binary`` : aptr<'T5> -> unit
    abstract GetQueryiv : ``target`` : QueryTarget * ``pname`` : QueryParameterName * ``params`` : nativeptr<int> -> unit
    abstract GetQueryiv : ``target`` : aptr<QueryTarget> * ``pname`` : aptr<QueryParameterName> * ``params`` : aptr<int> -> unit
    abstract GetQueryObjectuiv : ``id`` : uint32 * ``pname`` : QueryObjectParameterName * ``params`` : nativeptr<uint32> -> unit
    abstract GetQueryObjectuiv : ``id`` : aptr<uint32> * ``pname`` : aptr<QueryObjectParameterName> * ``params`` : aptr<uint32> -> unit
    abstract GetSamplerParameteriv : ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``params`` : nativeptr<int> -> unit
    abstract GetSamplerParameteriv : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterI> * ``params`` : aptr<int> -> unit
    abstract GetSamplerParameterfv : ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``params`` : nativeptr<float32> -> unit
    abstract GetSamplerParameterfv : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterF> * ``params`` : aptr<float32> -> unit
    abstract GetStringi : ``name`` : StringName * ``index`` : uint32 * ``returnValue`` : nativeptr<nativeptr<uint8>> -> unit
    abstract GetStringi : ``name`` : aptr<StringName> * ``index`` : aptr<uint32> * ``returnValue`` : aptr<nativeptr<uint8>> -> unit
    abstract GetSynciv : ``sync`` : nativeint * ``pname`` : SyncParameterName * ``count`` : uint32 * ``length`` : nativeptr<uint32> * ``values`` : nativeptr<int> -> unit
    abstract GetSynciv : ``sync`` : aptr<nativeint> * ``pname`` : aptr<SyncParameterName> * ``count`` : aptr<uint32> * ``length`` : aptr<uint32> * ``values`` : aptr<int> -> unit
    abstract GetTransformFeedbackVarying : ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<uint32> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    abstract GetTransformFeedbackVarying : ``program`` : aptr<uint32> * ``index`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``size`` : aptr<uint32> * ``type`` : aptr<GLEnum> * ``name`` : aptr<uint8> -> unit
    abstract GetUniformuiv : ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<uint32> -> unit
    abstract GetUniformuiv : ``program`` : aptr<uint32> * ``location`` : aptr<int> * ``params`` : aptr<uint32> -> unit
    abstract GetUniformBlockIndex : ``program`` : uint32 * ``uniformBlockName`` : nativeptr<uint8> * ``returnValue`` : nativeptr<uint32> -> unit
    abstract GetUniformBlockIndex : ``program`` : aptr<uint32> * ``uniformBlockName`` : aptr<uint8> * ``returnValue`` : aptr<uint32> -> unit
    abstract GetUniformIndices : ``program`` : uint32 * ``uniformCount`` : uint32 * ``uniformNames`` : nativeptr<nativeptr<uint8>> * ``uniformIndices`` : nativeptr<uint32> -> unit
    abstract GetUniformIndices : ``program`` : aptr<uint32> * ``uniformCount`` : aptr<uint32> * ``uniformNames`` : aptr<nativeptr<uint8>> * ``uniformIndices`` : aptr<uint32> -> unit
    abstract GetVertexAttribIiv : ``index`` : uint32 * ``pname`` : VertexAttribEnum * ``params`` : nativeptr<int> -> unit
    abstract GetVertexAttribIiv : ``index`` : aptr<uint32> * ``pname`` : aptr<VertexAttribEnum> * ``params`` : aptr<int> -> unit
    abstract GetVertexAttribIuiv : ``index`` : uint32 * ``pname`` : VertexAttribEnum * ``params`` : nativeptr<uint32> -> unit
    abstract GetVertexAttribIuiv : ``index`` : aptr<uint32> * ``pname`` : aptr<VertexAttribEnum> * ``params`` : aptr<uint32> -> unit
    abstract InvalidateFramebuffer : ``target`` : FramebufferTarget * ``numAttachments`` : uint32 * ``attachments`` : nativeptr<GLEnum> -> unit
    abstract InvalidateFramebuffer : ``target`` : aptr<FramebufferTarget> * ``numAttachments`` : aptr<uint32> * ``attachments`` : aptr<GLEnum> -> unit
    abstract InvalidateSubFramebuffer : ``target`` : FramebufferTarget * ``numAttachments`` : uint32 * ``attachments`` : nativeptr<GLEnum> * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract InvalidateSubFramebuffer : ``target`` : aptr<FramebufferTarget> * ``numAttachments`` : aptr<uint32> * ``attachments`` : aptr<GLEnum> * ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract IsQuery : ``id`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsQuery : ``id`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsSampler : ``sampler`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsSampler : ``sampler`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsSync : ``sync`` : nativeint * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsSync : ``sync`` : aptr<nativeint> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsTransformFeedback : ``id`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsTransformFeedback : ``id`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsVertexArray : ``array`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsVertexArray : ``array`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract PauseTransformFeedback : unit -> unit
    abstract ProgramBinary : ``program`` : uint32 * ``binaryFormat`` : GLEnum * ``binary`` : nativeint * ``length`` : uint32 -> unit
    abstract ProgramBinary : ``program`` : aptr<uint32> * ``binaryFormat`` : aptr<GLEnum> * ``binary`` : aptr<'T3> * ``length`` : aptr<uint32> -> unit
    abstract ProgramParameteri : ``program`` : uint32 * ``pname`` : ProgramParameterPName * ``value`` : int -> unit
    abstract ProgramParameteri : ``program`` : aptr<uint32> * ``pname`` : aptr<ProgramParameterPName> * ``value`` : aptr<int> -> unit
    abstract ReadBuffer : ``src`` : ReadBufferMode -> unit
    abstract ReadBuffer : ``src`` : aptr<ReadBufferMode> -> unit
    abstract RenderbufferStorageMultisample : ``target`` : RenderbufferTarget * ``samples`` : uint32 * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract RenderbufferStorageMultisample : ``target`` : aptr<RenderbufferTarget> * ``samples`` : aptr<uint32> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract ResumeTransformFeedback : unit -> unit
    abstract SamplerParameteri : ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``param`` : int -> unit
    abstract SamplerParameteri : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterI> * ``param`` : aptr<int> -> unit
    abstract SamplerParameteriv : ``sampler`` : uint32 * ``pname`` : SamplerParameterI * ``param`` : nativeptr<int> -> unit
    abstract SamplerParameteriv : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterI> * ``param`` : aptr<int> -> unit
    abstract SamplerParameterf : ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``param`` : float32 -> unit
    abstract SamplerParameterf : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterF> * ``param`` : aptr<float32> -> unit
    abstract SamplerParameterfv : ``sampler`` : uint32 * ``pname`` : SamplerParameterF * ``param`` : nativeptr<float32> -> unit
    abstract SamplerParameterfv : ``sampler`` : aptr<uint32> * ``pname`` : aptr<SamplerParameterF> * ``param`` : aptr<float32> -> unit
    abstract TexImage3D : ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``border`` : int * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    abstract TexImage3D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``depth`` : aptr<uint32> * ``border`` : aptr<int> * ``format`` : aptr<PixelFormat> * ``type`` : aptr<PixelType> * ``pixels`` : aptr<nativeint> -> unit
    abstract TexStorage2D : ``target`` : TextureTarget * ``levels`` : uint32 * ``internalformat`` : SizedInternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract TexStorage2D : ``target`` : aptr<TextureTarget> * ``levels`` : aptr<uint32> * ``internalformat`` : aptr<SizedInternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract TexStorage3D : ``target`` : TextureTarget * ``levels`` : uint32 * ``internalformat`` : SizedInternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 -> unit
    abstract TexStorage3D : ``target`` : aptr<TextureTarget> * ``levels`` : aptr<uint32> * ``internalformat`` : aptr<SizedInternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``depth`` : aptr<uint32> -> unit
    abstract TexSubImage3D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``zoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``depth`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    abstract TexSubImage3D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``zoffset`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``depth`` : aptr<uint32> * ``format`` : aptr<PixelFormat> * ``type`` : aptr<PixelType> * ``pixels`` : aptr<nativeint> -> unit
    abstract TransformFeedbackVaryings : ``program`` : uint32 * ``count`` : uint32 * ``varyings`` : nativeptr<nativeptr<uint8>> * ``bufferMode`` : TransformFeedbackBufferMode -> unit
    abstract TransformFeedbackVaryings : ``program`` : aptr<uint32> * ``count`` : aptr<uint32> * ``varyings`` : aptr<nativeptr<uint8>> * ``bufferMode`` : aptr<TransformFeedbackBufferMode> -> unit
    abstract Uniform1ui : ``location`` : int * ``v0`` : uint32 -> unit
    abstract Uniform1ui : ``location`` : aptr<int> * ``v0`` : aptr<uint32> -> unit
    abstract Uniform1uiv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    abstract Uniform1uiv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<uint32> -> unit
    abstract Uniform2ui : ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 -> unit
    abstract Uniform2ui : ``location`` : aptr<int> * ``v0`` : aptr<uint32> * ``v1`` : aptr<uint32> -> unit
    abstract Uniform2uiv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    abstract Uniform2uiv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<uint32> -> unit
    abstract Uniform3ui : ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 * ``v2`` : uint32 -> unit
    abstract Uniform3ui : ``location`` : aptr<int> * ``v0`` : aptr<uint32> * ``v1`` : aptr<uint32> * ``v2`` : aptr<uint32> -> unit
    abstract Uniform3uiv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    abstract Uniform3uiv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<uint32> -> unit
    abstract Uniform4ui : ``location`` : int * ``v0`` : uint32 * ``v1`` : uint32 * ``v2`` : uint32 * ``v3`` : uint32 -> unit
    abstract Uniform4ui : ``location`` : aptr<int> * ``v0`` : aptr<uint32> * ``v1`` : aptr<uint32> * ``v2`` : aptr<uint32> * ``v3`` : aptr<uint32> -> unit
    abstract Uniform4uiv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<uint32> -> unit
    abstract Uniform4uiv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<uint32> -> unit
    abstract UniformBlockBinding : ``program`` : uint32 * ``uniformBlockIndex`` : uint32 * ``uniformBlockBinding`` : uint32 -> unit
    abstract UniformBlockBinding : ``program`` : aptr<uint32> * ``uniformBlockIndex`` : aptr<uint32> * ``uniformBlockBinding`` : aptr<uint32> -> unit
    abstract UniformMatrix2x3fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix2x3fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix2x4fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix2x4fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix3x2fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix3x2fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix3x4fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix3x4fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix4x2fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix4x2fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix4x3fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix4x3fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract VertexAttribDivisor : ``index`` : uint32 * ``divisor`` : uint32 -> unit
    abstract VertexAttribDivisor : ``index`` : aptr<uint32> * ``divisor`` : aptr<uint32> -> unit
    abstract VertexAttribI4i : ``index`` : uint32 * ``x`` : int * ``y`` : int * ``z`` : int * ``w`` : int -> unit
    abstract VertexAttribI4i : ``index`` : aptr<uint32> * ``x`` : aptr<int> * ``y`` : aptr<int> * ``z`` : aptr<int> * ``w`` : aptr<int> -> unit
    abstract VertexAttribI4ui : ``index`` : uint32 * ``x`` : uint32 * ``y`` : uint32 * ``z`` : uint32 * ``w`` : uint32 -> unit
    abstract VertexAttribI4ui : ``index`` : aptr<uint32> * ``x`` : aptr<uint32> * ``y`` : aptr<uint32> * ``z`` : aptr<uint32> * ``w`` : aptr<uint32> -> unit
    abstract VertexAttribI4iv : ``index`` : uint32 * ``v`` : nativeptr<int> -> unit
    abstract VertexAttribI4iv : ``index`` : aptr<uint32> * ``v`` : aptr<int> -> unit
    abstract VertexAttribI4uiv : ``index`` : uint32 * ``v`` : nativeptr<uint32> -> unit
    abstract VertexAttribI4uiv : ``index`` : aptr<uint32> * ``v`` : aptr<uint32> -> unit
    abstract VertexAttribIPointer : ``index`` : uint32 * ``size`` : int * ``type`` : VertexAttribIType * ``stride`` : uint32 * ``pointer`` : nativeint -> unit
    abstract VertexAttribIPointer : ``index`` : aptr<uint32> * ``size`` : aptr<int> * ``type`` : aptr<VertexAttribIType> * ``stride`` : aptr<uint32> * ``pointer`` : aptr<'T5> -> unit
    abstract WaitSync : ``sync`` : nativeint * ``flags`` : SyncBehaviorFlags * ``timeout`` : uint64 -> unit
    abstract WaitSync : ``sync`` : aptr<nativeint> * ``flags`` : aptr<SyncBehaviorFlags> * ``timeout`` : aptr<uint64> -> unit
    abstract ActiveTexture : ``texture`` : TextureUnit -> unit
    abstract ActiveTexture : ``texture`` : aptr<TextureUnit> -> unit
    abstract AttachShader : ``program`` : uint32 * ``shader`` : uint32 -> unit
    abstract AttachShader : ``program`` : aptr<uint32> * ``shader`` : aptr<uint32> -> unit
    abstract BindAttribLocation : ``program`` : uint32 * ``index`` : uint32 * ``name`` : nativeptr<uint8> -> unit
    abstract BindAttribLocation : ``program`` : aptr<uint32> * ``index`` : aptr<uint32> * ``name`` : aptr<uint8> -> unit
    abstract BindBuffer : ``target`` : BufferTargetARB * ``buffer`` : uint32 -> unit
    abstract BindBuffer : ``target`` : aptr<BufferTargetARB> * ``buffer`` : aptr<uint32> -> unit
    abstract BindFramebuffer : ``target`` : FramebufferTarget * ``framebuffer`` : uint32 -> unit
    abstract BindFramebuffer : ``target`` : aptr<FramebufferTarget> * ``framebuffer`` : aptr<uint32> -> unit
    abstract BindRenderbuffer : ``target`` : RenderbufferTarget * ``renderbuffer`` : uint32 -> unit
    abstract BindRenderbuffer : ``target`` : aptr<RenderbufferTarget> * ``renderbuffer`` : aptr<uint32> -> unit
    abstract BindTexture : ``target`` : TextureTarget * ``texture`` : uint32 -> unit
    abstract BindTexture : ``target`` : aptr<TextureTarget> * ``texture`` : aptr<uint32> -> unit
    abstract BlendColor : ``red`` : float32 * ``green`` : float32 * ``blue`` : float32 * ``alpha`` : float32 -> unit
    abstract BlendColor : ``red`` : aptr<float32> * ``green`` : aptr<float32> * ``blue`` : aptr<float32> * ``alpha`` : aptr<float32> -> unit
    abstract BlendEquation : ``mode`` : BlendEquationModeEXT -> unit
    abstract BlendEquation : ``mode`` : aptr<BlendEquationModeEXT> -> unit
    abstract BlendEquationSeparate : ``modeRGB`` : BlendEquationModeEXT * ``modeAlpha`` : BlendEquationModeEXT -> unit
    abstract BlendEquationSeparate : ``modeRGB`` : aptr<BlendEquationModeEXT> * ``modeAlpha`` : aptr<BlendEquationModeEXT> -> unit
    abstract BlendFunc : ``sfactor`` : BlendingFactor * ``dfactor`` : BlendingFactor -> unit
    abstract BlendFunc : ``sfactor`` : aptr<BlendingFactor> * ``dfactor`` : aptr<BlendingFactor> -> unit
    abstract BlendFuncSeparate : ``sfactorRGB`` : BlendingFactor * ``dfactorRGB`` : BlendingFactor * ``sfactorAlpha`` : BlendingFactor * ``dfactorAlpha`` : BlendingFactor -> unit
    abstract BlendFuncSeparate : ``sfactorRGB`` : aptr<BlendingFactor> * ``dfactorRGB`` : aptr<BlendingFactor> * ``sfactorAlpha`` : aptr<BlendingFactor> * ``dfactorAlpha`` : aptr<BlendingFactor> -> unit
    abstract BufferData : ``target`` : BufferTargetARB * ``size`` : unativeint * ``data`` : nativeint * ``usage`` : BufferUsageARB -> unit
    abstract BufferData : ``target`` : aptr<BufferTargetARB> * ``size`` : aptr<unativeint> * ``data`` : aptr<nativeint> * ``usage`` : aptr<BufferUsageARB> -> unit
    abstract BufferSubData : ``target`` : BufferTargetARB * ``offset`` : nativeint * ``size`` : unativeint * ``data`` : nativeint -> unit
    abstract BufferSubData : ``target`` : aptr<BufferTargetARB> * ``offset`` : aptr<nativeint> * ``size`` : aptr<unativeint> * ``data`` : aptr<nativeint> -> unit
    abstract CheckFramebufferStatus : ``target`` : FramebufferTarget * ``returnValue`` : nativeptr<GLEnum> -> unit
    abstract CheckFramebufferStatus : ``target`` : aptr<FramebufferTarget> * ``returnValue`` : aptr<GLEnum> -> unit
    abstract Clear : ``mask`` : ClearBufferMask -> unit
    abstract Clear : ``mask`` : aptr<ClearBufferMask> -> unit
    abstract ClearColor : ``red`` : float32 * ``green`` : float32 * ``blue`` : float32 * ``alpha`` : float32 -> unit
    abstract ClearColor : ``red`` : aptr<float32> * ``green`` : aptr<float32> * ``blue`` : aptr<float32> * ``alpha`` : aptr<float32> -> unit
    abstract ClearDepthf : ``d`` : float32 -> unit
    abstract ClearDepthf : ``d`` : aptr<float32> -> unit
    abstract ClearStencil : ``s`` : int -> unit
    abstract ClearStencil : ``s`` : aptr<int> -> unit
    abstract ColorMask : ``red`` : Boolean * ``green`` : Boolean * ``blue`` : Boolean * ``alpha`` : Boolean -> unit
    abstract ColorMask : ``red`` : aptr<Boolean> * ``green`` : aptr<Boolean> * ``blue`` : aptr<Boolean> * ``alpha`` : aptr<Boolean> -> unit
    abstract CompileShader : ``shader`` : uint32 -> unit
    abstract CompileShader : ``shader`` : aptr<uint32> -> unit
    abstract CompressedTexImage2D : ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    abstract CompressedTexImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``border`` : aptr<int> * ``imageSize`` : aptr<uint32> * ``data`` : aptr<nativeint> -> unit
    abstract CompressedTexSubImage2D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : InternalFormat * ``imageSize`` : uint32 * ``data`` : nativeint -> unit
    abstract CompressedTexSubImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``format`` : aptr<InternalFormat> * ``imageSize`` : aptr<uint32> * ``data`` : aptr<nativeint> -> unit
    abstract CopyTexImage2D : ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int -> unit
    abstract CopyTexImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``internalformat`` : aptr<InternalFormat> * ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``border`` : aptr<int> -> unit
    abstract CopyTexSubImage2D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract CopyTexSubImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract CreateProgram : ``returnValue`` : nativeptr<uint32> -> unit
    abstract CreateProgram : ``returnValue`` : aptr<uint32> -> unit
    abstract CreateShader : ``type`` : ShaderType * ``returnValue`` : nativeptr<uint32> -> unit
    abstract CreateShader : ``type`` : aptr<ShaderType> * ``returnValue`` : aptr<uint32> -> unit
    abstract CullFace : ``mode`` : CullFaceMode -> unit
    abstract CullFace : ``mode`` : aptr<CullFaceMode> -> unit
    abstract DeleteBuffers : ``n`` : uint32 * ``buffers`` : nativeptr<uint32> -> unit
    abstract DeleteBuffers : ``n`` : aptr<uint32> * ``buffers`` : aptr<uint32> -> unit
    abstract DeleteFramebuffers : ``n`` : uint32 * ``framebuffers`` : nativeptr<uint32> -> unit
    abstract DeleteFramebuffers : ``n`` : aptr<uint32> * ``framebuffers`` : aptr<uint32> -> unit
    abstract DeleteProgram : ``program`` : uint32 -> unit
    abstract DeleteProgram : ``program`` : aptr<uint32> -> unit
    abstract DeleteRenderbuffers : ``n`` : uint32 * ``renderbuffers`` : nativeptr<uint32> -> unit
    abstract DeleteRenderbuffers : ``n`` : aptr<uint32> * ``renderbuffers`` : aptr<uint32> -> unit
    abstract DeleteShader : ``shader`` : uint32 -> unit
    abstract DeleteShader : ``shader`` : aptr<uint32> -> unit
    abstract DeleteTextures : ``n`` : uint32 * ``textures`` : nativeptr<uint32> -> unit
    abstract DeleteTextures : ``n`` : aptr<uint32> * ``textures`` : aptr<uint32> -> unit
    abstract DepthFunc : ``func`` : DepthFunction -> unit
    abstract DepthFunc : ``func`` : aptr<DepthFunction> -> unit
    abstract DepthMask : ``flag`` : Boolean -> unit
    abstract DepthMask : ``flag`` : aptr<Boolean> -> unit
    abstract DepthRangef : ``n`` : float32 * ``f`` : float32 -> unit
    abstract DepthRangef : ``n`` : aptr<float32> * ``f`` : aptr<float32> -> unit
    abstract DetachShader : ``program`` : uint32 * ``shader`` : uint32 -> unit
    abstract DetachShader : ``program`` : aptr<uint32> * ``shader`` : aptr<uint32> -> unit
    abstract Disable : ``cap`` : EnableCap -> unit
    abstract Disable : ``cap`` : aptr<EnableCap> -> unit
    abstract DisableVertexAttribArray : ``index`` : uint32 -> unit
    abstract DisableVertexAttribArray : ``index`` : aptr<uint32> -> unit
    abstract DrawArrays : ``mode`` : PrimitiveType * ``first`` : int * ``count`` : uint32 -> unit
    abstract DrawArrays : ``mode`` : aptr<PrimitiveType> * ``first`` : aptr<int> * ``count`` : aptr<uint32> -> unit
    abstract DrawElements : ``mode`` : PrimitiveType * ``count`` : uint32 * ``type`` : DrawElementsType * ``indices`` : nativeint -> unit
    abstract DrawElements : ``mode`` : aptr<PrimitiveType> * ``count`` : aptr<uint32> * ``type`` : aptr<DrawElementsType> * ``indices`` : aptr<nativeint> -> unit
    abstract Enable : ``cap`` : EnableCap -> unit
    abstract Enable : ``cap`` : aptr<EnableCap> -> unit
    abstract EnableVertexAttribArray : ``index`` : uint32 -> unit
    abstract EnableVertexAttribArray : ``index`` : aptr<uint32> -> unit
    abstract Finish : unit -> unit
    abstract Flush : unit -> unit
    abstract FramebufferRenderbuffer : ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``renderbuffertarget`` : RenderbufferTarget * ``renderbuffer`` : uint32 -> unit
    abstract FramebufferRenderbuffer : ``target`` : aptr<FramebufferTarget> * ``attachment`` : aptr<FramebufferAttachment> * ``renderbuffertarget`` : aptr<RenderbufferTarget> * ``renderbuffer`` : aptr<uint32> -> unit
    abstract FramebufferTexture2D : ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``textarget`` : TextureTarget * ``texture`` : uint32 * ``level`` : int -> unit
    abstract FramebufferTexture2D : ``target`` : aptr<FramebufferTarget> * ``attachment`` : aptr<FramebufferAttachment> * ``textarget`` : aptr<TextureTarget> * ``texture`` : aptr<uint32> * ``level`` : aptr<int> -> unit
    abstract FrontFace : ``mode`` : FrontFaceDirection -> unit
    abstract FrontFace : ``mode`` : aptr<FrontFaceDirection> -> unit
    abstract GenBuffers : ``n`` : uint32 * ``buffers`` : nativeptr<uint32> -> unit
    abstract GenBuffers : ``n`` : aptr<uint32> * ``buffers`` : aptr<uint32> -> unit
    abstract GenerateMipmap : ``target`` : TextureTarget -> unit
    abstract GenerateMipmap : ``target`` : aptr<TextureTarget> -> unit
    abstract GenFramebuffers : ``n`` : uint32 * ``framebuffers`` : nativeptr<uint32> -> unit
    abstract GenFramebuffers : ``n`` : aptr<uint32> * ``framebuffers`` : aptr<uint32> -> unit
    abstract GenRenderbuffers : ``n`` : uint32 * ``renderbuffers`` : nativeptr<uint32> -> unit
    abstract GenRenderbuffers : ``n`` : aptr<uint32> * ``renderbuffers`` : aptr<uint32> -> unit
    abstract GenTextures : ``n`` : uint32 * ``textures`` : nativeptr<uint32> -> unit
    abstract GenTextures : ``n`` : aptr<uint32> * ``textures`` : aptr<uint32> -> unit
    abstract GetActiveAttrib : ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<int> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    abstract GetActiveAttrib : ``program`` : aptr<uint32> * ``index`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``size`` : aptr<int> * ``type`` : aptr<GLEnum> * ``name`` : aptr<uint8> -> unit
    abstract GetActiveUniform : ``program`` : uint32 * ``index`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``size`` : nativeptr<int> * ``type`` : nativeptr<GLEnum> * ``name`` : nativeptr<uint8> -> unit
    abstract GetActiveUniform : ``program`` : aptr<uint32> * ``index`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``size`` : aptr<int> * ``type`` : aptr<GLEnum> * ``name`` : aptr<uint8> -> unit
    abstract GetAttachedShaders : ``program`` : uint32 * ``maxCount`` : uint32 * ``count`` : nativeptr<uint32> * ``shaders`` : nativeptr<uint32> -> unit
    abstract GetAttachedShaders : ``program`` : aptr<uint32> * ``maxCount`` : aptr<uint32> * ``count`` : aptr<uint32> * ``shaders`` : aptr<uint32> -> unit
    abstract GetAttribLocation : ``program`` : uint32 * ``name`` : nativeptr<uint8> * ``returnValue`` : nativeptr<int> -> unit
    abstract GetAttribLocation : ``program`` : aptr<uint32> * ``name`` : aptr<uint8> * ``returnValue`` : aptr<int> -> unit
    abstract GetBooleanv : ``pname`` : GetPName * ``data`` : nativeptr<Boolean> -> unit
    abstract GetBooleanv : ``pname`` : aptr<GetPName> * ``data`` : aptr<Boolean> -> unit
    abstract GetBufferParameteriv : ``target`` : BufferTargetARB * ``pname`` : BufferPNameARB * ``params`` : nativeptr<int> -> unit
    abstract GetBufferParameteriv : ``target`` : aptr<BufferTargetARB> * ``pname`` : aptr<BufferPNameARB> * ``params`` : aptr<int> -> unit
    abstract GetError : ``returnValue`` : nativeptr<GLEnum> -> unit
    abstract GetError : ``returnValue`` : aptr<GLEnum> -> unit
    abstract GetFloatv : ``pname`` : GetPName * ``data`` : nativeptr<float32> -> unit
    abstract GetFloatv : ``pname`` : aptr<GetPName> * ``data`` : aptr<float32> -> unit
    abstract GetFramebufferAttachmentParameteriv : ``target`` : FramebufferTarget * ``attachment`` : FramebufferAttachment * ``pname`` : FramebufferAttachmentParameterName * ``params`` : nativeptr<int> -> unit
    abstract GetFramebufferAttachmentParameteriv : ``target`` : aptr<FramebufferTarget> * ``attachment`` : aptr<FramebufferAttachment> * ``pname`` : aptr<FramebufferAttachmentParameterName> * ``params`` : aptr<int> -> unit
    abstract GetIntegerv : ``pname`` : GetPName * ``data`` : nativeptr<int> -> unit
    abstract GetIntegerv : ``pname`` : aptr<GetPName> * ``data`` : aptr<int> -> unit
    abstract GetProgramiv : ``program`` : uint32 * ``pname`` : ProgramPropertyARB * ``params`` : nativeptr<int> -> unit
    abstract GetProgramiv : ``program`` : aptr<uint32> * ``pname`` : aptr<ProgramPropertyARB> * ``params`` : aptr<int> -> unit
    abstract GetProgramInfoLog : ``program`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``infoLog`` : nativeptr<uint8> -> unit
    abstract GetProgramInfoLog : ``program`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``infoLog`` : aptr<uint8> -> unit
    abstract GetRenderbufferParameteriv : ``target`` : RenderbufferTarget * ``pname`` : RenderbufferParameterName * ``params`` : nativeptr<int> -> unit
    abstract GetRenderbufferParameteriv : ``target`` : aptr<RenderbufferTarget> * ``pname`` : aptr<RenderbufferParameterName> * ``params`` : aptr<int> -> unit
    abstract GetShaderiv : ``shader`` : uint32 * ``pname`` : ShaderParameterName * ``params`` : nativeptr<int> -> unit
    abstract GetShaderiv : ``shader`` : aptr<uint32> * ``pname`` : aptr<ShaderParameterName> * ``params`` : aptr<int> -> unit
    abstract GetShaderInfoLog : ``shader`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``infoLog`` : nativeptr<uint8> -> unit
    abstract GetShaderInfoLog : ``shader`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``infoLog`` : aptr<uint8> -> unit
    abstract GetShaderPrecisionFormat : ``shadertype`` : ShaderType * ``precisiontype`` : PrecisionType * ``range`` : nativeptr<int> * ``precision`` : nativeptr<int> -> unit
    abstract GetShaderPrecisionFormat : ``shadertype`` : aptr<ShaderType> * ``precisiontype`` : aptr<PrecisionType> * ``range`` : aptr<int> * ``precision`` : aptr<int> -> unit
    abstract GetShaderSource : ``shader`` : uint32 * ``bufSize`` : uint32 * ``length`` : nativeptr<uint32> * ``source`` : nativeptr<uint8> -> unit
    abstract GetShaderSource : ``shader`` : aptr<uint32> * ``bufSize`` : aptr<uint32> * ``length`` : aptr<uint32> * ``source`` : aptr<uint8> -> unit
    abstract GetString : ``name`` : StringName * ``returnValue`` : nativeptr<nativeptr<uint8>> -> unit
    abstract GetString : ``name`` : aptr<StringName> * ``returnValue`` : aptr<nativeptr<uint8>> -> unit
    abstract GetTexParameterfv : ``target`` : TextureTarget * ``pname`` : GetTextureParameter * ``params`` : nativeptr<float32> -> unit
    abstract GetTexParameterfv : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<GetTextureParameter> * ``params`` : aptr<float32> -> unit
    abstract GetTexParameteriv : ``target`` : TextureTarget * ``pname`` : GetTextureParameter * ``params`` : nativeptr<int> -> unit
    abstract GetTexParameteriv : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<GetTextureParameter> * ``params`` : aptr<int> -> unit
    abstract GetUniformfv : ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<float32> -> unit
    abstract GetUniformfv : ``program`` : aptr<uint32> * ``location`` : aptr<int> * ``params`` : aptr<float32> -> unit
    abstract GetUniformiv : ``program`` : uint32 * ``location`` : int * ``params`` : nativeptr<int> -> unit
    abstract GetUniformiv : ``program`` : aptr<uint32> * ``location`` : aptr<int> * ``params`` : aptr<int> -> unit
    abstract GetUniformLocation : ``program`` : uint32 * ``name`` : nativeptr<uint8> * ``returnValue`` : nativeptr<int> -> unit
    abstract GetUniformLocation : ``program`` : aptr<uint32> * ``name`` : aptr<uint8> * ``returnValue`` : aptr<int> -> unit
    abstract GetVertexAttribfv : ``index`` : uint32 * ``pname`` : VertexAttribPropertyARB * ``params`` : nativeptr<float32> -> unit
    abstract GetVertexAttribfv : ``index`` : aptr<uint32> * ``pname`` : aptr<VertexAttribPropertyARB> * ``params`` : aptr<float32> -> unit
    abstract GetVertexAttribiv : ``index`` : uint32 * ``pname`` : VertexAttribPropertyARB * ``params`` : nativeptr<int> -> unit
    abstract GetVertexAttribiv : ``index`` : aptr<uint32> * ``pname`` : aptr<VertexAttribPropertyARB> * ``params`` : aptr<int> -> unit
    abstract GetVertexAttribPointerv : ``index`` : uint32 * ``pname`` : VertexAttribPointerPropertyARB * ``pointer`` : nativeptr<nativeint> -> unit
    abstract GetVertexAttribPointerv : ``index`` : aptr<uint32> * ``pname`` : aptr<VertexAttribPointerPropertyARB> * ``pointer`` : aptr<nativeint> -> unit
    abstract Hint : ``target`` : HintTarget * ``mode`` : HintMode -> unit
    abstract Hint : ``target`` : aptr<HintTarget> * ``mode`` : aptr<HintMode> -> unit
    abstract IsBuffer : ``buffer`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsBuffer : ``buffer`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsEnabled : ``cap`` : EnableCap * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsEnabled : ``cap`` : aptr<EnableCap> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsFramebuffer : ``framebuffer`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsFramebuffer : ``framebuffer`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsProgram : ``program`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsProgram : ``program`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsRenderbuffer : ``renderbuffer`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsRenderbuffer : ``renderbuffer`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsShader : ``shader`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsShader : ``shader`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract IsTexture : ``texture`` : uint32 * ``returnValue`` : nativeptr<Boolean> -> unit
    abstract IsTexture : ``texture`` : aptr<uint32> * ``returnValue`` : aptr<Boolean> -> unit
    abstract LineWidth : ``width`` : float32 -> unit
    abstract LineWidth : ``width`` : aptr<float32> -> unit
    abstract LinkProgram : ``program`` : uint32 -> unit
    abstract LinkProgram : ``program`` : aptr<uint32> -> unit
    abstract PixelStorei : ``pname`` : PixelStoreParameter * ``param`` : int -> unit
    abstract PixelStorei : ``pname`` : aptr<PixelStoreParameter> * ``param`` : aptr<int> -> unit
    abstract PolygonOffset : ``factor`` : float32 * ``units`` : float32 -> unit
    abstract PolygonOffset : ``factor`` : aptr<float32> * ``units`` : aptr<float32> -> unit
    abstract ReadPixels : ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    abstract ReadPixels : ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``format`` : aptr<PixelFormat> * ``type`` : aptr<PixelType> * ``pixels`` : aptr<'T7> -> unit
    abstract ReleaseShaderCompiler : unit -> unit
    abstract RenderbufferStorage : ``target`` : RenderbufferTarget * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract RenderbufferStorage : ``target`` : aptr<RenderbufferTarget> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract SampleCoverage : ``value`` : float32 * ``invert`` : Boolean -> unit
    abstract SampleCoverage : ``value`` : aptr<float32> * ``invert`` : aptr<Boolean> -> unit
    abstract Scissor : ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract Scissor : ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract ShaderBinary : ``count`` : uint32 * ``shaders`` : nativeptr<uint32> * ``binaryFormat`` : ShaderBinaryFormat * ``binary`` : nativeint * ``length`` : uint32 -> unit
    abstract ShaderBinary : ``count`` : aptr<uint32> * ``shaders`` : aptr<uint32> * ``binaryFormat`` : aptr<ShaderBinaryFormat> * ``binary`` : aptr<'T4> * ``length`` : aptr<uint32> -> unit
    abstract ShaderSource : ``shader`` : uint32 * ``count`` : uint32 * ``string`` : nativeptr<nativeptr<uint8>> * ``length`` : nativeptr<int> -> unit
    abstract ShaderSource : ``shader`` : aptr<uint32> * ``count`` : aptr<uint32> * ``string`` : aptr<nativeptr<uint8>> * ``length`` : aptr<int> -> unit
    abstract StencilFunc : ``func`` : StencilFunction * ``ref`` : int * ``mask`` : uint32 -> unit
    abstract StencilFunc : ``func`` : aptr<StencilFunction> * ``ref`` : aptr<int> * ``mask`` : aptr<uint32> -> unit
    abstract StencilFuncSeparate : ``face`` : StencilFaceDirection * ``func`` : StencilFunction * ``ref`` : int * ``mask`` : uint32 -> unit
    abstract StencilFuncSeparate : ``face`` : aptr<StencilFaceDirection> * ``func`` : aptr<StencilFunction> * ``ref`` : aptr<int> * ``mask`` : aptr<uint32> -> unit
    abstract StencilMask : ``mask`` : uint32 -> unit
    abstract StencilMask : ``mask`` : aptr<uint32> -> unit
    abstract StencilMaskSeparate : ``face`` : StencilFaceDirection * ``mask`` : uint32 -> unit
    abstract StencilMaskSeparate : ``face`` : aptr<StencilFaceDirection> * ``mask`` : aptr<uint32> -> unit
    abstract StencilOp : ``fail`` : StencilOp * ``zfail`` : StencilOp * ``zpass`` : StencilOp -> unit
    abstract StencilOp : ``fail`` : aptr<StencilOp> * ``zfail`` : aptr<StencilOp> * ``zpass`` : aptr<StencilOp> -> unit
    abstract StencilOpSeparate : ``face`` : StencilFaceDirection * ``sfail`` : StencilOp * ``dpfail`` : StencilOp * ``dppass`` : StencilOp -> unit
    abstract StencilOpSeparate : ``face`` : aptr<StencilFaceDirection> * ``sfail`` : aptr<StencilOp> * ``dpfail`` : aptr<StencilOp> * ``dppass`` : aptr<StencilOp> -> unit
    abstract TexImage2D : ``target`` : TextureTarget * ``level`` : int * ``internalformat`` : InternalFormat * ``width`` : uint32 * ``height`` : uint32 * ``border`` : int * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    abstract TexImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``internalformat`` : aptr<InternalFormat> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``border`` : aptr<int> * ``format`` : aptr<PixelFormat> * ``type`` : aptr<PixelType> * ``pixels`` : aptr<nativeint> -> unit
    abstract TexParameterf : ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``param`` : float32 -> unit
    abstract TexParameterf : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<TextureParameterName> * ``param`` : aptr<float32> -> unit
    abstract TexParameterfv : ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``params`` : nativeptr<float32> -> unit
    abstract TexParameterfv : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<TextureParameterName> * ``params`` : aptr<float32> -> unit
    abstract TexParameteri : ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``param`` : int -> unit
    abstract TexParameteri : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<TextureParameterName> * ``param`` : aptr<int> -> unit
    abstract TexParameteriv : ``target`` : TextureTarget * ``pname`` : TextureParameterName * ``params`` : nativeptr<int> -> unit
    abstract TexParameteriv : ``target`` : aptr<TextureTarget> * ``pname`` : aptr<TextureParameterName> * ``params`` : aptr<int> -> unit
    abstract TexSubImage2D : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : uint32 * ``height`` : uint32 * ``format`` : PixelFormat * ``type`` : PixelType * ``pixels`` : nativeint -> unit
    abstract TexSubImage2D : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> * ``format`` : aptr<PixelFormat> * ``type`` : aptr<PixelType> * ``pixels`` : aptr<nativeint> -> unit
    abstract Uniform1f : ``location`` : int * ``v0`` : float32 -> unit
    abstract Uniform1f : ``location`` : aptr<int> * ``v0`` : aptr<float32> -> unit
    abstract Uniform1fv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    abstract Uniform1fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<float32> -> unit
    abstract Uniform1i : ``location`` : int * ``v0`` : int -> unit
    abstract Uniform1i : ``location`` : aptr<int> * ``v0`` : aptr<int> -> unit
    abstract Uniform1iv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    abstract Uniform1iv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<int> -> unit
    abstract Uniform2f : ``location`` : int * ``v0`` : float32 * ``v1`` : float32 -> unit
    abstract Uniform2f : ``location`` : aptr<int> * ``v0`` : aptr<float32> * ``v1`` : aptr<float32> -> unit
    abstract Uniform2fv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    abstract Uniform2fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<float32> -> unit
    abstract Uniform2i : ``location`` : int * ``v0`` : int * ``v1`` : int -> unit
    abstract Uniform2i : ``location`` : aptr<int> * ``v0`` : aptr<int> * ``v1`` : aptr<int> -> unit
    abstract Uniform2iv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    abstract Uniform2iv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<int> -> unit
    abstract Uniform3f : ``location`` : int * ``v0`` : float32 * ``v1`` : float32 * ``v2`` : float32 -> unit
    abstract Uniform3f : ``location`` : aptr<int> * ``v0`` : aptr<float32> * ``v1`` : aptr<float32> * ``v2`` : aptr<float32> -> unit
    abstract Uniform3fv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    abstract Uniform3fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<float32> -> unit
    abstract Uniform3i : ``location`` : int * ``v0`` : int * ``v1`` : int * ``v2`` : int -> unit
    abstract Uniform3i : ``location`` : aptr<int> * ``v0`` : aptr<int> * ``v1`` : aptr<int> * ``v2`` : aptr<int> -> unit
    abstract Uniform3iv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    abstract Uniform3iv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<int> -> unit
    abstract Uniform4f : ``location`` : int * ``v0`` : float32 * ``v1`` : float32 * ``v2`` : float32 * ``v3`` : float32 -> unit
    abstract Uniform4f : ``location`` : aptr<int> * ``v0`` : aptr<float32> * ``v1`` : aptr<float32> * ``v2`` : aptr<float32> * ``v3`` : aptr<float32> -> unit
    abstract Uniform4fv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<float32> -> unit
    abstract Uniform4fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<float32> -> unit
    abstract Uniform4i : ``location`` : int * ``v0`` : int * ``v1`` : int * ``v2`` : int * ``v3`` : int -> unit
    abstract Uniform4i : ``location`` : aptr<int> * ``v0`` : aptr<int> * ``v1`` : aptr<int> * ``v2`` : aptr<int> * ``v3`` : aptr<int> -> unit
    abstract Uniform4iv : ``location`` : int * ``count`` : uint32 * ``value`` : nativeptr<int> -> unit
    abstract Uniform4iv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``value`` : aptr<int> -> unit
    abstract UniformMatrix2fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix2fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix3fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix3fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UniformMatrix4fv : ``location`` : int * ``count`` : uint32 * ``transpose`` : Boolean * ``value`` : nativeptr<float32> -> unit
    abstract UniformMatrix4fv : ``location`` : aptr<int> * ``count`` : aptr<uint32> * ``transpose`` : aptr<Boolean> * ``value`` : aptr<float32> -> unit
    abstract UseProgram : ``program`` : uint32 -> unit
    abstract UseProgram : ``program`` : aptr<uint32> -> unit
    abstract ValidateProgram : ``program`` : uint32 -> unit
    abstract ValidateProgram : ``program`` : aptr<uint32> -> unit
    abstract VertexAttrib1f : ``index`` : uint32 * ``x`` : float32 -> unit
    abstract VertexAttrib1f : ``index`` : aptr<uint32> * ``x`` : aptr<float32> -> unit
    abstract VertexAttrib1fv : ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    abstract VertexAttrib1fv : ``index`` : aptr<uint32> * ``v`` : aptr<float32> -> unit
    abstract VertexAttrib2f : ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 -> unit
    abstract VertexAttrib2f : ``index`` : aptr<uint32> * ``x`` : aptr<float32> * ``y`` : aptr<float32> -> unit
    abstract VertexAttrib2fv : ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    abstract VertexAttrib2fv : ``index`` : aptr<uint32> * ``v`` : aptr<float32> -> unit
    abstract VertexAttrib3f : ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 * ``z`` : float32 -> unit
    abstract VertexAttrib3f : ``index`` : aptr<uint32> * ``x`` : aptr<float32> * ``y`` : aptr<float32> * ``z`` : aptr<float32> -> unit
    abstract VertexAttrib3fv : ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    abstract VertexAttrib3fv : ``index`` : aptr<uint32> * ``v`` : aptr<float32> -> unit
    abstract VertexAttrib4f : ``index`` : uint32 * ``x`` : float32 * ``y`` : float32 * ``z`` : float32 * ``w`` : float32 -> unit
    abstract VertexAttrib4f : ``index`` : aptr<uint32> * ``x`` : aptr<float32> * ``y`` : aptr<float32> * ``z`` : aptr<float32> * ``w`` : aptr<float32> -> unit
    abstract VertexAttrib4fv : ``index`` : uint32 * ``v`` : nativeptr<float32> -> unit
    abstract VertexAttrib4fv : ``index`` : aptr<uint32> * ``v`` : aptr<float32> -> unit
    abstract VertexAttribPointer : ``index`` : uint32 * ``size`` : int * ``type`` : VertexAttribPointerType * ``normalized`` : Boolean * ``stride`` : uint32 * ``pointer`` : nativeint -> unit
    abstract VertexAttribPointer : ``index`` : aptr<uint32> * ``size`` : aptr<int> * ``type`` : aptr<VertexAttribPointerType> * ``normalized`` : aptr<Boolean> * ``stride`` : aptr<uint32> * ``pointer`` : aptr<'T6> -> unit
    abstract Viewport : ``x`` : int * ``y`` : int * ``width`` : uint32 * ``height`` : uint32 -> unit
    abstract Viewport : ``x`` : aptr<int> * ``y`` : aptr<int> * ``width`` : aptr<uint32> * ``height`` : aptr<uint32> -> unit
    abstract GetBufferSubData : ``target`` : BufferTargetARB * ``offset`` : nativeint * ``size`` : unativeint * ``dst`` : nativeint -> unit
    abstract GetBufferSubData : ``target`` : aptr<BufferTargetARB> * ``offset`` : aptr<nativeint> * ``size`` : aptr<unativeint> * ``dst`` : aptr<nativeint> -> unit
    abstract MultiDrawArraysIndirect : ``mode`` : PrimitiveType * ``indirectBuffer`` : uint32 * ``count`` : int * ``bindingInfo`` : nativeint -> unit
    abstract MultiDrawArraysIndirect : ``mode`` : aptr<PrimitiveType> * ``indirectBuffer`` : aptr<uint32> * ``count`` : aptr<int> * ``bindingInfo`` : aptr<nativeint> -> unit
    abstract MultiDrawArrays : ``mode`` : PrimitiveType * ``indirect`` : nativeint * ``count`` : int * ``bindingInfo`` : nativeint -> unit
    abstract MultiDrawArrays : ``mode`` : aptr<PrimitiveType> * ``indirect`` : aptr<nativeint> * ``count`` : aptr<int> * ``bindingInfo`` : aptr<nativeint> -> unit
    abstract MultiDrawElementsIndirect : ``mode`` : PrimitiveType * ``indirectBuffer`` : uint32 * ``count`` : int * ``indexType`` : DrawElementsType * ``bindingInfo`` : nativeint -> unit
    abstract MultiDrawElementsIndirect : ``mode`` : aptr<PrimitiveType> * ``indirectBuffer`` : aptr<uint32> * ``count`` : aptr<int> * ``indexType`` : aptr<DrawElementsType> * ``bindingInfo`` : aptr<nativeint> -> unit
    abstract MultiDrawElements : ``mode`` : PrimitiveType * ``indirect`` : nativeint * ``count`` : int * ``indexType`` : DrawElementsType * ``bindingInfo`` : nativeint -> unit
    abstract MultiDrawElements : ``mode`` : aptr<PrimitiveType> * ``indirect`` : aptr<nativeint> * ``count`` : aptr<int> * ``indexType`` : aptr<DrawElementsType> * ``bindingInfo`` : aptr<nativeint> -> unit
    abstract Commit : unit -> unit
    abstract TexSubImage2DJSImage : ``target`` : TextureTarget * ``level`` : int * ``xoffset`` : int * ``yoffset`` : int * ``width`` : int * ``height`` : int * ``format`` : PixelFormat * ``typ`` : PixelType * ``imgHandle`` : int -> unit
    abstract TexSubImage2DJSImage : ``target`` : aptr<TextureTarget> * ``level`` : aptr<int> * ``xoffset`` : aptr<int> * ``yoffset`` : aptr<int> * ``width`` : aptr<int> * ``height`` : aptr<int> * ``format`` : aptr<PixelFormat> * ``typ`` : aptr<PixelType> * ``imgHandle`` : aptr<int> -> unit
