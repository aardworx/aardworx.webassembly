(function(self) {
    const xr = {};

    const sessions = [];
    const layers = [];
    const views = [];
    const refSpaces = [];
    const frames = [];
    const inputSources = [];
    const bindings = [];
    const viewerPoses = [];
    const anchors = [];
    const depthInfos = [];
    const hitTestSources = [];
    const hitTestResults = [];
    const gamepads = [];
    
    function getGLHandle(thing, table) {
        let glHandle = 0;
        if(thing) {
            if("moduleId" in thing) {
                glHandle = thing.moduleId;
            }
            else {
                const id = Module.GL.getNewId(table);
                table[id] = thing;
                thing.moduleId = id;
                glHandle = id;
            }
        }
        return glHandle;
    }
    
    function getHandle(thing, table) {
        let id = 0;
        if(thing) {
            if("moduleId" in thing) {
                id = thing.moduleId;
            }
            else {
                id = table.length === 0 ? 1 : table.length;
                thing.moduleId = id;
                table[id] = thing;
            }
        }
        return id;
    }
    
    function transformToObject(pose) {
        return {
            position: { x: pose.position.x, y: pose.position.y, z: pose.position.z },
            orientation: { x: pose.orientation.x, y: pose.orientation.y, z: pose.orientation.z, w: pose.orientation.w },
        };
    }

    xr.isSupported = async function(mode) {
        if(navigator.xr) {
            return await navigator.xr.isSessionSupported(mode);
        }
        else {
            return false;
        }
    };
    
    xr.requestSession = async function(mode, options) {
        try {
            const session = await navigator.xr.requestSession(mode, options);
            if(!session) return 0;
            return getHandle(session, sessions);
        } catch(e) {
            console.error(e);
            return 0;
        }
    };
    
    xr.session_getDepthDataFormat = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.depthDataFormat;
    };
    
    xr.session_getDepthUsage = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.depthUsage;
    };
    
    xr.session_getDomOverlayState = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.domOverlayState;
    };
    
    
    xr.session_getInputSources = function(sessionId, inputSourcePtr, inputSourcePtrLen) {
        if(sessionId <= 0) return 0;
        const session = sessions[sessionId];
        const arr = new Int32Array(Module.HEAPU8.buffer, inputSourcePtr, inputSourcePtrLen);
        
        let existing = session.inputSources.length;
        const count = inputSourcePtrLen < existing ? inputSourcePtrLen : existing;
        for(let i = 0; i < count; i++) {
            arr[i] = getHandle(session.inputSources[i], inputSources);
        }
        return existing;
    };
    
    xr.session_getEnvironmentBlendMode = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.environmentBlendMode;
    };
    
    xr.session_getInteractionMode = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.interactionMode;
    };
    
    xr.session_getPreferredReflectionFormat = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.preferredReflectionFormat;
    };
    
    xr.session_getRenderState = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        
        const obj =
            {
                baseLayer: session.renderState.baseLayer ? getHandle(session.renderState.baseLayer, layers) : 0,
                depthFar: session.renderState.depthFar,
                depthNear: session.renderState.depthNear,
                inlineVerticalFieldOfView: session.renderState.inlineVerticalFieldOfView,
                layers: session.renderState.layers ? session.renderState.layers.map((layer) => getHandle(layer, layers)) : null
            };
        
        return JSON.stringify(obj);
    };
    
    xr.session_updateRenderState = function(sessionId, options) {
        if(sessionId <= 0) return;
        const session = sessions[sessionId];
        options = JSON.parse(options);
        
        const state = {};
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
        session.updateRenderState(state);
    };
    
    xr.session_getVisibilityState = function(sessionId) {
        if(sessionId <= 0) return null;
        const session = sessions[sessionId];
        return session.visibilityState;
    };
    
    xr.session_requestReferenceSpace = async function(sessionId, type) {
        if(sessionId <= 0) return Promise.resolve(0);
        const session = sessions[sessionId];
        const space = await session.requestReferenceSpace(type);
        return getHandle(space, refSpaces);
    };
    
    let session_callback = null;
    
    xr.session_addEventListener = function(sessionId, type) {
        if(sessionId <= 0) return;
        const session = sessions[sessionId];
        if(!session_callback) {
            session_callback = Module.mono_bind_static_method("[Aardworx.WebAssembly.WebXR] Aardworx.WebAssembly.WebXR.WebXRModule:session_callback");
        }
        
        session.addEventListener(type, function(_event) {
            session_callback(sessionId, type);
        });
    };

    
    xr.session_createXRWebGLLayer = function(sessionId, contextId, options) {
        if(sessionId <= 0) return 0;
        const session = sessions[sessionId];
        const ctx = Module.GL.contexts[contextId].GLctx;
        options = JSON.parse(options);
        const layer = new XRWebGLLayer(session, ctx, options);
        return getHandle(layer, layers);
    };
    
    xr.session_createXRWebGLBinding = function(sessionId, contextId) {
        if(sessionId <= 0) return 0;
        try {
            if(window.XRWebGLBinding) {
                const session = sessions[sessionId];
                const ctx = Module.GL.contexts[contextId].GLctx;
                const binding = new XRWebGLBinding(session, ctx);
                return getHandle(binding, bindings);
            }
            else return 0;
        } catch(e) {
            return 0;
        }
    };
    
    let session_animationFrameCallback = null;
    xr.session_requestAnimationFrame = function(sessionId) {
        if(sessionId <= 0) return;
        const session = sessions[sessionId];
        if(!session_animationFrameCallback) {
            session_animationFrameCallback = Module.mono_bind_static_method("[Aardworx.WebAssembly.WebXR] Aardworx.WebAssembly.WebXR.WebXRModule:session_animationFrameCallback");
        }
        session.requestAnimationFrame(function(time, frame) {
            const frameHandle = getHandle(frame, frames);
            session_animationFrameCallback(sessionId, time, frameHandle);
        });
    };

    xr.layer_getFramebuffer = function(layerId) {
        if(layerId <= 0) return 0;
        const layer = layers[layerId];
        return getGLHandle(layer.framebuffer, Module.GL.framebuffers);
    };
    
    xr.layer_getFramebufferWidth = function(layerId) {
        if(layerId <= 0) return 0;
        const layer = layers[layerId];
        return layer.framebufferWidth;
    };
    xr.layer_getFramebufferHeight = function(layerId) {
        if(layerId <= 0) return 0;
        const layer = layers[layerId];
        return layer.framebufferHeight;
    };
    
    xr.layer_getIgnoreDepthValues = function(layerId) {
        if(layerId <= 0) return true;
        const layer = layers[layerId];
        return layer.ignoreDepthValues;
    };
    
    xr.layer_getFixedFoveation = function(layerId) {
        if(layerId <= 0) return 0.0;
        const layer = layers[layerId];
        return layer.fixedFoveation;
    };
    xr.layer_getAntialias = function(layerId) {
        if(layerId <= 0) return false;
        const layer = layers[layerId];
        return layer.antialias;
    };
    
    xr.layer_getViewport = function(layerId, viewId, vpPtr) {
        if(layerId <= 0 || viewId <= 0) return null;
        const layer = layers[layerId];
        const view = views[viewId]; 
        const vp = layer.getViewport(view);
        
        var vpArr = new Float64Array(Module.HEAPU8.buffer, vpPtr, 4);
        vpArr[0] = vp.x;
        vpArr[1] = vp.y;
        vpArr[2] = vp.width;
        vpArr[3] = vp.height;
    };
    
    xr.frame_getTrackedAnchors = function(frameId) {
        const frame = frames[frameId];
        const a = frame.trackedAnchors;
        const res = Array.from(a).map((a) => getHandle(a, anchors));
        return JSON.stringify(res);
    };
    
    xr.frame_createAnchor = async function(frameId, pose, spaceId) {
        const frame = frames[frameId];
        const space = refSpaces[spaceId];
        
        pose = JSON.parse(pose);
        pose = new XRRigidTransform(pose.position, pose.orientation);
        
        var anchor = await frame.createAnchor(pose, space);
        return getHandle(anchor, anchors);
    };
    
    xr.frame_fillJointRadii = function(frameId, jointSpacePtr, jointSpaceLen, ptr) {
        const ids = new Int32Array(Module.HEAPU8.buffer, jointSpacePtr, jointSpaceLen);
        const spaces = Array.from(ids).map((id) => refSpaces[id]);
        const res = new Float32Array(Module.HEAPU8.buffer, ptr, spaces.length);
        const frame = frames[frameId];
        return frame.fillJointRadii(spaces, res);
    };
    
    function swap(arr, i, j) {
        const temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
    
    xr.frame_fillPoses = function(frameId, jointSpacePtr, jointSpaceLen, baseSpace, ptr) {
        const ids = new Int32Array(Module.HEAPU8.buffer, jointSpacePtr, jointSpaceLen);
        const spaces = Array.from(ids).map((id) => refSpaces[id]);
        const res = new Float32Array(Module.HEAPU8.buffer, ptr, 16*spaces.length);
        const frame = frames[frameId];
        baseSpace = spaces[baseSpace];
        const ret = frame.fillPoses(spaces, baseSpace, res);
        
        let off = 0;
        for(i = 0; i < spaces.length; i++) {
            swap(res, off + 1, off + 4);
            swap(res, off + 2, off + 8);
            swap(res, off + 3, off + 12);
            swap(res, off + 6, off + 9);
            swap(res, off + 7, off + 13);
            swap(res, off + 11, off + 14);
            off += 16;
        }
        
        return ret;
    };
    
    xr.frame_getDepthInformation = function(frameId, viewId) {
        try {
            const frame = frames[frameId];
            const view = views[viewId];
            const depthInfo = frame.getDepthInformation(view);
            return getHandle(depthInfo, depthInfos);
        } catch(e) {
            console.error(e);
            return 0;
        }
    };
    
    xr.frame_getHitTestResults = function(frameId, hitTestSourceId) {
        const frame = frames[frameId];
        const hitTestSource = hitTestSources[hitTestSourceId];
        const results = frame.getHitTestResults(hitTestSource);
        
        const ids = results.map((r) => getHandle(r, hitTestResults));
        return JSON.stringify(ids);
    };
    
    xr.frame_getJointPose = function(frameId, jointSpaceId, baseSpaceId) {
        const frame = frames[frameId];
        const jointSpace = refSpaces[jointSpaceId];
        const baseSpace = refSpaces[baseSpaceId];
        const pose = frame.getJointPose(jointSpace, baseSpace);
        
        return JSON.stringify({
            radius: pose.radius,
            angularVelocity: Array.from(pose.angularVelocity),
            linearVelocity: Array.from(pose.linearVelocity),
            emulatedPosition: pose.emulatedPosition,
            transform: transformToObject(pose.transform)
        });
        
    };
     
    xr.frame_getPose = function(frameId, jointSpaceId, baseSpaceId) {
        const frame = frames[frameId];
        const jointSpace = refSpaces[jointSpaceId];
        const baseSpace = refSpaces[baseSpaceId];
        const pose = frame.getPose(jointSpace, baseSpace);
        
        return JSON.stringify({
            angularVelocity: Array.from(pose.angularVelocity),
            linearVelocity: Array.from(pose.linearVelocity),
            emulatedPosition: pose.emulatedPosition,
            transform: transformToObject(pose.transform)
        });
        
    };
    
    xr.frame_getViewerPose = function(frameId, refSpaceId) {
        const frame = frames[frameId];
        const refSpace = refSpaces[refSpaceId];
        const pose = frame.getViewerPose(refSpace);
        return getHandle(pose, viewerPoses);
    };
    
    xr.frame_getMeshSetCount = function(frameId) {
        const frame = frames[frameId];
        const meshes = frame.detectedMeshes;
        console.log("meshes", meshes);
        if(meshes) return meshes.size;
        else return 0;
    };
    
    xr.frame_getMesh = function(frameId, index, pPositions, pPositionLength, pIndices, pIndexLength) {
        const frame = frames[frameId];
        const meshes = frame.detectedMeshes;
        if(meshes) {
            const mesh = meshes[index];
            lkamsndjklmaskdlas
        }
        else {
            Module.HEAP32[pPositionLength >> 2] = 0;
            Module.HEAP32[pIndexLength >> 2] = 0;
        }
    };
    
    xr.view_getEye = function(viewId) {
        const view = views[viewId];
        return view.eye;
    };
    
    xr.view_getIsFirstPersonObserver = function(viewId) {
        const view = views[viewId];
        return view.isFirstPersonObserver;
    };
    
    xr.view_getProjectionMatrix = function(viewId, ptr) {
        const view = views[viewId];
        const res = new Float32Array(Module.HEAPU8.buffer, ptr, 16);
        res[0] = view.projectionMatrix[0];
        res[1] = view.projectionMatrix[4];
        res[2] = view.projectionMatrix[8];
        res[3] = view.projectionMatrix[12];
        res[4] = view.projectionMatrix[1];
        res[5] = view.projectionMatrix[5];
        res[6] = view.projectionMatrix[9];
        res[7] = view.projectionMatrix[13];
        res[8] = view.projectionMatrix[2];
        res[9] = view.projectionMatrix[6];
        res[10] = view.projectionMatrix[10];
        res[11] = view.projectionMatrix[14];
        res[12] = view.projectionMatrix[3];
        res[13] = view.projectionMatrix[7];
        res[14] = view.projectionMatrix[11];
        res[15] = view.projectionMatrix[15];
    };
    
    xr.view_getRecommendedViewportScale = function(viewId) {
        const view = views[viewId];
        return view.recommendedViewportScale ?? 1.0;
    };
    
    xr.view_getTransform = function(viewId, ptr) {
        const view = views[viewId];
        const res = new Float64Array(Module.HEAPU8.buffer, ptr, 7);
        res[0] = view.transform.orientation.w;
        res[1] = view.transform.orientation.x;
        res[2] = view.transform.orientation.y;
        res[3] = view.transform.orientation.z;
        res[4] = view.transform.position.x;
        res[5] = view.transform.position.y;
        res[6] = view.transform.position.z;
    };
    
    xr.view_requestViewportScale = function(viewId, scale) {
        const view = views[viewId];
        view.requestViewportScale(scale);
    };
    
    xr.depthInfo_isCpu = function(depthId) {
        if(depthId <= 0) return false;
        const depthInfo = depthInfos[depthId];
        return depthInfo instanceof XRCPUDepthInformation;  
    };
    
    xr.depthInfo_isGpu = function(depthId) {
        if(depthId <= 0) return false;
        const depthInfo = depthInfos[depthId];
        return depthInfo instanceof XRWebGLDepthInformation;  
    };
    
    xr.depthInfo_getHeight = function(depthId) {
        if(depthId <= 0) return 1;
        const depthInfo = depthInfos[depthId];
        return depthInfo.height;
    }
    xr.depthInfo_getWidth = function(depthId) {
        if(depthId <= 0) return 1;
        const depthInfo = depthInfos[depthId];
        return depthInfo.width;
    }
    
    xr.depthInfo_getNormDepthBufferFromNormView  = function(depthId, ptr) {
        if(depthId <= 0) return;
        const depthInfo = depthInfos[depthId];
        
        const trafo = depthInfo.normDepthBufferFromNormView;
        const res = new Float64Array(Module.HEAPU8.buffer, ptr, 7);
        res[0] = trafo.orientation.w;
        res[1] = trafo.orientation.x;
        res[2] = trafo.orientation.y;
        res[3] = trafo.orientation.z;
        res[4] = trafo.position.x;
        res[5] = trafo.position.y;
        res[6] = trafo.position.z;
    };
    
    xr.depthInfo_getRawValueToMeters = function(depthId) {
        if(depthId <= 0) return 1.0;
        const depthInfo = depthInfos[depthId];
        return depthInfo.rawValueToMeters;
    };
    
    xr.depthInfo_getData = function(depthId, buffer) {
        if(depthId <= 0) return false;
        const depthInfo = depthInfos[depthId];
        if(depthInfo instanceof XRCPUDepthInformation) {
            Module.HEAPU8.set(depthInfo.data, buffer);
            return true;
        }
        else return false;
    };
    
    xr.depthInfo_getDepthInMeters = function(depthId, x, y) {
        if(depthId <= 0) return 0;
        const depthInfo = depthInfos[depthId];
        if(depthInfo instanceof XRCPUDepthInformation) {
            return depthInfo.getDepthInMeters(x, y);
        }
        else return 0;
    };
    
    xr.viewerPose_getViews = function(poseId, viewsPtr, viewsLen) {
        if(poseId <= 0) return 0;
        const pose = viewerPoses[poseId];
        
        const arr = new Int32Array(Module.HEAPU8.buffer, viewsPtr, viewsLen);
        let index = 0;
        for(const view of pose.views) {
            if(index >= viewsLen) break;
            arr[index] = getHandle(view, views);
            index++;
        }
        return index;
        
    };
    
    xr.depthInfo_getTexture = function(depthId) {
        if(depthId <= 0) return 0;
        const depthInfo = depthInfos[depthId];
        return getGLHandle(depthInfo.texture, Module.GL.textures);
    };
    
    xr.binding_getSubImage = function(bindingId, layerId, frameId, eye) {
        if(bindingId <= 0) return 0;
        const binding = bindings[bindingId];
        const layer = layers[layerId];
        const frame = frames[frameId];
        const subImage = binding.getSubImage(layer, frame, eye);
        console.log(subImage);
        return getGLHandle(subImage.colorTexture, Module.GL.textures);
    };
    
    
    xr.binding_getNativeProjectionScaleFactor = function(bindingId) {
        if(bindingId <= 0) return 1.0;
        const binding = bindings[bindingId];
        return binding.nativeProjectionScaleFactor;  
    };
    
    xr.binding_getDepthInformation = function(bindingId, viewId) { 
        if(bindingId <= 0) return 0;
        const binding = bindings[bindingId];
        try {
            return getHandle(binding.getDepthInformation(views[viewId]), depthInfos);
        } catch(e) {
            return 0;
        }
    };
    
    xr.inputSource_getGamepad = function(inputSourceId) {
        const inputSource = inputSources[inputSourceId];
        return getHandle(inputSource.gamepad, gamepads);
    };
    
    xr.gamepad_getId = function(gamepadId) {
        const gamepad = gamepads[gamepadId];
        return gamepad.id;
    };
    
    xr.gamepad_getIndex = function(gamepadId) {
        const gamepad = gamepads[gamepadId];
        return gamepad.index;
    };
    
    xr.gamepad_getHand = function(gamepadId) {
        const gamepad = gamepads[gamepadId];
        return gamepad.hand;
    };
    xr.gamepad_getMapping = function(gamepadId) {
        const gamepad = gamepads[gamepadId];
        return gamepad.mapping;
    };
    
    xr.gamepad_getButtons = function(gamepadId, buttonStatePtr, buttonStateLen) {
        const gamepad = gamepads[gamepadId];

        const iStates = new Int32Array(Module.HEAPU8.buffer, buttonStatePtr, 3*buttonStateLen);
        const fStates = new Float32Array(Module.HEAPU8.buffer, buttonStatePtr, 3*buttonStateLen);
        
        const total = gamepad.buttons.length;
        const count = buttonStateLen < total ? buttonStateLen : total;
        let o = 0;
        for(let i = 0; i < count; i++) {
            const button = gamepad.buttons[i];
            iStates[o++] = button.pressed ? 1 : 0;
            iStates[o++] = button.touched ? 1 : 0;
            fStates[o++] = button.value;
        }
        
        return total;
    };
    
    xr.makeXRCompatible = async function(contextId) {
        return await new Promise(function (resolve) {
            const res = Module.GL.contexts[contextId].GLctx.makeXRCompatible();
            res.then(function() { resolve(true); }).catch(function() { resolve(false); });
        });
    };
    
    
    
    self.xr = xr;
})(window);