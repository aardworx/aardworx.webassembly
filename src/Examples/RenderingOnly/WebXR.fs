namespace Aardworx.WebXR

open System
open Aardvark.Base
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

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
 
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module WebXR =
    open Microsoft.JSInterop
    
        
    type XRRenderState =
        {
            BaseLayer : option<int>
            DepthFar : option<float>
            DepthNear : option<float>
            InlineVerticalFieldOfView : option<float>
            Layers : option<int[]>
        }
        
        static member Default =
            {
                BaseLayer = None
                DepthFar = None
                DepthNear = None
                InlineVerticalFieldOfView = None
                Layers = None
            }
     
    
    type Mode =
        | ImmersiveVR
        | ImmersiveAR
        | Inline
    
    let private js = 
        """
        (function(self) {
            var xr = {};
      
            var sessions = [];
            var layers = [];
            var views = [];
            var refSpaces = [];
            var frames = [];
      
            function glAlloc(thing, table) {
                var glHandle = 0;
                if(thing) {
                    if("moduleId" in thing) {
                        glHandle = thing.moduleId;
                    }
                    else {
                        var id = Module.GL.getNewId(table);
                        table[id] = thing;
                        thing.moduleId = id;
                        glHandle = id;
                    }
                }
                return glHandle;
            }
            
            function newHandle(thing, table) {
                var id = 0;
                if(thing) {
                    if("moduleId" in thing) {
                        id = thing.moduleId;
                    }
                    else {
                        id = table.length == 0 ? 1 : table.length;
                        thing.moduleId = id;
                        table[id] = thing;
                    }
                }
                return id;
            }
            
        
            xr.isSupported = async function(mode) {
                console.log(mode);
                if(navigator.xr) {
                    var supported = await navigator.xr.isSessionSupported(mode);
                    return supported;
                }
                else {
                    return false;
                }
            };
            
            
            xr.requestSession = async function(mode, options) {
                try {
                    console.log(options);
                    var session = await navigator.xr.requestSession(mode, {
                      optionalFeatures: ["depth-sensing"],
                      depthSensing: {
                        usagePreference: ["gpu-optimized", "cpu-optimized"],
                        dataFormatPreference: ["luminance-alpha", "float32"]
                      }
                    });
                    if(!session) return 0;
                    console.log(session.depthUsage);
                    console.log(session);
                    const myId = newHandle(session, sessions);
                    return myId;
                } catch(e) {
                    console.error(e);
                    return 0;
                }
            };
            
            xr.getDepthDataFormat = function(sessionId) {
                var session = sessions[sessionId];
                return session.depthDataFormat;
            };
            
            xr.getDepthUsage = function(sessionId) {
                var session = sessions[sessionId];
                return session.depthUsage;
            };
            
            xr.requestReferenceSpace = async function(sessionId, type) {
                var session = sessions[sessionId];
                var space = await session.requestReferenceSpace(type);
                var myId = newHandle(space, refSpaces);
                return myId;
            };
            let frameCallback = null;
            let frameCallback2 = null;
            
            xr.getLayerFramebuffer = function(layerId) {
                var layer = layers[layerId];
                var frameBufferId = glAlloc(layer.framebuffer, Module.GL.framebuffers);
                return frameBufferId;
            };
            
            xr.getLayerFramebufferWidth = function(layerId) {
                var layer = layers[layerId];
                return layer.framebufferWidth;
            };
            xr.getLayerFramebufferHeight = function(layerId) {
                var layer = layers[layerId];
                return layer.framebufferHeight;
            };
            
            xr.getLayerIgnoreDepthValues = function(layerId) {
                var layer = layers[layerId];
                return layer.ignoreDepthValues;
            };
            
            xr.getLayerFixedFoveation = function(layerId) {
                var layer = layers[layerId];
                return layer.fixedFoveation;
            };
            xr.getLayerAntialias = function(layerId) {
                var layer = layers[layerId];
                return layer.antialias;
            };
            
            xr.getLayerViewport = function(layerId, viewId) {
                var layer = layers[layerId];
                var view = views[viewId]; 
                var vp = layer.getViewport(view);
                return JSON.stringify({ x: vp.x, y: vp.y, width: vp.width, height: vp.height });
            };
            
            xr.requestAnimationFrame = function(sessionId, layerId, refSpace, contextId) {
                if(!frameCallback) {
                    frameCallback = Module.mono_bind_static_method("[RenderingOnly] Aardworx.WebXR.WebXRModule:frameCallback");
                    frameCallback2 = Module.mono_bind_static_method("[RenderingOnly] Aardworx.WebXR.WebXRModule:frameCallback2");
                }
                var session = sessions[sessionId];
                var refSpace = refSpaces[refSpace];
                var glLayer = layers[layerId];
                var gl = Module.GL.contexts[contextId].GLctx;
                
                try {
                    session.requestAnimationFrame(function(time, frame) {
                        var frameHandle = newHandle(frame, frames);
                        frameCallback2(sessionId, time, frameHandle);
                        var pose = frame.getViewerPose(refSpace);
                        
                        glLayer = session.renderState.baseLayer;
                        var frameBufferId = glAlloc(glLayer.framebuffer, Module.GL.framebuffers);
                        
                        var viewports = pose.views.map((view) => {
                            var vp = glLayer.getViewport(view);
                            
                            
                            var depthInfo = session.glBinding ? session.glBinding.getDepthInformation(view) : null;
                            var tid = 0;
                            if(depthInfo && depthInfo.texture) {
                                tid = glAlloc(depthInfo.texture, Module.GL.textures);
                            }
                            
                            depthInfo.texture
                            return {
                                x: vp.x,
                                y: vp.y,
                                width: vp.width,
                                height: vp.height,
                                view: Array.from(view.transform.matrix),
                                proj: Array.from(view.projectionMatrix),
                                depthTextureId: tid,
                                depthScale: depthInfo.rawValueToMeters
                            };
                        });
                        
                        var ret = JSON.stringify({ framebuffer: frameBufferId, framebufferWidth: glLayer.framebufferWidth, framebufferHeight: glLayer.framebufferHeight, viewports: viewports });
                        frameCallback(sessionId, ret);
                    });;
                } catch(e) {
                    frameCallback(sessionId, JSON.stringify(e));
                }
            };
            
            xr.makeXRCompatible = async function(contextId) {
                return await new Promise(function (resolve) {
                    var res = Module.GL.contexts[contextId].GLctx.makeXRCompatible();
                    res.then(function() { resolve(true); }).catch(function() { resolve(false); });
                });
            };
            
            xr.createXRWebGLLayer = function(sessionId, contextId, options) {
                var session = sessions[sessionId];
                var ctx = Module.GL.contexts[contextId].GLctx;
                // var binding = new XRWebGLBinding(session, ctx);
                // session.glBinding = binding;
                var layer = new XRWebGLLayer(session, ctx, options);
                var myId = newHandle(layer, layers);
                return myId;
            };
            
            xr.updateRenderState = function(sessionId, options) {
                var session = sessions[sessionId];
                var state = {};
                if(options.baseLayer) {
                    state.baseLayer = layers[options.baseLayer];
                }
                if("depthFar" in options) {
                    state.depthFar = options.depthFar;
                }
                if("depthNear" in options) {
                    state.depthNear = options.depthNear;
                }
                if("inlineVerticalFieldOfView" in options) {
                    state.inlineVerticalFieldOfView = options.frameOfReference;
                }
                if(options.layers) {
                    state.layers = options.layers.map((lid) => layers[lid]);
                }
                console.log(state);
                session.updateRenderState(state);
                
            };
            xr.updateRenderStateNoState = function(sessionId) {
                var session = sessions[sessionId];
                session.updateRenderState();
                
            };
            
            self.xr = xr;
        })(window);
        """
    
    let mutable private installed = false
    let init() =
        if not installed then
            printfn "install WebXR"
            JsObj.InstallScript js
            installed <- true
        
    
    let private modeString (mode : Mode) =
        match mode with
        | Mode.ImmersiveAR -> "immersive-ar"
        | Mode.ImmersiveVR -> "immersive-vr"
        | Mode.Inline -> "inline"
    
    let isSupported (mode : Mode) =
        init()
        let mode = modeString mode
        JSRuntime.Instance.InvokeAsync<bool>("window.xr.isSupported", [| mode :> obj |])
    
    let requestSession (mode : Mode) (options : obj) =
        init()
        let mode = modeString mode
        JSRuntime.Instance.InvokeAsync<int>("window.xr.requestSession", [| mode :> obj; options |])
    
    let getDepthDataFormat (session : int) =
        JSRuntime.Instance.Invoke<string>("window.xr.getDepthDataFormat", [| session :> obj |])
    
    let getDepthUsage (session : int) =
        JSRuntime.Instance.Invoke<string>("window.xr.getDepthUsage", [| session :> obj |])
    
    let private callbacks = Dict<int, ref<list<string -> unit>>>()
    let frameCallback (session : int) (content : string) =
        match callbacks.TryRemove session with
        | (true, cbs) ->
            for c in cbs.Value do c content
        | _ ->
            ()
    let private callbacks2 = Dict<int, ref<list<double -> int -> unit>>>()
    let frameCallback2 (session : int) (time : double) (frameHandle : int) =
        match callbacks2.TryRemove session with
        | (true, cbs) ->
            for c in cbs.Value do c time frameHandle
        | _ ->
            ()
    
    
    let requestAnimationFrame (session : int) (layer : int) (refSpace : int) (context : WebGLContext) (callback : string -> unit) =
        init()
        let l = callbacks.GetOrCreate(session, fun _ -> ref [])
        l.Value <- callback :: l.Value
        JSRuntime.Instance.InvokeVoid("window.xr.requestAnimationFrame", [| session :> obj; layer :> obj; refSpace :> obj; context.Handle :> obj |])
        
    let requestAnimationFrame2 (session : int) (layer : int) (refSpace : int) (context : WebGLContext) (callback : double -> int -> unit) =
        init()
        let l = callbacks2.GetOrCreate(session, fun _ -> ref [])
        l.Value <- callback :: l.Value
        JSRuntime.Instance.InvokeVoid("window.xr.requestAnimationFrame", [| session :> obj; layer :> obj; refSpace :> obj; context.Handle :> obj |])
        
    
    
    let requestReferenceSpace (session : int) (space : string) =
        init()
        JSRuntime.Instance.InvokeAsync<int>("window.xr.requestReferenceSpace", [| session :> obj; space :> obj |])
    
    let makeXRCompatible (context : WebGLContext) =
        init()
        JSRuntime.Instance.InvokeAsync<bool>("window.xr.makeXRCompatible", [| context.Handle :> obj |])
  
    let createXRWebGLLayer (session : int) (context : WebGLContext) (options : XRWebGLLayerOptions) =
        init()
        
        let options =
            JsObj.New [
                "alpha", options.Alpha
                "antialias", options.Antialias
                "depth", options.Depth
                "stencil", options.Stencil
                "framebufferScaleFactor", options.FramebufferScaleFactor
                "ignoreDepthValues", options.IgnoreDepthValues
            ]
        
        
        JSRuntime.Instance.Invoke<int>("window.xr.createXRWebGLLayer", [| session :> obj; context.Handle :> obj; options.Reference :> obj |])
  
    let getLayerFramebuffer (layer : int) =
        JSRuntime.Instance.Invoke<uint32>("window.xr.getLayerFramebuffer", [| layer :> obj |])
  
    let getLayerFramebufferSize (layer : int) =
        let w = JSRuntime.Instance.Invoke<double>("window.xr.getLayerFramebufferWidth", [| layer :> obj |]) |> int
        let h = JSRuntime.Instance.Invoke<double>("window.xr.getLayerFramebufferHeight", [| layer :> obj |]) |> int
        V2i(w, h)
        
    let getLayerIgnoreDepthValues (layer : int) =
        JSRuntime.Instance.Invoke<bool>("window.xr.getLayerIgnoreDepthValues", [| layer :> obj |])
        
    let getLayerFixedFoveation (layer : int) =
        JSRuntime.Instance.Invoke<double>("window.xr.getLayerFixedFoveation", [| layer :> obj |])
        
    let getLayerAntialias (layer : int) =
        JSRuntime.Instance.Invoke<bool>("window.xr.getLayerAntialias", [| layer :> obj |])
        
    let getLayerViewport (layer : int) (view : int) =
        let json = JSRuntime.Instance.Invoke<string>("window.xr.getLayerViewport", [| layer :> obj; view :> obj |])
        let doc = System.Text.Json.JsonDocument.Parse json
        
        let x = doc.RootElement.GetProperty("x").GetDouble()
        let y = doc.RootElement.GetProperty("y").GetDouble()
        let w = doc.RootElement.GetProperty("width").GetDouble()
        let h = doc.RootElement.GetProperty("height").GetDouble()
        Box2d(V2d(x,y), V2d(x+w-1.0,y+h-1.0))
        
        
    let updateRenderState (session : int) (state : XRRenderState) =
        init()
        let options =
            JsObj.New [
                match state.BaseLayer with
                | Some l -> "baseLayer", state.BaseLayer :> obj
                | None -> ()
                
                match state.DepthFar with
                | Some d -> "depthFar", state.DepthFar :> obj
                | None -> ()
                
                match state.DepthNear with
                | Some d -> "depthNear", state.DepthNear :> obj
                | None -> ()
                
                match state.InlineVerticalFieldOfView with
                | Some f -> "inlineVerticalFieldOfView", state.InlineVerticalFieldOfView :> obj
                | None -> ()
                
                match state.Layers with
                | Some l -> "layers", state.Layers :> obj
                | None -> ()
            ]
        
        JSRuntime.Instance.InvokeVoid("window.xr.updateRenderState", [| session :> obj; options.Reference :> obj |])
  
    let updateRenderStateNoState (session : int) =
        init()
        JSRuntime.Instance.InvokeVoid("window.xr.updateRenderStateNoState", [| session :> obj |])


