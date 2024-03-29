﻿module Ankat.View.Scenary

open System
open System.Windows.Forms
open System.Drawing

open MainWindow

let updateGridViewBinding() = 
    MyWinForms.Utils.GridView.updateBinding gridScenary


[<AutoOpen>]
module private Helpers =

    type C = DataGridViewColumn
    type CheckBoxColumn = MyWinForms.GridViewCheckBoxColumn
    type TextColumn = DataGridViewTextBoxColumn
    let party = Ankat.AppContent.party
    let (~%%) x = x :> C
    let popupDialog = MyWinForms.PopupDialog.create
    type Dlg = MyWinForms.PopupDialog.Options

module private Popup =

    let clearLoggigng() =             
        fst <| popupDialog
            { Dlg.def() with 
                Dlg.Text = Some  "Пожалуйста, подтвердите необходимость очистки журнала выполнения сценария." 
                Dlg.ButtonAcceptText = "Очистить" 
                Dlg.Title = "Очистка журнала"
                Width = 300 }
            ( fun () -> Some () )
            ( fun () ->
                party.Journal <- Map.empty 
                updateGridViewBinding() )

    let delayTime() = 
        let d = Ankat.ViewModel.DelaysHelperViewModel ()
        let g = new PropertyGrid(SelectedObject = d, Width = 400,
                                    Font = new Font("Consolas", 12.f),
                                    ToolbarVisible = false, Height = 500,
                                    PropertySort = PropertySort.Alphabetical)
        let popup = new MyWinForms.Popup(g)    
        popup


module private SelectedOperation = 

    let get() = 
        let xs = gridScenary.SelectedCells
        if xs.Count=0 then None else
        let x = xs.[0]
        try
            x.OwningRow.DataBoundItem 
            :?> Ankat.ViewModel.Operations.RunOperationInfo 
            |> Some
        with e ->
            Logging.debug "error Ankat.View.Scenary.SelectedOperation.get! %A" e
            None

    let showLoggigng() = 
        get()
        |> Option.iter(fun x -> 
            LoggingHtml.set webbJournal x.Operation.FullName x.Operation.RunInfo.LoggingRecords )

    

    
let initialize =
    gridScenary.DataSource <- Thread2.operations 
    gridScenary.SelectionChanged.Add <| fun _ ->
        SelectedOperation.showLoggigng()

    let rec h = EventHandler( fun _ _ -> 
        if gridScenary.RowCount > 0 then
            gridScenary.CurrentCell <- null
            gridScenary.CurrentCell <- gridScenary.Rows.[0].Cells.[0]
        form.Activated.RemoveHandler h )
    form.Activated.AddHandler h

    gridScenary.DataBindingComplete.Add <| fun _ ->
        if gridScenary.RowCount > 0 then
            gridScenary.CurrentCell <- gridScenary.Rows.[0].Cells.[0]

    party.OnAddLogging.Add <| fun (operation, level,text) ->
        SelectedOperation.get()
        |> Option.iter( fun op -> 
            
            let xs = Ankat.ViewModel.Operations.Operation.tree op.Operation
            if xs |> List.exists( fun o -> o.FullName = operation) then
                webbJournal.PerformThreadSafeAction <| fun () -> 
                    LoggingHtml.addRecord webbJournal level text )

    Thread2.PerfomOperationEvent.Add <| function
        | operation,true ->
            match SelectedOperation.get() with            
            | Some op when op.Operation.FullName = operation.FullName -> 
                webbJournal.PerformThreadSafeAction <| fun () ->
                    
                    LoggingHtml.set webbJournal operation.FullName []
            | _ -> ()
        | _ -> ()

    gridScenary.Columns.AddRange            
        [|  %% new TextColumn(DataPropertyName = "Name", HeaderText = "Операция")
            %% new TextColumn(DataPropertyName = "Delaytime", HeaderText = "Задержка") 
            %% new TextColumn(DataPropertyName = "Status", HeaderText = "Статус") |]
    gridScenary.CellFormatting.Add <| fun e ->
        let row = gridScenary.Rows.[e.RowIndex]
        let col = gridScenary.Columns.[e.ColumnIndex]        
        let cell = row.Cells.[e.ColumnIndex]
        let i = row.DataBoundItem :?> Ankat.ViewModel.Operations.RunOperationInfo

        let x = e.CellStyle
            
        if i.WasPerformed then
            x.SelectionForeColor <- SystemColors.HighlightText
            x.SelectionBackColor <- SystemColors.Highlight
            x.ForeColor <- Color.Black
            x.BackColor <- Color.Bisque

        if i.HasErrors then                
            x.SelectionForeColor <- Color.Yellow
            x.SelectionBackColor <- Color.MidnightBlue 
            x.ForeColor <- Color.Red  
            x.BackColor <- Color.LightGray 

        if i.IsPerforming then 
            x.SelectionForeColor <- SystemColors.HighlightText
            x.SelectionBackColor <- SystemColors.Highlight
            x.ForeColor <- Color.Black
            x.BackColor <- Color.Aquamarine
        e.FormattingApplied <- true

    let _ = new Panel( Parent = TabsheetScenary.BottomTab, Dock = DockStyle.Top, Height = 3)
    let b = new Button( Parent = TabsheetScenary.BottomTab, Dock = DockStyle.Top, 
                        Text = "Очистить журнал", Height = 45, FlatStyle = FlatStyle.Flat,
                        TextAlign = ContentAlignment.MiddleLeft)
    b.Click.AddHandler(fun _ _ ->
        Popup.clearLoggigng().Show b )

    let _ = new Panel( Parent = TabsheetScenary.BottomTab, Dock = DockStyle.Top, Height = 3)
    let b = new Button( Parent = TabsheetScenary.BottomTab, Dock = DockStyle.Top, 
                        Text = "Длит. задержек", Height = 45, FlatStyle = FlatStyle.Flat,
                        TextAlign = ContentAlignment.MiddleLeft)
    b.Click.AddHandler(fun _ _ ->
        Popup.delayTime().Show b )

    fun () -> ()