namespace Aardworx.Rendering.WebGL

open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Microsoft.FSharp.NativeInterop
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Aardworx.Rendering.WebGL.Streams
open Aardworx.WebAssembly

#nowarn "9"

[<RequireQualifiedAccess>]
type CommandStreamMode =
    | Managed
    | Debug
    | Javascript


[<AutoOpen>]
module private Converters =
    open Microsoft.FSharp.Quotations
    open Microsoft.FSharp.Quotations.Patterns

    module PrimitiveType =
        let ofIndexedGeometryMode =
            LookupTable.lookup [
                IndexedGeometryMode.PointList, PrimitiveType.Points
                IndexedGeometryMode.LineList, PrimitiveType.Lines
                IndexedGeometryMode.LineStrip, PrimitiveType.LineStrip
                IndexedGeometryMode.LineAdjacencyList, PrimitiveType.LinesAdjacency
                IndexedGeometryMode.TriangleList, PrimitiveType.Triangles
                IndexedGeometryMode.TriangleStrip, PrimitiveType.TriangleStrip
                IndexedGeometryMode.TriangleAdjacencyList, PrimitiveType.TrianglesAdjacency
                IndexedGeometryMode.QuadList, PrimitiveType.Quads
            ]

    module BlendingFactor =
        let ofBlendFactor =
            LookupTable.lookup [
                BlendFactor.Zero, BlendingFactor.Zero
                BlendFactor.One, BlendingFactor.One
                BlendFactor.SourceColor, BlendingFactor.SrcColor
                BlendFactor.InvSourceColor, BlendingFactor.OneMinusSrcColor
                BlendFactor.DestinationColor, BlendingFactor.DstColor
                BlendFactor.InvDestinationColor, BlendingFactor.OneMinusDstColor
                BlendFactor.SourceAlpha, BlendingFactor.SrcAlpha
                BlendFactor.InvSourceAlpha, BlendingFactor.OneMinusSrcAlpha
                BlendFactor.DestinationAlpha, BlendingFactor.DstAlpha
                BlendFactor.InvDestinationAlpha, BlendingFactor.OneMinusDstAlpha
                BlendFactor.ConstantColor, BlendingFactor.ConstantColor
                BlendFactor.InvConstantColor, BlendingFactor.OneMinusConstantColor
                BlendFactor.ConstantAlpha, BlendingFactor.ConstantAlpha
                BlendFactor.InvConstantAlpha, BlendingFactor.OneMinusConstantAlpha
                BlendFactor.SourceAlphaSaturate, BlendingFactor.SrcAlphaSaturate
                BlendFactor.SecondarySourceColor, BlendingFactor.Src1Color
                BlendFactor.InvSecondarySourceColor, BlendingFactor.OneMinusSrc1Color
                BlendFactor.SecondarySourceAlpha, BlendingFactor.Src1Alpha
                BlendFactor.InvSecondarySourceAlpha, BlendingFactor.OneMinusSrc1Alpha
            ]

    module BlendEquationMode =
        let ofBlendOperation =
            LookupTable.lookup [
                BlendOperation.Add, BlendEquationModeEXT.FuncAdd
                BlendOperation.Subtract, BlendEquationModeEXT.FuncSubtract
                BlendOperation.ReverseSubtract, BlendEquationModeEXT.FuncReverseSubtract
                BlendOperation.Minimum, BlendEquationModeEXT.Min
                BlendOperation.Maximum, BlendEquationModeEXT.Max
            ]

    module DepthFunction =
        let ofDepthTest =
            LookupTable.lookup [
                DepthTest.Always, DepthFunction.Always
                DepthTest.Equal, DepthFunction.Equal
                DepthTest.Greater, DepthFunction.Greater
                DepthTest.GreaterOrEqual, DepthFunction.Gequal
                DepthTest.Less, DepthFunction.Less
                DepthTest.LessOrEqual, DepthFunction.Lequal
                DepthTest.Never, DepthFunction.Never
                DepthTest.None, DepthFunction.Always
                DepthTest.NotEqual, DepthFunction.Notequal
            ]

    module StencilFunction =
        let ofComparisonFunction =
            LookupTable.lookup [
                ComparisonFunction.Always, StencilFunction.Always
                ComparisonFunction.Never, StencilFunction.Never
                ComparisonFunction.Less, StencilFunction.Less
                ComparisonFunction.Equal, StencilFunction.Equal
                ComparisonFunction.LessOrEqual, StencilFunction.Lequal
                ComparisonFunction.Greater, StencilFunction.Greater
                ComparisonFunction.GreaterOrEqual, StencilFunction.Gequal
                ComparisonFunction.NotEqual, StencilFunction.Notequal
            ]

    module StencilOp =
        let ofStencilOperation =
            LookupTable.lookup [
                StencilOperation.Keep, StencilOp.Keep
                StencilOperation.Zero, StencilOp.Zero
                StencilOperation.Replace, StencilOp.Replace
                StencilOperation.Increment, StencilOp.Incr
                StencilOperation.IncrementWrap, StencilOp.IncrWrap
                StencilOperation.Decrement, StencilOp.Decr
                StencilOperation.DecrementWrap, StencilOp.DecrWrap
                StencilOperation.Invert, StencilOp.Invert
            ]

    module CullFaceMode =
        let ofCullMode =    
            LookupTable.lookup [
                CullMode.None, unbox<CullFaceMode> 0
                CullMode.Front, CullFaceMode.Back
                CullMode.Back, CullFaceMode.Front
                CullMode.FrontAndBack, CullFaceMode.FrontAndBack
            ]

    module PolygonMode =
        let ofFillMode =
            LookupTable.lookup [
                FillMode.Fill, PolygonMode.Fill
                FillMode.Line, PolygonMode.Line
                FillMode.Point, PolygonMode.Point
            ]

    let offsetOf (e : Expr<'a -> 'b>) : nativeint =
        let rec getOffset (e : Expr) =
            match e with
            | Lambda(_, b) -> getOffset b
            | Sequential(_, b) -> getOffset b
            | Let(_,_,b) -> getOffset b
            | FieldGet(_, f) -> Marshal.OffsetOf(f.DeclaringType, f.Name)
            | PropertyGet(_, p, []) -> 
                try Marshal.OffsetOf(p.DeclaringType, p.Name + "@")
                with _ ->
                    failwith "no offset for Property"
            | _ -> failwith ""
        getOffset e

    let pointerOffset (e : Expr<'a -> 'b>) : aptr<'a> -> aptr<'b> =
        let rec getOffset (e : Expr) =
            match e with
            | Lambda(_, b) -> getOffset b
            | Sequential(_, b) -> getOffset b
            | Let(_,_,b) -> getOffset b
            | FieldGet(_, f) -> Marshal.OffsetOf(f.DeclaringType, f.Name)
            | _ -> failwith ""
        let off = getOffset e
        if off = 0n then 
            APtr.cast
        else
            APtr.cast >> APtr.addBytes (int off)

    module DrawCallInfo =
        let faceVertexCount = pointerOffset <@ fun (c : DrawCallInfo) -> c.FaceVertexCount @>
        let firstInstance = pointerOffset <@ fun (c : DrawCallInfo) -> c.FirstInstance @>
        let firstIndex = pointerOffset <@ fun (c : DrawCallInfo) -> c.FirstIndex @>
        let instanceCount = pointerOffset <@ fun (c : DrawCallInfo) -> c.InstanceCount @>
        let baseVertex = pointerOffset <@ fun (c : DrawCallInfo) -> c.BaseVertex @>


    
