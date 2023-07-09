namespace Aardworx.WebAssembly
open System
open Aardworx.AsyncPrimitives
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open System.Reflection
open Aardworx.WebAssembly
open Microsoft.JSInterop
open System.Linq.Expressions
open BlazorWorker.WorkerCore
open BlazorWorker.Core
open BlazorWorker.BackgroundServiceFactory
open BlazorWorker.Extensions.JSRuntime
open Aardvark.Base
open Aardvark.Dom

#nowarn "9"

[<AbstractClass>]
type BinaryWorker() = 
    static let mutable instance = Unchecked.defaultof<BinaryWorker>

    static let code = 
        """
        (function() {
            self.setupCallback = function() { 
                let recv = Module.mono_bind_static_method("[Aardworx.WebAssembly] Aardworx.WebAssembly.BinaryWorker:Receive");
                
                self.send = function(ptr, len) {
                    let data = new Uint8Array(HEAPU8.subarray(ptr, ptr + len)).buffer;
                    self.postMessage(data, [data]);
                    return 1;
                };
                
                let old = self.onmessage;
                self.onmessage = function(e) {
                    if(typeof e.data === "string") { old(e); }
                    else {
			            const data = e.data;
			            const length = data.byteLength;
			            const buffer = Module._malloc(length);
			            Module.HEAPU8.set(new Uint8Array(data), buffer);
                        recv(buffer, length);
                        Module._free(buffer);
                    }
                };

                Object.defineProperty(self, "onmessage", {
                    get: function() { return old; },
                    set: function(v) { old = v }
                });
                
            };
        })();
        
        """

    let post (data : byte[]) =
        use ptr = fixed data
        JSInvokeService.Invoke<int>("send", NativePtr.toNativeInt ptr, data.Length) |> ignore

    member x.Send(data : byte[]) =
        post data

    abstract member Run : data : byte[] -> unit

    [<JSInvokable>]
    static member Receive(ptr : nativeint, len : int) =
        let arr = Array.zeroCreate<byte> len
        Marshal.Copy(ptr, arr, 0, len)
        instance.Run arr

    member x.Start() =  
        instance <- x
        JSInvokeService.Invoke<unit>("eval", code)
        JSInvokeService.Invoke<unit>("setupCallback")

type private WorkerInstance<'Worker when 'Worker :> BinaryWorker and 'Worker : not struct> private (w : IWorker, service : BlazorWorker.WorkerBackgroundService.IWorkerBackgroundService<'Worker>, id : int) =
    inherit WorkerInstance(id)
    
    static let assemblies = 
        let rec collect (all : System.Collections.Generic.HashSet<Assembly>) (ass : Assembly) =
            if all.Add ass then
                ass.GetReferencedAssemblies()
                |> Array.iter (fun n ->
                    try
                        let ass = Assembly.Load n.FullName
                        collect all ass
                    with _ ->
                        ()
                )
    
        let all = System.Collections.Generic.HashSet()
        
        let t = typeof<'Worker>
        
        let types =
            if t.IsGenericType then Array.append [|t|] (t.GetGenericArguments())
            else [|t|]
        
        types |> Array.iter (fun t -> collect all t.Assembly)
        
        let all = 
            all
            |> Seq.toArray
            |> Array.choose (fun a ->
                try Some (a.GetName().Name + ".dll")
                with _ -> None
            )
        all //Array.append [|"System.Threading.ThreadPool.dll"; "FSharp.Data.Adaptive.dll"|] all
 
    static let worker =
        async {
            let factory = BlazorWorker.Core.WorkerFactory(JsObj.Runtime)
            let! worker = Async.AwaitTask (factory.CreateAsync())
            return worker
        } |> Async.StartAsTask
       
    static let code =
        """
        (function() {
            let recv = Module.mono_bind_static_method("[Aardworx.WebAssembly] Aardworx.WebAssembly.WorkerInstance:Receive");
            let workers = window.workers;
            if(!workers) { workers = {}; window.workers = workers; }
            
            function extractWorker(id) {
                let worker = null;
                var o = Worker.prototype.postMessage;
                Worker.prototype.postMessage = function(m) { worker = this; };
                BlazorWorker.postMessage(id, "hans");
                Worker.prototype.postMessage = o;
                return worker;
            }
            window.sendWorker = function(id, ptr, len) {
                if(ptr < 0 || ptr + len > HEAPU8.length) {
                    console.error("out of bounds access");
                }
                else {
                    let data = new Uint8Array(HEAPU8.subarray(ptr, ptr + len)).buffer;
                    workers[id].postMessage(data, [data]);
                }
            };
            
            window.setupWorker = function(id) {
                const worker = extractWorker(id);
                workers[id] = worker;
                let old = worker.onmessage;
                worker.onmessage = function(e) {
                    if(typeof e.data === "string") {
                        old(e);
                    }
                    else {
			            const data = e.data;
			            const length = data.byteLength;
			            const buffer = Module._malloc(length);
			            Module.HEAPU8.set(new Uint8Array(data), buffer);
                        recv(id, buffer, length);
			            Module._free(buffer);
                    }
                };

                
                Object.defineProperty(worker, "onmessage", {
                    get: function() { return old; },
                    set: function(v) { old = v }
                });
            };
        })();
        
        """       
 
    let messageEvent = Event<byte[]>()
    
    
    override x.OnMessage = messageEvent.Publish

    override x.Received(ptr : nativeint, len : int) =
        try
            let arr = Array.zeroCreate<byte> len
            Marshal.Copy(ptr, arr, 0, len)
            messageEvent.Trigger arr
        with e ->
            printfn "ERROR: %A" e             
    
    override x.Send (data : byte[]) : unit =
        use ptr = fixed data
        JsObj.Runtime.InvokeVoid("window.sendWorker", int w.Identifier, int (NativePtr.toNativeInt ptr), data.Length)
    
    override x.Dispose() =
        service.DisposeAsync() |> ignore
        worker.Dispose()

    static member CreateInternal() =
        task {
            let! worker = worker
            
            let! s = 
                worker.CreateBackgroundServiceAsync<'Worker>(fun o -> 
                    o.Debug <- false
                    o.AddAssemblies(assemblies).AddHttpClient().AddBlazorWorkerJsRuntime() |> ignore
                )
                
        
            let lambda =
                let meth = typeof<BinaryWorker>.GetMethod("Start")
                let p = Expression.Parameter(typeof<'Worker>, "serv")
                let body = Expression.Call(p, meth)
                Expression.Lambda<System.Action<'Worker>>(body, p)
            do! s.RunAsync(lambda)
            JsObj.Runtime.InvokeVoid("window.eval", code)
            JsObj.Runtime.InvokeVoid("window.setupWorker", int worker.Identifier)
            
            return new WorkerInstance<'Worker>(worker, s, int worker.Identifier) :> WorkerInstance
        }

