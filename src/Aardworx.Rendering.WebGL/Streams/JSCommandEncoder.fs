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
open Aardworx.Rendering.WebGL
open Aardworx.WebAssembly
open Microsoft.JSInterop
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
open System.Text

#nowarn "9"
type JSCommandEncoder(device : Device) =
    inherit CommandEncoder(device)
    static let dstr (v : float) = v.ToString("r", System.Globalization.CultureInfo.InvariantCulture)
    static let fstr (v : float32) = v.ToString("r", System.Globalization.CultureInfo.InvariantCulture)
    static let bstr (v : Boolean) = match v with | Boolean.True -> "1" | _ -> "0"
    let commands = StringBuilder()
    let mutable cachedAction = None
    let mutable currentGL = Unchecked.defaultof<GL>
    
    let mutable currentId = 1
    let cleanup = System.Collections.Generic.List<unit -> unit>()
    
    
    let newName() =
        let id = currentId
        let res = sprintf "o%04d" id
        currentId <- currentId + 1
        res
    
    let getAction() =
        match cachedAction with
        | Some a -> a
        | None ->
            let code = sprintf "return { run: (self) => { %s } };" (string commands)
            let a = JsObj.Evaluate<IJSInProcessObjectReference> code
            cachedAction <- Some a
            a
    
    let run(self : JsObj) =
        let action = getAction()
        action.InvokeVoid("run", self.Reference :> obj)
    
    let appendCommands (str : string[]) =
        cachedAction <- None
        for str in str do
            commands.AppendFormat("{0}\n", str) |> ignore
    let appendCommand (str : string) =
        cachedAction <- None
        commands.AppendFormat("{0}\n", str) |> ignore
    
    override this.Destroy() =
        for c in cleanup do c()
        cleanup.Clear()
        commands.Clear() |> ignore
        cachedAction <- None
    
    override this.Clear() =
        for c in cleanup do c()
        cleanup.Clear()
        commands.Clear() |> ignore
        cachedAction <- None
    
    override x.Begin() =
        appendCommand "if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }"
        
    override x.End() =
        ()
    
    override this.Perform(gl) =
        try
            currentGL <- gl
            let self = JsObj.New []
            run(self)
        finally
            currentGL <- Unchecked.defaultof<GL>

    override this.Add(a,b,res) =
        let a = this.Use a
        let b = this.Use b
        let res = this.Use res
        
        let resOffset = int (res.Pointer / 4n)
        let aOffset = int (a.Pointer / 4n)
        let bOffset = int (b.Pointer / 4n)
        appendCommand $"Module.HEAP32[{resOffset}] = Module.HEAP32[{aOffset}] + Module.HEAP32[{bOffset}];"
     
    override this.Mad(a,b,c,res) =
        let a = this.Use a
        let b = this.Use b
        let c = this.Use c
        let res = this.Use res
        
        let resOffset = int (res.Pointer / 4n)
        let aOffset = int (a.Pointer / 4n)
        let bOffset = int (b.Pointer / 4n)
        let cOffset = int (c.Pointer / 4n)
        appendCommand $"Module.HEAP32[{resOffset}] = Module.HEAP32[{aOffset}] + Module.HEAP32[{bOffset}] * Module.HEAP32[{cOffset}];"

    override this.Bgra(colors,count) =
        let colors = this.Use colors
        let count = this.Use count
        
        appendCommands [|
            "{"
            $"  const count = Module.HEAP32[{int (count.Pointer / 4n)}];"
            $"  let offset = {int colors.Pointer};"
            $"  for(let i = 0; i < count; i++) {{"
            $"    const r = Module.HEAPU8[offset];"
            $"    Module.HEAPU8[offset] = Module.HEAPU8[offset+2];"
            $"    Module.HEAPU8[offset+2] = r;"
            $"    offset += 4;"
            $"  }}"
            "}"
        |]  
        
    override this.CopyBgra(src,dst,count) =
        let src = this.Use src
        let dst = this.Use dst
        let count = this.Use count
        
        appendCommands [|
            "{"
            $"  const count = Module.HEAP32[{int (count.Pointer / 4n)}];"
            $"  let srcOff = {int src.Pointer};"
            $"  let dstOff = {int dst.Pointer};"
            $"  for(let i = 0; i < count; i++) {{"
            $"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 2];"
            $"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 1];"
            $"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff];"
            $"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 3];"
            $"    srcOff += 4;"
            $"  }}"
            "}"
        |]  

    override this.Copy(src,dst,size) =
        appendCommands [|
            "{"
            $"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src}, {int size});"
            $"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst}, {int size});"
            $"  b.set(a);"
            "}"
        |]
    
    override this.CopyDD(src,dst,size) =
        let src = this.Use src
        let dst = this.Use dst
        let size = this.Use size
        appendCommands [|
            "{"
            $"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];"
            $"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src.Pointer}, size);"
            $"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst.Pointer}, size);"
            $"  b.set(a);"
            "}"
        |]
        
    override this.CopyDI(src,dst,size) =
        let src = this.Use src
        let dst = this.Use dst
        let size = this.Use size
        appendCommands [|
            "{"
            $"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];"
            $"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src.Pointer}, size);"
            $"  let b = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (dst.Pointer / 4n)}], size);"
            $"  b.set(a);"
            "}"
        |]
        
    override this.CopyID(src,dst,size) = 
        let src = this.Use src
        let dst = this.Use dst
        let size = this.Use size
        appendCommands [|
            "{"
            $"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];"
            $"  let a = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (src.Pointer / 4n)}], size);"
            $"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst.Pointer}, size);"
            $"  b.set(a);"
            "}"
        |]
        
    override this.CopyII(src,dst,size) = 
        let src = this.Use src
        let dst = this.Use dst
        let size = this.Use size
        appendCommands [|
            "{"
            $"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];"
            $"  let a = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (src.Pointer / 4n)}], size);"
            $"  let b = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (dst.Pointer / 4n)}], size);"
            $"  b.set(a);"
            "}" 
        |]
        
    member this.JS(code : string[]) =
        appendCommands code
        
    member this.Action = getAction()
    override this.Custom(action) = failwith "custom not implemented"
    override this.Pop(mem : nativeptr<'a>) =
        let s = sizeof<'a>
        appendCommands [|
            "{"
            "  if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }"
            $"  self.stackOffset -= {s};"
            $"  let src = new Uint8Array(self.stack, self.stackOffset, {s});"
            $"  let dst = new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s});"
            "  dst.set(src);"
            "}"
        |]
        
    override this.Push(mem : nativeptr<'a>) =
        let s = sizeof<'a>
        appendCommands [|
            "{"
            "  if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }"
            $"  let src = new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s});"
            $"  let dst = new Uint8Array(self.stack, self.stackOffset, {s});"
            "  dst.set(src);"
            $"  self.stackOffset += {s};"
            "}"
        |]
    override this.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) =
        let location = this.Use(location).Pointer
        match cases with
        | [] -> fallback this
        | [(v, ifTrue)] ->
            appendCommand $"if(Module.HEAP32[{int (location / 4n)}] == {v}) {{"
            ifTrue this
            appendCommands [| "}"; "else {" |]
            fallback this
            appendCommand "}"
        | many ->
            appendCommands [|
                "{"
                $"  const value = Module.HEAP32[{int (location / 4n)}];"
            |]
            
            let mutable first = true
            for (v, ifTrue) in many do
                if first then
                    appendCommands [| $"if(value == {v}) {{" |]
                    first <- false
                else
                    appendCommands [| $"}} else if(value == {v}) {{" |]
                ifTrue this
                
            appendCommands [| "} else {" |]
            fallback this
            appendCommands [| "}"; "}" |]
            

    override this.Call(func) = failwith "todo"
    override this.Call(func,arg0) = failwith "todo"
    override this.Call(func,arg0,arg1) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2,arg3) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2,arg3,arg4) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5,arg6) = failwith "todo"
    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7) = failwith "todo"
    
    override this.BeginQuery(``target`` : QueryTarget, ``id`` : uint32) = 
        appendCommand $"Module._emscripten_glBeginQuery({int ``target``}, {``id``});"
    override this.BeginQuery(``target`` : aptr<QueryTarget>, ``id`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``id``= this.Use(``id``).Pointer
        appendCommand $"Module._emscripten_glBeginQuery(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``id`` / 4n}]);"
    override this.BeginTransformFeedback(``primitiveMode`` : PrimitiveType) = 
        appendCommand $"Module._emscripten_glBeginTransformFeedback({int ``primitiveMode``});"
    override this.BeginTransformFeedback(``primitiveMode`` : aptr<PrimitiveType>) = 
        let ``primitiveMode``= this.Use(``primitiveMode``).Pointer
        appendCommand $"Module._emscripten_glBeginTransformFeedback(Module.HEAPU32[{``primitiveMode`` / 4n}]);"
    override this.BindBufferBase(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32) = 
        appendCommand $"Module._emscripten_glBindBufferBase({int ``target``}, {``index``}, {``buffer``});"
    override this.BindBufferBase(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``buffer``= this.Use(``buffer``).Pointer
        appendCommand $"Module._emscripten_glBindBufferBase(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``buffer`` / 4n}]);"
    override this.BindBufferRange(``target`` : BufferTargetARB, ``index`` : uint32, ``buffer`` : uint32, ``offset`` : nativeint, ``size`` : unativeint) = 
        appendCommand $"Module._emscripten_glBindBufferRange({int ``target``}, {``index``}, {``buffer``}, {``offset``}, {``size``});"
    override this.BindBufferRange(``target`` : aptr<BufferTargetARB>, ``index`` : aptr<uint32>, ``buffer`` : aptr<uint32>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``buffer``= this.Use(``buffer``).Pointer
        let ``offset``= this.Use(``offset``).Pointer
        let ``size``= this.Use(``size``).Pointer
        appendCommand $"Module._emscripten_glBindBufferRange(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``buffer`` / 4n}], Module.HEAP32[{``offset`` / 4n}], {``size``});"
    override this.BindSampler(``unit`` : uint32, ``sampler`` : uint32) = 
        appendCommand $"Module._emscripten_glBindSampler({``unit``}, {``sampler``});"
    override this.BindSampler(``unit`` : aptr<uint32>, ``sampler`` : aptr<uint32>) = 
        let ``unit``= this.Use(``unit``).Pointer
        let ``sampler``= this.Use(``sampler``).Pointer
        appendCommand $"Module._emscripten_glBindSampler(Module.HEAPU32[{``unit`` / 4n}], Module.HEAPU32[{``sampler`` / 4n}]);"
    override this.BindTransformFeedback(``target`` : BindTransformFeedbackTarget, ``id`` : uint32) = 
        appendCommand $"Module._emscripten_glBindTransformFeedback({int ``target``}, {``id``});"
    override this.BindTransformFeedback(``target`` : aptr<BindTransformFeedbackTarget>, ``id`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``id``= this.Use(``id``).Pointer
        appendCommand $"Module._emscripten_glBindTransformFeedback(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``id`` / 4n}]);"
    override this.BindVertexArray(``array`` : uint32) = 
        appendCommand $"Module._emscripten_glBindVertexArray({``array``});"
    override this.BindVertexArray(``array`` : aptr<uint32>) = 
        let ``array``= this.Use(``array``).Pointer
        appendCommand $"Module._emscripten_glBindVertexArray(Module.HEAPU32[{``array`` / 4n}]);"
    override this.BlitFramebuffer(``srcX0`` : int, ``srcY0`` : int, ``srcX1`` : int, ``srcY1`` : int, ``dstX0`` : int, ``dstY0`` : int, ``dstX1`` : int, ``dstY1`` : int, ``mask`` : ClearBufferMask, ``filter`` : BlitFramebufferFilter) = 
        appendCommand $"Module._emscripten_glBlitFramebuffer({``srcX0``}, {``srcY0``}, {``srcX1``}, {``srcY1``}, {``dstX0``}, {``dstY0``}, {``dstX1``}, {``dstY1``}, {int ``mask``}, {int ``filter``});"
    override this.BlitFramebuffer(``srcX0`` : aptr<int>, ``srcY0`` : aptr<int>, ``srcX1`` : aptr<int>, ``srcY1`` : aptr<int>, ``dstX0`` : aptr<int>, ``dstY0`` : aptr<int>, ``dstX1`` : aptr<int>, ``dstY1`` : aptr<int>, ``mask`` : aptr<ClearBufferMask>, ``filter`` : aptr<BlitFramebufferFilter>) = 
        let ``srcX0``= this.Use(``srcX0``).Pointer
        let ``srcY0``= this.Use(``srcY0``).Pointer
        let ``srcX1``= this.Use(``srcX1``).Pointer
        let ``srcY1``= this.Use(``srcY1``).Pointer
        let ``dstX0``= this.Use(``dstX0``).Pointer
        let ``dstY0``= this.Use(``dstY0``).Pointer
        let ``dstX1``= this.Use(``dstX1``).Pointer
        let ``dstY1``= this.Use(``dstY1``).Pointer
        let ``mask``= this.Use(``mask``).Pointer
        let ``filter``= this.Use(``filter``).Pointer
        appendCommand $"Module._emscripten_glBlitFramebuffer(Module.HEAP32[{``srcX0`` / 4n}], Module.HEAP32[{``srcY0`` / 4n}], Module.HEAP32[{``srcX1`` / 4n}], Module.HEAP32[{``srcY1`` / 4n}], Module.HEAP32[{``dstX0`` / 4n}], Module.HEAP32[{``dstY0`` / 4n}], Module.HEAP32[{``dstX1`` / 4n}], Module.HEAP32[{``dstY1`` / 4n}], Module.HEAPU32[{``mask`` / 4n}], Module.HEAPU32[{``filter`` / 4n}]);"
    override this.ClearBufferiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glClearBufferiv({int ``buffer``}, {``drawbuffer``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.ClearBufferiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<int>) = 
        let ``buffer``= this.Use(``buffer``).Pointer
        let ``drawbuffer``= this.Use(``drawbuffer``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glClearBufferiv(Module.HEAPU32[{``buffer`` / 4n}], Module.HEAP32[{``drawbuffer`` / 4n}], {``value``});"
    override this.ClearBufferuiv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glClearBufferuiv({int ``buffer``}, {``drawbuffer``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.ClearBufferuiv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<uint32>) = 
        let ``buffer``= this.Use(``buffer``).Pointer
        let ``drawbuffer``= this.Use(``drawbuffer``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glClearBufferuiv(Module.HEAPU32[{``buffer`` / 4n}], Module.HEAP32[{``drawbuffer`` / 4n}], {``value``});"
    override this.ClearBufferfv(``buffer`` : BufferKind, ``drawbuffer`` : int, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glClearBufferfv({int ``buffer``}, {``drawbuffer``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.ClearBufferfv(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``value`` : aptr<float32>) = 
        let ``buffer``= this.Use(``buffer``).Pointer
        let ``drawbuffer``= this.Use(``drawbuffer``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glClearBufferfv(Module.HEAPU32[{``buffer`` / 4n}], Module.HEAP32[{``drawbuffer`` / 4n}], {``value``});"
    override this.ClearBufferfi(``buffer`` : BufferKind, ``drawbuffer`` : int, ``depth`` : float32, ``stencil`` : int) = 
        appendCommand $"Module._emscripten_glClearBufferfi({int ``buffer``}, {``drawbuffer``}, {fstr ``depth``}, {``stencil``});"
    override this.ClearBufferfi(``buffer`` : aptr<BufferKind>, ``drawbuffer`` : aptr<int>, ``depth`` : aptr<float32>, ``stencil`` : aptr<int>) = 
        let ``buffer``= this.Use(``buffer``).Pointer
        let ``drawbuffer``= this.Use(``drawbuffer``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        let ``stencil``= this.Use(``stencil``).Pointer
        appendCommand $"Module._emscripten_glClearBufferfi(Module.HEAPU32[{``buffer`` / 4n}], Module.HEAP32[{``drawbuffer`` / 4n}], Module.HEAPF32[{``depth`` / 4n}], Module.HEAP32[{``stencil`` / 4n}]);"
    override this.ClientWaitSync(``sync`` : nativeint, ``flags`` : SyncObjectMask, ``timeout`` : uint64, ``result`` : nativeptr<GLEnum>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glClientWaitSync({``sync``}, {int ``flags``}, {``timeout``});"
    override this.ClientWaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncObjectMask>, ``timeout`` : aptr<uint64>, ``result`` : aptr<GLEnum>) = 
        let ``sync``= this.Use(``sync``).Pointer
        let ``flags``= this.Use(``flags``).Pointer
        let ``timeout``= this.Use(``timeout``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glClientWaitSync(Module.HEAP32[{``sync`` / 4n}], Module.HEAPU32[{``flags`` / 4n}], {``timeout``});"
    override this.CompressedTexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        appendCommand $"Module._emscripten_glCompressedTexImage3D({int ``target``}, {``level``}, {int ``internalformat``}, {``width``}, {``height``}, {``depth``}, {``border``}, {``imageSize``}, {int ``data``});"
    override this.CompressedTexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        let ``border``= this.Use(``border``).Pointer
        let ``imageSize``= this.Use(``imageSize``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glCompressedTexImage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``depth`` / 4n}], Module.HEAP32[{``border`` / 4n}], Module.HEAPU32[{``imageSize`` / 4n}], Module.HEAP32[{``data`` / 4n}]);"
    override this.CompressedTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        appendCommand $"Module._emscripten_glCompressedTexSubImage3D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``zoffset``}, {``width``}, {``height``}, {``depth``}, {int ``format``}, {``imageSize``}, {int ``data``});"
    override this.CompressedTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``zoffset``= this.Use(``zoffset``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``imageSize``= this.Use(``imageSize``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glCompressedTexSubImage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAP32[{``zoffset`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``depth`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``imageSize`` / 4n}], Module.HEAP32[{``data`` / 4n}]);"
    override this.CopyBufferSubData(``readTarget`` : CopyBufferSubDataTarget, ``writeTarget`` : CopyBufferSubDataTarget, ``readOffset`` : nativeint, ``writeOffset`` : nativeint, ``size`` : unativeint) = 
        appendCommand $"Module._emscripten_glCopyBufferSubData({int ``readTarget``}, {int ``writeTarget``}, {``readOffset``}, {``writeOffset``}, {``size``});"
    override this.CopyBufferSubData(``readTarget`` : aptr<CopyBufferSubDataTarget>, ``writeTarget`` : aptr<CopyBufferSubDataTarget>, ``readOffset`` : aptr<nativeint>, ``writeOffset`` : aptr<nativeint>, ``size`` : aptr<unativeint>) = 
        let ``readTarget``= this.Use(``readTarget``).Pointer
        let ``writeTarget``= this.Use(``writeTarget``).Pointer
        let ``readOffset``= this.Use(``readOffset``).Pointer
        let ``writeOffset``= this.Use(``writeOffset``).Pointer
        let ``size``= this.Use(``size``).Pointer
        appendCommand $"Module._emscripten_glCopyBufferSubData(Module.HEAPU32[{``readTarget`` / 4n}], Module.HEAPU32[{``writeTarget`` / 4n}], Module.HEAP32[{``readOffset`` / 4n}], Module.HEAP32[{``writeOffset`` / 4n}], {``size``});"
    override this.CopyTexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glCopyTexSubImage3D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``zoffset``}, {``x``}, {``y``}, {``width``}, {``height``});"
    override this.CopyTexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``zoffset``= this.Use(``zoffset``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glCopyTexSubImage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAP32[{``zoffset`` / 4n}], Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.DeleteQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteQueries({``n``}, {int (NativePtr.toNativeInt ``ids``)});"
    override this.DeleteQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``ids``= this.Use(``ids``).Pointer
        appendCommand $"Module._emscripten_glDeleteQueries(Module.HEAPU32[{``n`` / 4n}], {``ids``});"
    override this.DeleteSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteSamplers({``count``}, {int (NativePtr.toNativeInt ``samplers``)});"
    override this.DeleteSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count``= this.Use(``count``).Pointer
        let ``samplers``= this.Use(``samplers``).Pointer
        appendCommand $"Module._emscripten_glDeleteSamplers(Module.HEAPU32[{``count`` / 4n}], {``samplers``});"
    override this.DeleteSync(``sync`` : nativeint) = 
        appendCommand $"Module._emscripten_glDeleteSync({``sync``});"
    override this.DeleteSync(``sync`` : aptr<nativeint>) = 
        let ``sync``= this.Use(``sync``).Pointer
        appendCommand $"Module._emscripten_glDeleteSync(Module.HEAP32[{``sync`` / 4n}]);"
    override this.DeleteTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteTransformFeedbacks({``n``}, {int (NativePtr.toNativeInt ``ids``)});"
    override this.DeleteTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``ids``= this.Use(``ids``).Pointer
        appendCommand $"Module._emscripten_glDeleteTransformFeedbacks(Module.HEAPU32[{``n`` / 4n}], {``ids``});"
    override this.DeleteVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteVertexArrays({``n``}, {int (NativePtr.toNativeInt ``arrays``)});"
    override this.DeleteVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``arrays``= this.Use(``arrays``).Pointer
        appendCommand $"Module._emscripten_glDeleteVertexArrays(Module.HEAPU32[{``n`` / 4n}], {``arrays``});"
    override this.DrawArraysInstanced(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32, ``instancecount`` : uint32) = 
        appendCommand $"Module._emscripten_glDrawArraysInstanced({int ``mode``}, {``first``}, {``count``}, {``instancecount``});"
    override this.DrawArraysInstanced(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>, ``instancecount`` : aptr<uint32>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``first``= this.Use(``first``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``instancecount``= this.Use(``instancecount``).Pointer
        appendCommand $"Module._emscripten_glDrawArraysInstanced(Module.HEAPU32[{``mode`` / 4n}], Module.HEAP32[{``first`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``instancecount`` / 4n}]);"
    override this.DrawBuffers(``n`` : uint32, ``bufs`` : nativeptr<GLEnum>) = 
        appendCommand $"Module._emscripten_glDrawBuffers({``n``}, {int (NativePtr.toNativeInt ``bufs``)});"
    override this.DrawBuffers(``n`` : aptr<uint32>, ``bufs`` : aptr<nativeptr<GLEnum>>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``bufs``= this.Use(``bufs``).Pointer
        appendCommand $"Module._emscripten_glDrawBuffers(Module.HEAPU32[{``n`` / 4n}], Module.HEAP32[{``bufs`` / 4n}]);"
    override this.DrawElementsInstanced(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint, ``instancecount`` : uint32) = 
        appendCommand $"Module._emscripten_glDrawElementsInstanced({int ``mode``}, {``count``}, {int ``type``}, {int ``indices``}, {``instancecount``});"
    override this.DrawElementsInstanced(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>, ``instancecount`` : aptr<uint32>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``indices``= this.Use(``indices``).Pointer
        let ``instancecount``= this.Use(``instancecount``).Pointer
        appendCommand $"Module._emscripten_glDrawElementsInstanced(Module.HEAPU32[{``mode`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``indices`` / 4n}], Module.HEAPU32[{``instancecount`` / 4n}]);"
    override this.DrawRangeElements(``mode`` : PrimitiveType, ``start`` : uint32, ``end`` : uint32, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        appendCommand $"Module._emscripten_glDrawRangeElements({int ``mode``}, {``start``}, {``end``}, {``count``}, {int ``type``}, {int ``indices``});"
    override this.DrawRangeElements(``mode`` : aptr<PrimitiveType>, ``start`` : aptr<uint32>, ``end`` : aptr<uint32>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``start``= this.Use(``start``).Pointer
        let ``end``= this.Use(``end``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``indices``= this.Use(``indices``).Pointer
        appendCommand $"Module._emscripten_glDrawRangeElements(Module.HEAPU32[{``mode`` / 4n}], Module.HEAPU32[{``start`` / 4n}], Module.HEAPU32[{``end`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``indices`` / 4n}]);"
    override this.EndQuery(``target`` : QueryTarget) = 
        appendCommand $"Module._emscripten_glEndQuery({int ``target``});"
    override this.EndQuery(``target`` : aptr<QueryTarget>) = 
        let ``target``= this.Use(``target``).Pointer
        appendCommand $"Module._emscripten_glEndQuery(Module.HEAPU32[{``target`` / 4n}]);"
    override this.EndTransformFeedback() = 
        appendCommand $"Module._emscripten_glEndTransformFeedback();"
    override this.FenceSync(``condition`` : SyncCondition, ``flags`` : SyncBehaviorFlags, ``result`` : nativeptr<nativeint>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glFenceSync({int ``condition``}, {int ``flags``});"
    override this.FenceSync(``condition`` : aptr<SyncCondition>, ``flags`` : aptr<SyncBehaviorFlags>, ``result`` : aptr<nativeint>) = 
        let ``condition``= this.Use(``condition``).Pointer
        let ``flags``= this.Use(``flags``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glFenceSync(Module.HEAPU32[{``condition`` / 4n}], Module.HEAPU32[{``flags`` / 4n}]);"
    override this.FramebufferTextureLayer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``texture`` : uint32, ``level`` : int, ``layer`` : int) = 
        appendCommand $"Module._emscripten_glFramebufferTextureLayer({int ``target``}, {int ``attachment``}, {``texture``}, {``level``}, {``layer``});"
    override this.FramebufferTextureLayer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>, ``layer`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``attachment``= this.Use(``attachment``).Pointer
        let ``texture``= this.Use(``texture``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``layer``= this.Use(``layer``).Pointer
        appendCommand $"Module._emscripten_glFramebufferTextureLayer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``attachment`` / 4n}], Module.HEAPU32[{``texture`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``layer`` / 4n}]);"
    override this.GenQueries(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenQueries({``n``}, {int (NativePtr.toNativeInt ``ids``)});"
    override this.GenQueries(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``ids``= this.Use(``ids``).Pointer
        appendCommand $"Module._emscripten_glGenQueries(Module.HEAPU32[{``n`` / 4n}], {``ids``});"
    override this.GenSamplers(``count`` : uint32, ``samplers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenSamplers({``count``}, {int (NativePtr.toNativeInt ``samplers``)});"
    override this.GenSamplers(``count`` : aptr<uint32>, ``samplers`` : aptr<uint32>) = 
        let ``count``= this.Use(``count``).Pointer
        let ``samplers``= this.Use(``samplers``).Pointer
        appendCommand $"Module._emscripten_glGenSamplers(Module.HEAPU32[{``count`` / 4n}], {``samplers``});"
    override this.GenTransformFeedbacks(``n`` : uint32, ``ids`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenTransformFeedbacks({``n``}, {int (NativePtr.toNativeInt ``ids``)});"
    override this.GenTransformFeedbacks(``n`` : aptr<uint32>, ``ids`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``ids``= this.Use(``ids``).Pointer
        appendCommand $"Module._emscripten_glGenTransformFeedbacks(Module.HEAPU32[{``n`` / 4n}], {``ids``});"
    override this.GenVertexArrays(``n`` : uint32, ``arrays`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenVertexArrays({``n``}, {int (NativePtr.toNativeInt ``arrays``)});"
    override this.GenVertexArrays(``n`` : aptr<uint32>, ``arrays`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``arrays``= this.Use(``arrays``).Pointer
        appendCommand $"Module._emscripten_glGenVertexArrays(Module.HEAPU32[{``n`` / 4n}], {``arrays``});"
    override this.GetActiveUniformBlockiv(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``pname`` : UniformBlockPName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetActiveUniformBlockiv({``program``}, {``uniformBlockIndex``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetActiveUniformBlockiv(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``pname`` : aptr<UniformBlockPName>, ``params`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformBlockIndex``= this.Use(``uniformBlockIndex``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetActiveUniformBlockiv(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``uniformBlockIndex`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetActiveUniformBlockName(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``uniformBlockName`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetActiveUniformBlockName({``program``}, {``uniformBlockIndex``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``uniformBlockName``)});"
    override this.GetActiveUniformBlockName(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformBlockIndex``= this.Use(``uniformBlockIndex``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``uniformBlockName``= this.Use(``uniformBlockName``).Pointer
        appendCommand $"Module._emscripten_glGetActiveUniformBlockName(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``uniformBlockIndex`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``uniformBlockName``});"
    override this.GetActiveUniformsiv(``program`` : uint32, ``uniformCount`` : uint32, ``uniformIndices`` : nativeptr<uint32>, ``pname`` : UniformPName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetActiveUniformsiv({``program``}, {``uniformCount``}, {int (NativePtr.toNativeInt ``uniformIndices``)}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetActiveUniformsiv(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformIndices`` : aptr<uint32>, ``pname`` : aptr<UniformPName>, ``params`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformCount``= this.Use(``uniformCount``).Pointer
        let ``uniformIndices``= this.Use(``uniformIndices``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetActiveUniformsiv(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``uniformCount`` / 4n}], {``uniformIndices``}, Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetBufferParameteri64v(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int64>) = 
        appendCommand $"Module._emscripten_glGetBufferParameteri64v({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetBufferParameteri64v(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int64>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetBufferParameteri64v(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetFragDataLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``result`` : nativeptr<int>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetFragDataLocation({``program``}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetFragDataLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``result`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``name``= this.Use(``name``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetFragDataLocation(Module.HEAPU32[{``program`` / 4n}], {``name``});"
    override this.GetIntegeri_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetIntegeri_v({int ``target``}, {``index``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetIntegeri_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetIntegeri_v(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``index`` / 4n}], {``data``});"
    override this.GetInteger64v(``pname`` : GetPName, ``data`` : nativeptr<int64>) = 
        appendCommand $"Module._emscripten_glGetInteger64v({int ``pname``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetInteger64v(``pname`` : aptr<GetPName>, ``data`` : aptr<int64>) = 
        let ``pname``= this.Use(``pname``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetInteger64v(Module.HEAPU32[{``pname`` / 4n}], {``data``});"
    override this.GetInteger64i_v(``target`` : GetPName, ``index`` : uint32, ``data`` : nativeptr<int64>) = 
        appendCommand $"Module._emscripten_glGetInteger64i_v({int ``target``}, {``index``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetInteger64i_v(``target`` : aptr<GetPName>, ``index`` : aptr<uint32>, ``data`` : aptr<int64>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetInteger64i_v(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``index`` / 4n}], {``data``});"
    override this.GetInternalformativ(``target`` : TextureTarget, ``internalformat`` : InternalFormat, ``pname`` : InternalFormatPName, ``count`` : uint32, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetInternalformativ({int ``target``}, {int ``internalformat``}, {int ``pname``}, {``count``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetInternalformativ(``target`` : aptr<TextureTarget>, ``internalformat`` : aptr<InternalFormat>, ``pname`` : aptr<InternalFormatPName>, ``count`` : aptr<uint32>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetInternalformativ(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``params``});"
    override this.GetProgramBinary(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``binaryFormat`` : nativeptr<GLEnum>, ``binary`` : nativeint) = 
        appendCommand $"Module._emscripten_glGetProgramBinary({``program``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``binaryFormat``)}, {int ``binary``});"
    override this.GetProgramBinary(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<_>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``binaryFormat``= this.Use(``binaryFormat``).Pointer
        let ``binary``= this.Use(``binary``).Pointer
        appendCommand $"Module._emscripten_glGetProgramBinary(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``binaryFormat``}, {``binary``});"
    override this.GetQueryiv(``target`` : QueryTarget, ``pname`` : QueryParameterName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetQueryiv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetQueryiv(``target`` : aptr<QueryTarget>, ``pname`` : aptr<QueryParameterName>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetQueryiv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetQueryObjectuiv(``id`` : uint32, ``pname`` : QueryObjectParameterName, ``params`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGetQueryObjectuiv({``id``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetQueryObjectuiv(``id`` : aptr<uint32>, ``pname`` : aptr<QueryObjectParameterName>, ``params`` : aptr<uint32>) = 
        let ``id``= this.Use(``id``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetQueryObjectuiv(Module.HEAPU32[{``id`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetSamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetSamplerParameteriv({``sampler``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetSamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``params`` : aptr<int>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetSamplerParameteriv(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetSamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``params`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glGetSamplerParameterfv({``sampler``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetSamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``params`` : aptr<float32>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetSamplerParameterfv(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetStringi(``name`` : StringName, ``index`` : uint32, ``result`` : nativeptr<nativeptr<uint8>>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetStringi({int ``name``}, {``index``});"
    override this.GetStringi(``name`` : aptr<StringName>, ``index`` : aptr<uint32>, ``result`` : aptr<nativeptr<uint8>>) = 
        let ``name``= this.Use(``name``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetStringi(Module.HEAPU32[{``name`` / 4n}], Module.HEAPU32[{``index`` / 4n}]);"
    override this.GetSynciv(``sync`` : nativeint, ``pname`` : SyncParameterName, ``count`` : uint32, ``length`` : nativeptr<uint32>, ``values`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetSynciv({``sync``}, {int ``pname``}, {``count``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``values``)});"
    override this.GetSynciv(``sync`` : aptr<nativeint>, ``pname`` : aptr<SyncParameterName>, ``count`` : aptr<uint32>, ``length`` : aptr<uint32>, ``values`` : aptr<int>) = 
        let ``sync``= this.Use(``sync``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``values``= this.Use(``values``).Pointer
        appendCommand $"Module._emscripten_glGetSynciv(Module.HEAP32[{``sync`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``length``}, {``values``});"
    override this.GetTransformFeedbackVarying(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<uint32>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetTransformFeedbackVarying({``program``}, {``index``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``size``)}, {int (NativePtr.toNativeInt ``type``)}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetTransformFeedbackVarying(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<uint32>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``name``= this.Use(``name``).Pointer
        appendCommand $"Module._emscripten_glGetTransformFeedbackVarying(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``size``}, {``type``}, {``name``});"
    override this.GetUniformuiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGetUniformuiv({``program``}, {``location``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetUniformuiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``location``= this.Use(``location``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetUniformuiv(Module.HEAPU32[{``program`` / 4n}], Module.HEAP32[{``location`` / 4n}], {``params``});"
    override this.GetUniformBlockIndex(``program`` : uint32, ``uniformBlockName`` : nativeptr<uint8>, ``result`` : nativeptr<uint32>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetUniformBlockIndex({``program``}, {int (NativePtr.toNativeInt ``uniformBlockName``)});"
    override this.GetUniformBlockIndex(``program`` : aptr<uint32>, ``uniformBlockName`` : aptr<uint8>, ``result`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformBlockName``= this.Use(``uniformBlockName``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetUniformBlockIndex(Module.HEAPU32[{``program`` / 4n}], {``uniformBlockName``});"
    override this.GetUniformIndices(``program`` : uint32, ``uniformCount`` : uint32, ``uniformNames`` : nativeptr<nativeptr<uint8>>, ``uniformIndices`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGetUniformIndices({``program``}, {``uniformCount``}, {int (NativePtr.toNativeInt ``uniformNames``)}, {int (NativePtr.toNativeInt ``uniformIndices``)});"
    override this.GetUniformIndices(``program`` : aptr<uint32>, ``uniformCount`` : aptr<uint32>, ``uniformNames`` : aptr<nativeptr<uint8>>, ``uniformIndices`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformCount``= this.Use(``uniformCount``).Pointer
        let ``uniformNames``= this.Use(``uniformNames``).Pointer
        let ``uniformIndices``= this.Use(``uniformIndices``).Pointer
        appendCommand $"Module._emscripten_glGetUniformIndices(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``uniformCount`` / 4n}], {``uniformNames``}, {``uniformIndices``});"
    override this.GetVertexAttribIiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetVertexAttribIiv({``index``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetVertexAttribIiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<int>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetVertexAttribIiv(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetVertexAttribIuiv(``index`` : uint32, ``pname`` : VertexAttribEnum, ``params`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGetVertexAttribIuiv({``index``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetVertexAttribIuiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribEnum>, ``params`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetVertexAttribIuiv(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.InvalidateFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>) = 
        appendCommand $"Module._emscripten_glInvalidateFramebuffer({int ``target``}, {``numAttachments``}, {int (NativePtr.toNativeInt ``attachments``)});"
    override this.InvalidateFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``numAttachments``= this.Use(``numAttachments``).Pointer
        let ``attachments``= this.Use(``attachments``).Pointer
        appendCommand $"Module._emscripten_glInvalidateFramebuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``numAttachments`` / 4n}], {``attachments``});"
    override this.InvalidateSubFramebuffer(``target`` : FramebufferTarget, ``numAttachments`` : uint32, ``attachments`` : nativeptr<GLEnum>, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glInvalidateSubFramebuffer({int ``target``}, {``numAttachments``}, {int (NativePtr.toNativeInt ``attachments``)}, {``x``}, {``y``}, {``width``}, {``height``});"
    override this.InvalidateSubFramebuffer(``target`` : aptr<FramebufferTarget>, ``numAttachments`` : aptr<uint32>, ``attachments`` : aptr<GLEnum>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``numAttachments``= this.Use(``numAttachments``).Pointer
        let ``attachments``= this.Use(``attachments``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glInvalidateSubFramebuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``numAttachments`` / 4n}], {``attachments``}, Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.IsQuery(``id`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsQuery({``id``});"
    override this.IsQuery(``id`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``id``= this.Use(``id``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsQuery(Module.HEAPU32[{``id`` / 4n}]);"
    override this.IsSampler(``sampler`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsSampler({``sampler``});"
    override this.IsSampler(``sampler`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsSampler(Module.HEAPU32[{``sampler`` / 4n}]);"
    override this.IsSync(``sync`` : nativeint, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsSync({``sync``});"
    override this.IsSync(``sync`` : aptr<nativeint>, ``result`` : aptr<Boolean>) = 
        let ``sync``= this.Use(``sync``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsSync(Module.HEAP32[{``sync`` / 4n}]);"
    override this.IsTransformFeedback(``id`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsTransformFeedback({``id``});"
    override this.IsTransformFeedback(``id`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``id``= this.Use(``id``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsTransformFeedback(Module.HEAPU32[{``id`` / 4n}]);"
    override this.IsVertexArray(``array`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsVertexArray({``array``});"
    override this.IsVertexArray(``array`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``array``= this.Use(``array``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsVertexArray(Module.HEAPU32[{``array`` / 4n}]);"
    override this.PauseTransformFeedback() = 
        appendCommand $"Module._emscripten_glPauseTransformFeedback();"
    override this.ProgramBinary(``program`` : uint32, ``binaryFormat`` : GLEnum, ``binary`` : nativeint, ``length`` : uint32) = 
        appendCommand $"Module._emscripten_glProgramBinary({``program``}, {int ``binaryFormat``}, {int ``binary``}, {``length``});"
    override this.ProgramBinary(``program`` : aptr<uint32>, ``binaryFormat`` : aptr<GLEnum>, ``binary`` : aptr<_>, ``length`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``binaryFormat``= this.Use(``binaryFormat``).Pointer
        let ``binary``= this.Use(``binary``).Pointer
        let ``length``= this.Use(``length``).Pointer
        appendCommand $"Module._emscripten_glProgramBinary(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``binaryFormat`` / 4n}], {``binary``}, Module.HEAPU32[{``length`` / 4n}]);"
    override this.ProgramParameteri(``program`` : uint32, ``pname`` : ProgramParameterPName, ``value`` : int) = 
        appendCommand $"Module._emscripten_glProgramParameteri({``program``}, {int ``pname``}, {``value``});"
    override this.ProgramParameteri(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramParameterPName>, ``value`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glProgramParameteri(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAP32[{``value`` / 4n}]);"
    override this.ReadBuffer(``src`` : ReadBufferMode) = 
        appendCommand $"Module._emscripten_glReadBuffer({int ``src``});"
    override this.ReadBuffer(``src`` : aptr<ReadBufferMode>) = 
        let ``src``= this.Use(``src``).Pointer
        appendCommand $"Module._emscripten_glReadBuffer(Module.HEAPU32[{``src`` / 4n}]);"
    override this.RenderbufferStorageMultisample(``target`` : RenderbufferTarget, ``samples`` : uint32, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glRenderbufferStorageMultisample({int ``target``}, {``samples``}, {int ``internalformat``}, {``width``}, {``height``});"
    override this.RenderbufferStorageMultisample(``target`` : aptr<RenderbufferTarget>, ``samples`` : aptr<uint32>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``samples``= this.Use(``samples``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glRenderbufferStorageMultisample(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``samples`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.ResumeTransformFeedback() = 
        appendCommand $"Module._emscripten_glResumeTransformFeedback();"
    override this.SamplerParameteri(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : int) = 
        appendCommand $"Module._emscripten_glSamplerParameteri({``sampler``}, {int ``pname``}, {``param``});"
    override this.SamplerParameteri(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glSamplerParameteri(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAP32[{``param`` / 4n}]);"
    override this.SamplerParameteriv(``sampler`` : uint32, ``pname`` : SamplerParameterI, ``param`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glSamplerParameteriv({``sampler``}, {int ``pname``}, {int (NativePtr.toNativeInt ``param``)});"
    override this.SamplerParameteriv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterI>, ``param`` : aptr<int>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glSamplerParameteriv(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``param``});"
    override this.SamplerParameterf(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : float32) = 
        appendCommand $"Module._emscripten_glSamplerParameterf({``sampler``}, {int ``pname``}, {fstr ``param``});"
    override this.SamplerParameterf(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glSamplerParameterf(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAPF32[{``param`` / 4n}]);"
    override this.SamplerParameterfv(``sampler`` : uint32, ``pname`` : SamplerParameterF, ``param`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glSamplerParameterfv({``sampler``}, {int ``pname``}, {int (NativePtr.toNativeInt ``param``)});"
    override this.SamplerParameterfv(``sampler`` : aptr<uint32>, ``pname`` : aptr<SamplerParameterF>, ``param`` : aptr<float32>) = 
        let ``sampler``= this.Use(``sampler``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glSamplerParameterfv(Module.HEAPU32[{``sampler`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``param``});"
    override this.TexImage3D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        appendCommand $"Module._emscripten_glTexImage3D({int ``target``}, {``level``}, {int ``internalformat``}, {``width``}, {``height``}, {``depth``}, {``border``}, {int ``format``}, {int ``type``}, {int ``pixels``});"
    override this.TexImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        let ``border``= this.Use(``border``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``pixels``= this.Use(``pixels``).Pointer
        appendCommand $"Module._emscripten_glTexImage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``depth`` / 4n}], Module.HEAP32[{``border`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``pixels`` / 4n}]);"
    override this.TexStorage2D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glTexStorage2D({int ``target``}, {``levels``}, {int ``internalformat``}, {``width``}, {``height``});"
    override this.TexStorage2D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``levels``= this.Use(``levels``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glTexStorage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``levels`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.TexStorage3D(``target`` : TextureTarget, ``levels`` : uint32, ``internalformat`` : SizedInternalFormat, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32) = 
        appendCommand $"Module._emscripten_glTexStorage3D({int ``target``}, {``levels``}, {int ``internalformat``}, {``width``}, {``height``}, {``depth``});"
    override this.TexStorage3D(``target`` : aptr<TextureTarget>, ``levels`` : aptr<uint32>, ``internalformat`` : aptr<SizedInternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``levels``= this.Use(``levels``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        appendCommand $"Module._emscripten_glTexStorage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``levels`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``depth`` / 4n}]);"
    override this.TexSubImage3D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``zoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``depth`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        appendCommand $"Module._emscripten_glTexSubImage3D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``zoffset``}, {``width``}, {``height``}, {``depth``}, {int ``format``}, {int ``type``}, {int ``pixels``});"
    override this.TexSubImage3D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``zoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``depth`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``zoffset``= this.Use(``zoffset``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``depth``= this.Use(``depth``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``pixels``= this.Use(``pixels``).Pointer
        appendCommand $"Module._emscripten_glTexSubImage3D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAP32[{``zoffset`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``depth`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``pixels`` / 4n}]);"
    override this.TransformFeedbackVaryings(``program`` : uint32, ``count`` : uint32, ``varyings`` : nativeptr<nativeptr<uint8>>, ``bufferMode`` : TransformFeedbackBufferMode) = 
        appendCommand $"Module._emscripten_glTransformFeedbackVaryings({``program``}, {``count``}, {int (NativePtr.toNativeInt ``varyings``)}, {int ``bufferMode``});"
    override this.TransformFeedbackVaryings(``program`` : aptr<uint32>, ``count`` : aptr<uint32>, ``varyings`` : aptr<nativeptr<uint8>>, ``bufferMode`` : aptr<TransformFeedbackBufferMode>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``varyings``= this.Use(``varyings``).Pointer
        let ``bufferMode``= this.Use(``bufferMode``).Pointer
        appendCommand $"Module._emscripten_glTransformFeedbackVaryings(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``varyings``}, Module.HEAPU32[{``bufferMode`` / 4n}]);"
    override this.Uniform1ui(``location`` : int, ``v0`` : uint32) = 
        appendCommand $"Module._emscripten_glUniform1ui({``location``}, {``v0``});"
    override this.Uniform1ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        appendCommand $"Module._emscripten_glUniform1ui(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``v0`` / 4n}]);"
    override this.Uniform1uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glUniform1uiv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform1uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform1uiv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform2ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32) = 
        appendCommand $"Module._emscripten_glUniform2ui({``location``}, {``v0``}, {``v1``});"
    override this.Uniform2ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        appendCommand $"Module._emscripten_glUniform2ui(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``v0`` / 4n}], Module.HEAPU32[{``v1`` / 4n}]);"
    override this.Uniform2uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glUniform2uiv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform2uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform2uiv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform3ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32) = 
        appendCommand $"Module._emscripten_glUniform3ui({``location``}, {``v0``}, {``v1``}, {``v2``});"
    override this.Uniform3ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        appendCommand $"Module._emscripten_glUniform3ui(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``v0`` / 4n}], Module.HEAPU32[{``v1`` / 4n}], Module.HEAPU32[{``v2`` / 4n}]);"
    override this.Uniform3uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glUniform3uiv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform3uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform3uiv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform4ui(``location`` : int, ``v0`` : uint32, ``v1`` : uint32, ``v2`` : uint32, ``v3`` : uint32) = 
        appendCommand $"Module._emscripten_glUniform4ui({``location``}, {``v0``}, {``v1``}, {``v2``}, {``v3``});"
    override this.Uniform4ui(``location`` : aptr<int>, ``v0`` : aptr<uint32>, ``v1`` : aptr<uint32>, ``v2`` : aptr<uint32>, ``v3`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        let ``v3``= this.Use(``v3``).Pointer
        appendCommand $"Module._emscripten_glUniform4ui(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``v0`` / 4n}], Module.HEAPU32[{``v1`` / 4n}], Module.HEAPU32[{``v2`` / 4n}], Module.HEAPU32[{``v3`` / 4n}]);"
    override this.Uniform4uiv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glUniform4uiv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform4uiv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<uint32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform4uiv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.UniformBlockBinding(``program`` : uint32, ``uniformBlockIndex`` : uint32, ``uniformBlockBinding`` : uint32) = 
        appendCommand $"Module._emscripten_glUniformBlockBinding({``program``}, {``uniformBlockIndex``}, {``uniformBlockBinding``});"
    override this.UniformBlockBinding(``program`` : aptr<uint32>, ``uniformBlockIndex`` : aptr<uint32>, ``uniformBlockBinding`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``uniformBlockIndex``= this.Use(``uniformBlockIndex``).Pointer
        let ``uniformBlockBinding``= this.Use(``uniformBlockBinding``).Pointer
        appendCommand $"Module._emscripten_glUniformBlockBinding(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``uniformBlockIndex`` / 4n}], Module.HEAPU32[{``uniformBlockBinding`` / 4n}]);"
    override this.UniformMatrix2x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix2x3fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix2x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix2x3fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix2x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix2x4fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix2x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix2x4fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix3x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix3x2fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix3x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix3x2fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix3x4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix3x4fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix3x4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix3x4fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix4x2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix4x2fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix4x2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix4x2fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix4x3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix4x3fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix4x3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix4x3fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.VertexAttribDivisor(``index`` : uint32, ``divisor`` : uint32) = 
        appendCommand $"Module._emscripten_glVertexAttribDivisor({``index``}, {``divisor``});"
    override this.VertexAttribDivisor(``index`` : aptr<uint32>, ``divisor`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``divisor``= this.Use(``divisor``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribDivisor(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``divisor`` / 4n}]);"
    override this.VertexAttribI4i(``index`` : uint32, ``x`` : int, ``y`` : int, ``z`` : int, ``w`` : int) = 
        appendCommand $"Module._emscripten_glVertexAttribI4i({``index``}, {``x``}, {``y``}, {``z``}, {``w``});"
    override this.VertexAttribI4i(``index`` : aptr<uint32>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``z`` : aptr<int>, ``w`` : aptr<int>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``z``= this.Use(``z``).Pointer
        let ``w``= this.Use(``w``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribI4i(Module.HEAPU32[{``index`` / 4n}], Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAP32[{``z`` / 4n}], Module.HEAP32[{``w`` / 4n}]);"
    override this.VertexAttribI4ui(``index`` : uint32, ``x`` : uint32, ``y`` : uint32, ``z`` : uint32, ``w`` : uint32) = 
        appendCommand $"Module._emscripten_glVertexAttribI4ui({``index``}, {``x``}, {``y``}, {``z``}, {``w``});"
    override this.VertexAttribI4ui(``index`` : aptr<uint32>, ``x`` : aptr<uint32>, ``y`` : aptr<uint32>, ``z`` : aptr<uint32>, ``w`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``z``= this.Use(``z``).Pointer
        let ``w``= this.Use(``w``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribI4ui(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``x`` / 4n}], Module.HEAPU32[{``y`` / 4n}], Module.HEAPU32[{``z`` / 4n}], Module.HEAPU32[{``w`` / 4n}]);"
    override this.VertexAttribI4iv(``index`` : uint32, ``v`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glVertexAttribI4iv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttribI4iv(``index`` : aptr<uint32>, ``v`` : aptr<int>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribI4iv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttribI4uiv(``index`` : uint32, ``v`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glVertexAttribI4uiv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttribI4uiv(``index`` : aptr<uint32>, ``v`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribI4uiv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttribIPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribIType, ``stride`` : uint32, ``pointer`` : nativeint) = 
        appendCommand $"Module._emscripten_glVertexAttribIPointer({``index``}, {``size``}, {int ``type``}, {``stride``}, {int ``pointer``});"
    override this.VertexAttribIPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribIType>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<_>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``stride``= this.Use(``stride``).Pointer
        let ``pointer``= this.Use(``pointer``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribIPointer(Module.HEAPU32[{``index`` / 4n}], Module.HEAP32[{``size`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAPU32[{``stride`` / 4n}], {``pointer``});"
    override this.WaitSync(``sync`` : nativeint, ``flags`` : SyncBehaviorFlags, ``timeout`` : uint64) = 
        appendCommand $"Module._emscripten_glWaitSync({``sync``}, {int ``flags``}, {``timeout``});"
    override this.WaitSync(``sync`` : aptr<nativeint>, ``flags`` : aptr<SyncBehaviorFlags>, ``timeout`` : aptr<uint64>) = 
        let ``sync``= this.Use(``sync``).Pointer
        let ``flags``= this.Use(``flags``).Pointer
        let ``timeout``= this.Use(``timeout``).Pointer
        appendCommand $"Module._emscripten_glWaitSync(Module.HEAP32[{``sync`` / 4n}], Module.HEAPU32[{``flags`` / 4n}], {``timeout``});"
    override this.ActiveTexture(``texture`` : TextureUnit) = 
        appendCommand $"Module._emscripten_glActiveTexture({int ``texture``});"
    override this.ActiveTexture(``texture`` : aptr<TextureUnit>) = 
        let ``texture``= this.Use(``texture``).Pointer
        appendCommand $"Module._emscripten_glActiveTexture(Module.HEAPU32[{``texture`` / 4n}]);"
    override this.AttachShader(``program`` : uint32, ``shader`` : uint32) = 
        appendCommand $"Module._emscripten_glAttachShader({``program``}, {``shader``});"
    override this.AttachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``shader``= this.Use(``shader``).Pointer
        appendCommand $"Module._emscripten_glAttachShader(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``shader`` / 4n}]);"
    override this.BindAttribLocation(``program`` : uint32, ``index`` : uint32, ``name`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glBindAttribLocation({``program``}, {``index``}, {int (NativePtr.toNativeInt ``name``)});"
    override this.BindAttribLocation(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``name`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``name``= this.Use(``name``).Pointer
        appendCommand $"Module._emscripten_glBindAttribLocation(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``index`` / 4n}], {``name``});"
    override this.BindBuffer(``target`` : BufferTargetARB, ``buffer`` : uint32) = 
        appendCommand $"Module._emscripten_glBindBuffer({int ``target``}, {``buffer``});"
    override this.BindBuffer(``target`` : aptr<BufferTargetARB>, ``buffer`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``buffer``= this.Use(``buffer``).Pointer
        appendCommand $"Module._emscripten_glBindBuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``buffer`` / 4n}]);"
    override this.BindFramebuffer(``target`` : FramebufferTarget, ``framebuffer`` : uint32) = 
        appendCommand $"Module._emscripten_glBindFramebuffer({int ``target``}, {``framebuffer``});"
    override this.BindFramebuffer(``target`` : aptr<FramebufferTarget>, ``framebuffer`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``framebuffer``= this.Use(``framebuffer``).Pointer
        appendCommand $"Module._emscripten_glBindFramebuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``framebuffer`` / 4n}]);"
    override this.BindRenderbuffer(``target`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        appendCommand $"Module._emscripten_glBindRenderbuffer({int ``target``}, {``renderbuffer``});"
    override this.BindRenderbuffer(``target`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``renderbuffer``= this.Use(``renderbuffer``).Pointer
        appendCommand $"Module._emscripten_glBindRenderbuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``renderbuffer`` / 4n}]);"
    override this.BindTexture(``target`` : TextureTarget, ``texture`` : uint32) = 
        appendCommand $"Module._emscripten_glBindTexture({int ``target``}, {``texture``});"
    override this.BindTexture(``target`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``texture``= this.Use(``texture``).Pointer
        appendCommand $"Module._emscripten_glBindTexture(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``texture`` / 4n}]);"
    override this.BlendColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        appendCommand $"Module._emscripten_glBlendColor({fstr ``red``}, {fstr ``green``}, {fstr ``blue``}, {fstr ``alpha``});"
    override this.BlendColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red``= this.Use(``red``).Pointer
        let ``green``= this.Use(``green``).Pointer
        let ``blue``= this.Use(``blue``).Pointer
        let ``alpha``= this.Use(``alpha``).Pointer
        appendCommand $"Module._emscripten_glBlendColor(Module.HEAPF32[{``red`` / 4n}], Module.HEAPF32[{``green`` / 4n}], Module.HEAPF32[{``blue`` / 4n}], Module.HEAPF32[{``alpha`` / 4n}]);"
    override this.BlendEquation(``mode`` : BlendEquationModeEXT) = 
        appendCommand $"Module._emscripten_glBlendEquation({int ``mode``});"
    override this.BlendEquation(``mode`` : aptr<BlendEquationModeEXT>) = 
        let ``mode``= this.Use(``mode``).Pointer
        appendCommand $"Module._emscripten_glBlendEquation(Module.HEAPU32[{``mode`` / 4n}]);"
    override this.BlendEquationSeparate(``modeRGB`` : BlendEquationModeEXT, ``modeAlpha`` : BlendEquationModeEXT) = 
        appendCommand $"Module._emscripten_glBlendEquationSeparate({int ``modeRGB``}, {int ``modeAlpha``});"
    override this.BlendEquationSeparate(``modeRGB`` : aptr<BlendEquationModeEXT>, ``modeAlpha`` : aptr<BlendEquationModeEXT>) = 
        let ``modeRGB``= this.Use(``modeRGB``).Pointer
        let ``modeAlpha``= this.Use(``modeAlpha``).Pointer
        appendCommand $"Module._emscripten_glBlendEquationSeparate(Module.HEAPU32[{``modeRGB`` / 4n}], Module.HEAPU32[{``modeAlpha`` / 4n}]);"
    override this.BlendFunc(``sfactor`` : BlendingFactor, ``dfactor`` : BlendingFactor) = 
        appendCommand $"Module._emscripten_glBlendFunc({int ``sfactor``}, {int ``dfactor``});"
    override this.BlendFunc(``sfactor`` : aptr<BlendingFactor>, ``dfactor`` : aptr<BlendingFactor>) = 
        let ``sfactor``= this.Use(``sfactor``).Pointer
        let ``dfactor``= this.Use(``dfactor``).Pointer
        appendCommand $"Module._emscripten_glBlendFunc(Module.HEAPU32[{``sfactor`` / 4n}], Module.HEAPU32[{``dfactor`` / 4n}]);"
    override this.BlendFuncSeparate(``sfactorRGB`` : BlendingFactor, ``dfactorRGB`` : BlendingFactor, ``sfactorAlpha`` : BlendingFactor, ``dfactorAlpha`` : BlendingFactor) = 
        appendCommand $"Module._emscripten_glBlendFuncSeparate({int ``sfactorRGB``}, {int ``dfactorRGB``}, {int ``sfactorAlpha``}, {int ``dfactorAlpha``});"
    override this.BlendFuncSeparate(``sfactorRGB`` : aptr<BlendingFactor>, ``dfactorRGB`` : aptr<BlendingFactor>, ``sfactorAlpha`` : aptr<BlendingFactor>, ``dfactorAlpha`` : aptr<BlendingFactor>) = 
        let ``sfactorRGB``= this.Use(``sfactorRGB``).Pointer
        let ``dfactorRGB``= this.Use(``dfactorRGB``).Pointer
        let ``sfactorAlpha``= this.Use(``sfactorAlpha``).Pointer
        let ``dfactorAlpha``= this.Use(``dfactorAlpha``).Pointer
        appendCommand $"Module._emscripten_glBlendFuncSeparate(Module.HEAPU32[{``sfactorRGB`` / 4n}], Module.HEAPU32[{``dfactorRGB`` / 4n}], Module.HEAPU32[{``sfactorAlpha`` / 4n}], Module.HEAPU32[{``dfactorAlpha`` / 4n}]);"
    override this.BufferData(``target`` : BufferTargetARB, ``size`` : unativeint, ``data`` : nativeint, ``usage`` : BufferUsageARB) = 
        appendCommand $"Module._emscripten_glBufferData({int ``target``}, {``size``}, {int ``data``}, {int ``usage``});"
    override this.BufferData(``target`` : aptr<BufferTargetARB>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>, ``usage`` : aptr<BufferUsageARB>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``data``= this.Use(``data``).Pointer
        let ``usage``= this.Use(``usage``).Pointer
        appendCommand $"Module._emscripten_glBufferData(Module.HEAPU32[{``target`` / 4n}], {``size``}, Module.HEAP32[{``data`` / 4n}], Module.HEAPU32[{``usage`` / 4n}]);"
    override this.BufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``data`` : nativeint) = 
        appendCommand $"Module._emscripten_glBufferSubData({int ``target``}, {``offset``}, {``size``}, {int ``data``});"
    override this.BufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``data`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``offset``= this.Use(``offset``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glBufferSubData(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``offset`` / 4n}], {``size``}, Module.HEAP32[{``data`` / 4n}]);"
    override this.CheckFramebufferStatus(``target`` : FramebufferTarget, ``result`` : nativeptr<GLEnum>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glCheckFramebufferStatus({int ``target``});"
    override this.CheckFramebufferStatus(``target`` : aptr<FramebufferTarget>, ``result`` : aptr<GLEnum>) = 
        let ``target``= this.Use(``target``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glCheckFramebufferStatus(Module.HEAPU32[{``target`` / 4n}]);"
    override this.Clear(``mask`` : ClearBufferMask) = 
        appendCommand $"Module._emscripten_glClear({int ``mask``});"
    override this.Clear(``mask`` : aptr<ClearBufferMask>) = 
        let ``mask``= this.Use(``mask``).Pointer
        appendCommand $"Module._emscripten_glClear(Module.HEAPU32[{``mask`` / 4n}]);"
    override this.ClearColor(``red`` : float32, ``green`` : float32, ``blue`` : float32, ``alpha`` : float32) = 
        appendCommand $"Module._emscripten_glClearColor({fstr ``red``}, {fstr ``green``}, {fstr ``blue``}, {fstr ``alpha``});"
    override this.ClearColor(``red`` : aptr<float32>, ``green`` : aptr<float32>, ``blue`` : aptr<float32>, ``alpha`` : aptr<float32>) = 
        let ``red``= this.Use(``red``).Pointer
        let ``green``= this.Use(``green``).Pointer
        let ``blue``= this.Use(``blue``).Pointer
        let ``alpha``= this.Use(``alpha``).Pointer
        appendCommand $"Module._emscripten_glClearColor(Module.HEAPF32[{``red`` / 4n}], Module.HEAPF32[{``green`` / 4n}], Module.HEAPF32[{``blue`` / 4n}], Module.HEAPF32[{``alpha`` / 4n}]);"
    override this.ClearDepthf(``d`` : float32) = 
        appendCommand $"Module._emscripten_glClearDepthf({fstr ``d``});"
    override this.ClearDepthf(``d`` : aptr<float32>) = 
        let ``d``= this.Use(``d``).Pointer
        appendCommand $"Module._emscripten_glClearDepthf(Module.HEAPF32[{``d`` / 4n}]);"
    override this.ClearStencil(``s`` : int) = 
        appendCommand $"Module._emscripten_glClearStencil({``s``});"
    override this.ClearStencil(``s`` : aptr<int>) = 
        let ``s``= this.Use(``s``).Pointer
        appendCommand $"Module._emscripten_glClearStencil(Module.HEAP32[{``s`` / 4n}]);"
    override this.ColorMask(``red`` : Boolean, ``green`` : Boolean, ``blue`` : Boolean, ``alpha`` : Boolean) = 
        appendCommand $"Module._emscripten_glColorMask({bstr ``red``}, {bstr ``green``}, {bstr ``blue``}, {bstr ``alpha``});"
    override this.ColorMask(``red`` : aptr<Boolean>, ``green`` : aptr<Boolean>, ``blue`` : aptr<Boolean>, ``alpha`` : aptr<Boolean>) = 
        let ``red``= this.Use(``red``).Pointer
        let ``green``= this.Use(``green``).Pointer
        let ``blue``= this.Use(``blue``).Pointer
        let ``alpha``= this.Use(``alpha``).Pointer
        appendCommand $"Module._emscripten_glColorMask(Module.HEAPU32[{``red`` / 4n}], Module.HEAPU32[{``green`` / 4n}], Module.HEAPU32[{``blue`` / 4n}], Module.HEAPU32[{``alpha`` / 4n}]);"
    override this.CompileShader(``shader`` : uint32) = 
        appendCommand $"Module._emscripten_glCompileShader({``shader``});"
    override this.CompileShader(``shader`` : aptr<uint32>) = 
        let ``shader``= this.Use(``shader``).Pointer
        appendCommand $"Module._emscripten_glCompileShader(Module.HEAPU32[{``shader`` / 4n}]);"
    override this.CompressedTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``imageSize`` : uint32, ``data`` : nativeint) = 
        appendCommand $"Module._emscripten_glCompressedTexImage2D({int ``target``}, {``level``}, {int ``internalformat``}, {``width``}, {``height``}, {``border``}, {``imageSize``}, {int ``data``});"
    override this.CompressedTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``border``= this.Use(``border``).Pointer
        let ``imageSize``= this.Use(``imageSize``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glCompressedTexImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAP32[{``border`` / 4n}], Module.HEAPU32[{``imageSize`` / 4n}], Module.HEAP32[{``data`` / 4n}]);"
    override this.CompressedTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : InternalFormat, ``imageSize`` : uint32, ``data`` : nativeint) = 
        appendCommand $"Module._emscripten_glCompressedTexSubImage2D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``width``}, {``height``}, {int ``format``}, {``imageSize``}, {int ``data``});"
    override this.CompressedTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<InternalFormat>, ``imageSize`` : aptr<uint32>, ``data`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``imageSize``= this.Use(``imageSize``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glCompressedTexSubImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``imageSize`` / 4n}], Module.HEAP32[{``data`` / 4n}]);"
    override this.CopyTexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``border`` : int) = 
        appendCommand $"Module._emscripten_glCopyTexImage2D({int ``target``}, {``level``}, {int ``internalformat``}, {``x``}, {``y``}, {``width``}, {``height``}, {``border``});"
    override this.CopyTexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``border``= this.Use(``border``).Pointer
        appendCommand $"Module._emscripten_glCopyTexImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAP32[{``border`` / 4n}]);"
    override this.CopyTexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glCopyTexSubImage2D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``x``}, {``y``}, {``width``}, {``height``});"
    override this.CopyTexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glCopyTexSubImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.CreateProgram(``result`` : nativeptr<uint32>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glCreateProgram();"
    override this.CreateProgram(``result`` : aptr<uint32>) = 
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glCreateProgram();"
    override this.CreateShader(``type`` : ShaderType, ``result`` : nativeptr<uint32>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glCreateShader({int ``type``});"
    override this.CreateShader(``type`` : aptr<ShaderType>, ``result`` : aptr<uint32>) = 
        let ``type``= this.Use(``type``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glCreateShader(Module.HEAPU32[{``type`` / 4n}]);"
    override this.CullFace(``mode`` : CullFaceMode) = 
        appendCommand $"Module._emscripten_glCullFace({int ``mode``});"
    override this.CullFace(``mode`` : aptr<CullFaceMode>) = 
        let ``mode``= this.Use(``mode``).Pointer
        appendCommand $"Module._emscripten_glCullFace(Module.HEAPU32[{``mode`` / 4n}]);"
    override this.DeleteBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteBuffers({``n``}, {int (NativePtr.toNativeInt ``buffers``)});"
    override this.DeleteBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``buffers``= this.Use(``buffers``).Pointer
        appendCommand $"Module._emscripten_glDeleteBuffers(Module.HEAPU32[{``n`` / 4n}], {``buffers``});"
    override this.DeleteFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteFramebuffers({``n``}, {int (NativePtr.toNativeInt ``framebuffers``)});"
    override this.DeleteFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``framebuffers``= this.Use(``framebuffers``).Pointer
        appendCommand $"Module._emscripten_glDeleteFramebuffers(Module.HEAPU32[{``n`` / 4n}], {``framebuffers``});"
    override this.DeleteProgram(``program`` : uint32) = 
        appendCommand $"Module._emscripten_glDeleteProgram({``program``});"
    override this.DeleteProgram(``program`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        appendCommand $"Module._emscripten_glDeleteProgram(Module.HEAPU32[{``program`` / 4n}]);"
    override this.DeleteRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteRenderbuffers({``n``}, {int (NativePtr.toNativeInt ``renderbuffers``)});"
    override this.DeleteRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``renderbuffers``= this.Use(``renderbuffers``).Pointer
        appendCommand $"Module._emscripten_glDeleteRenderbuffers(Module.HEAPU32[{``n`` / 4n}], {``renderbuffers``});"
    override this.DeleteShader(``shader`` : uint32) = 
        appendCommand $"Module._emscripten_glDeleteShader({``shader``});"
    override this.DeleteShader(``shader`` : aptr<uint32>) = 
        let ``shader``= this.Use(``shader``).Pointer
        appendCommand $"Module._emscripten_glDeleteShader(Module.HEAPU32[{``shader`` / 4n}]);"
    override this.DeleteTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glDeleteTextures({``n``}, {int (NativePtr.toNativeInt ``textures``)});"
    override this.DeleteTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``textures``= this.Use(``textures``).Pointer
        appendCommand $"Module._emscripten_glDeleteTextures(Module.HEAPU32[{``n`` / 4n}], {``textures``});"
    override this.DepthFunc(``func`` : DepthFunction) = 
        appendCommand $"Module._emscripten_glDepthFunc({int ``func``});"
    override this.DepthFunc(``func`` : aptr<DepthFunction>) = 
        let ``func``= this.Use(``func``).Pointer
        appendCommand $"Module._emscripten_glDepthFunc(Module.HEAPU32[{``func`` / 4n}]);"
    override this.DepthMask(``flag`` : Boolean) = 
        appendCommand $"Module._emscripten_glDepthMask({bstr ``flag``});"
    override this.DepthMask(``flag`` : aptr<Boolean>) = 
        let ``flag``= this.Use(``flag``).Pointer
        appendCommand $"Module._emscripten_glDepthMask(Module.HEAPU32[{``flag`` / 4n}]);"
    override this.DepthRangef(``n`` : float32, ``f`` : float32) = 
        appendCommand $"Module._emscripten_glDepthRangef({fstr ``n``}, {fstr ``f``});"
    override this.DepthRangef(``n`` : aptr<float32>, ``f`` : aptr<float32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``f``= this.Use(``f``).Pointer
        appendCommand $"Module._emscripten_glDepthRangef(Module.HEAPF32[{``n`` / 4n}], Module.HEAPF32[{``f`` / 4n}]);"
    override this.DetachShader(``program`` : uint32, ``shader`` : uint32) = 
        appendCommand $"Module._emscripten_glDetachShader({``program``}, {``shader``});"
    override this.DetachShader(``program`` : aptr<uint32>, ``shader`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``shader``= this.Use(``shader``).Pointer
        appendCommand $"Module._emscripten_glDetachShader(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``shader`` / 4n}]);"
    override this.Disable(``cap`` : EnableCap) = 
        appendCommand $"Module._emscripten_glDisable({int ``cap``});"
    override this.Disable(``cap`` : aptr<EnableCap>) = 
        let ``cap``= this.Use(``cap``).Pointer
        appendCommand $"Module._emscripten_glDisable(Module.HEAPU32[{``cap`` / 4n}]);"
    override this.DisableVertexAttribArray(``index`` : uint32) = 
        appendCommand $"Module._emscripten_glDisableVertexAttribArray({``index``});"
    override this.DisableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        appendCommand $"Module._emscripten_glDisableVertexAttribArray(Module.HEAPU32[{``index`` / 4n}]);"
    override this.DrawArrays(``mode`` : PrimitiveType, ``first`` : int, ``count`` : uint32) = 
        appendCommand $"Module._emscripten_glDrawArrays({int ``mode``}, {``first``}, {``count``});"
    override this.DrawArrays(``mode`` : aptr<PrimitiveType>, ``first`` : aptr<int>, ``count`` : aptr<uint32>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``first``= this.Use(``first``).Pointer
        let ``count``= this.Use(``count``).Pointer
        appendCommand $"Module._emscripten_glDrawArrays(Module.HEAPU32[{``mode`` / 4n}], Module.HEAP32[{``first`` / 4n}], Module.HEAPU32[{``count`` / 4n}]);"
    override this.DrawElements(``mode`` : PrimitiveType, ``count`` : uint32, ``type`` : DrawElementsType, ``indices`` : nativeint) = 
        appendCommand $"Module._emscripten_glDrawElements({int ``mode``}, {``count``}, {int ``type``}, {int ``indices``});"
    override this.DrawElements(``mode`` : aptr<PrimitiveType>, ``count`` : aptr<uint32>, ``type`` : aptr<DrawElementsType>, ``indices`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``indices``= this.Use(``indices``).Pointer
        appendCommand $"Module._emscripten_glDrawElements(Module.HEAPU32[{``mode`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``indices`` / 4n}]);"
    override this.Enable(``cap`` : EnableCap) = 
        appendCommand $"Module._emscripten_glEnable({int ``cap``});"
    override this.Enable(``cap`` : aptr<EnableCap>) = 
        let ``cap``= this.Use(``cap``).Pointer
        appendCommand $"Module._emscripten_glEnable(Module.HEAPU32[{``cap`` / 4n}]);"
    override this.EnableVertexAttribArray(``index`` : uint32) = 
        appendCommand $"Module._emscripten_glEnableVertexAttribArray({``index``});"
    override this.EnableVertexAttribArray(``index`` : aptr<uint32>) = 
        let ``index``= this.Use(``index``).Pointer
        appendCommand $"Module._emscripten_glEnableVertexAttribArray(Module.HEAPU32[{``index`` / 4n}]);"
    override this.Finish() = 
        appendCommand $"Module._emscripten_glFinish();"
    override this.Flush() = 
        appendCommand $"Module._emscripten_glFlush();"
    override this.FramebufferRenderbuffer(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``renderbuffertarget`` : RenderbufferTarget, ``renderbuffer`` : uint32) = 
        appendCommand $"Module._emscripten_glFramebufferRenderbuffer({int ``target``}, {int ``attachment``}, {int ``renderbuffertarget``}, {``renderbuffer``});"
    override this.FramebufferRenderbuffer(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``renderbuffertarget`` : aptr<RenderbufferTarget>, ``renderbuffer`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``attachment``= this.Use(``attachment``).Pointer
        let ``renderbuffertarget``= this.Use(``renderbuffertarget``).Pointer
        let ``renderbuffer``= this.Use(``renderbuffer``).Pointer
        appendCommand $"Module._emscripten_glFramebufferRenderbuffer(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``attachment`` / 4n}], Module.HEAPU32[{``renderbuffertarget`` / 4n}], Module.HEAPU32[{``renderbuffer`` / 4n}]);"
    override this.FramebufferTexture2D(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``textarget`` : TextureTarget, ``texture`` : uint32, ``level`` : int) = 
        appendCommand $"Module._emscripten_glFramebufferTexture2D({int ``target``}, {int ``attachment``}, {int ``textarget``}, {``texture``}, {``level``});"
    override this.FramebufferTexture2D(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``textarget`` : aptr<TextureTarget>, ``texture`` : aptr<uint32>, ``level`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``attachment``= this.Use(``attachment``).Pointer
        let ``textarget``= this.Use(``textarget``).Pointer
        let ``texture``= this.Use(``texture``).Pointer
        let ``level``= this.Use(``level``).Pointer
        appendCommand $"Module._emscripten_glFramebufferTexture2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``attachment`` / 4n}], Module.HEAPU32[{``textarget`` / 4n}], Module.HEAPU32[{``texture`` / 4n}], Module.HEAP32[{``level`` / 4n}]);"
    override this.FrontFace(``mode`` : FrontFaceDirection) = 
        appendCommand $"Module._emscripten_glFrontFace({int ``mode``});"
    override this.FrontFace(``mode`` : aptr<FrontFaceDirection>) = 
        let ``mode``= this.Use(``mode``).Pointer
        appendCommand $"Module._emscripten_glFrontFace(Module.HEAPU32[{``mode`` / 4n}]);"
    override this.GenBuffers(``n`` : uint32, ``buffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenBuffers({``n``}, {int (NativePtr.toNativeInt ``buffers``)});"
    override this.GenBuffers(``n`` : aptr<uint32>, ``buffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``buffers``= this.Use(``buffers``).Pointer
        appendCommand $"Module._emscripten_glGenBuffers(Module.HEAPU32[{``n`` / 4n}], {``buffers``});"
    override this.GenerateMipmap(``target`` : TextureTarget) = 
        appendCommand $"Module._emscripten_glGenerateMipmap({int ``target``});"
    override this.GenerateMipmap(``target`` : aptr<TextureTarget>) = 
        let ``target``= this.Use(``target``).Pointer
        appendCommand $"Module._emscripten_glGenerateMipmap(Module.HEAPU32[{``target`` / 4n}]);"
    override this.GenFramebuffers(``n`` : uint32, ``framebuffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenFramebuffers({``n``}, {int (NativePtr.toNativeInt ``framebuffers``)});"
    override this.GenFramebuffers(``n`` : aptr<uint32>, ``framebuffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``framebuffers``= this.Use(``framebuffers``).Pointer
        appendCommand $"Module._emscripten_glGenFramebuffers(Module.HEAPU32[{``n`` / 4n}], {``framebuffers``});"
    override this.GenRenderbuffers(``n`` : uint32, ``renderbuffers`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenRenderbuffers({``n``}, {int (NativePtr.toNativeInt ``renderbuffers``)});"
    override this.GenRenderbuffers(``n`` : aptr<uint32>, ``renderbuffers`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``renderbuffers``= this.Use(``renderbuffers``).Pointer
        appendCommand $"Module._emscripten_glGenRenderbuffers(Module.HEAPU32[{``n`` / 4n}], {``renderbuffers``});"
    override this.GenTextures(``n`` : uint32, ``textures`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGenTextures({``n``}, {int (NativePtr.toNativeInt ``textures``)});"
    override this.GenTextures(``n`` : aptr<uint32>, ``textures`` : aptr<uint32>) = 
        let ``n``= this.Use(``n``).Pointer
        let ``textures``= this.Use(``textures``).Pointer
        appendCommand $"Module._emscripten_glGenTextures(Module.HEAPU32[{``n`` / 4n}], {``textures``});"
    override this.GetActiveAttrib(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetActiveAttrib({``program``}, {``index``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``size``)}, {int (NativePtr.toNativeInt ``type``)}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetActiveAttrib(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``name``= this.Use(``name``).Pointer
        appendCommand $"Module._emscripten_glGetActiveAttrib(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``size``}, {``type``}, {``name``});"
    override this.GetActiveUniform(``program`` : uint32, ``index`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``size`` : nativeptr<int>, ``type`` : nativeptr<GLEnum>, ``name`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetActiveUniform({``program``}, {``index``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``size``)}, {int (NativePtr.toNativeInt ``type``)}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetActiveUniform(``program`` : aptr<uint32>, ``index`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<GLEnum>, ``name`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``index``= this.Use(``index``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``name``= this.Use(``name``).Pointer
        appendCommand $"Module._emscripten_glGetActiveUniform(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``size``}, {``type``}, {``name``});"
    override this.GetAttachedShaders(``program`` : uint32, ``maxCount`` : uint32, ``count`` : nativeptr<uint32>, ``shaders`` : nativeptr<uint32>) = 
        appendCommand $"Module._emscripten_glGetAttachedShaders({``program``}, {``maxCount``}, {int (NativePtr.toNativeInt ``count``)}, {int (NativePtr.toNativeInt ``shaders``)});"
    override this.GetAttachedShaders(``program`` : aptr<uint32>, ``maxCount`` : aptr<uint32>, ``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``maxCount``= this.Use(``maxCount``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``shaders``= this.Use(``shaders``).Pointer
        appendCommand $"Module._emscripten_glGetAttachedShaders(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``maxCount`` / 4n}], {``count``}, {``shaders``});"
    override this.GetAttribLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``result`` : nativeptr<int>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetAttribLocation({``program``}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetAttribLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``result`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``name``= this.Use(``name``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetAttribLocation(Module.HEAPU32[{``program`` / 4n}], {``name``});"
    override this.GetBooleanv(``pname`` : GetPName, ``data`` : nativeptr<Boolean>) = 
        appendCommand $"Module._emscripten_glGetBooleanv({int ``pname``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetBooleanv(``pname`` : aptr<GetPName>, ``data`` : aptr<Boolean>) = 
        let ``pname``= this.Use(``pname``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetBooleanv(Module.HEAPU32[{``pname`` / 4n}], {``data``});"
    override this.GetBufferParameteriv(``target`` : BufferTargetARB, ``pname`` : BufferPNameARB, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetBufferParameteriv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetBufferParameteriv(``target`` : aptr<BufferTargetARB>, ``pname`` : aptr<BufferPNameARB>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetBufferParameteriv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetError(``result`` : nativeptr<GLEnum>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetError();"
    override this.GetError(``result`` : aptr<GLEnum>) = 
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetError();"
    override this.GetFloatv(``pname`` : GetPName, ``data`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glGetFloatv({int ``pname``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetFloatv(``pname`` : aptr<GetPName>, ``data`` : aptr<float32>) = 
        let ``pname``= this.Use(``pname``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetFloatv(Module.HEAPU32[{``pname`` / 4n}], {``data``});"
    override this.GetFramebufferAttachmentParameteriv(``target`` : FramebufferTarget, ``attachment`` : FramebufferAttachment, ``pname`` : FramebufferAttachmentParameterName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetFramebufferAttachmentParameteriv({int ``target``}, {int ``attachment``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetFramebufferAttachmentParameteriv(``target`` : aptr<FramebufferTarget>, ``attachment`` : aptr<FramebufferAttachment>, ``pname`` : aptr<FramebufferAttachmentParameterName>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``attachment``= this.Use(``attachment``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetFramebufferAttachmentParameteriv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``attachment`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetIntegerv(``pname`` : GetPName, ``data`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetIntegerv({int ``pname``}, {int (NativePtr.toNativeInt ``data``)});"
    override this.GetIntegerv(``pname`` : aptr<GetPName>, ``data`` : aptr<int>) = 
        let ``pname``= this.Use(``pname``).Pointer
        let ``data``= this.Use(``data``).Pointer
        appendCommand $"Module._emscripten_glGetIntegerv(Module.HEAPU32[{``pname`` / 4n}], {``data``});"
    override this.GetProgramiv(``program`` : uint32, ``pname`` : ProgramPropertyARB, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetProgramiv({``program``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetProgramiv(``program`` : aptr<uint32>, ``pname`` : aptr<ProgramPropertyARB>, ``params`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetProgramiv(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetProgramInfoLog(``program`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetProgramInfoLog({``program``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``infoLog``)});"
    override this.GetProgramInfoLog(``program`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``infoLog``= this.Use(``infoLog``).Pointer
        appendCommand $"Module._emscripten_glGetProgramInfoLog(Module.HEAPU32[{``program`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``infoLog``});"
    override this.GetRenderbufferParameteriv(``target`` : RenderbufferTarget, ``pname`` : RenderbufferParameterName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetRenderbufferParameteriv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetRenderbufferParameteriv(``target`` : aptr<RenderbufferTarget>, ``pname`` : aptr<RenderbufferParameterName>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetRenderbufferParameteriv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetShaderiv(``shader`` : uint32, ``pname`` : ShaderParameterName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetShaderiv({``shader``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetShaderiv(``shader`` : aptr<uint32>, ``pname`` : aptr<ShaderParameterName>, ``params`` : aptr<int>) = 
        let ``shader``= this.Use(``shader``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetShaderiv(Module.HEAPU32[{``shader`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetShaderInfoLog(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``infoLog`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetShaderInfoLog({``shader``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``infoLog``)});"
    override this.GetShaderInfoLog(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``infoLog`` : aptr<uint8>) = 
        let ``shader``= this.Use(``shader``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``infoLog``= this.Use(``infoLog``).Pointer
        appendCommand $"Module._emscripten_glGetShaderInfoLog(Module.HEAPU32[{``shader`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``infoLog``});"
    override this.GetShaderPrecisionFormat(``shadertype`` : ShaderType, ``precisiontype`` : PrecisionType, ``range`` : nativeptr<int>, ``precision`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetShaderPrecisionFormat({int ``shadertype``}, {int ``precisiontype``}, {int (NativePtr.toNativeInt ``range``)}, {int (NativePtr.toNativeInt ``precision``)});"
    override this.GetShaderPrecisionFormat(``shadertype`` : aptr<ShaderType>, ``precisiontype`` : aptr<PrecisionType>, ``range`` : aptr<int>, ``precision`` : aptr<int>) = 
        let ``shadertype``= this.Use(``shadertype``).Pointer
        let ``precisiontype``= this.Use(``precisiontype``).Pointer
        let ``range``= this.Use(``range``).Pointer
        let ``precision``= this.Use(``precision``).Pointer
        appendCommand $"Module._emscripten_glGetShaderPrecisionFormat(Module.HEAPU32[{``shadertype`` / 4n}], Module.HEAPU32[{``precisiontype`` / 4n}], {``range``}, {``precision``});"
    override this.GetShaderSource(``shader`` : uint32, ``bufSize`` : uint32, ``length`` : nativeptr<uint32>, ``source`` : nativeptr<uint8>) = 
        appendCommand $"Module._emscripten_glGetShaderSource({``shader``}, {``bufSize``}, {int (NativePtr.toNativeInt ``length``)}, {int (NativePtr.toNativeInt ``source``)});"
    override this.GetShaderSource(``shader`` : aptr<uint32>, ``bufSize`` : aptr<uint32>, ``length`` : aptr<uint32>, ``source`` : aptr<uint8>) = 
        let ``shader``= this.Use(``shader``).Pointer
        let ``bufSize``= this.Use(``bufSize``).Pointer
        let ``length``= this.Use(``length``).Pointer
        let ``source``= this.Use(``source``).Pointer
        appendCommand $"Module._emscripten_glGetShaderSource(Module.HEAPU32[{``shader`` / 4n}], Module.HEAPU32[{``bufSize`` / 4n}], {``length``}, {``source``});"
    override this.GetString(``name`` : StringName, ``result`` : nativeptr<nativeptr<uint8>>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetString({int ``name``});"
    override this.GetString(``name`` : aptr<StringName>, ``result`` : aptr<nativeptr<uint8>>) = 
        let ``name``= this.Use(``name``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetString(Module.HEAPU32[{``name`` / 4n}]);"
    override this.GetTexParameterfv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glGetTexParameterfv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetTexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<float32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetTexParameterfv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetTexParameteriv(``target`` : TextureTarget, ``pname`` : GetTextureParameter, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetTexParameteriv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetTexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<GetTextureParameter>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetTexParameteriv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetUniformfv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glGetUniformfv({``program``}, {``location``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetUniformfv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<float32>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``location``= this.Use(``location``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetUniformfv(Module.HEAPU32[{``program`` / 4n}], Module.HEAP32[{``location`` / 4n}], {``params``});"
    override this.GetUniformiv(``program`` : uint32, ``location`` : int, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetUniformiv({``program``}, {``location``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetUniformiv(``program`` : aptr<uint32>, ``location`` : aptr<int>, ``params`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``location``= this.Use(``location``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetUniformiv(Module.HEAPU32[{``program`` / 4n}], Module.HEAP32[{``location`` / 4n}], {``params``});"
    override this.GetUniformLocation(``program`` : uint32, ``name`` : nativeptr<uint8>, ``result`` : nativeptr<int>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glGetUniformLocation({``program``}, {int (NativePtr.toNativeInt ``name``)});"
    override this.GetUniformLocation(``program`` : aptr<uint32>, ``name`` : aptr<uint8>, ``result`` : aptr<int>) = 
        let ``program``= this.Use(``program``).Pointer
        let ``name``= this.Use(``name``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glGetUniformLocation(Module.HEAPU32[{``program`` / 4n}], {``name``});"
    override this.GetVertexAttribfv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glGetVertexAttribfv({``index``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetVertexAttribfv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetVertexAttribfv(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetVertexAttribiv(``index`` : uint32, ``pname`` : VertexAttribPropertyARB, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glGetVertexAttribiv({``index``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.GetVertexAttribiv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPropertyARB>, ``params`` : aptr<int>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glGetVertexAttribiv(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.GetVertexAttribPointerv(``index`` : uint32, ``pname`` : VertexAttribPointerPropertyARB, ``pointer`` : nativeptr<nativeint>) = 
        appendCommand $"Module._emscripten_glGetVertexAttribPointerv({``index``}, {int ``pname``}, {int (NativePtr.toNativeInt ``pointer``)});"
    override this.GetVertexAttribPointerv(``index`` : aptr<uint32>, ``pname`` : aptr<VertexAttribPointerPropertyARB>, ``pointer`` : aptr<nativeint>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``pointer``= this.Use(``pointer``).Pointer
        appendCommand $"Module._emscripten_glGetVertexAttribPointerv(Module.HEAPU32[{``index`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``pointer``});"
    override this.Hint(``target`` : HintTarget, ``mode`` : HintMode) = 
        appendCommand $"Module._emscripten_glHint({int ``target``}, {int ``mode``});"
    override this.Hint(``target`` : aptr<HintTarget>, ``mode`` : aptr<HintMode>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``mode``= this.Use(``mode``).Pointer
        appendCommand $"Module._emscripten_glHint(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``mode`` / 4n}]);"
    override this.IsBuffer(``buffer`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsBuffer({``buffer``});"
    override this.IsBuffer(``buffer`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``buffer``= this.Use(``buffer``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsBuffer(Module.HEAPU32[{``buffer`` / 4n}]);"
    override this.IsEnabled(``cap`` : EnableCap, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsEnabled({int ``cap``});"
    override this.IsEnabled(``cap`` : aptr<EnableCap>, ``result`` : aptr<Boolean>) = 
        let ``cap``= this.Use(``cap``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsEnabled(Module.HEAPU32[{``cap`` / 4n}]);"
    override this.IsFramebuffer(``framebuffer`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsFramebuffer({``framebuffer``});"
    override this.IsFramebuffer(``framebuffer`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``framebuffer``= this.Use(``framebuffer``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsFramebuffer(Module.HEAPU32[{``framebuffer`` / 4n}]);"
    override this.IsProgram(``program`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsProgram({``program``});"
    override this.IsProgram(``program`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``program``= this.Use(``program``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsProgram(Module.HEAPU32[{``program`` / 4n}]);"
    override this.IsRenderbuffer(``renderbuffer`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsRenderbuffer({``renderbuffer``});"
    override this.IsRenderbuffer(``renderbuffer`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``renderbuffer``= this.Use(``renderbuffer``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsRenderbuffer(Module.HEAPU32[{``renderbuffer`` / 4n}]);"
    override this.IsShader(``shader`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsShader({``shader``});"
    override this.IsShader(``shader`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``shader``= this.Use(``shader``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsShader(Module.HEAPU32[{``shader`` / 4n}]);"
    override this.IsTexture(``texture`` : uint32, ``result`` : nativeptr<Boolean>) = 
        appendCommand $"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module._emscripten_glIsTexture({``texture``});"
    override this.IsTexture(``texture`` : aptr<uint32>, ``result`` : aptr<Boolean>) = 
        let ``texture``= this.Use(``texture``).Pointer
        let result = this.Use(result).Pointer
        appendCommand $"Module.HEAPU32[{result / 4n}] = Module._emscripten_glIsTexture(Module.HEAPU32[{``texture`` / 4n}]);"
    override this.LineWidth(``width`` : float32) = 
        appendCommand $"Module._emscripten_glLineWidth({fstr ``width``});"
    override this.LineWidth(``width`` : aptr<float32>) = 
        let ``width``= this.Use(``width``).Pointer
        appendCommand $"Module._emscripten_glLineWidth(Module.HEAPF32[{``width`` / 4n}]);"
    override this.LinkProgram(``program`` : uint32) = 
        appendCommand $"Module._emscripten_glLinkProgram({``program``});"
    override this.LinkProgram(``program`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        appendCommand $"Module._emscripten_glLinkProgram(Module.HEAPU32[{``program`` / 4n}]);"
    override this.PixelStorei(``pname`` : PixelStoreParameter, ``param`` : int) = 
        appendCommand $"Module._emscripten_glPixelStorei({int ``pname``}, {``param``});"
    override this.PixelStorei(``pname`` : aptr<PixelStoreParameter>, ``param`` : aptr<int>) = 
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glPixelStorei(Module.HEAPU32[{``pname`` / 4n}], Module.HEAP32[{``param`` / 4n}]);"
    override this.PolygonOffset(``factor`` : float32, ``units`` : float32) = 
        appendCommand $"Module._emscripten_glPolygonOffset({fstr ``factor``}, {fstr ``units``});"
    override this.PolygonOffset(``factor`` : aptr<float32>, ``units`` : aptr<float32>) = 
        let ``factor``= this.Use(``factor``).Pointer
        let ``units``= this.Use(``units``).Pointer
        appendCommand $"Module._emscripten_glPolygonOffset(Module.HEAPF32[{``factor`` / 4n}], Module.HEAPF32[{``units`` / 4n}]);"
    override this.ReadPixels(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        appendCommand $"Module._emscripten_glReadPixels({``x``}, {``y``}, {``width``}, {``height``}, {int ``format``}, {int ``type``}, {int ``pixels``});"
    override this.ReadPixels(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<_>) = 
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``pixels``= this.Use(``pixels``).Pointer
        appendCommand $"Module._emscripten_glReadPixels(Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``type`` / 4n}], {``pixels``});"
    override this.ReleaseShaderCompiler() = 
        appendCommand $"Module._emscripten_glReleaseShaderCompiler();"
    override this.RenderbufferStorage(``target`` : RenderbufferTarget, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glRenderbufferStorage({int ``target``}, {int ``internalformat``}, {``width``}, {``height``});"
    override this.RenderbufferStorage(``target`` : aptr<RenderbufferTarget>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glRenderbufferStorage(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.SampleCoverage(``value`` : float32, ``invert`` : Boolean) = 
        appendCommand $"Module._emscripten_glSampleCoverage({fstr ``value``}, {bstr ``invert``});"
    override this.SampleCoverage(``value`` : aptr<float32>, ``invert`` : aptr<Boolean>) = 
        let ``value``= this.Use(``value``).Pointer
        let ``invert``= this.Use(``invert``).Pointer
        appendCommand $"Module._emscripten_glSampleCoverage(Module.HEAPF32[{``value`` / 4n}], Module.HEAPU32[{``invert`` / 4n}]);"
    override this.Scissor(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glScissor({``x``}, {``y``}, {``width``}, {``height``});"
    override this.Scissor(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glScissor(Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.ShaderBinary(``count`` : uint32, ``shaders`` : nativeptr<uint32>, ``binaryFormat`` : ShaderBinaryFormat, ``binary`` : nativeint, ``length`` : uint32) = 
        appendCommand $"Module._emscripten_glShaderBinary({``count``}, {int (NativePtr.toNativeInt ``shaders``)}, {int ``binaryFormat``}, {int ``binary``}, {``length``});"
    override this.ShaderBinary(``count`` : aptr<uint32>, ``shaders`` : aptr<uint32>, ``binaryFormat`` : aptr<ShaderBinaryFormat>, ``binary`` : aptr<_>, ``length`` : aptr<uint32>) = 
        let ``count``= this.Use(``count``).Pointer
        let ``shaders``= this.Use(``shaders``).Pointer
        let ``binaryFormat``= this.Use(``binaryFormat``).Pointer
        let ``binary``= this.Use(``binary``).Pointer
        let ``length``= this.Use(``length``).Pointer
        appendCommand $"Module._emscripten_glShaderBinary(Module.HEAPU32[{``count`` / 4n}], {``shaders``}, Module.HEAPU32[{``binaryFormat`` / 4n}], {``binary``}, Module.HEAPU32[{``length`` / 4n}]);"
    override this.ShaderSource(``shader`` : uint32, ``count`` : uint32, ``string`` : nativeptr<nativeptr<uint8>>, ``length`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glShaderSource({``shader``}, {``count``}, {int (NativePtr.toNativeInt ``string``)}, {int (NativePtr.toNativeInt ``length``)});"
    override this.ShaderSource(``shader`` : aptr<uint32>, ``count`` : aptr<uint32>, ``string`` : aptr<nativeptr<uint8>>, ``length`` : aptr<int>) = 
        let ``shader``= this.Use(``shader``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``string``= this.Use(``string``).Pointer
        let ``length``= this.Use(``length``).Pointer
        appendCommand $"Module._emscripten_glShaderSource(Module.HEAPU32[{``shader`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``string``}, {``length``});"
    override this.StencilFunc(``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        appendCommand $"Module._emscripten_glStencilFunc({int ``func``}, {``ref``}, {``mask``});"
    override this.StencilFunc(``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``func``= this.Use(``func``).Pointer
        let ``ref``= this.Use(``ref``).Pointer
        let ``mask``= this.Use(``mask``).Pointer
        appendCommand $"Module._emscripten_glStencilFunc(Module.HEAPU32[{``func`` / 4n}], Module.HEAP32[{``ref`` / 4n}], Module.HEAPU32[{``mask`` / 4n}]);"
    override this.StencilFuncSeparate(``face`` : StencilFaceDirection, ``func`` : StencilFunction, ``ref`` : int, ``mask`` : uint32) = 
        appendCommand $"Module._emscripten_glStencilFuncSeparate({int ``face``}, {int ``func``}, {``ref``}, {``mask``});"
    override this.StencilFuncSeparate(``face`` : aptr<StencilFaceDirection>, ``func`` : aptr<StencilFunction>, ``ref`` : aptr<int>, ``mask`` : aptr<uint32>) = 
        let ``face``= this.Use(``face``).Pointer
        let ``func``= this.Use(``func``).Pointer
        let ``ref``= this.Use(``ref``).Pointer
        let ``mask``= this.Use(``mask``).Pointer
        appendCommand $"Module._emscripten_glStencilFuncSeparate(Module.HEAPU32[{``face`` / 4n}], Module.HEAPU32[{``func`` / 4n}], Module.HEAP32[{``ref`` / 4n}], Module.HEAPU32[{``mask`` / 4n}]);"
    override this.StencilMask(``mask`` : uint32) = 
        appendCommand $"Module._emscripten_glStencilMask({``mask``});"
    override this.StencilMask(``mask`` : aptr<uint32>) = 
        let ``mask``= this.Use(``mask``).Pointer
        appendCommand $"Module._emscripten_glStencilMask(Module.HEAPU32[{``mask`` / 4n}]);"
    override this.StencilMaskSeparate(``face`` : StencilFaceDirection, ``mask`` : uint32) = 
        appendCommand $"Module._emscripten_glStencilMaskSeparate({int ``face``}, {``mask``});"
    override this.StencilMaskSeparate(``face`` : aptr<StencilFaceDirection>, ``mask`` : aptr<uint32>) = 
        let ``face``= this.Use(``face``).Pointer
        let ``mask``= this.Use(``mask``).Pointer
        appendCommand $"Module._emscripten_glStencilMaskSeparate(Module.HEAPU32[{``face`` / 4n}], Module.HEAPU32[{``mask`` / 4n}]);"
    override this.StencilOp(``fail`` : StencilOp, ``zfail`` : StencilOp, ``zpass`` : StencilOp) = 
        appendCommand $"Module._emscripten_glStencilOp({int ``fail``}, {int ``zfail``}, {int ``zpass``});"
    override this.StencilOp(``fail`` : aptr<StencilOp>, ``zfail`` : aptr<StencilOp>, ``zpass`` : aptr<StencilOp>) = 
        let ``fail``= this.Use(``fail``).Pointer
        let ``zfail``= this.Use(``zfail``).Pointer
        let ``zpass``= this.Use(``zpass``).Pointer
        appendCommand $"Module._emscripten_glStencilOp(Module.HEAPU32[{``fail`` / 4n}], Module.HEAPU32[{``zfail`` / 4n}], Module.HEAPU32[{``zpass`` / 4n}]);"
    override this.StencilOpSeparate(``face`` : StencilFaceDirection, ``sfail`` : StencilOp, ``dpfail`` : StencilOp, ``dppass`` : StencilOp) = 
        appendCommand $"Module._emscripten_glStencilOpSeparate({int ``face``}, {int ``sfail``}, {int ``dpfail``}, {int ``dppass``});"
    override this.StencilOpSeparate(``face`` : aptr<StencilFaceDirection>, ``sfail`` : aptr<StencilOp>, ``dpfail`` : aptr<StencilOp>, ``dppass`` : aptr<StencilOp>) = 
        let ``face``= this.Use(``face``).Pointer
        let ``sfail``= this.Use(``sfail``).Pointer
        let ``dpfail``= this.Use(``dpfail``).Pointer
        let ``dppass``= this.Use(``dppass``).Pointer
        appendCommand $"Module._emscripten_glStencilOpSeparate(Module.HEAPU32[{``face`` / 4n}], Module.HEAPU32[{``sfail`` / 4n}], Module.HEAPU32[{``dpfail`` / 4n}], Module.HEAPU32[{``dppass`` / 4n}]);"
    override this.TexImage2D(``target`` : TextureTarget, ``level`` : int, ``internalformat`` : InternalFormat, ``width`` : uint32, ``height`` : uint32, ``border`` : int, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        appendCommand $"Module._emscripten_glTexImage2D({int ``target``}, {``level``}, {int ``internalformat``}, {``width``}, {``height``}, {``border``}, {int ``format``}, {int ``type``}, {int ``pixels``});"
    override this.TexImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``internalformat`` : aptr<InternalFormat>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``border`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``internalformat``= this.Use(``internalformat``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``border``= this.Use(``border``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``pixels``= this.Use(``pixels``).Pointer
        appendCommand $"Module._emscripten_glTexImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAPU32[{``internalformat`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAP32[{``border`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``pixels`` / 4n}]);"
    override this.TexParameterf(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : float32) = 
        appendCommand $"Module._emscripten_glTexParameterf({int ``target``}, {int ``pname``}, {fstr ``param``});"
    override this.TexParameterf(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<float32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glTexParameterf(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAPF32[{``param`` / 4n}]);"
    override this.TexParameterfv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glTexParameterfv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.TexParameterfv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<float32>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glTexParameterfv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.TexParameteri(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``param`` : int) = 
        appendCommand $"Module._emscripten_glTexParameteri({int ``target``}, {int ``pname``}, {``param``});"
    override this.TexParameteri(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``param`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``param``= this.Use(``param``).Pointer
        appendCommand $"Module._emscripten_glTexParameteri(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], Module.HEAP32[{``param`` / 4n}]);"
    override this.TexParameteriv(``target`` : TextureTarget, ``pname`` : TextureParameterName, ``params`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glTexParameteriv({int ``target``}, {int ``pname``}, {int (NativePtr.toNativeInt ``params``)});"
    override this.TexParameteriv(``target`` : aptr<TextureTarget>, ``pname`` : aptr<TextureParameterName>, ``params`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``pname``= this.Use(``pname``).Pointer
        let ``params``= this.Use(``params``).Pointer
        appendCommand $"Module._emscripten_glTexParameteriv(Module.HEAPU32[{``target`` / 4n}], Module.HEAPU32[{``pname`` / 4n}], {``params``});"
    override this.TexSubImage2D(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : uint32, ``height`` : uint32, ``format`` : PixelFormat, ``type`` : PixelType, ``pixels`` : nativeint) = 
        appendCommand $"Module._emscripten_glTexSubImage2D({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``width``}, {``height``}, {int ``format``}, {int ``type``}, {int ``pixels``});"
    override this.TexSubImage2D(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>, ``format`` : aptr<PixelFormat>, ``type`` : aptr<PixelType>, ``pixels`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``pixels``= this.Use(``pixels``).Pointer
        appendCommand $"Module._emscripten_glTexSubImage2D(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAP32[{``pixels`` / 4n}]);"
    override this.Uniform1f(``location`` : int, ``v0`` : float32) = 
        appendCommand $"Module._emscripten_glUniform1f({``location``}, {fstr ``v0``});"
    override this.Uniform1f(``location`` : aptr<int>, ``v0`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        appendCommand $"Module._emscripten_glUniform1f(Module.HEAP32[{``location`` / 4n}], Module.HEAPF32[{``v0`` / 4n}]);"
    override this.Uniform1fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniform1fv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform1fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform1fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform1i(``location`` : int, ``v0`` : int) = 
        appendCommand $"Module._emscripten_glUniform1i({``location``}, {``v0``});"
    override this.Uniform1i(``location`` : aptr<int>, ``v0`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        appendCommand $"Module._emscripten_glUniform1i(Module.HEAP32[{``location`` / 4n}], Module.HEAP32[{``v0`` / 4n}]);"
    override this.Uniform1iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glUniform1iv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform1iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform1iv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform2f(``location`` : int, ``v0`` : float32, ``v1`` : float32) = 
        appendCommand $"Module._emscripten_glUniform2f({``location``}, {fstr ``v0``}, {fstr ``v1``});"
    override this.Uniform2f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        appendCommand $"Module._emscripten_glUniform2f(Module.HEAP32[{``location`` / 4n}], Module.HEAPF32[{``v0`` / 4n}], Module.HEAPF32[{``v1`` / 4n}]);"
    override this.Uniform2fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniform2fv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform2fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform2i(``location`` : int, ``v0`` : int, ``v1`` : int) = 
        appendCommand $"Module._emscripten_glUniform2i({``location``}, {``v0``}, {``v1``});"
    override this.Uniform2i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        appendCommand $"Module._emscripten_glUniform2i(Module.HEAP32[{``location`` / 4n}], Module.HEAP32[{``v0`` / 4n}], Module.HEAP32[{``v1`` / 4n}]);"
    override this.Uniform2iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glUniform2iv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform2iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform2iv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform3f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32) = 
        appendCommand $"Module._emscripten_glUniform3f({``location``}, {fstr ``v0``}, {fstr ``v1``}, {fstr ``v2``});"
    override this.Uniform3f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        appendCommand $"Module._emscripten_glUniform3f(Module.HEAP32[{``location`` / 4n}], Module.HEAPF32[{``v0`` / 4n}], Module.HEAPF32[{``v1`` / 4n}], Module.HEAPF32[{``v2`` / 4n}]);"
    override this.Uniform3fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniform3fv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform3fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform3i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int) = 
        appendCommand $"Module._emscripten_glUniform3i({``location``}, {``v0``}, {``v1``}, {``v2``});"
    override this.Uniform3i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        appendCommand $"Module._emscripten_glUniform3i(Module.HEAP32[{``location`` / 4n}], Module.HEAP32[{``v0`` / 4n}], Module.HEAP32[{``v1`` / 4n}], Module.HEAP32[{``v2`` / 4n}]);"
    override this.Uniform3iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glUniform3iv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform3iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform3iv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform4f(``location`` : int, ``v0`` : float32, ``v1`` : float32, ``v2`` : float32, ``v3`` : float32) = 
        appendCommand $"Module._emscripten_glUniform4f({``location``}, {fstr ``v0``}, {fstr ``v1``}, {fstr ``v2``}, {fstr ``v3``});"
    override this.Uniform4f(``location`` : aptr<int>, ``v0`` : aptr<float32>, ``v1`` : aptr<float32>, ``v2`` : aptr<float32>, ``v3`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        let ``v3``= this.Use(``v3``).Pointer
        appendCommand $"Module._emscripten_glUniform4f(Module.HEAP32[{``location`` / 4n}], Module.HEAPF32[{``v0`` / 4n}], Module.HEAPF32[{``v1`` / 4n}], Module.HEAPF32[{``v2`` / 4n}], Module.HEAPF32[{``v3`` / 4n}]);"
    override this.Uniform4fv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniform4fv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform4fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.Uniform4i(``location`` : int, ``v0`` : int, ``v1`` : int, ``v2`` : int, ``v3`` : int) = 
        appendCommand $"Module._emscripten_glUniform4i({``location``}, {``v0``}, {``v1``}, {``v2``}, {``v3``});"
    override this.Uniform4i(``location`` : aptr<int>, ``v0`` : aptr<int>, ``v1`` : aptr<int>, ``v2`` : aptr<int>, ``v3`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``v0``= this.Use(``v0``).Pointer
        let ``v1``= this.Use(``v1``).Pointer
        let ``v2``= this.Use(``v2``).Pointer
        let ``v3``= this.Use(``v3``).Pointer
        appendCommand $"Module._emscripten_glUniform4i(Module.HEAP32[{``location`` / 4n}], Module.HEAP32[{``v0`` / 4n}], Module.HEAP32[{``v1`` / 4n}], Module.HEAP32[{``v2`` / 4n}], Module.HEAP32[{``v3`` / 4n}]);"
    override this.Uniform4iv(``location`` : int, ``count`` : uint32, ``value`` : nativeptr<int>) = 
        appendCommand $"Module._emscripten_glUniform4iv({``location``}, {``count``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.Uniform4iv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``value`` : aptr<int>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniform4iv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], {``value``});"
    override this.UniformMatrix2fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix2fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix2fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix2fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix3fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix3fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix3fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix3fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UniformMatrix4fv(``location`` : int, ``count`` : uint32, ``transpose`` : Boolean, ``value`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glUniformMatrix4fv({``location``}, {``count``}, {bstr ``transpose``}, {int (NativePtr.toNativeInt ``value``)});"
    override this.UniformMatrix4fv(``location`` : aptr<int>, ``count`` : aptr<uint32>, ``transpose`` : aptr<Boolean>, ``value`` : aptr<float32>) = 
        let ``location``= this.Use(``location``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``transpose``= this.Use(``transpose``).Pointer
        let ``value``= this.Use(``value``).Pointer
        appendCommand $"Module._emscripten_glUniformMatrix4fv(Module.HEAP32[{``location`` / 4n}], Module.HEAPU32[{``count`` / 4n}], Module.HEAPU32[{``transpose`` / 4n}], {``value``});"
    override this.UseProgram(``program`` : uint32) = 
        appendCommand $"Module._emscripten_glUseProgram({``program``});"
    override this.UseProgram(``program`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        appendCommand $"Module._emscripten_glUseProgram(Module.HEAPU32[{``program`` / 4n}]);"
    override this.ValidateProgram(``program`` : uint32) = 
        appendCommand $"Module._emscripten_glValidateProgram({``program``});"
    override this.ValidateProgram(``program`` : aptr<uint32>) = 
        let ``program``= this.Use(``program``).Pointer
        appendCommand $"Module._emscripten_glValidateProgram(Module.HEAPU32[{``program`` / 4n}]);"
    override this.VertexAttrib1f(``index`` : uint32, ``x`` : float32) = 
        appendCommand $"Module._emscripten_glVertexAttrib1f({``index``}, {fstr ``x``});"
    override this.VertexAttrib1f(``index`` : aptr<uint32>, ``x`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib1f(Module.HEAPU32[{``index`` / 4n}], Module.HEAPF32[{``x`` / 4n}]);"
    override this.VertexAttrib1fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glVertexAttrib1fv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttrib1fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib1fv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttrib2f(``index`` : uint32, ``x`` : float32, ``y`` : float32) = 
        appendCommand $"Module._emscripten_glVertexAttrib2f({``index``}, {fstr ``x``}, {fstr ``y``});"
    override this.VertexAttrib2f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib2f(Module.HEAPU32[{``index`` / 4n}], Module.HEAPF32[{``x`` / 4n}], Module.HEAPF32[{``y`` / 4n}]);"
    override this.VertexAttrib2fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glVertexAttrib2fv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttrib2fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib2fv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttrib3f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32) = 
        appendCommand $"Module._emscripten_glVertexAttrib3f({``index``}, {fstr ``x``}, {fstr ``y``}, {fstr ``z``});"
    override this.VertexAttrib3f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``z``= this.Use(``z``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib3f(Module.HEAPU32[{``index`` / 4n}], Module.HEAPF32[{``x`` / 4n}], Module.HEAPF32[{``y`` / 4n}], Module.HEAPF32[{``z`` / 4n}]);"
    override this.VertexAttrib3fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glVertexAttrib3fv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttrib3fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib3fv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttrib4f(``index`` : uint32, ``x`` : float32, ``y`` : float32, ``z`` : float32, ``w`` : float32) = 
        appendCommand $"Module._emscripten_glVertexAttrib4f({``index``}, {fstr ``x``}, {fstr ``y``}, {fstr ``z``}, {fstr ``w``});"
    override this.VertexAttrib4f(``index`` : aptr<uint32>, ``x`` : aptr<float32>, ``y`` : aptr<float32>, ``z`` : aptr<float32>, ``w`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``z``= this.Use(``z``).Pointer
        let ``w``= this.Use(``w``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib4f(Module.HEAPU32[{``index`` / 4n}], Module.HEAPF32[{``x`` / 4n}], Module.HEAPF32[{``y`` / 4n}], Module.HEAPF32[{``z`` / 4n}], Module.HEAPF32[{``w`` / 4n}]);"
    override this.VertexAttrib4fv(``index`` : uint32, ``v`` : nativeptr<float32>) = 
        appendCommand $"Module._emscripten_glVertexAttrib4fv({``index``}, {int (NativePtr.toNativeInt ``v``)});"
    override this.VertexAttrib4fv(``index`` : aptr<uint32>, ``v`` : aptr<float32>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``v``= this.Use(``v``).Pointer
        appendCommand $"Module._emscripten_glVertexAttrib4fv(Module.HEAPU32[{``index`` / 4n}], {``v``});"
    override this.VertexAttribPointer(``index`` : uint32, ``size`` : int, ``type`` : VertexAttribPointerType, ``normalized`` : Boolean, ``stride`` : uint32, ``pointer`` : nativeint) = 
        appendCommand $"Module._emscripten_glVertexAttribPointer({``index``}, {``size``}, {int ``type``}, {bstr ``normalized``}, {``stride``}, {int ``pointer``});"
    override this.VertexAttribPointer(``index`` : aptr<uint32>, ``size`` : aptr<int>, ``type`` : aptr<VertexAttribPointerType>, ``normalized`` : aptr<Boolean>, ``stride`` : aptr<uint32>, ``pointer`` : aptr<_>) = 
        let ``index``= this.Use(``index``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``type``= this.Use(``type``).Pointer
        let ``normalized``= this.Use(``normalized``).Pointer
        let ``stride``= this.Use(``stride``).Pointer
        let ``pointer``= this.Use(``pointer``).Pointer
        appendCommand $"Module._emscripten_glVertexAttribPointer(Module.HEAPU32[{``index`` / 4n}], Module.HEAP32[{``size`` / 4n}], Module.HEAPU32[{``type`` / 4n}], Module.HEAPU32[{``normalized`` / 4n}], Module.HEAPU32[{``stride`` / 4n}], {``pointer``});"
    override this.Viewport(``x`` : int, ``y`` : int, ``width`` : uint32, ``height`` : uint32) = 
        appendCommand $"Module._emscripten_glViewport({``x``}, {``y``}, {``width``}, {``height``});"
    override this.Viewport(``x`` : aptr<int>, ``y`` : aptr<int>, ``width`` : aptr<uint32>, ``height`` : aptr<uint32>) = 
        let ``x``= this.Use(``x``).Pointer
        let ``y``= this.Use(``y``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        appendCommand $"Module._emscripten_glViewport(Module.HEAP32[{``x`` / 4n}], Module.HEAP32[{``y`` / 4n}], Module.HEAPU32[{``width`` / 4n}], Module.HEAPU32[{``height`` / 4n}]);"
    override this.GetBufferSubData(``target`` : BufferTargetARB, ``offset`` : nativeint, ``size`` : unativeint, ``dst`` : nativeint) = 
        appendCommand $"Module._glGetBufferSubData({int ``target``}, {``offset``}, {``size``}, {int ``dst``});"
    override this.GetBufferSubData(``target`` : aptr<BufferTargetARB>, ``offset`` : aptr<nativeint>, ``size`` : aptr<unativeint>, ``dst`` : aptr<nativeint>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``offset``= this.Use(``offset``).Pointer
        let ``size``= this.Use(``size``).Pointer
        let ``dst``= this.Use(``dst``).Pointer
        appendCommand $"Module._glGetBufferSubData(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``offset`` / 4n}], {``size``}, Module.HEAP32[{``dst`` / 4n}]);"
    override this.MultiDrawArraysIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``bindingInfo`` : nativeint) = 
        appendCommand $"Module._glMultiDrawArraysIndirect({int ``mode``}, {``indirectBuffer``}, {``count``}, {``bindingInfo``});"
    override this.MultiDrawArraysIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``indirectBuffer``= this.Use(``indirectBuffer``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``bindingInfo``= this.Use(``bindingInfo``).Pointer
        appendCommand $"Module._glMultiDrawArraysIndirect(Module.HEAPU32[{``mode`` / 4n}], Module.HEAPU32[{``indirectBuffer`` / 4n}], Module.HEAP32[{``count`` / 4n}], Module.HEAP32[{``bindingInfo`` / 4n}]);"
    override this.MultiDrawArrays(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``bindingInfo`` : nativeint) = 
        appendCommand $"Module._glMultiDrawArrays({int ``mode``}, {``indirect``}, {``count``}, {``bindingInfo``});"
    override this.MultiDrawArrays(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``indirect``= this.Use(``indirect``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``bindingInfo``= this.Use(``bindingInfo``).Pointer
        appendCommand $"Module._glMultiDrawArrays(Module.HEAPU32[{``mode`` / 4n}], Module.HEAP32[{``indirect`` / 4n}], Module.HEAP32[{``count`` / 4n}], Module.HEAP32[{``bindingInfo`` / 4n}]);"
    override this.MultiDrawElementsIndirect(``mode`` : PrimitiveType, ``indirectBuffer`` : uint32, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        appendCommand $"Module._glMultiDrawElementsIndirect({int ``mode``}, {``indirectBuffer``}, {``count``}, {int ``indexType``}, {``bindingInfo``});"
    override this.MultiDrawElementsIndirect(``mode`` : aptr<PrimitiveType>, ``indirectBuffer`` : aptr<uint32>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``indirectBuffer``= this.Use(``indirectBuffer``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``indexType``= this.Use(``indexType``).Pointer
        let ``bindingInfo``= this.Use(``bindingInfo``).Pointer
        appendCommand $"Module._glMultiDrawElementsIndirect(Module.HEAPU32[{``mode`` / 4n}], Module.HEAPU32[{``indirectBuffer`` / 4n}], Module.HEAP32[{``count`` / 4n}], Module.HEAPU32[{``indexType`` / 4n}], Module.HEAP32[{``bindingInfo`` / 4n}]);"
    override this.MultiDrawElements(``mode`` : PrimitiveType, ``indirect`` : nativeint, ``count`` : int, ``indexType`` : DrawElementsType, ``bindingInfo`` : nativeint) = 
        appendCommand $"Module._glMultiDrawElements({int ``mode``}, {``indirect``}, {``count``}, {int ``indexType``}, {``bindingInfo``});"
    override this.MultiDrawElements(``mode`` : aptr<PrimitiveType>, ``indirect`` : aptr<nativeint>, ``count`` : aptr<int>, ``indexType`` : aptr<DrawElementsType>, ``bindingInfo`` : aptr<nativeint>) = 
        let ``mode``= this.Use(``mode``).Pointer
        let ``indirect``= this.Use(``indirect``).Pointer
        let ``count``= this.Use(``count``).Pointer
        let ``indexType``= this.Use(``indexType``).Pointer
        let ``bindingInfo``= this.Use(``bindingInfo``).Pointer
        appendCommand $"Module._glMultiDrawElements(Module.HEAPU32[{``mode`` / 4n}], Module.HEAP32[{``indirect`` / 4n}], Module.HEAP32[{``count`` / 4n}], Module.HEAPU32[{``indexType`` / 4n}], Module.HEAP32[{``bindingInfo`` / 4n}]);"
    override this.Commit() = 
        appendCommand $"Module._glCommit();"
    override this.TexSubImage2DJSImage(``target`` : TextureTarget, ``level`` : int, ``xoffset`` : int, ``yoffset`` : int, ``width`` : int, ``height`` : int, ``format`` : PixelFormat, ``typ`` : PixelType, ``imgHandle`` : int) = 
        appendCommand $"Module._glTexSubImage2DJSImage({int ``target``}, {``level``}, {``xoffset``}, {``yoffset``}, {``width``}, {``height``}, {int ``format``}, {int ``typ``}, {``imgHandle``});"
    override this.TexSubImage2DJSImage(``target`` : aptr<TextureTarget>, ``level`` : aptr<int>, ``xoffset`` : aptr<int>, ``yoffset`` : aptr<int>, ``width`` : aptr<int>, ``height`` : aptr<int>, ``format`` : aptr<PixelFormat>, ``typ`` : aptr<PixelType>, ``imgHandle`` : aptr<int>) = 
        let ``target``= this.Use(``target``).Pointer
        let ``level``= this.Use(``level``).Pointer
        let ``xoffset``= this.Use(``xoffset``).Pointer
        let ``yoffset``= this.Use(``yoffset``).Pointer
        let ``width``= this.Use(``width``).Pointer
        let ``height``= this.Use(``height``).Pointer
        let ``format``= this.Use(``format``).Pointer
        let ``typ``= this.Use(``typ``).Pointer
        let ``imgHandle``= this.Use(``imgHandle``).Pointer
        appendCommand $"Module._glTexSubImage2DJSImage(Module.HEAPU32[{``target`` / 4n}], Module.HEAP32[{``level`` / 4n}], Module.HEAP32[{``xoffset`` / 4n}], Module.HEAP32[{``yoffset`` / 4n}], Module.HEAP32[{``width`` / 4n}], Module.HEAP32[{``height`` / 4n}], Module.HEAPU32[{``format`` / 4n}], Module.HEAPU32[{``typ`` / 4n}], Module.HEAP32[{``imgHandle`` / 4n}]);"
