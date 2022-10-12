namespace rec Aardworx.WebAssembly

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Microsoft.JSInterop
open Microsoft.JSInterop.WebAssembly
open Aardvark.Base


#nowarn "9"
[<AutoOpen>]
module private JSHelpers =
    
    [<Literal>]
    let private aardvarkScript = 
        """
        if (typeof aardvark == "undefined") {
            const isWorker = typeof window == "undefined";
            let aardvark = {};
            if(isWorker) self.aardvark = aardvark;
            else window.aardvark = aardvark;

            const wobj = isWorker ? self : window;

            aardvark.getBaseUrl = function() {
                return wobj.location.href;
            };

            aardvark.wrap = function (v) {
                return v;

                if (v) {
                    if (typeof v === "object") return DotNet.createJSObjectReference(v);
                    else return v;
                }
                else return null;
            };

            aardvark.getProperty = function (obj, name) {
                return aardvark.wrap(obj[name]);
            };

            aardvark.setProperty = function (obj, name, value) {
                obj[name] = value;
            };

            aardvark.tryGetProperty = function (obj, name) {
                if (name in obj) return aardvark.wrap({ value: obj[name] });
                else return null;
            };

            aardvark.getGlobalObject = function () {
                return aardvark.wrap(wobj);
            };

            aardvark.addEventListener = function (target, name, callback, phase) {
                const handler = { handleEvent: function (a) { callback.invokeMethodAsync('Invoke', DotNet.createJSObjectReference(a)); } };
                target.addEventListener(name, handler, phase);
                return DotNet.createJSObjectReference({ dispose: function() { target.removeEventListener(name, handler, phase); } });
            };

            aardvark.requestAnimationFrame = function (callback) {
                if(isWorker) setTimeout(function () { callback.invokeMethodAsync('Invoke'); }, 0);
                else window.requestAnimationFrame(function () { callback.invokeMethodAsync('Invoke'); });
            };

            aardvark.setTimeout = function (time, callback) {
                return setTimeout(function () { callback.invokeMethodAsync('Invoke'); }, time);
            };

            

            aardvark.newObject = function (ctor, ...args) {
                if (ctor) return aardvark.wrap(new wobj[ctor](...args));
                else return aardvark.wrap({});
            };

            aardvark.loadImage = function (offset, size, callback) {
                const blob = new Blob([HEAPU8.subarray(offset, offset + size)], { type: "image/jpeg"});
                const image = new Image();
                let url = null;
                image.onload = function (e) {
                    const canvas = document.createElement("canvas");
                    canvas.width = image.width;
                    canvas.height = image.height;
                    const ctx = canvas.getContext("2d");
                    ctx.drawImage(image, 0, 0);
                    const img = ctx.getImageData(0, 0, image.width, image.height);

                    var buf = Module._malloc(img.data.length);
                    Module.HEAPU8.set(img.data, buf);
                    callback.invokeMethod('Invoke', img.width, img.height, buf);
                    Module._free(buf);
                    URL.revokeObjectURL(url);
                };
                url = URL.createObjectURL(blob);
                image.src = url;
                
            };


            aardvark.loadImageURL = function (url, callback) {
                const image = new Image();
                image.onload = function (e) {
                    const canvas = document.createElement("canvas");
                    const ctx = canvas.getContext("2d");
                    ctx.drawImage(image, 0, 0);
                    const img = ctx.getImageData(0, 0, image.width, image.height);

                    var buf = Module._malloc(img.data.length);
                    Module.HEAPU8.set(img.data, buf);
                    callback.invokeMethodAsync('Invoke', img.width, img.height, buf);
                    Module._free(buf);
                };
                image.src = url;

            };

            aardvark.mountFileSystem = function(callback) {
                FS.mkdir('/aardvark');
                FS.mount(IDBFS, {}, '/aardvark');
                FS.syncfs(true, function (err) {
                    if(err) { console.error(err); }
                    else { callback.invokeMethodAsync('Invoke'); }
                });
            };
            
            aardvark.syncFileSystem = function(callback) {
                FS.syncfs(false, function (err) {
                    if(err) { console.error(err); }
                    else { callback.invokeMethodAsync('Invoke'); }
                });
            }


        }
        """
        
    let runtime =
        lazy (
            let args = [| "Web"; "WebGL2" |]
            let builder = WebAssemblyHostBuilder.CreateDefault(args)

            let host = builder.Build()
            let res = host.Services.GetService(typeof<IJSRuntime>) :?> WebAssemblyJSRuntime
            CoreUtilities.installScript aardvarkScript
            res
        )
        



    let js (o : obj) =
        match o with
        | :? JsObj as o -> o.Reference :> obj
        | o -> o


    let newObj (fields : #seq<string * obj>) =
        let runtime = runtime.Value
        let o = runtime.Invoke<IJSInProcessObjectReference>("aardvark.newObject") |> JsObj
        for k, v in fields do 
            o.SetProperty<obj>(k, v)
        o

    let createObj (ctor : string) (args : array<obj>) : JsObj =
        let runtime = runtime.Value
        let arr = Array.zeroCreate (args.Length + 1)
        arr.[0] <- ctor :> obj
        let mutable i = 1
        for a in args do arr.[i] <- js a; i <- i + 1 
        runtime.Invoke<IJSInProcessObjectReference>("aardvark.newObject", arr) |> JsObj

    type ObjectInvoker<'a>() =
        static let invoke : IJSInProcessObjectReference -> string -> array<obj> -> 'a =
            let t = typeof<'a>
            if t = typeof<unit> then
                unbox <| fun (o : IJSInProcessObjectReference) (name : string) (args : array<obj>) -> o.InvokeVoid(name, Array.map js args)
            elif t = typeof<JsObj> then
                unbox <| fun (o : IJSInProcessObjectReference) (name : string) (args : array<obj>) -> 
                    try
                        let r = o.Invoke<IJSInProcessObjectReference>(name, Array.map js args)
                        if isNull r then null
                        else JsObj(r)
                    with _ ->
                        null
            elif typeof<JsObj>.IsAssignableFrom t then
                let allFlags =
                    System.Reflection.BindingFlags.Public ||| 
                    System.Reflection.BindingFlags.NonPublic ||| 
                    System.Reflection.BindingFlags.Instance ||| 
                    System.Reflection.BindingFlags.CreateInstance
                let ctor = t.GetConstructor(allFlags, [| typeof<IJSInProcessObjectReference> |])
                if isNull ctor then failwithf "cannot construct %s(IJSInProcessObjectReference)" t.Name
                fun (o : IJSInProcessObjectReference) (name : string) (args : array<obj>) -> 
                    try
                        let r = o.Invoke<IJSInProcessObjectReference>(name, Array.map js args)
                        if isNull r then Unchecked.defaultof<'a>
                        else ctor.Invoke([|r|]) |> unbox<'a>
                    with _ ->
                        Unchecked.defaultof<'a>
            elif t = typeof<obj> then
                unbox <| fun (o : IJSInProcessObjectReference) (name : string) (args : array<obj>) -> 
                    match o.Invoke<obj>(name, Array.map js args) with
                    | :? IJSInProcessObjectReference as o -> JsObj(o) :> obj
                    | o -> o
            else
                unbox <| fun (o : IJSInProcessObjectReference) (name : string) (args : array<obj>) -> o.Invoke<'a>(name, Array.map js args)
    
        static member Invoke(o : IJSInProcessObjectReference, name : string, args : obj[]) =
            let runtime = runtime.Value
            invoke o name args

    type Invoker<'ret>() =
        static let invoke : string -> obj[] -> 'ret =
            let t = typeof<'ret>
            if t = typeof<unit> then
                unbox <| fun (name : string) (args : array<obj>) ->  runtime.Value.InvokeVoid(name, Array.map js args)
            elif t = typeof<JsObj> then
                unbox <| fun (name : string) (args : array<obj>) -> 
                    try
                        let r = runtime.Value.Invoke<IJSInProcessObjectReference>(name, Array.map js args)
                        if isNull r then null
                        else JsObj(unbox r)
                    with _ ->
                        null
            elif typeof<JsObj>.IsAssignableFrom t then
                let allFlags =
                    System.Reflection.BindingFlags.Public ||| 
                    System.Reflection.BindingFlags.NonPublic ||| 
                    System.Reflection.BindingFlags.Instance ||| 
                    System.Reflection.BindingFlags.CreateInstance
                let ctor = t.GetConstructor(allFlags, [| typeof<IJSInProcessObjectReference> |])
                if isNull ctor then failwithf "cannot construct %s(IJSInProcessObjectReference)" t.Name
                fun (name : string) (args : array<obj>) -> 
                    try
                        let r = runtime.Value.Invoke<IJSInProcessObjectReference>(name, Array.map js args)
                        if isNull r then Unchecked.defaultof<'ret>
                        else ctor.Invoke([|r|]) |> unbox<'ret>
                    with _ ->
                        Unchecked.defaultof<'ret>
            elif t = typeof<obj> then
                unbox <| fun (name : string) (args : array<obj>) -> 
                    match runtime.Value.Invoke<obj>(name, Array.map js args) with
                    | :? IJSInProcessObjectReference as o -> JsObj(o) :> obj
                    | o -> o
            else
                unbox <| fun (name : string) (args : array<obj>) -> 
                    runtime.Value.Invoke<'ret>(name, Array.map js args)

        static member Invoke(method : string, args : obj[]) =
            let runtime = runtime.Value
            invoke method args



module JSActions =
    type JSAction(action : unit -> unit) =
        [<JSInvokable("Invoke")>]
        member x.Invoke() = action()
 
    type JSActionImage(action : int -> int -> nativeint -> unit) =
        
        [<JSInvokable("Invoke")>]
        member x.Invoke(w : int, h : int, ptr : int) = action w h (nativeint ptr)


    type JSActionO(action : IJSInProcessObjectReference -> unit) =

        [<JSInvokable("Invoke")>]
        member x.Invoke(o : IJSInProcessObjectReference) = action o
        
    type JSActionString(action : string -> unit) =
        
        [<JSInvokable("Invoke")>]
        member x.Invoke(value : string) = action value
                
    type JSActionPtrInt(action : nativeint -> int -> unit) =
        
        [<JSInvokable("Invoke")>]
        member x.Invoke(data : int, len : int) = action (nativeint data) len
        
    type JSActionStringString(action : string -> string -> unit) =
        
        [<JSInvokable("Invoke")>]
        member x.Invoke(a : string, b : string) = action a b
                



[<AllowNullLiteral>]
type JsObj(r : IJSInProcessObjectReference) =

    static member Runtime = runtime.Value

    member x.Reference : IJSInProcessObjectReference = r

    member x.Invoke<'a>(meth : string, args : obj[]) =
        ObjectInvoker<'a>.Invoke(r, meth, args)
        
    member x.GetProperty<'a>(name : string) =
        Invoker<'a>.Invoke("aardvark.getProperty", [| r :> obj; name :> obj |])
        
    member x.SetProperty<'a>(name : string, value : 'a) : unit=
        Invoker<unit>.Invoke("aardvark.setProperty", [| r :> obj; name :> obj; js value |])
  
  