namespace Aardworx.WebAssembly

open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
open System.Runtime.CompilerServices
open System.Reflection.Emit
open Aardvark.Base
open System
open Microsoft.FSharp.Reflection
open System.Reflection
open System.Security

#nowarn "9"
#nowarn "51"


module VoidPtr = 
    let zero =
        NativePtr.toVoidPtr NativePtr.zero<byte>

    let ofNativeInt (ptr : nativeint) =
        NativePtr.toVoidPtr (NativePtr.ofNativeInt<byte> ptr)

    let toNativeInt (ptr : voidptr) =
        NativePtr.toNativeInt (NativePtr.ofVoidPtr<byte> ptr)
    
[<AutoOpen>]
module MarshalExtensions =
    
    type Marshal with   
        static member Copy(src : nativeint, dst : nativeint, size : int) =
            let sSrc = Span<byte>(VoidPtr.ofNativeInt src, size)
            let sDst = Span<byte>(VoidPtr.ofNativeInt dst, size)
            sSrc.CopyTo sDst

        static member Copy(src : nativeint, dst : nativeint, size : nativeint) =
            Marshal.Copy(src, dst, int size)

    //let inline (:=) (a : ^a) (b : ^b) =
    //    (^a : (member set_Value : ^b -> unit) (a, b))
        
    //let inline (!) (a : ^a) : ^b  =
    //    (^a : (member Value : ^b) (a))

