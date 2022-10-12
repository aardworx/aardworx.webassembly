namespace Aardvark.Dom


type RenderControl =
    
    static member FXAA = Attribute("data-antialiasing", AttributeValue.String "fxaa")
    static member MSAA(samples : int) = Attribute("data-samples", AttributeValue.Int samples)
