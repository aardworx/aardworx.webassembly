namespace Aardworx.Rendering.WebGL

open Aardvark.Base
open Aardvark.Rendering
open Silk.NET.OpenGLES
open Aardworx.Rendering.WebGL
open System.Runtime.CompilerServices
open Aardworx.WebAssembly
open Microsoft.FSharp.NativeInterop
open FSharp.Data.Adaptive

#nowarn "9"


type Framebuffer(device : Device, signature : FramebufferSignature, size : V2i, colors : Map<int, Choice<Renderbuffer, TextureLevel>>, depth : option<Choice<Renderbuffer, TextureLevel>>, handle : uint32) =
    inherit Resource(device, "Framebuffer", handle, 0L)


    let attachments =
        Map.ofList [
            for KeyValue(slot, { Name = sem }) in signature.ColorAttachments do
                match Map.tryFind slot colors with 
                | Some tex ->
                    match tex with
                    | Choice1Of2 r -> yield sem, r :> IFramebufferOutput
                    | Choice2Of2 t -> yield sem, t :> IFramebufferOutput
                | _ ->
                    ()

            match depth with
            | Some d ->
                match d with
                | Choice1Of2 r -> yield DefaultSemantic.DepthStencil, r :> IFramebufferOutput
                | Choice2Of2 t -> yield DefaultSemantic.DepthStencil, t :> IFramebufferOutput
            | _ ->
                ()
        ]

    let ifaceAttachments =
        Map.ofList [
            for KeyValue(slot, { Name = sem }) in signature.ColorAttachments do
                match Map.tryFind slot colors with 
                | Some tex ->
                    match tex with
                    | Choice1Of2 r -> yield sem, r :> IFramebufferOutput
                    | Choice2Of2 t -> yield sem, (t.Texture :> IBackendTexture).[TextureAspect.Color, t.Level, t.BaseLayer .. t.BaseLayer + t.Layers-1] :> IFramebufferOutput
                | _ ->
                    ()

            match depth with
            | Some d ->
                match d with
                | Choice1Of2 r -> yield DefaultSemantic.DepthStencil, r :> IFramebufferOutput
                | Choice2Of2 t -> yield DefaultSemantic.DepthStencil, (t.Texture :> IBackendTexture).[TextureAspect.Depth, t.Level, t.BaseLayer .. t.BaseLayer + t.Layers-1] :> IFramebufferOutput
            | _ ->
                ()
        ]
        

    interface IFramebuffer with
        member x.GetHandle _ = handle :> obj
        member x.Attachments = ifaceAttachments
        member x.Signature = signature :> IFramebufferSignature
        member x.Size = size

    member x.Attachments = attachments
    member x.Signature = signature
    member x.Size = size

    override x.Destroy (gl : GL) =
        gl.DeleteFramebuffer handle

    new(device : Device, signature : FramebufferSignature, colors : Map<int, Choice<Renderbuffer, TextureLevel>>, depth : option<Choice<Renderbuffer, TextureLevel>>, handle : uint32) =
        
        let size =
            match depth with
            | Some d ->
                match d with
                | Choice1Of2 d -> d.Size
                | Choice2Of2 d -> d.Size
            | None ->
                if Map.isEmpty colors then
                    V2i.II
                else
                    let (KeyValue(_, c)) = colors |> Seq.head
                    match c with
                    | Choice1Of2 d -> d.Size
                    | Choice2Of2 d -> d.Size
        new Framebuffer(device, signature, size, colors, depth, handle)