[<Struct; StructLayout(LayoutKind.Sequential)>]
type VertexAttribInfo =
    {
        Index : int
        Offset : int
        Size : int
        Stride : int
        Type : GLEnum
        Normalized : int
        Integer : int
        Buffer : uint32
        Divisor : int
    }
        
[<Struct; StructLayout(LayoutKind.Sequential)>]
type VertexBufferBindingInfo =
    {
        IndexType           : DrawElementsType
        IndexOffset         : nativeint
        IndexElementSize    : nativeint
        BindingCount        : int
        Bindings            : nativeptr<VertexAttribInfo>
    }
    
        
[<Struct; StructLayout(LayoutKind.Sequential)>]
type FramebufferInfo =
    {
        ReadFbo         : int
        DrawFbo         : int
        ViewportOffset  : V2i
        ViewportSize    : V2i
    }

type CommandStreamState(device : Device) =
    inherit ResourceBase(device, "CommandStreamState", 0L)

    let mutable vbbInfo = NativePtr.alloc<VertexBufferBindingInfo> 1
    let mutable fboInfo = 
        let ptr = NativePtr.alloc<FramebufferInfo> 1
        NativePtr.write ptr { ReadFbo = 0; DrawFbo = 0; ViewportOffset = V2i.Zero; ViewportSize = V2i.II }
        ptr

    let mutable tmp = NativePtr.alloc<uint32> 128
    let mutable fboSignature = FramebufferSignature.Empty
    let mutable signatureStack = []
    let currentContext  = cval<WebGLContext> null

    static let indexTypeOffset = offsetOf <@ fun (v : VertexBufferBindingInfo) -> v.IndexType @>
    static let indexOffsetOffset = offsetOf <@ fun (v : VertexBufferBindingInfo) -> v.IndexOffset @>
    static let indexElemSizeOffset = offsetOf <@ fun (v : VertexBufferBindingInfo) -> v.IndexElementSize @>
    static let bindingCountOffset = offsetOf <@ fun (v : VertexBufferBindingInfo) -> v.BindingCount @>
    static let bindingsOffset = offsetOf <@ fun (v : VertexBufferBindingInfo) -> v.Bindings @>


    member x.Device = device

    member x.VertexBufferBindingInfo = vbbInfo

    member x.IndexType : nativeptr<DrawElementsType> = NativePtr.ofNativeInt (NativePtr.toNativeInt vbbInfo + indexTypeOffset)
    member x.IndexOffset : nativeptr<nativeint> = NativePtr.ofNativeInt (NativePtr.toNativeInt vbbInfo + indexOffsetOffset)
    member x.IndexElementSize : nativeptr<nativeint> = NativePtr.ofNativeInt (NativePtr.toNativeInt vbbInfo + indexElemSizeOffset)
    member x.BindingCount : nativeptr<int> = NativePtr.ofNativeInt (NativePtr.toNativeInt vbbInfo + bindingCountOffset)
    member x.Bindings : nativeptr<nativeint> = NativePtr.ofNativeInt (NativePtr.toNativeInt vbbInfo + bindingsOffset)
    member x.FramebufferInfo = fboInfo

    member x.TemporaryStorage = tmp
    
    member x.CurrentContext 
        with get() = currentContext.Value
        and set v = transact (fun () -> currentContext.Value <- v)

    member x.CurrentContextVal =
        currentContext :> aval<_>

    member x.FramebufferSignature
        with get() = fboSignature
        and set v = fboSignature <- v

    member x.PushFramebufferSignature() =
        signatureStack <- fboSignature :: signatureStack
        
    member x.PopFramebufferSignature() =
        match signatureStack with
        | h :: t ->
            fboSignature <- h
            signatureStack <- t
            Some h
        | [] ->
            None

    member x.Reset() =
        fboSignature <- FramebufferSignature.Empty
        transact (fun () -> currentContext.Value <- null)

    override x.Free() =
        x.Reset()
        if tmp <> NativePtr.zero then
            NativePtr.free tmp
            NativePtr.free vbbInfo
            vbbInfo <- NativePtr.zero
            tmp <- NativePtr.zero

