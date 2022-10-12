namespace Aardworx.Rendering.WebGL

open System
open System.Threading
open Silk.NET.OpenGLES
open System.Diagnostics
open Aardvark.Base
open System.Runtime.CompilerServices

#nowarn "9"

type UniqueId private() =
    static let currentIds = System.Collections.Concurrent.ConcurrentDictionary<string, ref<Set<int>> * ref<int>>()

    static member Free(name : string, id : int) =
        let free, _ = currentIds.GetOrAdd(name, fun _ -> ref Set.empty, ref 0)
        Interlocked.Change(&free.contents, fun f ->
            Set.add id f
        ) |> ignore
        
    static member Alloc(name : string) =
        let free, id = currentIds.GetOrAdd(name, fun _ -> ref Set.empty, ref 0)

        let mine =
            Interlocked.Change(&free.contents, fun f ->
                if Set.isEmpty f then
                    f, ValueNone
                else
                    let v = Seq.head f
                    Set.remove v f, ValueSome v
            )
        let myId = 
            match mine with
            | ValueSome mine -> mine
            | ValueNone -> Interlocked.Increment(&id.contents)
        myId //sprintf "%s%03d" name myId

[<AllowNullLiteral>]
type IResource =
    inherit IDisposable
    abstract Device : Device
    abstract IsDisposed : bool
    abstract TryAddReference : unit -> bool
    abstract UniqueName : string

[<AbstractClass; Sealed; Extension>]
type IResourceExtensions private() =

    [<Extension>]
    static member AddReference(this : IResource) =
        if not (this.TryAddReference()) then
            raise <| ObjectDisposedException(this.GetType().FullName)

[<AbstractClass; AllowNullLiteral>]
type ResourceBase(device : Device, name : string, sizeInBytes : int64) as this =

    let uniqueId = UniqueId.Alloc name
    let uniqueName = sprintf "%s%03d" name  uniqueId

    do //Report.Debug("created {0}", uniqueName)
       device.ResourceCreated(uniqueName)

    let mutable refCount = 1

    let mutable trackerObj = 
        if device.Debug then 
            if sizeInBytes > 0L then GC.AddMemoryPressure(sizeInBytes)
            DebugResourceFinalizer(this) 
        else 
            null
            
    abstract Free : unit -> unit
    
    member x.UniqueName = uniqueName

    member internal x.CheckDisposed() =
        if refCount <= 0 then raise <| ObjectDisposedException(x.GetType().FullName)
        
    member x.Device = 
        x.CheckDisposed()
        device

    member x.TryAddReference() =
        lock x (fun () ->
            if refCount <= 0 then 
                false
            else
                refCount <- refCount + 1
                //Report.Debug("added reference to {0}: {1}", uniqueName, refCount)
                true
        )

    member x.AddReference() =
        lock x (fun () ->
            if refCount <= 0 then raise <| ObjectDisposedException(x.GetType().FullName)
            refCount <- refCount + 1
            //Report.Debug("added reference to {0}: {1}", uniqueName, refCount)
        )
        
    member x.IsDisposed =
        refCount <= 0

    member x.Dispose() =
        let delete = 
            lock x (fun () ->
                if refCount > 0 then
                    refCount <- refCount - 1
                    //Report.Debug("removed reference to {0}: {1}", uniqueName, refCount)   
                    refCount = 0
                else
                    false
            )
        if delete then
            if not (isNull trackerObj) then 
                trackerObj.Dispose()
                trackerObj <- null
            x.Free()
            device.ResourceDestroyed(uniqueName)
            //Report.Debug("deleted {0}", uniqueName)
            UniqueId.Free(name, uniqueId)
   
    interface IDisposable with
        member x.Dispose() = x.Dispose()

    interface IResource with
        member x.Device = x.Device
        member x.IsDisposed = x.IsDisposed
        member x.UniqueName = x.UniqueName
        member x.TryAddReference() = x.TryAddReference()

and [<AbstractClass; AllowNullLiteral>] Resource(device : Device, name : string, handle : uint32, sizeInBytes : int64) =
    inherit ResourceBase(device, name, sizeInBytes)
    let mutable handle = handle

    abstract Destroy : GL -> unit

    override x.Free() =
        if not device.IsDisposed then
            device.Run x.Destroy
          
        handle <- Unchecked.defaultof<_>

    member internal x.HandleTask =
        x.CheckDisposed()
        handle

    member x.Handle = 
        x.CheckDisposed()
        handle

and [<AbstractClass>] UnsharedResource<'a>(device : Device, name : string, createHandle : UnsharedResource<'a> -> GL -> 'a, deleteHandle : UnsharedResource<'a> -> GL -> 'a -> unit, sizeInBytes : int64) =
    inherit ResourceBase(device, name, sizeInBytes)

    let handles = Dict<WebGLContext, 'a>()

    member x.GetHandle(ctx : WebGLContext) =
        if not ctx.IsCurrent then 
            failwithf "[WebGL] cannot get handle for non-current context"

        let cached = 
            lock handles (fun () ->
                match handles.TryGetValue ctx with
                | true, h -> ValueSome h
                | _ -> ValueNone
            )

        match cached with
        | ValueSome cached ->
            cached
        | ValueNone ->
            let h = createHandle x ctx.GL
            let realHandle = 
                lock handles (fun () ->
                    let n = handles.GetOrCreate(ctx, fun _ -> h)
                    n
                )
            if not (Unchecked.equals h realHandle) then
                deleteHandle x ctx.GL h
            realHandle
          
    abstract Destroy : unit -> unit
    default x.Destroy() = ()

    abstract Destroyed : unit -> unit
    default x.Destroyed() = ()

    override x.Free() =
        let cnt = ref handles.Count
        for KeyValue(ctx, h) in handles do
            ctx.WhenCurrent (fun _gl ->
                deleteHandle x ctx.GL h
                if Interlocked.Decrement(&cnt.contents) = 0 then
                    x.Destroyed()
            )
        handles.Clear()
        x.Destroy()


and [<AllowNullLiteral>] internal DebugResourceFinalizer(parent : IResource) =

    let stack = StackTrace(2, true)

    member x.Dispose() =
        GC.SuppressFinalize x

    override x.Finalize() = 
        if not parent.IsDisposed then
            Log.warn "[GL] leaking resource: %A\nallocated at:\n%A" parent stack
            parent.Dispose()
    
