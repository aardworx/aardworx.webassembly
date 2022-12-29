namespace Aardworx.Rendering.WebGL.Streams

open System
open Aardvark.Base
open FSharp.Data.Adaptive
open Silk.NET.OpenGLES
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"
#nowarn "4321"



[<AbstractClass>]
type CommandEncoderBase(device : Device) =
    inherit AdaptiveObject()

    let lockObj = obj()
    let mutable all = System.Collections.Generic.HashSet<AdaptivePointer>()
    let dirtyLock = obj()
    let mutable dirtySet = System.Collections.Generic.HashSet<AdaptivePointer>()
    let mutable pointers = Dict<IAdaptiveObject, System.Collections.Generic.HashSet<AdaptivePointer>>()
    let mutable disposables = System.Collections.Generic.List<IDisposable>()
    let mutable nested = System.Collections.Generic.List<CommandEncoderBase>()

    let addPtr (x : CommandEncoderBase) (ptr : aptr<'a>) =
        lock lockObj (fun () ->
            let isNew = all.Add ptr
            if isNew then ptr.Acquire()

            if not ptr.IsConstant then
                if isNew then lock dirtyLock (fun () -> dirtySet.Add ptr |> ignore)
                let l = pointers.GetOrCreate(ptr.Input, fun _ -> System.Collections.Generic.HashSet())
                l.Add ptr |> ignore
                if not x.OutOfDate then transact x.MarkOutdated

            ptr
        )

    override x.InputChangedObject(_, input) =
        match lock lockObj (fun () -> pointers.TryGetValue input) with
        | true, ptrs -> lock dirtyLock (fun () -> dirtySet.UnionWith ptrs)
        | _ -> ()

    member x.Device = device
    
    member x.Use<'a when 'a : unmanaged> (value : aptr<'a>) =
        addPtr x value

    member x.Update(token : AdaptiveToken) =
        x.EvaluateIfNeeded token () (fun token ->
            let dirty = 
                lock dirtyLock (fun () ->
                    let d = dirtySet
                    dirtySet <- System.Collections.Generic.HashSet()
                    d
                )

            for d in dirty do d.Update token
            for n in nested do n.Update token
        )
    
    member x.AddTemporaryResource (d : IDisposable) = disposables.Add d

    member x.AddNested (n : CommandEncoderBase) =
        nested.Add n
    
    [<CompilerMessage("internal use only", 4321, IsHidden = true)>]
    abstract Destroy : unit -> unit
    default x.Destroy() = ()

    [<CompilerMessage("internal use only", 4321, IsHidden = true)>]
    abstract Clear : unit -> unit
    default x.Clear() = ()

    abstract Begin : unit -> unit
    default x.Begin() = ()

    abstract End : unit -> unit
    default x.End() = ()
    
    abstract Perform : GL -> unit

    member x.UnsafeRunSynchronously(gl : GL) =
        x.Perform gl

    abstract Custom : action : (GL -> unit) -> unit
    abstract Push : nativeptr<'a> -> unit
    abstract Pop : nativeptr<'a> -> unit
    abstract Copy : nativeint * nativeint * nativeint -> unit

    abstract CopyDD : aptr<'a> * aptr<'a> * aptr<nativeint> -> unit
    abstract CopyDI : aptr<'a> * aptr<nativeint> * aptr<nativeint> -> unit
    abstract CopyID : aptr<nativeint> * aptr<'a> * aptr<nativeint> -> unit
    abstract CopyII : aptr<nativeint> * aptr<nativeint> * aptr<nativeint> -> unit
    
    /// res <- a + b
    abstract Add : a : aptr<nativeint> * b : aptr<nativeint> * res : aptr<nativeint> -> unit
    /// res <- a + b*c
    abstract Mad : a : aptr<nativeint> * b : aptr<nativeint> * c : aptr<nativeint> * res : aptr<nativeint> -> unit

    abstract Bgra : colors : aptr<byte> * count : aptr<int> -> unit
    abstract CopyBgra : src : aptr<byte> * dst : aptr<byte> * count : aptr<int> -> unit

    //member x.Start() =
    //    x.Update AdaptiveToken.Top
    //    device.Enqueue (fun gl ->
    //        x.UnsafeRunSynchronously gl
    //    )

    member x.Run(token : AdaptiveToken) =
        x.Update token
        device.Run (fun gl ->
            x.UnsafeRunSynchronously gl
        )
    
    member x.Reset() =
        dirtySet.Clear()
        for k in all do
            k.Input.Outputs.Remove x |> ignore
            k.Release()
        all.Clear()
        pointers.Clear()
        x.Clear()
        for d in disposables do d.Dispose()
        disposables.Clear()
        for n in nested do n.Dispose()
        nested.Clear()
        if not x.OutOfDate then transact x.MarkOutdated

    member x.Update(action : CommandEncoderBase -> 'a) =
        x.Reset()
        x.Begin()
        try action x
        finally x.End()
    

    member x.Dispose() =
        x.Reset()
        x.Destroy()

    interface IDisposable with
        member x.Dispose() = x.Dispose()

