namespace Aardworx.Rendering.WebGL

open System
open FSharp.Data.Adaptive
open Aardvark.Base
open Microsoft.FSharp.NativeInterop
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"


[<AllowNullLiteral>]
type ICommandFragment =
    inherit IDisposable
    abstract Tag : obj
    abstract Prev : ICommandFragment with get, set
    abstract Next : ICommandFragment with get, set
    abstract Update : action : (CommandStream -> unit) -> unit
    abstract IsDisposed : bool

type ICommandProgram =
    inherit IDisposable
    abstract State : CommandStreamState
    abstract First : ICommandFragment with get, set
    abstract Last : ICommandFragment with get, set
    abstract InsertAfter : ref : ICommandFragment * ?tag : obj-> ICommandFragment
    abstract InsertBefore : ref : ICommandFragment * ?tag : obj -> ICommandFragment
    abstract Run : AdaptiveToken -> unit

[<AutoOpen>]
module internal FragmentInterpreter =
    [<Struct; StructLayout(LayoutKind.Sequential)>]
    type NativeCommandFragmentInfo =
        {
            prev : nativeptr<NativeCommandFragmentInfo>
            next : nativeptr<NativeCommandFragmentInfo>
            code : nativeint
            size : int
        }   

    [<DllImport("Interpreter")>]
    extern int emRun(NativeCommandFragmentInfo* frag, nativeint stack)

[<AllowNullLiteral>]
type NativeCommandFragment(parent : ICommandProgram, tag : obj) =
    let enc = new Streams.NativeCommandEncoder(parent.State.Device)
    let cmd = new CommandStream(parent.State, enc)

    let mutable prev : NativeCommandFragment = null
    let mutable next : NativeCommandFragment = null
    let mutable isDisposed = false

    let self = 
        let p = NativePtr.alloc<NativeCommandFragmentInfo> 1
        NativePtr.write p { prev = NativePtr.zero; next = NativePtr.zero; code = 0n; size = 0 }
        p

    let updateSelf() =
        let pprev =
            if isNull prev then NativePtr.zero
            else prev.Pointer
            
        let pnext =
            if isNull next then NativePtr.zero
            else next.Pointer

        NativePtr.write self { prev = pprev; next = pnext; code = enc.Pointer; size = enc.Length }
        
        
    member x.Tag = tag
        
    member x.IsDisposed = isDisposed
    member internal x.Pointer = self


    member x.Update(token : AdaptiveToken) =
        cmd.Update token

    member x.Update(action : CommandStream -> unit) =
        cmd.Update(fun cmd ->
            action cmd   
        )
        cmd.Tag <- x
        updateSelf()

    member x.Prev
        with get() = prev
        and set (v : NativeCommandFragment) =
            if v <> prev then
                prev <- v
                updateSelf()
        
    member x.Next
        with get() = next
        and set (v : NativeCommandFragment) =
            if v <> next then
                next <- v
                updateSelf()
        
    member x.Dispose() =
        if not isDisposed then
            isDisposed <- true
            cmd.Dispose()
            if isNull prev then parent.First <- next
            else prev.Next <- next
            if isNull next then parent.Last <- prev
            else next.Prev <- prev
            NativePtr.free self
            prev <- null
            next <- null
    
    interface ICommandFragment with
        member x.Tag = x.Tag
        member x.IsDisposed = isDisposed
        member x.Dispose() = x.Dispose()
        member x.Next
            with get() = x.Next :> _
            and set n = x.Next <- n :?> NativeCommandFragment
        member x.Prev
            with get() = x.Prev :> _
            and set n = x.Prev <- n :?> NativeCommandFragment
        member x.Update(action : CommandStream -> unit) =
            x.Update action

