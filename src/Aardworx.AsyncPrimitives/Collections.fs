namespace Aardworx.AsyncPrimitives

open System
open System.Collections.Generic

type AsyncBlockingCollection<'a>() =
    let store = Queue<'a>()
    let mutable completed = false
    let isNonEmpty = AsyncManualResetEvent(false)
    
    member x.Put(value : 'a) =
        let needsSet = 
            lock store (fun () ->
                store.Enqueue value
                store.Count = 1
            )
        if needsSet then isNonEmpty.Set()
    
    member x.Take() =
        task {
            do! isNonEmpty.Wait()
            let value = 
                lock store (fun () ->
                    if store.Count > 0 then Some (store.Dequeue())
                    elif completed then raise <| OperationCanceledException()
                    else None
                )
            match value with
            | Some v -> return v
            | None ->
                isNonEmpty.Reset()
                return! x.Take()
        }

    member x.Completed() =
        lock store (fun () ->
            completed <- true
            isNonEmpty.Set()
        )
        