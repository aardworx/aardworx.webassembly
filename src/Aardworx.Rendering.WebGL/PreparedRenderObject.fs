namespace Aardworx.Rendering.WebGL

open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open System.Runtime.CompilerServices
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"


type PreparedRenderObject(  key : list<obj>,
                            original : IRenderObject,
                            program : Program,
                            blend : BlendState, 
                            depth : DepthState, 
                            stencil : StencilState,
                            rasterizer : RasterizerState,
                            vbb : ares<VertexBufferBinding>, 
                            uniformBuffers : Map<int, AdaptiveUniformBuffer>,
                            samplers : Map<int, ares<Texture> * ares<Sampler>>,
                            isIndexed : bool, isActive : aval<bool>,
                            mode : IndexedGeometryMode,
                            lineWidth : aval<float>,
                            draw : DrawCalls,
                            activation : System.IDisposable) =
    member x.Activation = activation
    member x.Program = program
    member x.Key = key
    member x.BlendState = blend
    member x.StencilState = stencil
    member x.DepthState = depth
    member x.RasterizerState = rasterizer
    member x.VertexBufferBinding = vbb
    member x.UniformBuffers = uniformBuffers
    member x.Samplers = samplers
    member x.IsIndexed = isIndexed
    member x.DrawCalls = draw
    member x.Mode = mode
    member x.LineWidth = lineWidth
    member x.IsActive = isActive
    
    member x.Dispose() =
        activation.Dispose()
    member x.Update(_t, _rt) = 
        () // TODO: really acquire and update resource
        
    interface IRenderObject with
        member x.Id = original.Id
        member x.AttributeScope = original.AttributeScope
        member x.RenderPass = original.RenderPass

    interface IPreparedRenderObject with
        member x.Dispose() = x.Dispose()
        member x.Update(t, rt) = x.Update(t, rt)
        member x.Original = None

type PreparedMultiRenderObject(objs : list<PreparedRenderObject>) =
    let first = List.head objs :> IPreparedRenderObject
    member x.Objects = objs
    
    member x.Dispose() = objs |> List.iter (fun o -> o.Dispose())
    member x.Update(t, rt) = objs |> List.iter (fun o -> o.Update(t, rt))
    interface IRenderObject with
        member x.Id = first.Id
        member x.AttributeScope = first.AttributeScope
        member x.RenderPass = first.RenderPass

    interface IPreparedRenderObject with
        member x.Dispose() = x.Dispose()
        member x.Update(t, rt) = x.Update(t, rt)
        member x.Original = first.Original



[<AbstractClass; Sealed; Extension>]
type RenderObjectCommandExtensions private() =

    static let lineModes =
        HashSet.ofArray [|
            IndexedGeometryMode.LineList
            IndexedGeometryMode.LineStrip
            IndexedGeometryMode.LineAdjacencyList
        |]
        
    [<Extension>]
    static member SetLineWidth(this : CommandStream, value : float) =
        this.BaseStream.LineWidth(float32 value)
        
    [<Extension>]
    static member SetLineWidth(this : CommandStream, value : aval<float>) =
        if value.IsConstant then
            this.BaseStream.LineWidth(float32 (AVal.force value))
        else
            this.BaseStream.LineWidth(value |> APtr.mapVal (fun v -> float32 v))


    [<Extension>]
    static member Render(this : CommandStream, o : PreparedRenderObject) =
        this.SetProgram o.Program
        this.SetBlendState o.BlendState
        this.SetDepthState o.DepthState
        this.SetStencilState o.StencilState
        this.SetRasterizerState o.RasterizerState
        this.SetVertexBufferBinding o.VertexBufferBinding
        for KeyValue(i, ub) in o.UniformBuffers do
            this.SetUniformBuffer(i, ub)
        for KeyValue(i, (t, s)) in o.Samplers do
            this.SetTexture(i, t)
            this.SetSampler(i, s)
        if lineModes.Contains o.Mode then this.SetLineWidth(o.LineWidth)

        let calls =
            if o.IsActive.IsConstant then
                if AVal.force o.IsActive then o.DrawCalls 
                else DrawCalls.Direct (AVal.constant [])
            else
                match o.DrawCalls with
                | DrawCalls.Direct calls ->
                    o.IsActive |> AVal.bind (function true -> calls | _ -> AVal.constant []) |> DrawCalls.Direct
                | DrawCalls.Indirect calls ->
                    o.IsActive |> AVal.bind (function true -> calls | _ -> AVal.constant (IndirectBuffer.ofList o.IsIndexed [])) |> DrawCalls.Indirect

        this.Draw(o.Mode, o.IsIndexed, calls)
        
    [<Extension>]
    static member Render(this : CommandStream, prev : PreparedRenderObject, o : PreparedRenderObject) =
        let programChanged = prev.Program <> o.Program
        if programChanged then
            this.SetProgram o.Program
        if prev.BlendState <> o.BlendState then
            this.SetBlendState o.BlendState
        if prev.DepthState <> o.DepthState then
            this.SetDepthState o.DepthState
        if prev.StencilState <> o.StencilState then
            this.SetStencilState o.StencilState
        if prev.RasterizerState <> o.RasterizerState then
            this.SetRasterizerState o.RasterizerState
        if programChanged || prev.VertexBufferBinding <> o.VertexBufferBinding then
            this.SetVertexBufferBinding o.VertexBufferBinding

        for KeyValue(i, ub) in o.UniformBuffers do
            match Map.tryFind i prev.UniformBuffers with
            | Some oub when oub = ub && not programChanged -> ()
            | _ -> this.SetUniformBuffer(i, ub)

        for KeyValue(i, (t, s)) in o.Samplers do
            match Map.tryFind i prev.Samplers with
            | Some (ot, os) ->
                if ot <> t || programChanged then this.SetTexture(i, t)
                if os <> s || programChanged then this.SetSampler(i, s)
            | _ ->
                this.SetTexture(i, t)
                this.SetSampler(i, s)
                
        if lineModes.Contains o.Mode && prev.LineWidth <> o.LineWidth then this.SetLineWidth(o.LineWidth)

        let calls =
            if o.IsActive.IsConstant then
                if AVal.force o.IsActive then o.DrawCalls 
                else DrawCalls.Direct (AVal.constant [])
            else
                match o.DrawCalls with
                | DrawCalls.Direct calls ->
                    o.IsActive |> AVal.bind (function true -> calls | _ -> AVal.constant []) |> DrawCalls.Direct
                | DrawCalls.Indirect calls ->
                    o.IsActive |> AVal.bind (function true -> calls | _ -> AVal.constant (IndirectBuffer.ofList o.IsIndexed [])) |> DrawCalls.Indirect

        this.Draw(o.Mode, o.IsIndexed, calls)
        
    [<Extension>]
    static member Render(this : CommandStream, prev : option<PreparedRenderObject>, o : PreparedRenderObject) =
        match prev with
        | Some prev -> this.Render(prev, o)
        | None -> this.Render(o)
        
    [<Extension>]
    static member Render(this : CommandStream, prev : option<PreparedMultiRenderObject>, o : PreparedMultiRenderObject) =
        let mutable last =
            match prev with
            | Some p -> p.Objects |> List.tryLast
            | None -> None

        for o in o.Objects do
            this.Render(last, o)
            last <- Some o

