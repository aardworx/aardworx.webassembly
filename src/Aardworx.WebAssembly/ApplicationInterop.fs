namespace rec Aardworx.WebAssembly

open System
open Aardvark.Base

#nowarn "9"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Keys =
    open Aardvark.Application

    let tryOfKeyCode =
        LookupTable.lookupTable' [
            8,      Keys.Back
            9,      Keys.Tab
            13,     Keys.Enter
            16,     Keys.LeftShift
            17,     Keys.LeftCtrl
            18,     Keys.LeftAlt
            19,     Keys.Pause
            20,     Keys.CapsLock
            27,     Keys.Escape
            32,     Keys.Space
            33,     Keys.PageUp
            34,     Keys.PageDown
            35,     Keys.End
            36,     Keys.Home
            37,     Keys.Left
            38,     Keys.Up
            39,     Keys.Right
            40,     Keys.Down
            45,     Keys.Insert
            46,     Keys.Delete

            48,     Keys.D0
            49,     Keys.D1
            50,     Keys.D2
            51,     Keys.D3
            52,     Keys.D4
            53,     Keys.D5
            54,     Keys.D6
            55,     Keys.D7
            56,     Keys.D8
            57,     Keys.D9

            65,     Keys.A
            66,     Keys.B
            67,     Keys.C
            68,     Keys.D
            69,     Keys.E
            70,     Keys.F
            71,     Keys.G
            72,     Keys.H
            73,     Keys.I
            74,     Keys.J
            75,     Keys.K
            76,     Keys.L
            77,     Keys.M
            78,     Keys.N
            79,     Keys.O
            80,     Keys.P
            81,     Keys.Q
            82,     Keys.R
            83,     Keys.S
            84,     Keys.T
            85,     Keys.U
            86,     Keys.V
            87,     Keys.W
            88,     Keys.X
            89,     Keys.Y
            90,     Keys.Z

            91,     Keys.LWin
            92,     Keys.RWin
            93,     Keys.Select

            96,     Keys.NumPad0 
            97,     Keys.NumPad1
            98,     Keys.NumPad2 
            99,     Keys.NumPad3 
            100,    Keys.NumPad4 
            101,    Keys.NumPad5 
            102,    Keys.NumPad6 
            103,    Keys.NumPad7 
            104,    Keys.NumPad8 
            105,    Keys.NumPad9 
            106,    Keys.Multiply
            107,    Keys.Add
            109,    Keys.Subtract
            110,    Keys.Decimal
            111,    Keys.Divide

            112,    Keys.F1
            113,    Keys.F2
            114,    Keys.F3
            115,    Keys.F4
            116,    Keys.F5
            117,    Keys.F6
            118,    Keys.F7
            119,    Keys.F8
            120,    Keys.F9
            121,    Keys.F10
            122,    Keys.F11
            123,    Keys.F12
            133,    Keys.LeftCtrl

            144,    Keys.NumLock
            145,    Keys.Scroll
            186,    Keys.OemSemicolon
            187,    Keys.OemPlus
            188,    Keys.OemComma
            189,    Keys.OemMinus
            190,    Keys.OemPeriod
            191,    Keys.OemQuestion
            192,    Keys.OemTilde

            219,    Keys.OemOpenBrackets
            220,    Keys.OemBackslash
            221,    Keys.OemCloseBrackets
            222,    Keys.OemQuotes
            226,    Keys.OemPipe

        ]

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MouseButtons =
    open Aardvark.Application

    let tryOfButton =
        LookupTable.lookupTable' [
            0, MouseButtons.Left
            1, MouseButtons.Middle
            2, MouseButtons.Right
        ]

    let ofButton (b : int) =
        match tryOfButton b with
        | Some b -> b
        | None -> MouseButtons.None

type private AKey = Aardvark.Application.Keys