[<AbstractClass; Sealed; Extension>]
type DeviceFramebufferExtensions private() =


    static let allCubeFaces = 
        [|
            TextureTarget.TextureCubeMapPositiveX
            TextureTarget.TextureCubeMapNegativeX
            TextureTarget.TextureCubeMapPositiveY
            TextureTarget.TextureCubeMapNegativeY
            TextureTarget.TextureCubeMapPositiveZ
            TextureTarget.TextureCubeMapNegativeZ
        |]
    
    
    static let create (this : Device) (signature : FramebufferSignature) (attachments : Map<Symbol, #IFramebufferOutput>) =

        let inline box (v : option<'a>) =
            match v with
            | Some a -> Some (a :> IFramebufferOutput)
            | None -> None

        let colors =
            let mutable res = Map.empty
            for KeyValue(slot, { Name = sem }) in signature.ColorAttachments do
                match box (Map.tryFind sem attachments) with
                | Some (:? Renderbuffer as r) ->
                    res <- Map.add slot (Choice1Of2 r) res
                | Some (:? TextureLevel as t) -> 
                    res <- Map.add slot (Choice2Of2 t) res
                | Some (:? ITextureLevel as t) ->
                    match t.Texture with
                    | :? Texture as tex ->
                        res <- Map.add slot (Choice2Of2 tex.[t.Level, t.Slices.Min .. t.Slices.Max]) res
                    | _ ->
                        Log.warn "[WebGL] bad texture: %A" t.Texture
                | _ -> 
                    ()
            res

        let depth =
            if Option.isSome signature.Depth then
                match box (Map.tryFind DefaultSemantic.DepthStencil attachments) with
                | Some (:? Renderbuffer as r) ->
                    Some (Choice1Of2 r)
                | Some (:? TextureLevel as t) -> 
                    Some (Choice2Of2 t)
                | Some (:? ITextureLevel as t) -> 
                    match t.Texture with
                    | :? Texture as tex ->
                        Some (Choice2Of2 tex.[t.Level, t.Slices.Min .. t.Slices.Max])
                    | _ ->
                        Log.warn "[WebGL] bad texture: %A" t.Texture
                        None
                | _ -> 
                    None
            else
                None


        let handle =
            this.Run(fun gl ->
                let fbo = gl.GenFramebuffer()

                let target = FramebufferTarget.Framebuffer
                let old = gl.GetInteger(GetPName.ReadFramebufferBinding) |> uint32
                gl.BindFramebuffer(target, fbo)
                
                let mutable samples = Set.empty

                let attach (att : FramebufferAttachment) (tex : Choice<Renderbuffer, TextureLevel>) =
                    match tex with
                    | Choice1Of2 rbo ->
                        gl.FramebufferRenderbuffer(target, att, RenderbufferTarget.Renderbuffer, rbo.Handle)
                        samples <- Set.add (defaultArg rbo.Samples 1) samples

                    | Choice2Of2 tex ->
                        samples <- Set.add (defaultArg tex.Texture.Samples 1) samples
                        if tex.Layers > 1 then failf "cannot attach multi-layered texture %A" tex.Layers

                        match tex.Texture.Dimension with
                        | TextureDimension.TextureCube ->
                            match tex.Texture.Layers with
                            | None -> 
                                gl.FramebufferTexture2D(target, att, allCubeFaces.[tex.BaseLayer], tex.Texture.Handle, tex.Level)
                            | Some _ -> 
                                gl.FramebufferTextureLayer(target, att, tex.Texture.Handle, tex.Level, tex.BaseLayer)

                        | TextureDimension.Texture2D ->
                            match tex.Texture.Layers with
                            | None -> 
                                gl.FramebufferTexture2D(target, att, TextureTarget.Texture2D, tex.Texture.Handle, tex.Level)
                            | Some _ -> 
                                gl.FramebufferTextureLayer(target, att, tex.Texture.Handle, tex.Level, tex.BaseLayer)

                        | dim -> 
                            failf "cannot attach %A texture to Framebuffer" dim
                       
                try
                    for KeyValue(slot, tex) in colors do
                        let att = unbox<FramebufferAttachment> (int FramebufferAttachment.ColorAttachment0 + slot)
                        attach att tex
                         
                    match depth with
                    | Some tex ->
                        let att = unbox<FramebufferAttachment> GLEnum.DepthAttachment
                        attach att tex
                        
                        let att = unbox<FramebufferAttachment> GLEnum.StencilAttachment
                        attach att tex
                    | None ->
                        ()

                    if Set.count samples > 1 then failf "inconsistent sample-counts in Frambuffer: %A" samples
                        
                    let status = gl.CheckFramebufferStatus target
                    if status <> GLEnum.FramebufferComplete then
                        failf "framebuffer creation failed: %A" status

                    gl.BindFramebuffer(target, old)
                    fbo

                with e ->
                    gl.BindFramebuffer(target, old)
                    gl.DeleteFramebuffer fbo
                    reraise()
            )

        new Framebuffer(this, signature, colors, depth, handle)
        
    static let defaultSignature =
        new FramebufferSignature(
            Unchecked.defaultof<_>,
            Map.ofList [0, { Name = DefaultSemantic.Colors; Format = TextureFormat.Rgba8 }],
            Some TextureFormat.Depth24Stencil8,
            1,1
        )
        
    [<Extension>]
    static member DefaultFramebuffer(this : Device, size : V2i) =
        new Framebuffer(
            this, defaultSignature,
            size,
            Map.ofList [0, Choice1Of2 (new Renderbuffer(this, TextureFormat.Rgba8, size, None, 0u)) ],
            Some (Choice1Of2 (new Renderbuffer(this, TextureFormat.Depth24Stencil8, size, None, 0u))),
            0u
        )


        
    [<Extension>]
    static member CreateFramebuffer(this : Device, signature : FramebufferSignature, attachments : Map<Symbol, #IFramebufferOutput>) =
        attachments
        |> create this signature

    [<Extension>]
    static member CreateFramebuffer(this : Device, signature : FramebufferSignature, attachments : seq<Symbol * #IFramebufferOutput>) =
        attachments
        |> Map.ofSeq
        |> create this signature

    [<Extension>]
    static member CreateFramebuffer(this : Device, signature : FramebufferSignature, attachments : seq<string * #IFramebufferOutput>) =
        attachments
        |> Seq.map (fun (n, t) -> Symbol.Create n, t :> IFramebufferOutput)
        |> Map.ofSeq
        |> create this signature

        
    [<Extension>]
    static member CreateFramebuffer(this : Device, signature : FramebufferSignature, attachments : list<Symbol * #IFramebufferOutput>) =
        attachments
        |> Map.ofSeq
        |> create this signature

    [<Extension>]
    static member CreateFramebuffer(this : Device, signature : FramebufferSignature, attachments : list<string * #IFramebufferOutput>) =
        attachments
        |> Seq.map (fun (n, t) -> Symbol.Create n, t :> IFramebufferOutput)
        |> Map.ofSeq
        |> create this signature




    [<Extension>]
    static member CreateFramebuffer(signature : FramebufferSignature, attachments : Map<Symbol, #IFramebufferOutput>) =
        attachments
        |> create signature.Device signature

    [<Extension>]
    static member CreateFramebuffer(signature : FramebufferSignature, attachments : seq<Symbol * #IFramebufferOutput>) =
        attachments
        |> Map.ofSeq
        |> create signature.Device signature

    [<Extension>]
    static member CreateFramebuffer(signature : FramebufferSignature, attachments : seq<string * #IFramebufferOutput>) =
        attachments
        |> Seq.map (fun (n, t) -> Symbol.Create n, t :> IFramebufferOutput)
        |> Map.ofSeq
        |> create signature.Device signature

        
    [<Extension>]
    static member CreateFramebuffer(signature : FramebufferSignature, attachments : list<Symbol * #IFramebufferOutput>) =
        attachments
        |> Map.ofSeq
        |> create signature.Device signature

    [<Extension>]
    static member CreateFramebuffer(signature : FramebufferSignature, attachments : list<string * #IFramebufferOutput>) =
        attachments
        |> Seq.map (fun (n, t) -> Symbol.Create n, t :> IFramebufferOutput)
        |> Map.ofSeq
        |> create signature.Device signature

        
    [<Extension>]
    static member GetDrawBuffers(this : FramebufferSignature) = 
        let res = Array.zeroCreate 128 
        let mutable maxIndex = -1
        for KeyValue(i, _) in this.ColorAttachments do
            res.[i] <- unbox<GLEnum> (int GLEnum.ColorAttachment0 + i)
            maxIndex <- max i maxIndex
        Array.take (maxIndex + 1) res
            



    [<Extension>]
    static member SetFramebuffer(this : CommandStream, target : FramebufferTarget, fbo : Framebuffer) =
        let backend = this.BaseStream

        backend.BindFramebuffer(target, fbo.Handle)
        backend.Viewport(0, 0, uint32 fbo.Size.X, uint32 fbo.Size.Y)

        let buffers = 
            if fbo.Handle = 0u then [| GLEnum.Back |]
            else fbo.Signature.GetDrawBuffers()

        backend.DrawBuffers(APtr.constant (uint32 buffers.Length), APtr.pinArrayPtr buffers |> APtr.map NativePtr.ofNativeInt<GLEnum>)

        this.State.FramebufferSignature <- fbo.Signature
        let handle = APtr.pinArray [| fbo.Handle |]
        match target with
        | FramebufferTarget.Framebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.ReadFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.DrawFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | _ ->
            ()
        let ptr = APtr.pinArray [| fbo.Size |]
        this.BaseStream.CopyDD (ptr, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 16n), APtr.constant (nativeint sizeof<V2i>))
            

    [<Extension>]
    static member SetFramebuffer(this : CommandStream, target : FramebufferTarget, signature : FramebufferSignature, fbo : aval<Framebuffer>) =
        let backend = this.BaseStream

        let handle = fbo |> APtr.mapVal (fun f -> f.Handle)
        let psize : aptr<uint32> = fbo |> APtr.mapVal (fun f -> f.Size) |> APtr.cast
        backend.BindFramebuffer(APtr.constant target, handle)
        backend.Viewport(APtr.constant 0, APtr.constant 0, psize, APtr.add 1 psize)

        
        let buffers = 
            let all = signature.GetDrawBuffers()
            fbo |> AVal.map (fun fbo ->
                if fbo.Handle = 0u then [| GLEnum.Back |]
                else all
            )
        backend.DrawBuffers(buffers |> APtr.mapVal (fun b -> uint32 b.Length), APtr.pinAdaptiveArrayPtr buffers |> APtr.map NativePtr.ofNativeInt<GLEnum>)


        this.State.FramebufferSignature <- signature
        match target with
        | FramebufferTarget.Framebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.ReadFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.DrawFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | _ ->
            ()
        this.BaseStream.CopyDD (psize, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 16n), APtr.constant (nativeint sizeof<V2i>))
            
    [<Extension>]
    static member SetFramebuffer(this : CommandStream, target : FramebufferTarget, signature : FramebufferSignature, handle : aptr<uint32>) =
        let backend = this.BaseStream

        backend.BindFramebuffer(APtr.constant target, handle)

        let back = [|GLEnum.Back|]
        let all = signature.GetDrawBuffers()
        backend.Switch(
            APtr.cast handle,
            [
                0, fun cmd -> 
                    cmd.DrawBuffers(APtr.constant 1u, back |> APtr.pinArrayPtr |> APtr.map NativePtr.ofNativeInt<GLEnum>)
            ],
            fun cmd ->
                cmd.DrawBuffers(APtr.constant (uint32 all.Length), all |> APtr.pinArrayPtr |> APtr.map NativePtr.ofNativeInt<GLEnum>)
        )
        //    fbo |> AVal.map (fun fbo ->
        //        if fbo.Handle = 0u then [| GLEnum.Back |]
        //        else signature.ColorAttachments |> Map.toArray |> Array.map (fun (i,_) -> unbox<GLEnum> (int GLEnum.ColorAttachment0 + i))
        //    )
        //backend.DrawBuffers(buffers |> APtr.mapVal (fun b -> uint32 b.Length), APtr.pinAdaptiveArrayPtr buffers |> APtr.map NativePtr.ofNativeInt<GLEnum>)



        this.State.FramebufferSignature <- signature
        match target with
        | FramebufferTarget.Framebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.ReadFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo), APtr.constant (nativeint sizeof<uint32>))
        | FramebufferTarget.DrawFramebuffer ->
            backend.CopyDD (handle, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 4n), APtr.constant (nativeint sizeof<uint32>))
        | _ ->
            ()
            
            
    [<Extension>]
    static member SetFramebuffer(this : CommandStream, fbo : Framebuffer) =
        this.SetFramebuffer(FramebufferTarget.Framebuffer, fbo)
        
            
    [<Extension>]
    static member SetFramebuffer(this : CommandStream, signature : FramebufferSignature, fbo : aval<Framebuffer>) =
        this.SetFramebuffer(FramebufferTarget.Framebuffer, signature, fbo)
        
    
    [<Extension>]
    static member SetViewport(this : CommandStream, viewport : Box2i) =
        let size = V2i.II + viewport.Max - viewport.Min
        this.BaseStream.Viewport(
            viewport.Min.X, 
            viewport.Min.Y, 
            uint32 size.X,
            uint32 size.Y
        )

        let data = APtr.pinArray [| viewport.Min; size|]
        this.BaseStream.CopyDD (data, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i> * 2n))

        
    [<Extension>]
    static member SetViewport(this : CommandStream, offset : nativeptr<V2i>, size : nativeptr<V2i>) =
        let o : nativeptr<int> = NativePtr.cast offset
        let s : nativeptr<uint32> = NativePtr.cast size
        this.BaseStream.Viewport(
            APtr.ofNativePtr o, 
            APtr.ofNativePtr (NativePtr.add o 1), 
            APtr.ofNativePtr s, 
            APtr.ofNativePtr (NativePtr.add s 1)
        )

        this.BaseStream.CopyDD (APtr.ofNativePtr o, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i>))
        this.BaseStream.CopyDD (APtr.ofNativePtr s, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 16n), APtr.constant (nativeint sizeof<V2i>))

        
    [<Extension>]
    static member SetViewport(this : CommandStream, offset : V2i, size : V2i) =
        this.BaseStream.Viewport(
            offset.X, 
            offset.Y, 
            uint32 size.X,
            uint32 size.Y
        )
        let data = APtr.pinArray [| offset; size|]
        this.BaseStream.CopyDD (data, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i> * 2n))
        
    [<Extension>]
    static member SetViewport(this : CommandStream,size : V2i) =
        this.BaseStream.Viewport(
            0, 
            0, 
            uint32 size.X,
            uint32 size.Y
        )
        let data = APtr.pinArray [| V2i.Zero; size|]
        this.BaseStream.CopyDD (data, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i> * 2n))
        
    
    [<Extension>]
    static member SetViewport(this : CommandStream, viewport : aval<Box2i>) =
        let ptr = viewport |> APtr.mapVal(fun b -> V4i(b.Min, V2i.II + b.Max - b.Min))
        this.BaseStream.Viewport(
            APtr.cast ptr, 
            APtr.cast ptr |> APtr.add 1, 
            APtr.cast ptr |> APtr.add 2,
            APtr.cast ptr |> APtr.add 3
        )
        this.BaseStream.CopyDD (ptr, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i> * 2n))
        
    [<Extension>]
    static member SetViewport(this : CommandStream, offset : aval<V2i>, size : aval<V2i>) =
        let ptr = (offset, size) ||> APtr.mapVal2(fun o s -> V4i(o, s))
        this.BaseStream.Viewport(
            APtr.cast ptr, 
            APtr.cast ptr |> APtr.add 1, 
            APtr.cast ptr |> APtr.add 2,
            APtr.cast ptr |> APtr.add 3
        )
        this.BaseStream.CopyDD (ptr, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 8n), APtr.constant (nativeint sizeof<V2i> * 2n))
        
         
    [<Extension>]
    static member SetViewport(this : CommandStream, size : aval<V2i>) =
        let ptr = size |> APtr.ofAVal
        this.BaseStream.Viewport(
            APtr.constant 0, 
            APtr.constant 0, 
            APtr.cast ptr |> APtr.add 0,
            APtr.cast ptr |> APtr.add 1
        )
        this.BaseStream.CopyDD (ptr, APtr.ofNativeInt (NativePtr.toNativeInt this.State.FramebufferInfo + 16n), APtr.constant (nativeint sizeof<V2i>))
        

    [<Extension>]
    static member PushFramebuffer(this : CommandStream) =
        let pp  : nativeptr<uint32>= this.State.FramebufferInfo |> NativePtr.cast
        for o in 0 .. 5 do
            this.BaseStream.Push (NativePtr.add pp o)

        this.State.PushFramebufferSignature()

    [<Extension>]
    static member PushFramebuffer(this : CommandStream, fbo : Framebuffer) =
        this.PushFramebuffer()
        this.SetFramebuffer(FramebufferTarget.Framebuffer, fbo) 
        
   

    [<Extension>]
    static member PopFramebuffer(this : CommandStream) =
        let pp : nativeptr<uint32> = this.State.FramebufferInfo |> NativePtr.cast
        
        for o in 0 .. 5 do
            this.BaseStream.Pop (NativePtr.add pp (5 - o))

        this.BaseStream.BindFramebuffer(APtr.constant FramebufferTarget.ReadFramebuffer, APtr.ofNativePtr pp)
        this.BaseStream.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, APtr.ofNativePtr (NativePtr.add pp 1))
        this.BaseStream.Viewport(
            APtr.ofNativePtr (NativePtr.cast (NativePtr.add pp 2)), APtr.ofNativePtr (NativePtr.cast (NativePtr.add pp 3)),
            APtr.ofNativePtr (NativePtr.cast (NativePtr.add pp 4)), APtr.ofNativePtr (NativePtr.cast (NativePtr.add pp 5))
        )
        match this.State.PopFramebufferSignature() with
        | Some s ->
            ()
            //let back = [| GLEnum.Back |]
            //let all = s.GetDrawBuffers()

            //this.BaseStream.Switch(
            //    APtr.ofNativePtr (NativePtr.cast (NativePtr.add pp 1)),
            //    [
            //        0, fun cmd -> 
            //            cmd.DrawBuffers(APtr.constant (uint32 back.Length), APtr.pinArrayPtr back |> APtr.map NativePtr.ofNativeInt<GLEnum>)
                        
            //    ],
            //    fun cmd -> 
            //        cmd.DrawBuffers(APtr.constant (uint32 all.Length), APtr.pinArrayPtr all |> APtr.map NativePtr.ofNativeInt<GLEnum>)
            //)

        | None ->
            ()
        
        
    [<Extension>]
    static member UseFramebuffer(this : CommandStream, fbo : Framebuffer, action : CommandStream -> unit) =
        this.PushFramebuffer fbo
        try action this
        finally this.PopFramebuffer()
        
    [<Extension>]
    static member ReadBuffer(this : CommandStream, buffer : ReadBufferMode) =
        this.BaseStream.ReadBuffer(buffer)
  
    [<Extension>]
    static member DrawBuffers(this : CommandStream, buffers : DrawBufferMode[]) =
        let pBuffers = APtr.pinArrayPtr buffers |> APtr.map NativePtr.ofNativeInt
        this.BaseStream.DrawBuffers(APtr.constant (uint32 buffers.Length), pBuffers)
        
    [<Extension>]
    static member ReadBuffer(this : CommandStream, buffer : int) =
        this.ReadBuffer(unbox<ReadBufferMode> (int ReadBufferMode.ColorAttachment0 + buffer))
   
    [<Extension>]
    static member BlitFramebuffer(this : CommandStream, srcOffset : V2i, srcSize : V2i, dstOffset : V2i, dstSize : V2i, mask : ClearBufferMask, filter : BlitFramebufferFilter) =
        this.BaseStream.BlitFramebuffer(
            srcOffset.X, srcOffset.Y,
            srcOffset.X + srcSize.X, srcOffset.Y + srcSize.Y,
            dstOffset.X, dstOffset.Y,
            dstOffset.X + dstSize.X, dstOffset.Y + dstSize.Y,
            mask, filter
        )
        
    [<Extension>]
    static member Blit(this : CommandStream, src : SubRenderbuffer, dst : SubRenderbuffer, ?linear : bool) =  
        
        let filter =
            match linear with
            | Some false -> BlitFramebufferFilter.Nearest
            | _ -> BlitFramebufferFilter.Linear

        let srcDepth = TextureFormat.isDepth src.Renderbuffer.Format
        let dstDepth = TextureFormat.isDepth dst.Renderbuffer.Format
        if srcDepth <> dstDepth then
            failf "cannot blit %A to %A" src.Renderbuffer.Format dst.Renderbuffer.Format

        let attachment =
            if srcDepth then FramebufferAttachment.DepthAttachment
            else FramebufferAttachment.ColorAttachment0
            
        let clearMask =
            if srcDepth then ClearBufferMask.DepthBufferBit
            else ClearBufferMask.ColorBufferBit

        let b = this.BaseStream

        let fbos = APtr.temporary<uint32> 2
        b.GenFramebuffers(APtr.constant 2u, fbos)

        this.PushFramebuffer()


        b.BindFramebuffer(APtr.constant FramebufferTarget.ReadFramebuffer, fbos)
        b.FramebufferRenderbuffer(FramebufferTarget.ReadFramebuffer, attachment, RenderbufferTarget.Renderbuffer, src.Renderbuffer.Handle)

        b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, APtr.add 1 fbos)
        b.FramebufferRenderbuffer(FramebufferTarget.DrawFramebuffer, attachment, RenderbufferTarget.Renderbuffer, src.Renderbuffer.Handle)

        let s0 = src.Offset
        let s1 = src.Offset + src.Size

        let d0 = dst.Offset
        let d1 = dst.Offset + dst.Size

        b.BlitFramebuffer(
            s0.X, s0.Y, s1.X, s1.Y,
            d0.X, d0.Y, d1.X, d1.Y,
            clearMask,
            filter
        )

        b.DeleteFramebuffers(APtr.constant 2u, fbos)

        this.PopFramebuffer()

    [<Extension>]
    static member Blit(this : CommandStream, src : SubRenderbuffer, dst : SubTextureImage, ?linear : bool) =  
        
        let filter =
            match linear with
            | Some false -> BlitFramebufferFilter.Nearest
            | _ -> BlitFramebufferFilter.Linear

        let srcDepth = TextureFormat.isDepth src.Renderbuffer.Format
        let dstDepth = TextureFormat.isDepth dst.Texture.Format
        if srcDepth <> dstDepth then
            failf "cannot blit %A to %A" src.Renderbuffer.Format dst.Texture.Format

        let attachment =
            if srcDepth then FramebufferAttachment.DepthAttachment
            else FramebufferAttachment.ColorAttachment0
            
        let clearMask =
            if srcDepth then ClearBufferMask.DepthBufferBit
            else ClearBufferMask.ColorBufferBit

        let b = this.BaseStream

        let fbos = APtr.temporary<uint32> 2
        b.GenFramebuffers(APtr.constant 2u, fbos)

        this.PushFramebuffer()


        b.BindFramebuffer(APtr.constant FramebufferTarget.ReadFramebuffer, fbos)
        b.FramebufferRenderbuffer(FramebufferTarget.ReadFramebuffer, attachment, RenderbufferTarget.Renderbuffer, src.Renderbuffer.Handle)

        b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, APtr.add 1 fbos)
        match dst.Texture.Layers with
        | Some _ ->
            if dst.Layers > 1 then failf "cannot blit to multi-layer texture"
            b.FramebufferTextureLayer(FramebufferTarget.DrawFramebuffer, attachment, dst.Texture.Handle, dst.Level, dst.BaseLayer)
        | None -> 
            let dstTarget =
                match dst.Texture.Dimension with
                | TextureDimension.Texture2D -> TextureTarget.Texture2D
                | TextureDimension.TextureCube -> allCubeFaces.[dst.Layer]
                | _ -> failf "cannot copy to %A" dst.Texture.Dimension
        
            b.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, attachment, dstTarget, dst.Texture.Handle, dst.Level)

        let s0 = src.Offset
        let s1 = src.Offset + src.Size

        let d0 = dst.Offset.XY
        let d1 = dst.Offset.XY + dst.Size3D.XY

        b.BlitFramebuffer(
            s0.X, s0.Y, s1.X, s1.Y,
            d0.X, d0.Y, d1.X, d1.Y,
            clearMask,
            filter
        )

        b.DeleteFramebuffers(APtr.constant 2u, fbos)

        this.PopFramebuffer()

    [<Extension>]
    static member Blit(this : CommandStream, src : SubTextureImage, dst : SubTextureImage, ?linear : bool) =  

        let filter =
            match linear with
            | Some false -> BlitFramebufferFilter.Nearest
            | _ -> BlitFramebufferFilter.Linear

        let srcDepth = TextureFormat.isDepth src.Texture.Format
        let dstDepth = TextureFormat.isDepth dst.Texture.Format
        if srcDepth <> dstDepth then
            failf "cannot blit %A to %A" src.Texture.Format dst.Texture.Format

        let attachment =
            if srcDepth then FramebufferAttachment.DepthAttachment
            else FramebufferAttachment.ColorAttachment0
            
        let clearMask =
            if srcDepth then ClearBufferMask.DepthBufferBit
            else ClearBufferMask.ColorBufferBit

        let b = this.BaseStream

        let fbos = APtr.temporary<uint32> 2
        b.GenFramebuffers(APtr.constant 2u, fbos)

        this.PushFramebuffer()

        b.BindFramebuffer(APtr.constant FramebufferTarget.ReadFramebuffer, fbos)
        match src.Texture.Layers with
        | Some _ ->
            if src.Layers > 1 then failf "cannot blit to multi-layer texture"
            b.FramebufferTextureLayer(FramebufferTarget.ReadFramebuffer, attachment, src.Texture.Handle, src.Level, src.BaseLayer)
        | None -> 
            let srcTarget =
                match src.Texture.Dimension with
                | TextureDimension.Texture2D -> TextureTarget.Texture2D
                | TextureDimension.TextureCube -> allCubeFaces.[src.Layer]
                | _ -> failf "cannot copy to %A" src.Texture.Dimension
            b.FramebufferTexture2D(FramebufferTarget.ReadFramebuffer, attachment, srcTarget, src.Texture.Handle, src.Level)

        b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, APtr.add 1 fbos)
        match dst.Texture.Layers with
        | Some _ ->
            if dst.Layers > 1 then failf "cannot blit to multi-layer texture"
            b.FramebufferTextureLayer(FramebufferTarget.DrawFramebuffer, attachment, dst.Texture.Handle, dst.Level, dst.BaseLayer)
        | None -> 
            let dstTarget =
                match dst.Texture.Dimension with
                | TextureDimension.Texture2D -> TextureTarget.Texture2D
                | TextureDimension.TextureCube -> allCubeFaces.[dst.Layer]
                | _ -> failf "cannot copy to %A" dst.Texture.Dimension
        
            b.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, attachment, dstTarget, dst.Texture.Handle, dst.Level)

        let s0 = src.Offset.XY
        let s1 = src.Offset.XY + src.Size3D.XY

        let d0 = dst.Offset.XY
        let d1 = dst.Offset.XY + dst.Size3D.XY

        b.BlitFramebuffer(
            s0.X, s0.Y, s1.X, s1.Y,
            d0.X, d0.Y, d1.X, d1.Y,
            clearMask,
            filter
        )

        b.DeleteFramebuffers(APtr.constant 2u, fbos)

        this.PopFramebuffer()

    [<Extension>]
    static member Blit(this : CommandStream, src : SubTextureImage, dst : SubRenderbuffer, ?linear : bool) =  
        let filter =
            match linear with
            | Some false -> BlitFramebufferFilter.Nearest
            | _ -> BlitFramebufferFilter.Linear

        let srcDepth = TextureFormat.isDepth src.Texture.Format
        let dstDepth = TextureFormat.isDepth dst.Renderbuffer.Format
        if srcDepth <> dstDepth then
            failf "cannot blit %A to %A" src.Texture.Format dst.Renderbuffer.Format

        let attachment =
            if srcDepth then FramebufferAttachment.DepthAttachment
            else FramebufferAttachment.ColorAttachment0
            
        let clearMask =
            if srcDepth then ClearBufferMask.DepthBufferBit
            else ClearBufferMask.ColorBufferBit

        let b = this.BaseStream

        let fbos = APtr.temporary<uint32> 2
        b.GenFramebuffers(APtr.constant 2u, fbos)

        this.PushFramebuffer()

        b.BindFramebuffer(APtr.constant FramebufferTarget.ReadFramebuffer, fbos)
        match src.Texture.Layers with
        | Some _ ->
            if src.Layers > 1 then failf "cannot blit to multi-layer texture"
            b.FramebufferTextureLayer(FramebufferTarget.ReadFramebuffer, attachment, src.Texture.Handle, src.Level, src.BaseLayer)
        | None -> 
            let srcTarget =
                match src.Texture.Dimension with
                | TextureDimension.Texture2D -> TextureTarget.Texture2D
                | TextureDimension.TextureCube -> allCubeFaces.[src.Layer]
                | _ -> failf "cannot copy to %A" src.Texture.Dimension
            b.FramebufferTexture2D(FramebufferTarget.ReadFramebuffer, attachment, srcTarget, src.Texture.Handle, src.Level)


        b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, APtr.add 1 fbos)
        b.FramebufferRenderbuffer(FramebufferTarget.DrawFramebuffer, attachment, RenderbufferTarget.Renderbuffer, dst.Renderbuffer.Handle)

        let s0 = src.Offset.XY
        let s1 = src.Offset.XY + src.Size3D.XY

        let d0 = dst.Offset
        let d1 = dst.Offset + dst.Size

        b.BlitFramebuffer(
            s0.X, s0.Y, s1.X, s1.Y,
            d0.X, d0.Y, d1.X, d1.Y,
            clearMask,
            filter
        )

        b.DeleteFramebuffers(APtr.constant 2u, fbos)

        this.PopFramebuffer()



    [<Extension>]
    static member Clear(this : CommandStream, texture : TextureImage, color : C4f) =
        let b = this.BaseStream
        let attachment = FramebufferAttachment.ColorAttachment0
        this.PushFramebuffer()
        let fbo = APtr.temporary 1
        b.GenFramebuffers(APtr.constant 1u, fbo)
        b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, fbo)
        match texture.Texture.Layers with
        | Some _ ->
            if texture.Layers > 1 then failf "cannot clear multi-layer texture"
            b.FramebufferTextureLayer(FramebufferTarget.DrawFramebuffer, attachment, texture.Texture.Handle, texture.Level, texture.BaseLayer)
        | None -> 
            let srcTarget =
                match texture.Texture.Dimension with
                | TextureDimension.Texture2D -> TextureTarget.Texture2D
                | TextureDimension.TextureCube -> allCubeFaces.[texture.Layer]
                | _ -> failf "cannot clear %A" texture.Texture.Dimension
            b.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, attachment, srcTarget, texture.Texture.Handle, texture.Level)


        b.ClearColor(color.R, color.G, color.B, color.A)
        b.Clear(ClearBufferMask.ColorBufferBit)
        b.DeleteFramebuffers(APtr.constant 1u, fbo)
        this.PopFramebuffer()
        
    [<Extension>]
    static member ClearDepthStencil(this : CommandStream, texture : TextureImage, ?depth : float, ?stencil : int) =
        let b = this.BaseStream
        let mask =
            match depth with
            | Some d ->
                b.ClearDepthf(float32 d)
                match stencil with
                | Some stencil ->   
                    b.ClearStencil(stencil)
                    ClearBufferMask.DepthBufferBit ||| ClearBufferMask.StencilBufferBit
                | None ->
                    ClearBufferMask.DepthBufferBit 
            | None ->
                match stencil with
                | Some stencil ->   
                    b.ClearStencil(stencil)
                    ClearBufferMask.StencilBufferBit
                | None -> 
                    unbox<ClearBufferMask> 0

        if int mask <> 0 then
            let attachment = FramebufferAttachment.DepthAttachment

            this.PushFramebuffer()
            let fbo = APtr.temporary 1
            b.GenFramebuffers(APtr.constant 1u, fbo)
            b.BindFramebuffer(APtr.constant FramebufferTarget.DrawFramebuffer, fbo)
            match texture.Texture.Layers with
            | Some _ ->
                if texture.Layers > 1 then failf "cannot clear multi-layer texture"
                b.FramebufferTextureLayer(FramebufferTarget.DrawFramebuffer, attachment, texture.Texture.Handle, texture.Level, texture.BaseLayer)
            | None -> 
                let srcTarget =
                    match texture.Texture.Dimension with
                    | TextureDimension.Texture2D -> TextureTarget.Texture2D
                    | TextureDimension.TextureCube -> allCubeFaces.[texture.Layer]
                    | _ -> failf "cannot clear %A" texture.Texture.Dimension
                b.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, attachment, srcTarget, texture.Texture.Handle, texture.Level)

            b.Clear mask
            b.DeleteFramebuffers(APtr.constant 1u, fbo)
            this.PopFramebuffer()

