namespace Aardworx.Rendering.WebGL

open System
open System.Threading
open Silk.NET.OpenGLES
open System.Diagnostics
open Aardvark.Base
open System.Runtime.CompilerServices

#nowarn "9"

/// Unique ID generator.
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

/// Basic interface for resources.
[<AllowNullLiteral>]
type IResource =
    inherit IDisposable
    
    /// The resource's device.
    abstract Device : Device
    
    /// Whether the resource is disposed.
    abstract IsDisposed : bool
    
    /// Tries to add a reference to the resource. (fails if resource is already disposed)
    abstract TryAddReference : unit -> bool
    
    /// The resource's unique name.
    abstract UniqueName : string

/// Resource Extensions.
[<AbstractClass; Sealed; Extension>]
type IResourceExtensions private() =

    /// Adds a reference to the resource and fails if the resource is already disposed.
    [<Extension>]
    static member AddReference(this : IResource) =
        if not (this.TryAddReference()) then
            raise <| ObjectDisposedException(this.GetType().FullName)

/// Base class for resources.
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
            
    /// abstract method to free the resource.
    abstract Free : unit -> unit
    
    member internal x.CheckDisposed() =
        if refCount <= 0 then raise <| ObjectDisposedException(x.GetType().FullName)
        
    /// The resource's unique name.
    member x.UniqueName = uniqueName

    /// The resource's device.
    member x.Device = 
        x.CheckDisposed()
        device

    /// Tries to add a reference to the resource. (fails if resource is already disposed)
    member x.TryAddReference() =
        lock x (fun () ->
            if refCount <= 0 then 
                false
            else
                refCount <- refCount + 1
                //Report.Debug("added reference to {0}: {1}", uniqueName, refCount)
                true
        )

    /// Adds a reference to the resource and fails if the resource is already disposed.
    member x.AddReference() =
        lock x (fun () ->
            if refCount <= 0 then raise <| ObjectDisposedException(x.GetType().FullName)
            refCount <- refCount + 1
            //Report.Debug("added reference to {0}: {1}", uniqueName, refCount)
        )
        
    /// Whether the resource is disposed.
    member x.IsDisposed =
        refCount <= 0

    /// Disposes the resource.
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

/// Base class for resources.
and [<AbstractClass; AllowNullLiteral>] Resource(device : Device, name : string, handle : uint32, sizeInBytes : int64) =
    inherit ResourceBase(device, name, sizeInBytes)
    let mutable handle = handle

    /// Abstract method for destroying the resource.
    abstract Destroy : GL -> unit

    /// Free implementation
    override x.Free() =
        if not device.IsDisposed then
            device.Run x.Destroy
          
        handle <- Unchecked.defaultof<_>

    /// Handle of the resource.
    member internal x.HandleTask =
        x.CheckDisposed()
        handle

    /// Handle of the resource.
    member x.Handle = 
        x.CheckDisposed()
        handle

/// Base class for resources that are not shared between contexts.
and [<AbstractClass>] UnsharedResource<'a>(device : Device, name : string, createHandle : UnsharedResource<'a> -> GL -> 'a, deleteHandle : UnsharedResource<'a> -> GL -> 'a -> unit, sizeInBytes : int64) =
    inherit ResourceBase(device, name, sizeInBytes)

    let handles = Dict<WebGLContext, 'a>()

    /// Gets the Handle for a given context (creates it if necessary).
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
          
    /// Abstract method for destroying the resource.
    abstract Destroy : unit -> unit
    default x.Destroy() = ()

    /// Abstract method that is invoked when the resource is destroyed.
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
    
