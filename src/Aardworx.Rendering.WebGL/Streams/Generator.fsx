
open System.Reflection
open System.Runtime.InteropServices


#r "System.Xml.dll"
#r "System.Xml.Linq.dll"
#r "nuget: FSharp.Data"
#r "nuget: FSharp.Data.Adaptive"
#r "nuget: Silk.NET.OpenGLES, 2.9.0"
#r "nuget: Silk.NET.OpenGLES.Extensions.EXT, 2.9.0"
open System.IO
open System.Xml
open System.Xml.Linq
open Silk.NET.Core.Native
open FSharp.Data.Adaptive

let anonymousCallCount = 8

module String =
    let lineBreak = System.Text.RegularExpressions.Regex @"\r?\n"
    let indent (cnt : int) (str : string) =
        
        if cnt < 0 then
            lineBreak.Split str
            |> Array.map (fun l -> if l.Length + cnt >= 0 then l.Substring(0, l.Length + cnt) else l)
            |> String.concat "\r\n"
        elif cnt > 0 then
            let i = System.String(' ', cnt)
            lineBreak.Split str
            |> Array.map (fun l -> i + l)
            |> String.concat "\r\n"
        else
            str
            
            
type MyGL =
    [<NativeApi(EntryPoint = "glGetBufferSubData")>]
    member _.GetBufferSubData(target : Silk.NET.OpenGLES.BufferTargetARB, offset : nativeint, size : unativeint, dst : voidptr) =
        ()

    // GLenum mode, int count, uint32_t indirectBuffer, VertexAttribInfo* infos, int attribCount) {
    [<NativeApi(EntryPoint = "glMultiDrawArraysIndirect")>]
    member _.MultiDrawArraysIndirect(mode : Silk.NET.OpenGLES.PrimitiveType, indirectBuffer : uint32, count : int, bindingInfo : nativeint) =
        ()
        
    [<NativeApi(EntryPoint = "glMultiDrawArrays")>]
    member _.MultiDrawArraysIndirect(mode : Silk.NET.OpenGLES.PrimitiveType, indirect : nativeint, count : int, bindingInfo : nativeint) =
        ()


    // glMultiDrawElementsIndirect(GLenum mode, uint32_t indirectBuffer, int count, GLenum * indexType, VertexAttribInfo * infos, int attribCount)

    [<NativeApi(EntryPoint = "glMultiDrawElementsIndirect")>]
    member _.MultiDrawElementsIndirect(mode : Silk.NET.OpenGLES.PrimitiveType, indirectBuffer : uint32, count : int, indexType : Silk.NET.OpenGLES.DrawElementsType, bindingInfo : nativeint) =
        ()
        
    [<NativeApi(EntryPoint = "glMultiDrawElements")>]
    member _.MultiDrawElementsIndirect(mode : Silk.NET.OpenGLES.PrimitiveType, indirect : nativeint, count : int, indexType : Silk.NET.OpenGLES.DrawElementsType, bindingInfo : nativeint) =
        ()
 
    [<NativeApi(EntryPoint = "glCommit")>]
    member _.Commit() =
        ()



let silkNetCommands =
    Array.concat [
        typeof<Silk.NET.OpenGLES.GL>.GetMethods(System.Reflection.BindingFlags.Instance ||| System.Reflection.BindingFlags.Public)
        typeof<MyGL>.GetMethods(System.Reflection.BindingFlags.Instance ||| System.Reflection.BindingFlags.Public)

    ]
    |> Array.choose (fun m -> 
        let hasDelegate = m.GetParameters() |> Array.exists (fun p -> typeof<System.Delegate>.IsAssignableFrom p.ParameterType)

        // TODO
        let hasPtrToVoidPtr = false // m.GetParameters() |> Array.exists (fun p -> p.ParameterType.IsPointer && p.ParameterType.GetElementType() = typeof<voidptr>)
        //let hasByRef = m.GetParameters() |> Array.exists (fun p -> p.ParameterType.IsByRef)
        if hasDelegate || hasPtrToVoidPtr then
            None
        //elif m.ReturnType <> typeof<System.Void> then  
        //    let atts = m.GetCustomAttributes(typeof<NativeApiAttribute>, true)
        //    if atts.Length > 0 then
        //        let att = unbox<NativeApiAttribute> atts.[0] 
        //        Some (m.Name, att.EntryPoint, m)
            
        else
            let atts = m.GetCustomAttributes(typeof<NativeApiAttribute>, true)
            if atts.Length > 0 then
                let att = unbox<NativeApiAttribute> atts.[0]
                //printfn "%s: %s %A" m.Name att.EntryPoint att.Prefix
                Some (m.Name, att.EntryPoint, m)
            else
                None
    )
    |> Array.groupBy (fun (_,n,m) -> 
        n
    )
    |> Array.map (fun (e, g) ->
        let (name,_,_) = g.[0]
        e.Substring 2, (g |> Array.map (fun (_, e, m) -> e,m))
    )


let primaryCommands =
    let silkAss = typeof<Silk.NET.OpenGLES.GL>.Assembly
    silkNetCommands |> Array.map (fun (name, overloads) ->
        
        let entry, best = 
            overloads |> Array.maxBy (fun (_,o) ->
 
                o.GetParameters() |> Array.sumBy (fun p -> 
                    let modifier = 
                        if p.ParameterType.Name.EndsWith "ARB" || p.ParameterType.Name.EndsWith "EXT" then -1 else 0
                    let modifier =
                        if p.ParameterType.Assembly = silkAss then modifier - 5
                        else modifier

                    if p.ParameterType.IsByRef || p.ParameterType.IsArray then -1000
                    elif p.ParameterType = typeof<Silk.NET.OpenGLES.GLEnum> then modifier + 10
                    elif p.ParameterType.Assembly = silkAss then 
                        if p.ParameterType.IsEnum then modifier + 20
                        else modifier - 20
                    else modifier
                )
            )
        name, entry, best

    )




type Spec = FSharp.Data.XmlProvider< "https://www.khronos.org/registry/OpenGL/xml/gl.xml" >


let rec typeName (typ : System.Type) =
    if typ.IsPointer || typ.IsByRef then 
        let e = typ.GetElementType()
        if e = typeof<System.Void> then "nativeint"
        //elif e = typeof<voidptr> then "nativeptr<nativeint>"
        else sprintf "nativeptr<%s>" (typeName (typ.GetElementType()))
    elif typ.IsGenericType then
        let targs = typ.GetGenericArguments() |> Array.map typeName |> String.concat ", "
        sprintf "%s<%s>" typ.Name targs
    elif typ = typeof<int8> then "int8"
    elif typ = typeof<uint8> then "uint8"
    elif typ = typeof<int16> then "int16"
    elif typ = typeof<uint16> then "uint16"
    elif typ = typeof<int> then "int"
    elif typ = typeof<uint32> then "uint32"
    elif typ = typeof<int64> then "int64"
    elif typ = typeof<uint64> then "uint64"
    elif typ = typeof<nativeint> then "nativeint"
    elif typ = typeof<unativeint> then "unativeint"
    elif typ = typeof<float32> then "float32"
    elif typ = typeof<float> then "float"
    elif typ = typeof<bool> then "Boolean"
    elif typ = typeof<System.Void> then "unit"
    else typ.Name

type ParameterKind =
    | Value
    | ReadPointer
    | AVal
    | Pointer of ofNativeInt : (string -> string)

let variants (def : MethodInfo) =
    let pars = def.GetParameters()

    let parameters = 
        pars
        |> Array.toList 
        |> List.map (fun p -> p.Name, p.ParameterType)

    let parameters =
        if def.ReturnType <> typeof<System.Void> then
            List.append parameters [ "returnValue", def.ReturnType.MakePointerType() ]
        else
            parameters

    [
        parameters |> List.map (fun (n, t) -> 
            n, typeName t, ParameterKind.Value
        )
        //parameters |> List.map (fun (n, t) -> 
        //    n, sprintf "aval<%s>" (typeName t), ParameterKind.AVal
        //)
        parameters |> List.mapi (fun i (n, t) -> 
            if not (def.Name.Contains "Draw") && not (def.Name.Contains "Image")  && not (def.Name.EndsWith "Data") && (t.IsPointer || t = typeof<voidptr>) then
                let payload =
                    if t = typeof<nativeint> || t = typeof<voidptr> then
                        sprintf "'T%d" (i+1)
                    else
                        t.GetElementType() |> typeName

                let ofNativeInt =
                    if t = typeof<nativeint> then id
                    elif t = typeof<voidptr> then id //sprintf "VoidPtr.ofNativeInt %s"
                    else sprintf "NativePtr.ofNativeInt %s"

                n, sprintf "aptr<%s>" payload, ParameterKind.Pointer ofNativeInt
            else
            //if not t.IsEnum && not t.IsPointer && t <> typeof<nativeint> then
                n, sprintf "aptr<%s>" (typeName t), ParameterKind.ReadPointer
            //else
            //    n, typeName t, ParameterKind.Value
                
        )
    ]
  
let rec cName (typ : System.Type) =
    if typ.IsEnum then
        "GLenum"
    elif typ.IsPointer || typ.IsByRef then 
        let e = typ.GetElementType()
        if e = typeof<System.Void> then "void*"
        //elif e = typeof<voidptr> then "nativeptr<nativeint>"
        else sprintf "%s*" (cName (typ.GetElementType()))
    elif typ.IsGenericType then
        let targs = typ.GetGenericArguments() |> Array.map typeName |> String.concat ", "
        sprintf "%s<%s>" typ.Name targs
    elif typ = typeof<int8> then "int8_t"
    elif typ = typeof<uint8> then "uint8_t"
    elif typ = typeof<int16> then "int16_t"
    elif typ = typeof<uint16> then "uint16_t"
    elif typ = typeof<int> then "int"
    elif typ = typeof<uint32> then "uint32_t"
    elif typ = typeof<int64> then "int64_t"
    elif typ = typeof<uint64> then "uint64_t"
    elif typ = typeof<nativeint> then "intptr_t"
    elif typ = typeof<unativeint> then "uintptr_t"
    elif typ = typeof<float32> then "float"
    elif typ = typeof<float> then "double"
    elif typ = typeof<bool> then "int"
    elif typ = typeof<System.Void> then "void"
    else typ.Name
                  

