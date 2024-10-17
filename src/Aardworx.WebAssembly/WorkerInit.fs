namespace Aardworx.WebAssembly

open System
open System.Reflection
open System
open System.Reflection
open System.Threading
open System.Threading.Tasks
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
open Aardvark.Base
open Aardvark.Dom

open Microsoft.JSInterop
#nowarn "9"

[<RequireQualifiedAccess>]
type WorkerMessage =
    | Binary of byte[]
    | String of string
  
type WorkerContext =
    {
        Receive : unit -> Task<WorkerMessage>
        Send : WorkerMessage -> unit
        Terminate : unit -> unit
    }
  
[<AbstractClass>]
type AbstractWorker() = 
    
    static let code =  
        let assName = typeof<AbstractWorker>.Assembly.GetName().Name
        let selfName = typeof<AbstractWorker>.FullName 
        String.concat "\n" [
            $"let receiveBinary = Module.mono_bind_static_method(\"[{assName}] {selfName}:ReceiveBinary\");"
            $"let receiveString = Module.mono_bind_static_method(\"[{assName}] {selfName}:ReceiveString\");"
            $"self.onmessage = function(e) {{"
            $"  if (e.data instanceof ArrayBuffer) {{"
            $"    let ptr = Module._malloc(e.data.byteLength);" 
            $"    Module.HEAPU8.set(new Uint8Array(e.data), ptr);"
            $"    receiveBinary(ptr, e.data.byteLength);"
            $"    Module._free(ptr);"
            $"  }} else if(typeof e.data === 'string') {{"
            $"    receiveString(e.data);" 
            $"  }};"
            $"}}"
            $"function sendBinary(ptr, len) {{"
            $"    let data = new Uint8Array(Module.HEAPU8.subarray(ptr, ptr + len)).buffer;"
            $"    self.postMessage(data, [data]);"
            $"}}"
            $"function sendString(str) {{"
            $"    self.postMessage(str);"
            $"}}"
        ]
         
    static let mutable instance = Unchecked.defaultof<AbstractWorker>
        
    let messages = Aardworx.AsyncPrimitives.AsyncBlockingCollection<WorkerMessage>()
        
    abstract Run : WorkerContext -> Task<unit>
                
    member private x.Received(msg : WorkerMessage) =
        messages.Put msg

        
    member private x.RunServerInternal() =
        let send message =
            match message with 
            | WorkerMessage.Binary data ->
                use ptr = fixed data 
                JSRuntime.Instance.InvokeVoid("sendBinary", int (NativePtr.toNativeInt ptr), data.Length)
            | WorkerMessage.String str ->
                JSRuntime.Instance.InvokeVoid("sendString", str)
            
        
        x.Run {
            Send = send
            Receive = messages.Take
            Terminate = fun () -> ()
        }
            
    static member private ReceiveString(str : string) =
        instance.Received (WorkerMessage.String str)
        
    static member private ReceiveBinary(ptr : int, length : int) =
        System.GC.Collect(3, GCCollectionMode.Forced, true, true)
        let arr = Array.zeroCreate<byte> length
        Marshal.Copy(nativeint ptr, arr, 0, length)
        instance.Received (WorkerMessage.Binary arr)
        
    static member private RunServer(typeName : string) =
        JSRuntime.Instance.InvokeVoid("eval", code)
        let t = Type.GetType typeName
        if isNull t then
            failwithf "Type '%s' not found" typeName
        elif typeof<AbstractWorker>.IsAssignableFrom t then
            let ctor = t.GetConstructor(BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Instance ||| BindingFlags.CreateInstance, [||])
            if isNull ctor then
                failwithf "Type '%s' has no default constructor" typeName
            else
                let w = ctor.Invoke([||]) :?> AbstractWorker
                instance <- w
                w.RunServerInternal() |> ignore // TODO: termination?
        else
            failwithf "Type '%s' is not a worker-type" typeName
        
 
