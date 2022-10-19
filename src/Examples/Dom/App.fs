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

type Message =
    | Increment
    | Decrement
    | Hover of option<V3d>
    | Click of V3d
    | Delete of V3d
    | Clear

module App =
    let initial = 
        { 
            Value = 3
            Hover = None
            Points = HashSet.empty
        }

    let update (_env : Env<Message>) (model : Model) (msg : Message) =
        match msg with
        | Increment ->
            { model with Value = model.Value + 1 }
        | Decrement ->
            { model with Value = model.Value - 1 }
        | Hover p ->
            { model with Hover = p }
        | Click p ->
            { model with Points = HashSet.add p model.Points }
        | Delete p ->
            { model with Points = HashSet.remove p model.Points }
        | Clear ->
            { model with Points = HashSet.empty }
            
    let view (env : Env<Message>) (model : AdaptiveModel) =
    
        let a = FShade.Effect.ofFunction DefaultSurfaces.trafo
        printfn "%A" a
        body {

            OnBoot [
                "const l = document.getElementById('loader');"
                "if(l) l.remove();"
            ]
            
            renderControl {
                let! size = RenderControl.ViewportSize
                Style [
                    Width "100%"
                    Height "100%"
                    Background "linear-gradient(to top, #051937, #314264, #5d6f95, #8ba1c9, #bbd5ff)"
                ]

                RenderControl.FXAA
                
                let proj =
                    size |> AVal.map (fun s ->
                        Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
                    )

                Sg.View(CameraView.lookAt (V3d(3,4,3)) V3d.Zero V3d.OOI |> CameraView.viewTrafo)
                Sg.Proj(AVal.map Frustum.projTrafo proj)
                
                Sg.OnPointerLeave(fun _ -> 
                    env.Emit [Hover None]
                )
                
                Sg.OnTap(fun e -> 
                    env.Emit [Click e.Position]
                )

                Sg.OnPointerMove(fun p ->
                    env.Emit [Hover (Some p.Position)]
                    false
                )

                // fori shading we simply use Aardvark.Rendering's default shaders doing transformations and 
                // applying a simple phong-illumination with a headlight.
                Sg.Shader {
                    DefaultSurfaces.trafo
                    DefaultSurfaces.constantColor C4f.Red
                    DefaultSurfaces.simpleLighting
                    Shader.nothing
                }

                // render a centered floor-plane of size 20
                sg {
                    Sg.Scale 10.0
                    Primitives.Quad(Quad3d(V3d(-1, -1, 0), V3d(2, 0, 0), V3d(0.0, 2.0, 0.0)), C4b.SandyBrown)
                }
                
                // render a green teapot centered at the origin
                Primitives.Teapot(C4b.Green)
                
                // a yellow octahedron hovering 1 unit above the teapot
                sg {
                    Sg.Translate(0.0, 0.0, 1.0)
                    Primitives.Octahedron(C4b.Yellow)
                }

                sg {
                    Sg.Active(model.Hover |> AVal.map Option.isSome)
                    let pos = model.Hover |> AVal.map (function Some p -> p | None -> V3d.Zero)
                    Sg.NoEvents
                    Primitives.Sphere(pos, 0.1, C4b.Red)
                }

                sg {
                    model.Points |> ASet.map (fun pos ->
                        sg {
                            Sg.Cursor "no-drop"
                            Sg.OnPointerMove(fun _ ->
                                env.Emit [Hover None]
                                false
                            )
                            Sg.OnTap (fun e ->
                                env.Emit [ Delete pos ]
                                false
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
         
                div {
                    "click to place spheres in the scene"
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
    