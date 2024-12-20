//8f4f9a98-db85-c6c0-b680-d0f649e16b05
//a79950b3-25a3-d3a7-6f9e-aae1424035ba
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
type AdaptiveOrbitState(value : OrbitState) =
    let mutable _super_ = FSharp.Data.Adaptive.cval(value.super)
    let mutable _animationRunning_ = FSharp.Data.Adaptive.cval(value.animationRunning)
    let _sky_ = FSharp.Data.Adaptive.cval(value.sky)
    let _center_ = FSharp.Data.Adaptive.cval(value.center)
    let _phi_ = FSharp.Data.Adaptive.cval(value.phi)
    let _theta_ = FSharp.Data.Adaptive.cval(value.theta)
    let _radius_ = FSharp.Data.Adaptive.cval(value.radius)
    let _shift_ = FSharp.Data.Adaptive.cval(value.shift)
    let _userModifiedAngles_ = FSharp.Data.Adaptive.cval(value.userModifiedAngles)
    let _userModifiedCenter_ = FSharp.Data.Adaptive.cval(value.userModifiedCenter)
    let _userModifiedRadius_ = FSharp.Data.Adaptive.cval(value.userModifiedRadius)
    let _centerAnimation_ = FSharp.Data.Adaptive.cval(value.centerAnimation)
    let _locationAnimation_ = FSharp.Data.Adaptive.cval(value.locationAnimation)
    let _panAnimation_ = FSharp.Data.Adaptive.cval(value.panAnimation)
    let _targetPhi_ = FSharp.Data.Adaptive.cval(value.targetPhi)
    let _targetTheta_ = FSharp.Data.Adaptive.cval(value.targetTheta)
    let _targetRadius_ = FSharp.Data.Adaptive.cval(value.targetRadius)
    let _dragging_ = FSharp.Data.Adaptive.cval(value.dragging)
    let _dragStarts_ = FSharp.Data.Adaptive.cval(value.dragStarts)
    let _view_ = FSharp.Data.Adaptive.cval(value.view)
    let _radiusRange_ = FSharp.Data.Adaptive.cval(value.radiusRange)
    let _thetaRange_ = FSharp.Data.Adaptive.cval(value.thetaRange)
    let _moveSensitivity_ = FSharp.Data.Adaptive.cval(value.moveSensitivity)
    let _zoomSensitivity_ = FSharp.Data.Adaptive.cval(value.zoomSensitivity)
    let _speed_ = FSharp.Data.Adaptive.cval(value.speed)
    let _pick_ = FSharp.Data.Adaptive.cval(value.pick)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : OrbitState) = AdaptiveOrbitState(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : OrbitState) -> AdaptiveOrbitState(value)) (fun (adaptive : AdaptiveOrbitState) (value : OrbitState) -> adaptive.Update(value))
    member __.Update(value : OrbitState) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<OrbitState>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _super_.Value <- value.super
            _animationRunning_.Value <- value.animationRunning
            _sky_.Value <- value.sky
            _center_.Value <- value.center
            _phi_.Value <- value.phi
            _theta_.Value <- value.theta
            _radius_.Value <- value.radius
            _shift_.Value <- value.shift
            _userModifiedAngles_.Value <- value.userModifiedAngles
            _userModifiedCenter_.Value <- value.userModifiedCenter
            _userModifiedRadius_.Value <- value.userModifiedRadius
            _centerAnimation_.Value <- value.centerAnimation
            _locationAnimation_.Value <- value.locationAnimation
            _panAnimation_.Value <- value.panAnimation
            _targetPhi_.Value <- value.targetPhi
            _targetTheta_.Value <- value.targetTheta
            _targetRadius_.Value <- value.targetRadius
            _dragging_.Value <- value.dragging
            _dragStarts_.Value <- value.dragStarts
            _view_.Value <- value.view
            _radiusRange_.Value <- value.radiusRange
            _thetaRange_.Value <- value.thetaRange
            _moveSensitivity_.Value <- value.moveSensitivity
            _zoomSensitivity_.Value <- value.zoomSensitivity
            _speed_.Value <- value.speed
            _pick_.Value <- value.pick
    member __.Current = __adaptive
    member __.super = _super_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.int>
    member __.animationRunning = _animationRunning_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.sky = _sky_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V3d>
    member __.center = _center_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V3d>
    member __.phi = _phi_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.theta = _theta_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.radius = _radius_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.shift = _shift_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V2d>
    member __.userModifiedAngles = _userModifiedAngles_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.userModifiedCenter = _userModifiedCenter_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.userModifiedRadius = _userModifiedRadius_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.centerAnimation = _centerAnimation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Animation<Aardvark.Base.V3d>>>
    member __.locationAnimation = _locationAnimation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Animation<Aardvark.Base.V3d>>>
    member __.panAnimation = _panAnimation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Animation<Aardvark.Base.V2d>>>
    member __.targetPhi = _targetPhi_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.targetTheta = _targetTheta_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.targetRadius = _targetRadius_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.dragging = _dragging_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.lockedToScene = __value.lockedToScene
    member __.isOrtho = __value.isOrtho
    member __.rotateButton = __value.rotateButton
    member __.panButton = __value.panButton
    member __.dragStarts = _dragStarts_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.MapExt<Microsoft.FSharp.Core.int, (Aardvark.Base.V2i * Aardvark.Dom.Button)>>
    member __.lastRender = __value.lastRender
    member __.view = _view_ :> FSharp.Data.Adaptive.aval<Aardvark.Rendering.CameraView>
    member __.radiusRange = _radiusRange_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V2d>
    member __.thetaRange = _thetaRange_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V2d>
    member __.moveSensitivity = _moveSensitivity_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.zoomSensitivity = _zoomSensitivity_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.speed = _speed_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.pick = _pick_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V2d -> Microsoft.FSharp.Core.option<Aardvark.Base.V3d>>