type NodeKeyboard(node : HTMLElement) as this =
    inherit Aardvark.Application.EventKeyboard()

    static let swallowKeys =
        HashSet.ofList [
            AKey.Tab; AKey.LeftShift; AKey.RightShift
            AKey.Up; AKey.Down; AKey.Left; AKey.Right
            AKey.Return; AKey.Enter; AKey.LWin; AKey.RWin

            AKey.F1; AKey.F2; AKey.F3; AKey.F4; AKey.F5; AKey.F6
            AKey.F7; AKey.F8; AKey.F9; AKey.F10
        ]

    static let realCode (l : KeyboardLocation) (code : AKey) =
        match l with
        | KeyboardLocation.Right ->
            match code with
            | AKey.LeftAlt -> AKey.RightAlt
            | AKey.LeftCtrl -> AKey.RightCtrl
            | AKey.LeftShift -> AKey.RightShift
            | AKey.LWin -> AKey.RWin
            | _ -> code

        | KeyboardLocation.NumPad ->
            match code with
            | AKey.Return -> AKey.Enter
            | _ -> code

        | _ ->
            code

    let focusSub =
        node.TabIndex <- 0
        node.SubscribeEventListener("mousedown", true, fun _ ->
            node.Focus()
        )
        

    let subscriptions =
        [
            node.SubscribeEventListener("keydown", true, fun e ->
                let e = KeyboardEvent e
                let code = e.KeyCode
                match Keys.tryOfKeyCode code with
                | Some k ->
                    let k = realCode e.Location k
                    this.KeyDown k
                    if this.ClaimsKeyEvents && swallowKeys.Contains k then 
                        e.StopImmediatePropagation()
                        e.PreventDefault()
                        
                | None -> 
                    Log.warn "bad key: %d" code
            )

            node.SubscribeEventListener("keyup", true, fun e ->
                let e = KeyboardEvent e
                let code = e.KeyCode
                match Keys.tryOfKeyCode code with
                | Some k ->
                    let k = realCode e.Location k
                    this.KeyUp k
                    if this.ClaimsKeyEvents && swallowKeys.Contains k then 
                        e.StopImmediatePropagation()
                        e.PreventDefault()
                | None -> 
                    Log.warn "bad key: %d" code
            )

            //JS.Window.Document.SubscribeEventListener("keypress", fun e ->
            //    let e = KeyboardEvent e
            //    if e.Target.Reference = node.Reference then
            //        for c in e.Key do this.KeyPress c
            //        if this.ClaimsKeyEvents then
            //            e.PreventDefault()
            //            e.StopImmediatePropagation()
                    
                
            //)
            node.SubscribeEventListener("keypress", true, fun e ->
                let e = KeyboardEvent e
                for c in e.Key do this.KeyPress c
                if this.ClaimsKeyEvents then
                    e.PreventDefault()
                    e.StopImmediatePropagation()
            )


        ]



    member x.Dispose() =
        focusSub.Dispose()
        for s in subscriptions do s.Dispose()

    interface IDisposable with
        member x.Dispose() = x.Dispose()

type NodeMouse(node : HTMLElement) as this =
    inherit Aardvark.Application.EventMouse(false)

    let getBounds() =
        let r = node.Invoke<JsObj>("getBoundingClientRect", [||])
        Box2i.FromMinAndSize(
            V2i(r.GetProperty<float>("left"), r.GetProperty<float>("top")),
            V2i(r.GetProperty<float>("width"), r.GetProperty<float>("height"))
        )

    let getPosition (e : MouseEvent) =
        let pos = V2i(int e.ClientX, int e.ClientY)
        PixelPosition(pos, getBounds())
        

    let subscriptions =
        [
            node.OnPointerDown.Subscribe(fun e ->
                match e.PointerType with
                | PointerType.Mouse ->
                    let pp = getPosition e
                    this.Down(pp, MouseButtons.ofButton (int e.Button))
                    node.SetPointerCapture e.PointerId
                | _ ->
                    ()
            )

            node.OnPointerUp.Subscribe(fun e ->
                match e.PointerType with
                | PointerType.Mouse ->
                    let pp = getPosition e
                    this.Up(pp, MouseButtons.ofButton (int e.Button))
                    node.ReleasePointerCapture e.PointerId
                | _ ->
                    ()
            )

            node.OnPointerMove.Subscribe(fun e ->
                match e.PointerType with
                | PointerType.Mouse ->
                    let pp = getPosition e
                    this.Move(pp)
                | _ ->
                    ()
                
            )

            node.OnClick.Subscribe(fun e ->
                let pp = getPosition e
                match MouseButtons.tryOfButton (int e.Button) with
                | Some button ->
                    e.PreventDefault()
                    this.Click(pp, button)
                | None ->
                    ()
            )

            node.OnAuxClick.Subscribe(fun e ->
                let pp = getPosition e
                match MouseButtons.tryOfButton (int e.Button) with
                | Some button ->
                    e.PreventDefault()
                    this.Click(pp, button)
                | None ->
                    ()
                
            )

            node.OnContextMenu.Subscribe(fun e ->
                e.PreventDefault()
            )

            node.OnWheel.Subscribe(fun e ->
                let pp = getPosition e
                e.PreventDefault()

                let delta =
                    match e.DeltaMode with
                    | WheelDeltaMode.Line -> e.DeltaY * 16.0
                    | WheelDeltaMode.Page -> e.DeltaY * 1024.0
                    | _ -> e.DeltaY

                this.Scroll(pp, -delta)
                
            )
        ]

    member x.Dispose() =
        for s in subscriptions do s.Dispose()

    interface IDisposable with
        member x.Dispose() = x.Dispose()

