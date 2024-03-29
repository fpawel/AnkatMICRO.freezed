﻿module Ankat.View.Menus

open System
open System.Windows.Forms
open System.Drawing
open System.Collections.Generic
open System.ComponentModel
open System.ComponentModel.DataAnnotations

open MyWinForms.TopDialog
open MainWindow
open Ankat.ViewModel.Operations
open Thread2
open Ankat
open Ankat.PartyWorks
open Ankat.View
open Pneumo

[<AutoOpen>]
module private Helpers =
    type P = Ankat.ViewModel.Product     
    let party = AppContent.party
    let popupDialog = MyWinForms.PopupDialog.create
    type Dlg = MyWinForms.PopupDialog.Options
    let none _ = None
    let form = MainWindow.form

    let getSelectedProducts() = 
        seq{ for x in gridProducts.SelectedCells -> x.RowIndex, x.OwningRow }
        |> Map.ofSeq
        |> Map.toList
        |> List.map( fun (_,x) -> x.DataBoundItem :?> P )

    let simpleMenu = MyWinForms.Utils.buttonsPopupMenu (new Font("Consolas", 12.f)) ( Some 300 ) 

    let popupConfig = MyWinForms.Utils.popupConfig


let popupNumberDialog<'a> 
    prompt title tryParse work 
    (btn : Button) 
    (parentPopup : MyWinForms.Popup) =
    let tb = new TextBox(Width = 290, Text = "")                    
    let dialog,validate  = 
        popupDialog 
            { Dlg.def() with 
                Text = Some prompt
                ButtonAcceptText = "Применить" 
                Title = title
                Width = 300
                Content = tb }
            ( fun () -> 
                tryParse tb.Text)
            ( fun (value : 'a) ->  
                parentPopup.Hide()
                work value ) 
    tb.TextChanged.Add <| fun _ -> validate()                        
    dialog.Show btn

let modbusToolsPopup = 
    [   yield "Установка адреса", (fun _ _ -> setAddr())
        for i = 0 to 2 do 
            yield sprintf "Установка режима %d" i, (fun _ _ -> setWorkMode i)
        yield!
            Command.valuesList
            |> List.filter( (<>) CmdSetAddr ) 
            |> List.map( fun cmd -> 
                (sprintf "MDBUS: %s" cmd.What), 
                    popupNumberDialog 
                        (sprintf "Введите значение аргумента команды %A" cmd.What)
                        cmd.What
                        String.tryParseDecimal
                        (fun value -> sendCommand (cmd,value) ) ) ]
    |> simpleMenu

let pneumoToolsPopup = 
    
    [   yield! Pneumo.Clapan.valuesList |> List.map ( fun gas -> 
            gas.What, fun _ _   -> 
                PartyWorks.Pneumoblock.switch gas )
        yield "Выкл.", fun _ _ -> 
            PartyWorks.Pneumoblock.close()  ]
    |> simpleMenu
    
let termoToolsPopup = 

    let setpoint = 
        popupNumberDialog 
            "Введите значение уставки термокамеры"
            "Задать уставку термокамеры"
            String.tryParseDecimal
            PartyWorks.TermoChamber.setSetpoint
    [   yield "Старт", fun _ _ -> PartyWorks.TermoChamber.start()
        yield "Стоп", fun _ _ -> PartyWorks.TermoChamber.stop()
        yield "Уставка", setpoint   ]
    |> simpleMenu
    

let private initButtons1 = 
    let buttons1placeholder = 
        new Panel
            (   Parent = TabsheetParty.BottomTab, Dock = DockStyle.Top, Height = 89 )

    let imgbtn1 left top key tooltip f = 
        let x = 
            new Button( Parent = buttons1placeholder, Left = left, Top = top,
                        ImageKey = key, Width = 40, Height = 40,
                        FlatStyle = FlatStyle.Flat,
                        ImageList = Widgets.Icons.instance.imageList1)
        MainWindow.setTooltip x tooltip
        x.Click.Add <| fun _ ->  
            f x
        x

    let imgbtn left top key tooltip f = imgbtn1 left top key tooltip f 

    let btnOpenParty = imgbtn 3 3 "open" "Открыть ранее сохранённую партию" OpenPartyDialog.showDialog
    let btnNewParty = imgbtn 46 3 "add" "Создать новую партию" PartyProductsDialogs.createNewParty

    let btnAddProd = imgbtn 3 46 "additem" "Добавить в партию новые приборы" PartyProductsDialogs.addProducts
    let btnDelProd = 
        let b = imgbtn1 46 46 "removeitem" "Удалить выбранные приборы из партии" PartyProductsDialogs.deleteProducts
        b.Visible <- false
        let g = gridProducts
        g.SelectionChanged.Add <| fun _ ->
            b.Visible <- g.SelectedCells.Count > 0 
        b

    Thread2.IsRunningChangedEvent.addHandler <| fun (_,isRunning) ->
        [btnOpenParty; btnNewParty; btnAddProd; btnDelProd ]
        |> Seq.iter(fun b -> b.Enabled <- not isRunning )

    let _ = imgbtn 89 3 "todo" "Выбрать опрашиваемые параметры" ( fun b ->
        let popup = 
            MyWinForms.Utils.popupConfig 
                "Опрашиваемые параметры" 
                (ViewModel.SelectPhysVars()) 
                PropertySort.Alphabetical
        popup.Closed.Add( fun _ ->
           View.Products.updatePhysVarsGridColsVisibility()  )
        popup.Show b )    

    fun () -> ()

open Ankat.View.TopBar

let initialize =
    let buttonRun = new Button( Parent = TopBar.thread1ButtonsBar, Dock = DockStyle.Left, AutoSize = true,
                                ImageKey = "run",
                                Text = (sprintf "%A" Thread2.scenary.Value.FullName),
                                ImageAlign = ContentAlignment.MiddleLeft,
                                TextImageRelation = TextImageRelation.ImageBeforeText,
                                TextAlign = ContentAlignment.MiddleCenter,
                                FlatStyle = FlatStyle.Flat,
                                ImageList = Widgets.Icons.instance.imageList1)
    TopBar.thread1ButtonsBar.Controls.Add <| new Panel(Dock = DockStyle.Left, Width = 3)
    MainWindow.setTooltip buttonRun ("Выполнить " + buttonRun.Text)
    buttonRun.Click.Add <| fun _ ->  
        Thread2.run NeedStopHardware Thread2.scenary.Value
    Thread2.scenary.AddChanged <| fun (_,x) ->
        buttonRun.Text <- sprintf "%A" x.FullName
        MainWindow.setTooltip buttonRun ("Выполнить " + buttonRun.Text)
        buttonRun.AutoSize <- false
        buttonRun.AutoSize <- true

    let (<==) (text,tooltip) f = 
        let b = new Button( Parent = TopBar.thread1ButtonsBar, Dock = DockStyle.Left, 
                            FlatStyle = FlatStyle.Flat,
                            Text = text, AutoSize = true )
        b.Click.Add <| fun _ ->  
            f b    
        MainWindow.setTooltip b tooltip
        TopBar.thread1ButtonsBar.Controls.Add <| new Panel(Dock = DockStyle.Left, Width = 3)

    let imgBtn (key,tooltip) f = 
        let b = new Button( Parent = TopBar.thread1ButtonsBar, Dock = DockStyle.Left, 
                            ImageList = Widgets.Icons.instance.imageList1,
                            FlatStyle = FlatStyle.Flat, ImageKey = key,
                            Width = 40, Height = 40,
                            AutoSize = true )
        b.Click.Add <| fun _ ->  
            f b    
        MainWindow.setTooltip b tooltip
        TopBar.thread1ButtonsBar.Controls.Add <| new Panel(Dock = DockStyle.Left, Width = 3)
        
        
    imgBtn ( "loop","Опрос выбранных параметров приборов партии" ) <| fun _ ->
        runInterrogate()
    
    imgBtn ("network", "Управление приборами по Modbus") <| fun x ->
        modbusToolsPopup.Show x

    imgBtn ("pneumo", "Управление пневмоблоком") <| fun x ->
        pneumoToolsPopup.Show x   

    imgBtn  ("termochamber", "Управление термокамерой") <| fun x ->
        termoToolsPopup.Show x   

    Thread2.addKeepRunningHandler <| fun (_,isKeepRunning) -> 
        form.PerformThreadSafeAction <| fun () ->
            modbusToolsPopup.Content.Enabled <- not isKeepRunning
            pneumoToolsPopup.Content.Enabled <- not isKeepRunning
            termoToolsPopup.Content.Enabled <- not isKeepRunning

    do
        let x = 
            new Button( Parent = TopBar.thread1ButtonsBar, Dock = DockStyle.Left, AutoSize = true,
                        ImageKey = "script", Width = 40, Height = 40,
                        FlatStyle = FlatStyle.Flat,
                        ImageList = Widgets.Icons.instance.imageList1)
        MainWindow.setTooltip x "Выбрать сценарий настройки"
        x.Click.Add <| fun _ ->  
            SelectScenaryDialog.showSelectScenaryDialog x
        TopBar.thread1ButtonsBar.Controls.Add <| new Panel(Dock = DockStyle.Left, Width = 3)

    initButtons1()
    
    let buttonSettings = 
        new Button( Parent = right, Height = 40, Width = 40, Visible = true,
                    ImageList = Widgets.Icons.instance.imageList1,
                    FlatStyle = FlatStyle.Flat,
                    Dock = DockStyle.Right, ImageKey = "settings")
    right.Controls.Add <| new Panel(Dock = DockStyle.Right, Width = 3)
    setTooltip buttonSettings "Параметры приложения" 

    buttonSettings.Click.Add <| fun _ -> 
        let popup = 
            MyWinForms.Utils.popupConfig 
                "Параметры" 
                (AppConfigView())
                PropertySort.NoSort
        popup.Font <- form.Font        
        popup.Show(buttonSettings)
       
    Thread2.scenary.Set (production())

    fun () -> ()