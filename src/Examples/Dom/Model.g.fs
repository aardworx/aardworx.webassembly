//1a8f1adb-f703-b44f-5d1e-2d35acf9248a
//31430623-3f32-2fb9-8fdb-b4edee887e21
#nowarn "49" // upper case patterns
#nowarn "66" // upcast is unncecessary
#nowarn "1337" // internal types
#nowarn "1182" // value is unused
namespace rec App

open System
open FSharp.Data.Adaptive
open Adaptify
open App
[<System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
type AdaptiveModel(value : Model) =
    let _Camera_ = AdaptiveOrbitState(value.Camera)
    let _Value_ = FSharp.Data.Adaptive.cval(value.Value)
    let _Hover_ = FSharp.Data.Adaptive.cval(value.Hover)
    let _Points_ = FSharp.Data.Adaptive.clist(value.Points)
    let _DraggingPoint_ = FSharp.Data.Adaptive.cval(value.DraggingPoint)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : Model) = AdaptiveModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : Model) -> AdaptiveModel(value)) (fun (adaptive : AdaptiveModel) (value : Model) -> adaptive.Update(value))
    member __.Update(value : Model) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<Model>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _Camera_.Update(value.Camera)
            _Value_.Value <- value.Value
            _Hover_.Value <- value.Hover
            _Points_.Value <- value.Points
            _DraggingPoint_.Value <- value.DraggingPoint
    member __.Current = __adaptive
    member __.Camera = _Camera_
    member __.Value = _Value_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.int>
    member __.Hover = _Hover_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.option<Aardvark.Base.V3d>>
    member __.Points = _Points_ :> FSharp.Data.Adaptive.alist<Aardvark.Base.V3d>
    member __.DraggingPoint = _DraggingPoint_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.option<(FSharp.Data.Adaptive.Index * Aardvark.Base.V3d)>>

