namespace Aardworx.Rendering.WebGL.Streams
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open System.Runtime.InteropServices
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
#nowarn "9"
module private Interpreter =
    [<DllImport("Interpreter")>]
    extern int emInterpret(nativeint code, int length, nativeint stack)
type NativeCommandEncoder(device : Device) =
    inherit CommandEncoder(device)
    let mutable currentGL = Unchecked.defaultof<GL>
    let mutable capacity = 128
    let mutable mem = Marshal.AllocHGlobal capacity
    let mutable current = mem
    let mutable len = 0
    let mutable entry =
        let p = NativePtr.alloc<nativeint> 1
        NativePtr.write p mem
        p
    let resize (newSize : int) = 
        if newSize <> capacity then
            let o = current - mem
            let n = Marshal.AllocHGlobal newSize
            Marshal.Copy(mem, n, min newSize len)
            Marshal.FreeHGlobal mem
            mem <- n
            NativePtr.write entry mem
            current <- mem + o
    let ensureFree (add : int) =
        let e = len + add
        if e > capacity then resize (Fun.NextPowerOfTwo e)
    member x.Entry = entry
    member x.Pointer = mem
    member x.Length = len
    member private x.Write(value : uint8) =
        ensureFree 1
        NativePtr.write (NativePtr.ofNativeInt current) value
        current <- current + 1n
        len <- len + 1
    member private x.Write(value : uint16) =
        ensureFree 2
        NativePtr.write (NativePtr.ofNativeInt current) value
        current <- current + 2n
        len <- len + 2
    member private x.Write(value : uint32) =
        ensureFree 4
        NativePtr.write (NativePtr.ofNativeInt current) value
        current <- current + 4n
        len <- len + 4
    member private x.Write(value : int) =
        ensureFree 4
        NativePtr.write (NativePtr.ofNativeInt current) value
        current <- current + 4n
        len <- len + 4
    member private x.Write(value : nativeint) =
        ensureFree 4
        NativePtr.write (NativePtr.ofNativeInt current) value
        current <- current + 4n
        len <- len + 4
    override this.Uniform2iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 0us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 1us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 2us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        len <- len + 18
    override this.Uniform3f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 3us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.Uniform3fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 4us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 5us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform3i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 6us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        len <- len + 18
    override this.Uniform3i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 7us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.Uniform3iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 8us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform3iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 9us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 10us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v3``
        current <- current + 4n
        len <- len + 22
    override this.Uniform4f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>, ``v3`` : aptr<float32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 11us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v3``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.Uniform4fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 12us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 13us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform4i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int, ``v3`` : int) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 14us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v3``
        current <- current + 4n
        len <- len + 22
    override this.Uniform4i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>, ``v3`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 15us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v3``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.Uniform4iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 16us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform4iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 17us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.UniformMatrix2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 18us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 19us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 20us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 21us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 22us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 23us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UseProgram(``program`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 24us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        len <- len + 6
    override this.UseProgram(``program`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 25us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.ValidateProgram(``program`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 26us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        len <- len + 6
    override this.ValidateProgram(``program`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 27us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.VertexAttrib1f(``index`` : uint32, ``x`` : float32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 28us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib1f(``index`` : aptr<uint32>, ``x`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 29us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib1fv(``index`` : uint32, ``v`` : nativeptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 30us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib1fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 31us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 32us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        len <- len + 14
    override this.VertexAttrib2f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 33us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.VertexAttrib2fv(``index`` : uint32, ``v`` : nativeptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 34us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib2fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 35us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 36us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``z``
        current <- current + 4n
        len <- len + 18
    override this.VertexAttrib3f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 37us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``z``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.VertexAttrib3fv(``index`` : uint32, ``v`` : nativeptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 38us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib3fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 39us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 40us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``z``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``w``
        current <- current + 4n
        len <- len + 22
    override this.VertexAttrib4f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>, ``w`` : aptr<float32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 41us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``z``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``w``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.VertexAttrib4fv(``index`` : uint32, ``v`` : nativeptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 42us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttrib4fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 43us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribPointerType, ``normalized`` : Boolean, ``stride`` : uint32, ``pointer`` : nativeint) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 44us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``normalized``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``stride``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pointer``
        current <- current + 4n
        len <- len + 26
    override this.VertexAttribPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribPointerType>, ``normalized`` : aptr<Boolean>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<_>) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 45us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``normalized``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``stride``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pointer``).Pointer)
        current <- current + 4n
        len <- len + 26
    override this.Viewport(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 46us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 18
    override this.Viewport(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 47us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.TexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 48us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``border``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pixels``
        current <- current + 4n
        len <- len + 38
    override this.TexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 49us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``border``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pixels``).Pointer)
        current <- current + 4n
        len <- len + 38
    override this.TexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 50us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.TexParameterf(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 51us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.TexParameterfv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 52us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.TexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 53us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.TexParameteri(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : int) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 54us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.TexParameteri(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 55us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.TexParameteriv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 56us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.TexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 57us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.TexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 58us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pixels``
        current <- current + 4n
        len <- len + 38
    override this.TexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 59us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pixels``).Pointer)
        current <- current + 4n
        len <- len + 38
    override this.Uniform1f(``location`` : int, ``v0`` : float32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 60us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        len <- len + 10
    override this.Uniform1f(``location`` : aptr<int>, ``v0`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 61us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.Uniform1fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 62us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform1fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 63us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform1i(``location`` : int, ``v0`` : int) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 64us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        len <- len + 10
    override this.Uniform1i(``location`` : aptr<int>, ``v0`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 65us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.Uniform1iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 66us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform1iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 67us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 68us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 69us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform2fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 70us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 71us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform2i(``location`` : int, ``v0`` : int, ``v1`` : int) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 72us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 73us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 74us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pixels``
        current <- current + 4n
        len <- len + 30
    override this.ReadPixels(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<_>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 75us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pixels``).Pointer)
        current <- current + 4n
        len <- len + 30
    override this.ReleaseShaderCompiler() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 76us
        current <- current + 2n
        len <- len + 2
    override this.RenderbufferStorage(``target`` : RenderbufferTarget, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 77us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 18
    override this.RenderbufferStorage(``target`` : aptr<RenderbufferTarget>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 78us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.SampleCoverage(``value`` : float32, ``invert`` : Boolean) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 79us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``invert``
        current <- current + 4n
        len <- len + 10
    override this.SampleCoverage(``value`` : aptr<float32>, ``invert`` : aptr<Boolean>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 80us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``invert``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.Scissor(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 81us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 18
    override this.Scissor(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 82us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.ShaderBinary(``count`` : uint32, ``shaders`` : nativeptr<uint32>, ``binaryFormat`` : ShaderBinaryFormat, ``binary`` : nativeint, ``length`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 83us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``shaders``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binaryFormat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binary``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        len <- len + 22
    override this.ShaderBinary(``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>, ``binaryFormat`` : aptr<ShaderBinaryFormat>, ``binary`` : aptr<_>, ``length`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 84us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shaders``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binaryFormat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binary``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.ShaderSource(``shader`` : uint32, ``count`` : uint32, ``string`` : nativeptr<nativeptr<uint8>>, ``length`` : nativeptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 85us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``string``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        len <- len + 18
    override this.ShaderSource(``shader`` : aptr<uint32>, ``count`` : aptr<uint32>, ``string`` : aptr<nativeptr<uint8>>, ``length`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 86us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``string``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.StencilFunc(``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 87us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``func``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ref``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        len <- len + 14
    override this.StencilFunc(``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 88us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``func``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ref``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.StencilFuncSeparate(``face`` : StencilFaceDirection, ``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 89us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``face``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``func``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ref``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        len <- len + 18
    override this.StencilFuncSeparate(``face`` : aptr<StencilFaceDirection>, ``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 90us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``face``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``func``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ref``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.StencilMask(``mask`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 91us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        len <- len + 6
    override this.StencilMask(``mask`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 92us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.StencilMaskSeparate(``face`` : StencilFaceDirection, ``mask`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 93us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``face``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        len <- len + 10
    override this.StencilMaskSeparate(``face`` : aptr<StencilFaceDirection>, ``mask`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 94us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``face``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.StencilOp(``fail`` : StencilOp, ``zfail`` : StencilOp, ``zpass`` : StencilOp) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 95us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``fail``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``zfail``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``zpass``
        current <- current + 4n
        len <- len + 14
    override this.StencilOp(``fail`` : aptr<StencilOp>, ``zfail`` : aptr<StencilOp>, ``zpass`` : aptr<StencilOp>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 96us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``fail``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``zfail``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``zpass``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.StencilOpSeparate(``face`` : StencilFaceDirection, ``sfail`` : StencilOp, ``dpfail`` : StencilOp, ``dppass`` : StencilOp) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 97us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``face``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``sfail``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dpfail``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dppass``
        current <- current + 4n
        len <- len + 18
    override this.StencilOpSeparate(``face`` : aptr<StencilFaceDirection>, ``sfail`` : aptr<StencilOp>, ``dpfail`` : aptr<StencilOp>, ``dppass`` : aptr<StencilOp>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 98us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``face``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sfail``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dpfail``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dppass``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetShaderInfoLog(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 99us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``infoLog``
        current <- current + 4n
        len <- len + 18
    override this.GetShaderInfoLog(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 100us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``infoLog``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetShaderPrecisionFormat(``shadertype`` : ShaderType, ``precisiontype`` : PrecisionType, ``range`` : nativeptr<int>, ``precision`` : nativeptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 101us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shadertype``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``precisiontype``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``range``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``precision``
        current <- current + 4n
        len <- len + 18
    override this.GetShaderPrecisionFormat(``shadertype`` : aptr<ShaderType>, ``precisiontype`` : aptr<PrecisionType>, ``range`` : aptr<int>, ``precision`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 102us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shadertype``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``precisiontype``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``range``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``precision``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetShaderSource(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``source`` : nativeptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 103us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``source``
        current <- current + 4n
        len <- len + 18
    override this.GetShaderSource(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``source`` : aptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 104us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``source``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetString(``name`` : StringName, ``returnValue`` : nativeptr<nativeptr<uint8>>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 105us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 6
    override this.GetString(``name`` : aptr<StringName>, ``returnValue`` : aptr<nativeptr<uint8>>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 106us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.GetTexParameterfv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 107us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetTexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 108us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetTexParameteriv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 109us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetTexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 110us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetUniformfv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 111us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetUniformfv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 112us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetUniformiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 113us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetUniformiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 114us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetUniformLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 115us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 10
    override this.GetUniformLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 116us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetVertexAttribfv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 117us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribfv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 118us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribiv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 119us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 120us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribPointerv(``index`` : uint32, ``pname`` : VertexAttribPointerPropertyARB, ``pointer`` : nativeptr<nativeint>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 121us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pointer``
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribPointerv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPointerPropertyARB>, ``pointer`` : aptr<nativeint>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 122us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pointer``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Hint(``target`` : HintTarget, ``mode`` : HintMode) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 123us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        len <- len + 10
    override this.Hint(``target`` : aptr<HintTarget>, ``mode`` : aptr<HintMode>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 124us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.IsBuffer(``buffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 125us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        len <- len + 6
    override this.IsBuffer(``buffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 126us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsEnabled(``cap`` : EnableCap, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 127us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``cap``
        current <- current + 4n
        len <- len + 6
    override this.IsEnabled(``cap`` : aptr<EnableCap>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 128us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``cap``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsFramebuffer(``framebuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 129us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``framebuffer``
        current <- current + 4n
        len <- len + 6
    override this.IsFramebuffer(``framebuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 130us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``framebuffer``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsProgram(``program`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 131us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        len <- len + 6
    override this.IsProgram(``program`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 132us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsRenderbuffer(``renderbuffer`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 133us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffer``
        current <- current + 4n
        len <- len + 6
    override this.IsRenderbuffer(``renderbuffer`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 134us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffer``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsShader(``shader`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 135us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        len <- len + 6
    override this.IsShader(``shader`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 136us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsTexture(``texture`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 137us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``texture``
        current <- current + 4n
        len <- len + 6
    override this.IsTexture(``texture`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 138us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``texture``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.LineWidth(``width`` : float32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 139us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        len <- len + 6
    override this.LineWidth(``width`` : aptr<float32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 140us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.LinkProgram(``program`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 141us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        len <- len + 6
    override this.LinkProgram(``program`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 142us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.PixelStorei(``pname`` : PixelStoreParameter, ``param`` : int) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 143us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 10
    override this.PixelStorei(``pname`` : aptr<PixelStoreParameter>, ``param`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 144us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.PolygonOffset(``factor`` : float32, ``units`` : float32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 145us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``factor``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``units``
        current <- current + 4n
        len <- len + 10
    override this.PolygonOffset(``factor`` : aptr<float32>, ``units`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 146us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``factor``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``units``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 147us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 30
    override this.GetActiveUniform(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 148us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 30
    override this.GetAttachedShaders(``program`` : uint32, ``maxCount`` : uint32, ``count`` : nativeptr<uint32>, ``shaders`` : nativeptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 149us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``maxCount``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``shaders``
        current <- current + 4n
        len <- len + 18
    override this.GetAttachedShaders(``program`` : aptr<uint32>, ``maxCount`` : aptr<uint32>, ``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 150us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``maxCount``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shaders``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetAttribLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 151us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 10
    override this.GetAttribLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 152us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetBooleanv(``pname`` : GetPName, ``data`` : nativeptr<Boolean>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 153us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 10
    override this.GetBooleanv(``pname`` : aptr<GetPName>, ``data`` : aptr<Boolean>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 154us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetBufferParameteriv(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 155us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetBufferParameteriv(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 156us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetError(``returnValue`` : nativeptr<GLEnum>) =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 157us
        current <- current + 2n
        len <- len + 2
    override this.GetError(``returnValue`` : aptr<GLEnum>) =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 158us
        current <- current + 2n
        len <- len + 2
    override this.GetFloatv(``pname`` : GetPName, ``data`` : nativeptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 159us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 10
    override this.GetFloatv(``pname`` : aptr<GetPName>, ``data`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 160us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetFramebufferAttachmentParameteriv(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``pname`` : FramebufferAttachmentParameterName, ``params`` : nativeptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 161us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachment``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 18
    override this.GetFramebufferAttachmentParameteriv(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``pname`` : aptr<FramebufferAttachmentParameterName>, ``params`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 162us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachment``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetIntegerv(``pname`` : GetPName, ``data`` : nativeptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 163us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 10
    override this.GetIntegerv(``pname`` : aptr<GetPName>, ``data`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 164us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetProgramiv(``program`` : uint32, ``pname`` : ProgramPropertyARB, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 165us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetProgramiv(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramPropertyARB>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 166us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetProgramInfoLog(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 167us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``infoLog``
        current <- current + 4n
        len <- len + 18
    override this.GetProgramInfoLog(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 168us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``infoLog``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetRenderbufferParameteriv(``target`` : RenderbufferTarget, ``pname`` : RenderbufferParameterName, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 169us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetRenderbufferParameteriv(``target`` : aptr<RenderbufferTarget>, ``pname`` : aptr<RenderbufferParameterName>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 170us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetShaderiv(``shader`` : uint32, ``pname`` : ShaderParameterName, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 171us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetShaderiv(``shader`` : aptr<uint32>, ``pname`` : aptr<ShaderParameterName>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 172us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<AttributeType>, ``name`` : nativeptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 173us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 30
    override this.GetActiveAttrib(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<AttributeType>, ``name`` : aptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 174us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 30
    override this.DeleteBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 175us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffers``
        current <- current + 4n
        len <- len + 10
    override this.DeleteBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 176us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 177us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``framebuffers``
        current <- current + 4n
        len <- len + 10
    override this.DeleteFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 178us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``framebuffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteProgram(``program`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 179us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        len <- len + 6
    override this.DeleteProgram(``program`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 180us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DeleteRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 181us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffers``
        current <- current + 4n
        len <- len + 10
    override this.DeleteRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 182us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteShader(``shader`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 183us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        len <- len + 6
    override this.DeleteShader(``shader`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 184us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DeleteTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 185us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``textures``
        current <- current + 4n
        len <- len + 10
    override this.DeleteTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 186us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``textures``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DepthFunc(``func`` : DepthFunction) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 187us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``func``
        current <- current + 4n
        len <- len + 6
    override this.DepthFunc(``func`` : aptr<DepthFunction>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 188us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``func``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DepthMask(``flag`` : Boolean) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 189us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``flag``
        current <- current + 4n
        len <- len + 6
    override this.DepthMask(``flag`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 190us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``flag``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DepthRangef(``n`` : float32, ``f`` : float32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 191us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``f``
        current <- current + 4n
        len <- len + 10
    override this.DepthRangef(``n`` : aptr<float32>, ``f`` : aptr<float32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 192us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``f``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DetachShader(``program`` : uint32, ``shader`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 193us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        len <- len + 10
    override this.DetachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 194us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.Disable(``cap`` : EnableCap) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 195us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``cap``
        current <- current + 4n
        len <- len + 6
    override this.Disable(``cap`` : aptr<EnableCap>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 196us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``cap``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DisableVertexAttribArray(``index`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 197us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        len <- len + 6
    override this.DisableVertexAttribArray(``index`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 198us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DrawArrays(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 199us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``first``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        len <- len + 14
    override this.DrawArrays(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 200us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``first``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.DrawElements(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 201us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indices``
        current <- current + 4n
        len <- len + 18
    override this.DrawElements(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 202us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indices``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.Enable(``cap`` : EnableCap) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 203us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``cap``
        current <- current + 4n
        len <- len + 6
    override this.Enable(``cap`` : aptr<EnableCap>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 204us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``cap``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.EnableVertexAttribArray(``index`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 205us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        len <- len + 6
    override this.EnableVertexAttribArray(``index`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 206us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.Finish() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 207us
        current <- current + 2n
        len <- len + 2
    override this.Flush() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 208us
        current <- current + 2n
        len <- len + 2
    override this.FramebufferRenderbuffer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``renderbuffertarget`` : RenderbufferTarget, ``renderbuffer`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 209us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachment``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffertarget``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffer``
        current <- current + 4n
        len <- len + 18
    override this.FramebufferRenderbuffer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``renderbuffertarget`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 210us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachment``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffertarget``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffer``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.FramebufferTexture2D(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``textarget`` : TextureTarget, ``texture`` : uint32, ``level`` : int) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 211us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachment``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``textarget``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``texture``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        len <- len + 22
    override this.FramebufferTexture2D(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``textarget`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 212us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachment``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``textarget``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``texture``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.FrontFace(``mode`` : FrontFaceDirection) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 213us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        len <- len + 6
    override this.FrontFace(``mode`` : aptr<FrontFaceDirection>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 214us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.GenBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 215us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffers``
        current <- current + 4n
        len <- len + 10
    override this.GenBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 216us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenerateMipmap(``target`` : TextureTarget) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 217us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        len <- len + 6
    override this.GenerateMipmap(``target`` : aptr<TextureTarget>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 218us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.GenFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 219us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``framebuffers``
        current <- current + 4n
        len <- len + 10
    override this.GenFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 220us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``framebuffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 221us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffers``
        current <- current + 4n
        len <- len + 10
    override this.GenRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 222us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 223us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``textures``
        current <- current + 4n
        len <- len + 10
    override this.GenTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 224us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``textures``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BlendFuncSeparate(``sfactorRGB`` : BlendingFactor, ``dfactorRGB`` : BlendingFactor, ``sfactorAlpha`` : BlendingFactor, ``dfactorAlpha`` : BlendingFactor) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 225us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sfactorRGB``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dfactorRGB``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``sfactorAlpha``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dfactorAlpha``
        current <- current + 4n
        len <- len + 18
    override this.BlendFuncSeparate(``sfactorRGB`` : aptr<BlendingFactor>, ``dfactorRGB`` : aptr<BlendingFactor>, ``sfactorAlpha`` : aptr<BlendingFactor>, ``dfactorAlpha`` : aptr<BlendingFactor>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 226us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sfactorRGB``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dfactorRGB``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sfactorAlpha``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dfactorAlpha``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.BufferData(``target`` : BufferTargetARB, ``size`` : unativeint, ``data`` : nativeint, ``usage`` : BufferUsageARB) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 227us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``usage``
        current <- current + 4n
        len <- len + 18
    override this.BufferData(``target`` : aptr<BufferTargetARB>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>, ``usage`` : aptr<BufferUsageARB>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 228us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``usage``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.BufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``data`` : nativeint) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 229us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``offset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 18
    override this.BufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 230us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``offset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.CheckFramebufferStatus(``target`` : FramebufferTarget, ``returnValue`` : nativeptr<GLEnum>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 231us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        len <- len + 6
    override this.CheckFramebufferStatus(``target`` : aptr<FramebufferTarget>, ``returnValue`` : aptr<GLEnum>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 232us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.Clear(``mask`` : ClearBufferMask) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 233us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        len <- len + 6
    override this.Clear(``mask`` : aptr<ClearBufferMask>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 234us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.ClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 235us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``red``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``green``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``blue``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``alpha``
        current <- current + 4n
        len <- len + 18
    override this.ClearColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 236us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``red``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``green``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``blue``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``alpha``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.ClearDepthf(``d`` : float32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 237us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``d``
        current <- current + 4n
        len <- len + 6
    override this.ClearDepthf(``d`` : aptr<float32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 238us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``d``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.ClearStencil(``s`` : int) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 239us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``s``
        current <- current + 4n
        len <- len + 6
    override this.ClearStencil(``s`` : aptr<int>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 240us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``s``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.ColorMask(``red`` : Boolean, ``green`` : Boolean, ``blue`` : Boolean, ``alpha`` : Boolean) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 241us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``red``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``green``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``blue``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``alpha``
        current <- current + 4n
        len <- len + 18
    override this.ColorMask(``red`` : aptr<Boolean>, ``green`` : aptr<Boolean>, ``blue`` : aptr<Boolean>, ``alpha`` : aptr<Boolean>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 242us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``red``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``green``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``blue``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``alpha``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.CompileShader(``shader`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 243us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        len <- len + 6
    override this.CompileShader(``shader`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 244us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.CompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 245us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``border``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``imageSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 34
    override this.CompressedTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 246us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``border``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``imageSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 34
    override this.CompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 247us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``imageSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 38
    override this.CompressedTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 248us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``imageSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 38
    override this.CopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 249us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``border``
        current <- current + 4n
        len <- len + 34
    override this.CopyTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 250us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``border``).Pointer)
        current <- current + 4n
        len <- len + 34
    override this.CopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 251us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 34
    override this.CopyTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 34
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 252us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 34
    override this.CreateProgram(``returnValue`` : nativeptr<uint32>) =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 253us
        current <- current + 2n
        len <- len + 2
    override this.CreateProgram(``returnValue`` : aptr<uint32>) =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 254us
        current <- current + 2n
        len <- len + 2
    override this.CreateShader(``type`` : ShaderType, ``returnValue`` : nativeptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 255us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        len <- len + 6
    override this.CreateShader(``type`` : aptr<ShaderType>, ``returnValue`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 256us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.CullFace(``mode`` : CullFaceMode) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 257us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        len <- len + 6
    override this.CullFace(``mode`` : aptr<CullFaceMode>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 258us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.TransformFeedbackVaryings(``program`` : uint32, ``count`` : uint32, ``varyings`` : nativeptr<nativeptr<uint8>>, ``bufferMode`` : TransformFeedbackBufferMode) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 259us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``varyings``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufferMode``
        current <- current + 4n
        len <- len + 18
    override this.TransformFeedbackVaryings(``program`` : aptr<uint32>, ``count`` : aptr<uint32>, ``varyings`` : aptr<nativeptr<uint8>>, ``bufferMode`` : aptr<TransformFeedbackBufferMode>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 260us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``varyings``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufferMode``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.Uniform1ui(``location`` : int, ``v0`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 261us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        len <- len + 10
    override this.Uniform1ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 262us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.Uniform1uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 263us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform1uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 264us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform2ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 265us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 266us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform2uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 267us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform2uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 268us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform3ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 269us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        len <- len + 18
    override this.Uniform3ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 270us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.Uniform3uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 271us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform3uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 272us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.Uniform4ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32, ``v3`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 273us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v2``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v3``
        current <- current + 4n
        len <- len + 22
    override this.Uniform4ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>, ``v3`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 274us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v2``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v3``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.Uniform4uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 275us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.Uniform4uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 276us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.UniformBlockBinding(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``uniformBlockBinding`` : uint32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 277us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockIndex``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockBinding``
        current <- current + 4n
        len <- len + 14
    override this.UniformBlockBinding(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``uniformBlockBinding`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 278us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockIndex``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockBinding``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.UniformMatrix2x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 279us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix2x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 280us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix2x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 281us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix2x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 282us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 283us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 284us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 285us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix3x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 286us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 287us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 288us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 289us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``transpose``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 18
    override this.UniformMatrix4x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 290us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``transpose``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.VertexAttribDivisor(``index`` : uint32, ``divisor`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 291us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``divisor``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribDivisor(``index`` : aptr<uint32>, ``divisor`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 292us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``divisor``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribI4i(``index`` : uint32, ``x`` : int, ``y`` : int, ``z`` : int, ``w`` : int) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 293us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``z``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``w``
        current <- current + 4n
        len <- len + 22
    override this.VertexAttribI4i(``index`` : aptr<uint32>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``z`` : aptr<int>, ``w`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 294us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``z``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``w``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.VertexAttribI4ui(``index`` : uint32, ``x`` : uint32, ``y`` : uint32, ``z`` : uint32, ``w`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 295us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``z``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``w``
        current <- current + 4n
        len <- len + 22
    override this.VertexAttribI4ui(``index`` : aptr<uint32>, ``x`` : aptr<uint32>, ``y`` : aptr<uint32>, ``z`` : aptr<uint32>, ``w`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 296us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``z``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``w``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.VertexAttribI4iv(``index`` : uint32, ``v`` : nativeptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 297us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribI4iv(``index`` : aptr<uint32>, ``v`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 298us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribI4uiv(``index`` : uint32, ``v`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 299us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``v``
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribI4uiv(``index`` : aptr<uint32>, ``v`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 300us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``v``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.VertexAttribIPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribIType, ``stride`` : uint32, ``pointer`` : nativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 301us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``stride``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pointer``
        current <- current + 4n
        len <- len + 22
    override this.VertexAttribIPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribIType>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<_>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 302us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``stride``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pointer``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.WaitSync(``sync`` : nativeint, ``flags`` : SyncBehaviorFlags, ``timeout`` : uint64) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 303us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sync``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``flags``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``timeout``
        current <- current + 8n
        len <- len + 18
    override this.WaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncBehaviorFlags>, ``timeout`` : aptr<uint64>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 304us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sync``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``flags``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``timeout``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ActiveTexture(``texture`` : TextureUnit) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 305us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``texture``
        current <- current + 4n
        len <- len + 6
    override this.ActiveTexture(``texture`` : aptr<TextureUnit>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 306us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``texture``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.AttachShader(``program`` : uint32, ``shader`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 307us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``shader``
        current <- current + 4n
        len <- len + 10
    override this.AttachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 308us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``shader``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindAttribLocation(``program`` : uint32, ``index`` : uint32, ``name`` : nativeptr<uint8>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 309us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 14
    override this.BindAttribLocation(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``name`` : aptr<uint8>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 310us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.BindBuffer(``target`` : BufferTargetARB, ``buffer`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 311us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        len <- len + 10
    override this.BindBuffer(``target`` : aptr<BufferTargetARB>, ``buffer`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 312us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindFramebuffer(``target`` : FramebufferTarget, ``framebuffer`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 313us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``framebuffer``
        current <- current + 4n
        len <- len + 10
    override this.BindFramebuffer(``target`` : aptr<FramebufferTarget>, ``framebuffer`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 314us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``framebuffer``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindRenderbuffer(``target`` : RenderbufferTarget, ``renderbuffer`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 315us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``renderbuffer``
        current <- current + 4n
        len <- len + 10
    override this.BindRenderbuffer(``target`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 316us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``renderbuffer``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindTexture(``target`` : TextureTarget, ``texture`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 317us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``texture``
        current <- current + 4n
        len <- len + 10
    override this.BindTexture(``target`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 318us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``texture``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 319us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``red``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``green``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``blue``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``alpha``
        current <- current + 4n
        len <- len + 18
    override this.BlendColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 320us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``red``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``green``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``blue``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``alpha``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.BlendEquation(``mode`` : BlendEquationModeEXT) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 321us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        len <- len + 6
    override this.BlendEquation(``mode`` : aptr<BlendEquationModeEXT>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 322us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.BlendEquationSeparate(``modeRGB`` : BlendEquationModeEXT, ``modeAlpha`` : BlendEquationModeEXT) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 323us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``modeRGB``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``modeAlpha``
        current <- current + 4n
        len <- len + 10
    override this.BlendEquationSeparate(``modeRGB`` : aptr<BlendEquationModeEXT>, ``modeAlpha`` : aptr<BlendEquationModeEXT>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 324us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``modeRGB``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``modeAlpha``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BlendFunc(``sfactor`` : BlendingFactor, ``dfactor`` : BlendingFactor) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 325us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sfactor``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dfactor``
        current <- current + 4n
        len <- len + 10
    override this.BlendFunc(``sfactor`` : aptr<BlendingFactor>, ``dfactor`` : aptr<BlendingFactor>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 326us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sfactor``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dfactor``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.SamplerParameteri(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : int) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 327us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameteri(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 328us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 329us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 330us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 331us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameterf(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 332us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 333us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``param``
        current <- current + 4n
        len <- len + 14
    override this.SamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 334us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``param``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.TexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let e = len + 42
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 335us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``border``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pixels``
        current <- current + 4n
        len <- len + 42
    override this.TexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) =
        let e = len + 42
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 336us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``border``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pixels``).Pointer)
        current <- current + 4n
        len <- len + 42
    override this.TexStorage2D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 337us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``levels``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 22
    override this.TexStorage2D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 338us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``levels``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.TexStorage3D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 339us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``levels``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        len <- len + 26
    override this.TexStorage3D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 340us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``levels``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        len <- len + 26
    override this.TexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) =
        let e = len + 46
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 341us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``zoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pixels``
        current <- current + 4n
        len <- len + 46
    override this.TexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) =
        let e = len + 46
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 342us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``zoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pixels``).Pointer)
        current <- current + 4n
        len <- len + 46
    override this.GetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<AttributeType>, ``name`` : nativeptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 343us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 30
    override this.GetTransformFeedbackVarying(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<uint32>, ``type`` : aptr<AttributeType>, ``name`` : aptr<uint8>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 344us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 30
    override this.GetUniformuiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 345us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``location``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetUniformuiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 346us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``location``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetUniformBlockIndex(``program`` : uint32, ``uniformBlockName`` : nativeptr<uint8>, ``returnValue`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 347us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockName``
        current <- current + 4n
        len <- len + 10
    override this.GetUniformBlockIndex(``program`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>, ``returnValue`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 348us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockName``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetUniformIndices(``program`` : uint32, ``uniformCount`` : uint32, ``uniformNames`` : nativeptr<nativeptr<uint8>>, ``uniformIndices`` : nativeptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 349us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformCount``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformNames``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformIndices``
        current <- current + 4n
        len <- len + 18
    override this.GetUniformIndices(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformNames`` : aptr<nativeptr<uint8>>, ``uniformIndices`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 350us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformCount``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformNames``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformIndices``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetVertexAttribIiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 351us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribIiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 352us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribIuiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 353us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetVertexAttribIuiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 354us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.InvalidateFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 355us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``numAttachments``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachments``
        current <- current + 4n
        len <- len + 14
    override this.InvalidateFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 356us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``numAttachments``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachments``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.InvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 357us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``numAttachments``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachments``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 30
    override this.InvalidateSubFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 30
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 358us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``numAttachments``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachments``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 30
    override this.IsQuery(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 359us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``id``
        current <- current + 4n
        len <- len + 6
    override this.IsQuery(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 360us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``id``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsSampler(``sampler`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 361us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        len <- len + 6
    override this.IsSampler(``sampler`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 362us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsSync(``sync`` : nativeint, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 363us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sync``
        current <- current + 4n
        len <- len + 6
    override this.IsSync(``sync`` : aptr<nativeint>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 364us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sync``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsTransformFeedback(``id`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 365us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``id``
        current <- current + 4n
        len <- len + 6
    override this.IsTransformFeedback(``id`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 366us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``id``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.IsVertexArray(``array`` : uint32, ``returnValue`` : nativeptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 367us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``array``
        current <- current + 4n
        len <- len + 6
    override this.IsVertexArray(``array`` : aptr<uint32>, ``returnValue`` : aptr<Boolean>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 368us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``array``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.PauseTransformFeedback() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 369us
        current <- current + 2n
        len <- len + 2
    override this.ProgramBinary(``program`` : uint32, ``binaryFormat`` : GLEnum, ``binary`` : nativeint, ``length`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 370us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binaryFormat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binary``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        len <- len + 18
    override this.ProgramBinary(``program`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<_>, ``length`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 371us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binaryFormat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binary``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.ProgramParameteri(``program`` : uint32, ``pname`` : ProgramParameterPName, ``value`` : int) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 372us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.ProgramParameteri(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramParameterPName>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 373us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ReadBuffer(``src`` : ReadBufferMode) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 374us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``src``
        current <- current + 4n
        len <- len + 6
    override this.ReadBuffer(``src`` : aptr<ReadBufferMode>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 375us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``src``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.RenderbufferStorageMultisample(``target`` : RenderbufferTarget, ``samples`` : uint32, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 376us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``samples``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 22
    override this.RenderbufferStorageMultisample(``target`` : aptr<RenderbufferTarget>, ``samples`` : aptr<uint32>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 377us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``samples``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.ResumeTransformFeedback() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 378us
        current <- current + 2n
        len <- len + 2
    override this.GetInteger64v(``pname`` : GetPName, ``data`` : nativeptr<int64>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 379us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 10
    override this.GetInteger64v(``pname`` : aptr<GetPName>, ``data`` : aptr<int64>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 380us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetInteger64i_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int64>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 381us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 14
    override this.GetInteger64i_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int64>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 382us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetInternalformativ(``target`` : TextureTarget, ``internalformat`` : InternalFormat, ``pname`` : InternalFormatPName, ``count`` : uint32, ``params`` : nativeptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 383us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 22
    override this.GetInternalformativ(``target`` : aptr<TextureTarget>, ``internalformat`` : aptr<InternalFormat>, ``pname`` : aptr<InternalFormatPName>, ``count`` : aptr<uint32>, ``params`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 384us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.GetProgramBinary(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``binaryFormat`` : nativeptr<GLEnum>, ``binary`` : nativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 385us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binaryFormat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``binary``
        current <- current + 4n
        len <- len + 22
    override this.GetProgramBinary(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<_>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 386us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binaryFormat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``binary``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.GetQueryiv(``target`` : QueryTarget, ``pname`` : QueryParameterName, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 387us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetQueryiv(``target`` : aptr<QueryTarget>, ``pname`` : aptr<QueryParameterName>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 388us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetQueryObjectuiv(``id`` : uint32, ``pname`` : QueryObjectParameterName, ``params`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 389us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``id``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetQueryObjectuiv(``id`` : aptr<uint32>, ``pname`` : aptr<QueryObjectParameterName>, ``params`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 390us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``id``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetSamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``params`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 391us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetSamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``params`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 392us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetSamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``params`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 393us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetSamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``params`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 394us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetStringi(``name`` : StringName, ``index`` : uint32, ``returnValue`` : nativeptr<nativeptr<uint8>>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 395us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        len <- len + 10
    override this.GetStringi(``name`` : aptr<StringName>, ``index`` : aptr<uint32>, ``returnValue`` : aptr<nativeptr<uint8>>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 396us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetSynciv(``sync`` : nativeint, ``pname`` : SyncParameterName, ``count`` : uint32, ``length`` : nativeptr<uint32>, ``values`` : nativeptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 397us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sync``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``values``
        current <- current + 4n
        len <- len + 22
    override this.GetSynciv(``sync`` : aptr<nativeint>, ``pname`` : aptr<SyncParameterName>, ``count`` : aptr<uint32>, ``length`` : aptr<uint32>, ``values`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 398us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sync``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``values``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.DrawElementsInstanced(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint, ``instancecount`` : uint32) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 399us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indices``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``instancecount``
        current <- current + 4n
        len <- len + 22
    override this.DrawElementsInstanced(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>, ``instancecount`` : aptr<uint32>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 400us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indices``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``instancecount``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.DrawRangeElements(``mode`` : PrimitiveType, ``start`` : uint32, ``end`` : uint32, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 401us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``start``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``end``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``type``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indices``
        current <- current + 4n
        len <- len + 26
    override this.DrawRangeElements(``mode`` : aptr<PrimitiveType>, ``start`` : aptr<uint32>, ``end`` : aptr<uint32>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) =
        let e = len + 26
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 402us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``start``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``end``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``type``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indices``).Pointer)
        current <- current + 4n
        len <- len + 26
    override this.EndQuery(``target`` : QueryTarget) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 403us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        len <- len + 6
    override this.EndQuery(``target`` : aptr<QueryTarget>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 404us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.EndTransformFeedback() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 405us
        current <- current + 2n
        len <- len + 2
    override this.FenceSync(``condition`` : SyncCondition, ``flags`` : SyncBehaviorFlags, ``returnValue`` : nativeptr<nativeint>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 406us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``condition``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``flags``
        current <- current + 4n
        len <- len + 10
    override this.FenceSync(``condition`` : aptr<SyncCondition>, ``flags`` : aptr<SyncBehaviorFlags>, ``returnValue`` : aptr<nativeint>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 407us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``condition``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``flags``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.FramebufferTextureLayer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``texture`` : uint32, ``level`` : int, ``layer`` : int) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 408us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``attachment``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``texture``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``layer``
        current <- current + 4n
        len <- len + 22
    override this.FramebufferTextureLayer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>, ``layer`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 409us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``attachment``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``texture``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``layer``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.GenQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 410us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ids``
        current <- current + 4n
        len <- len + 10
    override this.GenQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 411us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ids``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 412us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``samplers``
        current <- current + 4n
        len <- len + 10
    override this.GenSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 413us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``samplers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 414us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ids``
        current <- current + 4n
        len <- len + 10
    override this.GenTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 415us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ids``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GenVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 416us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``arrays``
        current <- current + 4n
        len <- len + 10
    override this.GenVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 417us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``arrays``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetActiveUniformBlockiv(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``pname`` : UniformBlockPName, ``params`` : nativeptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 418us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockIndex``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 18
    override this.GetActiveUniformBlockiv(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``pname`` : aptr<UniformBlockPName>, ``params`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 419us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockIndex``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.GetActiveUniformBlockName(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``uniformBlockName`` : nativeptr<uint8>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 420us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockIndex``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``length``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformBlockName``
        current <- current + 4n
        len <- len + 22
    override this.GetActiveUniformBlockName(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 421us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockIndex``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``length``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformBlockName``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.GetActiveUniformsiv(``program`` : uint32, ``uniformCount`` : uint32, ``uniformIndices`` : nativeptr<uint32>, ``pname`` : UniformPName, ``params`` : nativeptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 422us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformCount``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``uniformIndices``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 22
    override this.GetActiveUniformsiv(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformIndices`` : aptr<uint32>, ``pname`` : aptr<UniformPName>, ``params`` : aptr<int>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 423us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformCount``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``uniformIndices``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.GetBufferParameteri64v(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int64>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 424us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``pname``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``params``
        current <- current + 4n
        len <- len + 14
    override this.GetBufferParameteri64v(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int64>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 425us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``pname``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``params``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.GetFragDataLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``returnValue`` : nativeptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 426us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``program``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``name``
        current <- current + 4n
        len <- len + 10
    override this.GetFragDataLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``returnValue`` : aptr<int>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 427us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``program``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``name``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.GetIntegeri_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 428us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 14
    override this.GetIntegeri_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 429us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.BindBufferBase(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 430us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        len <- len + 14
    override this.BindBufferBase(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 431us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.BindBufferRange(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32, ``offset`` : nativeint, ``size`` : unativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 432us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``index``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``offset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        len <- len + 22
    override this.BindBufferRange(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 433us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``index``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``offset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.BindSampler(``unit`` : uint32, ``sampler`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 434us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``unit``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``sampler``
        current <- current + 4n
        len <- len + 10
    override this.BindSampler(``unit`` : aptr<uint32>, ``sampler`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 435us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``unit``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sampler``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindTransformFeedback(``target`` : BindTransformFeedbackTarget, ``id`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 436us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``id``
        current <- current + 4n
        len <- len + 10
    override this.BindTransformFeedback(``target`` : aptr<BindTransformFeedbackTarget>, ``id`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 437us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``id``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BindVertexArray(``array`` : uint32) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 438us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``array``
        current <- current + 4n
        len <- len + 6
    override this.BindVertexArray(``array`` : aptr<uint32>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 439us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``array``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.BlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) =
        let e = len + 42
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 440us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``srcX0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``srcY0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``srcX1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``srcY1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dstX0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dstY0``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dstX1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dstY1``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``mask``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``filter``
        current <- current + 4n
        len <- len + 42
    override this.BlitFramebuffer(``srcX0`` : aptr<int>, ``srcY0`` : aptr<int>, ``srcX1`` : aptr<int>, ``srcY1`` : aptr<int>, ``dstX0`` : aptr<int>, ``dstY0`` : aptr<int>, ``dstX1`` : aptr<int>, ``dstY1`` : aptr<int>, ``mask`` : aptr<ClearBufferMask>, ``filter`` : aptr<BlitFramebufferFilter>) =
        let e = len + 42
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 441us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``srcX0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``srcY0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``srcX1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``srcY1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dstX0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dstY0``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dstX1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dstY1``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mask``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``filter``).Pointer)
        current <- current + 4n
        len <- len + 42
    override this.ClearBufferiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 442us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``drawbuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<int>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 443us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``drawbuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferuiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 444us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``drawbuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferuiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<uint32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 445us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``drawbuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferfv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 446us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``drawbuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``value``
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferfv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<float32>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 447us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``drawbuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``value``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.ClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 448us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``buffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``drawbuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``stencil``
        current <- current + 4n
        len <- len + 18
    override this.ClearBufferfi(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``depth`` : aptr<float32>, ``stencil`` : aptr<int>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 449us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``buffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``drawbuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``stencil``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.ClientWaitSync(``sync`` : nativeint, ``flags`` : SyncObjectMask, ``timeout`` : uint64, ``returnValue`` : nativeptr<GLEnum>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 450us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sync``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``flags``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``timeout``
        current <- current + 8n
        len <- len + 18
    override this.ClientWaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncObjectMask>, ``timeout`` : aptr<uint64>, ``returnValue`` : aptr<GLEnum>) =
        let e = len + 14
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 451us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sync``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``flags``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``timeout``).Pointer)
        current <- current + 4n
        len <- len + 14
    override this.CompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 452us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``internalformat``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``border``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``imageSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 38
    override this.CompressedTexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 453us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``internalformat``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``border``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``imageSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 38
    override this.CompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) =
        let e = len + 46
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 454us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``zoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``depth``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``imageSize``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``data``
        current <- current + 4n
        len <- len + 46
    override this.CompressedTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) =
        let e = len + 46
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 455us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``zoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``depth``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``imageSize``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``data``).Pointer)
        current <- current + 4n
        len <- len + 46
    override this.CopyBufferSubData(``readTarget`` : CopyBufferSubDataTarget, ``writeTarget`` : CopyBufferSubDataTarget, ``readOffset`` : nativeint, ``writeOffset`` : nativeint, ``size`` : unativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 456us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``readTarget``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``writeTarget``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``readOffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``writeOffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        len <- len + 22
    override this.CopyBufferSubData(``readTarget`` : aptr<CopyBufferSubDataTarget>, ``writeTarget`` : aptr<CopyBufferSubDataTarget>, ``readOffset`` : aptr<nativeint>, ``writeOffset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 457us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``readTarget``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``writeTarget``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``readOffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``writeOffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.CopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 458us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``zoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``x``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``y``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        len <- len + 38
    override this.CopyTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 459us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``zoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``x``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``y``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        len <- len + 38
    override this.DeleteQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 460us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ids``
        current <- current + 4n
        len <- len + 10
    override this.DeleteQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 461us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ids``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 462us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``samplers``
        current <- current + 4n
        len <- len + 10
    override this.DeleteSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 463us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``samplers``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteSync(``sync`` : nativeint) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 464us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``sync``
        current <- current + 4n
        len <- len + 6
    override this.DeleteSync(``sync`` : aptr<nativeint>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 465us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``sync``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.DeleteTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 466us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``ids``
        current <- current + 4n
        len <- len + 10
    override this.DeleteTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 467us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``ids``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DeleteVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 468us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``arrays``
        current <- current + 4n
        len <- len + 10
    override this.DeleteVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 469us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``arrays``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.DrawArraysInstanced(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32, ``instancecount`` : uint32) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 470us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``first``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``instancecount``
        current <- current + 4n
        len <- len + 18
    override this.DrawArraysInstanced(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>, ``instancecount`` : aptr<uint32>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 471us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``first``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``instancecount``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.DrawBuffers(``n`` : uint32, ``bufs`` : nativeptr<GLEnum>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 472us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``n``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bufs``
        current <- current + 4n
        len <- len + 10
    override this.DrawBuffers(``n`` : aptr<uint32>, ``bufs`` : aptr<nativeptr<GLEnum>>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 473us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``n``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bufs``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BeginQuery(``target`` : QueryTarget, ``id`` : uint32) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 474us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``id``
        current <- current + 4n
        len <- len + 10
    override this.BeginQuery(``target`` : aptr<QueryTarget>, ``id`` : aptr<uint32>) =
        let e = len + 10
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 475us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``id``).Pointer)
        current <- current + 4n
        len <- len + 10
    override this.BeginTransformFeedback(``primitiveMode`` : PrimitiveType) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 476us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``primitiveMode``
        current <- current + 4n
        len <- len + 6
    override this.BeginTransformFeedback(``primitiveMode`` : aptr<PrimitiveType>) =
        let e = len + 6
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 477us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``primitiveMode``).Pointer)
        current <- current + 4n
        len <- len + 6
    override this.GetBufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``dst`` : nativeint) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 478us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``offset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``size``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``dst``
        current <- current + 4n
        len <- len + 18
    override this.GetBufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``dst`` : aptr<nativeint>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 479us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``offset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``size``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``dst``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.MultiDrawArraysIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``bindingInfo`` : nativeint) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 480us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indirectBuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bindingInfo``
        current <- current + 4n
        len <- len + 18
    override this.MultiDrawArraysIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 481us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indirectBuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bindingInfo``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.MultiDrawArrays(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``bindingInfo`` : nativeint) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 482us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indirect``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bindingInfo``
        current <- current + 4n
        len <- len + 18
    override this.MultiDrawArrays(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) =
        let e = len + 18
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 483us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indirect``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bindingInfo``).Pointer)
        current <- current + 4n
        len <- len + 18
    override this.MultiDrawElementsIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 484us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indirectBuffer``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indexType``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bindingInfo``
        current <- current + 4n
        len <- len + 22
    override this.MultiDrawElementsIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 485us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indirectBuffer``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indexType``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bindingInfo``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.MultiDrawElements(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 486us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``mode``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indirect``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``count``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``indexType``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``bindingInfo``
        current <- current + 4n
        len <- len + 22
    override this.MultiDrawElements(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) =
        let e = len + 22
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 487us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``mode``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indirect``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``count``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``indexType``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``bindingInfo``).Pointer)
        current <- current + 4n
        len <- len + 22
    override this.Commit() =
        let e = len + 2
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 488us
        current <- current + 2n
        len <- len + 2
    override this.TexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 489us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) ``target``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``level``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``xoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``yoffset``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``width``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``height``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``format``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``typ``
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) ``imgHandle``
        current <- current + 4n
        len <- len + 38
    override this.TexSubImage2DJSImage(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<int>, ``height`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``typ`` : aptr<PixelType>, ``imgHandle`` : aptr<int>) =
        let e = len + 38
        if e > capacity then resize (Fun.NextPowerOfTwo e)
        NativePtr.write (NativePtr.ofNativeInt current) 490us
        current <- current + 2n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``target``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``level``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``xoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``yoffset``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``width``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``height``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``format``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``typ``).Pointer)
        current <- current + 4n
        NativePtr.write (NativePtr.ofNativeInt current) (this.Use(``imgHandle``).Pointer)
        current <- current + 4n
        len <- len + 38
    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) =
        ensureFree 14
        x.Write(512us)
        x.Write(x.Use(src).Pointer)
        x.Write(x.Use(dst).Pointer)
        x.Write(x.Use(size).Pointer)
    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        ensureFree 14
        x.Write(513us)
        x.Write(x.Use(src).Pointer)
        x.Write(x.Use(dst).Pointer)
        x.Write(x.Use(size).Pointer)
    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) =
        ensureFree 14
        x.Write(514us)
        x.Write(x.Use(src).Pointer)
        x.Write(x.Use(dst).Pointer)
        x.Write(x.Use(size).Pointer)
    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) =
        ensureFree 14
        x.Write(515us)
        x.Write(x.Use(src).Pointer)
        x.Write(x.Use(dst).Pointer)
        x.Write(x.Use(size).Pointer)
    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) =
        ensureFree 14
        x.Write(518us)
        x.Write(src)
        x.Write(dst)
        x.Write(size)
    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) =
        ensureFree 14
        x.Write(516us)
        x.Write(x.Use(a).Pointer)
        x.Write(x.Use(b).Pointer)
        x.Write(x.Use(res).Pointer)
    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) =
        ensureFree 18
        x.Write(517us)
        x.Write(x.Use(a).Pointer)
        x.Write(x.Use(b).Pointer)
        x.Write(x.Use(c).Pointer)
        x.Write(x.Use(res).Pointer)
    override x.Custom(action : GL -> unit) =
        let ptr = x.Use(APtr.pinDelegate (System.Action(fun () -> action currentGL)))
        ensureFree 6
        x.Write(519us)
        x.Write(ptr.Pointer)
    override x.Bgra(values : aptr<byte>, count : aptr<int>) = 
        ensureFree 8
        x.Write(531us)
        x.Write(x.Use(values).Pointer)
        x.Write(x.Use(count).Pointer)
    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = 
        ensureFree 12
        x.Write(532us)
        x.Write(x.Use(src).Pointer)
        x.Write(x.Use(dst).Pointer)
        x.Write(x.Use(count).Pointer)
    override x.Push(value : nativeptr<'a>) =
        match sizeof<'a> with
        | 1 -> x.Write(523us); x.Write(NativePtr.toNativeInt value)
        | 2 -> x.Write(525us); x.Write(NativePtr.toNativeInt value)
        | 4 -> x.Write(527us); x.Write(NativePtr.toNativeInt value)
        | 8 -> x.Write(529us); x.Write(NativePtr.toNativeInt value)
        | _ -> failwith "not implemented"
    override x.Pop(value : nativeptr<'a>) =
        match sizeof<'a> with
        | 1 -> x.Write(524us); x.Write(NativePtr.toNativeInt value)
        | 2 -> x.Write(526us); x.Write(NativePtr.toNativeInt value)
        | 4 -> x.Write(528us); x.Write(NativePtr.toNativeInt value)
        | 8 -> x.Write(530us); x.Write(NativePtr.toNativeInt value)
        | _ -> failwith "not implemented"
    member x.Log(str : string) =
        x.Write(522us)
        let data = System.Text.Encoding.UTF8.GetBytes(str)
        x.Write (data.Length + 1)
        for b in data do x.Write b
        x.Write 0uy
    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, otherwise : CommandEncoder -> unit) =
        let cases = List.toArray cases
        ensureFree (10 + 8*cases.Length)
        x.Write(520us)
        x.Write(x.Use(location).Pointer)
        x.Write(cases.Length)
        let caseOffsets =
            cases |> Array.map (fun (i, e) ->
                x.Write(i)
                let o = current - mem
                x.Write(0n)
                o
            )
        otherwise x
        let endjumps = System.Collections.Generic.List<nativeint>()
        x.Write(521us)
        endjumps.Add (current - mem)
        x.Write 0n
        for i in 0 .. cases.Length - 1 do
            let (k, a) = cases.[i]
            let pi = current - mem
            let pj = caseOffsets.[i]
            NativePtr.write (NativePtr.ofNativeInt (mem + pj)) (pi - pj - 4n)
            a x
            x.Write(521us)
            endjumps.Add (current - mem)
            x.Write 0n
        let fin = current - mem
        for o in endjumps do
            NativePtr.write (NativePtr.ofNativeInt (mem + o)) (fin - o)
    override x.Call(func : aptr<nativeint>) : unit =
        x.Write(519us)
        x.Write(x.Use(func).Pointer)
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>) : unit = failwith "not implemented"
    override __.Call(func : aptr<nativeint>, arg0 : aptr<'a>, arg1 : aptr<'b>, arg2 : aptr<'c>, arg3 : aptr<'d>, arg4 : aptr<'e>, arg5 : aptr<'f>, arg6 : aptr<'g>, arg7 : aptr<'h>) : unit = failwith "not implemented"
    override x.Destroy() = 
        Marshal.FreeHGlobal mem
        mem <- 0n
    override x.Clear() =
        Marshal.FreeHGlobal mem
        capacity <- 128
        mem <- Marshal.AllocHGlobal capacity
        current <- mem
        len <- 0
    override x.Perform(gl : GL) = 
        currentGL <- gl
        use stack = fixed (Array.zeroCreate<int> 512)
        if Interpreter.emInterpret(mem, len, NativePtr.toNativeInt stack) <> 0 then failwith "interpreter failed"
