namespace Aardworx.Rendering.WebGL

open Aardworx.WebAssembly
open System.Runtime.InteropServices
open Silk.NET.Core.Contexts
open System
open System.Collections.Generic
open Silk.NET.OpenGLES

#nowarn "9"

/// Raw wrappers for WebGL.
module WebGLRaw =
    
    /// WebGL context handle.
    type WebGLContextHandle = int

    /// WebGL bindings.
    module WebGL =

        /// Creates a WebGL context for a canvas with the given element-id.
        [<DllImport("WebGL")>]
        extern WebGLContextHandle emCreateContext(string id)
        
        /// swaps the front and back buffers of the current context.
        [<DllImport("WebGL")>]
        extern int emSwapBuffers()
    
        /// Destroys the given context.
        [<DllImport("WebGL")>]
        extern int emDestroyContext(WebGLContextHandle handle)

        /// Makes the given context the current context.
        [<DllImport("WebGL")>]
        extern int emMakeCurrent(WebGLContextHandle ctx)
    
        /// Returns whether the given context is the current context.
        [<DllImport("WebGL")>]
        extern int emIsCurrent(WebGLContextHandle ctx)
        
        /// Returns the current context.
        [<DllImport("WebGL")>]
        extern WebGLContextHandle emGetCurrent()
        
        /// Enables the given extension for the given context.
        [<DllImport("WebGL")>]
        extern int emEnableExtension(WebGLContextHandle ctx, string name)

        /// Returns the address of the given function.
        [<DllImport("WebGL")>]
        extern nativeint emGetProcAddress(string name)

/// WebGL context.
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

    /// Creates a new WebGL context for the given handle.
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

    /// Creates a new WebGL context for the given selector.
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
        
    /// Creates a new WebGL context by adding an invisible canvas to the document.
    static member Create() =
        let id = Guid.NewGuid() |> string
        let c = JS.Window.Document.CreateCanvasElement()
        c.Id <- id
        c.Style.Display <- "none"
        c.Width <- 1024
        c.Height <- 768
        WebGLContext.Create("#" + id)

    /// Returns the current WebGL context (or null if none is current).
    static member Current : WebGLContext = 
        let h = WebGLRaw.WebGL.emGetCurrent()
        if h = 0 then null
        else WebGLContext.Create h

    /// Disposes the context.
    member x.Dispose() =
        WebGLRaw.WebGL.emDestroyContext handle |> ignore

    /// Returns the address of the given function.
    member x.GetProcAddress(proc : string) = 
        WebGLRaw.WebGL.emGetProcAddress(proc)

    /// Returns the address of the given function (if exisiting)
    member x.TryGetProcAddress(proc : string , result : byref<nativeint>) =
        result <- WebGLRaw.WebGL.emGetProcAddress(proc)
        result <> 0n

    /// Registers an action to be executed when the context becomes current.
    member x.WhenCurrent(action : GL -> unit) =
        if x.IsCurrent then action gl
        else whenCurrent <- action :: whenCurrent

    /// Makes the context the current context.
    member x.MakeCurrent() =
        if WebGLRaw.WebGL.emGetCurrent() <> handle then
            let res = WebGLRaw.WebGL.emMakeCurrent handle
            if res <> 0 then failwithf "[WebGL] could not make %A current: %d" x res

        for c in whenCurrent do c gl
        whenCurrent <- []

    /// Releases the current context.
    member x.ReleaseCurrent() =
        if WebGLRaw.WebGL.emGetCurrent() <> handle then failwithf "[WebGL] cannot release non-current context %A" x
        let res = WebGLRaw.WebGL.emMakeCurrent 0
        if res <> 0 then failwithf "[WebGL] could not release current %A: %d" x res

    /// Swaps the front and back buffers of the current context.
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

    /// Returns whether the context is the current context.
    member x.IsCurrent =
        WebGLRaw.WebGL.emGetCurrent() = handle

    /// Returns the underlying GL object.
    member x.GL = gl

    /// Returns the handle of the context.
    member x.Handle = handle

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



exception WebGLException of string

[<AutoOpen>]
module WebGLExceptions =

    /// Raises a WebGL exception.
    let inline fail str = raise <| WebGLException str

    /// Raises a WebGL exception.
    let inline failf fmt = Printf.kprintf (fun str -> raise <| WebGLException str) fmt