module NativePtr =
    let isNull (ptr : nativeptr<'a>) =
        NativePtr.toNativeInt ptr = 0n
        
    let notNull (ptr : nativeptr<'a>) =
        NativePtr.toNativeInt ptr <> 0n

        
module Unchecked = 
    [<MethodImpl(MethodImplOptions.NoInlining ||| MethodImplOptions.NoOptimization)>]
    let reinterpret<'a, 'b when 'a : unmanaged and 'b : unmanaged> (value : 'a) =
        //if sizeof<'a> <> sizeof<'b> then failwithf "cannot reinterpret %A as %A" typeof<'a> typeof<'b>
        let mutable v = value
        NativePtr.read<'b> (NativePtr.ofNativeInt (NativePtr.toNativeInt &&v))



[<AbstractClass; Sealed>]
type DelegateType private() =
    static let bAss = AssemblyBuilder.DefineDynamicAssembly(AssemblyName "DynamicDelegates", AssemblyBuilderAccess.RunAndCollect)
    static let bMod = bAss.DefineDynamicModule("MainModule")

    static let cache = Dict<list<Type> * Type, Type>()
    static let wrapperCache = Dict<Type, obj -> Delegate>()



    static let wrapper (t : Type) =
        lock wrapperCache (fun () ->
            wrapperCache.GetOrCreate(t, System.Func<_,_>(fun tLambda ->
                let rec deconstruct (t : Type) =
                    if FSharpType.IsFunction t then
                        let d, r = FSharpType.GetFunctionElements t
                        let args, ret = deconstruct r
                        d :: args, ret
                    else
                        [], t

                let args, ret = deconstruct tLambda
                 
                match args with
                | [a] when a = typeof<unit> ->
                    let invoke = tLambda.GetMethod("Invoke", [| typeof<unit> |])
                    let tDel = DelegateType.Get([], ret)

                    let delRet =
                        if ret = typeof<unit> then typeof<Void>
                        else ret

                    
                    let d = DynamicMethod(Guid.NewGuid() |> string, delRet, [| tLambda |])
                    let il = d.GetILGenerator()
                    il.Emit(OpCodes.Ldarg_0)
                    il.Emit(OpCodes.Ldnull)
                    il.EmitCall(OpCodes.Callvirt, invoke, null)
                    if ret = typeof<unit> then il.Emit(OpCodes.Pop)
                    il.Emit(OpCodes.Ret)

                    fun (o : obj) ->
                        d.CreateDelegate(tDel, o)

                | [a] ->
                    let invoke = tLambda.GetMethod("Invoke", [|a|])
                    let tDel = DelegateType.Get([a], ret)
                    
                    let delRet =
                        if ret = typeof<unit> then typeof<Void>
                        else ret
                    
                    let d = DynamicMethod(Guid.NewGuid() |> string, delRet, [| tLambda; a |])
                    let il = d.GetILGenerator()
                    il.Emit(OpCodes.Ldarg_0)
                    il.Emit(OpCodes.Ldarg_1)
                    il.EmitCall(OpCodes.Callvirt, invoke, null)
                    if ret = typeof<unit> then il.Emit(OpCodes.Pop)
                    il.Emit(OpCodes.Ret)


                    fun (o : obj) ->
                        d.CreateDelegate(tDel, o)
        
                | _ -> 
                    let optimized =
                        typedefof<OptimizedClosures.FSharpFunc<_,_,_>>
                            .DeclaringType
                            .GetNestedType(sprintf "FSharpFunc`%d" (1 + List.length args))
                            .MakeGenericType(List.toArray (args @ [ret]))

                    let adapt = optimized.GetMethod("Adapt", [| tLambda |])
                    let invoke = optimized.GetMethod("Invoke", List.toArray args)
                    let tDel = DelegateType.Get(args, ret)
                    
                    
                    let delRet =
                        if ret = typeof<unit> then typeof<Void>
                        else ret

                    let d = DynamicMethod(Guid.NewGuid() |> string, delRet, Array.append [| optimized |] (List.toArray args))
                    let il = d.GetILGenerator()
                    il.Emit(OpCodes.Ldarg_0)
                    for i, _a in List.indexed args do
                        il.Emit(OpCodes.Ldarg, 1 + i)
                    il.EmitCall(OpCodes.Callvirt, invoke, null)
                    if ret = typeof<unit> then il.Emit(OpCodes.Pop)
                    il.Emit(OpCodes.Ret)


                    fun (o : obj) ->
                        let func = adapt.Invoke(null, [|o|])
                        d.CreateDelegate(tDel, func)
            ))
        )

    static member Get(args : list<Type>, ret : Type) =
        lock cache (fun () ->
            let args = args |> List.filter (fun a -> a <> typeof<unit>)
            cache.GetOrCreate((args, ret), fun (args, ret) ->
                match args with
                | [] when ret = typeof<unit> || ret = typeof<Void> ->
                    typeof<Action>
                | _ ->
                    let ret =
                        if ret = typeof<unit> then typeof<Void>
                        else ret
                    let name = Guid.NewGuid() |> string
                    let t = bMod.DefineType(name, TypeAttributes.Public ||| TypeAttributes.Sealed, typeof<MulticastDelegate>)

                    let ctor = 
                        t.DefineConstructor(
                            MethodAttributes.RTSpecialName ||| MethodAttributes.HideBySig ||| MethodAttributes.Public,
                            CallingConventions.Standard, [| typeof<obj>; typeof<nativeint> |]
                        )
                    ctor.SetImplementationFlags(MethodImplAttributes.CodeTypeMask)
                    
                    let args =
                        match args with
                        | [u] when u = typeof<unit> -> []
                        | _ -> args

                    let invoke = 
                        t.DefineMethod("Invoke", 
                            MethodAttributes.HideBySig ||| MethodAttributes.Virtual ||| MethodAttributes.Public,
                            ret, List.toArray args
                        )
                    invoke.SetImplementationFlags(MethodImplAttributes.CodeTypeMask)
                    t.SetCustomAttribute(typeof<SuppressUnmanagedCodeSecurityAttribute>.GetConstructor [||], [||])
                    t.CreateTypeInfo().AsType()

            )
        )

    static member Wrap(func : 'a) =
        wrapper typeof<'a> (func :> obj)

    static member Invoke(ptr : nativeint, arg0 : 'a) : 'z =
        let tDelegate = DelegateType.Get([typeof<'a>], typeof<'z>)
        let del = Marshal.GetDelegateForFunctionPointer(ptr, tDelegate)
        del.DynamicInvoke [|arg0 :> obj|] |> unbox