// usagePreference: ["gpu-optimized", "cpu-optimized"],
// dataFormatPreference: ["luminance-alpha", "float32"]
 
type XRDepthUsagePreference =
    | GpuOptimized
    | CpuOptimized
 
[<RequireQualifiedAccess>]
type XRDepthFormat =
    | LuminanceAlpha
    | Float32
 
type XRDepthSensingOptions =
    {
        UsagePreference : list<XRDepthUsagePreference>
        FormatPreference : list<XRDepthFormat>
    }
 
type XRFeature =
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
 
type XRFrame() =
    class end
 
type XRView(handle : int) =
    member x.Handle = handle
 
type XRWebGLLayer(handle : int, device : Device) =
    let signature = lazy (device.GetDefaultFramebufferSignature())
    member x.Handle = handle
    member x.Device = device
 
    member x.Framebuffer =
        let fboHandle = WebXR.getLayerFramebuffer handle
        let size = WebXR.getLayerFramebufferSize handle
        new Framebuffer(device, signature.Value, size, Map.empty, None, fboHandle)

    member x.IgnoreDepthValues =
        WebXR.getLayerIgnoreDepthValues handle

    member x.FixedFoveation =
        WebXR.getLayerFixedFoveation handle

    member x.Antialias =
        WebXR.getLayerAntialias handle

    member x.GetViewport(view : XRView) =
        WebXR.getLayerViewport handle view.Handle        

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
     
 
type XRSession(handle : int) =
    
    let mutable renderState = None
    
    member x.Handle = handle
 
    
    member x.DepthUsage =
        match WebXR.getDepthUsage handle with
        | "gpu-optimized" -> Some XRDepthUsagePreference.GpuOptimized
        | "cpu-optimized" -> Some XRDepthUsagePreference.CpuOptimized
        | _ -> None
    
    member x.DepthDataFormat =
        match WebXR.getDepthDataFormat handle with
        | "float32" -> XRDepthFormat.Float32 |> Some
        | "luminance-alpha" -> XRDepthFormat.LuminanceAlpha |> Some
        | _ -> None
 
    member x.CreateWebGLLayer (device : Device, options : XRWebGLLayerOptions) =
        XRWebGLLayer(WebXR.createXRWebGLLayer handle device.Context options, device)
    
    member x.UpdateRenderState (state : XRRenderState) =
        renderState <- Some state
        
        let internalState =
            {
                WebXR.BaseLayer = state.BaseLayer |> Option.map (fun l -> l.Handle)
                WebXR.DepthFar = state.DepthFar
                WebXR.DepthNear = state.DepthNear
                WebXR.InlineVerticalFieldOfView = state.InlineVerticalFieldOfView
                WebXR.Layers = state.Layers |> Option.map (fun l -> l |> Array.map (fun l -> l.Handle))
            }
        
        WebXR.updateRenderState handle internalState
 
    member x.UpdateRenderState() =
        WebXR.updateRenderStateNoState handle
        
    member x.RequestAnimationFrame (callback : XRFrame -> unit) =
        match renderState with
        | Some state ->
            let layer0 =
                match state.BaseLayer with
                | Some l -> Some l
                | None ->
                    match state.Layers with
                    | Some arr when arr.Length > 0 -> Some arr.[0]
                    | _ -> None
            
            match layer0 with
            | Some layer0 -> 
                WebXR.requestAnimationFrame handle layer0.Handle 0 layer0.Device.Context (fun info ->
                    ()    
                )
            | None ->
                ()
        | None ->
            ()
 
type XRSystem =
    
    static member IsSessionSupported (mode : WebXR.Mode) =
        WebXR.isSupported mode
    
    static member RequestSession (mode : WebXR.Mode, ?requiredFeatures : list<XRFeature>, ?optionalFeatures : list<XRFeature>) =
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
        
        let depthUsageStr (fmt : XRDepthUsagePreference) =
            match fmt with
            | XRDepthUsagePreference.GpuOptimized -> "gpu-optimized"
            | XRDepthUsagePreference.CpuOptimized -> "cpu-optimized"
        
        let mutable depthSensingOptions = None
        let featureStr (f : XRFeature) =
            match f with
            | DepthSensing o ->
                depthSensingOptions <- Some o
                "depth-sensing"
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
            
        
        task {
            let! handle = WebXR.requestSession mode options.Reference
            return XRSession(handle)
        }
    
 