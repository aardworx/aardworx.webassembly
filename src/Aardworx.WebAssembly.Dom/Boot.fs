namespace Aardworx.WebAssembly.Dom

open Aardvark.Base
open Aardvark.Dom
open Aardvark.Dom.Remote
open Aardworx.AsyncPrimitives
open Aardworx.WebAssembly
open Microsoft.JSInterop
open System.Threading.Tasks
open System
open FSharp.Data.Adaptive
open System.Threading
open System.Web
open Aardworx.Rendering.WebGL
open Aardvark.Application

type HtmlBackend(app : WebGLApplication, server : IServer) =
    inherit AbstractRemoteHtmlBackend(server)
        
    static let initTable = Dict<string, unit -> unit>()
    static let disposables = Dict<int64, IDisposable>()
    
    [<JSInvokable>]
    static member Init(id : string) =
        match initTable.TryRemove id with
        | (true, a) -> a()
        | _ -> ()

    override x.Clone() =
        HtmlBackend(app, server)

    override x.SetupRenderer(a, b) =
        let id = Guid.NewGuid() |> string
        transact (fun () -> b.ClearColor.Value <- C4f(0.0f, 0.0f, 0.0f, 0.0f))
    
        x.Code.AppendLine $"Module.mono_bind_static_method('[Aardworx.WebAssembly.Dom] Aardworx.WebAssembly.Dom.HtmlBackend:Init')('{id}');"
        initTable.[id] <- fun () ->
            let parentDiv = Window.GetProperty<JsObj>("aardvark").Invoke<HTMLElement>("get", [| int a :> obj |])
 
            let antialias = 
                if parentDiv.HasAttribute "data-antialiasing" then
                    match string(parentDiv.GetAttribute "data-antialiasing").ToLower() with
                    | "fxaa" -> Antialiasing.FXAA
                    | _ -> 
                        Log.warn "MSAA currently broken"
                        Antialiasing.FXAA
                        //match Int32.TryParse(string (parentDiv.GetAttribute "data-samples")) with
                        //| (true, s) -> Antialiasing.MSAA s
                        //| _ -> Antialiasing.MSAA 4
                else
                    Antialiasing.None
                    //match Int32.TryParse(string (parentDiv.GetAttribute "data-samples")) with
                    //| (true, s) -> Antialiasing.MSAA s
                    //| _ -> Antialiasing.MSAA 4
              
 
            let canvas = Window.Document.CreateCanvasElement()
            canvas.Style.Width <- "100%"
            canvas.Style.Height <- "100%"
            canvas.Style.SetProperty("outline", "none", "important")
            parentDiv.AppendChild canvas
            
            //let sub = 
            //    b.Cursor.AddCallback(fun c ->
            //        let newCursor = 
            //            match c with
            //            | Some Cursor.Default -> None
            //            | Some Cursor.Hand -> Some "pointer"
            //            | Some Cursor.Arrow -> Some "default"
            //            | Some Cursor.HorizontalResize -> Some "ew-resize"
            //            | Some Cursor.VerticalResize -> Some "ns-resize"
            //            | Some Cursor.Crosshair -> Some "crosshair"
            //            | Some Cursor.None -> Some "none"
            //            | Some Cursor.Text -> Some "text"
            //            | Some (Cursor.Custom _) -> None
            //            | None -> None
            //        match newCursor with
            //        | Some c -> canvas.Style.Cursor <- c
            //        | None -> canvas.Style.RemoveProperty "cursor"
            //    )
                   
            
            let ctrl = app.CreateRenderControl(canvas, antialias)
            ctrl.ClearColor <- C4f(0.0f, 0.0f, 0.0f, 0.0f)
            ctrl.RenderTask <- b.RenderTask
            
            disposables.[a] <-
                { new IDisposable with
                    member x.Dispose() =
                        ctrl.Dispose()
                }

            
            
    override x.DestroyRenderer(a, b) =
        match disposables.TryRemove a with
        | (true, d) -> d.Dispose()
        | _ -> ()

