namespace Aardworx.Rendering.WebGL

open System
open Silk.NET.OpenGLES
open FSharp.Data.Adaptive
open Aardvark.Base
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL

#nowarn "9"





type Queue<'a>() =
    static let initialCapacity = 16
    
    let mutable mask = initialCapacity - 1
    let mutable store = Array.zeroCreate initialCapacity
    let mutable count = 0
    let mutable index = 0
    let mutable first = -1

    member x.Count = count

    member x.CopyTo(dst : 'a[], dstIndex : int) =
        let mutable idx = first //(index + store.Length - count) &&& mask
        let mutable oi = dstIndex
        for _ in 0 .. count-1 do
            dst.[oi] <- store.[idx]
            idx <- (idx + 1) &&& mask
            oi <- oi + 1
            
    member x.CopyTo(dst : 'b[], dstIndex : int, mapping : 'a -> 'b) =
        let mutable idx = first //(index + store.Length - count) &&& mask
        let mutable oi = dstIndex
        for _ in 0 .. count-1 do
            dst.[oi] <- mapping store.[idx]
            idx <- (idx + 1) &&& mask
            oi <- oi + 1

    member x.ToArray() =
        let arr = Array.zeroCreate count
        x.CopyTo(arr, 0)
        arr

    member x.ToArray(mapping : 'a -> 'b) =
        let arr = Array.zeroCreate count
        x.CopyTo(arr, 0, mapping)
        arr
      
    member x.Enqueue(element : 'a) =
        if count >= store.Length then
            let newStore = Array.zeroCreate (store.Length <<< 1)
            let mutable idx = index // store is full
            for i in 0 .. count-1 do
                newStore.[i] <- store.[idx]
                idx <- (idx + 1) &&& mask

            first <- 0
            index <- count
            mask <- newStore.Length - 1
            store <- newStore

        if count = 0 then
            store.[0] <- element
            index <- 1
            first <- 0
            count <- 1
        else
            store.[index] <- element
            index <- (index + 1) &&& mask
            count <- count + 1

    member x.Dequeue() =
        if count <= 0 then failwith "empty"

        let v = store.[first]
        count <- count - 1

        if store.Length > initialCapacity && count < (store.Length >>> 1) then
            let newStore = Array.zeroCreate (store.Length >>> 1)
            first <- (first + 1) &&& mask
            for i in 0 .. count - 1 do
                newStore.[i] <- store.[first]
                first <- (first + 1) &&& mask

            first <- 0
            index <- count
            mask <- newStore.Length - 1
            store <- newStore
        else
            store.[first] <- Unchecked.defaultof<'a>
            first <- (first + 1) &&& mask



        v

    member x.Item
        with get(i : int) = 
            if i < 0 || i >= count then failwithf "index out of range: %d (%d)" i count
            store.[(first + i) &&& mask]

    member x.GetEnumerator() = new QueueEnumerator<'a>(store, mask, first, count)

    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = new QueueEnumerator<'a>(store, mask, first, count) :> _

    interface System.Collections.Generic.IEnumerable<'a> with
        member x.GetEnumerator() = new QueueEnumerator<'a>(store, mask, first, count) :> _
        
    static member Test() =
        let q = Queue<int>()

        for i in 1 .. 6 do q.Enqueue i

        printfn "%0A" (q.ToArray())
        printfn "%0A" (q |> Seq.toArray)

        printfn "d(): %A" (q.Dequeue())
        printfn "d(): %A" (q.Dequeue())
        printfn "d(): %A" (q.Dequeue())
        printfn "d(): %A" (q.Dequeue())
        printfn "%0A" (q.ToArray())
        printfn "%0A" (q |> Seq.toArray)
        printfn "d(): %A" (q.Dequeue())
        printfn "d(): %A" (q.Dequeue())

and QueueEnumerator<'a> internal(store : 'a[], mask : int, index : int, count : int) =
    
    let mutable c = count
    let mutable i = index - 1

    member x.MoveNext() =
        if c > 0 then
            c <- c - 1
            i <- (i + 1) &&& mask
            true
        else    
            false

    member x.Current = store.[i]

    member x.Reset() =
        c <- count
        i <- index - 1

    member x.Dispose() =
        ()

    interface System.Collections.IEnumerator with
        member x.MoveNext() = x.MoveNext()
        member x.Reset() = x.Reset()
        member x.Current = x.Current :> obj

    interface System.Collections.Generic.IEnumerator<'a> with
        member x.Current = x.Current
        member x.Dispose() = x.Dispose()






open Aardvark.SceneGraph

module private ResolveShader =
    open FShade

    let nearestSam =
        sampler2d {
            texture uniform?DiffuseColorTexture
            addressU WrapMode.Wrap
            addressV WrapMode.Wrap
            filter Filter.MinMagPoint
        }

    let resolve (v : Effects.Vertex) =
        fragment {
            return nearestSam.SampleLevel(v.tc, 0.0)
        }

type RenderControl(element : HTMLCanvasElement, ?scale : float, ?commandStreamMode : CommandStreamMode) =
    let scale = defaultArg scale 1.0
    let commandStreamMode = defaultArg commandStreamMode CommandStreamMode.Managed
    let ctx =   
        let oid = element.Id
        if String.IsNullOrWhiteSpace oid then
            try 
                let id = RandomElementId()
                element.Id <- id
                WebGLContext.Create("#" + id)
            finally
                element.Id <- oid
        else
            WebGLContext.Create ("#" + oid)

    let device = Device(ctx)
    do Log.line "%s" (DeviceInformation.toString true device.Info)
    let runtime, _manager = 
        let r = Runtime(device, commandStreamMode) 
        r :> IRuntime, r.ResourceManager

    let keyboard = new NodeKeyboard(element)
    let mouse = new NodeMouse(element)

    let t0 = DateTime.Now
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let time = cval (t0 + sw.Elapsed)


    let signature =
        runtime.CreateFramebufferSignature [
            DefaultSemantic.Colors, TextureFormat.Rgba8
            DefaultSemantic.DepthStencil, TextureFormat.Depth24Stencil8
        ] :?> FramebufferSignature

    let clearTask = runtime.CompileClear(signature, C4f.Black, 1.0, 0)
    let mutable renderTask = RenderTask.empty
    let mutable renderTaskSub = { new IDisposable with member x.Dispose() = () }
    let mutable dirty = true
    let size = cval V2i.II


    let colorTexture = cval (device.CreateTexture2D(TextureFormat.Rgba8, size.Value))
    let mutable backbuffer =
       let color = colorTexture.Value
       let depth = device.CreateTexture2D(TextureFormat.Depth24Stencil8, size.Value)
       device.CreateFramebuffer(
           signature, [
               DefaultSemantic.Colors, color.[0,0] :> IFramebufferOutput
               DefaultSemantic.DepthStencil, depth.[0,0] :> IFramebufferOutput
           ]
       )
    let mutable framebuffer = device.DefaultFramebuffer size.Value

    let finalTask =
        Sg.fullScreenQuad
        |> Sg.diffuseTexture colorTexture
        |> Sg.shader {
            do! ResolveShader.resolve
        }
        |> Sg.compile runtime signature



    //let mutable framebuffer = device.DefaultFramebuffer V2i.II
    let mutable vsync = true

    let beforeRender = Event<unit>()
    let afterRender = Event<unit>()

    let frameTime = Event<MicroTime * MicroTime>()
    let watch = System.Diagnostics.Stopwatch()
    let emitTime (count : int) (cpu : float) (gpu : float) =
        frameTime.Trigger (MicroTime.FromSeconds (cpu / float count), MicroTime.FromSeconds (gpu / float count))

    let mutable frameCount = 0
    let mutable initial = true

    //let query = device.Run (fun gl -> gl.GenQuery())

   
    let queries = Queue<uint32 * float>()

    let adjustSize(inv : unit -> unit) =
        let htmlSize =
            let r = element.GetBoundingClientRect()
            r.Size

        let viewSize =
            V2i(round (Window.DevicePixelRatio * htmlSize))

        let renderSize =
            V2i(round (htmlSize * scale))


        if viewSize <> size.Value then
            transact (fun () ->     
                size.Value <- viewSize
                colorTexture.Value <- device.CreateTexture2D(TextureFormat.Rgba8, renderSize)
            )
            //framebuffer <- device.DefaultFramebuffer viewSize
            backbuffer.Attachments |> Map.iter (fun _ o ->
               let o = o :?> TextureLevel
               o.Texture.Dispose()
            )   
            backbuffer.Dispose()
            backbuffer <-
               let depth = device.CreateTexture2D(TextureFormat.Depth24Stencil8, renderSize)
               device.CreateFramebuffer(
                   signature, [
                       DefaultSemantic.Colors, colorTexture.Value.[0,0] :> IFramebufferOutput
                       DefaultSemantic.DepthStencil, depth.[0,0] :> IFramebufferOutput
                   ]
               )
            framebuffer <- device.DefaultFramebuffer viewSize

            element.Width <- viewSize.X
            element.Height <- viewSize.Y
            inv()

    let rec render() : unit =
        
        if not initial then
            if not watch.IsRunning then watch.Start()
        
        adjustSize id

        device.Run (fun gl ->
            beforeRender.Trigger()
            
            let mutable run = true
            while run && queries.Count > 0 do
                let q0, cpu = queries.[0]
                if device.Info.Features.TimerQuery then
                    let fin = gl.GetQueryObject(q0, QueryObjectParameterName.QueryResultAvailable)
                    if fin <> 0u then
                        let t = gl.GetQueryObject(q0, QueryObjectParameterName.QueryResult)
                        emitTime 1 cpu (float t / 1000000000.0)
                        queries.Dequeue() |> ignore
                        gl.DeleteQuery q0
                    else
                        run <- false
                else
                    emitTime 1 cpu 0.0
                    queries.Dequeue() |> ignore


            let mutable q = 0u
            if device.Info.Features.TimerQuery then
                q <- gl.GenQuery()
                gl.BeginQuery(QueryTarget.TimeElapsed, q)
            let sw = System.Diagnostics.Stopwatch.StartNew()
            lock renderTask (fun () ->
                clearTask.Run(RenderToken.Empty, backbuffer)
                renderTask.Run(RenderToken.Empty, backbuffer)
            )

            finalTask.Run(RenderToken.Empty, framebuffer)

            sw.Stop()
            
            if device.Info.Features.TimerQuery then
                gl.EndQuery(QueryTarget.TimeElapsed)
            gl.Flush()

            queries.Enqueue(q, sw.Elapsed.TotalSeconds)


            afterRender.Trigger()
        )

        transact (fun () -> time.Value <- t0 + sw.Elapsed)

        if not initial then
            frameCount <- frameCount + 1
        initial <- false
        if renderTask.OutOfDate then
            if frameCount >= 20 then 
                watch.Stop()
                //emitTime frameCount watch.Elapsed.TotalSeconds
                watch.Reset()
                frameCount <- 0
            if vsync then Window.RequestAnimationFrame render
            else Window.SetTimeout(0, render) |> ignore //System.Threading.Tasks.Task.Factory.StartNew render |> ignore
        else
            watch.Stop()
            if frameCount >= 20 then 
                //emitTime frameCount watch.Elapsed.TotalSeconds
                watch.Reset()
                frameCount <- 0
            dirty <- false


    let invalidate() =
        if not dirty then
            dirty <- true
            if vsync then Window.RequestAnimationFrame render
            else Window.SetTimeout(0, render) |> ignore//System.Threading.Tasks.Task.Factory.StartNew render |> ignore
            
    let rec checkResize() =
        adjustSize invalidate

        Window.SetTimeout(50, checkResize) |> ignore

    do checkResize()

    let mutable cursor = Aardvark.Application.Cursor.Default

    member x.Device = device
    
    member x.FrameTime = frameTime.Publish

    member x.CreateFrameTimeGraph(?size : V2i, ?count : int, ?windowSize : int, ?background : C4b, ?gpu : C4b, ?cpu : C4b) =
        let document = Window.Document

        let size = defaultArg size (V2i(300,200))
        let elems = defaultArg count 50
        let windowSize = defaultArg windowSize 16
        let background = defaultArg background (C4b(0uy,0uy,0uy,127uy))
        let gpu = defaultArg gpu C4b.Green
        let cpu = defaultArg cpu C4b.Red

        let svg = document.CreateCanvasElement()
        svg.Style.Width <- sprintf "%dpx" size.X
        svg.Style.Height <- sprintf "%dpx" size.Y
        svg.Width <- size.X
        svg.Height <- size.Y
        let ctx = svg.GetContext2d()


        let background = sprintf "rgba(%d,%d,%d,%f)" background.R background.G background.B (float background.A / 255.0)
        let cc = sprintf "rgba(%d,%d,%d,%f)" cpu.R cpu.G cpu.B (float cpu.A / 255.0)
        let gc = sprintf "rgba(%d,%d,%d,%f)" gpu.R gpu.G gpu.B (float gpu.A / 255.0)

        ctx.LineCap <- "round"
        ctx.FillStyle <- background
        ctx.Font <- "20px monospace"
        ctx.LineWidth <- 4.0

        ctx.FillRect(V2d.Zero, V2d size)


        let values = Queue<V3d>()
        let mutable iter = 0

        let mutable lastSum = V2d.Zero
        let mutable lastCount = 0

        let mutable first = true

        let append (cpu : MicroTime, gpu : MicroTime) =
            if first then
                first <- false
            else
                lastSum <- lastSum + V2d(gpu.TotalMilliseconds, cpu.TotalMilliseconds)
                lastCount <- lastCount + 1

                if lastCount >= windowSize then
                    let value = lastSum / float lastCount
                    let gpuTime = MicroTime.FromMilliseconds value.X
                    let cpuTime = MicroTime.FromMilliseconds value.Y
                    values.Enqueue(V3d(float iter, value.X, value.Y))
                    if values.Count > elems then values.Dequeue() |> ignore
                    lastSum <- V2d.Zero
                    lastCount <- 0
                    iter <- iter + 1

                    let mutable bb = Box2d.Invalid
                    bb.ExtendBy(V2d(values.[0].X, 0.0))
                    for i in 0 .. values.Count - 1 do 
                        let v = values.[i]
                        if v.Y >= 0.0 then bb.ExtendBy v.XY
                        if v.Z >= 0.0 then bb.ExtendBy v.XZ

                    if values.Count > 1 then

                        let bb = 
                            let mutable o = bb.Min
                            let mutable s = bb.Size

                            if s.X < 50.0 then s.X <- 50.0
                            if s.Y < 4.0 then s.Y <- 4.0
                            else s.Y <- ceil (s.Y / 4.0) * 4.0

                            Box2d(o, o + s)


                        let points =
                            let range = bb.Size
                            values.ToArray(fun p ->
                                let r = (p - bb.Min.XYY) / range.XYY
                                let c = V3d(r.X, 1.0 - r.Y, 1.0 - r.Z)
                                (c * V3d size.XYY)
                            )

                        let pg = points |> Array.map (fun p -> p.XY)
                        let pc = points |> Array.map (fun p -> p.XZ)

                        ctx.ClearRect(0.0, 0.0, float size.X, float size.Y)
                        ctx.FillRect(0.0, 0.0, float size.X, float size.Y)


                        ctx.StrokeStyle <- gc
                        ctx.BeginPath()
                        ctx.MoveTo(pg.[0])
                        for i in 1 .. pg.Length - 1 do
                            ctx.LineTo(pg.[i])
                        ctx.Stroke()
                    
                        ctx.StrokeStyle <- cc
                        ctx.BeginPath()
                        ctx.MoveTo(pc.[0])
                        for i in 1 .. pc.Length - 1 do
                            ctx.LineTo(pc.[i])
                        ctx.Stroke()



                        let text = sprintf "GPU: %.1fms\nCPU:%.1fms" gpuTime.TotalMilliseconds cpuTime.TotalMilliseconds
                        let padding = 10
                        ctx.FillStyle <- "white"
                        ctx.FillText(text, float padding, float padding + 20.0)
                        ctx.FillStyle <- background

        x.FrameTime.Add append

        svg :> HTMLElement
        
        









    member x.RenderTask
        with get() = renderTask
        and set t =
            if t <> renderTask then
                renderTaskSub.Dispose()
                renderTask <- t
                renderTaskSub <- t.AddMarkingCallback invalidate
                invalidate()

    member x.VSync
        with get() = vsync
        and set v = vsync <- v

    member x.Visible
        with get() = element.Style.Visibility <> "hidden"
        and set v =
            if v then element.Style.Visibility <- ""
            else element.Style.Visibility <- "hidden"

    member x.Start() =
        x.Visible <- true
        render()

    member x.BeforeRender = beforeRender.Publish
    member x.AfterRender = afterRender.Publish
    member x.FramebufferSignature = signature :> IFramebufferSignature
    member x.Runtime = runtime
    member x.Samples = 1
    member x.Time = time
    member x.Sizes = size :> aval<_>
    member x.SubSampling 
        with get() = 1.0
        and set (_ : float)  = ()

    member x.Keyboard = keyboard :> Aardvark.Application.IKeyboard
    member x.Mouse = mouse :> Aardvark.Application.IMouse
    member x.Cursor
        with get() = cursor
        and set c =
            if c <> cursor then
                cursor <- c
                match c with
                | Aardvark.Application.Cursor.None -> element.Style.Cursor <- "none"
                | Aardvark.Application.Cursor.Default -> element.Style.Cursor <- "default"
                | Aardvark.Application.Cursor.Arrow -> element.Style.Cursor <- "default"
                | Aardvark.Application.Cursor.Crosshair -> element.Style.Cursor <- "crosshair"
                | Aardvark.Application.Cursor.Hand -> element.Style.Cursor <- "pointer"
                | Aardvark.Application.Cursor.HorizontalResize -> element.Style.Cursor <- "col-resize"
                | Aardvark.Application.Cursor.VerticalResize -> element.Style.Cursor <- "row-resize"
                | Aardvark.Application.Cursor.Text -> element.Style.Cursor <- "text"
                | Aardvark.Application.Cursor.Custom _ -> element.Style.Cursor <- "default"

    interface Aardvark.Application.IRenderTarget with
        member x.BeforeRender = beforeRender.Publish
        member x.AfterRender = beforeRender.Publish
        member x.FramebufferSignature = signature :> _
        member x.Runtime = runtime
        member x.Samples = 1
        member x.Time = time :> aval<_>
        member x.Sizes = size :> aval<_>
        member x.SubSampling 
            with get() = 1.0
            and set _  = ()
        member x.RenderTask
            with get() = x.RenderTask
            and set v = x.RenderTask <- v

    interface Aardvark.Application.IRenderControl with
        member x.Keyboard = keyboard :> Aardvark.Application.IKeyboard
        member x.Mouse = mouse :> Aardvark.Application.IMouse
        member x.Cursor
            with get() = x.Cursor
            and set c = x.Cursor <- c
