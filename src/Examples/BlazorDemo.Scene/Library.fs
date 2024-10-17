namespace BlazorDemo

open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Aardvark.Dom
open Aardvark.Application

module Scene =
    let get (ctrl : IRenderControl) =
        let view =
            CameraView.lookAt (V3d(4,3,2)) V3d.Zero V3d.OOI 
            |> DefaultCameraController.control ctrl.Mouse ctrl.Keyboard ctrl.Time
            |> AVal.map CameraView.viewTrafo
        
        let proj =
            ctrl.Sizes |> AVal.map (fun s ->
                Frustum.perspective 90.0 0.1 100.0 (float s.X / float s.Y)
                |> Frustum.projTrafo
            )
        
        let scene = 
            sg {
                Sg.View view
                Sg.Proj proj
                Sg.Shader {
                    DefaultSurfaces.trafo
                    DefaultSurfaces.simpleLighting
                }
                
                Primitives.Box(V3d.III, C4b.Green)
            }
            
        struct(view, proj, scene)