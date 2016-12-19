module MyWinForms.TopDialog

open System
open System.Text
open System.Drawing
open System.Windows.Forms





type Placment = 
    | Center
    | RightBottom 
    | RightTop

[<AutoOpen>]
module private Helpers = 

    let (<==) (ownr:Control) chld = 
        ownr.Controls.Add chld


type TopmostBar(parent) = 
    let mutable btmCtrl : Control  option = None
    let mutable topCtrl : Control  option = None
    let mutable width = 300
    let mutable textForeColor = None    
    let mutable placement = Center     
    let mutable btnAcpt = None
    let mutable btnCncl = None
    let mutable isOpened = false
    let mutable canUpdate = false    
    let mutable text = ""
    let mutable title = "" 
    let mutable minTextHeight = 0

    let p = new Panel(  Visible = false, Parent = parent,
                        BorderStyle = BorderStyle.FixedSingle, 
                        Font = new Font("Consolas", 12.f ) )
    do
        p.Visible <- false
    let textHeight14 = 
        let sz = TextRenderer.MeasureText("X", p.Font)
        sz.Height
    let textBockTitle = 
        new Label(Parent = p, TextAlign = ContentAlignment.MiddleLeft,
                  ForeColor = Color.White, BackColor = Color.Navy)
    let textPanel = new Panel(Parent = p, Location = Point(5,5), Width = p.Width, AutoScroll = true)
    let textBlock = new Label(Parent = textPanel)
    let buttonAccept = new Button( Parent = p, Height = textHeight14 + 10, FlatStyle = FlatStyle.Flat )    
    let buttonCancel = new Button( Parent = p, Height = textHeight14 + 10, FlatStyle = FlatStyle.Flat )

    let isVisible() =  p.PerformThreadSafeAction <| fun () -> p.Visible

    let validateButtons() = 
        match btnAcpt, btnCncl with
        | Some buttonAcceptText, None ->
            let sz = TextRenderer.MeasureText(buttonAcceptText, p.Font)
            buttonAccept.Text <- buttonAcceptText 
            buttonAccept.Width <- sz.Width + 10
            buttonAccept.Left <- width / 2 - sz.Width / 2             
        | Some buttonAcceptText, Some buttonCancelText ->
            let sz =
                [buttonAcceptText; buttonCancelText]
                |> List.map( fun s -> TextRenderer.MeasureText(s, p.Font))
                |> List.maxBy( fun x -> width )
            let w = sz.Width
            let h = sz.Height

            buttonAccept.Text <- buttonAcceptText 
            buttonAccept.Width <- w
            buttonAccept.Left <- width / 4 - w / 2
            
            buttonCancel.Text <- buttonCancelText 
            buttonCancel.Width <- w
            buttonCancel.Left <- 3 * width / 4 - w / 2            
        | _ -> ()

    let validateLocation() = 
        p.Location <- 
            match placement with
            | Center -> 
                new Point ( p.Parent.ClientSize.Width / 2 - p.Width / 2, 
                            p.Parent.ClientSize.Height / 2 - p.Height / 2 )
            | RightBottom ->
                new Point ( p.Parent.ClientSize.Width - p.Size.Width - p.Margin.Right, 
                            p.Parent.ClientSize.Height - p.Size.Height - p.Margin.Bottom )
            | RightTop ->
                new Point ( p.Parent.ClientSize.Width - p.Size.Width - p.Margin.Right, 0 )

    let validateContent () = 
        p.Width <- width

        textBockTitle.Width <- width
        textBockTitle.Text <- title
        
        textBockTitle.Height <-
            let sz = TextRenderer.MeasureText(title, p.Font, Size(width-3, Int32.MaxValue), TextFormatFlags.WordBreak ) 
            sz.Height + 3

        let mutable y = textBockTitle.Bottom + 5

        match topCtrl with
        | None -> ()
        | Some topCtrl ->
            topCtrl.Parent <- p            
            topCtrl.Top <- y
            y <- topCtrl.Bottom + 5
            topCtrl.Width <- width


        textBlock.Text <- text
        textBlock.Height <- 
            let textHeight = 
                let sz = TextRenderer.MeasureText(text, p.Font, Size(textBlock.Width, Int32.MaxValue), TextFormatFlags.WordBreak)
                max sz.Height 5
            max minTextHeight textHeight

        if textBlock.Height > parent.ClientSize.Height - 100 then
            textPanel.Height <- parent.ClientSize.Height - 150
            textBlock.Width <- width - 70
            textPanel.Width <- width - 10
        else
            textPanel.Height <- textBlock.Height
            textBlock.Width <- width 
            textPanel.Width <- width 


        textPanel.Top <- y
        textBlock.ForeColor <- 
            match textForeColor with 
            | Some textForeColor -> textForeColor
            | _ -> textPanel.ForeColor
        y <- textPanel.Bottom + 5
        
        match btmCtrl with
        | None -> ()
        | Some btmCtrl ->
            btmCtrl.Parent <- p
            btmCtrl.Top <- y
            y <- btmCtrl.Bottom + 5
            btmCtrl.Width <- width

        buttonAccept.Visible <- btnAcpt.IsSome
        buttonCancel.Visible <- btnCncl.IsSome
        
        if btnAcpt.IsSome then            
            buttonAccept.Top <- y + 5 
            y <- buttonAccept.Bottom + 5
        if btnCncl.IsSome then            
            buttonCancel.Top <- buttonAccept.Top
        validateButtons()

        p.Height <- y
        p.Visible <- true

        p.BringToFront()
        p.Parent.PerformLayout()
        validateLocation()

    let parSizeChangedHandler = new EventHandler(fun _ _ -> 
        if p.Visible then
            validateContent() )

    do
        p.VisibleChanged.Add <| fun _ ->
            isOpened <- p.Visible
            canUpdate <- p.Visible

        buttonAccept.Click.Add <| fun _ -> 
            p.Visible <- false
            isOpened <- false

        buttonCancel.Click.Add <| fun _ -> 
            isOpened <- false
            p.Visible <- false

        parent.SizeChanged.AddHandler parSizeChangedHandler
        parent.Resize.AddHandler parSizeChangedHandler

    let update() = 
        if canUpdate then
            p.PerformThreadSafeAction validateContent

    member x.Margin
        with get() = p.Margin
        and set v = 
            if v<>p.Margin then
                p.Margin <- v
                update()
    
    member x.Placement
        with get() = placement
        and set v = 
            if v<>placement then
                placement <- v
                update()

    member x.MinTextHeight
        with get() = minTextHeight
        and set v = 
            if v<>minTextHeight then
                minTextHeight <- v
                update()

    member x.TextForeColor
        with get() = textForeColor
        and set v = 
            if v<>textForeColor then
                textForeColor <- v
                update()

    member x.TopControl
        with get() = topCtrl
        and set v = 
            if v<>topCtrl then
                match v, topCtrl with
                | None, Some ctrl -> ctrl.Parent <- null
                | _ -> ()
                topCtrl <- v
                update()

    member x.BottomControl
        with get() = btmCtrl
        and set v = 
            if v<>btmCtrl then
                match v, btmCtrl with
                | None, Some ctrl -> ctrl.Parent <- null
                | _ -> ()
                btmCtrl <- v
                update()

    member x.Text
        with get() = text
        and set v = 
            if v<>text then
                text <- v
                update()

    member x.Title
        with get() = title
        and set v = 
            if v<>title then
                title <- v
                update()

    member x.Width
        with get() = width
        and set v = 
            if v<>width then
                width <- v
                update()

    member x.Visible 
        with get() = isOpened
        and set v = 
            if v<>isOpened then
                isOpened <- v
                p.PerformThreadSafeAction <| fun () ->
                    p.Visible <- v
                    if p.Visible then
                        validateContent()

    member x.ButtonAccept 
        with get() =  btnAcpt 
        and set v = 
            if v<> btnAcpt then
                btnAcpt <- v
                update()

    member x.ButtonCancel 
        with get() =  btnCncl 
        and set v = 
            if v<> btnCncl then
                btnCncl <- v                
                update()
    
    member x.Font 
        with get() =  p.Font
        and set v = 
            p.PerformThreadSafeAction <| fun () ->
                p.Font <- v
                if isOpened then
                    validateContent()

    member x.DoUpdate f = 
        if isOpened then
            canUpdate <- false
            p.PerformThreadSafeAction <| fun () ->
                f()
                validateContent()
            canUpdate <- true
        else
            f()
            x.Visible <- true