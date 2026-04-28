namespace App

open Aardvark.Base
open Aardvark.Rendering
open FSharp.Data.Adaptive
open Aardvark.Dom
open Adaptify
open App

module Shader =
    open FShade

    /// Mode-B trigger: produces the `PickViewPosition` semantic so the pick
    /// chain composes the FinalB variant for any object using this effect.
    type Fragment = { [<Semantic("PickViewPosition")>] vp : V3f }

    let withViewPos (v : Effects.Vertex) =
        fragment {
            let vp4 = uniform.ProjTrafoInv * v.pos
            let vp  = vp4.XYZ / vp4.W
            return { vp = vp }
        }


type Message =
    | CameraMessage of OrbitMessage

module App =
    let initial =
        {
            Value = 0
            Hover = None
            Points = IndexList.empty
            DraggingPoint = None
            Camera = OrbitState.create V3d.Zero 1.0 0.3 8.0 Button.Left Button.Middle
        }

    let update (env : Env<Message>) (model : Model) (msg : Message) =
        match msg with
        | CameraMessage msg ->
            { model with Camera = OrbitController.update (Env.map CameraMessage env) model.Camera msg }

    // Plain cvals — UI-only state, not part of the persisted model.
    let private hovered = cval ""
    /// Arrow placement: Origin = world-space tap position, Direction =
    /// surface normal. NaN-origin = hidden.
    let private arrow = cval (Ray3d(V3d.NaN, V3d.OOI))

    let view (env : Env<Message>) (model : AdaptiveModel) =

        // Visual length of the arrow in world units. Teapots are under
        // `Sg.Scale 5.0` — make the arrow big enough to read at a glance.
        let arrowLen     = 1.8
        let arrowHead    = 0.5
        let arrowRadius  = 0.08
        let arrowColor   = AVal.constant C4b.Yellow

        let arrowVisible = arrow |> AVal.map (fun r -> not (Vec.AnyNaN r.Origin))

        let teapot (label : string) (baseColor : C4b) (snap : int) (x : float) (extraEffect : (Effects.Vertex -> 'a) option) =
            sg {
                Sg.PixelSnapRadius snap
                Sg.Translate(x, 0.0, 0.0)

                // Teapots that opt into mode B prepend `Shader.withViewPos`
                // to the user effect; the pick chain selector then picks
                // `pickFinalB` for them. (For mode A teapots we let the
                // outer `Sg.Shader` from the parent scene handle shading.)
                match extraEffect with
                | Some _ ->
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        DefaultSurfaces.simpleLighting
                        Shader.withViewPos
                    }
                | None -> ()

                Sg.OnPointerMove (fun _ ->
                    transact (fun () ->
                        hovered.Value <- sprintf "%s — snap radius %d" label snap)
                )
                Sg.OnPointerLeave (fun _ ->
                    transact (fun () ->
                        if hovered.Value.StartsWith label then
                            hovered.Value <- "")
                )
                Sg.OnTap (fun e ->
                    let n = if Vec.AllTiny e.Normal then V3d.OOI else Vec.normalize e.Normal
                    transact (fun () -> arrow.Value <- Ray3d(e.WorldPosition, n))
                    false
                )
                Primitives.Teapot baseColor
            }

        body {
            OnBoot [
                "const l = document.getElementById('loader');"
                "if(l) l.remove();"
            ]

            renderControl {
                RenderControl.Samples 4

                let! size = RenderControl.ViewportSize
                Style [
                    Width "100%"
                    Height "100%"
                    Background "linear-gradient(to top, #051937, #314264, #5d6f95, #8ba1c9, #bbd5ff)"
                ]

                OrbitController.getAttributes (Env.map CameraMessage env)

                RenderControl.OnRendered (fun _ ->
                    env.Emit [CameraMessage OrbitMessage.Rendered]
                )

                let proj =
                    size |> AVal.map (fun s ->
                        Frustum.perspective 60.0 0.1 100.0 (float s.X / float s.Y)
                    )

                Sg.View(model.Camera.view |> AVal.map CameraView.viewTrafo)
                Sg.Proj(proj |> AVal.map Frustum.projTrafo)

                // Mode A default shading for everything that doesn't override.
                Sg.Shader { DefaultSurfaces.trafo; DefaultSurfaces.simpleLighting }

                sg {
                    Sg.Scale 5.0
                    teapot "Red"   C4b.Red   0  -0.4 None
                    teapot "Green" C4b.Green 8   0.0 None
                    // Blue uses mode B (PickViewPosition) — exercises the
                    // FinalB pick path on the WebGL backend.
                    teapot "Blue"  C4b.Blue  16  0.4 (Some Shader.withViewPos)
                }

                // Arrow marker — appears at the last tapped position,
                // oriented along that surface's normal.
                sg {
                    Sg.NoEvents
                    Sg.Active arrowVisible
                    Primitives.Cylinder(
                        arrow |> AVal.map (fun r ->
                            let p0 = r.Origin
                            let p1 = r.GetPointOnRay (arrowLen - arrowHead)
                            Cylinder3d(p0, p1, arrowRadius)),
                        arrowColor)
                    Primitives.Cone(
                        arrow |> AVal.map (fun r ->
                            let baseP = r.GetPointOnRay (arrowLen - arrowHead)
                            // axis points from base toward tip; Cone3d takes
                            // (apex, axis-vector, half-angle).
                            let apex  = r.GetPointOnRay arrowLen
                            Cone3d(apex, baseP - apex, Constant.PiQuarter * 0.5)),
                        arrowColor)
                }
            }

            div {
                Style [
                    Position "fixed"; Top "10px"; Left "10px"
                    Color "white"; FontFamily "monospace"
                    Padding "10px"; Background "rgba(0,0,0,0.5)"; BorderRadius "8px"
                ]
                h2 {
                    "Pixel-snap demo (WebGL)"
                }
                div {
                    "Hover near each teapot — only inside the declared radius does the cursor snap to it."
                }
                div {
                    "Tap a teapot to plant the yellow arrow along its surface normal. (Blue uses mode B picking.)"
                }
                h3 {
                    hovered |> AVal.map (fun s -> if s = "" then "(no snap)" else s)
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
