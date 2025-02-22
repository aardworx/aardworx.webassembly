namespace App

open Aardvark.Base
open Aardvark.Rendering
open FSharp.Data.Adaptive
open Aardvark.Dom
open Adaptify
open App

module Shader =
    open FShade
    
    let nothing (v : Effects.Vertex) =
        fragment {
            return v.c
        }
        

    type Fragment =
        {
            [<Semantic("PickViewPosition")>] vp : V3d
        }
    
    let withViewPos (v : Effects.Vertex) =
        fragment {
            let vp = uniform.ProjTrafoInv * v.pos
            let vp = vp.XYZ / vp.W
            let vp = vp + V3d(0.1, 0.0, 0.0)
            return { vp = vp.XYZ }
        }
        

type Message =
    | Increment
    | Decrement
    | Hover of option<V3d>
    | Click of V3d
    | Update of Index * V3d
    | StartDrag of Index
    | StopDrag 
    | Delete of Index
    | Clear
    | CameraMessage of OrbitMessage

module App =
    let initial = 
        { 
            Value = 3
            Hover = None
            Points = IndexList.empty
            DraggingPoint = None 
            Camera = OrbitState.create V3d.Zero 1.0 0.3 3.0 Button.Left Button.Middle
        }

    let update (env : Env<Message>) (model : Model) (msg : Message) =
        match msg with
        | CameraMessage msg ->
            { model with Camera = OrbitController.update (Env.map CameraMessage env) model.Camera msg }
        | Increment ->
            { model with Value = model.Value + 1 }
        | Decrement ->
            { model with Value = model.Value - 1 }
        | Hover p ->
            { model with Hover = p }
        | Click p ->
            { model with Points = IndexList.add p model.Points }
        | Update(idx, p) ->
            match model.DraggingPoint with
            | Some (i, _) when i = idx -> { model with DraggingPoint = Some(idx, p) }
            | _ -> model
            //{ model with Points = IndexList.set idx p model.Points }
        | StartDrag idx ->
            { model with DraggingPoint = Some (idx, model.Points.[idx]) }
        | StopDrag ->
            match model.DraggingPoint with
            | Some (idx, pt) ->
                { model with DraggingPoint = None; Points = IndexList.set idx pt model.Points }
            | None ->
                model
        | Delete p ->
            { model with Points = IndexList.remove p model.Points }
        | Clear ->
            { model with Points = IndexList.empty }
            
    let view (env : Env<Message>) (model : AdaptiveModel) =
        let a = FShade.Effect.ofFunction DefaultSurfaces.trafo
        printfn "%A" a
        
        body {
            OnBoot [
                "const l = document.getElementById('loader');"
                "if(l) l.remove();"
            ]
           
            renderControl {
                RenderControl.Samples 1
                
                model.DraggingPoint |> AVal.map (fun v ->
                    if Option.isSome v then Some (Style [Css.Cursor "crosshair"])
                    else None
                )
                
                let! size = RenderControl.ViewportSize
                Style [
                    Width "100%"
                    Height "100%"
                    Background "linear-gradient(to top, #051937, #314264, #5d6f95, #8ba1c9, #bbd5ff)"
                ]

                //RenderControl.Samples 1
                
                OrbitController.getAttributes (Env.map CameraMessage env)
                
                RenderControl.OnRendered (fun _ -> 
                    env.Emit [CameraMessage OrbitMessage.Rendered]
                )
                
                let proj =
                    size |> AVal.map (fun s ->
                        Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
                    )

                Sg.View(model.Camera.view |> AVal.map CameraView.viewTrafo)
                Sg.Proj(proj |> AVal.map Frustum.projTrafo)
                
                Sg.OnPointerLeave(fun _ -> 
                    env.Emit [Hover None]
                )
                
                Sg.OnDoubleTap(fun e ->
                    env.Emit [CameraMessage (OrbitMessage.SetTargetCenter(true, AnimationKind.Tanh, e.WorldPosition))]
                    false
                )
                
                Sg.OnTap(fun e -> 
                    if e.Button = Button.Right then
                        env.Emit [Click e.Position]
                        false
                    else
                        true
                )

                Sg.OnLongPress(fun e -> 
                    env.Emit [Click e.Position]
                    false
                )
                Sg.OnPointerMove(fun p ->
                    env.Emit [Hover (Some p.Position)]
                )

                // fori shading we simply use Aardvark.Rendering's default shaders doing transformations and 
                // applying a simple phong-illumination with a headlight.
                Sg.Shader {
                    DefaultSurfaces.trafo
                    DefaultSurfaces.simpleLighting
                    Shader.nothing
                }

                // render a centered floor-plane of size 20
                sg {
                    Sg.Scale 10.0
                    Primitives.Quad(Quad3d(V3d(-1, -1, 0), V3d(2, 0, 0), V3d(0.0, 2.0, 0.0)), C4b.SandyBrown)
                }
                
                // render a green teapot centered at the origin
                sg {
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        DefaultSurfaces.simpleLighting
                        Shader.withViewPos
                    }
                    Primitives.Teapot(C4b.Green)
                }
                // a yellow octahedron hovering 1 unit above the teapot
                sg {
                    Sg.Translate(0.0, 0.0, 1.0)
                    Primitives.Octahedron(C4b.Yellow)
                }

                sg {
                    Sg.Active(model.DraggingPoint |> AVal.map Option.isNone)
                    Sg.Active(model.Hover |> AVal.map Option.isSome)
                    let pos = model.Hover |> AVal.map (function Some p -> p | None -> V3d.Zero)
                    Sg.NoEvents
                    Primitives.Sphere(pos, 0.1, C4b.Red)
                }

                sg {
                    
                    sg {
                        Sg.NoEvents
                        let pos = model.DraggingPoint |> AVal.map (function Some (_,p) -> Some p | _ -> None)
                        Sg.Active(pos |> AVal.map Option.isSome)
                        let renderPos = pos |> AVal.map (Option.defaultValue V3d.Zero)
                        Primitives.Sphere(renderPos, 0.1, C4b.Yellow)
                    }
                    
                    
                    
                    model.Points |> AList.mapi (fun idx pos ->
                        sg {
                            Sg.Active (model.DraggingPoint |> AVal.map (function Some (i,_) -> i <> idx | None -> true))
                            let mutable down = false
                            Sg.Cursor (model.DraggingPoint |> AVal.map (function
                                | Some _ -> None
                                | None -> Some "pointer"
                            ))
                            Sg.OnTap (true, fun e ->
                                if e.Button = Button.Right then
                                    env.Emit [ Delete idx ]
                                    false
                                else
                                    true
                            )
                            
                            Sg.OnPointerDown((fun e ->
                                if e.Button = Button.Left then
                                    down <- true
                                    e.Context.SetPointerCapture(e.Target, e.PointerId)
                                    env.Emit [StartDrag idx]
                                    false
                                else
                                    true
                            ))
                            
                            Sg.OnPointerMove(fun e ->
                                if down then
                                    env.Emit [ Message.Update(idx, e.WorldPosition) ]
                                    false
                                else
                                    true
                            )
                            
                            Sg.OnPointerUp(fun e ->
                                if e.Button = Button.Left && down then
                                    down <- false
                                    env.Emit [ Message.Update(idx, e.WorldPosition); StopDrag ]
                                    e.Context.ReleasePointerCapture(e.Target, e.PointerId)
                                    false
                                else
                                    true
                                    
                            )
                            
                            
                            Primitives.Sphere(pos, 0.1, C4b.Green)
                        }
                    )
                }

                sg {    
                    Sg.NoEvents
                    Sg.Translate(1.0, 0.0, 0.0)
                    ASet.range (AVal.constant 0) model.Value
                    |> ASet.map (fun i ->
                        sg {
                            Sg.Translate(0.0, 0.0, float i * 0.4)
                            
                            Primitives.Box(Box3d.FromCenterAndSize(V3d.Zero, V3d.III * 0.1), C4b.Blue)
                        }
                    )
                }
                

            }

            div {
                Style [
                    Position "fixed"
                    Top "10px"
                    Left "10px"
                    
                ]
                h1 {
                    "Counter: "
                    model.Value |> AVal.map string
                }
                button {
                    "+"
                    Dom.OnClick(fun _ -> env.Emit [Increment])
                }
                button {
                    "-"
                    Dom.OnClick(fun _ -> env.Emit [Decrement])
                }
            
                button {
                    "Clear Points"
                    Dom.OnClick(fun _ -> env.Emit [Clear])
                }

                h2 {
                    model.Hover |> AVal.map (function
                        | Some p -> "Hover: " + p.ToString("0.00")
                        | None -> "Hover: none"
                    )
                }
         
                ul {
                    li { "right-click to place spheres in the scene" }
                    li { "left-click and drag to move spheres" }
                    li { "right-click on spheres to delete them" }
                    li { "double-click to focus camera" }
                }

            }

        }

    
    let app =
        {
            initial = initial
            update = update
            view = view
            unpersist = Unpersist.instance
        }
    