namespace Aardworx.Rendering.WebGL

open System.Runtime.CompilerServices
open Silk.NET.OpenGLES

open Aardworx.Rendering.WebGL.Streams

[<AbstractClass; Sealed; Extension>]
type SilkExtensions private() =

    [<Extension>]
    static member GetBufferSubData(gl : GL, target : BufferTargetARB, offset : nativeint, size : unativeint, dst : nativeint) =
        GLDelegates.get(gl.Context).glGetBufferSubData.Invoke(target, offset, size, dst)

        
    [<Extension>]
    static member Commit(gl : GL) =
        GLDelegates.get(gl.Context).glCommit.Invoke()