type NativeFragmentProgram(device : Device) =
    inherit AdaptiveObject()

    let mutable first : NativeCommandFragment = null
    let mutable last : NativeCommandFragment = null
    
    let dirty = System.Collections.Generic.HashSet<NativeCommandFragment>()

    let state = new CommandStreamState(device)

    member private x.Mark(reason : NativeCommandFragment) =
        if not (isNull reason) then
            lock dirty (fun () -> dirty.Add reason |> ignore)
        transact x.MarkOutdated

    override x.InputChangedObject(_, o) =   
        match o.Tag with
        | :? NativeCommandFragment as o ->
            lock dirty (fun () -> dirty.Add o |> ignore)
        | _ ->
            ()

    member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> NativeCommandFragment
        let newFrag = new NativeCommandFragment(x, defaultArg tag null)

        if isNull ref then
            let f = first
            newFrag.Next <- f
            newFrag.Prev <- null
            if isNull f then last <- newFrag
            else f.Prev <- newFrag
            first <- newFrag
        else
            let next = ref.Next
            newFrag.Next <- next
            newFrag.Prev <- ref
            ref.Next <- newFrag
            if isNull next then last <- newFrag
            else next.Prev <- newFrag

        x.Mark newFrag
        newFrag
            
    member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> NativeCommandFragment
        let newFrag = new NativeCommandFragment(x, defaultArg tag null)
        if isNull ref then
            let l = last
            newFrag.Next <- null
            newFrag.Prev <- l
            if isNull l then first <- newFrag
            else l.Next <- newFrag
            last <- newFrag
        else   
            let prev = ref.Prev
            newFrag.Next <- ref
            newFrag.Prev <- prev
            ref.Prev <- newFrag
            if isNull prev then first <- newFrag
            else prev.Next <- newFrag

        x.Mark newFrag
        newFrag

    member x.Append() =
        x.InsertBefore null
        
    member x.Prepend() =
        x.InsertAfter null
        
    member x.Run(token : AdaptiveToken) =
        device.Run(fun _gl ->
            state.CurrentContext <- WebGLContext.Current

            x.EvaluateAlways token (fun token ->
                let dirtyFragments = 
                    lock dirty (fun () ->
                        let d = HashSet.toArray dirty
                        dirty.Clear()
                        d
                    )
                for d in dirtyFragments do   
                    if not d.IsDisposed then d.Update token
                
                device.RunPending()
                if emRun(first.Pointer, NativePtr.toNativeInt state.TemporaryStorage) <> 0 then
                    failf "CommandProgram execution failed"
            )
        )


    member x.Dispose() =
        let mutable c = first
        while not (isNull c) do
            let n = c.Next
            c.Dispose()
            c <- n
        dirty.Clear()
        first <- null
        last <- null
        state.Dispose()

    interface ICommandProgram with
        member x.State = state
        member x.Dispose() = 
            x.Dispose()

        member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
            x.InsertAfter(ref, ?tag = tag) :> ICommandFragment
            
        member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
            x.InsertBefore(ref, ?tag = tag) :> ICommandFragment

        member x.First
            with get() = first :> _
            and set f = 
                let f = f :?> NativeCommandFragment
                first <- f
        member x.Last
            with get() = last :> _
            and set f = last <- f :?> NativeCommandFragment

        member x.Run t = x.Run t

        
[<AllowNullLiteral>]
type ManagedCommandFragment(parent : ICommandProgram, mode : CommandStreamMode, tag : obj) =
    let cmd = new CommandStream(parent.State, mode)

    let mutable prev : ManagedCommandFragment = null
    let mutable next : ManagedCommandFragment = null
    let mutable isDisposed = false
    
    member x.Tag = tag
        
    member x.IsDisposed = isDisposed
    member x.Run(gl) = cmd.UnsafePerform gl


    member x.Update(token : AdaptiveToken) =
        cmd.Update token

    member x.Update(action : CommandStream -> unit) =
        cmd.Update(action)
        cmd.Tag <- x
        transact (fun () -> cmd.MarkOutdated())

    member x.Prev
        with get() = prev
        and set (v : ManagedCommandFragment) =
            if v <> prev then
                prev <- v
        
    member x.Next
        with get() = next
        and set (v : ManagedCommandFragment) =
            if v <> next then
                next <- v
        
    member x.Dispose() =
        if not isDisposed then
            isDisposed <- true
            cmd.Dispose()
            if isNull prev then parent.First <- next
            else prev.Next <- next
            if isNull next then parent.Last <- prev
            else next.Prev <- prev
            prev <- null
            next <- null
    
    interface ICommandFragment with
        member x.Tag = x.Tag
        member x.IsDisposed = isDisposed
        member x.Dispose() = x.Dispose()
        member x.Next
            with get() = x.Next :> _
            and set n = x.Next <- n :?> ManagedCommandFragment
        member x.Prev
            with get() = x.Prev :> _
            and set n = x.Prev <- n :?> ManagedCommandFragment
        member x.Update(action : CommandStream -> unit) =
            x.Update action