and [<AbstractClass>] WorkerInstance(id : int) as this =
    
    static let workers = Dict<int, WorkerInstance>()
    do workers.[id] <- this
    
    [<JSInvokable>]
    static member Receive(id : int, ptr : nativeint, len : int) =
        match workers.TryGetValue id with
        | (true, w) -> w.Received(ptr, len)
        | _ -> ()
    
    abstract Received : nativeint * int -> unit
    abstract OnMessage : IObservable<byte[]>
    abstract Send : byte[] -> unit
    abstract Dispose : unit -> unit

    interface IDisposable with
        member x.Dispose() = x.Dispose()

    static member Create<'Worker when 'Worker :> BinaryWorker and 'Worker : not struct>() =
        WorkerInstance<'Worker>.CreateInternal()
  
type BinaryWorkerSerializer private() =
    static let serializers = Dict<Type, obj -> byte[]>()
    static let deserializers = Dict<Type, byte[] -> obj>()
    
    static member Register(serialize : 'a -> byte[], deserialize : byte[] -> 'a) =
        serializers.[typeof<'a>] <- fun o -> serialize (o :?> 'a)
        deserializers.[typeof<'a>] <- fun o -> deserialize o :> obj
    
    static member Serialize(value : 'a) =
        match serializers.TryGetValue typeof<'a> with
        | (true, s) -> s value
        | _ -> failwithf "No serializer for type %s" (typeof<'a>).FullName
    
    static member Deserialize<'a>(value : byte[]) : 'a =
        match deserializers.TryGetValue typeof<'a> with
        | (true, s) -> s value :?> 'a
        | _ -> failwithf "No deserializer for type %s" (typeof<'a>).FullName

module WorkerExtensions =
    open System.Threading
    open System.Threading.Tasks

    type AdapterWorker<'t, 'a, 'b when 't :> Aardvark.Dom.AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() as this =
        inherit BinaryWorker()

        let instance = new 't()
        let things = AsyncBlockingCollection<'a>()
        let runner =
            instance.Run {
                Send = fun msgs ->
                    for m in msgs do
                        this.Send (BinaryWorkerSerializer.Serialize m)
                    Task.CompletedTask
                Receive = fun () ->
                    task {
                        let! v = things.Take()
                        do! Async.Sleep 0
                        return v
                    }
                FinishSending = fun () ->
                    ()
            }
        
        
        override this.Run(data) =
            try
                things.Put (BinaryWorkerSerializer.Deserialize<'a> data)
            with e ->
                printfn "RECV: %A" e
        
    let run<'t, 'a, 'b when 't :> Aardvark.Dom.AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() : Task<WorkerInstance<'b, 'a>> =
        task {
            let! instance = WorkerInstance.Create<AdapterWorker<'t, 'a, 'b>>()
            let received = AsyncBlockingCollection<'b>()
            let sub =
                instance.OnMessage.Subscribe (fun msg ->
                    try
                        let msg = BinaryWorkerSerializer.Deserialize<'b> msg
                        received.Put msg
                    with e ->
                        printfn "EXN: %A" e
                )
            
            return {
                Send = fun msgs ->
                    for m in msgs do
                        instance.Send (BinaryWorkerSerializer.Serialize m)
                    Task.CompletedTask
                Receive = fun () ->
                    received.Take()
                FinishSending = fun () ->
                    sub.Dispose()
                    instance.Dispose()
                    received.Completed()
            }
        
        }


