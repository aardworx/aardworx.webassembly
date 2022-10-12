namespace App

open FSharp.Data.Adaptive
open Aardvark.Base
open Adaptify


[<ModelType>]
type Model =
    {
        Value : int
        Hover : option<V3d>
        Points : HashSet<V3d>
    }