type ManagedFragmentProgram(device : Device, mode : CommandStreamMode) =
    inherit AdaptiveObject()

    let mutable first : ManagedCommandFragment = null
    let mutable last : ManagedCommandFragment = null
    
    let dirty = System.Collections.Generic.HashSet<ManagedCommandFragment>()

    let state = new CommandStreamState(device)

    member private x.Mark(reason : ManagedCommandFragment) =
        if not (isNull reason) then
            lock dirty (fun () -> dirty.Add reason |> ignore)
        let mark = lock x (fun () -> not x.OutOfDate)
        if mark then transact x.MarkOutdated

    override x.InputChangedObject(_, o) =   
        match o.Tag with
        | :? ManagedCommandFragment as o ->
            lock dirty (fun () -> dirty.Add o |> ignore)
        | _ ->
            ()

    member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> ManagedCommandFragment
        let newFrag = new ManagedCommandFragment(x, mode, defaultArg tag null)

        if isNull ref then
            let f = first
            newFrag.Next <- f
            newFrag.Prev <- null
            if isNull f then last <- newFrag
            else f.Prev <- newFrag
            first <- newFrag
        else
            let next = ref.Next
            newFrag.Next <- next
            newFrag.Prev <- ref
            ref.Next <- newFrag
            if isNull next then last <- newFrag
            else next.Prev <- newFrag

        x.Mark newFrag
        newFrag
            
    member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> ManagedCommandFragment
        let newFrag = new ManagedCommandFragment(x, mode, defaultArg tag null)
        if isNull ref then
            let l = last
            newFrag.Next <- null
            newFrag.Prev <- l
            if isNull l then first <- newFrag
            else l.Next <- newFrag
            last <- newFrag
        else   
            let prev = ref.Prev
            newFrag.Next <- ref
            newFrag.Prev <- prev
            ref.Prev <- newFrag
            if isNull prev then first <- newFrag
            else prev.Next <- newFrag

        x.Mark newFrag
        newFrag

    member x.Append() =
        x.InsertBefore null
        
    member x.Prepend() =
        x.InsertAfter null 
        
    member x.Run(token : AdaptiveToken) =
        device.Run(fun gl ->
            state.CurrentContext <- WebGLContext.Current

            x.EvaluateAlways token (fun token ->
                let dirtyFragments = 
                    lock dirty (fun () ->
                        let d = HashSet.toArray dirty
                        dirty.Clear()
                        d
                    )
                for d in dirtyFragments do   
                    if not d.IsDisposed then d.Update token
                    
                device.Run (fun _ ->
                    device.RunPending()
                    let mutable cnt = 0
                    let mutable c = first
                    while not (isNull c) do
                        c.Run gl
                        c <- c.Next
                        cnt <- cnt + 1
                )
            )
        )

    member x.Dispose() =
        let mutable c = first
        while not (isNull c) do
            let n = c.Next
            c.Dispose()
            c <- n
        dirty.Clear()
        first <- null
        last <- null
        state.Dispose()

    interface ICommandProgram with
        member x.State = state
        member x.Dispose() = 
            x.Dispose()

        member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
            x.InsertAfter(ref, ?tag = tag) :> ICommandFragment
            
        member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
            x.InsertBefore(ref, ?tag = tag) :> ICommandFragment

        member x.First
            with get() = first :> _
            and set f = 
                let f = f :?> ManagedCommandFragment
                first <- f
        member x.Last
            with get() = last :> _
            and set f = last <- f :?> ManagedCommandFragment

        member x.Run t = x.Run t

open Aardworx.WebAssembly

[<AllowNullLiteral>]
type JSCommandFragment(parent : ICommandProgram, tag : obj) =
    let baseStream = new Streams.JSCommandEncoder(parent.State.Device)
    let cmd = new CommandStream(parent.State, baseStream)
    
    
    let o = JsObj.New ["next", null]
    
    let mutable prev : JSCommandFragment = null
    let mutable next : JSCommandFragment = null
    let mutable isDisposed = false

    member x.JsObj = o
    member x.Tag = tag
        
    member x.IsDisposed = isDisposed
    member x.Run(gl) = cmd.UnsafePerform gl


    member x.Update(token : AdaptiveToken) =
        cmd.Update token
        o.SetProperty("run", baseStream.Action)

    member x.Update(action : CommandStream -> unit) =
        cmd.Update(action)
        cmd.Tag <- x
        transact (fun () -> cmd.MarkOutdated())

    member x.Prev
        with get() = prev
        and set (v : JSCommandFragment) =
            if v <> prev then
                prev <- v
        
    member x.Next
        with get() = next
        and set (v : JSCommandFragment) =
            if v <> next then
                next <- v
                if isNull v then o.SetProperty("next", null)
                else o.SetProperty("next", v.JsObj)
        
    member x.Dispose() =
        if not isDisposed then
            isDisposed <- true
            cmd.Dispose()
            if isNull prev then parent.First <- next
            else prev.Next <- next
            if isNull next then parent.Last <- prev
            else next.Prev <- prev
            prev <- null
            next <- null
    
    interface ICommandFragment with
        member x.Tag = x.Tag
        member x.IsDisposed = isDisposed
        member x.Dispose() = x.Dispose()
        member x.Next
            with get() = x.Next :> _
            and set n = x.Next <- n :?> JSCommandFragment
        member x.Prev
            with get() = x.Prev :> _
            and set n = x.Prev <- n :?> JSCommandFragment
        member x.Update(action : CommandStream -> unit) =
            x.Update action

