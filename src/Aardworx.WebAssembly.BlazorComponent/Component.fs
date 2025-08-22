namespace Aardworx.WebAssembly.BlazorComponent

open System
open Aardvark.Application
open FSharp.Data.Adaptive
open Aardvark.Base
open Microsoft.AspNetCore.Components
open Aardvark.Dom
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Microsoft.AspNetCore.Components.Rendering
open Microsoft.JSInterop
open Microsoft.JSInterop.WebAssembly

type RenderControl() =
    inherit ComponentBase()
    
    let mutable runtime : IJSRuntime = Unchecked.defaultof<_>
    let mutable scene : cval<System.Func<IRenderControl, struct(aval<Trafo3d> * aval<Trafo3d> * ISceneNode)>> = cval(System.Func<_,_>(fun _ -> struct(AVal.constant Trafo3d.Identity, AVal.constant Trafo3d.Identity, sg { () })))
    let id = RandomElementId()
    
    let mutable app : WebGLApplication = Unchecked.defaultof<_>
    let mutable ctrl : WebGLRenderControl = Unchecked.defaultof<_>
    let mutable canvas : HTMLCanvasElement = Unchecked.defaultof<_>

    [<Parameter>]
    member x.Scene
        with get() = scene.Value
        and set s = transact(fun () -> scene.Value <- s)
    
    
    [<Inject>]
    member x.Runtime
        with get() = runtime
        and set s = runtime <- s
    
    
    override this.BuildRenderTree(builder) =
        builder.AddMarkupContent(0, $"<div id=\"{id}\" style=\"position: relative\"></div>")
        base.BuildRenderTree(builder)
    
    override this.OnAfterRender(firstRender) =
        base.OnAfterRender(firstRender)
        
        if firstRender then
            JSRuntime.Instance <- runtime :?> WebAssemblyJSRuntime
            let div = Window.Document.GetElementById id
            canvas <- Window.Document.CreateCanvasElement()
            canvas.Style.Width <- "100%"
            canvas.Style.Height <- "100%"
            canvas.Style.Background <- "transparent"
            canvas.Style.Outline <- "none"
            div.AppendChild canvas
            app <- new WebGLApplication(CommandStreamMode.Managed, false)
            ctrl <- app.CreateRenderControl(canvas, Antialiasing.None)
            
            let tup = scene |> AVal.map (fun f -> f.Invoke ctrl)
            let view = tup |> AVal.bind (fun struct(v,_,_) -> v)
            let proj = tup |> AVal.bind (fun struct(_,p,_) -> p)
            
            
            let scene =
                sg {
                    Sg.View view
                    Sg.Proj proj
                    tup |> AVal.map (fun struct(_,_,s) -> s)
                }
            let task = app.Runtime.CompileRender(ctrl.FramebufferSignature, scene.GetRenderObjects(TraversalState.empty app.Runtime))
            
            
            ctrl.RenderTask <- task
          
    member x.Dispose() =
        printfn "DISPOSE"
        ctrl.Dispose()
        app.Dispose()
        canvas.Remove()
        
    interface IDisposable with
        member x.Dispose() = x.Dispose()
    
open Aardvark.Dom

type ElmRenderApp<'model, 'amodel, 'message> =
    {
        Initial         : 'model
        Update          : Env<'message> -> 'model -> 'message -> 'model
        View            : Env<'message> ->  RenderControlInfo -> 'amodel -> ISceneNode
        Unpersist       : Adaptify.Unpersist<'model, 'amodel>
    }
    
[<AbstractClass>]
type ElmComponent<'model, 'amodel, 'message>() =
    inherit ComponentBase()
    do printfn "INIT"
    let mutable runtime : IJSRuntime = Unchecked.defaultof<_>
    let id = RandomElementId()
    
    let mutable app : WebGLApplication = Unchecked.defaultof<_>

    let mutable amodel = Unchecked.defaultof<'amodel>
    let mutable env : Env<'message> = Unchecked.defaultof<_>

    abstract member ElmApp : App<'model, 'amodel, 'message>
    
    [<Inject>]
    member x.Runtime
        with get() = runtime
        and set s = runtime <- s
    
    member x.Model = amodel
    
    member x.Run(msgs : seq<'message>) =
        if not (isNull (env :> obj)) then
            env.Emit msgs
    
    override this.BuildRenderTree(builder) =
        builder.AddMarkupContent(0, $"<div id=\"{id}\"></div>")
        base.BuildRenderTree(builder)
    
    override this.OnAfterRender(firstRender) =
        base.OnAfterRender(firstRender)
        
        if firstRender then
            printfn "AFTER RENDER"
            JSRuntime.Instance <- runtime :?> WebAssemblyJSRuntime
            app <- new WebGLApplication(CommandStreamMode.Managed, false)
            let (adaptiveModel, e) = Aardworx.WebAssembly.Dom.Boot.runInElement (Some id) app this.ElmApp
            amodel <- adaptiveModel
            env <- e
            
          
    member x.Dispose() =
        printfn "DISPOSE"
        app.Dispose()
        
        
        
    interface IDisposable with
        member x.Dispose() = x.Dispose()
    
type SimpleElmComponent<'model, 'amodel, 'message>() =
    inherit ElmComponent<'model, 'amodel, 'message>()
    
    let mutable all = Unchecked.defaultof<App<'model, 'amodel, 'message>>
    
    member x.App
        with get() = all
        and set v = all <- v
    
    override x.ElmApp = all
    
    
//     
// type Thing() =
//     interface IComponent with
//         member this.Attach(renderHandle) =
//             ()
//         member this.SetParametersAsync(parameters) =
//             task {
//                 let mutable e = parameters.GetEnumerator()
//                 while e.MoveNext() do
//                     printfn "%A" e.Current
//             }
//           
//           
// type GroupComponent() =
//     inherit ComponentBase()
//     
//     let mutable children : RenderFragment = null
//     
//     [<Parameter>]
//     member x.ChildContent
//         with get() = children
//         and set (c : RenderFragment) = children <- c
//     
//     override x.OnInitialized() =
//         printfn "INIT: %A" children
//         use b = new RenderTreeBuilder()
//         children.Invoke(b)
//         let frame = b.GetFrames()
//         for i in 0 .. frame.Count - 1 do
//             let arr = frame.Array.[i]
//             printfn "%A" arr.Component
//     
//     
//           
          