namespace Aardworx.Rendering.WebGL

open Aardvark.Base
open Aardvark.Rendering
open System.Runtime.CompilerServices

type FramebufferSignature(device : Device, attachments : Map<int, AttachmentSignature>, depth : option<TextureFormat>, layers : int, samples : int) =
    let attachmentIndices, attachmentCount =
        let count = ref 0
        let indices = 
            attachments
            |> Map.toList
            |> List.map (fun (id, att) ->
                count.Value <- count.Value + 1
                att.Name, id
            )
            |> Map.ofList
        indices, count.Value

    static let empty = new FramebufferSignature(Unchecked.defaultof<Device>, Map.empty, None, 0, 0)
    static member Empty = empty

    member x.Device = device
    member x.AttachmentCount = attachmentCount
    member x.AttachmentIndices = attachmentIndices
    member x.ColorAttachments = attachments
    member x.Depth = depth
    member x.Samples = samples
    member x.Layers = layers
    
    interface IFramebufferSignature with
        member this.ColorAttachments = this.ColorAttachments
        member this.DepthStencilAttachment = this.Depth 
        member this.LayerCount = this.Layers
        member this.PerLayerUniforms = Set.empty
        member this.Runtime = device.Runtime :> _
        member this.Samples = samples
        member this.Dispose() = ()
        
[<AbstractClass; Sealed; Extension>]
type DeviceFramebufferSignatureExtensions private() =   
    [<Extension>]
    static member CreateFramebufferSignature(this : Device, attachments : Map<int, AttachmentSignature>, depth : option<TextureFormat>, ?samples : int) =
        new FramebufferSignature(this, attachments, depth, 1, defaultArg samples 1)
