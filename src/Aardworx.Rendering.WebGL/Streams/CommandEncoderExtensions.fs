namespace Aardworx.Rendering.WebGL.Streams

open System.Runtime.InteropServices
open System.Runtime.CompilerServices
open System.Security
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Microsoft.FSharp.NativeInterop
open Aardworx.WebAssembly

#nowarn "9"

[<AutoOpen>]
module private CommandEncoderExetensionHelpers =
    open Microsoft.FSharp.Quotations
    open Microsoft.FSharp.Quotations.Patterns
    open System.Reflection

    let getOffset (e : Expr<'a -> 'b>) =
        let rec getOffset (e : Expr) =
            match e with
            | Lambda(_, b) -> getOffset b
            | Sequential(_, b) -> getOffset b
            | Let(_,_,b) -> getOffset b
            | FieldGet(_, f) -> Marshal.OffsetOf(f.DeclaringType, f.Name)
            | PropertyGet(_, p, []) -> 
                let fields = p.DeclaringType.GetFields(BindingFlags.Public ||| BindingFlags.NonPublic ||| BindingFlags.Instance)
                
                let fields = 
                    fields |> Array.filter (fun f ->
                        f.Name.Contains p.Name
                    )

                if fields.Length = 0 then   
                    failwithf "no backing field found for %A" p
                else
                    let f = fields |> Array.minBy (fun f -> f.Name.Length)
                    Marshal.OffsetOf(f.DeclaringType, f.Name)
            | _ -> failwith ""
        getOffset e

    let pointerOffset (e : Expr<'a -> 'b>) : aptr<'a> -> aptr<'b> =
        let off = getOffset e
        if off = 0n then 
            APtr.cast
        else
            APtr.cast >> APtr.addBytes (int off)


[<Struct>]
type BufferRangeBindingInfo =
    {
        Buffer  : int
        Offset  : int64
        Size    : int64
    }

