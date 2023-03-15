namespace Aardworx.Rendering.WebGL

open System.Text
open Aardvark.Base
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardworx.Rendering.WebGL
open Aardworx.WebAssembly
open Microsoft.JSInterop

type JSCommandEncoder() =
    inherit AdaptiveObject()
    
    static let mutable currentId = 0
    
    static let newId() =
        currentId <- currentId + 1
        currentId
    
    
    let commands = StringBuilder()
    let handles = JsObj.New []
    let entries = System.Collections.Generic.Dictionary<string, IAdaptiveValue>()
    let keys = Dict<IAdaptiveValue, System.Collections.Generic.HashSet<string>>()
    let dirty = Dict<IAdaptiveValue, System.Collections.Generic.HashSet<string>>()
    let ptrs = Dict<IAdaptiveObject, System.Collections.Generic.HashSet<AdaptivePointer>>()
    let dirtyPtrs = System.Collections.Generic.HashSet<AdaptivePointer>()
    
    let mutable cachedAction = None
    
    let register (this : JSCommandEncoder) (value : aval<'a>) =
        let id = newId() |> sprintf "h%04d"
        entries.[id] <- value
        keys.GetOrCreate(value, fun _ -> System.Collections.Generic.HashSet()).Add id |> ignore
        dirty.GetOrCreate(value, fun _ -> System.Collections.Generic.HashSet()).Add id |> ignore
        if not this.OutOfDate then transact this.MarkOutdated
        id
    
    let appendCommand (str : string) =
        cachedAction <- None
        commands.AppendFormat("{0};", str) |> ignore
    
    override x.InputChangedObject(_, o) =
        match ptrs.TryGetValue o with
        | (true, ptrs) ->
            dirtyPtrs.UnionWith ptrs
        | _ ->
            ()
            
        match o with
        | :? IAdaptiveValue as o ->
            match keys.TryGetValue o with
            | (true, keys) ->
                let set = dirty.GetOrCreate(o, fun _ -> System.Collections.Generic.HashSet())
                set.UnionWith keys
            | _ ->
                ()
        | _ ->
            ()
            
    member x.Test(value : aval<string>) =
        let id = register x value
        appendCommand(sprintf "console.log(self.%s)" id)
   
    member x.Seppy(value : aptr<int>) =
        value.Acquire()
        ptrs.GetOrCreate(value.Input, fun _ -> System.Collections.Generic.HashSet()).Add value |> ignore
        dirtyPtrs.Add value |> ignore
        appendCommand $"console.log(Module.HEAPU32.at({int (value.Pointer / 4n)}))"
   
    member x.Run(token : AdaptiveToken) =
        x.EvaluateAlways token (fun token ->
            x.OutOfDate <- true    
        
            let action = 
                match cachedAction with
                | Some a -> a
                | None ->
                    let code = sprintf "(function() { return { run: (self) => { %s } }; })()" (string commands)
                    let a = JS.invoke<IJSInProcessObjectReference> "window.eval" [|code|]
                    cachedAction <- Some a
                    a
                    
            for KeyValue(object, keys) in dirty do
                let value = object.GetValueUntyped token
                for k in keys do
                    handles.SetProperty(k, value)
                    
            for ptr in dirtyPtrs do
                ptr.Update token
                    
            dirty.Clear()
            dirtyPtrs.Clear()
            
            action.Invoke("run", [| handles.Reference :> obj |])
        )
        
    member x.Run() = x.Run AdaptiveToken.Top

