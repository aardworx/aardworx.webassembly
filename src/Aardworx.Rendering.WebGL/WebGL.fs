namespace Aardworx.Rendering.WebGL

open Aardworx.WebAssembly
open System.Runtime.InteropServices
open Silk.NET.Core.Contexts
open System
open System.Collections.Generic
open Silk.NET.OpenGLES

#nowarn "9"

module WebGLRaw =
    

    type WebGLContextHandle = int

    module WebGL =

    
        [<DllImport("WebGL")>]
        extern WebGLContextHandle emCreateContext(string id)
        
        [<DllImport("WebGL")>]
        extern int emSwapBuffers()
    
        [<DllImport("WebGL")>]
        extern int emDestroyContext(WebGLContextHandle handle)

        [<DllImport("WebGL")>]
        extern int emMakeCurrent(WebGLContextHandle ctx)
    
        [<DllImport("WebGL")>]
        extern int emIsCurrent(WebGLContextHandle ctx)
        
        [<DllImport("WebGL")>]
        extern WebGLContextHandle emGetCurrent()
        
        [<DllImport("WebGL")>]
        extern int emEnableExtension(WebGLContextHandle ctx, string name)

        [<DllImport("WebGL")>]
        extern nativeint emGetProcAddress(string name)

[<AllowNullLiteral>]
type WebGLContext private(handle : WebGLRaw.WebGLContextHandle) =
    static let selectorTable = Dictionary<string, WebGLRaw.WebGLContextHandle>()
    static let table = Dictionary<WebGLRaw.WebGLContextHandle, WebGLContext>()

    let mutable gl = Unchecked.defaultof<GL>

    let mutable whenCurrent : list<GL -> unit> = []

    member private x.Init(g : GL) =
        gl <- g

    member internal x.EnableExtension(name : string) =
        WebGLRaw.WebGL.emEnableExtension(handle, name) <> 0

    static member Create(handle : WebGLRaw.WebGLContextHandle) : WebGLContext =
        if handle = 0 then failwith "[WebGL] invalid context handle" 
        lock table (fun () ->
            match table.TryGetValue handle with
            | true, ctx -> ctx
            | _ ->
                let ctx = new WebGLContext(handle)

                let gl = GL.GetApi ctx
                ctx.Init(gl)

                table.[handle] <- ctx
                ctx
        )

    static member Create(selector : string) : WebGLContext =
        let handle = 
            lock selectorTable (fun () ->
                match selectorTable.TryGetValue selector with
                | true, ctx -> ctx
                | _ ->
                    let handle = WebGLRaw.WebGL.emCreateContext(selector)
                    if handle = 0 then failwithf "[WebGL] could not create context for %A" selector
                    selectorTable.[selector] <- handle
                    handle
            )
        WebGLContext.Create(handle)
        
    static member Create() =
        let id = Guid.NewGuid() |> string
        let c = JS.Window.Document.CreateCanvasElement()
        c.Id <- id
        c.Style.Display <- "none"
        c.Width <- 1024
        c.Height <- 768
        WebGLContext.Create("#" + id)

    static member Current : WebGLContext = 
        let h = WebGLRaw.WebGL.emGetCurrent()
        if h = 0 then null
        else WebGLContext.Create h

    member x.Dispose() =
        WebGLRaw.WebGL.emDestroyContext handle |> ignore

    member x.GetProcAddress(proc : string) = 
        WebGLRaw.WebGL.emGetProcAddress(proc)

    member x.TryGetProcAddress(proc : string , result : byref<nativeint>) =
        result <- WebGLRaw.WebGL.emGetProcAddress(proc)
        result <> 0n

    member x.WhenCurrent(action : GL -> unit) =
        if x.IsCurrent then action gl
        else whenCurrent <- action :: whenCurrent

    member x.MakeCurrent() =
        if WebGLRaw.WebGL.emGetCurrent() <> handle then
            let res = WebGLRaw.WebGL.emMakeCurrent handle
            if res <> 0 then failwithf "[WebGL] could not make %A current: %d" x res

        for c in whenCurrent do c gl
        whenCurrent <- []

    member x.ReleaseCurrent() =
        if WebGLRaw.WebGL.emGetCurrent() <> handle then failwithf "[WebGL] cannot release non-current context %A" x
        let res = WebGLRaw.WebGL.emMakeCurrent 0
        if res <> 0 then failwithf "[WebGL] could not release current %A: %d" x res

    member x.SwapBuffers() =
        let o = WebGLContext.Current
        if o = x then 
            let res = WebGLRaw.WebGL.emSwapBuffers()
            if res <> 0 then failwithf "[WebGL] could not swap buffers: %d" res
        else
            try
                x.MakeCurrent()
                let res = WebGLRaw.WebGL.emSwapBuffers()
                if res <> 0 then failwithf "[WebGL] could not swap buffers: %d" res
            finally
                if isNull o then x.ReleaseCurrent()
                else o.MakeCurrent()


    member x.IsCurrent =
        WebGLRaw.WebGL.emGetCurrent() = handle

    member x.GL = gl

    member private x.Handle = handle

    override x.ToString() =
        sprintf "GL:%08X" handle

    override x.GetHashCode() =
        hash handle

    override x.Equals(o : obj) =
        match o with
        | :? WebGLContext as o -> o.Handle = handle
        | _ -> false

    interface IComparable with
        member x.CompareTo(o : obj) =
            compare handle (o :?> WebGLContext).Handle

    interface IComparable<WebGLContext> with
        member x.CompareTo(o : WebGLContext) =
            compare handle o.Handle
            
    interface INativeContext with
        member x.Dispose() = x.Dispose()
        member x.GetProcAddress(name, _slot) = x.GetProcAddress(name)
        member x.TryGetProcAddress(name, result, _slot) = x.TryGetProcAddress(name, &result)



[<AutoOpen>]
module WebGLExceptions =

    let inline fail str = failwith ("[WebGL] " + str)

    let inline failf fmt = Printf.kprintf (fun str -> failwith ("[WebGL] " + str)) fmt
