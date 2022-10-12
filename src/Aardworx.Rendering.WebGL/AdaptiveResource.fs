namespace Aardworx.WebAssembly

open FSharp.Data.Adaptive

#nowarn "9"
#nowarn "4321"

type IAdaptiveResource =
    abstract Release : unit -> unit

type IAdaptiveResource<'a> =
    inherit IAdaptiveResource
    abstract Acquire : unit -> aval<'a>

and ares<'a> = IAdaptiveResource<'a>
 
module ARes =
    let inline private (!) (a : ref<'a>) = a.Value
    let inline private (:=) (a : ref<'a>) (value : 'a) = a.Value <- value


    let ofCreateDestroy (create : unit -> aval<'a>) (destroy : aval<'a> -> unit) =
        let refCount = ref 0
        let current = ref Unchecked.defaultof<aval<'a>>
        { new ares<'a> with
            member x.Acquire() =
                lock x (fun () ->
                    if !refCount = 0 then
                        current := create()
                    refCount := !refCount + 1
                    !current
                )
            member x.Release() =
                lock x (fun () ->
                    if !refCount = 1 then
                        destroy !current
                        current := Unchecked.defaultof<_>
                        refCount := 0
                    else
                        refCount := !refCount - 1
                )
        }

    let mapVal (create : 'a -> 'b) (destroy : 'b -> unit) (value : aval<'a>) =
        let refCount = ref 0
        let aval = ref Unchecked.defaultof<aval<'b>>
        let last = ref ValueNone
        { new ares<'b> with
            member x.Acquire() =
                lock x (fun () ->
                    if !refCount = 0 then
                        aval :=
                            value |> AVal.map (fun v ->
                                match !last with
                                | ValueSome l -> destroy l
                                | ValueNone -> ()
                                let n = create v
                                last := ValueSome n
                                n
                            )
                    refCount := !refCount + 1
                    !aval
                )
                
            member x.Release() = 
                lock x (fun () ->
                    if !refCount = 1 then
                        match !last with
                        | ValueSome last -> destroy last
                        | ValueNone -> ()
                        last := ValueNone
                        aval := Unchecked.defaultof<_>
                        refCount := 0
                    else
                        refCount := !refCount - 1
                )
        }
        
    let mapVal2 (create : 'a -> 'b -> 'c) (destroy : 'c -> unit) (va : aval<'a>) (vb : aval<'b>) =
        let create = OptimizedClosures.FSharpFunc<_,_,_>.Adapt create
        let refCount = ref 0
        let aval = ref Unchecked.defaultof<aval<'c>>
        let last = ref ValueNone
        { new ares<'c> with
            member x.Acquire() =
                lock x (fun () ->
                    if !refCount = 0 then
                        aval :=
                            (va, vb) ||> AVal.map2 (fun va vb ->
                                match !last with
                                | ValueSome l -> destroy l
                                | ValueNone -> ()
                                let n = create.Invoke(va, vb)
                                last := ValueSome n
                                n
                            )
                    refCount := !refCount + 1
                    !aval
                )
                
            member x.Release() = 
                lock x (fun () ->
                    if !refCount = 1 then
                        match !last with
                        | ValueSome last -> destroy last
                        | ValueNone -> ()
                        last := ValueNone
                        aval := Unchecked.defaultof<_>
                        refCount := 0
                    else
                        refCount := !refCount - 1
                )
        }
    
    let map (mapping : 'a -> 'b) (input : ares<'a>) =
        let cache = ref Unchecked.defaultof<aval<'b>>
        let refCount = ref 0

        { new ares<'b> with
            member x.Acquire() =    
                lock x (fun () ->
                    let o = !refCount
                    if o = 0 then
                        let h = input.Acquire() |> AVal.map mapping
                        cache := h
                        refCount := 1
                    else
                        refCount := o + 1
                    !cache
                )
            member x.Release() =
                lock x (fun () ->
                    let o = !refCount
                    if o = 1 then
                        input.Release()
                        cache := Unchecked.defaultof<_>
                        refCount := 0
                    else
                        refCount := o - 1
                )
        }
        
    let map2 (mapping : 'a -> 'b -> 'c) (va : ares<'a>) (vb : ares<'b>) =
        let cache = ref Unchecked.defaultof<aval<'c>>
        let refCount = ref 0
        //let mapping = OptimizedClosures.FSharpFunc<_,_,_>.Adapt mapping

        { new ares<'c> with
            member x.Acquire() =    
                lock x (fun () ->
                    let o = !refCount
                    if o = 0 then
                        let h = (va.Acquire(), vb.Acquire()) ||> AVal.map2 mapping
                        cache := h
                        refCount := 1
                    else
                        refCount := o + 1
                    !cache
                )
            member x.Release() =
                lock x (fun () ->
                    let o = !refCount
                    if o = 1 then
                        va.Release()
                        vb.Release()
                        cache := Unchecked.defaultof<_>
                        refCount := 0
                    else
                        refCount := o - 1
                )
        }
