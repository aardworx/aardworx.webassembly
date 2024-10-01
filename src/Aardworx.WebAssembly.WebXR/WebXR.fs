namespace Aardworx.WebAssembly.WebXR

open System
open Aardvark.Base
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Microsoft.FSharp.NativeInterop

#nowarn "9"

[<AutoOpen>]
module private JsonHelpers =
    open System.Text.Json
    
    let (|Null|String|Array|Object|Boolean|Number|) (json : JsonElement) =
        match json.ValueKind with
        | JsonValueKind.String -> String (json.GetString())
        | JsonValueKind.False -> Boolean false
        | JsonValueKind.True -> Boolean true
        | JsonValueKind.Object -> Object json
        | JsonValueKind.Array ->
            let items = Array.init (json.GetArrayLength()) (fun i -> json[i])
            Array items
        | JsonValueKind.Number ->
            Number (json.GetDecimal())
        | JsonValueKind.Undefined | JsonValueKind.Null | _ ->
            Null
    
    let (|Int|Float|) (d : decimal) =
        let v = int64 d
        if decimal v = d then Int (int v)
        else Float (float d)
      
      
    let json (callback : Utf8JsonWriter -> unit) =
        use ms = new System.IO.MemoryStream()
        use w = new Utf8JsonWriter(ms)
        callback w
        w.Flush()
        System.Text.Encoding.UTF8.GetString(ms.ToArray())
        
        
    let pickleEuclidean (e : Euclidean3d) =
        json (fun w ->
            w.WriteStartObject()
            
            w.WritePropertyName "position"
            w.WriteStartObject()
            w.WriteNumber("x", e.Trans.X)
            w.WriteNumber("y", e.Trans.Y)
            w.WriteNumber("z", e.Trans.Z)
            w.WriteEndObject()
            
            w.WritePropertyName "orientation"
            w.WriteStartObject()
            w.WriteNumber("x", e.Rot.X)
            w.WriteNumber("y", e.Rot.Y)
            w.WriteNumber("z", e.Rot.Z)
            w.WriteNumber("w", e.Rot.W)
            w.WriteEndObject()
            
            w.WriteEndObject()
        )
        
    let unpickleEuclidean' (r : JsonElement) =
        let pos = r.GetProperty "position"
        let rot = r.GetProperty "orientation"
        
        let x = pos.GetProperty("x").GetDouble()
        let y = pos.GetProperty("y").GetDouble()
        let z = pos.GetProperty("z").GetDouble()
        let pos = V3d(x, y, z)
        
        
        let x = rot.GetProperty("x").GetDouble()
        let y = rot.GetProperty("y").GetDouble()
        let z = rot.GetProperty("z").GetDouble()
        let w = rot.GetProperty("w").GetDouble()
        let rot = Rot3d(w, x, y, z)
        Euclidean3d(rot, pos)
         
    let unpickleEuclidean (json : string) =
        let json = JsonDocument.Parse json
        unpickleEuclidean' json.RootElement
       
    let m44 (o : int) (v : float[]) =
        M44d(
            v.[o + 0], v.[o + 4], v.[o + 8],  v.[o + 12],
            v.[o + 1], v.[o + 5], v.[o + 9],  v.[o + 13],
            v.[o + 2], v.[o + 6], v.[o + 10], v.[o + 14],
            v.[o + 3], v.[o + 7], v.[o + 11], v.[o + 15]
        )
        
    let ptr (n : nativeptr<'a>) =
        NativePtr.toNativeInt n |> int
        
type XRWebGLLayerOptions =
    {
        Alpha : bool
        Antialias : bool
        Depth : bool
        Stencil : bool  
        FramebufferScaleFactor : float
        IgnoreDepthValues : bool
    }
    
    static member Default =
        {
            Alpha = true
            Antialias = true
            Depth = true
            Stencil = false
            FramebufferScaleFactor = 1.0
            IgnoreDepthValues = false
        }
 

type XRMode =
    | ImmersiveVR
    | ImmersiveAR
    | Inline
    
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module WebXR =
    open Microsoft.JSInterop
    type private Marker = Marker
    
    let private js =
        let name = typeof<Marker>.Assembly.GetManifestResourceNames() |> Array.find (fun n -> n.EndsWith "WebXR.js")
        use stream = typeof<Marker>.Assembly.GetManifestResourceStream(name)
        use reader = new System.IO.StreamReader(stream)
        reader.ReadToEnd()
        
    let mutable private installed = false
    let init() =
        if not installed then
            printfn "install WebXR"
            JsObj.InstallScript js
            installed <- true
        
    let isSupported (mode : string) =
        init()
        JsObj.Runtime.InvokeAsync<bool>("window.xr.isSupported", [| mode :> obj |])
    
    let requestSession (mode : string) (options : obj) =
        init()
        JsObj.Runtime.InvokeAsync<int>("window.xr.requestSession", [| mode :> obj; options |])
    
    let session_getDepthDataFormat (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getDepthDataFormat", [| session :> obj |])
    
    let session_getDepthUsage (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getDepthUsage", [| session :> obj |])
    
    let session_getDomOverlayState (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getDomOverlayState", [| session :> obj |])
    
    let session_getEnvironmentBlendMode (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getEnvironmentBlendMode", [| session :> obj |])
    
    let session_getInputSources (session : int) (handles : int[]) =
        init()
        use pHandles = fixed handles
        let cnt = JsObj.Runtime.Invoke<int>("window.xr.session_getInputSources", [| session :> obj; ptr pHandles; handles.Length |])
        cnt
        
    let session_getInteractionMode (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getInteractionMode", [| session :> obj |])
    
    let session_getPreferredReflectionFormat (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getPreferredReflectionFormat", [| session :> obj |])
    
    let session_getRenderState (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getRenderState", [| session :> obj |])
    
    let session_updateRenderState (session : int) (renderState : string) =
        init()
        JsObj.Runtime.InvokeVoid("window.xr.session_updateRenderState", [| session :> obj; renderState :> obj |])
    
    let session_getVisibilityState (session : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.session_getVisibilityState", [| session :> obj |])
    
    let session_requestReferenceSpace (session : int) (space : string) =
        init()
        JsObj.Runtime.InvokeAsync<int>("window.xr.session_requestReferenceSpace", [|session :> obj; space :> obj|])
    
    let private session_callbacks = Dict<int * string, ref<list<unit -> unit>>>()
    
    let session_callback (sessionId : int) (typ : string) =
        match session_callbacks.TryGetValue ((sessionId, typ)) with
        | (true, cbs) ->
            for cb in cbs.Value do
                try cb()
                with _ -> ()
        | _ ->
            ()
        
    let session_addEventListener (session : int) (typ : string) (callback : unit -> unit) =
        init()
        let key = (session, typ)
        let r = 
            match session_callbacks.TryGetValue key with
            | (true, r) ->
                r.Value <- callback :: r.Value
                r
            | _ ->
                let r = ref [callback]
                session_callbacks.[key] <- r
                JsObj.Runtime.InvokeVoid("window.xr.session_addEventListener", [| session :> obj; typ :> obj |])
                r
                
        { new IDisposable with
            member x.Dispose() =
                let newCbs = r.Value |> List.filter (fun cb -> not (System.Object.ReferenceEquals(cb, callback)))
                r.Value <- newCbs
                // TODO remove event listener if no more callbacks
        }
    
    let private session_animationFrameCallbacks = Dict<int, ref<list<double -> int -> unit>>>()
    
    let session_animationFrameCallback (session : int) (time : double) (frameHandle : int) =
        match session_animationFrameCallbacks.TryRemove session with
        | (true, cbs) ->
            for c in cbs.Value do
                try c time frameHandle
                with _ -> ()
        | _ ->
            ()
    
    let session_requestAnimationFrame (session : int) (callback : double -> int -> unit) =
        init()
        let l = session_animationFrameCallbacks.GetOrCreate(session, fun _ -> ref [])
        l.Value <- callback :: l.Value
        JsObj.Runtime.InvokeVoid("window.xr.session_requestAnimationFrame", [| session :> obj |])
    
    let session_createXRWebGLLayer (session : int) (context : WebGLContext) (options : string) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.session_createXRWebGLLayer", [|session :> obj; context.Handle :> obj; options :> obj|])
    
    let session_createXRWebGLBinding (session : int) (context : WebGLContext) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.session_createXRWebGLBinding", [|session :> obj; context.Handle :> obj|])
        
        
    
    let layer_getFramebuffer (layer : int) =
        init()
        JsObj.Runtime.Invoke<uint32>("window.xr.layer_getFramebuffer", [| layer :> obj |])
    
    let layer_getFramebufferWidth (layer : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.layer_getFramebufferWidth", [| layer :> obj |])
    
    let layer_getFramebufferHeight (layer : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.layer_getFramebufferHeight", [| layer :> obj |])
    
    let layer_getIgnoreDepthValues (layer : int) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.layer_getIgnoreDepthValues", [| layer :> obj |])
    
    let layer_getFixedFoveation (layer : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.layer_getFixedFoveation", [| layer :> obj |])
    
    let layer_getAntialias (layer : int) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.layer_getAntialias", [| layer :> obj |])
    
    let layer_getViewport (layer : int) (view : int) =
        init()
        let arr = Array.zeroCreate<V2d> 2
        use pArr = fixed arr
        JsObj.Runtime.InvokeVoid("window.xr.layer_getViewport", [| layer :> obj; view :> obj; ptr pArr |])
        arr.[0], arr.[1]

    let frame_getViewerPose (frame : int) (refSpace : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.frame_getViewerPose", [| frame :> obj; refSpace :> obj |])
    
    let frame_getTrackedAnchors (frame : int) =
        init()
        let json = JsObj.Runtime.Invoke<string>("window.xr.frame_getTrackedAnchors", [| frame :> obj |]) |> System.Text.Json.JsonDocument.Parse
        match json.RootElement with
        | Array values -> values |> Array.choose (function Number (Int v) -> Some v | _ -> None)
        | _ -> [||]
    
    let frame_createAnchor (frame : int) (pose : Euclidean3d) (space : int) =
        init()
        JsObj.Runtime.InvokeAsync<int>("window.xr.frame_createAnchor", [| frame :> obj; pickleEuclidean pose :> obj; space :> obj |])
    
    let frame_fillJointRadii (frame : int) (jointSpaces : int[]) (radii : float32[]) =
        init()
        use pJointSpaces = fixed jointSpaces
        use pRadii = fixed radii
        JsObj.Runtime.Invoke<bool>("window.xr.frame_getJointRadii", [| frame :> obj; ptr pJointSpaces :> obj; jointSpaces.Length :> obj; ptr pRadii :> obj |])
        
    let frame_fillPoses (frame : int) (jointSpaces : int[]) (baseSpace : int) (poses : M44f[]) =
        init()
        use pJointSpaces = fixed jointSpaces
        use pPoses = fixed poses
        JsObj.Runtime.Invoke<bool>("window.xr.frame_fillPoses", [| frame :> obj; ptr pJointSpaces :> obj; jointSpaces.Length :> obj; baseSpace :> obj; ptr pPoses |]) 

    let frame_getDepthInformation (frame : int) (view : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.frame_getDepthInformation", [| frame :> obj; view :> obj |])
    
    let frame_getHitTestResults (frame : int) (hitTestSource : int) =
        init()
        let json = JsObj.Runtime.Invoke<string>("window.xr.frame_getHitTestResults", [| frame :> obj; hitTestSource :> obj |]) |> System.Text.Json.JsonDocument.Parse
        match json.RootElement with
        | Array values -> values |> Array.choose (function Number (Int v) -> Some v | _ -> None)
        | _ -> [||]
    
    let frame_getJointPose (frame : int) (jointSpace : int) (baseSpace : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.frame_getJointPose", [| frame :> obj; jointSpace :> obj; baseSpace :> obj |])

    let frame_getPose (frame : int) (jointSpace : int) (baseSpace : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.frame_getPose", [| frame :> obj; jointSpace :> obj; baseSpace :> obj |])

    let frame_getMeshSetCount (frame : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.frame_getMeshSetCount", [| frame :> obj |])
    
    let view_getEye (view : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.view_getEye", [| view :> obj |])
    
    let view_getIsFirstPersonObserver (view : int) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.view_getIsFirstPersonObserver", [| view :> obj |])
    
    let view_getProjectionMatrix (view : int) (pMat : nativeptr<M44f>) =
        init()
        try
            JsObj.Runtime.InvokeVoid("window.xr.view_getProjectionMatrix", [| view :> obj; ptr pMat :> obj |])
        with e ->
            printfn "%A" e
    let view_getRecommendedViewportScale (view : int) =
        init()
        JsObj.Runtime.Invoke<float>("window.xr.view_getRecommendedViewportScale", [| view :> obj |])
    
    let view_getTransform (view : int) =
        init()
        use pp = fixed [| Unchecked.defaultof<Euclidean3d>  |]
        JsObj.Runtime.InvokeVoid("window.xr.view_getTransform", [| view :> obj; ptr pp |])
        NativePtr.read pp
    
    let view_requestViewportScale (view : int) (scale : float) =
        init()
        JsObj.Runtime.InvokeVoid("window.xr.view_requestViewportScale", [| view :> obj; scale :> obj |])
        
    let depthInfo_isCpu (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.depthInfo_isCpu", [| depthInfo :> obj |])
        
    let depthInfo_isGpu (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.depthInfo_isGpu", [| depthInfo :> obj |])
        
    let depthInfo_getHeight (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.depthInfo_getHeight", [| depthInfo :> obj |])
        
    let depthInfo_getWidth (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.depthInfo_getWidth", [| depthInfo :> obj |])
        
    let depthInfo_getNormDepthBufferFromNormView (depthInfo : int) =
        init()
        use pp = fixed [| Unchecked.defaultof<Euclidean3d> |]
        JsObj.Runtime.InvokeVoid("window.xr.depthInfo_getNormDepthBufferFromNormView", [| depthInfo :> obj; ptr pp |])
        NativePtr.read pp
        
    let depthInfo_getRawValueToMeters (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.depthInfo_getRawValueToMeters", [| depthInfo :> obj |])
        
    let depthInfo_getData<'a when 'a : unmanaged> (depthInfo : int) (pBuffer : nativeptr<'a>) =
        init()
        JsObj.Runtime.Invoke<bool>("window.xr.depthInfo_getData", [| depthInfo :> obj; ptr pBuffer :> obj |])
        
    let depthInfo_getDepthInMeters (depthInfo : int) (x : float) (y : float) =
        init()
        JsObj.Runtime.Invoke<float>("window.xr.depthInfo_getDepthInMeters", [| depthInfo :> obj; x :> obj; y :> obj |])
        
    let depthInfo_getTexture (depthInfo : int) =
        init()
        JsObj.Runtime.Invoke<uint32>("window.xr.depthInfo_getTexture", [| depthInfo :> obj |])
        
    let binding_getSubImage (depthInfo : int) (layerId : int) (frameId : int) (eye : string) =
        init()
        JsObj.Runtime.Invoke<uint32>("window.xr.binding_getSubImage", [| depthInfo :> obj; layerId; frameId; eye |])
        
        
    let binding_getNativeProjectionScaleFactor (binding : int) =
        init()
        JsObj.Runtime.Invoke<double>("window.xr.binding_getNativeProjectionScaleFactor", [| binding :> obj |])
         
    let binding_getDepthInformation (binding : int) (view : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.binding_getDepthInformation", [| binding :> obj; view :> obj |])
        
    let inputSource_getGamepad (inputSource : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.inputSource_getGamepad", [| inputSource :> obj |])
        
    let gamepad_getButtons (gamepad : int) (states : 'a[]) =
        init()
        use pStates = fixed states
        JsObj.Runtime.Invoke<int>("window.xr.gamepad_getButtons", [| gamepad :> obj; ptr pStates :> obj; states.Length |])
        
    let gamepad_getId (gamepad : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.gamepad_getId", [| gamepad :> obj |])
        
    let gamepad_getIndex (gamepad : int) =
        init()
        JsObj.Runtime.Invoke<int>("window.xr.gamepad_getIndex", [| gamepad :> obj |])
        
    let gamepad_getHand (gamepad : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.gamepad_getHand", [| gamepad :> obj |])
        
    let gamepad_getMapping (gamepad : int) =
        init()
        JsObj.Runtime.Invoke<string>("window.xr.gamepad_getMapping", [| gamepad :> obj |])
        
    let viewerPose_getViews (pose : int) (views : int[]) =
        init()
        use pViews = fixed views
        let cnt = JsObj.Runtime.Invoke<int>("window.xr.viewerPose_getViews", [| pose :> obj; ptr pViews; views.Length |])
        cnt
        
    let makeXRCompatible (context : WebGLContext) =
        init()
        JsObj.Runtime.InvokeAsync<bool>("window.xr.makeXRCompatible", [| context.Handle :> obj |])
    
            
    // let private callbacks = Dict<int, ref<list<string -> unit>>>()
    // let frameCallback (session : int) (content : string) =
    //     match callbacks.TryRemove session with
    //     | (true, cbs) ->
    //         for c in cbs.Value do c content
    //     | _ ->
    //         ()
    // let private callbacks2 = Dict<int, ref<list<double -> int -> unit>>>()
    // let frameCallback2 (session : int) (time : double) (frameHandle : int) =
    //     match callbacks2.TryRemove session with
    //     | (true, cbs) ->
    //         for c in cbs.Value do c time frameHandle
    //     | _ ->
    //         ()
    //
    // let requestAnimationFrame (session : int) (layer : int) (refSpace : int) (context : WebGLContext) (callback : double -> int -> unit) =
    //     init()
    //     let l = callbacks2.GetOrCreate(session, fun _ -> ref [])
    //     l.Value <- callback :: l.Value
    //     JsObj.Runtime.InvokeVoid("window.xr.requestAnimationFrame", [| session :> obj; layer :> obj; refSpace :> obj; context.Handle :> obj |])
    //     
    //
    //
    // let requestReferenceSpace (session : int) (space : string) =
    //     init()
    //     JsObj.Runtime.InvokeAsync<int>("window.xr.requestReferenceSpace", [| session :> obj; space :> obj |])
    //

    // let createXRWebGLLayer (session : int) (context : WebGLContext) (options : XRWebGLLayerOptions) =
    //     init()
    //     
    //     let options =
    //         JsObj.New [
    //             "alpha", options.Alpha
    //             "antialias", options.Antialias
    //             "depth", options.Depth
    //             "stencil", options.Stencil
    //             "framebufferScaleFactor", options.FramebufferScaleFactor
    //             "ignoreDepthValues", options.IgnoreDepthValues
    //         ]
    //     
    //     
    //     JsObj.Runtime.Invoke<int>("window.xr.createXRWebGLLayer", [| session :> obj; context.Handle :> obj; options.Reference :> obj |])
    //
    // let getLayerFramebuffer (layer : int) =
    //     JsObj.Runtime.Invoke<uint32>("window.xr.getLayerFramebuffer", [| layer :> obj |])
    //
    // let getLayerFramebufferSize (layer : int) =
    //     let w = JsObj.Runtime.Invoke<double>("window.xr.getLayerFramebufferWidth", [| layer :> obj |]) |> int
    //     let h = JsObj.Runtime.Invoke<double>("window.xr.getLayerFramebufferHeight", [| layer :> obj |]) |> int
    //     V2i(w, h)
    //     
    // let getLayerIgnoreDepthValues (layer : int) =
    //     JsObj.Runtime.Invoke<bool>("window.xr.getLayerIgnoreDepthValues", [| layer :> obj |])
    //     
    // let getLayerFixedFoveation (layer : int) =
    //     JsObj.Runtime.Invoke<double>("window.xr.getLayerFixedFoveation", [| layer :> obj |])
    //     
    // let getLayerAntialias (layer : int) =
    //     JsObj.Runtime.Invoke<bool>("window.xr.getLayerAntialias", [| layer :> obj |])
    //     
    // let getLayerViewport (layer : int) (view : int) =
    //     let json = JsObj.Runtime.Invoke<string>("window.xr.getLayerViewport", [| layer :> obj; view :> obj |])
    //     let doc = System.Text.Json.JsonDocument.Parse json
    //     
    //     let x = doc.RootElement.GetProperty("x").GetDouble()
    //     let y = doc.RootElement.GetProperty("y").GetDouble()
    //     let w = doc.RootElement.GetProperty("width").GetDouble()
    //     let h = doc.RootElement.GetProperty("height").GetDouble()
    //     Box2d(V2d(x,y), V2d(x+w-1.0,y+h-1.0))
    //     
    //     
    // let updateRenderState (session : int) (state : XRRenderState) =
    //     init()
    //     let options =
    //         JsObj.New [
    //             match state.BaseLayer with
    //             | Some l -> "baseLayer", state.BaseLayer :> obj
    //             | None -> ()
    //             
    //             match state.DepthFar with
    //             | Some d -> "depthFar", state.DepthFar :> obj
    //             | None -> ()
    //             
    //             match state.DepthNear with
    //             | Some d -> "depthNear", state.DepthNear :> obj
    //             | None -> ()
    //             
    //             match state.InlineVerticalFieldOfView with
    //             | Some f -> "inlineVerticalFieldOfView", state.InlineVerticalFieldOfView :> obj
    //             | None -> ()
    //             
    //             match state.Layers with
    //             | Some l -> "layers", state.Layers :> obj
    //             | None -> ()
    //         ]
    //     
    //     JsObj.Runtime.InvokeVoid("window.xr.updateRenderState", [| session :> obj; options.Reference :> obj |])
    //
    // let updateRenderStateNoState (session : int) =
    //     init()
    //     JsObj.Runtime.InvokeVoid("window.xr.updateRenderStateNoState", [| session :> obj |])
    //

// usagePreference: ["gpu-optimized", "cpu-optimized"],
// dataFormatPreference: ["luminance-alpha", "float32"]
 
type XRPose =
    {
        Radius : option<float>
        EmulatedPosition : bool
        AngularVelocity : V3d
        LinearVelocity : V3d
        Transform : Euclidean3d
    }
    
    static member TryParse (json : System.Text.Json.JsonElement) =
        
        let emulatedPosition =
            match json.TryGetProperty "emulatedPosition" with
            | (true, (Boolean v)) -> v
            | _ -> true
        
        let angularVelocity = 
            match json.TryGetProperty "angularVelocity" with
            | (true, (Array vs)) ->
                vs |> Array.map (fun v -> v.GetDouble()) |> V3d
            | _ ->
                V3d.Zero
              
        let linearVelocity = 
            match json.TryGetProperty "linearVelocity" with
            | (true, (Array vs)) ->
                vs |> Array.map (fun v -> v.GetDouble()) |> V3d
            | _ ->
                V3d.Zero  
        
        let radius =
            match json.TryGetProperty "radius" with
            | (true, (Number r)) -> Some (float r)
            | _ -> None
 
        match json.TryGetProperty "transform" with
        | (true, (Object r)) ->
            let transform =
                unpickleEuclidean' r
     
            Some {
                Radius = radius
                EmulatedPosition = emulatedPosition
                AngularVelocity = angularVelocity
                LinearVelocity = linearVelocity
                Transform = transform
            }
        | _ ->
            None
 
    static member TryParse (str : string) =
        let json = System.Text.Json.JsonDocument.Parse str
        XRPose.TryParse json.RootElement
 
    static member Parse (str : System.Text.Json.JsonElement) =
        match XRPose.TryParse str with
        | Some p -> p
        | None -> failwithf "cannot parse XRPose: %A" str
 
    static member Parse (str : string) =
        match XRPose.TryParse str with
        | Some p -> p
        | None -> failwithf "cannot parse XRPose: %A" str
 
[<RequireQualifiedAccess>]
type XRDepthUsage =
    | GpuOptimized
    | CpuOptimized
    | Unknown of string
 
[<RequireQualifiedAccess>]
type XRDepthFormat =
    | LuminanceAlpha
    | Float32
    | Unknown of string
 
[<RequireQualifiedAccess>]
type XRDomOverlayState =
    | Screen
    | HeadLocked
    | Floating
    | Unknown of string
    | None
 
[<RequireQualifiedAccess>]
type XRBlendMode =
    | Opaque
    | Additive
    | AlphaBlend
    | Unknown of string
    | None
 
[<RequireQualifiedAccess>]
type XRInteractionMode =
    | ScreenSpace
    | WorldSpace
    | Unknown of string
    | None
    
[<RequireQualifiedAccess>]
type XRReflectionFormat =
    | SRGBA8
    | RGBA16F
    | Unknown of string
    | None
 
[<RequireQualifiedAccess>]
type XRSpaceDescription =
    | BoundedFloor
    | Local
    | LocalFloor
    | Unbounded
    | Viewer
    | Unknown of string
 
type XRDepthSensingOptions =
    {
        UsagePreference : list<XRDepthUsage>
        FormatPreference : list<XRDepthFormat>
    }
 
type XRFeature =
    | PlaneDetection
    | MeshDetection
    | CameraAccess
    | DepthSensing of XRDepthSensingOptions
    | Anchors
    | BoundedFloor
    | DomOverlay
    | HandTracking
    | HitTest
    | Layers
    | LightEstimation
    | Local
    | LocalFloor
    | SecondaryViews
    | Unbounded
    | Viewer
 
[<RequireQualifiedAccess>]
type XREye =
    | Left
    | Right
    | Unknown of string
    | None
 
[<Struct>]
type XRView(device : Device, handle : int) =
    // TODO: implement
    
    member x.Device = device
    
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.Eye =
        match WebXR.view_getEye handle with
        | "left" -> XREye.Left
        | "right" -> XREye.Right
        | str ->
            if System.String.IsNullOrWhiteSpace str then XREye.None
            else XREye.Unknown str
 
    member x.IsFirstPersonObserver =
        WebXR.view_getIsFirstPersonObserver handle
        
    member x.ProjectionMatrix =
        let arr = [| M44f.Identity |]
        let ptr = fixed arr
        WebXR.view_getProjectionMatrix handle ptr
        arr.[0]
 
    member x.RecommendedViewportScale =
        WebXR.view_getRecommendedViewportScale handle
 
    member x.Transform =
        WebXR.view_getTransform handle
 
    member x.RequestViewportScale(scale : float) =
        WebXR.view_requestViewportScale handle scale
 
[<Struct>]
type XRHitTestSource(handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
[<Struct>]
type XRHitTestResult(handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
 
type XRDepthInformation(device : Device, handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.IsCpu =
        WebXR.depthInfo_getHeight
    
    member x.Height =
        WebXR.depthInfo_getHeight handle |> int
        
    member x.Width =
        WebXR.depthInfo_getWidth handle |> int
        
    member x.NormDepthBufferFromNormView =
        WebXR.depthInfo_getNormDepthBufferFromNormView handle
        
    member x.RawValueToMeters =
        WebXR.depthInfo_getRawValueToMeters handle
        
    member x.GetData(format : XRDepthFormat) =
        let elementSize = 
            match format with
            | XRDepthFormat.LuminanceAlpha -> sizeof<uint16> 
            | XRDepthFormat.Float32 -> sizeof<float32>
            | _ -> 0
        let buffer = Array.zeroCreate<byte> (elementSize * x.Width * x.Height)
        use ptr = fixed buffer
        if WebXR.depthInfo_getData handle ptr then
            Some buffer
        else
            None
            
    member x.GetDepthInMeters(pt : V2d) =
        WebXR.depthInfo_getDepthInMeters handle pt.X pt.Y
        
    member x.Texture =
        let handle = WebXR.depthInfo_getTexture handle
        let w = x.Width
        let h = x.Height
        new Texture(
            device, handle,
            Silk.NET.OpenGLES.TextureTarget.Texture2DArray,
            Aardvark.Rendering.TextureDimension.Texture2D,
            Aardvark.Rendering.TextureFormat.R32f, V3i(w, h, 1),
            1, None, Some 2
        )
        
    
[<Struct>]
type XRViewerPose(device : Device, handle : int) =
    member x.IsNull = handle = 0
    member x.Handle = handle
 
    member x.Views =
        let device = device
        let arr = Array.zeroCreate 2
        let cnt = WebXR.viewerPose_getViews handle arr
        Array.init cnt (fun i ->
            XRView(device, arr.[i])
        )  
        
type XRSpace(handle : int) =
    member x.IsNull = handle = 0
    member x.Handle = handle

type XRReferenceSpace(handle : int) =
    inherit XRSpace(handle)
    // TODO: implement
    
type XRJointSpace(handle : int) =
    inherit XRSpace(handle)
    // TODO: implement
    
[<Struct>]
type XRAnchor(handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
[<Struct>]
type XRFrame(device : Device, handle : int) =
    // ===== TODO ======
    // getHitTestResultsForTransientInput()
    // getLightEstimate()
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.MeshSetCount =
        WebXR.frame_getMeshSetCount handle
    
    member x.TrackedAnchors =
        WebXR.frame_getTrackedAnchors handle |> Array.map (fun id -> XRAnchor(id))
    
    member x.CreateAnchor(pose : Euclidean3d, space : XRSpace) =
        let handle = handle
        task {
            let! handle = WebXR.frame_createAnchor handle pose space.Handle
            return XRAnchor(handle)
        }
    
    member x.FillJointRadii(joints : XRSpace[], radii : float32[]) =
        let ids = joints |> Array.map (fun j -> j.Handle)
        WebXR.frame_fillJointRadii handle ids radii
    
    member x.FillPoses(joints : XRSpace[], baseSpace : XRSpace, poses : M44f[]) =
        let ids = joints |> Array.map (fun j -> j.Handle)
        WebXR.frame_fillPoses handle ids baseSpace.Handle poses
    
    member x.GetDepthInformation(view : XRView) =
        XRDepthInformation(view.Device, WebXR.frame_getDepthInformation handle view.Handle)
    
    member x.GetHitTestResults(source : XRHitTestSource) =
        WebXR.frame_getHitTestResults handle source.Handle
        |> Array.map XRHitTestResult
    
    member x.GetJointPose(jointSpace : XRJointSpace, baseSpace : XRSpace) =
        WebXR.frame_getJointPose handle jointSpace.Handle baseSpace.Handle |> XRPose.Parse
    
    member x.GetPose(jointSpace : XRSpace, baseSpace : XRSpace) =
        WebXR.frame_getPose handle jointSpace.Handle baseSpace.Handle |> XRPose.Parse
    
    member x.GetViewerPose(refSpace : XRReferenceSpace) =
        XRViewerPose(device, WebXR.frame_getViewerPose handle refSpace.Handle)
   
[<Struct>]
type GamepadButtonState(pressed : int, touched : int, value : float32) =
    member x.Pressed = pressed <> 0
    member x.Touched = touched <> 0
    member x.Value = float value
    
    override x.ToString() =
        if x.Touched || x.Pressed then
            sprintf "Button { Pressed: %b, Touched: %b, Value: %f }" x.Pressed x.Touched x.Value
        else
            sprintf "Button { Pressed: %b, Touched: %b }" x.Pressed x.Touched
            
[<Struct>]
type Gamepad(handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.Id =
        WebXR.gamepad_getId handle
    
    member x.Index =
        WebXR.gamepad_getIndex handle
    
    member x.Hand =
        WebXR.gamepad_getHand handle
    
    member x.Mapping =
        WebXR.gamepad_getMapping handle
    
    member x.Buttons =
        let arr = Array.zeroCreate<GamepadButtonState> 32
        let cnt = WebXR.gamepad_getButtons handle arr
        Array.take cnt arr
        
    
 
[<Struct>]
type XRInputSource(handle : int) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.Gamepad =
        let id = WebXR.inputSource_getGamepad handle
        if id <= 0 then None
        else Some (Gamepad id)
    
[<Struct>]
type XRWebGLBinding(handle : int, device : Device) =
    // TODO: implement
    member x.IsNull = handle = 0
    member x.Handle = handle
    
    member x.NativeProjectionScaleFactor =
        WebXR.binding_getNativeProjectionScaleFactor handle
    
    member x.GetDepthInformation(view : XRView) =
        XRDepthInformation(device, WebXR.binding_getDepthInformation handle view.Handle)
    
    member x.GetSubImage(layer : int, frame : int, eye : string) =
        let handle = WebXR.binding_getSubImage handle layer frame eye

        new Texture(
            device, handle,
            Silk.NET.OpenGLES.TextureTarget.Texture2D,
            Aardvark.Rendering.TextureDimension.Texture2D,
            Aardvark.Rendering.TextureFormat.R32f, V3i(1024, 1024, 1),
            1, None, Some 2
        )
    
type XRWebGLLayer(handle : int, device : Device) =
    let signature = lazy (device.GetDefaultFramebufferSignature())
    member x.IsNull = handle = 0
    member x.Handle = handle
    member x.Device = device
 
    member x.Framebuffer =
        let fboHandle = WebXR.layer_getFramebuffer handle
        let size = V2i(int (WebXR.layer_getFramebufferWidth handle), int (WebXR.layer_getFramebufferHeight handle))
        new Framebuffer(device, signature.Value, size, Map.empty, None, fboHandle)

    member x.IgnoreDepthValues =
        WebXR.layer_getIgnoreDepthValues handle

    member x.FixedFoveation =
        WebXR.layer_getFixedFoveation handle

    member x.Antialias =
        WebXR.layer_getAntialias handle

    member x.GetViewport(view : XRView) =
        WebXR.layer_getViewport handle view.Handle        

type XRRenderState =
    {
        BaseLayer : option<XRWebGLLayer>
        DepthFar : option<float>
        DepthNear : option<float>
        InlineVerticalFieldOfView : option<float>
        Layers : option<XRWebGLLayer[]>
    }
    
    static member Default =
        {
            BaseLayer = None
            DepthFar = None
            DepthNear = None
            InlineVerticalFieldOfView = None
            Layers = None
        }
 
type XRSession(handle : int, device : Device) =
    member x.IsNull = handle = 0
    member x.Handle = handle
 
    
    member x.DepthUsage =
        match WebXR.session_getDepthUsage handle with
        | "gpu-optimized" -> Some XRDepthUsage.GpuOptimized
        | "cpu-optimized" -> Some XRDepthUsage.CpuOptimized
        | str ->
            if System.String.IsNullOrWhiteSpace str then None
            else Some (XRDepthUsage.Unknown str)
    
    member x.DepthDataFormat =
        match WebXR.session_getDepthDataFormat handle with
        | "float32" -> XRDepthFormat.Float32 |> Some
        | "luminance-alpha" -> XRDepthFormat.LuminanceAlpha |> Some
        | str ->
            if System.String.IsNullOrWhiteSpace str then None
            else Some (XRDepthFormat.Unknown str)
 
    member x.DomOverlayState =
        match WebXR.session_getDomOverlayState handle with
        | "floating" -> XRDomOverlayState.Floating
        | "screen" -> XRDomOverlayState.Screen
        | "head-locked" -> XRDomOverlayState.HeadLocked
        | str ->
            if System.String.IsNullOrWhiteSpace str then XRDomOverlayState.None
            else XRDomOverlayState.Unknown str 
 
    member x.EnvironmentBlendMode =
        match WebXR.session_getEnvironmentBlendMode handle with
        | "opaque" -> XRBlendMode.Opaque
        | "additive" -> XRBlendMode.Additive
        | "alpha-blend" -> XRBlendMode.AlphaBlend
        | str ->
            if System.String.IsNullOrWhiteSpace str then XRBlendMode.None
            else XRBlendMode.Unknown str
        
    member x.InputSources =
        let ids = Array.zeroCreate 16
        let cnt = WebXR.session_getInputSources handle ids
        Array.init cnt (fun i ->
            XRInputSource(ids.[i])    
        )
        
    member x.InteractionMode =
        match WebXR.session_getInteractionMode handle with
        | "screen-space" -> XRInteractionMode.ScreenSpace
        | "world-space" -> XRInteractionMode.WorldSpace
        | str ->
            if System.String.IsNullOrWhiteSpace str then XRInteractionMode.None
            else XRInteractionMode.Unknown str
    
    member x.PreferredReflectionFormat =
        match WebXR.session_getPreferredReflectionFormat handle with
        | "srgba8" -> XRReflectionFormat.SRGBA8
        | "rgba16f" -> XRReflectionFormat.RGBA16F
        | str ->
            if System.String.IsNullOrWhiteSpace str then XRReflectionFormat.None
            else XRReflectionFormat.Unknown str
        
    
    member x.RenderState
        with get() =
            let str = WebXR.session_getRenderState handle
        
            let doc = System.Text.Json.JsonDocument.Parse str
        
            let baseLayer =
                match doc.RootElement.TryGetProperty "baseLayer" with
                | (true, id) ->
                    let id = id.GetInt32()
                    if id = 0 then None
                    else Some (XRWebGLLayer(id, device))
                | _ ->
                    None
            
            let tryGetFloat (name : string) =
                match doc.RootElement.TryGetProperty(name) with
                | false, _ -> None
                | true, v ->
                    match v with
                    | Number n -> Some (float n)
                    | _ -> None
            
            let layers =
                match doc.RootElement.TryGetProperty("layers") with
                | (true, e) ->
                    if e.ValueKind = System.Text.Json.JsonValueKind.Array then
                        Array.init (e.GetArrayLength()) (fun i ->
                            let id = e[i].GetInt32()
                            XRWebGLLayer(id, device)    
                        ) |> Some
                    else
                        None
                | _ ->
                    None
            
            {
                BaseLayer = baseLayer
                DepthNear = tryGetFloat "depthNear" 
                DepthFar = tryGetFloat "depthFar"
                InlineVerticalFieldOfView = tryGetFloat "inlineVerticalFieldOfView"
                Layers = layers
            }
        and set (v : XRRenderState) =
            use ms = new System.IO.MemoryStream()
            use w = new System.Text.Json.Utf8JsonWriter(ms)
            
            
            w.WriteStartObject()
            
            match v.BaseLayer with
            | Some l -> w.WriteNumber("baseLayer", l.Handle)
            | None -> ()
                
            match v.DepthNear with
            | Some l -> w.WriteNumber("depthNear", l)
            | None -> ()
            
            match v.DepthFar with
            | Some l -> w.WriteNumber("depthFar", l)
            | None -> ()
            
            match v.InlineVerticalFieldOfView with
            | Some l -> w.WriteNumber("inlineVerticalFieldOfView", l)
            | None -> ()
            
            match v.Layers with
            | Some l when l.Length > 0 ->
                w.WritePropertyName "layers"
                
                w.WriteStartArray()
                for l in l do
                    w.WriteNumberValue l.Handle
                w.WriteEndArray()
            | _ -> ()
            
            w.WriteEndObject()
            w.Flush()
            
            let str = System.Text.Encoding.UTF8.GetString(ms.ToArray())
            WebXR.session_updateRenderState handle str 
            
    member x.CreateWebGLLayer (device : Device, options : XRWebGLLayerOptions) =
        let options =
            json (fun w ->
                w.WriteStartObject()
                w.WriteBoolean("alpha", options.Alpha)
                w.WriteBoolean("antialias", options.Antialias)
                w.WriteBoolean("depth", options.Depth)
                w.WriteBoolean("stencil", options.Stencil)
                w.WriteNumber("framebufferScaleFactor", options.FramebufferScaleFactor)
                w.WriteBoolean("ignoreDepthValues", options.IgnoreDepthValues)
                w.WriteEndObject()
            )
        
        XRWebGLLayer(WebXR.session_createXRWebGLLayer handle device.Context options, device)
    
    member x.CreateWebGLBinding (device : Device) =
        XRWebGLBinding(WebXR.session_createXRWebGLBinding handle device.Context, device)
    
    member x.RequestReferenceSpace(space : XRSpaceDescription) =
        try
            let space =
                match space with
                | XRSpaceDescription.Local -> "local"
                | XRSpaceDescription.BoundedFloor -> "bounded-floor"
                | XRSpaceDescription.LocalFloor -> "local-floor"
                | XRSpaceDescription.Unbounded -> "unbounded"
                | XRSpaceDescription.Viewer -> "viewer"
                | XRSpaceDescription.Unknown str -> str
                
            
            task {
                try
                    let! handle = WebXR.session_requestReferenceSpace handle space
                    return XRReferenceSpace(handle)
                with e ->
                    printfn "%A" e
                    return raise e
            }
        with e ->
            reraise()
    
    member x.SubscribeEventListener(eventName : string, callback : unit -> unit) =
        WebXR.session_addEventListener handle eventName callback
    
    member x.AddEventListener(eventName : string, callback : unit -> unit) =
        x.SubscribeEventListener(eventName, callback) |> ignore
    
    member x.GetEventObservable (eventName : string) =
        { new Microsoft.FSharp.Control.IEvent<unit> with
            member _.Subscribe(obs : IObserver<unit>) =
                x.SubscribeEventListener(eventName, fun () -> obs.OnNext())
            member _.AddHandler(obs : Handler<unit>) =
                x.AddEventListener(eventName, fun () -> obs.Invoke(x, ()))
            member _.RemoveHandler(obs : Handler<unit>) =
                // TODO: remove handler
                ()
        }
        
    [<CLIEvent>]
    member x.OnEnd = x.GetEventObservable "end"
    [<CLIEvent>]
    member x.OnInputSourcesChange = x.GetEventObservable "inputsourceschange"
    [<CLIEvent>]
    member x.OnSelect = x.GetEventObservable "select"
    [<CLIEvent>]
    member x.OnSelectEnd = x.GetEventObservable "selectend"
    [<CLIEvent>]
    member x.OnSelectStart = x.GetEventObservable "selectstart"
    [<CLIEvent>]
    member x.OnSqueeze = x.GetEventObservable "squeeze"
    [<CLIEvent>]
    member x.OnSqueezeEnd = x.GetEventObservable "squeezeend"
    [<CLIEvent>]
    member x.OnSqueezeStart = x.GetEventObservable "squeezestart"
    [<CLIEvent>]
    member x.OnVisibilityChange = x.GetEventObservable "visibilitychange"
    
    member x.RequestAnimationFrame(callback : double -> XRFrame -> unit) =
        WebXR.session_requestAnimationFrame handle (fun time frameHandle -> callback time (XRFrame(device, frameHandle)))
    
    //     
    // member x.RequestAnimationFrame (callback : XRFrame -> unit) =
    //     match renderState with
    //     | Some state ->
    //         let layer0 =
    //             match state.BaseLayer with
    //             | Some l -> Some l
    //             | None ->
    //                 match state.Layers with
    //                 | Some arr when arr.Length > 0 -> Some arr.[0]
    //                 | _ -> None
    //         
    //         match layer0 with
    //         | Some layer0 -> 
    //             WebXR.requestAnimationFrame handle layer0.Handle 0 layer0.Device.Context (fun info ->
    //                 ()    
    //             )
    //         | None ->
    //             ()
    //     | None ->
    //         ()
 
type XRSystem =
    
    static member IsSessionSupported (mode : XRMode) =
        
        let mode =
            match mode with
            | XRMode.Inline -> "inline"
            | XRMode.ImmersiveAR -> "immersive-ar"
            | XRMode.ImmersiveVR -> "immersive-vr"
        
        WebXR.isSupported mode
    
    static member RequestSession (device : Device, mode : XRMode, ?requiredFeatures : list<XRFeature>, ?optionalFeatures : list<XRFeature>) =
         // var session = await navigator.xr.requestSession(mode, {
         //              requiredFeatures: ["depth-sensing"],
         //              depthSensing: {
         //                usagePreference: ["gpu-optimized", "cpu-optimized"],
         //                dataFormatPreference: ["luminance-alpha", "float32"]
         //              }
         //            });
       
        let depthFormatStr (fmt : XRDepthFormat) =
            match fmt with
            | XRDepthFormat.Float32 -> "float32"
            | XRDepthFormat.LuminanceAlpha -> "luminance-alpha"
            | XRDepthFormat.Unknown str -> str
        
        let depthUsageStr (fmt : XRDepthUsage) =
            match fmt with
            | XRDepthUsage.GpuOptimized -> "gpu-optimized"
            | XRDepthUsage.CpuOptimized -> "cpu-optimized"
            | XRDepthUsage.Unknown str -> str
        
        let mutable depthSensingOptions = None
        let featureStr (f : XRFeature) =
            match f with
            | PlaneDetection ->
                "plane-detection"
            | MeshDetection ->
                "mesh-detection"
            | DepthSensing o ->
                depthSensingOptions <- Some o
                "depth-sensing"
            | CameraAccess ->
                "camera-access"
            | Anchors ->
                "anchors"
            | BoundedFloor ->
                "bounded-floor"
            | DomOverlay ->
                "dom-overlay"
            | HandTracking ->
                "hand-tracking"
            | HitTest ->
                "hit-test"
            | Layers ->
                "layers"
            | LightEstimation ->
                "light-estimation"
            | Local ->
                "local"
            | LocalFloor ->
                "local-floor"
            | SecondaryViews ->
                "secondary-views"
            | Unbounded ->
                "unbounded"
            | Viewer ->
                "viewer"
        
        
        let options = 
            JsObj.New [
                match optionalFeatures with
                | Some features ->
                    let names = features |> List.map featureStr
                    "optionalFeatures", List.toArray names :> obj
                | None ->
                    ()
                match requiredFeatures with
                | Some features ->
                    let names = features |> List.map featureStr
                    "requiredFeatures", List.toArray names :> obj
                | None ->
                    ()
                    
                match depthSensingOptions with
                | Some options ->
                    "depthSensing", JsObj.New [
                        "usagePreference", (options.UsagePreference |> List.map depthUsageStr |> List.toArray :> obj)
                        "dataFormatPreference", (options.FormatPreference |> List.map depthFormatStr |> List.toArray :> obj)
                    ] :> obj
                | None ->
                    ()
                    
            ]
            
        let mode =
            match mode with
            | XRMode.Inline -> "inline"
            | XRMode.ImmersiveAR -> "immersive-ar"
            | XRMode.ImmersiveVR -> "immersive-vr"
        
        task {
            let! handle = WebXR.requestSession mode options.Reference
            return XRSession(handle, device)
        }
    
 