[<AbstractClass; Sealed; Extension>]
type CommandEncoderExtensions private() =
    
    static let bufferFieldOffset = getOffset <@ fun r -> r.Buffer @>
    static let offsetFieldOffset = getOffset <@ fun r -> r.Offset @>
    static let sizeFieldOffset = getOffset <@ fun r -> r.Size @>


    [<Extension>]
    static member If(cmd : CommandEncoder, condition : aval<bool>, code : CommandEncoder -> unit) =
        if condition.IsConstant then    
            if AVal.force condition then code cmd
        else
            let ptr = condition |> APtr.mapVal (function true -> 1 | _ -> 0)
            cmd.Switch(
                ptr,
                [1, code],
                ignore
            )
        
    [<Extension>]
    static member IfThenElse(cmd : CommandEncoder, condition : aval<bool>, ifTrue : CommandEncoder -> unit, ifFalse : CommandEncoder -> unit ) =
        if condition.IsConstant then    
            if AVal.force condition then ifTrue cmd
            else ifFalse cmd
        else
            let ptr = condition |> APtr.mapVal (function true -> 1 | _ -> 0)
            cmd.Switch(
                ptr,
                [1, ifTrue],
                ifFalse
            )
        
    [<Extension>]
    static member SwitchValue(cmd : CommandEncoder, value : aval<'a>, cases : list<'a * (CommandEncoder -> unit)>, ?fallback : CommandEncoder -> unit) =
        if value.IsConstant then
            let value = AVal.force value
            let code = 
                cases |> List.tryPick (fun (a, c) ->
                    if Unchecked.equals a value then Some c
                    else None
                )
            match code with
            | Some code -> code cmd
            | None ->
                match fallback with
                | Some code -> code cmd
                | None -> ()
        else
            let table =
                cases
                |> List.mapi (fun i (vi, ci) -> vi, i)
                |> HashMap.ofList

            let index =
                value |> APtr.mapVal(fun v -> 
                    match HashMap.tryFindV v table with
                    | ValueSome id -> id
                    | ValueNone -> -1
                )

            cmd.Switch(
                index,
                cases |> List.mapi (fun i (_, c) -> i, c),
                defaultArg fallback ignore
            )
            
    [<Extension>]
    static member Match(cmd : CommandEncoder, value : aval<'a>, cases : list<('a -> bool) * (CommandEncoder -> unit)>, ?fallback : CommandEncoder -> unit) =
        let table =
            cases
            |> List.mapi (fun i (test, code) -> i, test, code)

        let index =
            value |> APtr.mapVal(fun v ->
                let index = 
                    table |> List.tryPick (fun (i, test, _) ->
                        if test v then Some i
                        else None
                    )
                match index with
                | Some id -> id
                | None -> -1
            )

        cmd.Switch(
            index,
            cases |> List.mapi (fun i (_, c) -> i, c),
            defaultArg fallback ignore
        )


    [<Extension>]
    static member Set(cmd : CommandEncoder, cap : EnableCap, state : bool) =
        if state then cmd.Enable(cap)
        else cmd.Disable(cap)

    [<Extension>]
    static member Set(cmd : CommandEncoder, cap : EnableCap, state : aval<bool>) =
        if state.IsConstant then
            cmd.Set(cap, AVal.force state)
        else
            let ptr = state |> APtr.mapVal (function true -> 1 | _ -> 0)
            cmd.Switch(
                ptr,
                [1, fun cmd -> cmd.Enable cap],
                fun cmd -> cmd.Disable cap
            )
        
          
    [<Extension>]
    static member GetUniformBufferBinding(cmd : CommandEncoder, index : int, dst : nativeptr<BufferRangeBindingInfo>) =
        let bPtr = NativePtr.toNativeInt dst + bufferFieldOffset |> NativePtr.ofNativeInt<int>
        let oPtr = NativePtr.toNativeInt dst + offsetFieldOffset |> NativePtr.ofNativeInt<int64>
        let sPtr = NativePtr.toNativeInt dst + sizeFieldOffset |> NativePtr.ofNativeInt<int64>
        cmd.GetIntegeri_v(GetPName.UniformBufferBinding, uint32 index, bPtr)
        cmd.GetInteger64i_v(GetPName.UniformBufferStart, uint32 index, oPtr)
        cmd.GetInteger64i_v(GetPName.UniformBufferSize, uint32 index, sPtr)

        
    [<Extension>]
    static member SetUniformBufferBinding(cmd : CommandEncoder, index : int, binding : nativeptr<BufferRangeBindingInfo>) =
        let bPtr = NativePtr.toNativeInt binding + bufferFieldOffset |> NativePtr.ofNativeInt<uint32>
        let oPtr = NativePtr.toNativeInt binding + offsetFieldOffset |> NativePtr.ofNativeInt<nativeint>
        let sPtr = NativePtr.toNativeInt binding + sizeFieldOffset |> NativePtr.ofNativeInt<unativeint>
        
        cmd.BindBufferRange(
            APtr.constant BufferTargetARB.UniformBuffer, 
            APtr.constant (uint32 index),
            APtr.ofNativePtr bPtr,
            APtr.ofNativePtr oPtr,
            APtr.ofNativePtr sPtr
        )
        
    [<Extension>]
    static member PushStruct<'a when 'a : unmanaged>(cmd : CommandEncoder, location : nativeptr<'a>) =
        let s = sizeof<'a>
        let mutable o = 0
        let mutable r = s
        let mutable ptr = NativePtr.toNativeInt location

        while o < s do  
            let c = 
                if r = 4 then
                    cmd.Push(NativePtr.ofNativeInt<int> ptr)
                    4
                elif r = 8 then
                    cmd.Push(NativePtr.ofNativeInt<int64> ptr)
                    8
                elif r > 8 then
                    cmd.Push(NativePtr.ofNativeInt<int64> ptr)
                    8
                else
                    failwith "bad alignment"
                

            o <- o + c
            r <- r - c
            ptr <- ptr + nativeint c
            
    [<Extension>]
    static member PopStruct<'a when 'a : unmanaged>(cmd : CommandEncoder, location : nativeptr<'a>) =
        let s = sizeof<'a>
        let mutable o = 0
        let mutable r = s
        let mutable ptr = NativePtr.toNativeInt location + nativeint s

        while o < s do  
            let c = 
                if r = 4 then
                    cmd.Pop(NativePtr.ofNativeInt<int> (ptr - 4n))
                    4
                elif r = 8 then
                    cmd.Pop(NativePtr.ofNativeInt<int64> (ptr - 8n))
                    8
                elif r > 8 then
                    cmd.Pop(NativePtr.ofNativeInt<int64> (ptr - 8n))
                    8
                else
                    failwith "bad alignment"
                

            o <- o + c
            r <- r - c
            ptr <- ptr - nativeint c

