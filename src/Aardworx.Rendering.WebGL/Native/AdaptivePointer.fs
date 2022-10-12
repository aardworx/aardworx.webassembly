namespace Aardworx.WebAssembly

open System
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open Aardvark.Base
open FSharp.Data.Adaptive
open System.Buffers

#nowarn "9"
#nowarn "4321"


type IRefCounted =
    inherit IDisposable
    abstract AddReference : unit -> unit
    

[<AbstractClass; Sealed>]
type internal RefCounter<'a> private() =
    static let acquire = 
        if typeof<IRefCounted>.IsAssignableFrom typeof<'a> then
            fun (value : 'a) ->
                let r = value :> obj :?> IRefCounted
                r.AddReference()
        else
            fun (_ : 'a) -> ()

    static let release =
        if typeof<IRefCounted>.IsAssignableFrom typeof<'a> then
            fun (value : 'a) ->
                let r = value :> obj :?> IRefCounted
                r.Dispose()
        else
            fun (_ : 'a) -> ()
        
    static member Acquire(value : 'a) =
        acquire value
        

    static member Release(value : 'a) =
        release value

type AdaptivePointer =
    abstract Input : IAdaptiveObject
    abstract IsConstant : bool
    abstract IsVolatile : bool
    abstract Update : token : AdaptiveToken -> unit
    abstract Acquire : unit -> unit
    abstract Release : unit -> unit
    abstract Pointer : nativeint
    abstract Release : int -> unit
    
type AdaptivePointer<'a when 'a : unmanaged> =
    inherit AdaptivePointer
    abstract Value : 'a

type aptr<'a when 'a : unmanaged> = AdaptivePointer<'a>

module APtr =

    let inline private (!) (a : ref<'a>) = a.Value
    let inline private (:=) (a : ref<'a>) (value : 'a) = a.Value <- value

    module private Memory =
        //let mutable count = 0

        let alloc<'a when 'a : unmanaged>() =
            //let c = Interlocked.Increment(&count)
            //Log.line "%d %s" c ("pointer".Plural c)
            NativePtr.alloc<'a> 1

        let free (ptr : nativeptr<'a>) =
            NativePtr.free ptr
            //let c = Interlocked.Decrement(&count)
            //Log.line "%d %s" c ("pointer".Plural c)

    [<AbstractClass>]
    type internal AbstractPointer<'a when 'a : unmanaged>(input : IAdaptiveObject) =

        let mutable refCount = 0
        let mutable pointer = NativePtr.zero<'a>
        let mutable last : voption<'a> = ValueNone

        abstract Compute : AdaptiveToken -> 'a

        abstract Create : unit -> unit
        default x.Create() = ()
        abstract Destroy : unit -> unit
        default x.Destroy() = ()

        member x.Pointer = pointer

        member x.Acquire() =
            lock x (fun () ->
                let o = refCount
                refCount <- o + 1
                if o = 0 then 
                    pointer <- Memory.alloc()
                    x.Create()
            )

        member x.Release() =
            lock x (fun () ->
                let o = refCount
                refCount <- o - 1
                if o = 1 then 
                    x.Destroy()
                    match last with
                    | ValueSome _ -> last <- ValueNone
                    | ValueNone -> ()
                    Memory.free pointer
                    pointer <- NativePtr.zero
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                let o = refCount
                refCount <- o - cnt
                if o = cnt then 
                    x.Destroy()
                    match last with
                    | ValueSome _ -> last <- ValueNone
                    | ValueNone -> ()
                    Memory.free pointer
                    pointer <- NativePtr.zero
            )
        
        member x.Update(token : AdaptiveToken) =
            lock x (fun () ->
                if refCount <= 0 then raise <| ObjectDisposedException "aptr"
                let v = x.Compute token

                last <- ValueSome v
                NativePtr.write pointer v
            )

            
        interface AdaptivePointer with
            member x.IsConstant = false
            member x.IsVolatile = true
            member x.Input = input
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = NativePtr.read pointer

    type internal ConstantPointer<'a when 'a : unmanaged>(value : 'a, destroy : unit -> unit) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        let mutable refCount = 0
        let mutable pointer = NativePtr.zero<'a>

        member x.Pointer = 
            lock x (fun () ->
                if NativePtr.isNull pointer then
                    pointer <- Memory.alloc()
                    NativePtr.write pointer value
                pointer
            )

        member x.Acquire() =
            lock x (fun () ->
                let o = refCount
                if o = 0 then RefCounter.Acquire value
                refCount <- o + 1
            )

        member x.Release() =
            lock x (fun () ->
                let o = refCount
                refCount <- o - 1
                if o = 1 then
                    RefCounter.Release value
                    if NativePtr.notNull pointer then 
                        Memory.free pointer
                        pointer <- NativePtr.zero
                    destroy()
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                let o = refCount
                refCount <- o - cnt
                if o = cnt then 
                    RefCounter.Release value
                    if NativePtr.notNull pointer then 
                        Memory.free pointer
                        pointer <- NativePtr.zero
                    destroy()
            )
        
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = false
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = value

    type internal FixedPointer<'a when 'a : unmanaged>(pointer : nativeptr<'a>) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
 
        member x.Pointer = pointer

        member x.Acquire() = ()
        member x.Release(_cnt : int) = ()
        member x.Release() = ()
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = NativePtr.read pointer

    type internal NullPointer<'a when 'a : unmanaged> private() =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        static let instance = NullPointer<'a>() :> aptr<_>

        static member Instance = instance
        member x.Pointer = NativePtr.zero<'a>

        member x.Acquire() = ()
        member x.Release() = ()
        member x.Release(_cnt : int) = ()
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = raise <| NullReferenceException("NullPointer")

    type internal DecoratedPointer<'a when 'a : unmanaged>(inner : AdaptivePointer) =

        member x.Pointer = inner.Pointer

        member x.Acquire() = inner.Acquire()
        member x.Release() = inner.Release()
        member x.Release(cnt : int) = inner.Release(cnt)
        member x.Update(token : AdaptiveToken) = inner.Update(token)
        
        interface AdaptivePointer with
            member x.IsConstant = inner.IsConstant
            member x.IsVolatile = inner.IsVolatile
            member x.Input = inner.Input
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = NativePtr.read (NativePtr.ofNativeInt<'a> x.Pointer)

    type internal TemporaryPointer<'a when 'a : unmanaged>(count : int) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
 
        let mutable refCount = 0
        let mutable pointer : nativeptr<'a> = NativePtr.zero

        member x.Pointer = pointer

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    pointer <- NativePtr.alloc count
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    NativePtr.free pointer
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    NativePtr.free pointer
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = NativePtr.read pointer

    type internal OffsetPointer<'a when 'a : unmanaged>(input : aptr<'a>, offset : int) =
        let input, offset =
            match input with
            | :? OffsetPointer<'a> as input ->
                input.Input, input.Offset + offset
            | _ ->
                input, offset

        member x.Input = input
        member x.Offset = offset
        
        interface AdaptivePointer with
            member x.IsConstant = input.IsConstant
            member x.IsVolatile = input.IsVolatile
            member x.Input = input.Input
            member x.Update token = input.Update token
            member x.Pointer = input.Pointer + nativeint sizeof<'a> * nativeint offset
            member x.Acquire() = input.Acquire()
            member x.Release() = input.Release()
            member x.Release(cnt) = input.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = 
                NativePtr.get (NativePtr.ofNativeInt input.Pointer) offset

    type internal ByteOffsetPointer<'a when 'a : unmanaged>(input : aptr<'a>, offset : int) =
        let input, offset =
            match input with
            | :? ByteOffsetPointer<'a> as input ->
                input.Input, input.Offset + offset
            | _ ->
                input, offset

        member x.Input = input
        member x.Offset = offset
        
        interface AdaptivePointer with
            member x.IsConstant = input.IsConstant
            member x.IsVolatile = input.IsVolatile
            member x.Input = input.Input
            member x.Update token = input.Update token
            member x.Pointer = input.Pointer + nativeint offset
            member x.Acquire() = input.Acquire()
            member x.Release() = input.Release()
            member x.Release(cnt) = input.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = 
                NativePtr.read (NativePtr.ofNativeInt (input.Pointer + nativeint offset))

    type internal DelegatePointer(del : Delegate) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
 
        let mutable refCount = 0
        let mutable pointer = NativePtr.zero<nativeint>
        let mutable pinned = 0n

        member x.Pointer = pointer

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    pointer <- NativePtr.alloc 1
                    pinned <- Marshal.GetFunctionPointerForDelegate del
                    NativePtr.write pointer pinned
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    NativePtr.free pointer
                    pinned <- 0n
                    pinned <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    NativePtr.free pointer
                    
                    pinned <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = x.Pointer |> NativePtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<nativeint> with
            member x.Value = NativePtr.read pointer

    type internal PinnedMemoryPointer<'a when 'a : unmanaged>(mem : Memory<'a>) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        let mutable refCount = 0
        let mutable gc = Unchecked.defaultof<MemoryHandle>

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    gc <- mem.Pin()
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    gc.Dispose()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    gc.Dispose()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = gc.Pointer |> VoidPtr.toNativeInt
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<'a> with
            member x.Value = NativePtr.read (NativePtr.ofVoidPtr gc.Pointer)
    
    type internal PinnedArrayPointer(mem : Array) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        let mutable refCount = 0
        let mutable gc = Unchecked.defaultof<_>

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    gc <- GCHandle.Alloc(mem, GCHandleType.Pinned)
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    gc.Free()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    gc.Free()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = gc.AddrOfPinnedObject()
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<byte> with
            member x.Value = NativePtr.read (NativePtr.ofNativeInt (gc.AddrOfPinnedObject()))
    
    type internal PinnedMemoryPtrPtr<'a when 'a : unmanaged>(mem : Memory<'a>) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        let mutable refCount = 0
        let mutable gc = Unchecked.defaultof<MemoryHandle>
        let mutable ptr : nativeptr<nativeint> = NativePtr.zero

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    gc <- mem.Pin()
                    ptr <- NativePtr.alloc 1
                    NativePtr.write ptr (VoidPtr.toNativeInt gc.Pointer)
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    NativePtr.write ptr 0n
                    NativePtr.free ptr
                    ptr <- NativePtr.zero
                    gc.Dispose()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    NativePtr.write ptr 0n
                    NativePtr.free ptr
                    ptr <- NativePtr.zero
                    gc.Dispose()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = NativePtr.toNativeInt ptr
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<nativeint> with
            member x.Value = NativePtr.read ptr
    
    type internal PinnedArrayPtrPtr(mem : Array) =
        static let dummyInput = AVal.constant () :> IAdaptiveObject
        let mutable refCount = 0
        let mutable gc = Unchecked.defaultof<_>
        let mutable ptr : nativeptr<nativeint> = NativePtr.zero

        member x.Acquire() =
            lock x (fun () ->
                if refCount = 0 then
                    gc <- GCHandle.Alloc(mem, GCHandleType.Pinned)
                    ptr <- NativePtr.alloc 1
                    NativePtr.write ptr (gc.AddrOfPinnedObject())
                    refCount <- 1
                else
                    refCount <- refCount + 1
            )
        member x.Release() =
            lock x (fun () ->
                if refCount = 1 then
                    NativePtr.write ptr 0n
                    NativePtr.free ptr
                    ptr <- NativePtr.zero
                    gc.Free()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - 1
            )
        member x.Release(cnt : int) =
            lock x (fun () ->
                if refCount = cnt then
                    NativePtr.write ptr 0n
                    NativePtr.free ptr
                    ptr <- NativePtr.zero
                    gc.Free()
                    gc <- Unchecked.defaultof<_>
                    refCount <- 0
                else
                    refCount <- refCount - cnt
            )
        member x.Update(_token : AdaptiveToken) = ()
        
        interface AdaptivePointer with
            member x.IsConstant = true
            member x.IsVolatile = true
            member x.Input = dummyInput
            member x.Update token = x.Update token
            member x.Pointer = NativePtr.toNativeInt ptr
            member x.Acquire() = x.Acquire()
            member x.Release() = x.Release()
            member x.Release(cnt) = x.Release(cnt)
            
        interface AdaptivePointer<nativeint> with
            member x.Value = NativePtr.read ptr
    
    type internal AdaptivePinnedMemoryPtrPtr<'a, 'b when 'b : unmanaged>(value : aval<'a>, mapping : 'a -> Memory<'b>) =
        inherit AbstractPointer<nativeint>(value)

        let mutable last = None
        let mutable gc = Unchecked.defaultof<MemoryHandle>

        override x.Destroy() =
            if Option.isSome last then
                last <- None
                gc.Dispose()

        override x.Compute(token : AdaptiveToken) =
            let v = value.GetValue token
            match last with
            | None ->
                last <- Some v
                gc <- (mapping v).Pin()
                gc.Pointer |> VoidPtr.toNativeInt
            | Some o ->
                if DefaultEquality.equals o v then
                    gc.Pointer |> VoidPtr.toNativeInt
                else
                    gc.Dispose()
                    last <- Some v
                    gc <- (mapping v).Pin()
                    gc.Pointer |> VoidPtr.toNativeInt

    

    [<GeneralizableValue>]
    let temporary<'a when 'a : unmanaged> (count : int) = 
        TemporaryPointer<'a>(count) :> aptr<_>

    let zero<'a when 'a : unmanaged> =
        NullPointer<'a>.Instance

    let add (n : int) (ptr : aptr<'a>) =
        OffsetPointer(ptr, n) :> aptr<_>
        
    let addBytes (n : int) (ptr : aptr<'a>) =
        ByteOffsetPointer(ptr, n) :> aptr<_>

    let cast<'a when 'a : unmanaged> (ptr : AdaptivePointer) =
        match ptr with
        | :? aptr<'a> as p -> p
        | _ -> DecoratedPointer<'a>(ptr) :> aptr<_>
        
    let constant (value : 'a) =
        ConstantPointer(value, id) :> aptr<_>

    let ofNativePtr (ptr : nativeptr<'a>) =
        FixedPointer ptr :> aptr<_>
        
    let ofVoidPtr<'a when 'a : unmanaged> (ptr : voidptr) =
        FixedPointer<'a>(NativePtr.ofVoidPtr ptr) :> aptr<_>

    let ofNativeInt<'a when 'a : unmanaged> (ptr : nativeint) =
        FixedPointer<'a>(NativePtr.ofNativeInt ptr) :> aptr<_>
        
    let ofAVal (value : aval<'a>) : aptr<'a> =
        if value.IsConstant then
            ConstantPointer(AVal.force value, id) :> aptr<_>
        else
            { new AbstractPointer<'a>(value) with
                member x.Compute(token : AdaptiveToken) =
                    value.GetValue token
            } :> aptr<_>
            
    let mapVal<'a, 'b when 'b : unmanaged> (mapping : 'a -> 'b) (value : aval<'a>) : aptr<'b> =
        if value.IsConstant then
            let a = AVal.force value
            RefCounter.Acquire a
            ConstantPointer(mapping a, fun () -> RefCounter.Release a) :> aptr<_>
        else
            let last = ref ValueNone
            let lastValue = ref Unchecked.defaultof<'b>
            { new AbstractPointer<'b>(value) with
                member x.Compute(token : AdaptiveToken) =
                    let a = value.GetValue token
                    match !last with
                    | ValueSome l when Unchecked.equals l a ->
                        !lastValue
                    | _ -> 
                        RefCounter.Acquire a
                        match !last with
                        | ValueSome l -> RefCounter.Release l
                        | ValueNone -> ()
                        last := ValueSome a
                        let v = mapping a
                        lastValue := v
                        v
                member x.Destroy() =
                    match !last with
                    | ValueSome l -> 
                        RefCounter.Release l
                        last := ValueNone
                        lastValue := Unchecked.defaultof<_>
                    | ValueNone -> ()

            } :> aptr<_>
              
    let mapVal2 (mapping : 'a -> 'b -> 'c) (v1 : aval<'a>) (v2 : aval<'b>) : aptr<'c> =
        let last = ref ValueNone
        let lastValue = ref Unchecked.defaultof<'c>
        let input = (v1, v2) ||> AVal.map2 mapping

        { new AbstractPointer<'c>(input) with
            member x.Compute(token : AdaptiveToken) =
                let a = v1.GetValue token
                let b = v2.GetValue token
                match !last with
                | ValueSome struct(la, lb) when Unchecked.equals la a && Unchecked.equals lb b ->
                    !lastValue
                | _ ->
                    RefCounter.Acquire a
                    RefCounter.Acquire b
                    match !last with
                    | ValueSome struct(la, lb) -> 
                        RefCounter.Release la
                        RefCounter.Release lb
                    | _ ->
                        ()
                    last := ValueSome struct(a, b)
                    let v = mapping a b
                    lastValue := v
                    v
            member x.Destroy() =
                match !last with
                | ValueSome struct(la, lb) -> 
                    RefCounter.Release la
                    RefCounter.Release lb
                    last := ValueNone
                    lastValue := Unchecked.defaultof<_>
                | ValueNone -> ()

        } :> aptr<_>

    let map (mapping : 'a -> 'b) (input : aptr<'a>) =
        { new AbstractPointer<'b>(input.Input) with
            member x.Create() = input.Acquire()
            member x.Destroy() = input.Release()
            member x.Compute(token) =
                input.Update token
                input.Value |> mapping
        } :> aptr<_>
        
    let pinDelegate (del : #Delegate) =
        DelegatePointer del :> aptr<_>

    //let pinFunction (action : 'a -> 'b) =
    //    DelegateType.Wrap action
    //    |> pinDelegate
        
    let pinMemory (data : Memory<'a>) =
        PinnedMemoryPointer(data) :> aptr<_>

    let pinArray (data : 'a[]) =
        PinnedMemoryPointer(Memory data) :> aptr<_>
        
    let pinSystemArray (data : Array) =
        PinnedArrayPointer(data) :> aptr<_>

    let pinMemoryPtr (data : Memory<'a>) =
        PinnedMemoryPtrPtr<'a>(data) :> aptr<_>
        
    let pinArrayPtr (data : 'a[]) =
        PinnedMemoryPtrPtr<'a>(Memory data) :> aptr<_>
        
    let pinSystemArrayPtr (data : Array) =
        PinnedArrayPtrPtr(data) :> aptr<_>


    let pinAdaptiveArrayPtr (data : aval<'a[]>) =
        AdaptivePinnedMemoryPtrPtr(data, fun a -> Memory(a)) :> aptr<_>
        
    let pinAdaptiveMemoryPtr (data : aval<Memory<'a>>) =
        AdaptivePinnedMemoryPtrPtr(data, id) :> aptr<_>
