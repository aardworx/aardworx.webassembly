namespace Aardvark.Dom


type RenderControl =

    static member FXAA = Attribute("data-antialiasing", AttributeValue.String "fxaa")
    static member MSAA(samples : int) = Attribute("data-samples", AttributeValue.Int samples)

    /// Override the canvas backbuffer pixel ratio. Omit the attribute to use the
    /// browser's devicePixelRatio (full-res on HiDPI / Retina, the default).
    /// Pass 1.0 to disable HiDPI scaling and render at CSS resolution.
    static member PixelRatio(ratio : float) =
        Attribute("data-pixel-ratio", AttributeValue.String (sprintf "%g" ratio))
