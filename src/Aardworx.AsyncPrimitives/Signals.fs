namespace Aardworx.AsyncPrimitives

open System.Threading
open System.Threading.Tasks

type AsyncManualResetEvent(isSet : bool) =
    static let finished = Task.FromResult()
    
    let mutable isSet = isSet
    let mutable tcs : option<TaskCompletionSource<unit>> = None

    member x.Set() =
        lock x (fun () -> 
            isSet <- true
            match tcs with
            | Some t -> 
                t.SetResult()
                tcs <- None
            | None ->
                ()
        )
        
    member x.Reset() =
        lock x (fun () ->
            isSet <- false    
        )
            
    member x.Wait(ct : CancellationToken) =
        lock x (fun () ->
            if isSet then
                finished
            else
                match tcs with
                | Some tcs -> 
                    tcs.Task
                | None -> 
                    let t = TaskCompletionSource<unit>(TaskCreationOptions.RunContinuationsAsynchronously)
                    tcs <- Some t

                    if ct.CanBeCanceled then
                        Async.StartAsTask(Async.AwaitTask(t.Task), cancellationToken = ct)
                    else
                        t.Task
        )
                
    member x.Wait() = x.Wait(CancellationToken.None)
       
type AsyncAutoResetEvent(isSet : bool) =
    static let finished = Task.FromResult()
    
    let mutable isSet = isSet
    let mutable tcs : option<TaskCompletionSource<unit>> = None

    member x.Pulse() =
        lock x (fun () -> 
            match tcs with
            | Some t -> 
                t.SetResult()
                tcs <- None
                isSet <- false
            | None -> 
                isSet <- true
        )
            
    member x.Poll() =
        lock x (fun () ->
            if isSet then
                isSet <- false
                true
            else
                false
        )
            
    member x.Wait(ct : CancellationToken) =
        lock x (fun () ->
            if isSet then
                isSet <- false
                finished
            else
                match tcs with
                | Some tcs -> 
                    tcs.Task
                | None -> 
                    let t = TaskCompletionSource<unit>(TaskCreationOptions.RunContinuationsAsynchronously)
                    tcs <- Some t

                    if ct.CanBeCanceled then
                        Async.StartAsTask(Async.AwaitTask(t.Task), cancellationToken = ct)
                    else
                        t.Task
        )
                
    member x.Wait() = x.Wait(CancellationToken.None)
      