module Boot =


    let private connectCode =
        """
            (function(){
                const connector = function(cont) {
                    aardvark.newSocket = function (name) {
                        return new window.BlazorSocket(name);
                    };

                    aardvark.logCode = false;

                    aardvark.onReady(() => {

                        function receive(data) {
                            if (!data) return;
                            try {
                                const msg = JSON.parse(data);
                                switch (msg.command) {
                                    case "execute":
                                        if(aardvark.logCode) { console.debug(msg.code); }
                                        try { new Function(msg.code)(); }
                                        catch (e) { console.error("bad code", msg.code, e); }
                                        break;
                                    default:
                                        console.log(msg);
                                        break;
                                }
                            }
                            catch (e) {
                                console.error("bad message", data);
                            }
                        }

                        function runSocket(path, receive) {
                            let messageBuffer = [];
                            let socket = new window.BlazorSocket(path);
                            let connected = false;

                            let result =
                            {
                                socket: null,
                                send: (msg) => { messageBuffer.push(msg); }
                            }

                            socket.onopen = () => {
                                connected = true;
                                try {
                                    while (messageBuffer.length > 0) {
                                        const m = messageBuffer[0];
                                        socket.send(m);
                                        messageBuffer.splice(0, 1);
                                    }
                                }
                                catch (e) { }
                            };
                            socket.onmessage = (event) => {
                                if (event.data) {
                                    if (event.data !== "!pong") receive(event.data);
                                }
                            };
                            socket.onerror = () => { };
                            socket.onclose = () => {
                                connected = false;
                                console.log("reconnect");
                                setTimeout(() => {
                                    const res = runSocket(path, receive);
                                    result.socket = res.socket;
                                    result.send = res.send;
                                }, 500);
                            };

                       
                            function send(msg) {
                                if (connected) socket.send(msg);
                                else messageBuffer.push(msg);
                            }
                            result.send = send;
                            result.socket = socket;

                            return result;
                        }


                        let ws = runSocket("main", receive);
                        cont((msg) => { ws.send(msg); });
                    });
                };
                aardvark.connect(connector);
            })()
        """
   
    let runView (app : WebGLApplication) (view : DomContext -> DomNode) =
        
        let server =
            { new IServer with
                member x.RegisterWebSocket(action : IChannel -> Task<_>) =
                    let name = Guid.NewGuid() |> string
                    let mutable socket = None
                    let mutable self = { new IDisposable with member x.Dispose() = () }

                    self <-
                        Aardworx.WebAssembly.Dom.BlazorSocket.OnConnect.Subscribe (fun s ->
                            if s.Name = name then
                                socket <- Some s
                                let channel =
                                    { new IChannel with
                                        member x.Receive() = s.Receive()
                                        member x.Send msg = Task.FromResult (s.Send msg)
                                        member x.OnClose = s.OnClose
                                    }
                                action channel |> ignore
                                self.Dispose()
                        )
                    
                    let kill() =
                        self.Dispose()
                        match socket with
                        | Some s ->
                            socket <- None
                            s.Send ChannelMessage.Close
                        | None ->
                            ()

                    name, { new IDisposable with member x.Dispose() = kill() }
                    
                member x.RegisterResource(a, b, c) =
                    let name = Guid.NewGuid()
                    string name
            }
 
        let runUpdater (state : UpdateState<int64>) (b : HtmlBackend) (action : string -> unit) (updater : Updater<int64>) =
            let mutable running = true
            let signal = AsyncAutoResetEvent(true)
            let sub = updater.AddMarkingCallback signal.Pulse

            task {
                while running do
                    do! signal.Wait()
                    if running then
                        do! Async.SwitchToThreadPool()
                        try
                            updater.Update(state, b)
                            let code = b.GetCode().Trim()
                            if code <> "" then
                                action code
                        with e ->
                            printfn "update failed: %A" e
                sub.Dispose()

            }
            
        BlazorSocket.OnConnect.Add (fun s ->
            if s.Name = "main" then
                let backend = HtmlBackend(app, server)
                
                let execute (code : string) =
                    let code = sprintf "try { %s } catch (e) { console.error(e); }" code
                    
                    let data = 
                        System.Text.StringBuilder()
                            .Append("{")
                            .Append("\"command\": \"execute\", ")
                            .AppendFormat("\"code\": \"{0}\"", HttpUtility.JavaScriptStringEncode code)
                            .Append("}")
                            .ToString()
                    s.Send(ChannelMessage.Text data)
                    
                let mutable id = 0
                let newId() =
                    let o = id
                    id <- o + 1
                    o
                
                let callbackTable = Aardvark.Base.Dict()

                let run (code : string) (callback : System.Text.Json.JsonElement -> unit) = 
                    let mid = newId()
                    let midStr = sprintf "%08X" mid
                    callbackTable.[mid] <- callback
                    let body =
                        String.concat "\n" [
                            $"(function() {{"
                            $"  function run() {{"
                            $"      {code}"
                            $"  }}"
                            $"  let res = run();"
                            $"  if(!res) res = null;"
                            $"  aardvark.send('#{midStr}' + aardvark.stringify(res));"
                            $"}})();"
                        ]
                    execute body

                
                let receiver =
                    task {
                        while true do
                            let! msg = s.Receive()
                            match msg with
                            | ChannelMessage.Text json ->
                                if json.StartsWith "#" then
                                    let id = System.Int32.Parse(json.Substring(1, 8), System.Globalization.NumberStyles.HexNumber)
                                    match callbackTable.TryRemove id with
                                    | (true, cb) -> 
                                        let msg = System.Text.Json.JsonDocument.Parse(json.Substring 9).RootElement
                                        cb msg
                                    | _ ->
                                        ()
                                else
                                    try
                                        let msg = System.Text.Json.JsonDocument.Parse(json).RootElement
                                        let id = msg.GetProperty("source").GetInt64()
                                        let typ = msg.GetProperty("type").GetString()
                                        let data = msg.GetProperty "data"
                                        backend.RunCallback(id, typ, data)
                                    with _ ->
                                        ()
                            | _ ->
                                ()
                    }
                    
                let context : DomContext =
                    { new DomContext with
                        member x.Runtime = app.Runtime
                        member x.Execute code callback =
                            match callback with
                            | Some callback -> run code callback
                            | None -> execute code
                        member x.StartWorker<'t, 'a, 'b when 't :> Aardvark.Dom.AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() =
                            WorkerExtensions.run<'t, 'a, 'b>()
                    }
                let dom = view context

                let u = Updater.Body(app.Runtime, dom, backend :> IHtmlBackend<_>)

                
                let state = { token = AdaptiveToken.Top; runtime = app.Runtime }
                
                u |> runUpdater state backend execute |> ignore

                ()
        )

        let aardvarkDomJs = 
            let a = typeof<AbstractRemoteHtmlBackend>.Assembly
            use s = a.GetManifestResourceStream(a.GetName().Name + ".aardvark-dom.js")
            use r = new System.IO.StreamReader(s)
            r.ReadToEnd()
            
        JSRuntime.Instance.InvokeVoid("window.eval", aardvarkDomJs)
        JSRuntime.Instance.InvokeVoid("window.eval", connectCode)

    let run (glapp : WebGLApplication) (app : App<'model, 'adaptiveModel, 'message>) =
        runView glapp (fun ctx ->
            let mutable model = app.initial
            let adaptiveModel = app.unpersist.init model
            
            let queue = AsyncBlockingCollection<seq<'message>>()
            
            let additional = clist []
            
            let env =
                { new Env<'message> with
                    member x.RunModal(action) =
                        let idx = additional.Value.NewIndexAfter(additional.MaxIndex)
                        let disp =
                            { new IDisposable with
                                member x.Dispose() =
                                    transact (fun () -> additional.Remove idx |> ignore)
                            }
                        let dom = action disp
                        transact (fun () -> additional.[idx] <- dom)
                        disp
                        
                    member x.Emit msgs =
                        queue.Put msgs

                    member x.Run(js, callback) =
                        ctx.Execute js callback

                    member x.Runtime = glapp.Runtime
                    
                    member x.StartWorker<'t, 'a, 'b when 't :> AbstractWorker<'a, 'b> and 't : (new : unit -> 't)>() =
                        ctx.StartWorker<'t, 'a, 'b>()
                }

            let loop =
                task {
                    while true do
                        let! msg = queue.Take()
                        model <- (model, msg) ||> Seq.fold (fun m msg -> app.update env m msg)
                        transact (fun () ->
                            app.unpersist.update adaptiveModel model
                        )
                        do! Task.Yield()
                }
                
            let root = app.view env adaptiveModel
            match root with
            | DomNode.Element(tag, atts, cs) ->
                DomNode.Element(tag, atts, AList.append cs additional)
            | _ ->
                Log.warn "could not append additional nodes to root"
                root
        )
        