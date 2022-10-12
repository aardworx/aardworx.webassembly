namespace Aardworx.Rendering.WebGL

open System
open System.Runtime.CompilerServices
open Silk.NET.OpenGLES
open Aardvark.Base
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open FSharp.Data.Adaptive
open Microsoft.FSharp.NativeInterop

#nowarn "9"

[<RequireQualifiedAccess; Struct>]
type BindingFrequency =
    | Vertex
    | Instances of count : int
    | Constant

    static member Instance = BindingFrequency.Instances 1

[<Struct>]
type BufferBinding =
    {
        Buffer      : BufferRange
        Type        : Type
        Stride      : int
        Normalized  : bool
        Frequency   : BindingFrequency
    }
    
[<Struct>]
type IndexBufferBinding =
    {
        Buffer  : BufferRange
        Type    : Type
    }


module internal VertexArray =

    let private toDivisor (v : BindingFrequency) =
        match v with
        | BindingFrequency.Vertex -> 0u
        | BindingFrequency.Constant -> UInt32.MaxValue
        | BindingFrequency.Instances c -> uint32 c

    let private vertexAttribType =
        LookupTable.lookupTable [
            typeof<C3b>, VertexAttribType.UnsignedByte
            typeof<C4b>, VertexAttribType.UnsignedByte
            
            typeof<C3us>, VertexAttribType.UnsignedShort
            typeof<C4us>, VertexAttribType.UnsignedShort

            typeof<int>, VertexAttribType.Int
            typeof<V2i>, VertexAttribType.Int
            typeof<V3i>, VertexAttribType.Int
            typeof<V4i>, VertexAttribType.Int
            

            typeof<float32>, VertexAttribType.Float
            typeof<V2f>, VertexAttribType.Float
            typeof<V3f>, VertexAttribType.Float
            typeof<V4f>, VertexAttribType.Float
            
            typeof<M44f>, VertexAttribType.Float

            typeof<float>, VertexAttribType.Double
            typeof<V2d>, VertexAttribType.Double
            typeof<V3d>, VertexAttribType.Double
            typeof<V4d>, VertexAttribType.Double
        ]
        
    let private vertexAttribSize =
        LookupTable.lookupTable [
            typeof<C3b>, [|0, 3|]
            typeof<C4b>, [|0, 4|]
            
            typeof<C3us>, [|0, 3|]
            typeof<C4us>, [|0, 4|]

            typeof<int>, [|0, 1|]
            typeof<V2i>, [|0, 2|]
            typeof<V3i>, [|0, 3|]
            typeof<V4i>, [|0, 4|]
            

            typeof<float32>, [|0, 1|]
            typeof<V2f>, [|0, 2|]
            typeof<V3f>, [|0, 3|]
            typeof<V4f>, [|0, 4|]
            
            typeof<M44f>, [|0, 4; 16, 4; 32, 4; 48, 4|]
            
            typeof<float>, [|0, 1|]
            typeof<V2d>, [|0, 2|]
            typeof<V3d>, [|0, 3|]
            typeof<V4d>, [|0, 4|]
        ]

    let private intAttribTypes =
        HashSet.ofList [
            typeof<int>
            typeof<V2i>
            typeof<V3i>
            typeof<V4i>
        ]
        
    let private doubleAttribTypes =
        HashSet.ofList [
            typeof<float>
            typeof<V2d>
            typeof<V3d>
            typeof<V4d>
        ]

    let create (indexBuffer : option<IndexBufferBinding>) (vertexBuffers : Map<int, BufferBinding>) (gl : GL) =
       
        let vao = gl.GenVertexArray()
        gl.BindVertexArray(vao)
            
        let infos = System.Collections.Generic.List<VertexAttribInfo>()

        for id, binding in Map.toSeq vertexBuffers do
            let range = binding.Buffer
            let norm = 
                if binding.Normalized then Boolean.True 
                else Boolean.False
            
            
            let divisor =
                if binding.Stride = 0 then 1u <<< 31
                else toDivisor binding.Frequency

            gl.BindBuffer(BufferTargetARB.ArrayBuffer, range.Buffer.Handle)
            let sizes = vertexAttribSize binding.Type
            let mutable id = id
            for offset, size in sizes do
                let typ = unbox<GLEnum> (int (vertexAttribType binding.Type))

                if intAttribTypes.Contains binding.Type then
                    gl.VertexAttribIPointer(
                        uint32 id, size, typ,
                        uint32 binding.Stride,
                        VoidPtr.ofNativeInt (nativeint range.Offset + nativeint offset)
                    )

                    infos.Add {
                        Index = id; Size = size; Type = typ; Stride = binding.Stride
                        Offset = int range.Offset + offset; Normalized = 0; Integer = 1
                        Buffer = range.Buffer.Handle; Divisor = int divisor 
                    }

                else
                    gl.VertexAttribPointer(
                        uint32 id, 
                        size, 
                        unbox<GLEnum> (int (vertexAttribType binding.Type)), 
                        norm, 
                        uint32 binding.Stride, 
                        VoidPtr.ofNativeInt (nativeint range.Offset + nativeint offset)
                    )
                 
                    infos.Add {
                        Index = id; Size = size; Type = typ; Stride = binding.Stride
                        Offset = int range.Offset + offset; Normalized = int norm; Integer = 0
                        Buffer = range.Buffer.Handle; Divisor = int divisor 
                    }
                gl.EnableVertexAttribArray(uint32 id)
                gl.VertexAttribDivisor(uint32 id, divisor)
                id <- id + 1

        gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0u)
        let indexType, indexOffset = 
            match indexBuffer with
            | Some ib ->
                gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ib.Buffer.Buffer.Handle)


                let offset = nativeint ib.Buffer.Offset

                let typ = 
                    if ib.Type = typeof<uint8> || ib.Type = typeof<int8> then DrawElementsType.UnsignedByte
                    elif ib.Type = typeof<uint16> || ib.Type = typeof<int16> then DrawElementsType.UnsignedShort
                    elif ib.Type = typeof<uint32> || ib.Type = typeof<int32> then DrawElementsType.UnsignedInt
                    else failf "bad index-type: %A" ib.Type
                typ, offset

            | None ->
                gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0u)
                unbox<DrawElementsType> 0, 0n
                
        gl.BindVertexArray(0u)

        let infos = CSharpList.toArray infos
        let ptr = NativePtr.alloc infos.Length 
        for i in 0 .. infos.Length - 1 do NativePtr.set ptr i infos.[i] 

        let indexElemSize =
            match indexType with
            | DrawElementsType.UnsignedByte -> 1
            | DrawElementsType.UnsignedShort -> 2
            | DrawElementsType.UnsignedInt -> 4
            | _ -> 0

        let bindingInfo =
            {
                IndexType = indexType
                IndexElementSize = nativeint indexElemSize
                IndexOffset = indexOffset
                BindingCount = infos.Length
                Bindings = ptr
            }

        let pp = NativePtr.alloc 1
        NativePtr.write pp bindingInfo

        vao, pp

    let delete (_self : UnsharedResource<uint32>) (gl : GL) (vao : uint32) =
        gl.DeleteVertexArray vao