type internal RefCountingSet<'a>() =
    let store = Dict<'a, ref<int>>()

    member x.Add(value : 'a) =
        let r = store.GetOrCreate(value, fun _ -> ref 0)
        let o = r.Value
        r.Value <- o + 1
        o = 0

    member x.Remove(value : 'a) =
        match store.TryGetValue(value) with
        | true, r ->
            let o = r.Value
            if o = 1 then
                store.Remove value |> ignore
                true
            elif o > 1 then
                r.Value <- o - 1
                false
            else
                store.Remove value |> ignore
                Log.warn "[GL] RefCountingSet inconsistent state"
                false
        | _ ->
            false
            
    member x.Count = store.Count

    member x.Contains(value : 'a) =
        store.ContainsKey value

    member x.Clear() =
        store.Clear()

    member x.ToArray() =
        let arr = Array.zeroCreate store.Count
        let mutable i = 0
        for KeyValue(k, _) in store do
            arr.[i] <- k
            i <- i + 1
        arr

    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = store.Keys.GetEnumerator() :> _
            
    interface System.Collections.Generic.IEnumerable<'a> with
        member x.GetEnumerator() = store.Keys.GetEnumerator() 

        
     
type CommandStream private(state : CommandStreamState, ownState : bool, backend : CommandEncoder) as this =
    inherit AdaptiveObject()
    
    do if not ownState then state.AddReference()
    let device = state.Device
    let mutable refCount = 1


    let updaters = RefCountingSet<aval<unit>>()
    let dirtyUpdaters = RefCountingSet<aval<unit>>()
    let mutable ownBackend = true
    let mutable resources = HashMap.empty<IAdaptiveResource, IAdaptiveValue>

    let getResource (res : IAdaptiveResource<'a>) =
        let mutable result = Unchecked.defaultof<_>
        resources <-
            resources |> HashMap.alterV (res :> IAdaptiveResource) (fun o ->
                match o with
                | ValueSome o ->
                    result <- o
                    ValueSome o
                | ValueNone ->
                    let n = res.Acquire() :> IAdaptiveValue
                    result <- n
                    ValueSome n
            )
                        
        result :?> aval<'a>

    let uniqueId = UniqueId.Alloc "CommandStream"
    let uniqueName = sprintf "CommandStream%03d" uniqueId

    let mutable trackerObj =
        if state.Device.Debug then
            DebugResourceFinalizer(this)
        else
            null

    member x.Acquire(r : ares<'a>) =
        getResource r

    member private x.Resources
        with get() = resources
        and set r = resources <- r
        
    member private x.OwnBackend
        with get() = ownBackend
        and set v = ownBackend <- v

    new(device : Device, backend : CommandEncoder) =
        new CommandStream(new CommandStreamState(device), true, backend)
        
    new(state : CommandStreamState, backend : CommandEncoder) =
        new CommandStream(state, false, backend)

    new(state : CommandStreamState, mode : CommandStreamMode) =
        let backend = 
            match mode with
            | CommandStreamMode.Debug ->
                new DebugCommandEncoder(state.Device) :> CommandEncoder
            | CommandStreamMode.Managed ->
                new ManagedCommandEncoder(state.Device) :> CommandEncoder
            | CommandStreamMode.Javascript ->
                new JSCommandEncoder(state.Device) :> CommandEncoder
        new CommandStream(state, false, backend)

    new(device : Device, mode : CommandStreamMode) = 
        let backend = 
            match mode with
            | CommandStreamMode.Debug ->
                new DebugCommandEncoder(device) :> CommandEncoder
            | CommandStreamMode.Managed ->
                new ManagedCommandEncoder(device) :> CommandEncoder
            | CommandStreamMode.Javascript ->
                new JSCommandEncoder(device) :> CommandEncoder
        new CommandStream(new CommandStreamState(device), true, backend)

    member x.Device : Device = state.Device
    member x.State : CommandStreamState = state

    member x.UniqueName = uniqueName

    member x.IsDisposed =
        lock x (fun () ->
            refCount <= 0
        )

    member x.TryAddReference() =
        lock x (fun () ->
            if refCount > 0 then
                refCount <- refCount + 1
                true
            else
                false
        )

    member x.Dispose() =
        lock x (fun () ->
            let o = refCount
            if o = 1 then
                refCount <- 0
                if not (isNull trackerObj) then
                    trackerObj.Dispose()
                    trackerObj <- null

                for r, _ in resources do r.Release()
                resources <- HashMap.empty
                if ownBackend then
                    backend.Dispose()
                state.Dispose()
                device.ResourceDestroyed(uniqueName)
                UniqueId.Free("CommandStream", uniqueId)
            else
                refCount <- refCount - 1
        )

    // resets the CommandStream to its initial state
    member x.Reset() =
        for r, _ in resources do r.Release()
        resources <- HashMap.empty
        backend.Reset()
        
    member x.Update(action : CommandStream -> 'a) =
        let old = resources
        try
            resources <- HashMap.empty
            backend.Update (fun _ -> action x)
        finally
            for o, _ in old do o.Release()
        
        

    /// the underlying CommandStream
    member x.BaseStream = backend

    /// Starts the recording-mode
    member x.Begin() =
        backend.Begin()

    member x.AddResource d =
        backend.AddTemporaryResource d

    /// Stops the recording-mode
    member x.End() =
        backend.End()

    /// Synchronously executes the CommandStream
    member x.Run(token : AdaptiveToken) =
        device.Run(fun gl ->
            state.CurrentContext <- WebGLContext.Current
            x.Update token
            backend.UnsafeRunSynchronously(gl)
        )
        
    /// Synchronously executes the CommandStream
    member x.Run() =
        x.Run AdaptiveToken.Top
        
        
    /// executes the CommandStream on the current thread.
    /// Note that the GL context is assumed to be current.
    member x.UnsafePerform(gl : GL) =
        //state.CurrentContext <- WebGLContext.Current
        //x.Update(AdaptiveToken.Top)
        backend.UnsafeRunSynchronously(gl)



    member x.Conditional(condition : aval<bool>, action : CommandStream -> unit) =
        if condition.IsConstant then
            if AVal.force condition then
                action x
        else
            backend.If(condition, fun e ->
                use cmd = new CommandStream(state, e)
                cmd.Resources <- resources
                cmd.OwnBackend <- false
                try action cmd
                finally
                    resources <- cmd.Resources
                    cmd.Resources <- HashMap.empty
            )
            
    member x.IfThenElse(condition : aval<bool>, ifTrue : CommandStream -> unit, ifFalse : CommandStream -> unit) =
        if condition.IsConstant then
            if AVal.force condition then
                ifTrue x
            else
                ifFalse x
        else
            let t (e : CommandEncoder) =
                use cmd = new CommandStream(state, e)
                cmd.Resources <- resources
                cmd.OwnBackend <- false
                try ifTrue cmd
                finally 
                    resources <- cmd.Resources
                    cmd.Resources <- HashMap.empty
                
            let f (e : CommandEncoder) =
                use cmd = new CommandStream(state, e)
                cmd.Resources <- resources
                cmd.OwnBackend <- false
                try ifFalse cmd
                finally 
                    resources <- cmd.Resources
                    cmd.Resources <- HashMap.empty
            backend.IfThenElse(condition, t, f)
    

    member internal x.AddUpdater(u : aval<unit>) =
        match backend with
        | :? ImmediateCommandEncoder -> 
            u.GetValue AdaptiveToken.Top
        | _ -> 
            lock dirtyUpdaters (fun () ->
                updaters.Add u |> ignore
                dirtyUpdaters.Add u |> ignore
            )

    member internal x.RemoveUpdater(u : aval<unit>) =
        lock dirtyUpdaters (fun () ->
            updaters.Remove u |> ignore
            dirtyUpdaters.Remove u |> ignore
        )
        
            
    member x.Clear(signature : FramebufferSignature, values : aval<ClearValues>) =
    

        let depthMask = 
            values |> APtr.mapVal (fun c ->
                match c.Depth with
                | Some _ ->
                    match c.Stencil with
                    | Some _ -> 3
                    | None -> 1
                | None ->
                    match c.Stencil with
                    | Some _ -> 2
                    | None -> 0
            )
            
        let perTargetColors =
            signature.ColorAttachments
            |> Map.map (fun _i { Name = sym; Format = fmt } ->
                let isInteger = TextureFormat.isIntegerFormat fmt 
                if isInteger then
                    Choice2Of2 (
                        values |> AVal.map (fun v ->
                            let color = 
                                match Map.tryFind sym v.Colors with
                                | None -> v.Color
                                | c -> c
                            match color with
                            | Some c -> Some c.Integer
                            | None -> None
                        )
                    )
                else
                    Choice1Of2 (
                        values |> AVal.map (fun v ->
                            let color = 
                                match Map.tryFind sym v.Colors with
                                | None -> v.Color
                                | c -> c
                            match color with
                            | Some c -> Some c.Float
                            | None -> None
                        )

                    )

            )
           

        for KeyValue(buffer, color) in perTargetColors do
            match color with
            | Choice1Of2 floatColor ->
                let active = floatColor |> APtr.mapVal (function Some _ -> 1 |  _ -> 0)
                let value = floatColor |> APtr.mapVal (function Some c -> c | _ -> V4f(0.0f, 0.0f, 0.0f, 0.0f)) |> APtr.cast
                backend.Switch(
                    active,
                    [
                        1, fun cmd -> 
                            cmd.ClearBufferfv(APtr.constant BufferKind.Color, APtr.constant buffer, value)
                    ],
                    fun _cmd -> ()
                )
            | Choice2Of2 intColor ->
                let active = intColor |> APtr.mapVal (function Some _ -> 1 |  _ -> 0)
                let value = intColor |> APtr.mapVal (function Some c -> c | _ -> V4i(0, 0, 0, 0)) |> APtr.cast
                backend.Switch(
                    active,
                    [
                        1, fun cmd -> 
                            cmd.ClearBufferiv(APtr.constant BufferKind.Color, APtr.constant buffer, value)
                    ],
                    fun _cmd -> ()
                )


        let depthValue =
            values |> APtr.mapVal (fun d -> match d.Depth with Some v -> float32 v | None -> 0.0f)
            
        let stencilValue =
            values |> APtr.mapVal (fun v -> match v.Stencil with Some v -> int v | None -> 0)

        backend.Switch(
            depthMask,
            [
                1, fun cmd ->
                    cmd.ClearBufferfv(APtr.constant BufferKind.Depth, APtr.constant 0, depthValue)
                2, fun cmd ->
                    cmd.ClearBufferiv(APtr.constant BufferKind.Stencil, APtr.constant 0, stencilValue)
                3, fun cmd ->
                    cmd.ClearBufferfi(APtr.constant (unbox<BufferKind> (int GLEnum.DepthStencil)), APtr.constant 0, depthValue, stencilValue)
            ],
            fun _ -> ()
        )
            

    member x.Clear(values : aval<ClearValues>) =
        x.Clear(state.FramebufferSignature, values)
        

    member x.DrawIndexed(mode : aval<IndexedGeometryMode>, call : DrawCallInfo) =
        let mode = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

        let offset =
            let dst = APtr.temporary 1
            let first = APtr.constant (nativeint call.FirstIndex)
            let elemSize = APtr.ofNativePtr state.IndexElementSize
            let baseOffset = APtr.ofNativePtr state.IndexOffset
            backend.Mad(baseOffset, first, elemSize, dst)
            dst

        let indexType =
            APtr.ofNativePtr state.IndexType

        
        let fvc = uint32 call.FaceVertexCount


        if call.InstanceCount = 1 && call.FirstInstance = 0 then
            if call.BaseVertex = 0 then
                backend.DrawElements(
                    mode, APtr.constant fvc, indexType, offset
                )
            else
                failwith "DrawElementsBaseVertex"
                //backend.DrawElementsBaseVertex(
                //    mode, APtr.constant fvc, indexType, offset, 
                //    APtr.constant call.BaseVertex
                //)
        else
            if call.FirstInstance = 0 then
                if call.BaseVertex = 0 then
                    backend.DrawElementsInstanced(
                        mode, APtr.constant fvc, indexType, offset, 
                        APtr.constant (uint32 call.InstanceCount)
                    )
                else
                    failwith "DrawElementsInstancedBaseVertex"
                    //backend.DrawElementsInstancedBaseVertex(
                    //    mode, APtr.constant fvc, indexType, offset, 
                    //    APtr.constant (uint32 call.InstanceCount), 
                    //    APtr.constant call.BaseVertex
                    //)
            else
                if call.BaseVertex = 0 then
                    failwith "DrawElementsInstancedBaseInstance"
                    //backend.DrawElementsInstancedBaseInstance(
                    //    mode, APtr.constant fvc, APtr.cast indexType, offset,
                    //    APtr.constant (uint32 call.InstanceCount), 
                    //    APtr.constant (uint32 call.FirstInstance)
                    //)
                else
                    failwith "DrawElementsInstancedBaseVertexBaseInstance"
                    //backend.DrawElementsInstancedBaseVertexBaseInstance(
                    //    mode, APtr.constant fvc, APtr.cast indexType, offset,
                    //    APtr.constant (uint32 call.InstanceCount), 
                    //    APtr.constant call.BaseVertex,
                    //    APtr.constant (uint32 call.FirstInstance)
                    //)
    
    member x.DrawIndexed(mode : aval<IndexedGeometryMode>, call : aval<DrawCallInfo>) =
        if call.IsConstant then
            x.DrawIndexed(mode, AVal.force call)
        else
            let ptr = APtr.ofAVal call

            let offset =
                let dst = APtr.temporary 1
                let first = call |> APtr.mapVal (fun c -> nativeint c.FirstIndex)
                let elemSize = APtr.ofNativePtr state.IndexElementSize
                let baseOffset = APtr.ofNativePtr state.IndexOffset
                backend.Mad(baseOffset, first, elemSize, dst)
                dst

            let indexType =
                APtr.ofNativePtr state.IndexType

            let mode = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode
              
            let fvc = DrawCallInfo.faceVertexCount ptr |> APtr.cast<uint32>
            let instanceCount = DrawCallInfo.instanceCount ptr |> APtr.cast<uint32>

            backend.Switch(
                DrawCallInfo.firstInstance ptr,
                [
                    0, fun cmd ->
                        // firstInstance = 0
                        cmd.Switch(
                            DrawCallInfo.baseVertex ptr,
                            [
                                0, fun cmd ->
                                    // baseVertex = 0
                                    cmd.Switch(
                                        DrawCallInfo.instanceCount ptr,
                                        [
                                            1, fun cmd ->
                                                // instanceCount = 1
                                                cmd.DrawElements(mode, fvc, indexType, offset)
                                        ],
                                        fun cmd ->
                                            // instanceCount <> 1
                                            cmd.DrawElementsInstanced(mode, fvc, indexType, offset, instanceCount)
                                    )
                            ],
                            fun cmd ->
                                // baseVertex <> 0
                                cmd.Switch(
                                    DrawCallInfo.instanceCount ptr,
                                    [
                                        1, fun _cmd ->
                                            // instanceCount = 1
                                            // TODO!!!
                                            //cmd.DrawElementsBaseVertex(mode, fvc, indexType, offset, baseVertex)
                                            ()
                                    ],
                                    fun _cmd ->
                                        // instanceCount <> 1
                                        // TODO!!!
                                        //cmd.DrawElementsInstancedBaseVertex(mode, fvc, indexType, offset, instanceCount, baseVertex)
                                        ()
                                )
                                ()
                        )
                ],
                fun cmd ->
                    // firstInstance <> 0
                    cmd.Switch(
                        DrawCallInfo.baseVertex ptr,
                        [
                            0, fun _cmd ->
                                // baseVertex = 0
                                // TODO!!!
                                //cmd.DrawElementsInstancedBaseInstance(mode, fvc, APtr.cast indexType, offset, instanceCount, baseInstance)
                                ()
                        ],
                        fun _cmd ->  
                            // baseVertex <> 0
                            //TODO!!!
                            //cmd.DrawElementsInstancedBaseVertexBaseInstance(mode, fvc, APtr.cast indexType, offset, instanceCount, baseVertex, baseInstance)
                            ()
                    )
            )

    member x.DrawIndexed(mode : IndexedGeometryMode, call : aval<DrawCallInfo>) =
        if call.IsConstant then
            x.DrawIndexed(AVal.constant mode, AVal.force call)
        else
            x.DrawIndexed(AVal.constant mode, call)

    member x.DrawIndexed(mode : IndexedGeometryMode, call : DrawCallInfo) =
        x.DrawIndexed(AVal.constant mode, call)

    
    member x.Draw(mode : IndexedGeometryMode, call : DrawCallInfo) =
        let fvc = uint32 call.FaceVertexCount
        let mode = PrimitiveType.ofIndexedGeometryMode mode

        if call.InstanceCount = 1 && call.FirstInstance = 0 then
            backend.DrawArrays(
                mode, call.FirstIndex, fvc
            )
        else
            if call.FirstInstance = 0 then
                backend.DrawArraysInstanced(
                    mode, call.FirstIndex, fvc,
                    (uint32 call.InstanceCount)
                )
            else
                failwith "DrawArraysInstancedBaseInstance"

    member x.Draw(mode : aval<IndexedGeometryMode>, call : DrawCallInfo) =
        if mode.IsConstant then
            x.Draw(AVal.force mode, call)
        else
            let mode = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

            let fvc = uint32 call.FaceVertexCount

            if call.InstanceCount = 1 && call.FirstInstance = 0 then
                backend.DrawArrays(
                    mode, APtr.constant call.FirstIndex, APtr.constant fvc
                )
            else
                if call.FirstInstance = 0 then
                    backend.DrawArraysInstanced(
                        mode, APtr.constant call.FirstIndex, APtr.constant fvc,
                        APtr.constant (uint32 call.InstanceCount)
                    )
                else
                    failwith "DrawArraysInstancedBaseInstance"
        
    member x.Draw(mode : IndexedGeometryMode, call : aval<DrawCallInfo>) =
        if call.IsConstant then
            x.Draw(mode, AVal.force call)
        else
            let mode = PrimitiveType.ofIndexedGeometryMode mode
            let ptr = APtr.ofAVal call

            let fvc = DrawCallInfo.faceVertexCount ptr |> APtr.cast<uint32>
            let instanceCount = DrawCallInfo.instanceCount ptr |> APtr.cast<uint32>
            //let baseInstance = DrawCallInfo.firstInstance ptr |> APtr.cast<uint32>

            backend.Switch(
                DrawCallInfo.firstInstance ptr,
                [
                    0, fun cmd ->
                        cmd.Switch(
                            DrawCallInfo.instanceCount ptr,
                            [
                                1, fun cmd ->
                                    cmd.DrawArrays(APtr.constant mode, DrawCallInfo.firstIndex ptr, fvc)
                            ],
                            fun cmd ->
                                cmd.DrawArraysInstanced(APtr.constant mode, DrawCallInfo.firstIndex ptr, fvc, instanceCount)
                        )
                ],
                fun _cmd ->
                    // TODO!!!
                    //cmd.DrawArraysInstancedBaseInstance(mode, DrawCallInfo.firstIndex ptr, fvc, instanceCount, baseInstance)
                    ()
            )
  
    member x.Draw(mode : aval<IndexedGeometryMode>, call : aval<DrawCallInfo>) =    
        if mode.IsConstant then
            x.Draw(AVal.force mode, call)
        else
            let ptr = APtr.ofAVal call
            let mode = mode |> APtr.mapVal PrimitiveType.ofIndexedGeometryMode

            let fvc = DrawCallInfo.faceVertexCount ptr |> APtr.cast<uint32>
            let instanceCount = DrawCallInfo.instanceCount ptr |> APtr.cast<uint32>
            //let baseInstance = DrawCallInfo.firstInstance ptr |> APtr.cast<uint32>

            backend.Switch(
                DrawCallInfo.firstInstance ptr,
                [
                    0, fun cmd ->
                        cmd.Switch(
                            DrawCallInfo.instanceCount ptr,
                            [
                                1, fun cmd ->
                                    cmd.DrawArrays(mode, DrawCallInfo.firstIndex ptr, fvc)
                            ],
                            fun cmd ->
                                cmd.DrawArraysInstanced(mode, DrawCallInfo.firstIndex ptr, fvc, instanceCount)
                        )
                ],
                fun _cmd ->
                    // TODO!!!
                    //cmd.DrawArraysInstancedBaseInstance(mode, DrawCallInfo.firstIndex ptr, fvc, instanceCount, baseInstance)
                    ()
            )


    member x.SetBlendColor(blendColor : C4f) =
        backend.BlendColor(blendColor.R, blendColor.G, blendColor.B, blendColor.A)

    member x.SetBlendColor(blendColor : aval<C4f>) =
        if blendColor.IsConstant then
            let blendColor = AVal.force blendColor
            backend.BlendColor(blendColor.R, blendColor.G, blendColor.B, blendColor.A)
        else
            let r = blendColor |> APtr.mapVal (fun c -> c.R)
            let g = blendColor |> APtr.mapVal (fun c -> c.G)
            let b = blendColor |> APtr.mapVal (fun c -> c.B)
            let a = blendColor |> APtr.mapVal (fun c -> c.A)
            backend.BlendColor(r, g, b, a)

    member x.SetBlendMode(mode : BlendMode) =
        if mode.Enabled then
            backend.Enable(EnableCap.Blend)
            backend.BlendFuncSeparate(
                BlendingFactor.ofBlendFactor mode.SourceColorFactor,
                BlendingFactor.ofBlendFactor mode.DestinationColorFactor,
                BlendingFactor.ofBlendFactor mode.SourceAlphaFactor,
                BlendingFactor.ofBlendFactor mode.DestinationAlphaFactor
            )
            backend.BlendEquationSeparate(
                BlendEquationMode.ofBlendOperation mode.ColorOperation,
                BlendEquationMode.ofBlendOperation mode.AlphaOperation
            )
        else
            backend.Disable(EnableCap.Blend)
        
    member x.SetBlendMode(mode : aval<BlendMode>) =
        if mode.IsConstant then
            x.SetBlendMode(AVal.force mode)
        else
            let enabled = mode |> APtr.mapVal (fun m -> if m.Enabled then 1 else 0)
        
            let srcColor = mode |> APtr.mapVal (fun mode -> BlendingFactor.ofBlendFactor mode.SourceColorFactor)
            let dstColor = mode |> APtr.mapVal (fun mode -> BlendingFactor.ofBlendFactor mode.DestinationColorFactor)
            let srcAlpha = mode |> APtr.mapVal (fun mode -> BlendingFactor.ofBlendFactor mode.SourceAlphaFactor)
            let dstAlpha = mode |> APtr.mapVal (fun mode -> BlendingFactor.ofBlendFactor mode.DestinationAlphaFactor)
        
            let colorOp = mode |> APtr.mapVal (fun mode -> BlendEquationMode.ofBlendOperation mode.ColorOperation)
            let alphaOp = mode |> APtr.mapVal (fun mode -> BlendEquationMode.ofBlendOperation mode.AlphaOperation)

            backend.Switch(
                enabled,
                [
                    1, fun cmd -> 
                        cmd.Enable(EnableCap.Blend)
                        cmd.BlendFuncSeparate(
                            srcColor, dstColor,
                            srcAlpha, dstAlpha
                        )
                        cmd.BlendEquationSeparate(
                            colorOp, alphaOp
                        )
                ],
                fun cmd ->
                    cmd.Disable(EnableCap.Blend)
            )
       
    member x.SetColorMask(mask : ColorMask) =
        let r = if mask.HasFlag ColorMask.Red then Boolean.True else Boolean.False
        let g = if mask.HasFlag ColorMask.Green then Boolean.True else Boolean.False
        let b = if mask.HasFlag ColorMask.Blue then Boolean.True else Boolean.False
        let a = if mask.HasFlag ColorMask.Alpha then Boolean.True else Boolean.False
        backend.ColorMask(r, g, b, a)

    member x.SetColorMask(mask : aval<ColorMask>) =
        if mask.IsConstant then
            x.SetColorMask(AVal.force mask)
        else
            let r = mask |> APtr.mapVal (fun m -> if m.HasFlag ColorMask.Red then Boolean.True else Boolean.False)
            let g = mask |> APtr.mapVal (fun m -> if m.HasFlag ColorMask.Green then Boolean.True else Boolean.False)
            let b = mask |> APtr.mapVal (fun m -> if m.HasFlag ColorMask.Blue then Boolean.True else Boolean.False)
            let a = mask |> APtr.mapVal (fun m -> if m.HasFlag ColorMask.Alpha then Boolean.True else Boolean.False)
            backend.ColorMask(r, g, b, a)

    member x.SetBlendState(blendState : BlendState) =
        let modes =
            state.FramebufferSignature.AttachmentIndices |> Map.map (fun name _idx ->
                blendState.AttachmentMode |> AVal.bind (fun map ->
                    match Map.tryFind name map with
                    | Some mode -> AVal.constant mode
                    | None -> blendState.Mode
                )
            )

        let allModesEqual() =
            if modes |> Map.forall (fun _ m -> m.IsConstant) then
                let modes = modes |> Map.map (fun _ m -> AVal.force m)
                if Map.isEmpty modes then
                    true
                else
                    use e = (modes :> seq<_>).GetEnumerator()
                    e.MoveNext() |> ignore
                    let mutable eq = true
                    let m = e.Current.Value
                    while eq && e.MoveNext() do
                        if e.Current.Value <> m then
                            eq <- false
                    eq

            else
                false

        // BlendMode
        if state.FramebufferSignature.AttachmentCount = 1 || allModesEqual() then
            match Seq.tryHead modes with
            | Some (KeyValue(_, mode)) ->
                x.SetBlendMode(mode)
            | None ->
                x.SetBlendMode BlendMode.None
        else
            failwith "[WebGL] no support for per-attachment blending"
                
        // BlendColor
        x.SetBlendColor(blendState.ConstantColor)

        if state.FramebufferSignature.AttachmentCount = 1 then
            // WriteMasks
            x.SetColorMask(blendState.ColorWriteMask)




    member x.SetDepthTestMode(mode : DepthTest) =
        if mode <> DepthTest.None then
            backend.Enable(EnableCap.DepthTest)
            backend.DepthFunc(DepthFunction.ofDepthTest mode)
        else
            backend.Disable(EnableCap.DepthTest)

    member x.SetDepthTestMode(mode : aval<DepthTest>) =
        if mode.IsConstant then
            x.SetDepthTestMode(AVal.force mode)
        else
            let enabled = mode |> APtr.mapVal (function DepthTest.None -> 0 | _ -> 1)
            let func = mode |> APtr.mapVal DepthFunction.ofDepthTest

            backend.Switch(
                enabled,
                [
                    1, fun cmd ->
                        cmd.Enable EnableCap.DepthTest
                        cmd.DepthFunc func
                ],
                fun cmd ->
                    cmd.Disable EnableCap.DepthTest
            )
        
    member x.SetDepthBias(bias : DepthBias) =
        if bias.Enabled then
            backend.Enable(EnableCap.PolygonOffsetFill)
            if bias.Clamped then
                failwith "glPolygonOffsetClamp not available"
            else
                backend.PolygonOffset(float32 bias.SlopeScale, float32 bias.Constant)
        else
            backend.Disable(EnableCap.PolygonOffsetFill)
            
    member x.SetDepthBias(bias : aval<DepthBias>) = 
        if bias.IsConstant then
            x.SetDepthBias(AVal.force bias)
        else
            let biasEnabled = bias |> APtr.mapVal (fun b -> if b.Enabled then 1 else 0)
            backend.Switch(
                biasEnabled,
                [
                    1, fun cmd ->
                        let c = bias |> APtr.mapVal (fun b -> float32 b.Constant)
                        let s = bias |> APtr.mapVal (fun b -> float32 b.SlopeScale)

                        cmd.Enable(EnableCap.PolygonOffsetFill)
                        cmd.PolygonOffset(s, c)
                ],
                fun cmd ->
                    cmd.Disable(EnableCap.PolygonOffsetFill)
            )

    member x.SetDepthClamp(_clamp : bool) =
        ()
        //if clamp then backend.Enable(EnableCap.DepthClamp)
        //else backend.Disable(EnableCap.DepthClamp)

    member x.SetDepthClamp(_clamp : aval<bool>) =
        ()
        //if clamp.IsConstant then
        //    x.SetDepthClamp (AVal.force clamp)
        //else
        //    let p = clamp |> APtr.mapVal (function true -> 1 | _ -> 0)
        //    backend.Switch(
        //        p, 
        //        [ 
        //            1, fun cmd ->
        //                cmd.Enable(EnableCap.DepthClamp)
        //        ], fun cmd ->
        //            cmd.Disable(EnableCap.DepthClamp)
        //    )
        
    member x.SetDepthMask(mask : bool) =
        backend.DepthMask (if mask then Boolean.True else Boolean.False)
        
    member x.SetDepthMask(mask : aval<bool>) =
        if mask.IsConstant then
            x.SetDepthMask(AVal.force mask)
        else
            let ptr = mask |> APtr.mapVal (function true -> Boolean.True | _ -> Boolean.False)
            backend.DepthMask ptr

    member x.SetDepthState(mode : DepthState) =
        x.SetDepthTestMode mode.Test
        x.SetDepthBias mode.Bias
        x.SetDepthClamp mode.Clamp
        x.SetDepthMask mode.WriteMask


    member private x.SetStencilFaceMode(face : StencilFaceDirection, mode : StencilMode) =
        if mode.Enabled then
            backend.StencilFuncSeparate(
                face, 
                StencilFunction.ofComparisonFunction mode.Comparison, 
                mode.Reference, 
                mode.CompareMask.Value
            )
            backend.StencilOpSeparate(
                face,
                StencilOp.ofStencilOperation mode.Fail,
                StencilOp.ofStencilOperation mode.DepthFail,
                StencilOp.ofStencilOperation mode.Pass
            )
        else
            backend.StencilFuncSeparate(face, StencilFunction.Always, 0, 0u)
            backend.StencilOpSeparate(face, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep)

    member private x.SetStencilFaceMode(face : StencilFaceDirection, mode : aval<StencilMode>) =
        if mode.IsConstant then
            x.SetStencilFaceMode(face, AVal.force mode)
        else
            let pCmp = 
                mode |> APtr.mapVal (fun m -> 
                    if m.Enabled then
                        m.Comparison |> StencilFunction.ofComparisonFunction
                    else
                        StencilFunction.Always
                )
            let pRef = mode |> APtr.mapVal (fun m -> m.Reference)
            let pMask = mode |> APtr.mapVal (fun m -> m.CompareMask.Value)

            let pSFail = 
                mode |> APtr.mapVal (fun m -> 
                    if m.Enabled then StencilOp.ofStencilOperation m.Fail
                    else StencilOp.Keep
                )
            let pDFail = 
                mode |> APtr.mapVal (fun m -> 
                    if m.Enabled then StencilOp.ofStencilOperation m.DepthFail
                    else StencilOp.Keep
                )
            let pPass = 
                mode |> APtr.mapVal (fun m -> 
                    if m.Enabled then StencilOp.ofStencilOperation m.Pass
                    else StencilOp.Keep
                )

            backend.StencilFuncSeparate(
                APtr.constant face, 
                pCmp, pRef, pMask
            )
            backend.StencilOpSeparate(
                APtr.constant face,
                pSFail, pDFail, pPass
            )

    member private x.SetStencilFaceMask(face : StencilFaceDirection, mask : StencilMask) =
        backend.StencilMaskSeparate(face, mask.Value)
        
    member private x.SetStencilFaceMask(face : StencilFaceDirection, mask : aval<StencilMask>) =
        if mask.IsConstant then
            x.SetStencilFaceMask(face, AVal.force mask)
        else
            let pMask = mask |> APtr.mapVal (fun m -> m.Value)
            backend.StencilMaskSeparate(APtr.constant face, pMask)

    member x.SetStencilMode(front : StencilMode, back : StencilMode) =
        if front.Enabled || back.Enabled then
            backend.Enable EnableCap.StencilTest
            x.SetStencilFaceMode(StencilFaceDirection.Front, front)
            x.SetStencilFaceMode(StencilFaceDirection.Back, back)
        else
            backend.Disable EnableCap.StencilTest
            
    member x.SetStencilMode(front : aval<StencilMode>, back : aval<StencilMode>) =
        if front.IsConstant && back.IsConstant then
            x.SetStencilMode(AVal.force front, AVal.force back)

        elif front.IsConstant then
            let front = AVal.force front
            if front.Enabled then
                backend.Enable EnableCap.StencilTest
                x.SetStencilFaceMode(StencilFaceDirection.Front, front)
                x.SetStencilFaceMode(StencilFaceDirection.Back, back)   
            else
                let enable (cmd : CommandStream) =
                    cmd.BaseStream.Enable EnableCap.StencilTest
                    cmd.SetStencilFaceMode(StencilFaceDirection.Front, front)
                    cmd.SetStencilFaceMode(StencilFaceDirection.Back, back)
                    
                let disable (cmd : CommandStream) =
                    cmd.BaseStream.Disable EnableCap.StencilTest

                let backEnabled = back |> AVal.mapNonAdaptive (fun m -> m.Enabled)
                x.IfThenElse(backEnabled, enable, disable)
        elif back.IsConstant then
            let back = AVal.force back
            if back.Enabled then
                backend.Enable EnableCap.StencilTest
                x.SetStencilFaceMode(StencilFaceDirection.Front, front)
                x.SetStencilFaceMode(StencilFaceDirection.Back, back)   
            else
                let enable (cmd : CommandStream) =
                    cmd.BaseStream.Enable EnableCap.StencilTest
                    cmd.SetStencilFaceMode(StencilFaceDirection.Back, front)
                    cmd.SetStencilFaceMode(StencilFaceDirection.Front, front)
                    
                let disable (cmd : CommandStream) =
                    cmd.BaseStream.Disable EnableCap.StencilTest

                let frontEnabled = front |> AVal.mapNonAdaptive (fun m -> m.Enabled)
                x.IfThenElse(frontEnabled, enable, disable)
        else
            let enable (cmd : CommandStream) =
                cmd.BaseStream.Enable EnableCap.StencilTest
                cmd.SetStencilFaceMode(StencilFaceDirection.Back, front)
                cmd.SetStencilFaceMode(StencilFaceDirection.Front, front)
                    
            let disable (cmd : CommandStream) =
                cmd.BaseStream.Disable EnableCap.StencilTest

            let enabled = 
                AVal.logicalOr [
                    front |> AVal.mapNonAdaptive (fun m -> m.Enabled)
                    back |> AVal.mapNonAdaptive (fun m -> m.Enabled)
                ]
            x.IfThenElse(enabled, enable, disable)

    member x.SetStencilWriteMask(front : StencilMask, back : StencilMask) =
        x.SetStencilFaceMask(StencilFaceDirection.Front, front)
        x.SetStencilFaceMask(StencilFaceDirection.Back, back)
        
    member x.SetStencilWriteMask(front : aval<StencilMask>, back : aval<StencilMask>) =
        x.SetStencilFaceMask(StencilFaceDirection.Front, front)
        x.SetStencilFaceMask(StencilFaceDirection.Back, back)

    member x.SetStencilState(mode : StencilState) =
        x.SetStencilMode(mode.ModeFront, mode.ModeBack)
        x.SetStencilWriteMask(mode.WriteMaskFront, mode.WriteMaskBack)


    member x.SetCullMode(mode : CullMode) =
        match mode with
        | CullMode.None ->
            backend.Disable EnableCap.CullFace
        | _ ->
            backend.Enable EnableCap.CullFace
            backend.CullFace (CullFaceMode.ofCullMode mode)
            
    member x.SetCullMode(mode : aval<CullMode>) =
        if mode.IsConstant then
            x.SetCullMode(AVal.force mode)
        else
            let pEnabled = mode |> APtr.mapVal (function CullMode.None -> 0 | _ -> 1)
            backend.Switch(
                pEnabled,
                [1, fun cmd ->
                    cmd.Enable EnableCap.CullFace
                    cmd.CullFace (APtr.mapVal CullFaceMode.ofCullMode mode)
                
                ],
                fun cmd ->
                    cmd.Disable EnableCap.CullFace
            )

    member x.SetFrontFace(winding : WindingOrder) =
        backend.FrontFace(
            match winding with
            | WindingOrder.Clockwise -> FrontFaceDirection.CW
            | _ -> FrontFaceDirection.Ccw
        )

    member x.SetFrontFace(winding : aval<WindingOrder>) =
        if winding.IsConstant then
            x.SetFrontFace (AVal.force winding)
        else
            let pFrontFace =
                winding |> APtr.mapVal (function
                    | WindingOrder.Clockwise -> FrontFaceDirection.CW
                    | _ -> FrontFaceDirection.Ccw
                )
            backend.FrontFace pFrontFace

    member x.SetMultisample (_state : bool) =
        ()
        //if state then backend.Enable EnableCap.Multisample
        //else backend.Disable EnableCap.Multisample
        
    member x.SetMultisample (_state : aval<bool>) =
        ()
        //if state.IsConstant then
        //    x.SetMultisample (AVal.force state)
        //else
        //    backend.Set(EnableCap.Multisample, state)

    member x.SetRasterizerState(state : RasterizerState) =
        x.SetFrontFace state.FrontFacing
        x.SetCullMode state.CullMode
        x.SetMultisample state.Multisample
        if not state.ConservativeRaster.IsConstant || AVal.force state.ConservativeRaster then
            Log.warn "ConservativeRaster not supported"


    override x.InputChangedObject(_t,o) =
        match o with
        | :? aval<unit> as o when updaters.Contains o ->
            lock dirtyUpdaters (fun () ->
                dirtyUpdaters.Add o |> ignore
            )
        | _ ->
            ()

    member x.UpdateDirty(token : AdaptiveToken) =
        x.EvaluateAlways token (fun token -> 
            let dirty = 
                lock dirtyUpdaters (fun () ->
                    let d = dirtyUpdaters.ToArray()
                    dirtyUpdaters.Clear()
                    d
                )
            for d in dirty do d.GetValue token
        )
  
    /// Updates all `aptr` arguments bound
    member x.Update(token : AdaptiveToken) =
        device.Run(fun _gl ->
            state.CurrentContext <- WebGLContext.Current
            x.EvaluateAlways token (fun token -> 
                backend.Update token

                let dirty =
                    lock dirtyUpdaters (fun () ->
                        let d = dirtyUpdaters.ToArray()
                        dirtyUpdaters.Clear()
                        d
                    )
                for d in dirty do
                    d.GetValue token
            )
        )

    interface System.IDisposable with
        member x.Dispose() = x.Dispose()

    interface Aardworx.Rendering.WebGL.IResource with
        member x.Device = x.Device
        member x.IsDisposed = x.IsDisposed
        member x.UniqueName = x.UniqueName
        member x.TryAddReference() = x.TryAddReference()

[<AbstractClass; Sealed; Extension>]
type DeviceCommandStreamExtensions private() =

    [<Extension>]
    static member CreateCommandStream (device : Device, mode : CommandStreamMode) =
        new CommandStream(device, mode)

    [<Extension>]
    static member RunCommand (device : Device, action : CommandStream -> 'a) =
        device.Run(fun gl ->
            use c = new ImmediateCommandEncoder(device, gl)
            use s = new CommandStream(device, c)
            s.State.CurrentContext <- WebGLContext.Current
            s.Begin()
            try action s
            finally s.End()
        )