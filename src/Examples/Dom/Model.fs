namespace App

open FSharp.Data.Adaptive
open Aardvark.Base
open Adaptify


[<ModelType>]
type Model =
    {
        Camera : OrbitState
        Value : int
        Hover : option<V3d>
        Points : IndexList<V3d>
        DraggingPoint : option<Index * V3d>
    }

