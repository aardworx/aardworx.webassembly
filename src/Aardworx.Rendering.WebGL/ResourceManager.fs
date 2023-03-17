namespace Aardworx.Rendering.WebGL

open System
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"


[<AutoOpen>]
module private Caches =

    type private ConstantTester<'a> private() =
        static let isConstant =
            let aval = typeof<'a>.GetInterface typedefof<IAdaptiveValue>.FullName
            if isNull aval then fun _ -> false
            else fun (v : 'a) ->
                let v = v :> obj :?> IAdaptiveValue
                v.IsConstant

        static member IsConstant(value : 'a) =
            isConstant value


    type Cache<'a, 'b when 'a : not struct and 'b : not struct>() =
        let table = new ConditionalWeakTable<'a, 'b>()
        let constants = ConcurrentDict(Dict<'a, 'b>())

        member x.GetOrCreate(input : 'a, creator : 'a -> 'b) =  
            if ConstantTester.IsConstant input then
                constants.GetOrCreate(input, System.Func<_,_>(creator))
            else
                let result = 
                    lock table (fun () ->
                        match table.TryGetValue(input) with
                        | true, res -> ValueSome res
                        | _ -> ValueNone
                    )
                match result with
                | ValueSome res -> res
                | ValueNone ->
                    let res = creator input
                    lock table (fun () ->
                        match table.TryGetValue input with
                        | true, otherResult ->
                            // another thread created result
                            otherResult
                        | _ ->
                            table.Add(input, res)
                            res

                    )

        member x.Remove(input : 'a) =
            if ConstantTester.IsConstant input then
                constants.Remove input |> ignore
            else
                lock table (fun () ->
                    table.Remove input |> ignore
                )
                
    type CacheV<'a, 'b, 'c when 'a : not struct>() =
        let table = ConditionalWeakTable<'a, ConcurrentDict<'b, 'c>>()
        let constants = ConcurrentDict<struct('a * 'b), 'c>(Dict())

        member x.GetOrCreate(a : 'a, b : 'b, creator : 'a -> 'b -> 'c) =  
            if ConstantTester.IsConstant a then
                constants.GetOrCreate(struct(a,b), fun struct(a,b) -> creator a b)
            else
                let result = 
                    lock table (fun () ->
                        match table.TryGetValue a with
                        | true, tb -> 
                            match tb.TryGetValue b with
                            | true, res -> ValueSome res
                            | _ -> ValueNone
                        | _ -> 
                            ValueNone
                    )
                match result with
                | ValueSome res -> res
                | ValueNone ->
                    let res = creator a b
                    lock table (fun () ->
                        match table.TryGetValue a with
                        | true, tb ->
                            match tb.TryGetValue b with
                            | true, otherResult ->
                                // another thread created result
                                otherResult
                            | _ ->
                                tb.Add(b, res)
                                res
                            
                        | _ ->
                            let tb = new ConcurrentDict<'b, 'c>(Dict())
                            table.Add(a, tb)
                            tb.Add(b, res)
                            res

                    )

        member x.Remove(a : 'a, b : 'b) =
            if ConstantTester.IsConstant a then
                constants.Remove struct(a,b) |> ignore
            else
                lock table (fun () ->
                    match table.TryGetValue a with
                    | true, tb ->
                        tb.Remove b |> ignore
                        if tb.Count = 0 then table.Remove a |> ignore
                    | _ ->
                        ()
                )

    type Cache<'a, 'b, 'c when 'a : not struct and 'b : not struct and 'c : not struct>() =
        let table = ConditionalWeakTable<'a, ConditionalWeakTable<'b, 'c>>()

        member x.GetOrCreate(a : 'a, b : 'b, creator : 'a -> 'b -> 'c) =  
            let result = 
                lock table (fun () ->
                    match table.TryGetValue a with
                    | true, tb -> 
                        match tb.TryGetValue b with
                        | true, res -> ValueSome res
                        | _ -> ValueNone
                    | _ -> 
                        ValueNone
                )
            match result with
            | ValueSome res -> res
            | ValueNone ->
                let res = creator a b
                lock table (fun () ->
                    match table.TryGetValue a with
                    | true, tb ->
                        match tb.TryGetValue b with
                        | true, otherResult ->
                            // another thread created result
                            otherResult
                        | _ ->
                            tb.Add(b, res)
                            res
                            
                    | _ ->
                        let tb = new ConditionalWeakTable<'b, 'c>()
                        table.Add(a, tb)
                        tb.Add(b, res)
                        res

                )

        member x.Remove(a : 'a, b : 'b) =
            lock table (fun () ->
                match table.TryGetValue a with
                | true, tb ->
                    tb.Remove b |> ignore
                | _ ->
                    ()
            )



type ResourceManager(device : Device) =
    
    let bufferCache                 = CacheV<IAdaptiveValue, BufferUsage, ares<Buffer>>()
    let vertexBufferBindingCache    = Dict<struct (Map<int, BindingFrequency * BufferView> * option<BufferView>), ares<VertexBufferBinding>>()
    let textureCache                = Cache<IAdaptiveValue, ares<Texture>>()
    let samplerCache                = Cache<aval<SamplerState>, ares<Sampler>>()

    member x.Device = device

    member x.CreateProgram(signature : FramebufferSignature, effect : FShade.Effect) =
        device.CreateProgram(effect, signature)

    member x.CreateBuffer<'a when 'a :> IBuffer>(data : aval<'a>, usage : BufferUsage) =
        bufferCache.GetOrCreate(data, usage, fun data usage ->
            match data with
            | :? Aardvark.Rendering.IAdaptiveResource<'a> as res ->
                let mutable lastBuffer : option<Buffer> = None

                let create() =
                    res.Acquire()
                    res |> AVal.map (fun d -> 
                        let b = device.CreateBuffer(d, usage)
                        match lastBuffer with
                        | Some l -> l.Dispose()
                        | None -> ()
                        lastBuffer <- Some b
                        b
                    )

                let destroy _ =
                    bufferCache.Remove(data, usage)
                    match lastBuffer with
                    | Some b -> 
                        b.Dispose()
                        lastBuffer <- None
                    | None ->
                        ()
                    res.Release()

                ARes.ofCreateDestroy create destroy
            | _ ->
                data :?> aval<'a> |> ARes.mapVal
                    (fun d -> device.CreateBuffer(d, usage))
                    (fun b -> b.Dispose(); bufferCache.Remove(data, usage))
        )

    member x.CreateVertexBufferBinding(index : option<BufferView>, buffers : Map<int, BindingFrequency * BufferView>) =
        vertexBufferBindingCache.GetOrCreate(struct(buffers, index), fun struct(buffers, index) ->
            let ib = 
                index |> Option.map (fun data -> 
                    x.CreateBuffer(data.Buffer, BufferUsage.Index) |> ARes.map (fun b ->
                        { Buffer = b.[data.Offset .. ]; Type = data.ElementType }
                    )
                )

            let vbb =
                buffers |> Map.map (fun _ (freq, view) ->
                    x.CreateBuffer(view.Buffer, BufferUsage.Vertex) |> ARes.map (fun b ->

                        let normalized =
                            view.ElementType = typeof<C4b> ||
                            view.ElementType = typeof<C3b>

                        {
                            Buffer = b.[view.Offset .. ]
                            Type = view.ElementType
                            Stride = if view.Stride = 0 then Marshal.SizeOf view.ElementType else view.Stride
                            Normalized = normalized
                            Frequency = if view.IsSingleValue then BindingFrequency.Instances Int32.MaxValue else freq
                        }
                    )
                )

            let mutable last : option<VertexBufferBinding> = None

            let create() =
                let index = 
                    match ib with
                    | Some ib -> Some (ib.Acquire())
                    | None -> None
                
                let vbb =
                    vbb |> Map.map (fun _ b -> b.Acquire())

                AVal.custom (fun t ->
                    let idx = index |> Option.map (fun v -> v.GetValue t)
                    let vbb = vbb |> Map.map (fun _ v -> v.GetValue t)
                    match last with
                    | Some l -> l.Dispose()
                    | None -> ()
                    let h = device.CreateVertexBufferBinding(vbb, idx)
                    last <- Some h
                    h
                )

            let destroy _ =
                vertexBufferBindingCache.Remove struct(buffers, index) |> ignore
                match last with
                | Some l -> l.Dispose()
                | None -> ()
                last <- None
            
                match ib with
                | Some ib -> ib.Release()
                | None -> ()

                vbb |> Map.iter (fun _ v -> v.Release())



            ARes.ofCreateDestroy create destroy
        )
        
    member x.CreateUniformBuffer(block : FShade.GLSL.GLSLUniformBuffer, tryGetUniform : string -> voption<IAdaptiveValue>) =
        device.CreateUniformBuffer(block, tryGetUniform)

    member x.CreateTexture<'a when 'a :> ITexture>(tex : aval<'a>) =
        textureCache.GetOrCreate(tex, fun tex ->
            let old : ref<option<'a * Texture>> = ref None

            let res =
                match tex with
                | :? Aardvark.Rendering.IAdaptiveResource as r -> Some r
                | _ -> None

            let create() =
                res |> Option.iter (fun r -> r.Acquire())
                let tex = tex :?> aval<'a>
                tex |> AVal.map (fun value ->
                    match old.Value with
                    | Some (ot, h) when Unchecked.equals ot value ->
                        h
                    | ov ->
                        match ov with
                        | Some(_, o) -> o.Dispose()
                        | None -> ()

                        let tex = device.CreateTexture value
                        old.Value <- Some(value, tex)
                        tex
                )

            let destroy _ =
                res |> Option.iter (fun r -> r.Release())
                textureCache.Remove tex
                match old.Value with
                | Some (_, t) ->
                    t.Dispose()
                    old.Value <- None
                | None ->
                    ()

            ARes.ofCreateDestroy create destroy
        )
     
    member x.CreateSampler(sam : aval<SamplerState>) =
        samplerCache.GetOrCreate(sam, fun sam ->
            sam |> ARes.mapVal device.CreateSampler (fun s -> s.Dispose())
        )

[<AbstractClass; Sealed; Extension>]
type RenderObjectResourceManagerExtensions private() =
    [<Extension>]
    static member PrepareRenderObject(this : ResourceManager, fbo : FramebufferSignature, o : RenderObject) =
        let sub = o.Activate()
        let program, instancedGS, outputVertices, mode, offsets, stride =
            match o.Surface with
            | Surface.FShadeSimple effect ->
                this.CreateProgram(fbo, effect), false, 0, o.Mode, [||], 0
                // match effect.GeometryShader with
                // | Some gs ->
                //     match FShade.Effect.tryReplaceGeometry effect with
                //     | Some e -> 
                //         let vertices = 
                //             match gs.shaderOutputVertices with
                //             | FShade.ShaderOutputVertices.Computed a -> a
                //             | FShade.ShaderOutputVertices.UserGiven a -> a
                //             | _ -> failf "unknown outputvertices"
                //
                //         let mode =
                //             match gs.shaderOutputTopology with
                //             | Some FShade.OutputTopology.Points -> IndexedGeometryMode.PointList
                //             | Some FShade.OutputTopology.LineStrip -> IndexedGeometryMode.LineStrip
                //             | Some FShade.OutputTopology.TriangleStrip -> IndexedGeometryMode.TriangleStrip
                //             | None -> failf "no GS outputTopology"
                //
                //         let offsets, stride = 
                //             match o.Mode with
                //             | IndexedGeometryMode.PointList -> [|0|], 1
                //             | IndexedGeometryMode.LineList -> [|0;1|], 2
                //             | IndexedGeometryMode.LineStrip -> [|0;1|], 1
                //             | mode -> failf "bad GS mode: %A" mode
                //
                //         if Option.isSome o.Indices then
                //             failf "indexed GS simulation not implemented"
                //
                //         this.CreateProgram(fbo, e), true, vertices, mode, offsets, stride
                //     | None ->
                //         failf "could not simulate GeometryShader"
                // | None ->
                //     this.CreateProgram(fbo, effect), false, 0, o.Mode, [||], 0

            | Surface.Backend (:? Program as p) ->
                p, false, 0, o.Mode, [||], 0
            | s ->
                failf "bad surface: %A" s
            
        

        let buffers =
            let rx = System.Text.RegularExpressions.Regex @"([a-zA-Z0-9]+)_([0-9]+)"
            program.Interface.inputs |> Seq.choose (fun ip ->
                let inputName, index = 
                    let m = rx.Match ip.paramSemantic
                    if m.Success then m.Groups.[1].Value, Some (int m.Groups.[2].Value)
                    else ip.paramSemantic, None

                let sem = Symbol.Create inputName
                let loc = ip.paramLocation
                match o.VertexAttributes.TryGetAttribute sem with
                | Some vAtt ->
                    match index with
                    | Some idx ->
                        let attStride = if vAtt.Stride = 0 then Marshal.SizeOf vAtt.ElementType else vAtt.Stride
                        let view = BufferView(vAtt.Buffer, vAtt.ElementType, vAtt.Offset + offsets.[idx] * attStride, stride * attStride)
                        Some (loc, (BindingFrequency.Instance, view))
                    | None ->
                        Some (loc, (BindingFrequency.Vertex, vAtt) )
                | None ->
                    match o.InstanceAttributes.TryGetAttribute sem with
                    | Some iAtt ->
                        match index with
                        | Some _ -> failf "cannot instance GS"
                        | None -> Some (loc, (BindingFrequency.Instance, iAtt))
                    | None ->
                        Log.warn "attribute %s not found" ip.paramSemantic
                        None
            )
            |> Map.ofSeq

        let vbb = 
            this.CreateVertexBufferBinding(o.Indices, buffers)

        let tryGet (name : string) =
            match Uniforms.tryGetDerivedUniform name o.Uniforms with
            | Some v -> ValueSome v
            | None ->
                match o.Uniforms.TryGetUniform(Ag.Scope.Root, Symbol.Create name) with
                | Some v -> ValueSome v
                | None -> ValueNone
        let ub =
            program.Interface.uniformBuffers
            |> MapExt.toSeq
            |> Seq.map (fun (_name, ub) ->
                ub.ubBinding, this.CreateUniformBuffer(ub, tryGet)
            )

        let sampledTextures =
            program.Interface.samplers
            |> MapExt.toSeq
            |> Seq.collect (fun (_, t) ->
                let bi = t.samplerBinding
                t.samplerTextures |> Seq.indexed |> Seq.choose (fun (i, (name, _state)) ->
                    let texture = 
                        match tryGet name with
                        | ValueSome (:? aval<ITexture> as t) -> this.CreateTexture t |> Some
                        | ValueSome (:? aval<IBackendTexture> as t) -> this.CreateTexture t |> Some
                        | ValueSome (:? aval<Texture> as t) -> this.CreateTexture t |> Some
                        | _ -> None

                    match texture with
                    | Some texture ->
                        let s = program.Samplers.[bi + i]
                        let sampler = ARes.ofCreateDestroy (fun () -> AVal.constant s) (fun _ -> ())
                        
                        Some (bi + i, (texture, sampler))
                    | None ->
                        None
                )
            )
            |> Map.ofSeq

        let lineWidth =
            match tryGet "LineWidth" with
            | ValueSome (:? aval<float> as w) ->
                w
            | _ ->
                AVal.constant 1.0

        let calls =
            if instancedGS then
                match o.DrawCalls with
                | DrawCalls.Direct calls ->
                    calls |> AVal.map (List.map (fun c ->
                        let instanceCount = 
                            match o.Mode with
                            | IndexedGeometryMode.PointList -> c.FaceVertexCount
                            | IndexedGeometryMode.LineStrip -> c.FaceVertexCount - 1
                            | IndexedGeometryMode.LineList -> c.FaceVertexCount / 2
                            | mode -> failf "bad IndexedGeometryMode for simulated GS: %A" mode

                        DrawCallInfo(FaceVertexCount = outputVertices, InstanceCount = instanceCount)
                    )) |> DrawCalls.Direct
                | _ ->
                    failf "cannot use Indirect with simulated GS"
            else
                o.DrawCalls


        new PreparedRenderObject(
            [o.RenderPass :> obj; program :> obj; o.Id :> obj],
            o,
            program,
            o.BlendState,
            o.DepthState,
            o.StencilState,
            o.RasterizerState,
            vbb, Map.ofSeq ub, sampledTextures,
            Option.isSome o.Indices, o.IsActive,
            mode,
            lineWidth,
            calls,
            sub
        )

    [<Extension>]
    static member PrepareRenderObject(this : ResourceManager, fbo : FramebufferSignature, o : IRenderObject) =
        match o with
        | :? RenderObject as o -> 
            new PreparedMultiRenderObject([ this.PrepareRenderObject(fbo, o) ])

        | :? MultiRenderObject as o -> 
            new PreparedMultiRenderObject (
                o.Children |> List.collect (fun o -> this.PrepareRenderObject(fbo, o).Objects)
            )

        | :? PreparedRenderObject as o ->
            new PreparedMultiRenderObject([o])

        | :? PreparedMultiRenderObject as o ->
            o

        | :? CommandRenderObject ->
            // TODO CommandRenderObject
            failf "CommandRenderObject not implemented"

        | o ->
            failf "bad RenderObject: %A" o
