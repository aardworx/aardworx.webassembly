namespace Aardworx.WebAssembly.Dom


open Aardvark.Base
open Aardvark.Dom
open Aardworx.WebAssembly
open Microsoft.JSInterop
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open System.Threading.Tasks
open System.Threading

#nowarn "9"

type private AsyncSignal(isSet : bool) =
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
            
type AsyncCollection<'a>() =
    let queue = System.Collections.Generic.Queue<'a>()

    let signal = AsyncSignal(false)
    
    member x.Take() =
        lock queue (fun () ->
            if queue.Count > 0 then
                Task.FromResult(queue.Dequeue())
            else
                task {
                    do! signal.Wait()
                    return! x.Take()
                }
        )
    
    member x.Put(value : 'a) =
        lock queue (fun () ->
            queue.Enqueue value
            signal.Pulse()
        )
    
    
type BlazorSocket private(name : string) =


    static let code =
        """
            (function() {
            
                let recvString = Module.mono_bind_static_method("[Aardworx.WebAssembly.Dom] Aardworx.WebAssembly.Dom.BlazorSocket:ReceiveString");
                let recvBinary = Module.mono_bind_static_method("[Aardworx.WebAssembly.Dom] Aardworx.WebAssembly.Dom.BlazorSocket:ReceiveBinary");
                let connect = Module.mono_bind_static_method("[Aardworx.WebAssembly.Dom] Aardworx.WebAssembly.Dom.BlazorSocket:Connect");
                let disconnect = Module.mono_bind_static_method("[Aardworx.WebAssembly.Dom] Aardworx.WebAssembly.Dom.BlazorSocket:Disconnect");
            
                const allSockets = {};
            
                window.BlazorSocketReceive = function(name, a, b) {
                    let sock = allSockets[name]
                    if(sock) {
                        let msg = a;
                        if(typeof a === "number" && typeof b === "number") {
                            const arr = HEAPU8.subarray(a, a + b)
                            const res = Uint8Array(arr)
                            msg = res.buffer;
                        }

                        if(msg) { sock.trigger({ data: msg }); }
                        else sock.close();
                    }
                };
                
            
                class BlazorSocket {
                    constructor(name) {
                        this.name = name;
                        allSockets[name] = this;
                        connect(name);
                    }

                    set onopen(value) {
                        value();
                    }
                    
                    set onmessage(callback) {
                        this.callback = callback;
                    }
                    
                    trigger(e) {
                        if(this.callback) this.callback(e);
                    }

                    send(msg) {
                        if(msg instanceof ArrayBuffer) {
                            const ptr = Module._malloc(msg.byteLength);
                            const heapBytes = new Uint8Array(Module.HEAPU8.buffer, ptr, msg.byteLength);
                            heapBytes.set(new Uint8Array(msg));
                            recvBinary(this.name, ptr, msg.byteLength);
                            Module._free(ptr);
                        }
                        else if(typeof msg === "string") {
                            recvString(this.name, msg);
                        }
                        else {
                            console.error("bad message: ", msg);
                        }
                    }

                    close() {
                        delete allSockets[this.name];
                        disconnect(this.name);
                    }

                };
                
                window.BlazorSocket = BlazorSocket;
                

            })();
        """
        
    static let all = Dict<string, BlazorSocket>()
    
    static let init = lazy (JsObj.Runtime.InvokeVoid("window.eval", code))

    static let onConnect = Event<BlazorSocket>()

    let buffer = AsyncCollection<ChannelMessage>()
    let onClose = Event<unit>()
    
    do init.Value

    member x.Name = name
    
    member x.OnClose = onClose.Publish

    member private x.TriggerClose() =
        buffer.Put ChannelMessage.Close
        onClose.Trigger()
        
    member private x.Trigger(message : ChannelMessage) =
        buffer.Put message
        
    member x.Receive() =
        buffer.Take()

    member x.Send(message : ChannelMessage) =
        match message with
        | ChannelMessage.Text message ->
            JsObj.Runtime.InvokeVoid("window.BlazorSocketReceive", name, message)
            
        | ChannelMessage.Binary data ->
            use ptr = fixed data
            JsObj.Runtime.InvokeVoid("window.BlazorSocketReceive", int (NativePtr.toNativeInt ptr), data.Length)
            
        | ChannelMessage.Close ->
            JsObj.Runtime.InvokeVoid("window.BlazorSocketReceive", name)
        
        
    static member OnConnect = 
        init.Value
        onConnect.Publish
    
    static member Connect(name : string) =
        if not (all.ContainsKey name) then
            let s = BlazorSocket(name)
            all.[name] <- s
            onConnect.Trigger(s)
            
    static member Disconnect(name : string) =
        match all.TryRemove name with
        | (true, o) ->
            o.TriggerClose()
        | _ ->
            ()
            
    static member ReceiveString(name : string, message : string) =
        match all.TryGetValue name with
        | (true, s) -> s.Trigger(ChannelMessage.Text message)
        | _ -> 
            let s = BlazorSocket(name)
            all.[name] <- s
            onConnect.Trigger(s)
            s.Trigger(ChannelMessage.Text message)
        
    static member ReceiveBinary(name : string, ptr : int, len : int) =
        let ptr = nativeint ptr
        let res = Array.zeroCreate<byte> len
        Marshal.Copy(ptr, res, 0, res.Length)
        match all.TryGetValue name with
        | (true, s) -> 
            s.Trigger(ChannelMessage.Binary res)
        | _ ->
            let s = BlazorSocket(name)
            all.[name] <- s
            onConnect.Trigger(s)
            s.Trigger(ChannelMessage.Binary res)
            
    static member Get(name : string) =
        match all.TryGetValue name with
        | (true, s) -> s
        | _ -> 
            let s = BlazorSocket(name)
            all.[name] <- s
            s

