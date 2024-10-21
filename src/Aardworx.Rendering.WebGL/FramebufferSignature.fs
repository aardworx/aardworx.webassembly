namespace Aardworx.Rendering.WebGL

open Aardvark.Base
open Aardvark.Rendering
open System.Runtime.CompilerServices

/// Signature of a framebuffer.
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

    /// The device.
    member x.Device = device
    
    /// The number of color attachments.
    member x.AttachmentCount = attachmentCount
    
    // The indices of the color attachments.
    member x.AttachmentIndices = attachmentIndices
    
    /// The color attachments.
    member x.ColorAttachments = attachments
    
    /// The depth attachment.
    member x.Depth = depth
    
    /// The number of samples.
    member x.Samples = samples
    
    /// The number of layers.
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
    
    /// Creates a framebuffer signature.
    [<Extension>]
    static member CreateFramebufferSignature(this : Device, attachments : Map<int, AttachmentSignature>, depth : option<TextureFormat>, ?samples : int) =
        new FramebufferSignature(this, attachments, depth, 1, defaultArg samples 1)
