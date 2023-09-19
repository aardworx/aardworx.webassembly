namespace Aardworx.WebAssembly
open System
open Aardworx.AsyncPrimitives
open Aardvark.Base
open Aardvark.Dom

#nowarn "9"

open System.Runtime.InteropServices

type BinaryWorkerSerializer private() =
    static let serializers = Dict<Type, obj -> byte[]>()
    static let deserializers = Dict<Type, byte[] -> obj>()
    
    static let isBlittable (t : Type) =
        if t.IsValueType then
            let arr = System.Array.CreateInstance(t, 1)
            try
                let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
                gc.Free()
                true
            with _ ->
                false
        else
            false
            
    static member private TryGetPrimitiveSerializer<'a>() =
        let typ = typeof<'a>
        
        if typ = typeof<string> then
            let serialize (v : obj) = System.Text.Encoding.UTF8.GetBytes (v :?> string)
            let deserialize (v : byte[]) = System.Text.Encoding.UTF8.GetString(v, 0, v.Length) :> obj
            Some (serialize, deserialize)
        elif typ = typeof<bool> then
            let serialize (v : obj) = if v :?> bool then [|1uy|] else [|0uy|]
            let deserialize (v : byte[]) = (v.[0] <> 0uy) :> obj
            Some (serialize, deserialize)
            
        else
            let blittable =
                if typ.IsValueType then
                    let arr = Array.zeroCreate<'a> 1
                    try
                        let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
                        gc.Free()
                        true
                    with _ ->
                        false
                else
                    false
            if blittable then
                let serialize (v : 'a) =
                    let arr = [|v|]
                    let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
                    let bytes = Array.zeroCreate<byte> sizeof<'a>
                    Marshal.Copy(gc.AddrOfPinnedObject(), bytes, 0, bytes.Length)
                    gc.Free()
                    bytes
                let deserialize (v : byte[]) =
                    let arr = Array.zeroCreate<'a> 1
                    let gc = GCHandle.Alloc(arr, GCHandleType.Pinned)
                    Marshal.Copy(v, 0, gc.AddrOfPinnedObject(), v.Length)
                    gc.Free()
                    arr.[0] :> obj
                Some (unbox >> serialize, deserialize)
            else
                None
        
    static member Register(serialize : 'a -> byte[], deserialize : byte[] -> 'a) =
        serializers.[typeof<'a>] <- fun o -> serialize (o :?> 'a)
        deserializers.[typeof<'a>] <- fun o -> deserialize o :> obj
    
    static member Serialize(value : 'a) =
        match serializers.TryGetValue typeof<'a> with
        | (true, s) -> s value
        | _ ->
            match BinaryWorkerSerializer.TryGetPrimitiveSerializer<'a>() with
            | Some (s, d) ->
                serializers.[typeof<'a>] <- s
                deserializers.[typeof<'a>] <- d
                s value
            | None -> 
                failwithf "No serializer for type %s" (typeof<'a>).FullName
    
    static member Deserialize<'a>(value : byte[]) : 'a =
        match deserializers.TryGetValue typeof<'a> with
        | (true, s) -> s value :?> 'a
        | _ ->
            match BinaryWorkerSerializer.TryGetPrimitiveSerializer<'a>() with
            | Some (s, d) ->
                serializers.[typeof<'a>] <- s
                deserializers.[typeof<'a>] <- d
                d value :?> 'a
            | None -> 
                failwithf "No deserializer for type %s" (typeof<'a>).FullName

module WorkerExtensions =
    open System.Threading
    open System.Threading.Tasks

    type AdapterWorker<'t, 'a, 'b when 't :> Aardvark.Dom.AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() =
        inherit AbstractWorker()

        let instance = new 't()
        let mutable runner = Unchecked.defaultof<_>
        
        override this.Run(ctx) =
            let rec receive() =
                task {
                    let! v = ctx.Receive()
                    do! Async.Sleep 0
                    match v with
                    | WorkerMessage.Binary arr ->
                        return BinaryWorkerSerializer.Deserialize<'a> arr
                    | _ ->
                        return! receive()
                }
                
            runner <-
                instance.Run {
                    Send = fun msgs ->
                        for m in msgs do
                            ctx.Send (WorkerMessage.Binary (BinaryWorkerSerializer.Serialize m))
                        Task.CompletedTask
                    Receive = receive
                    FinishSending = ctx.Terminate
                }
            Task.FromResult()
        
        
    let run<'t, 'a, 'b when 't :> Aardvark.Dom.AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() : Task<WorkerInstance<'b, 'a>> =
        task {
            let! instance = Worker.start<AdapterWorker<'t, 'a, 'b>>()
            let rec receive() =
                task {
                    let! v = instance.Receive()
                    do! Async.Sleep 0
                    match v with
                    | WorkerMessage.Binary arr ->
                        return BinaryWorkerSerializer.Deserialize<'b> arr
                    | _ ->
                        return! receive()
                }
            return {
                Send = fun msgs ->
                    for m in msgs do
                        instance.Send (WorkerMessage.Binary (BinaryWorkerSerializer.Serialize m))
                    Task.CompletedTask
                Receive = receive
                FinishSending = instance.Terminate
            }
        
        }


