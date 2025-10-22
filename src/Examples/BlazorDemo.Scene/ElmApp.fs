namespace BlazorDemo
open Aardvark.Dom.Utilities
open Aardworx.WebAssembly.BlazorComponent
open Adaptify
open FSharp.Data.Adaptive
open Aardvark.Dom
open Microsoft.AspNetCore.Components
open Aardvark.Base
open Aardvark.Rendering

module ElmApp = 

    type Model =
        { value : int }
        
    type AdaptiveModel =
        { value : cval<int> }
        
    type Message =
        | Reset
        | Increment
        | Decrement
        
        
    let update (env : Env<Message>) (model : Model) (msg : Message) : Model =
        match msg with
        | Increment -> { model with value = model.value + 1 }
        | Decrement -> { model with value = model.value - 1 }
        | Reset -> { value = 0 }
        
    let view (env : Env<Message>) (model : AdaptiveModel) =
        div {
            h1 { "Counter" }
            p { Role "status"; "Current Count: "; model.value |> AVal.map string }
            button { "Increment"; Class "btn btn-primary"; Dom.OnClick(fun _ -> env.Emit [Increment]) }
            button { "Decrement"; Class "btn btn-primary"; Dom.OnClick(fun _ -> env.Emit [Decrement]) }
            
            renderControl {
                
                Style [
                    Width "400px"
                    Height "300px"
                ]
                
                SimpleFreeFlyController {
                    Location = V3d.III * 4.0
                    LookAt = V3d.Zero
                    Sky = V3d.OOI
                    Config = None
                }
                
                let! s = RenderControl.ViewportSize
                Sg.Proj(s |> AVal.map (fun s -> Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y) |> Frustum.projTrafo))
                
                sg {
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        DefaultSurfaces.simpleLighting
                    }
                    sg {
                        Sg.OnClick(fun _ -> env.Emit [Increment])
                        Primitives.Box(Box3d.FromCenterAndSize(V3d.IOO, V3d.III), C4b.Green)
                    }
                    sg {
                        Sg.OnClick(fun _ -> env.Emit [Decrement])
                        Primitives.Box(Box3d.FromCenterAndSize(-V3d.IOO, V3d.III), C4b.Red)
                    }
                }
                
                
            }
            
        }
        
        
    let app value =
        {
            initial = { Model.value = value }
            update = update
            view = view
            unpersist =
                let init (v : Model) = { AdaptiveModel.value = cval v.value }
                let update (m : AdaptiveModel) (v : Model) = m.value.Value <- v.value
                Unpersist.create init update
        }
    
type ElmComponent() =
    inherit ElmComponent<ElmApp.Model, ElmApp.AdaptiveModel, ElmApp.Message>()
    
    let mutable initial = 0
    
    [<Parameter>]
    member x.Initial
        with get() = initial
        and set v = initial <- v
    
    
    member x.Reset() =
        x.Run [ElmApp.Message.Reset]
    
    member x.Increment() =
        x.Run [ElmApp.Message.Increment]
    
    member x.Decrement() =
        x.Run [ElmApp.Message.Decrement]
    
    member x.Value = x.Model.value :> aval<_>
    
    override this.ElmApp = ElmApp.app initial
    