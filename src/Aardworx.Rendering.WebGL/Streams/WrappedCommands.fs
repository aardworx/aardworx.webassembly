// ===============================================================================
//                    AUTO GENERATED FILE (see Generator.fsx)
// ===============================================================================
namespace Aardworx.Rendering.WebGL
open Silk.NET.OpenGLES
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
#nowarn "9"

/// As of dotnet 8.0 the WebAssembly runtime does not support passing floats directly to C functions.
/// This module provides a way to convert float32 values to int32 values that can be passed to C functions.
module WrappedCommands =
    let inline private float32Bits (v : float32) =
        use ptr = fixed [| v |]
        ptr |> NativePtr.toNativeInt |> NativePtr.ofNativeInt<int> |> NativePtr.read
    type glBlitFramebufferArgs =
        struct
            val mutable public ``srcX0`` : int
            val mutable public ``srcY0`` : int
            val mutable public ``srcX1`` : int
            val mutable public ``srcY1`` : int
            val mutable public ``dstX0`` : int
            val mutable public ``dstY0`` : int
            val mutable public ``dstX1`` : int
            val mutable public ``dstY1`` : int
            val mutable public ``mask`` : ClearBufferMask
            val mutable public ``filter`` : BlitFramebufferFilter
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glBlitFramebuffer(glBlitFramebufferArgs* args)

    let glBlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) =
        let mutable args = Unchecked.defaultof<glBlitFramebufferArgs>
        args.``srcX0`` <- ``srcX0``
        args.``srcY0`` <- ``srcY0``
        args.``srcX1`` <- ``srcX1``
        args.``srcY1`` <- ``srcY1``
        args.``dstX0`` <- ``dstX0``
        args.``dstY0`` <- ``dstY0``
        args.``dstX1`` <- ``dstX1``
        args.``dstY1`` <- ``dstY1``
        args.``mask`` <- ``mask``
        args.``filter`` <- ``filter``
        use ptr = fixed [| args |]
        _glBlitFramebuffer(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glClearBufferfi(BufferKind buffer, int drawbuffer, int depth, int stencil)
    let glClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) =
        _glClearBufferfi(``buffer``, ``drawbuffer``, float32Bits ``depth``, ``stencil``);

    type glCompressedTexImage3DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``internalformat`` : InternalFormat
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``depth`` : uint32
            val mutable public ``border`` : int
            val mutable public ``imageSize`` : uint32
            val mutable public ``data`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCompressedTexImage3D(glCompressedTexImage3DArgs* args)

    let glCompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) =
        let mutable args = Unchecked.defaultof<glCompressedTexImage3DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``internalformat`` <- ``internalformat``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``depth`` <- ``depth``
        args.``border`` <- ``border``
        args.``imageSize`` <- ``imageSize``
        args.``data`` <- ``data``
        use ptr = fixed [| args |]
        _glCompressedTexImage3D(ptr)

    type glCompressedTexSubImage3DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``zoffset`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``depth`` : uint32
            val mutable public ``format`` : InternalFormat
            val mutable public ``imageSize`` : uint32
            val mutable public ``data`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCompressedTexSubImage3D(glCompressedTexSubImage3DArgs* args)

    let glCompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) =
        let mutable args = Unchecked.defaultof<glCompressedTexSubImage3DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``zoffset`` <- ``zoffset``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``depth`` <- ``depth``
        args.``format`` <- ``format``
        args.``imageSize`` <- ``imageSize``
        args.``data`` <- ``data``
        use ptr = fixed [| args |]
        _glCompressedTexSubImage3D(ptr)

    type glCopyTexSubImage3DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``zoffset`` : int
            val mutable public ``x`` : int
            val mutable public ``y`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCopyTexSubImage3D(glCopyTexSubImage3DArgs* args)

    let glCopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let mutable args = Unchecked.defaultof<glCopyTexSubImage3DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``zoffset`` <- ``zoffset``
        args.``x`` <- ``x``
        args.``y`` <- ``y``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        use ptr = fixed [| args |]
        _glCopyTexSubImage3D(ptr)

    type glGetTransformFeedbackVaryingArgs =
        struct
            val mutable public ``program`` : uint32
            val mutable public ``index`` : uint32
            val mutable public ``bufSize`` : uint32
            val mutable public ``length`` : nativeptr<uint32>
            val mutable public ``size`` : nativeptr<uint32>
            val mutable public ``type`` : nativeptr<GLEnum>
            val mutable public ``name`` : nativeptr<uint8>
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glGetTransformFeedbackVarying(glGetTransformFeedbackVaryingArgs* args)

    let glGetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) =
        let mutable args = Unchecked.defaultof<glGetTransformFeedbackVaryingArgs>
        args.``program`` <- ``program``
        args.``index`` <- ``index``
        args.``bufSize`` <- ``bufSize``
        args.``length`` <- ``length``
        args.``size`` <- ``size``
        args.``type`` <- ``type``
        args.``name`` <- ``name``
        use ptr = fixed [| args |]
        _glGetTransformFeedbackVarying(ptr)

    type glInvalidateSubFramebufferArgs =
        struct
            val mutable public ``target`` : FramebufferTarget
            val mutable public ``numAttachments`` : uint32
            val mutable public ``attachments`` : nativeptr<GLEnum>
            val mutable public ``x`` : int
            val mutable public ``y`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glInvalidateSubFramebuffer(glInvalidateSubFramebufferArgs* args)

    let glInvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let mutable args = Unchecked.defaultof<glInvalidateSubFramebufferArgs>
        args.``target`` <- ``target``
        args.``numAttachments`` <- ``numAttachments``
        args.``attachments`` <- ``attachments``
        args.``x`` <- ``x``
        args.``y`` <- ``y``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        use ptr = fixed [| args |]
        _glInvalidateSubFramebuffer(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glSamplerParameterf(uint32 sampler, SamplerParameterF pname, int param)
    let glSamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) =
        _glSamplerParameterf(``sampler``, ``pname``, float32Bits ``param``);

    type glTexImage3DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``internalformat`` : InternalFormat
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``depth`` : uint32
            val mutable public ``border`` : int
            val mutable public ``format`` : PixelFormat
            val mutable public ``type`` : PixelType
            val mutable public ``pixels`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glTexImage3D(glTexImage3DArgs* args)

    let glTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let mutable args = Unchecked.defaultof<glTexImage3DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``internalformat`` <- ``internalformat``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``depth`` <- ``depth``
        args.``border`` <- ``border``
        args.``format`` <- ``format``
        args.``type`` <- ``type``
        args.``pixels`` <- ``pixels``
        use ptr = fixed [| args |]
        _glTexImage3D(ptr)

    type glTexSubImage3DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``zoffset`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``depth`` : uint32
            val mutable public ``format`` : PixelFormat
            val mutable public ``type`` : PixelType
            val mutable public ``pixels`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glTexSubImage3D(glTexSubImage3DArgs* args)

    let glTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let mutable args = Unchecked.defaultof<glTexSubImage3DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``zoffset`` <- ``zoffset``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``depth`` <- ``depth``
        args.``format`` <- ``format``
        args.``type`` <- ``type``
        args.``pixels`` <- ``pixels``
        use ptr = fixed [| args |]
        _glTexSubImage3D(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glBlendColor(int red, int green, int blue, int alpha)
    let glBlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) =
        _glBlendColor(float32Bits ``red``, float32Bits ``green``, float32Bits ``blue``, float32Bits ``alpha``);

    [<DllImport("WrappedCommands")>]
    extern void private _glClearColor(int red, int green, int blue, int alpha)
    let glClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) =
        _glClearColor(float32Bits ``red``, float32Bits ``green``, float32Bits ``blue``, float32Bits ``alpha``);

    [<DllImport("WrappedCommands")>]
    extern void private _glClearDepthf(int d)
    let glClearDepthf(``d`` : float32) =
        _glClearDepthf(float32Bits ``d``);

    type glCompressedTexImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``internalformat`` : InternalFormat
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``border`` : int
            val mutable public ``imageSize`` : uint32
            val mutable public ``data`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCompressedTexImage2D(glCompressedTexImage2DArgs* args)

    let glCompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) =
        let mutable args = Unchecked.defaultof<glCompressedTexImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``internalformat`` <- ``internalformat``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``border`` <- ``border``
        args.``imageSize`` <- ``imageSize``
        args.``data`` <- ``data``
        use ptr = fixed [| args |]
        _glCompressedTexImage2D(ptr)

    type glCompressedTexSubImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``format`` : InternalFormat
            val mutable public ``imageSize`` : uint32
            val mutable public ``data`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCompressedTexSubImage2D(glCompressedTexSubImage2DArgs* args)

    let glCompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) =
        let mutable args = Unchecked.defaultof<glCompressedTexSubImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``format`` <- ``format``
        args.``imageSize`` <- ``imageSize``
        args.``data`` <- ``data``
        use ptr = fixed [| args |]
        _glCompressedTexSubImage2D(ptr)

    type glCopyTexImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``internalformat`` : InternalFormat
            val mutable public ``x`` : int
            val mutable public ``y`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``border`` : int
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCopyTexImage2D(glCopyTexImage2DArgs* args)

    let glCopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) =
        let mutable args = Unchecked.defaultof<glCopyTexImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``internalformat`` <- ``internalformat``
        args.``x`` <- ``x``
        args.``y`` <- ``y``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``border`` <- ``border``
        use ptr = fixed [| args |]
        _glCopyTexImage2D(ptr)

    type glCopyTexSubImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``x`` : int
            val mutable public ``y`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glCopyTexSubImage2D(glCopyTexSubImage2DArgs* args)

    let glCopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let mutable args = Unchecked.defaultof<glCopyTexSubImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``x`` <- ``x``
        args.``y`` <- ``y``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        use ptr = fixed [| args |]
        _glCopyTexSubImage2D(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glDepthRangef(int n, int f)
    let glDepthRangef(``n`` : float32, ``f`` : float32) =
        _glDepthRangef(float32Bits ``n``, float32Bits ``f``);

    type glGetActiveAttribArgs =
        struct
            val mutable public ``program`` : uint32
            val mutable public ``index`` : uint32
            val mutable public ``bufSize`` : uint32
            val mutable public ``length`` : nativeptr<uint32>
            val mutable public ``size`` : nativeptr<int>
            val mutable public ``type`` : nativeptr<GLEnum>
            val mutable public ``name`` : nativeptr<uint8>
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glGetActiveAttrib(glGetActiveAttribArgs* args)

    let glGetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) =
        let mutable args = Unchecked.defaultof<glGetActiveAttribArgs>
        args.``program`` <- ``program``
        args.``index`` <- ``index``
        args.``bufSize`` <- ``bufSize``
        args.``length`` <- ``length``
        args.``size`` <- ``size``
        args.``type`` <- ``type``
        args.``name`` <- ``name``
        use ptr = fixed [| args |]
        _glGetActiveAttrib(ptr)

    type glGetActiveUniformArgs =
        struct
            val mutable public ``program`` : uint32
            val mutable public ``index`` : uint32
            val mutable public ``bufSize`` : uint32
            val mutable public ``length`` : nativeptr<uint32>
            val mutable public ``size`` : nativeptr<int>
            val mutable public ``type`` : nativeptr<GLEnum>
            val mutable public ``name`` : nativeptr<uint8>
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glGetActiveUniform(glGetActiveUniformArgs* args)

    let glGetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) =
        let mutable args = Unchecked.defaultof<glGetActiveUniformArgs>
        args.``program`` <- ``program``
        args.``index`` <- ``index``
        args.``bufSize`` <- ``bufSize``
        args.``length`` <- ``length``
        args.``size`` <- ``size``
        args.``type`` <- ``type``
        args.``name`` <- ``name``
        use ptr = fixed [| args |]
        _glGetActiveUniform(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glLineWidth(int width)
    let glLineWidth(``width`` : float32) =
        _glLineWidth(float32Bits ``width``);

    [<DllImport("WrappedCommands")>]
    extern void private _glPolygonOffset(int factor, int units)
    let glPolygonOffset(``factor`` : float32, ``units`` : float32) =
        _glPolygonOffset(float32Bits ``factor``, float32Bits ``units``);

    type glReadPixelsArgs =
        struct
            val mutable public ``x`` : int
            val mutable public ``y`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``format`` : PixelFormat
            val mutable public ``type`` : PixelType
            val mutable public ``pixels`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glReadPixels(glReadPixelsArgs* args)

    let glReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let mutable args = Unchecked.defaultof<glReadPixelsArgs>
        args.``x`` <- ``x``
        args.``y`` <- ``y``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``format`` <- ``format``
        args.``type`` <- ``type``
        args.``pixels`` <- ``pixels``
        use ptr = fixed [| args |]
        _glReadPixels(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glSampleCoverage(int value, Boolean invert)
    let glSampleCoverage(``value`` : float32, ``invert`` : Boolean) =
        _glSampleCoverage(float32Bits ``value``, ``invert``);

    type glTexImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``internalformat`` : InternalFormat
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``border`` : int
            val mutable public ``format`` : PixelFormat
            val mutable public ``type`` : PixelType
            val mutable public ``pixels`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glTexImage2D(glTexImage2DArgs* args)

    let glTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let mutable args = Unchecked.defaultof<glTexImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``internalformat`` <- ``internalformat``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``border`` <- ``border``
        args.``format`` <- ``format``
        args.``type`` <- ``type``
        args.``pixels`` <- ``pixels``
        use ptr = fixed [| args |]
        _glTexImage2D(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glTexParameterf(TextureTarget target, TextureParameterName pname, int param)
    let glTexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) =
        _glTexParameterf(``target``, ``pname``, float32Bits ``param``);

    type glTexSubImage2DArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``width`` : uint32
            val mutable public ``height`` : uint32
            val mutable public ``format`` : PixelFormat
            val mutable public ``type`` : PixelType
            val mutable public ``pixels`` : nativeint
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glTexSubImage2D(glTexSubImage2DArgs* args)

    let glTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let mutable args = Unchecked.defaultof<glTexSubImage2DArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``format`` <- ``format``
        args.``type`` <- ``type``
        args.``pixels`` <- ``pixels``
        use ptr = fixed [| args |]
        _glTexSubImage2D(ptr)

    [<DllImport("WrappedCommands")>]
    extern void private _glUniform1f(int location, int v0)
    let glUniform1f(``location`` : int, ``v0`` : float32) =
        _glUniform1f(``location``, float32Bits ``v0``);

    [<DllImport("WrappedCommands")>]
    extern void private _glUniform2f(int location, int v0, int v1)
    let glUniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) =
        _glUniform2f(``location``, float32Bits ``v0``, float32Bits ``v1``);

    [<DllImport("WrappedCommands")>]
    extern void private _glUniform3f(int location, int v0, int v1, int v2)
    let glUniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) =
        _glUniform3f(``location``, float32Bits ``v0``, float32Bits ``v1``, float32Bits ``v2``);

    [<DllImport("WrappedCommands")>]
    extern void private _glUniform4f(int location, int v0, int v1, int v2, int v3)
    let glUniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) =
        _glUniform4f(``location``, float32Bits ``v0``, float32Bits ``v1``, float32Bits ``v2``, float32Bits ``v3``);

    [<DllImport("WrappedCommands")>]
    extern void private _glVertexAttrib1f(uint32 index, int x)
    let glVertexAttrib1f(``index`` : uint32, ``x`` : float32) =
        _glVertexAttrib1f(``index``, float32Bits ``x``);

    [<DllImport("WrappedCommands")>]
    extern void private _glVertexAttrib2f(uint32 index, int x, int y)
    let glVertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) =
        _glVertexAttrib2f(``index``, float32Bits ``x``, float32Bits ``y``);

    [<DllImport("WrappedCommands")>]
    extern void private _glVertexAttrib3f(uint32 index, int x, int y, int z)
    let glVertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) =
        _glVertexAttrib3f(``index``, float32Bits ``x``, float32Bits ``y``, float32Bits ``z``);

    [<DllImport("WrappedCommands")>]
    extern void private _glVertexAttrib4f(uint32 index, int x, int y, int z, int w)
    let glVertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) =
        _glVertexAttrib4f(``index``, float32Bits ``x``, float32Bits ``y``, float32Bits ``z``, float32Bits ``w``);

    type glTexSubImage2DJSImageArgs =
        struct
            val mutable public ``target`` : TextureTarget
            val mutable public ``level`` : int
            val mutable public ``xoffset`` : int
            val mutable public ``yoffset`` : int
            val mutable public ``width`` : int
            val mutable public ``height`` : int
            val mutable public ``format`` : PixelFormat
            val mutable public ``typ`` : PixelType
            val mutable public ``imgHandle`` : int
        end
    [<DllImport("WrappedCommands")>]
    extern void private _glTexSubImage2DJSImage(glTexSubImage2DJSImageArgs* args)

    let glTexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) =
        let mutable args = Unchecked.defaultof<glTexSubImage2DJSImageArgs>
        args.``target`` <- ``target``
        args.``level`` <- ``level``
        args.``xoffset`` <- ``xoffset``
        args.``yoffset`` <- ``yoffset``
        args.``width`` <- ``width``
        args.``height`` <- ``height``
        args.``format`` <- ``format``
        args.``typ`` <- ``typ``
        args.``imgHandle`` <- ``imgHandle``
        use ptr = fixed [| args |]
        _glTexSubImage2DJSImage(ptr)