type VertexBufferBinding internal(device : Device, index : option<IndexBufferBinding>, vertexBuffers : Map<int, BufferBinding>, handle : uint32, info : nativeptr<VertexBufferBindingInfo>) =
    inherit Resource( device, "VertexBufferBinding", handle, 0L)

    member x.Info = info
    member x.Index = index
    member x.VertexBuffers = vertexBuffers

    override x.Destroy(_gl : GL) =
        index |> Option.iter (fun i -> i.Buffer.Buffer.Dispose())
        vertexBuffers |> Map.iter (fun _ b -> b.Buffer.Buffer.Dispose())
        let i = NativePtr.read info
        NativePtr.free i.Bindings
        NativePtr.free info
        
[<AbstractClass; Sealed; Extension>]
type DeviceVertexBufferBindingExtensions private() =

    [<Extension>]
    static member CreateVertexBufferBinding(device : Device, vertexBuffers : Map<int, BufferBinding>, indexBuffer : option<IndexBufferBinding>) =
        indexBuffer |> Option.iter (fun i -> i.Buffer.Buffer.AddReference())
        vertexBuffers |> Map.iter (fun _ b -> b.Buffer.Buffer.AddReference())

        let handle, info = 
            device.Run(fun gl ->
                VertexArray.create indexBuffer vertexBuffers gl
            )

        new VertexBufferBinding(device, indexBuffer, vertexBuffers, handle, info)
        
    [<Extension>]
    static member CreateVertexBufferBinding(device : Device, vertexBuffers : Map<int, BufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            device,
            vertexBuffers,
            None
        )
        
    [<Extension>]
    static member CreateVertexBufferBinding(device : Device, vertexBuffers : list<int * BufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            device,
            Map.ofList vertexBuffers,
            None
        )
        
    [<Extension>]
    static member CreateVertexBufferBinding(device : Device, vertexBuffers : list<int * BufferBinding>, indexBuffer : option<IndexBufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            device,
            Map.ofList vertexBuffers,
            indexBuffer
        )
    
    [<Extension>]
    static member CreateVertexBufferBinding(program : Program, vertexBuffers : Map<Symbol, BufferBinding>, indexBuffer : option<IndexBufferBinding>) =
        let buffers = 
            program.InputSemantics |> Map.map (fun _ semantic ->
                match Map.tryFind semantic vertexBuffers with
                | Some buffer -> buffer
                | None -> 
                    failwithf "[GL] could not find attribute: %A" semantic
            )

        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            program.Device,
            buffers,
            indexBuffer
        )

    [<Extension>]
    static member CreateVertexBufferBinding(program : Program, vertexBuffers : Map<Symbol, BufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            program,
            vertexBuffers,
            None
        )

    [<Extension>]
    static member CreateVertexBufferBinding(program : Program, vertexBuffers : list<Symbol * BufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            program,
            Map.ofList vertexBuffers,
            None
        )
        
    [<Extension>]
    static member CreateVertexBufferBinding(program : Program, vertexBuffers : list<string * BufferBinding>) =
        DeviceVertexBufferBindingExtensions.CreateVertexBufferBinding(
            program,
            Map.ofList (vertexBuffers |> List.map (fun (s,b) -> Symbol.Create s, b)),
            None
        )


    [<Extension>]
    static member SetVertexBufferBinding(this : CommandStream, vbb : VertexBufferBinding) =
        this.BaseStream.BindVertexArray vbb.Handle
        this.BaseStream.Copy(
            NativePtr.toNativeInt vbb.Info, 
            NativePtr.toNativeInt this.State.VertexBufferBindingInfo, 
            nativeint sizeof<VertexBufferBindingInfo>
        )

        
    [<Extension>]
    static member SetVertexBufferBinding(this : CommandStream, vbb : aval<VertexBufferBinding>) =
        if vbb.IsConstant then
            this.SetVertexBufferBinding(AVal.force vbb)
        else
            let handle = vbb |> APtr.mapVal (fun v -> v.Handle)
            let info = vbb |> APtr.mapVal (fun v -> NativePtr.toNativeInt v.Info)
            this.BaseStream.BindVertexArray handle
            this.BaseStream.CopyID(
                info, 
                APtr.ofNativePtr this.State.VertexBufferBindingInfo, 
                APtr.constant (nativeint sizeof<VertexBufferBindingInfo>)
            )
        
        
    [<Extension>]
    static member SetVertexBufferBinding(this : CommandStream, vbb : ares<VertexBufferBinding>) =
        this.SetVertexBufferBinding(this.Acquire vbb)