let run() =
    let spec = Spec.Load "https://www.khronos.org/registry/OpenGL/xml/gl.xml"

    let commands = 
        let mutable aliases =
            Map.empty

        let mutable defs =
            Map.empty

        for f in spec.Features do
            printfn "%A" f.Api

        let features = 
            spec.Features |> Array.filter (fun f ->
                f.Api.StartsWith "gles" && f.Number <= 3.0m
            )

        let localOnes =
            typeof<MyGL>.GetMethods(BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Instance ||| BindingFlags.DeclaredOnly)
            |> Array.choose (fun m ->
                let att = m.GetCustomAttribute<Silk.NET.Core.Native.NativeApiAttribute>()
                if isNull att then None
                elif System.String.IsNullOrWhiteSpace att.EntryPoint then None
                else Some att.EntryPoint
            )
            |> Set.ofArray


        let allCommands =
            features |> Array.collect (fun f ->
                f.Requires |> Array.collect (fun r ->
                    r.Commands |> Array.map (fun c ->
                        c.Name
                    )
                )
            ) |> Set.ofArray
            |> Set.remove "glGetPointerv"
            |> Set.remove "glGetBufferPointerv"
            |> Set.remove "glMapBufferRange"
            |> Set.remove "glUnmapBuffer"
            |> Set.remove "glFlushMappedBufferRange"
            |> Set.remove "glFramebufferTexture"
            |> Set.filter (fun n -> not (n.StartsWith "glProgramUniform"))
            |> Set.union localOnes



        for c in spec.Commands.Commands do
            let name = c.Proto.Name
            
            defs <- Map.add name c defs
            match c.Alias with
            | Some alias ->
                let alias = alias.Name
                defs <- Map.add alias c defs
                match Map.tryFind alias aliases with
                | Some l -> aliases <- Map.add alias (Set.add name l) aliases
                | None -> aliases <- Map.add alias (Set.ofList [alias; name]) aliases
            | None ->
                ()

        let rx = System.Text.RegularExpressions.Regex @"^(.*?)([a-zA-Z_0-9]+)$"

        let special =   
            Map.ofList [
                "glGetBufferSubData", [| Some "GLenum"; Some "GLintptr"; Some "GLsizeiptr"; Some "void*" |]
                "glMultiDrawArrays", [| Some "GLenum"; Some "DrawElementsIndirectCommand*"; Some "int"; Some "VertexBufferBindingInfo*"|]
                "glMultiDrawArraysIndirect", [| Some "GLenum"; Some "GLuint"; Some "int"; Some "VertexBufferBindingInfo*"|]
                "glMultiDrawElements", [| Some "GLenum"; Some "DrawElementsIndirectCommand*"; Some "int"; Some "GLenum"; Some "VertexBufferBindingInfo*"|]
                "glMultiDrawElementsIndirect", [| Some "GLenum"; Some "GLuint"; Some "int"; Some "GLenum"; Some "VertexBufferBindingInfo*"|]
                
            ]

        primaryCommands
        |> Array.toList
        |> List.choose (fun (name, entry, meth) ->
            if Set.contains entry allCommands then
                let aliases = 
                    match Map.tryFind entry aliases with
                    | Some set ->
                        let l = entry :: (set |> Set.add entry |> Set.toList)
                        l
                    | _ ->  
                        List.singleton entry

                let specPars =
                    match aliases |> List.tryPick (fun n -> Map.tryFind n special) with
                    | Some v -> Some v
                    | None ->
                        aliases |> List.tryPick (fun n -> Map.tryFind n defs) |> Option.map (fun s ->
                            s.Params |> Array.map (fun p -> 
                                let m = rx.Match p.XElement.Value
                                let typ = 
                                    if m.Success then 
                                        m.Groups.[1].Value.Trim() |> Some
                                    else 
                                        p.Ptype
                                match typ with
                                | Some "GLhandleARB" -> Some "GLuint"
                                | Some a -> Some (a.Replace("GLcharARB", "GLchar").Replace("ARB", ""))
                                | None -> None
                            )
                        )

                Some (name, aliases, specPars, meth)
            else
                None
        )

    do
        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt
        printfn "namespace Aardworx.Rendering.WebGL.Streams"
        printfn ""
        printfn "open Silk.NET.OpenGLES"
        printfn "open FSharp.Data.Adaptive"
        printfn "open Aardworx.WebAssembly"
        printfn "open Aardworx.Rendering.WebGL"
        printfn ""
        printfn "[<AbstractClass>]"
        printfn "type CommandEncoder(device : Device) ="
        printfn "    inherit CommandEncoderBase(device)"

        printfn "    abstract Switch : location : aptr<int> * cases : list<int * (CommandEncoder -> unit)> * fallback : (CommandEncoder -> unit) -> unit"

        for cnt in 0 .. anonymousCallCount do
            let def = 
                "func : aptr<nativeint>" ::
                List.init cnt (fun i -> sprintf "arg%d : aptr<'%c>" i (char (i + int 'a'))) 
                |> String.concat " * "
            printfn "    abstract Call : %s -> unit" def

        for (name, aliases, _, def) in commands do
            let pars = def.GetParameters()

            if pars.Length = 0 && def.ReturnType = typeof<System.Void> then
                printfn "    abstract %s : unit -> unit" name
            else
                let variants = variants def
                for pars in variants do
                    let pars = pars |> List.map (fun (n, t, _) -> sprintf "``%s`` : %s" n t) |> String.concat " * "
                    printfn "    abstract %s : %s -> unit" name pars
        File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "CommandEncoder.fs"), string b)

    
    do 
        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt
        printfn "// ========================================================================================================="
        printfn "// AUTOGENERATED! DO NOT EDIT MANUALLY!"
        printfn "// ========================================================================================================="
        printfn ""
        printfn "namespace Aardworx.Rendering.WebGL.Streams"
        printfn "open System.Runtime.InteropServices"
        printfn "open System.Security"
        printfn "open Silk.NET.OpenGLES"
        printfn "open Aardworx.WebAssembly"
        printfn "open Aardworx.Rendering.WebGL"
        printfn "open FSharp.Data.Adaptive"
        printfn "open Microsoft.FSharp.NativeInterop"
        printfn "open Aardvark.Base"
        printfn "open Aardworx.Rendering.WebGL"
        printfn "open Aardworx.WebAssembly"
        printfn "open Microsoft.JSInterop"
        printfn "open System.Runtime.InteropServices"
        printfn "open Microsoft.FSharp.NativeInterop"
        printfn "open System.Text"
        printfn ""
        printfn "#nowarn \"9\""
        
        printfn "type JSCommandEncoder(device : Device) ="
        printfn "    inherit CommandEncoder(device)"
        printfn "    static let dstr (v : float) = v.ToString(\"r\", System.Globalization.CultureInfo.InvariantCulture)"
        printfn "    static let fstr (v : float32) = v.ToString(\"r\", System.Globalization.CultureInfo.InvariantCulture)"
        printfn "    static let bstr (v : Boolean) = match v with | Boolean.True -> \"1\" | _ -> \"0\""
        

        printfn "    let commands = StringBuilder()"
        //printfn "    let mutable handles = JsObj.New []"
        printfn "    let mutable cachedAction = None"
        printfn "    let mutable currentGL = Unchecked.defaultof<GL>"
        printfn "    "
        printfn "    let mutable currentId = 1"
        printfn "    let cleanup = System.Collections.Generic.List<unit -> unit>()"
        printfn "    "
        printfn "    "
        printfn "    let newName() ="
        printfn "        let id = currentId"
        printfn "        let res = sprintf \"o%%04d\" id"
        printfn "        currentId <- currentId + 1"
        printfn "        res"
        printfn "    "
        printfn "    let getAction() ="
        printfn "        match cachedAction with"
        printfn "        | Some a -> a"
        printfn "        | None ->"
        printfn "            let code = sprintf \"return { run: (self) => { %%s } };\" (string commands)"
        printfn "            let a = JsObj.Evaluate<IJSInProcessObjectReference> code"
        printfn "            cachedAction <- Some a"
        printfn "            a"
        printfn "    "
        printfn "    let run(self : JsObj) ="
        printfn "        let action = getAction()"
        printfn "        action.InvokeVoid(\"run\", self.Reference :> obj)"
        printfn "    "
        printfn "    let appendCommands (str : string[]) ="
        printfn "        cachedAction <- None"
        printfn "        for str in str do"
        printfn "            commands.AppendFormat(\"{0}\\n\", str) |> ignore"
        printfn "    let appendCommand (str : string) ="
        printfn "        cachedAction <- None"
        printfn "        commands.AppendFormat(\"{0}\\n\", str) |> ignore"
        printfn "    "
        printfn "    override this.Destroy() ="
        printfn "        for c in cleanup do c()"
        printfn "        cleanup.Clear()"
        printfn "        commands.Clear() |> ignore"
        //printfn "        handles <- null"
        printfn "        cachedAction <- None"
        printfn "    "
        printfn "    override this.Clear() ="
        printfn "        for c in cleanup do c()"
        printfn "        cleanup.Clear()"
        printfn "        commands.Clear() |> ignore"
        //printfn "        handles <- JsObj.New []"
        printfn "        cachedAction <- None"
        printfn "    "
        printfn "    override x.Begin() ="
        printfn "        appendCommand \"if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }\""
        printfn "        "
        printfn "    override x.End() ="
        printfn "        ()"
        printfn "    "
        printfn "    override this.Perform(gl) ="
        printfn "        try"
        printfn "            currentGL <- gl"
        printfn "            let self = JsObj.New []"
        printfn "            run(self)"
        printfn "        finally"
        printfn "            currentGL <- Unchecked.defaultof<GL>"
        printfn ""
        printfn "    override this.Add(a,b,res) ="
        printfn "        let a = this.Use a"
        printfn "        let b = this.Use b"
        printfn "        let res = this.Use res"
        printfn "        "
        printfn "        let resOffset = int (res.Pointer / 4n)"
        printfn "        let aOffset = int (a.Pointer / 4n)"
        printfn "        let bOffset = int (b.Pointer / 4n)"
        printfn "        appendCommand $\"Module.HEAP32[{resOffset}] = Module.HEAP32[{aOffset}] + Module.HEAP32[{bOffset}];\""
        printfn "     "
        printfn "    override this.Mad(a,b,c,res) ="
        printfn "        let a = this.Use a"
        printfn "        let b = this.Use b"
        printfn "        let c = this.Use c"
        printfn "        let res = this.Use res"
        printfn "        "
        printfn "        let resOffset = int (res.Pointer / 4n)"
        printfn "        let aOffset = int (a.Pointer / 4n)"
        printfn "        let bOffset = int (b.Pointer / 4n)"
        printfn "        let cOffset = int (c.Pointer / 4n)"
        printfn "        appendCommand $\"Module.HEAP32[{resOffset}] = Module.HEAP32[{aOffset}] + Module.HEAP32[{bOffset}] * Module.HEAP32[{cOffset}];\""
        printfn ""
        printfn "    override this.Bgra(colors,count) ="
        printfn "        let colors = this.Use colors"
        printfn "        let count = this.Use count"
        printfn "        "
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  const count = Module.HEAP32[{int (count.Pointer / 4n)}];\""
        printfn "            $\"  let offset = {int colors.Pointer};\""
        printfn "            $\"  for(let i = 0; i < count; i++) {{\""
        printfn "            $\"    const r = Module.HEAPU8[offset];\""
        printfn "            $\"    Module.HEAPU8[offset] = Module.HEAPU8[offset+2];\""
        printfn "            $\"    Module.HEAPU8[offset+2] = r;\""
        printfn "            $\"    offset += 4;\""
        printfn "            $\"  }}\""
        printfn "            \"}\""
        printfn "        |]  "
        printfn "        "
        printfn "    override this.CopyBgra(src,dst,count) ="
        printfn "        let src = this.Use src"
        printfn "        let dst = this.Use dst"
        printfn "        let count = this.Use count"
        printfn "        "
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  const count = Module.HEAP32[{int (count.Pointer / 4n)}];\""
        printfn "            $\"  let srcOff = {int src.Pointer};\""
        printfn "            $\"  let dstOff = {int dst.Pointer};\""
        printfn "            $\"  for(let i = 0; i < count; i++) {{\""
        printfn "            $\"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 2];\""
        printfn "            $\"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 1];\""
        printfn "            $\"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff];\""
        printfn "            $\"    Module.HEAPU8[dstOff++] = Module.HEAPU8[srcOff + 3];\""
        printfn "            $\"    srcOff += 4;\""
        printfn "            $\"  }}\""
        printfn "            \"}\""
        printfn "        |]  "
        printfn ""
        printfn "    override this.Copy(src,dst,size) ="
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src}, {int size});\""
        printfn "            $\"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst}, {int size});\""
        printfn "            $\"  b.set(a);\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "    "
        printfn "    override this.CopyDD(src,dst,size) ="
        printfn "        let src = this.Use src"
        printfn "        let dst = this.Use dst"
        printfn "        let size = this.Use size"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];\""
        printfn "            $\"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src.Pointer}, size);\""
        printfn "            $\"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst.Pointer}, size);\""
        printfn "            $\"  b.set(a);\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "        "
        printfn "    override this.CopyDI(src,dst,size) ="
        printfn "        let src = this.Use src"
        printfn "        let dst = this.Use dst"
        printfn "        let size = this.Use size"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];\""
        printfn "            $\"  let a = new Uint8Array(Module.HEAPU8.buffer, {int src.Pointer}, size);\""
        printfn "            $\"  let b = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (dst.Pointer / 4n)}], size);\""
        printfn "            $\"  b.set(a);\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "        "
        printfn "    override this.CopyID(src,dst,size) = "
        printfn "        let src = this.Use src"
        printfn "        let dst = this.Use dst"
        printfn "        let size = this.Use size"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];\""
        printfn "            $\"  let a = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (src.Pointer / 4n)}], size);\""
        printfn "            $\"  let b = new Uint8Array(Module.HEAPU8.buffer, {int dst.Pointer}, size);\""
        printfn "            $\"  b.set(a);\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "        "
        printfn "    override this.CopyII(src,dst,size) = "
        printfn "        let src = this.Use src"
        printfn "        let dst = this.Use dst"
        printfn "        let size = this.Use size"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            $\"  let size = Module.HEAP32[{int (size.Pointer / 4n)}];\""
        printfn "            $\"  let a = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (src.Pointer / 4n)}], size);\""
        printfn "            $\"  let b = new Uint8Array(Module.HEAPU8.buffer, Module.HEAP32[{int (dst.Pointer / 4n)}], size);\""
        printfn "            $\"  b.set(a);\""
        printfn "            \"}\" "
        printfn "        |]"
        printfn "        "
        printfn "    member this.JS(code : string[]) ="
        printfn "        appendCommands code"
        printfn "        "
        printfn "    member this.Action = getAction()"
        printfn "    override this.Custom(action) = failwith \"custom not implemented\""
        // printfn "        let name = newName()"
        // printfn "        let callback ="
        // printfn "            JSActions.JSAction(fun () ->"
        // printfn "                action currentGL    "
        // printfn "            )"
        // printfn "            "
        // printfn "        let gc = GCHandle.Alloc(callback)"
        // printfn "        let r = DotNetObjectReference.Create(callback)"
        // printfn "            "
        // printfn "        handles.SetProperty(name, r)"
        // printfn "        "
        // printfn "        cleanup.Add (fun () ->"
        // printfn "            r.Dispose()"
        // printfn "            gc.Free()"
        // printfn "        )"
        // printfn "        "
        // printfn "        appendCommand [|"
        // printfn "            $\"self.{name}.invokeMethod('Invoke');\""
        // printfn "        |]"
        // printfn ""
        // printfn "    override this.Pop(mem : nativeptr<'a>) ="
        // printfn "        let s = sizeof<'a>"
        // printfn "        appendCommands [|"
        // printfn "            $\"  self.stackOffset -= {s};\""
        // printfn "            $\"  new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s}).set(new Uint8Array(self.stack, self.stackOffset, {s}));\""
        // printfn "        |]"
        // printfn "        "
        // printfn "    override this.Push(mem : nativeptr<'a>) ="
        // printfn "        let s = sizeof<'a>"
        // printfn "        appendCommands [|"
        // printfn "            $\"  new Uint8Array(self.stack, self.stackOffset, {s}).set(new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s}));\""
        // printfn "            $\"  self.stackOffset += {s};\""
        // printfn "        |]"
        // printfn ""
        printfn "    override this.Pop(mem : nativeptr<'a>) ="
        printfn "        let s = sizeof<'a>"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            \"  if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }\""
        printfn "            $\"  self.stackOffset -= {s};\""
        printfn "            $\"  let src = new Uint8Array(self.stack, self.stackOffset, {s});\""
        printfn "            $\"  let dst = new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s});\""
        printfn "            \"  dst.set(src);\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "        "
        printfn "    override this.Push(mem : nativeptr<'a>) ="
        printfn "        let s = sizeof<'a>"
        printfn "        appendCommands [|"
        printfn "            \"{\""
        printfn "            \"  if(!self.stack) { self.stack = new ArrayBuffer(65536); self.stackOffset = 0; }\""
        printfn "            $\"  let src = new Uint8Array(Module.HEAPU8.buffer, {int (NativePtr.toNativeInt mem)}, {s});\""
        printfn "            $\"  let dst = new Uint8Array(self.stack, self.stackOffset, {s});\""
        printfn "            \"  dst.set(src);\""
        printfn "            $\"  self.stackOffset += {s};\""
        printfn "            \"}\""
        printfn "        |]"
        printfn "    override this.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) ="
        printfn "        let location = this.Use(location).Pointer"
        printfn "        match cases with"
        printfn "        | [] -> fallback this"
        printfn "        | [(v, ifTrue)] ->"
        printfn "            appendCommand $\"if(Module.HEAP32[{int (location / 4n)}] == {v}) {{\""
        printfn "            ifTrue this"
        printfn "            appendCommands [| \"}\"; \"else {\" |]"
        printfn "            fallback this"
        printfn "            appendCommand \"}\""
        printfn "        | many ->"
        printfn "            appendCommands [|"
        printfn "                \"{\""
        printfn "                $\"  const value = Module.HEAP32[{int (location / 4n)}];\""
        printfn "            |]"
        printfn "            "
        printfn "            let mutable first = true"
        printfn "            for (v, ifTrue) in many do"
        printfn "                if first then"
        printfn "                    appendCommands [| $\"if(value == {v}) {{\" |]"
        printfn "                    first <- false"
        printfn "                else"
        printfn "                    appendCommands [| $\"}} else if(value == {v}) {{\" |]"
        printfn "                ifTrue this"
        printfn "                "
        printfn "            appendCommands [| \"} else {\" |]"
        printfn "            fallback this"
        printfn "            appendCommands [| \"}\"; \"}\" |]"
        printfn "            "
        printfn ""
        printfn "    override this.Call(func) = failwith \"todo\""
        printfn "    override this.Call(func,arg0) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2,arg3) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2,arg3,arg4) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5,arg6) = failwith \"todo\""
        printfn "    override this.Call(func,arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7) = failwith \"todo\""
        printfn "    "
        
        for (name, aliases, sepc, def) in commands do
            let pars = def.GetParameters()
        
            let glName =
                if def.DeclaringType = typeof<MyGL> then "_gl" + name
                else "_emscripten_gl" + name
                      
            do 
                let args =
                    let pars = pars |> Array.map (fun p -> if isNull p then null, null else p.Name, p.ParameterType)
                    let pars = 
                        if def.ReturnType <> typeof<System.Void> then
                            Array.append pars [| "result", def.ReturnType.MakePointerType() |]
                        else pars
                    pars |> Seq.map (fun (n, t) -> sprintf "``%s`` : %s" n (typeName t)) |> String.concat ", "
                    
                let parUse =
                    pars |> Array.map (fun p ->
                        if p.ParameterType.Name = "Boolean" then sprintf "{bstr ``%s``}" p.Name
                        elif p.ParameterType = typeof<float32> then sprintf "{fstr ``%s``}" p.Name
                        elif p.ParameterType = typeof<float> then sprintf "{dstr ``%s``}" p.Name
                        elif p.ParameterType.IsEnum then sprintf "{int ``%s``}" p.Name
                        elif p.ParameterType.IsPointer then
                            if p.ParameterType = typeof<nativeint> || p.ParameterType = typeof<voidptr> then sprintf "{int ``%s``}" p.Name
                            elif p.ParameterType = typeof<unativeint> then sprintf "{uint32 ``%s``}" p.Name
                            elif p.ParameterType.GetElementType() = typeof<System.Void> then sprintf "{int ``%s``}" p.Name
                            else sprintf "{int (NativePtr.toNativeInt ``%s``)}" p.Name
                        else sprintf "{``%s``}" p.Name
                    )
                printfn "    override this.%s(%s) = " name args
                if def.ReturnType <> typeof<System.Void> then
                    if def.ReturnType = typeof<int> || def.ReturnType = typeof<uint32> ||
                       def.ReturnType = typeof<bool> || def.ReturnType = typeof<nativeint> || def.ReturnType.IsEnum || def.ReturnType.IsPointer then
                        printfn "        appendCommand $\"Module.HEAPU32[{NativePtr.toNativeInt result / 4n}] = Module.%s(%s);\"" glName (String.concat ", " parUse)
                    else
                        failwithf "unexpected return-type: %A" def.ReturnType
                else
                    printfn "        appendCommand $\"Module.%s(%s);\"" glName (String.concat ", " parUse)
               
        
               
            if pars.Length > 0 || def.ReturnType <> typeof<System.Void> then
                let args =
                    let pars = pars |> Array.map (fun p -> p.Name, p.ParameterType)
                    let pars = 
                        if def.ReturnType <> typeof<System.Void> then
                            Array.append pars [| "result", def.ReturnType.MakePointerType() |]
                        else pars
                        
                    pars |> Seq.map (fun (n, t) ->
                        let typeName = 
                            if not (def.Name.Contains "Draw") && not (def.Name.Contains "Image")  && not (def.Name.EndsWith "Data") && (t.IsPointer || t = typeof<voidptr>) then
                                let el = t.GetElementType()
                                if el = typeof<System.Void> then sprintf "aptr<_>"
                                else sprintf "aptr<%s>" (typeName el)
                            else
                                sprintf "aptr<%s>" (typeName t)
                            
                        sprintf "``%s`` : %s" n typeName
                    ) |> String.concat ", "
                    
                let parUse =
                    pars |> Array.map (fun p ->
                        if p.ParameterType = typeof<float32> then sprintf "Module.HEAPF32[{``%s`` / 4n}]" p.Name
                        elif p.ParameterType = typeof<float> then sprintf "Module.HEAPF64[{``%s`` / 8n}]" p.Name
                        elif p.ParameterType.IsEnum then sprintf "Module.HEAPU32[{``%s`` / 4n}]" p.Name
                        elif p.ParameterType = typeof<uint8> then sprintf "Module.HEAPU8[{``%s``}]" p.Name
                        elif p.ParameterType = typeof<int8> then sprintf "Module.HEAP8[{``%s``}]" p.Name
                        elif p.ParameterType = typeof<uint16> then sprintf "Module.HEAPU16[{``%s`` / 2n}]" p.Name
                        elif p.ParameterType = typeof<int16> then sprintf "Module.HEAP16[{``%s`` / 2n}]" p.Name
                        elif p.ParameterType = typeof<uint32> then sprintf "Module.HEAPU32[{``%s`` / 4n}]" p.Name
                        elif p.ParameterType = typeof<int32> then sprintf "Module.HEAP32[{``%s`` / 4n}]" p.Name
                        elif not (def.Name.Contains "Draw") && not (def.Name.Contains "Image")  && not (def.Name.EndsWith "Data") && (p.ParameterType.IsPointer || p.ParameterType = typeof<voidptr>) then
              
                            sprintf "{``%s``}" p.Name 
                        elif p.ParameterType.IsPointer || p.ParameterType = typeof<voidptr> || p.ParameterType = typeof<nativeint> then
                            sprintf "Module.HEAP32[{``%s`` / 4n}]" p.Name 
                            
                        else sprintf "{``%s``}" p.Name 
                    )
                printfn "    override this.%s(%s) = " name args
                for p in pars do
                    printfn "        let ``%s``= this.Use(``%s``).Pointer" p.Name p.Name
                    
                if def.ReturnType <> typeof<System.Void> then
                    
                    printfn "        let result = this.Use(result).Pointer" 
                    
                if def.ReturnType <> typeof<System.Void> then
                    if def.ReturnType = typeof<int> || def.ReturnType = typeof<uint32> ||
                       def.ReturnType = typeof<bool> || def.ReturnType = typeof<nativeint> || def.ReturnType.IsEnum || def.ReturnType.IsPointer then
                        printfn "        appendCommand $\"Module.HEAPU32[{result / 4n}] = Module.%s(%s);\"" glName (String.concat ", " parUse)
                    else
                        failwithf "unexpected return-type: %A" def.ReturnType
                        
                else
                    printfn "        appendCommand $\"Module.%s(%s);\"" glName (String.concat ", " parUse)
        
       
        File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "JSCommandEncoder.fs"), string b)
        
    do 
        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt
        printfn "// ========================================================================================================="
        printfn "// AUTOGENERATED! DO NOT EDIT MANUALLY!"
        printfn "// ========================================================================================================="
        printfn ""
        printfn "namespace Aardworx.Rendering.WebGL.Streams"
        printfn "open System.Runtime.InteropServices"
        printfn "open System.Security"
        printfn "open Silk.NET.OpenGLES"
        printfn "open Aardworx.WebAssembly"
        printfn "open Aardworx.Rendering.WebGL"
        printfn "open FSharp.Data.Adaptive"
        printfn "open Microsoft.FSharp.NativeInterop"
        printfn "open Aardvark.Base"
        printfn ""
        printfn "#nowarn \"9\""
        printfn ""
        printfn ""
        printfn "type internal GLFunctionPointers() ="
        for (name, aliases, _, _) in commands do
            printfn "    [<DefaultValue>]"
            printfn "    val mutable public %s : nativeint" (Seq.head aliases)
        
        printfn ""
        printfn "" 
        printfn "module internal GLFunctionPointers ="
        printfn "    let private cache = System.Collections.Concurrent.ConcurrentDictionary<Silk.NET.Core.Contexts.INativeContext, GLFunctionPointers>()"
        printfn "    let private getProc1 (ctx : Silk.NET.Core.Contexts.INativeContext) (name : string) ="
        printfn "        let mutable res = 0n"
        printfn "        if ctx.TryGetProcAddress(name, &res) then res"
        printfn "        else 0n"
        printfn ""
        printfn "    let private getProc (ctx : Silk.NET.Core.Contexts.INativeContext) (names : list<string>) ="
        printfn "        names |> List.tryPick (fun n -> let p = getProc1 ctx n in if p <> 0n then Some p else None) |> Option.defaultValue 0n"
        printfn ""
        printfn "    let private load (ctx : Silk.NET.Core.Contexts.INativeContext) ="
        printfn "        let res = GLFunctionPointers()"
        for (name, aliases, _, _) in commands do
            let all = aliases |> List.map (sprintf "\"%s\"") |> String.concat "; "
            printfn "        res.%s <- getProc ctx [%s]" (Seq.head aliases) all
        printfn "        res"
        printfn ""
        printfn "    let get (ctx : Silk.NET.Core.Contexts.INativeContext) ="
        printfn "        cache.GetOrAdd(ctx, fun ctx ->"
        printfn "            load ctx"
        printfn "        )"
        printfn ""
        
        printfn "module internal GLDelegates ="
        for (name, aliases, _, def) in commands do
            let pars = def.GetParameters()
            let args = 
                if pars.Length = 0 then "unit"
                else pars |> Seq.map (fun p -> sprintf "``%s`` : %s" p.Name (typeName p.ParameterType)) |> String.concat " * "
            printfn "    [<SuppressUnmanagedCodeSecurity>]"
            printfn "    type %sDelegate = delegate of %s -> %s" (Seq.head aliases) args (typeName def.ReturnType)

        printfn "    type GLDelegates() ="
        for (name, aliases, _, _) in commands do
            printfn "        [<DefaultValue>]"
            printfn "        val mutable public %s : %sDelegate" (Seq.head aliases) (Seq.head aliases)
        printfn "    let private cache = System.Collections.Concurrent.ConcurrentDictionary<Silk.NET.Core.Contexts.INativeContext, GLDelegates>()"
        printfn "    let private getDelegate<'t> (ptr : nativeint) ="
        printfn "        if ptr = 0n then Unchecked.defaultof<'t>"
        printfn "        else Marshal.GetDelegateForFunctionPointer(ptr, typeof<'t>) |> unbox<'t>"
        printfn "    let get (ctx : Silk.NET.Core.Contexts.INativeContext) ="
        printfn "        cache.GetOrAdd(ctx, fun ctx ->"
        printfn "            let ptrs = GLFunctionPointers.get ctx"
        printfn "            let res = GLDelegates()"
        for (name, aliases, _, def) in commands do
            let noArgs = 
                match def.GetParameters().Length with
                | 0 -> "()"
                | cnt -> Seq.init cnt (fun _ -> "_") |> String.concat " "
            printfn "            res.%s <- if ptrs.%s = 0n then %sDelegate(fun %s -> failwith \"%s not found\") else getDelegate<%sDelegate> ptrs.%s" (Seq.head aliases) (Seq.head aliases) (Seq.head aliases) noArgs (Seq.head aliases) (Seq.head aliases) (Seq.head aliases)
        printfn "            res"
        printfn "        )"

        // ==========================================================================================
        // ManagedCommandEncoder
        // ==========================================================================================
        printfn "type ManagedCommandEncoder(device : Device) ="
        printfn "    inherit CommandEncoder(device)"
        printfn "    let gl = GLDelegates.get device.Context"
        printfn "    let mutable currentGL = Unchecked.defaultof<_>"
        printfn "    let commands = System.Collections.Generic.List<unit -> unit>()"
        printfn "    let mutable stack : list<int64> = []"
        printfn ""
        printfn "    override x.Destroy() ="
        printfn "        commands.Clear()"
        printfn ""
        printfn "    override x.Clear() ="
        printfn "        commands.Clear()"
        printfn ""
        printfn "    override x.Perform gl ="
        printfn "        currentGL <- gl"
        printfn "        stack <- []"
        printfn "        for i in 0 .. commands.Count - 1 do commands.[i] ()"
        printfn ""
        printfn "    override x.Custom(action : GL -> unit) ="
        printfn "        commands.Add (fun () -> action currentGL)"
        printfn ""
        printfn "    override x.Push (location : nativeptr<'a>) ="
        printfn "        commands.Add <| fun () -> "
        printfn "            if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack"
        printfn "            else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack"
        printfn "    "
        printfn "    override x.Pop (location : nativeptr<'a>) ="
        printfn "        commands.Add <| fun () -> "
        printfn "            let h = List.head stack"
        printfn "            stack <- List.tail stack"
        printfn "            if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)"
        printfn "            else NativePtr.write location (Unchecked.reinterpret (int h))"
        printfn "    "
        printfn "    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = "
        printfn "        let location = x.Use location"
        printfn "        let table ="
        printfn "            cases |> Map.ofList |> Map.map (fun _ action -> "
        printfn "                let res = new ManagedCommandEncoder(device)"
        printfn "                x.AddNested res"
        printfn "                action (res :> CommandEncoder)"
        printfn "                res"
        printfn "            )"
        printfn "        let def = "
        printfn "            let res = new ManagedCommandEncoder(device)"
        printfn "            x.AddNested res"
        printfn "            fallback (res :> CommandEncoder)"
        printfn "            res"
        printfn "        commands.Add <| fun () ->"
        printfn "            let v = location.Value"
        printfn "            match Map.tryFind v table with"
        printfn "            | Some c -> c.UnsafeRunSynchronously currentGL"
        printfn "            | None -> def.UnsafeRunSynchronously currentGL"
        printfn "    "
        printfn "    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) ="
        printfn "        commands.Add <| fun () -> Marshal.Copy(src, dst, size)"
        printfn "    "
        printfn "    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        commands.Add <| fun () -> Marshal.Copy(src.Pointer, dst.Pointer, size.Value)"
        printfn "    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        commands.Add <| fun () -> Marshal.Copy(src.Pointer, dst.Value, size.Value)"
        printfn "    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        commands.Add <| fun () -> Marshal.Copy(src.Value, dst.Pointer, size.Value)"
        printfn "    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        commands.Add <| fun () -> Marshal.Copy(src.Value, dst.Value, size.Value)"
        printfn "    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        commands.Add <| fun () -> NativePtr.write res (NativePtr.read a + NativePtr.read b)"
        printfn "    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let c = x.Use(c).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        commands.Add <| fun () -> NativePtr.write res (NativePtr.read a + NativePtr.read b * NativePtr.read c)"
        
        printfn "    override x.Bgra(values : aptr<byte>, count : aptr<int>) = "
        printfn "        let values = x.Use(values).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        commands.Add <| fun () ->"
        printfn "            let mutable off = 0"
        printfn "            for i in 0 .. NativePtr.read count - 1 do"
        printfn "                let t = NativePtr.get values off"
        printfn "                NativePtr.set values off (NativePtr.get values (off + 2))"
        printfn "                NativePtr.set values (off + 2) t"
        printfn "                off <- off + 4"
        
        printfn "    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = "
        printfn "        let src = x.Use(src).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let dst = x.Use(dst).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        commands.Add <| fun () ->"
        printfn "            let mutable off = 0"
        printfn "            for i in 0 .. NativePtr.read count - 1 do"
        printfn "                NativePtr.set dst (off + 2) (NativePtr.get src (off+0))"
        printfn "                NativePtr.set dst (off + 1) (NativePtr.get src (off+1))"
        printfn "                NativePtr.set dst (off + 0) (NativePtr.get src (off+2))"
        printfn "                NativePtr.set dst (off + 3) (NativePtr.get src (off+3))"
        printfn "                off <- off + 4"

        for cnt in 0 .. anonymousCallCount do
            let def = 
                "func : aptr<nativeint>" ::
                List.init cnt (fun i -> sprintf "arg%d : aptr<'%c>" i (char (i + int 'a'))) 
                |> String.concat ", "

            let args = List.init cnt (fun i -> sprintf "arg%d.Value :> obj" i) |> String.concat "; " 
            let argTypes = List.init cnt (fun i -> sprintf "typeof<'%c>" (char (int 'a' + i))) |> String.concat "; "
            printfn "    override this.Call(%s) =" def
            printfn "        let tDel = DelegateType.Get([%s], typeof<unit>)" argTypes
            for i in 0 .. cnt - 1 do
                printfn "        let arg%d = this.Use arg%d" i i
            printfn "        if func.IsVolatile then"
            printfn "            let func = this.Use func"
            printfn "            commands.Add (fun () ->"
            printfn "                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)"
            printfn "                del.DynamicInvoke [| %s |] |> ignore" args
            printfn "            )"
            printfn "        else" 
            printfn "            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)"
            printfn "            commands.Add (fun () ->"
            printfn "                del.DynamicInvoke [| %s |] |> ignore" args
            printfn "            )"
            printfn ""
            printfn ""
            

        for (name, aliases, _, def) in commands do
            let pars = def.GetParameters()
            if pars.Length = 0 && def.ReturnType = typeof<System.Void> then
                printfn "    override this.%s() = " name
                printfn "        commands.Add <| fun () -> gl.%s.Invoke()" (Seq.head aliases)

            else
                for parameters in variants def do
                    let parDef = parameters |> List.map (fun (n,t,_) -> sprintf "``%s`` : %s" n t) |> String.concat ", "
                    let parUse = 
                        parameters |> List.map (fun (n,t,k) -> 
                            match k with
                            | AVal -> sprintf "``%s``.Value" n
                            | Value -> sprintf "``%s``" n
                            | ReadPointer -> sprintf "``%s``.Value" n
                            | Pointer r -> r (sprintf "``%s``.Pointer" n)
                        )

                    let parUse =
                        if def.ReturnType <> typeof<System.Void> then
                            List.take (List.length parUse - 1) parUse
                            |> String.concat ", "
                        else
                            parUse |> String.concat ", "

                    printfn "    override this.%s(%s) = " name parDef
                    for (n, _, k) in parameters do      
                        match k with
                        | AVal -> printfn "        let ``%s`` = this.Pin ``%s``" n n
                        | ReadPointer | Pointer _ -> printfn "        let ``%s`` = this.Use ``%s``" n n
                        | Value -> ()

                    if def.ReturnType <> typeof<System.Void> then
                        let (_,_,k) = List.last parameters
                        match k with
                        | Value ->
                            printfn "        commands.Add <| fun () -> gl.%s.Invoke(%s) |> NativePtr.write returnValue" (Seq.head aliases) parUse
                        | _ ->
                            printfn "        commands.Add <| fun () -> gl.%s.Invoke(%s) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)" (Seq.head aliases) parUse
                    else
                        printfn "        commands.Add <| fun () -> gl.%s.Invoke(%s)" (Seq.head aliases) parUse
              
        // ==========================================================================================
        // ImmediateCommandEncoder
        // ==========================================================================================
        printfn "type internal ImmediateCommandEncoder(device : Device, currentGL : GL) ="
        printfn "    inherit CommandEncoder(device)"
        printfn "    let gl = GLDelegates.get device.Context"
        printfn "    let mutable stack : list<int64> = []"
        printfn "    let mutable pointers = System.Collections.Generic.HashSet<AdaptivePointer>()"
        printfn ""
        printfn "    member private x.Acquire(r : AdaptivePointer) ="
        printfn "        if pointers.Add r then r.Acquire()"
        printfn "        if not r.IsConstant then r.Update AdaptiveToken.Top"
        printfn ""
        printfn "    override x.Begin() ="
        printfn "        stack <- []"
        printfn ""
        printfn "    override x.End() ="
        printfn "        stack <- []"
        printfn "        x.Dispose()"
        printfn ""
        printfn "    override x.Destroy() ="
        printfn "        if pointers.Count > 0 then "
        printfn "             for p in pointers do p.Release()"
        printfn "             pointers <- System.Collections.Generic.HashSet<AdaptivePointer>()"
        printfn ""
        printfn "    override x.Clear() ="
        printfn "        if pointers.Count > 0 then "
        printfn "             for p in pointers do p.Release()"
        printfn "             pointers <- System.Collections.Generic.HashSet<AdaptivePointer>()"
        printfn ""
        printfn "    override x.Perform gl ="
        printfn "        ()"
        printfn ""
        printfn "    override x.Custom(action : GL -> unit) ="
        printfn "        action currentGL"
        printfn ""
        printfn "    override x.Push (location : nativeptr<'a>) ="
        printfn "        if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack"
        printfn "        else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack"
        printfn "    "
        printfn "    override x.Pop (location : nativeptr<'a>) ="
        printfn "        let h = List.head stack"
        printfn "        stack <- List.tail stack"
        printfn "        if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)"
        printfn "        else NativePtr.write location (Unchecked.reinterpret (int h))"
        printfn "    "
        printfn "    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = "
        printfn "        x.Acquire location"
        printfn "        let v = location.Value"
        printfn "        match cases |> List.tryPick (fun (vi, c) -> if vi = v then Some c else None)  with"
        printfn "        | Some c -> c (x :> CommandEncoder)"
        printfn "        | None -> fallback (x :> CommandEncoder)"
        
        printfn "    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) ="
        printfn "        Marshal.Copy(src, dst, size)"
        printfn "    "
        printfn "    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        x.Acquire src; x.Acquire dst; x.Acquire size"
        printfn "        Marshal.Copy(src.Pointer, dst.Pointer, size.Value)"
        printfn "    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        x.Acquire src; x.Acquire dst; x.Acquire size"
        printfn "        Marshal.Copy(src.Pointer, dst.Value, size.Value)"
        printfn "    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        x.Acquire src; x.Acquire dst; x.Acquire size"
        printfn "        Marshal.Copy(src.Value, dst.Pointer, size.Value)"
        printfn "    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        x.Acquire src; x.Acquire dst; x.Acquire size"
        printfn "        Marshal.Copy(src.Value, dst.Value, size.Value)"
        printfn "    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        x.Acquire a; x.Acquire b; x.Acquire res"
        printfn "        let pa = a.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let pb = b.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let pr = res.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        NativePtr.write pr (NativePtr.read pa + NativePtr.read pb)"
        printfn "    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        x.Acquire a; x.Acquire b; x.Acquire c; x.Acquire res"
        printfn "        let pa = a.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let pb = b.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let pc = c.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let pr = res.Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        NativePtr.write pr (NativePtr.read pa + NativePtr.read pb * NativePtr.read pc)"
        
        printfn "    override x.Bgra(values : aptr<byte>, count : aptr<int>) = "
        printfn "        x.Acquire(values); x.Acquire(count)"
        printfn "        let values = values.Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = count.Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        let mutable off = 0"
        printfn "        for i in 0 .. NativePtr.read count - 1 do"
        printfn "            let t = NativePtr.get values off"
        printfn "            NativePtr.set values off (NativePtr.get values (off + 2))"
        printfn "            NativePtr.set values (off + 2) t"
        printfn "            off <- off + 4"

        
        printfn "    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = "
        printfn "        x.Acquire(src); x.Acquire(dst); x.Acquire(count)"
        printfn "        let src = src.Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let dst = dst.Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = count.Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        let mutable off = 0"
        printfn "        for i in 0 .. NativePtr.read count - 1 do"
        printfn "            NativePtr.set dst (off + 2) (NativePtr.get src (off+0))"
        printfn "            NativePtr.set dst (off + 1) (NativePtr.get src (off+1))"
        printfn "            NativePtr.set dst (off + 0) (NativePtr.get src (off+2))"
        printfn "            NativePtr.set dst (off + 3) (NativePtr.get src (off+3))"
        printfn "            off <- off + 4"



        for cnt in 0 .. anonymousCallCount do
            let def = 
                "func : aptr<nativeint>" ::
                List.init cnt (fun i -> sprintf "arg%d : aptr<'%c>" i (char (i + int 'a'))) 
                |> String.concat ", "

            let args = List.init cnt (fun i -> sprintf "arg%d.Value :> obj" i) |> String.concat "; " 
            let argTypes = List.init cnt (fun i -> sprintf "typeof<'%c>" (char (int 'a' + i))) |> String.concat "; "
            printfn "    override this.Call(%s) =" def
            printfn "        let tDel = DelegateType.Get([%s], typeof<unit>)" argTypes
            printfn "        this.Acquire func"
            for i in 0 .. cnt - 1 do
                printfn "        this.Acquire arg%d" i
            printfn "        let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)"
            printfn "        del.DynamicInvoke [| %s |] |> ignore" args
            printfn ""
            printfn ""
            

        for (name, aliases, _, def) in commands do
            let pars = def.GetParameters()
            if pars.Length = 0 && def.ReturnType = typeof<System.Void> then
                printfn "    override this.%s() = " name
                printfn "        gl.%s.Invoke()" (Seq.head aliases)

            else
                for parameters in variants def do
                    let parDef = parameters |> List.map (fun (n,t,_) -> sprintf "``%s`` : %s" n t) |> String.concat ", "
                    let parUse = 
                        parameters |> List.map (fun (n,t,k) -> 
                            match k with
                            | AVal -> sprintf "``%s``.Value" n
                            | Value -> sprintf "``%s``" n
                            | ReadPointer -> sprintf "``%s``.Value" n
                            | Pointer r -> r (sprintf "``%s``.Pointer" n)
                        )
                        
                    let parUse =
                        if def.ReturnType <> typeof<System.Void> then
                            List.take (List.length parUse - 1) parUse
                            |> String.concat ", "
                        else
                            parUse |> String.concat ", "

                    printfn "    override this.%s(%s) = " name parDef
                    for (n, _, k) in parameters do      
                        match k with
                        | Value -> ()
                        | _ -> printfn "        this.Acquire ``%s``" n
                    if def.ReturnType <> typeof<System.Void> then
                        let (_,_,k) = List.last parameters
                        match k with
                        | Value ->
                            printfn "        gl.%s.Invoke(%s) |> NativePtr.write returnValue" (Seq.head aliases) parUse
                        | _ ->
                            printfn "        gl.%s.Invoke(%s) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)" (Seq.head aliases) parUse
                    else
                        printfn "        gl.%s.Invoke(%s)" (Seq.head aliases) parUse

              
              

        // ==========================================================================================
        // DebugCommandEncoder
        // ==========================================================================================
        
        printfn "type DebugCommandEncoder(device : Device) ="
        printfn "    inherit CommandEncoder(device)"
        printfn "    let gl = GLDelegates.get device.Context"
        printfn "    let mutable currentGL = Unchecked.defaultof<GL>" 
        printfn "    let mutable stack : list<int64> = []"
        printfn "    let commands = System.Collections.Generic.List<(unit -> string) * (unit -> unit)>()"
        printfn ""
        printfn "    [<DefaultValue; System.ThreadStatic>]"
        printfn "    static val mutable private InCommandStream : bool"
        printfn ""
        printfn "    override x.Destroy() ="
        printfn "        commands.Clear()"
        printfn ""
        printfn "    override x.Clear() ="
        printfn "        commands.Clear()"
        printfn ""
        printfn "    override x.Custom(action : GL -> unit) ="
        printfn "        commands.Add ((fun () -> \"custom\"), (fun () -> action currentGL))"
        printfn ""
        printfn "    override x.Push (location : nativeptr<'a>) ="
        printfn "        let run() = "
        printfn "            if sizeof<'a> = sizeof<int64> then stack <- Unchecked.reinterpret (NativePtr.read location) :: stack"
        printfn "            else stack <- int64 (Unchecked.reinterpret<_,int> (NativePtr.read location)) :: stack"
        printfn "        let name() ="
        printfn "            sprintf \"Push(%%A)\" location"
        printfn "        commands.Add (name, run)"
        printfn "    "
        printfn "    override x.Pop (location : nativeptr<'a>) ="
        printfn "        let run() = "
        printfn "            let h = List.head stack"
        printfn "            stack <- List.tail stack"
        printfn "            if sizeof<'a> = sizeof<int64> then NativePtr.write location (Unchecked.reinterpret h)"
        printfn "            else NativePtr.write location (Unchecked.reinterpret (int h))"
        printfn "        let name() ="
        printfn "            sprintf \"Pop(%%A)\" location"
        printfn "        commands.Add (name, run)"
        printfn "    "
        printfn "    override x.Perform gl ="
        printfn "        let o = DebugCommandEncoder.InCommandStream"
        printfn "        if not o then"
        printfn "            DebugCommandEncoder.InCommandStream <- true"
        printfn "        currentGL <- gl"
        printfn "        stack <- []"
        printfn "        for i in 0 .. commands.Count - 1 do"
        printfn "            let (getName, run) = commands.[i]"
        printfn "            let name = getName()"
        printfn "            if not (System.String.IsNullOrWhiteSpace name) then Aardvark.Base.Report.Debug(\"{0}\", name)"
        printfn "            device.CurrentCall <- Some name"
        printfn "            run()"
        printfn "        if not o then"
        printfn "            DebugCommandEncoder.InCommandStream <- false"
        printfn ""
        printfn "    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, fallback : CommandEncoder -> unit) = "
        printfn "        let location = x.Use location"
        printfn "        let table ="
        printfn "            cases |> Map.ofList |> Map.map (fun _ action -> "
        printfn "                let res = new DebugCommandEncoder(device)"
        printfn "                x.AddNested res"
        printfn "                action (res :> CommandEncoder)"
        printfn "                res"
        printfn "            )"
        printfn "        let def = "
        printfn "            let res = new DebugCommandEncoder(device)"
        printfn "            x.AddNested res"
        printfn "            fallback (res :> CommandEncoder)"
        printfn "            res"
        printfn "        let run() ="
        printfn "            let v = location.Value"
        printfn "            match Map.tryFind v table with"
        printfn "            | Some c -> c.UnsafeRunSynchronously currentGL"
        printfn "            | None -> def.UnsafeRunSynchronously currentGL"
        printfn "        let name() ="
        printfn "            \"\""
        printfn "        commands.Add(name, run)"
        printfn "    "
        
        printfn "    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) ="
        printfn "        let run() = Marshal.Copy(src, dst, size)"
        printfn "        let name() = sprintf \"memcpy(%%A, %%A, %%A)\" src dst size"
        printfn "        commands.Add(name, run)"
        printfn "    "
        printfn "    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        let run() = Marshal.Copy(src.Pointer, dst.Pointer, size.Value)"
        printfn "        let name() = sprintf \"memcpy(%%A, %%A, %%A)\" src.Pointer dst.Pointer size.Value"
        printfn "        commands.Add(name, run)"
        printfn "    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        let run() = Marshal.Copy(src.Pointer, dst.Value, size.Value)"
        printfn "        let name() = sprintf \"memcpy(%%A, %%A, %%A)\" src.Pointer dst.Value size.Value"
        printfn "        commands.Add(name, run)"
        printfn "    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        let run() = Marshal.Copy(src.Value, dst.Pointer, size.Value)"
        printfn "        let name() = sprintf \"memcpy(%%A, %%A, %%A)\" src.Value dst.Pointer size.Value"
        printfn "        commands.Add(name, run)"
        printfn "    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
        printfn "        let src = x.Use src"
        printfn "        let dst = x.Use dst"
        printfn "        let size = x.Use size"
        printfn "        let run() = Marshal.Copy(src.Value, dst.Value, size.Value)"
        printfn "        let name() = sprintf \"memcpy(%%A, %%A, %%A)\" src.Value dst.Value size.Value"
        printfn "        commands.Add(name, run)"
        printfn "    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let run() = NativePtr.write res (NativePtr.read a + NativePtr.read b)"
        printfn "        let name() = sprintf \"add(%%A, %%A, %%A)\" a b res"
        printfn "        commands.Add(name, run)"
        printfn "    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) ="
        printfn "        let a = x.Use(a).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let b = x.Use(b).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let c = x.Use(c).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let res = x.Use(res).Pointer |> NativePtr.ofNativeInt<nativeint>"
        printfn "        let run() = NativePtr.write res (NativePtr.read a + NativePtr.read b * NativePtr.read c)"
        printfn "        let name() = sprintf \"mad(%%A, %%A, %%A, %%A)\" a b c res"
        printfn "        commands.Add(name, run)"

        
        printfn "    override x.Bgra(values : aptr<byte>, count : aptr<int>) = "
        printfn "        let values = x.Use(values).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        let run() ="
        printfn "            let mutable off = 0"
        printfn "            for i in 0 .. NativePtr.read count - 1 do"
        printfn "                let t = NativePtr.get values off"
        printfn "                NativePtr.set values off (NativePtr.get values (off + 2))"
        printfn "                NativePtr.set values (off + 2) t"
        printfn "                off <- off + 4"
        printfn "        commands.Add((fun () -> \"bgra\"), run)"
        
        printfn "    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = "
        printfn "        let src = x.Use(src).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let dst = x.Use(dst).Pointer |> NativePtr.ofNativeInt<byte>"
        printfn "        let count = x.Use(count).Pointer |> NativePtr.ofNativeInt<int>"
        printfn "        let run() ="
        printfn "            let mutable off = 0"
        printfn "            for i in 0 .. NativePtr.read count - 1 do"
        printfn "                NativePtr.set dst (off + 2) (NativePtr.get src (off+0))"
        printfn "                NativePtr.set dst (off + 1) (NativePtr.get src (off+1))"
        printfn "                NativePtr.set dst (off + 0) (NativePtr.get src (off+2))"
        printfn "                NativePtr.set dst (off + 3) (NativePtr.get src (off+3))"
        printfn "                off <- off + 4"
        printfn "        commands.Add((fun () -> \"copyBgra\"), run)"

        for cnt in 0 .. anonymousCallCount do
            let def = 
                "func : aptr<nativeint>" ::
                List.init cnt (fun i -> sprintf "arg%d : aptr<'%c>" i (char (i + int 'a'))) 
                |> String.concat ", "

            let args = List.init cnt (fun i -> sprintf "arg%d.Value :> obj" i) |> String.concat "; " 
            let curr = List.init cnt (fun i -> sprintf "arg%d.Value" i) |> String.concat " " 
            let fmt = List.init cnt (fun i -> "%A") |> String.concat ", "
            let argTypes = List.init cnt (fun i -> sprintf "typeof<'%c>" (char (int 'a' + i))) |> String.concat "; "
            printfn "    override this.Call(%s) =" def
            printfn "        let tDel = DelegateType.Get([%s], typeof<unit>)" argTypes
            for i in 0 .. cnt - 1 do
                printfn "        let arg%d = this.Use arg%d" i i
            printfn "        if func.IsVolatile then"
            printfn "            let func = this.Use func"
            printfn "            let name() ="
            printfn "                sprintf \"%%016X(%s)\" func.Value %s" fmt curr
            printfn "            let run() ="
            printfn "                let del = Marshal.GetDelegateForFunctionPointer(func.Value, tDel)"
            printfn "                del.DynamicInvoke [| %s |] |> ignore" args
            printfn "            commands.Add(name, run)"
            printfn "        else"
            printfn "            let del = Marshal.GetDelegateForFunctionPointer(this.Use(func).Value, tDel)"
            printfn "            let name() ="
            printfn "                sprintf \"%%016X(%s)\" func.Value %s" fmt curr
            printfn "            let run() ="
            printfn "                del.DynamicInvoke [| %s |] |> ignore" args
            printfn "            commands.Add(name, run)"
            printfn ""
        for (name, aliases, _, def) in commands do
            let pars = def.GetParameters()
            if pars.Length = 0 && def.ReturnType = typeof<System.Void> then
                printfn "    override this.%s() = " name
                printfn "        let run() = gl.%s.Invoke()" (Seq.head aliases)
                printfn "        commands.Add(((fun () -> \"%s\"), run))" name

            else
                for parameters in variants def do
                    let parDef = parameters |> List.map (fun (n,t,_) -> sprintf "``%s`` : %s" n t) |> String.concat ", "
                    let parUse = 
                        parameters |> List.map (fun (n,t,k) -> 
                            match k with
                            | AVal -> sprintf "``%s``.Value" n
                            | Value -> sprintf "``%s``" n
                            | ReadPointer -> sprintf "``%s``.Value" n
                            | Pointer r -> r (sprintf "``%s``.Pointer" n)
                        )
                        
                    let parUse =
                        if def.ReturnType <> typeof<System.Void> then
                            List.take (List.length parUse - 1) parUse
                        else
                            parUse 


                    let printUses = 
                        parameters |> List.map (fun (n,t,k) -> 
                            match k with
                            | AVal -> sprintf "``%s``.Value" n
                            | Value -> sprintf "``%s``" n
                            | ReadPointer -> sprintf "(``%s``.Value)" n
                            | Pointer _ -> sprintf "(``%s``.Pointer)" n
                        )
                        
                    let printUses =
                        if def.ReturnType <> typeof<System.Void> then
                            List.take (List.length printUses - 1) printUses
                        else
                            printUses


                    let format =
                        printUses |> List.map (fun _ -> "%A") |> String.concat ", "

                    printfn "    override this.%s(%s) = " name parDef
                    for (n, _, k) in parameters do      
                        match k with
                        | AVal -> printfn "        let ``%s`` = this.Pin ``%s``" n n
                        | ReadPointer | Pointer _ -> printfn "        let ``%s`` = this.Use ``%s``" n n
                        | Value -> ()
                    if def.ReturnType <> typeof<System.Void> then
                        let (_,_,k) = List.last parameters
                        match k with
                        | Value ->
                            printfn "        let run() = gl.%s.Invoke(%s) |> NativePtr.write returnValue" (Seq.head aliases) (String.concat ", " parUse)
                        | _ ->
                            printfn "        let run() = gl.%s.Invoke(%s) |> NativePtr.write (NativePtr.ofNativeInt returnValue.Pointer)" (Seq.head aliases) (String.concat ", " parUse)
                        
                    else
                        printfn "        let run() = gl.%s.Invoke(%s)" (Seq.head aliases) (String.concat ", " parUse)
                    printfn "        let name() = sprintf \"%s(%s)\" %s" name format (String.concat " " printUses)
                    printfn "        commands.Add((name, run))"


        File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "ManagedCommandEncoder.fs"), string b)
    
        
        let interpreterCommands =
            commands
            |> List.map (fun (name, _, pars, def) ->
                let getCType (i : int) =
                    match pars with
                    | Some p -> p.[i]
                    | None -> None
                let pars = def.GetParameters() |> Array.mapi (fun i p -> p.ParameterType, p.Name, getCType i)
                name, def.ReturnType, pars
            )
            |> List.collect (fun (name, ret, pars) ->
                if pars.Length > 0 || ret <> typeof<System.Void> then
                    [
                        name, ret, false, pars
                        name, ret, true, (pars |> Array.map (fun (a,b,c) -> a,b,c))
                    ]
                else    
                    [name, ret, false, pars]
            )

        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt

        let copyDD = 512
        let copyDI = 513
        let copyID = 514
        let copyII = 515
        let add = 516
        let mad = 517
        let copy = 518
        let custom = 519
        let switch = 520
        let jmp = 521
        let log = 522
        let push1 = 523
        let pop1 = 524
        let push2 = 525
        let pop2 = 526
        let push4 = 527
        let pop4 = 528
        let push8 = 529
        let pop8 = 530
        let bgra = 531
        let copyBgra = 532

        do
            printfn "#include <emscripten.h>"
            printfn "#include <emscripten/html5.h>"
            printfn "#include <string.h>"
            printfn "#include <stdlib.h>"
            printfn "#include <stdint.h>"
            printfn "#include <GLES3/gl3.h>"
            printfn "#include \"../Native/WebGL.h\""
            printfn ""
            printfn "typedef enum {"
            let mutable commandCount = 0
            for i, (name, _, ptr, _) in Seq.indexed interpreterCommands do
                if ptr then printfn "    %sI = %d," name i
                else printfn "    %s = %d," name i
                commandCount <- commandCount + 1

            printfn "    CopyDD = %d," copyDD
            printfn "    CopyDI = %d," copyDI
            printfn "    CopyID = %d," copyID
            printfn "    CopyII = %d," copyII
            printfn "    Add = %d," add
            printfn "    Mad = %d," mad
            printfn "    Copy = %d," copy
            printfn "    Custom = %d," custom
            printfn "    Switch = %d," switch
            printfn "    Jmp = %d," jmp
            printfn "    Log = %d," log
            printfn "    Push1 = %d," push1
            printfn "    Pop1 = %d," pop1
            printfn "    Push2 = %d," push2
            printfn "    Pop2 = %d," pop2
            printfn "    Push4 = %d," push4
            printfn "    Pop4 = %d," pop4
            printfn "    Push8 = %d," push8
            printfn "    Pop8 = %d," pop8
            printfn "    Bgra = %d," bgra
            printfn "    CopyBgra = %d," copyBgra
            printfn "} OpCode;"
    
            printfn "int emInterpret(intptr_t code, int length, intptr_t stack) {"
            printfn "    intptr_t e = code + length;"
            printfn "    int value, cnt;"
            printfn "    int temp1, temp2;"
            printfn "    while(code < e) {"
            printfn "        OpCode op = (OpCode)(*(uint16_t*)code);"
            printfn "        code += 2;"
            printfn "        switch(op) {"
            
            for i, (name, ret, isPtr, pars) in Seq.indexed interpreterCommands do
   
                let readCount (t : System.Type) =
                    if isPtr then 
                        if not (name.Contains "Draw") && not (name.Contains "Image") && not (name.EndsWith "Data") && (t.IsPointer || t = typeof<voidptr>) then
                            if t.IsPointer then 
                                1
                            else 
                                2
                        else
                            2
                    else 
                        1

                let mutable offset = 0
                let args =
                    pars |> Array.map (fun (t, n, ct) ->
                        let rc = readCount t
                        let t = 
                            match ct with
                            | Some ct -> ct
                            | None -> cName t
                        let ptr = 
                            if offset = 0 then "code"
                            else sprintf "(code + %d)" offset
                        let v = 
                            match rc with
                            | 1 -> sprintf "*(%s*)%s" t ptr
                            | 2 -> sprintf "**(%s**)%s" t ptr
                            | _ -> failwithf "bad readCount: %d" rc
                            //if isPtr then sprintf "**(%s**)%s" t ptr
                            //else sprintf "*(%s*)%s" t ptr
                        offset <- offset + 4
                        v
                    )

                let caseName = 
                    if isPtr then sprintf "%sI" name
                    else name
                printfn "        case %s:" caseName
                if ret = typeof<System.Void> then
                    printfn "            gl%s(%s);" name (String.concat ", " args)
                    printfn "            code += %d;" offset
                    printfn "            break;"
                else    
                    
                    let retPtr = 
                        if ret.IsPointer && ret.GetElementType() = typeof<byte> then sprintf "*(const GLubyte**)(code + %d)" offset
                        elif ret = typeof<nativeint> then  sprintf "*(GLsync*)(code + %d)" offset
                        else sprintf "*(%s*)(code + %d)" (cName ret) offset
                    printfn "            %s = gl%s(%s);" retPtr name (String.concat ", " args)
                    printfn "            code += %d;" (offset + 4)
                    printfn "            break;"

            printfn "        case CopyDD:"
            printfn "            memcpy(*(void**)(code+4), *(void**)code, **(size_t**)(code+8));"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case CopyDI:"
            printfn "            memcpy(**(void***)(code+4), *(void**)code, **(size_t**)(code+8));"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case CopyID:"
            printfn "            memcpy(**(void***)(code+4), *(void**)code, **(size_t**)(code+8));"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case CopyII:"
            printfn "            memcpy(**(void***)(code+4), **(void***)code, **(size_t**)(code+8));"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case Add:"
            printfn "            *(intptr_t*)(code+8) = *(intptr_t*)code + *(intptr_t*)(code+4);"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case Mad:"
            printfn "            *(intptr_t*)(code+12) = *(intptr_t*)code + *(intptr_t*)(code+4) * *(intptr_t*)(code+8);"
            printfn "            code += 16;"
            printfn "            break;"
            printfn "        case Copy:"
            printfn "            memcpy(*(void**)(code+4), *(void**)code, **(size_t**)(code+8));"
            printfn "            code += 12;"
            printfn "            break;"
            printfn "        case Custom:"
            printfn "            ((void (*) (void))**(void***)code)();"
            printfn "            code += 4;"
            printfn "            break;"
            printfn "        case Switch:"
            printfn "            value = **(int**)code; code += 4;"
            printfn "            cnt = *(int*)code; code += 4;"
            printfn "            for(int i = 0; i < cnt; i++) {"
            printfn "                int v = *(int*)code; code += 4;"
            printfn "                int o = *(int*)code; code += 4;"
            printfn "                if (v == value) {"
            printfn "                    code += o;"
            printfn "                    break;"
            printfn "                }"
            printfn "            }"
            printfn "            break;"
            printfn "        case Jmp:"
            printfn "            code += *(intptr_t*)code;"
            printfn "            break;"
            printfn "        case Log:"
            printfn "            cnt = *(int*)code; code += 4;"
            printfn "            emscripten_log(EM_LOG_INFO, (const char*)code); code += cnt;"
            printfn "            break;"
            printfn "        case Push1:"
            printfn "            *(uint8_t*)stack = **(uint8_t**)code;"
            printfn "            code += 4; stack += 1;"
            printfn "            break;"
            printfn "        case Push2:"
            printfn "            *(uint16_t*)stack = **(uint16_t**)code;"
            printfn "            code += 4; stack += 2;"
            printfn "            break;"
            printfn "        case Push4:"
            printfn "            *(uint32_t*)stack = **(uint32_t**)code;"
            printfn "            code += 4; stack += 4;"
            printfn "            break;"
            printfn "        case Push8:"
            printfn "            *(uint64_t*)stack = **(uint64_t**)code;"
            printfn "            code += 4; stack += 8;"
            printfn "            break;"
            printfn "        case Pop1:"
            printfn "            stack -= 1;"
            printfn "            **(uint8_t**)code = *(uint8_t*)stack;"
            printfn "            code += 4;"
            printfn "            break;"
            printfn "        case Pop2:"
            printfn "            stack -= 2;"
            printfn "            **(uint16_t**)code = *(uint16_t*)stack;"
            printfn "            code += 4;"
            printfn "            break;"
            printfn "        case Pop4:"
            printfn "            stack -= 4;"
            printfn "            **(uint32_t**)code = *(uint32_t*)stack;"
            printfn "            code += 4;"
            printfn "            break;"
            printfn "        case Pop8:"
            printfn "            stack -= 8;"
            printfn "            **(uint64_t**)code = *(uint64_t*)stack;"
            printfn "            code += 4;"
            printfn "            break;"
            printfn "        case Bgra:"
            printfn "            value = *(intptr_t*)code; code += 4;"
            printfn "            cnt = **(int**)code; code += 4;"
            printfn "            temp1 = 0;"
            printfn "            for(int i = 0; i < cnt; i++) {"
            printfn "                 temp2 = ((char*)value)[temp1];"
            printfn "                 ((char*)value)[temp1] = ((char*)value)[temp1+2];"
            printfn "                 ((char*)value)[temp1+2] = (char)temp2;"
            printfn "                 temp1 += 4;"
            printfn "            }"
            printfn "            break;"
            printfn "        case CopyBgra:"
            printfn "            temp1 = *(intptr_t*)code; code += 4;"
            printfn "            temp2 = *(intptr_t*)code; code += 4;"
            printfn "            cnt = **(int**)code; code += 4;"
            printfn "            for(int i = 0, o = 0; i < cnt; i++, o+=4) {"
            printfn "                 ((char*)temp2)[o+2] = ((char*)temp1)[o];"
            printfn "                 ((char*)temp2)[o+1] = ((char*)temp1)[o+1];"
            printfn "                 ((char*)temp2)[o] = ((char*)temp1)[o+2];"
            printfn "                 ((char*)temp2)[o+3] = ((char*)temp1)[o+3];"
            printfn "            }"
            printfn "            break;"

            printfn "        default:"
            printfn "            EM_ASM_({ console.error(\"bad OP: \", $0); }, op);"
            printfn "            return -1;"
            printfn "        }"
            printfn "    }"
            printfn "    return 0;"
            printfn "}"
            printfn ""
            printfn "typedef struct Fragment {"
            printfn "    struct Fragment* Prev;"
            printfn "    struct Fragment* Next;"
            printfn "    intptr_t Code;"
            printfn "    int      Length;"
            printfn "} Fragment;"
            printfn "int emRun(Fragment* frag, intptr_t stack) {"
            printfn "    while(frag) {"
            printfn "        if(emInterpret(frag->Code, frag->Length, stack) != 0) return -1;"
            printfn "        frag = frag->Next;"
            printfn "    }"
            printfn "    return 0;"
            printfn "}"


        File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "Interpreter.c"), string b)
    
    
        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt
        do
            printfn "namespace Aardworx.Rendering.WebGL.Streams"
            printfn "open Silk.NET.OpenGLES"
            printfn "open FSharp.Data.Adaptive"
            printfn "open Aardvark.Base"
            printfn "open Aardworx.WebAssembly"
            printfn "open Aardworx.Rendering.WebGL"
            printfn "open System.Runtime.InteropServices"
            printfn "open System.Runtime.InteropServices"
            printfn "open Microsoft.FSharp.NativeInterop"
            printfn "#nowarn \"9\""
            //printfn "type OpCode ="
            //for i, (name, aliases, def) in Seq.indexed commands do
            //    printfn "    | %s = %dus" name i

            //printfn ""

            printfn "module private Interpreter ="
            printfn "    [<DllImport(\"Interpreter\")>]"
            printfn "    extern int emInterpret(nativeint code, int length, nativeint stack)"
            
            printfn "type NativeCommandEncoder(device : Device) ="
            printfn "    inherit CommandEncoder(device)"
            printfn "    let mutable currentGL = Unchecked.defaultof<GL>"
            printfn "    let mutable capacity = 128"
            printfn "    let mutable mem = Marshal.AllocHGlobal capacity"
            printfn "    let mutable current = mem"
            printfn "    let mutable len = 0"
            printfn "    let mutable entry ="
            printfn "        let p = NativePtr.alloc<nativeint> 1"
            printfn "        NativePtr.write p mem"
            printfn "        p"

            printfn "    let resize (newSize : int) = "
            printfn "        if newSize <> capacity then"
            printfn "            let o = current - mem"
            printfn "            let n = Marshal.AllocHGlobal newSize"
            printfn "            Marshal.Copy(mem, n, min newSize len)"
            printfn "            Marshal.FreeHGlobal mem"
            printfn "            mem <- n"
            printfn "            NativePtr.write entry mem"
            printfn "            current <- mem + o"
            
            printfn "    let ensureFree (add : int) ="
            printfn "        let e = len + add"
            printfn "        if e > capacity then resize (Fun.NextPowerOfTwo e)"
            
            printfn "    member x.Entry = entry"
            printfn "    member x.Pointer = mem"
            printfn "    member x.Length = len"

            printfn "    member private x.Write(value : uint8) ="
            printfn "        ensureFree 1"
            printfn "        NativePtr.write (NativePtr.ofNativeInt current) value"
            printfn "        current <- current + 1n"
            printfn "        len <- len + 1"
            
            printfn "    member private x.Write(value : uint16) ="
            printfn "        ensureFree 2"
            printfn "        NativePtr.write (NativePtr.ofNativeInt current) value"
            printfn "        current <- current + 2n"
            printfn "        len <- len + 2"
            
            printfn "    member private x.Write(value : uint32) ="
            printfn "        ensureFree 4"
            printfn "        NativePtr.write (NativePtr.ofNativeInt current) value"
            printfn "        current <- current + 4n"
            printfn "        len <- len + 4"
            
            printfn "    member private x.Write(value : int) ="
            printfn "        ensureFree 4"
            printfn "        NativePtr.write (NativePtr.ofNativeInt current) value"
            printfn "        current <- current + 4n"
            printfn "        len <- len + 4"
            
            printfn "    member private x.Write(value : nativeint) ="
            printfn "        ensureFree 4"
            printfn "        NativePtr.write (NativePtr.ofNativeInt current) value"
            printfn "        current <- current + 4n"
            printfn "        len <- len + 4"

            let sizeof (t : System.Type) =
                if t.IsPointer || t = typeof<nativeint> || t = typeof<unativeint> || t.IsByRef then 4
                elif t.IsEnum then 4
                else Marshal.SizeOf t

            for i, (name, ret, isPtr, args) in Seq.indexed interpreterCommands do
                let typeName (t : System.Type) =
                    if isPtr then 
                        if not (name.Contains "Draw") && not (name.Contains "Image") && not (name.EndsWith "Data") && (t.IsPointer || t = typeof<voidptr>) then
                            if t.IsPointer then 
                                let el = t.GetElementType()
                                if el = typeof<System.Void> then sprintf "aptr<_>"
                                else sprintf "aptr<%s>" (typeName el)
                            else 
                                typeName t |> sprintf "aptr<%s>"
                        else
                            typeName t |> sprintf "aptr<%s>"
                    else 
                        typeName t

                let sizeof (t : System.Type) =
                    if isPtr then 4
                    else sizeof t

                let argDef = 
                    let args = 
                        if ret <> typeof<System.Void> then 
                            Array.append args  [|ret.MakePointerType(), "returnValue", None|]
                        else args
                    args |> Array.map (fun (t, n, _) -> sprintf "``%s`` : %s" n (typeName t)) |> String.concat ", "

                let needed = 2 + (args |> Array.sumBy (fun (t,_,_) -> sizeof t))
                printfn "    override this.%s(%s) =" name argDef
                printfn "        let e = len + %d" needed
                printfn "        if e > capacity then resize (Fun.NextPowerOfTwo e)"
                printfn "        NativePtr.write (NativePtr.ofNativeInt current) %dus" i
                printfn "        current <- current + 2n"
                for (t,n,_) in args do
                    let value =
                        if isPtr then sprintf "(this.Use(``%s``).Pointer)" n
                        else sprintf "``%s``" n
                    printfn "        NativePtr.write (NativePtr.ofNativeInt current) %s" value
                    printfn "        current <- current + %dn" (sizeof t)
                    
                printfn "        len <- len + %d" needed

            let needed = 2 + 12
            printfn "    override x.CopyDD(src : aptr<'a>, dst : aptr<'a>, size : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copyDD
            printfn "        x.Write(x.Use(src).Pointer)"
            printfn "        x.Write(x.Use(dst).Pointer)"
            printfn "        x.Write(x.Use(size).Pointer)"
            
            printfn "    override x.CopyDI(src : aptr<'a>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copyDI
            printfn "        x.Write(x.Use(src).Pointer)"
            printfn "        x.Write(x.Use(dst).Pointer)"
            printfn "        x.Write(x.Use(size).Pointer)"
            
            printfn "    override x.CopyID(src : aptr<nativeint>, dst : aptr<'a>, size : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copyID
            printfn "        x.Write(x.Use(src).Pointer)"
            printfn "        x.Write(x.Use(dst).Pointer)"
            printfn "        x.Write(x.Use(size).Pointer)"

            printfn "    override x.CopyII(src : aptr<nativeint>, dst : aptr<nativeint>, size : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copyII
            printfn "        x.Write(x.Use(src).Pointer)"
            printfn "        x.Write(x.Use(dst).Pointer)"
            printfn "        x.Write(x.Use(size).Pointer)"
            
            let needed = 2 + 12
            printfn "    override x.Copy(src : nativeint, dst : nativeint, size : nativeint) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copy
            printfn "        x.Write(src)"
            printfn "        x.Write(dst)"
            printfn "        x.Write(size)"

            let needed = 2 + 12
            printfn "    override x.Add(a : aptr<nativeint>, b : aptr<nativeint>, res : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" add
            printfn "        x.Write(x.Use(a).Pointer)"
            printfn "        x.Write(x.Use(b).Pointer)"
            printfn "        x.Write(x.Use(res).Pointer)"
            
            let needed = 2 + 16
            printfn "    override x.Mad(a : aptr<nativeint>, b : aptr<nativeint>, c : aptr<nativeint>, res : aptr<nativeint>) ="
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" mad
            printfn "        x.Write(x.Use(a).Pointer)"
            printfn "        x.Write(x.Use(b).Pointer)"
            printfn "        x.Write(x.Use(c).Pointer)"
            printfn "        x.Write(x.Use(res).Pointer)"
            
            let needed = 2 + 4
            printfn "    override x.Custom(action : GL -> unit) ="
            printfn "        let ptr = x.Use(APtr.pinDelegate (System.Action(fun () -> action currentGL)))"
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" custom
            printfn "        x.Write(ptr.Pointer)"

            let needed = 8
            printfn "    override x.Bgra(values : aptr<byte>, count : aptr<int>) = "
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" bgra
            printfn "        x.Write(x.Use(values).Pointer)"
            printfn "        x.Write(x.Use(count).Pointer)"
            
            let needed = 12
            printfn "    override x.CopyBgra(src : aptr<byte>, dst : aptr<byte>, count : aptr<int>) = "
            printfn "        ensureFree %d" needed
            printfn "        x.Write(%dus)" copyBgra
            printfn "        x.Write(x.Use(src).Pointer)"
            printfn "        x.Write(x.Use(dst).Pointer)"
            printfn "        x.Write(x.Use(count).Pointer)"


            printfn "    override x.Push(value : nativeptr<'a>) ="
            printfn "        match sizeof<'a> with"
            printfn "        | 1 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" push1
            printfn "        | 2 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" push2
            printfn "        | 4 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" push4
            printfn "        | 8 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" push8
            printfn "        | _ -> failwith \"not implemented\""

            printfn "    override x.Pop(value : nativeptr<'a>) ="
            printfn "        match sizeof<'a> with"
            printfn "        | 1 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" pop1
            printfn "        | 2 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" pop2
            printfn "        | 4 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" pop4
            printfn "        | 8 -> x.Write(%dus); x.Write(NativePtr.toNativeInt value)" pop8
            printfn "        | _ -> failwith \"not implemented\""

            printfn "    member x.Log(str : string) ="
            printfn "        x.Write(%dus)" log
            printfn "        let data = System.Text.Encoding.UTF8.GetBytes(str)"
            printfn "        x.Write (data.Length + 1)"
            printfn "        for b in data do x.Write b"
            printfn "        x.Write 0uy"


            printfn "    override x.Switch(location : aptr<int>, cases : list<int * (CommandEncoder -> unit)>, otherwise : CommandEncoder -> unit) ="
            printfn "        let cases = List.toArray cases"
            printfn "        ensureFree (10 + 8*cases.Length)"
            printfn "        x.Write(%dus)" switch
            printfn "        x.Write(x.Use(location).Pointer)"
            printfn "        x.Write(cases.Length)"
            printfn "        let caseOffsets ="
            printfn "            cases |> Array.map (fun (i, e) ->"
            printfn "                x.Write(i)"
            printfn "                let o = current - mem"
            printfn "                x.Write(0n)"
            printfn "                o"
            printfn "            )"
            printfn "        otherwise x"

            printfn "        let endjumps = System.Collections.Generic.List<nativeint>()"
            printfn "        x.Write(%dus)" jmp
            printfn "        endjumps.Add (current - mem)"
            printfn "        x.Write 0n"

            printfn "        for i in 0 .. cases.Length - 1 do"
            printfn "            let (k, a) = cases.[i]"
            printfn "            let pi = current - mem"
            printfn "            let pj = caseOffsets.[i]"
            printfn "            NativePtr.write (NativePtr.ofNativeInt (mem + pj)) (pi - pj - 4n)"
            printfn "            a x"

            printfn "            x.Write(%dus)" jmp
            printfn "            endjumps.Add (current - mem)"
            printfn "            x.Write 0n"

            printfn "        let fin = current - mem"
            printfn "        for o in endjumps do"
            printfn "            NativePtr.write (NativePtr.ofNativeInt (mem + o)) (fin - o)"
            
            printfn "    override x.Call(func : aptr<nativeint>) : unit ="
            printfn "        x.Write(%dus)" custom
            printfn "        x.Write(x.Use(func).Pointer)"

            for cnt in 1 .. anonymousCallCount do
                let def = 
                    "func : aptr<nativeint>" ::
                    List.init cnt (fun i -> sprintf "arg%d : aptr<'%c>" i (char (i + int 'a'))) 
                    |> String.concat ", "
                printfn "    override __.Call(%s) : unit = failwith \"not implemented\"" def


            printfn "    override x.Destroy() = "
            printfn "        Marshal.FreeHGlobal mem"
            printfn "        mem <- 0n"

            printfn "    override x.Clear() ="
            printfn "        Marshal.FreeHGlobal mem"
            printfn "        capacity <- 128"
            printfn "        mem <- Marshal.AllocHGlobal capacity"
            printfn "        current <- mem"
            printfn "        len <- 0"

            printfn "    override x.Perform(gl : GL) = "
            printfn "        currentGL <- gl"
            printfn "        use stack = fixed (Array.zeroCreate<int> 512)"
            printfn "        if Interpreter.emInterpret(mem, len, NativePtr.toNativeInt stack) <> 0 then failwith \"interpreter failed\""

        File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "NativeCommandEncoder.fs"), string b)



run()