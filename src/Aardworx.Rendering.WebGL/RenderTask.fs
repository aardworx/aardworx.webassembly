namespace Aardworx.Rendering.WebGL

open System
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Microsoft.FSharp.NativeInterop
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"

type RenderObjectProgram(manager : ResourceManager, signature : FramebufferSignature, mode : CommandStreamMode) =
    inherit AdaptiveObject()

    let device = manager.Device
    let cmd = device.CreateCommandProgram(mode)
    do cmd.State.FramebufferSignature <- signature
       cmd.State.AddReference()

    let order = OrderMaintenanceTrie<obj, ref<ICommandFragment>>()
    let recompile = System.Collections.Generic.HashSet<ICommandFragment>()

    let head =
        let ref = 
            order.AddOrUpdate(
                [], fun old ->
                    match old with
                    | ValueSome o -> o
                    | ValueNone -> ref null
            )
        let c = cmd.InsertAfter(null)
        ref.Value.Value <- c
        c

    let fbo = NativePtr.alloc 1
    let viewportOffset = NativePtr.alloc 1
    let viewportSize = NativePtr.alloc 1

    do  head.Update (fun cmd ->
            cmd.SetFramebuffer(FramebufferTarget.Framebuffer, signature, APtr.ofNativePtr fbo)
            cmd.SetViewport(viewportOffset, viewportSize)
        )

    member x.Add(ro : PreparedMultiRenderObject) =
        let key = (List.head ro.Objects).Key
        let ref = 
            order.AddOrUpdate(
                key, fun old ->
                    match old with
                    | ValueSome o -> o
                    | ValueNone -> ref null
            )
        let self = ref.Value
        if isNull self.Value then
            match ref.Prev with
            | ValueSome prev ->
                self.Value <- cmd.InsertAfter(prev.Value.Value, ro)
            | ValueNone ->
                match ref.Next with
                | ValueSome next ->
                    self.Value <- cmd.InsertBefore(next.Value.Value, ro)
                | ValueNone ->
                    self.Value <- cmd.InsertAfter(null, ro)

        recompile.Add self.Value |> ignore
        if not (isNull self.Value.Next) then
            recompile.Add self.Value.Next |> ignore

    member x.Remove(ro : PreparedMultiRenderObject) =
        let key = (List.head ro.Objects).Key
        match order.TryGetValue key with
        | ValueSome self ->
            let self = self.Value
            match order.TryRemove(key) with
            | ValueSome (l, r) ->
                match r with
                | ValueSome r -> 
                    recompile.Add r.Value.Value |> ignore
                | ValueNone ->
                    ()
                recompile.Remove self |> ignore
                self.Dispose()
                ro.Dispose()
                  
            | ValueNone ->
                ()
        | _ ->
            ()

    member x.SetFramebuffer(framebuffer : Framebuffer, viewport : Box2i) =
        NativePtr.write fbo framebuffer.Handle
        NativePtr.write viewportOffset viewport.Min
        NativePtr.write viewportSize (V2i.II + viewport.Max - viewport.Min) // TODO: check if old bug still exists

    member x.Run(token : AdaptiveToken) =
        x.EvaluateAlways token (fun token ->
            let recompile = 
                lock recompile (fun () ->
                    let res = HashSet.toArray recompile
                    recompile.Clear()
                    res
                )

            for r in recompile do
                match r.Tag with
                | :? PreparedMultiRenderObject as ro ->
                    let po =
                        if isNull r.Prev then 
                            None
                        else 
                            match r.Prev.Tag with
                            | :? PreparedMultiRenderObject as o -> Some o
                            | _ -> None

                    r.Update(fun cmd ->
                        cmd.Render(po, ro)
                    )
                | _ ->
                    Log.warn "not a RenderObject"
                    ()

            cmd.Run token
        )

    member x.Dispose() =
        for _, cmd in order do cmd.Value.Dispose()
        order.Clear()
        recompile.Clear()
        recompile.TrimExcess()
        cmd.State.Dispose()
        cmd.Dispose()
        NativePtr.free fbo
        NativePtr.free viewportOffset
        NativePtr.free viewportSize

    new(manager : ResourceManager, signature : FramebufferSignature) =
        let mode =
            if manager.Device.Debug then CommandStreamMode.Debug
            else CommandStreamMode.Native
        new RenderObjectProgram(manager, signature, mode)

    interface IDisposable with  
        member x.Dispose() = x.Dispose()

type RenderTask(manager : ResourceManager, signature : FramebufferSignature, mode : CommandStreamMode, objs : aset<IRenderObject>) =
    inherit AdaptiveObject()

    let id = newId()
    let mutable reader = objs.GetReader()
    let cache = Dict<IRenderObject, PreparedMultiRenderObject>()
    let prog = new RenderObjectProgram(manager, signature, mode)
    let mutable frameId = 0UL
    
    //member x.Update(t : AdaptiveToken, rt : RenderToken) =
    //    x.EvaluateAlways t (fun t ->
    //        let ops = reader.GetChanges t

    //        for op in ops do
    //            match op with
    //            | Add(_,o) ->  
    //                let po = cache.GetOrCreate(o, fun o -> manager.PrepareRenderObject(signature, o))
    //                prog.Add po

    //            | Rem(_,o) ->
    //                match cache.TryRemove o with
    //                | (true, po) -> prog.Remove po
    //                | _ -> ()

    //        prog.Update t
    //    )
    member x.Run(t : AdaptiveToken, _rt : RenderToken, o : OutputDescription) =
        x.EvaluateAlways t (fun t ->
            try
                let ops = reader.GetChanges t

                for op in ops do
                    match op with
                    | Add(_,o) ->  
                        let po = cache.GetOrCreate(o, fun o -> manager.PrepareRenderObject(signature, o))
                        prog.Add po

                    | Rem(_,o) ->
                        match cache.TryRemove o with
                        | true, po -> prog.Remove po
                        | _ -> ()

                manager.Device.Run(fun gl ->
                    prog.SetFramebuffer(o.framebuffer :?> Framebuffer, o.viewport)
                    prog.Run(t)
                    gl.BindVertexArray 0u
                )
                frameId <- frameId + 1UL
            with e ->
                Log.warn "RenderTask failed: %A" e
        )
    member x.Dispose() =
        prog.Dispose()
        cache.Clear()
        reader <- Unchecked.defaultof<_>

    interface IRenderTask with
        member this.Dispose() = this.Dispose()
        member this.FrameId = frameId
        member this.FramebufferSignature = Some (signature :> _)
        member this.Id = id
        member this.Run(arg1: AdaptiveToken, arg2: RenderToken, arg3: OutputDescription) = 
            this.Run(arg1, arg2, arg3)
        member this.Runtime: Option<IRuntime> = manager.Device.Runtime |> Some
        member this.Update(_arg1: AdaptiveToken, _arg2: RenderToken): unit = 
            ()
        member this.Use(arg1: unit -> 'a): 'a = 
            arg1()