type JSFragmentProgram(device : Device) =
    inherit AdaptiveObject()

    static let runCode =
        String.concat "" [
            "return {"
            "  run: function(o, a) {"
            "    while(o) { o.run.run(a); o = o.next; }"
            "  }"
            "};"
        ]
    // sprintf "(function() { return { run: (self) => { %s } }; })()" (string commands)
    static let runner =
        JsObj.Evaluate<Microsoft.JSInterop.IJSInProcessObjectReference>(runCode)
        //JsObj.Runtime.Invoke<Microsoft.JSInterop.IJSInProcessObjectReference>("eval", $"(function() {{ {runCode} }})()")
    
    let mutable first : JSCommandFragment = null
    let mutable last : JSCommandFragment = null
    
    let dirty = System.Collections.Generic.HashSet<JSCommandFragment>()

    let state = new CommandStreamState(device)

    member private x.Mark(reason : JSCommandFragment) =
        if not (isNull reason) then
            lock dirty (fun () -> dirty.Add reason |> ignore)
        let mark = lock x (fun () -> not x.OutOfDate)
        if mark then transact x.MarkOutdated

    override x.InputChangedObject(_, o) =   
        match o.Tag with
        | :? JSCommandFragment as o ->
            lock dirty (fun () -> dirty.Add o |> ignore)
        | _ ->
            ()

    member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> JSCommandFragment
        let newFrag = new JSCommandFragment(x, defaultArg tag null)

        if isNull ref then
            let f = first
            newFrag.Next <- f
            newFrag.Prev <- null
            if isNull f then last <- newFrag
            else f.Prev <- newFrag
            first <- newFrag
        else
            let next = ref.Next
            newFrag.Next <- next
            newFrag.Prev <- ref
            ref.Next <- newFrag
            if isNull next then last <- newFrag
            else next.Prev <- newFrag

        x.Mark newFrag
        newFrag
            
    member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
        let ref = ref :?> JSCommandFragment
        let newFrag = new JSCommandFragment(x, defaultArg tag null)
        if isNull ref then
            let l = last
            newFrag.Next <- null
            newFrag.Prev <- l
            if isNull l then first <- newFrag
            else l.Next <- newFrag
            last <- newFrag
        else   
            let prev = ref.Prev
            newFrag.Next <- ref
            newFrag.Prev <- prev
            ref.Prev <- newFrag
            if isNull prev then first <- newFrag
            else prev.Next <- newFrag

        x.Mark newFrag
        newFrag

    member x.Append() =
        x.InsertBefore null
        
    member x.Prepend() =
        x.InsertAfter null 
        
    member x.Run(token : AdaptiveToken) =
        device.Run(fun gl ->
            state.CurrentContext <- WebGLContext.Current

            x.EvaluateAlways token (fun token ->
                let dirtyFragments = 
                    lock dirty (fun () ->
                        let d = HashSet.toArray dirty
                        dirty.Clear()
                        d
                    )
                for d in dirtyFragments do   
                    if not d.IsDisposed then d.Update token
                    
                device.Run (fun _ ->
                    device.RunPending()
                    if not (isNull first) then
                        let self = JsObj.New []
                        runner.Invoke("run", first.JsObj.Reference, self.Reference)
                )
            )
        )

    member x.Dispose() =
        let mutable c = first
        while not (isNull c) do
            let n = c.Next
            c.Dispose()
            c <- n
        dirty.Clear()
        first <- null
        last <- null
        state.Dispose()

    interface ICommandProgram with
        member x.State = state
        member x.Dispose() = 
            x.Dispose()

        member x.InsertAfter(ref : ICommandFragment, ?tag : obj) =
            x.InsertAfter(ref, ?tag = tag) :> ICommandFragment
            
        member x.InsertBefore(ref : ICommandFragment, ?tag : obj) =
            x.InsertBefore(ref, ?tag = tag) :> ICommandFragment

        member x.First
            with get() = first :> _
            and set f = 
                let f = f :?> JSCommandFragment
                first <- f
        member x.Last
            with get() = last :> _
            and set f = last <- f :?> JSCommandFragment

        member x.Run t = x.Run t


[<AbstractClass; Sealed; Extension>]
type FragmentProgramDeviceExtensions private() =
    [<Extension>]
    static member CreateCommandProgram(this : Device, mode : CommandStreamMode) =
        match mode with
        | CommandStreamMode.Managed ->
            printfn "USING MANAGED FRAGMENT PROGRAM"
            new ManagedFragmentProgram(this, CommandStreamMode.Managed) :> ICommandProgram
        | CommandStreamMode.Debug ->
            printfn "USING DEBUG FRAGMENT PROGRAM"
            new ManagedFragmentProgram(this, CommandStreamMode.Debug) :> ICommandProgram
        | CommandStreamMode.Javascript ->
            printfn "USING JS FRAGMENT PROGRAM"
            new JSFragmentProgram(this) :> ICommandProgram
    