module Worker =
    
    let mutable forceGCOnReceive = true
    
    type private Marker = Marker
    
    let private getEmbeddedObjectUrl (replacements : list<string * string>) (name : string) =
        let self = typeof<Marker>.Assembly
        use s = self.GetManifestResourceStream($"{self.GetName().Name}.resources.{name}")
        use r = new System.IO.StreamReader(s)
        let mutable str = r.ReadToEnd()
         
        for (o, n) in replacements do
            str <- str.Replace(o, n)
         
             
        JSRuntime.Instance.Invoke<string>("aardvark.toObjectUrlString", str :> obj, "text/javascript" :> obj)
        
          
    let private verbose = "false" 
     
    let private fakeEnvUrl = lazy ( getEmbeddedObjectUrl [] "spawndev.blazorjs.webworkers.faux-env.js" )
    let private workerUrl =
        lazy (
            let baseUrl = JSRuntime.Instance.Invoke<string>("aardvark.getBaseUrl")
            let location = baseUrl + "_framework/hans.js"
            getEmbeddedObjectUrl [
                "__FAUX_SCRIPT_URL__", fakeEnvUrl.Value
                "__VERBOSE__", verbose
                "__LOCATION__", location
            ] "spawndev.blazorjs.webworkers.js"
        )  
         
    let mutable private currentId = 1
       
    let private workerMessageQueues = Dict<int, Aardworx.AsyncPrimitives.AsyncBlockingCollection<WorkerMessage>>()
    let private workerStart = Dict<int,  TaskCompletionSource<WorkerContext>>()
       
    let private receiveString (id : int) (str : string) =
        match workerMessageQueues.TryGetValue id with
        | (true, q) -> q.Put(WorkerMessage.String str)
        | _ -> ()
        
    let private receiveBinary (id : int) (ptr : int) (len : int) =
        match workerMessageQueues.TryGetValue id with
        | (true, q) ->
            if forceGCOnReceive then
                System.GC.Collect(3, GCCollectionMode.Forced, true, true)
            let arr = Array.zeroCreate<byte> len
            Marshal.Copy(nativeint ptr, arr, 0, len) 
            q.Put(WorkerMessage.Binary arr)  
        | _ -> ()
        
       
    let private initWorkers =
        lazy (
            let code =  
                String.concat "\n" [
                    "try {"
                    "window.workers = {};"
                    "window.workers.list = [];"
                    
                    "window.workers.sendBinary = function(id, ptr, len) {"
                    "    let data = new Uint8Array(Module.HEAPU8.subarray(ptr, ptr + len)).buffer;"
                    "    window.workers.list[id].postMessage(data, [data]);"
                    "};"
                    "window.workers.sendString = function(id, str) {"
                    "    window.workers.list[id].postMessage(str);"
                    "};"
                    
                    "window.workers.receiveString = Module.mono_bind_static_method('[Aardworx.WebAssembly] Aardworx.WebAssembly.Worker:receiveString');"
                    "window.workers.receiveBinary = Module.mono_bind_static_method('[Aardworx.WebAssembly] Aardworx.WebAssembly.Worker:receiveBinary');"
                    "window.workers.booted = Module.mono_bind_static_method('[Aardworx.WebAssembly] Aardworx.WebAssembly.Worker:workerBooted');"
                    "} catch(e) { console.error(e); }"
                ]
            JSRuntime.Instance.InvokeVoid("eval", code)
        )
       
    let private workerBooted(id : int) =
        match workerStart.TryRemove id with
        | (true, w) ->
            
            let queue = Aardworx.AsyncPrimitives.AsyncBlockingCollection()
            workerMessageQueues.[id] <- queue
            w.SetResult {
                Send = fun msg -> 
                    match msg with
                    | WorkerMessage.Binary arr ->
                        use ptr = fixed arr
                        JSRuntime.Instance.InvokeVoid("window.workers.sendBinary", id, int (NativePtr.toNativeInt ptr), arr.Length)
                    | WorkerMessage.String str ->
                        JSRuntime.Instance.InvokeVoid("window.workers.sendString", id, str)
                Receive = queue.Take
                Terminate = fun () ->
                    JSRuntime.Instance.InvokeVoid("window.workers.terminate", id)
                    match workerMessageQueues.TryRemove id with
                    | (true, q) -> q.Completed()
                    | _ -> ()
            }
        | _ ->
            ()
            
         
    let start<'a when 'a :> AbstractWorker and 'a : (new : unit -> 'a)>() =
        initWorkers.Value
        let id = currentId
        currentId <- id + 1
        let typeName = typeof<'a>.AssemblyQualifiedName
        let tcs = TaskCompletionSource<_>()
        workerStart.[id] <- tcs 
        let className = typeof<AbstractWorker>.FullName
        printfn "%A %A" className typeName
        let code =
            String.concat "\n" [
                $"const w = new Worker(\"{workerUrl.Value}\");"
                $"window.workers.list[{id}] = w;"
                $"w.onmessage = (e) => {{"
                $"    let msg = e.data;"
                $"    if (msg === \"ready\") {{"
                $"        w.postMessage(\"Module.mono_bind_static_method('[Aardworx.WebAssembly] {className}:RunServer')('{typeName}');\");"
                $"        window.workers.list[{id}].onmessage = function(msg) {{"
                $"            let data = msg.data;"
                $"            if (data instanceof ArrayBuffer) {{"
                $"                let ptr = Module._malloc(data.byteLength);"
                $"                let arr = new Uint8Array(data);"
                $"                Module.HEAPU8.set(arr, ptr);"
                $"                window.workers.receiveBinary({id}, ptr, data.byteLength);"
                $"                Module._free(ptr);"
                $"            }} else if(typeof data === 'string') {{"
                $"                window.workers.receiveString({id}, data);"
                $"            }} else {{"
                $"                console.error(\"unknown message type\", data);"
                $"            }}"
                $"        }};"
                $"        window.workers.booted({id});"
                $"    }}"
                $"}};"
            ]
        JSRuntime.Instance.InvokeVoid("eval", code)
        tcs.Task
       