﻿module Ankat.View.TabPages

open System
open System.Windows.Forms
open System.Drawing
open System.Collections.Generic

open MyWinForms.Utils
open WinFormsControlUtils

open MainWindow
open Ankat
open Ankat.View.Products

type private P = Ankat.ViewModel.Product
type private VE = Ankat.Alchemy.ValueError


module TabsheetVars =
    type Page = 
        {   PhysVar : PhysVar
            ProductionPoint : ProductionPoint
            TermoPt : TermoPt
            PressPt : PressPt} 
        
    let mutable private page = { 
        PhysVar = Sens1.Conc
        ProductionPoint = Correction <| CorrectionLinScale Sens1
        TermoPt = TermoNorm
        PressPt = PressNorm}

    let private addcol dataPropertyName headerText = 
        new DataGridViewTextBoxColumn( DataPropertyName = dataPropertyName, HeaderText = headerText)
        |> gridProducts.Columns.AddColumn

    let private termoLeter = function
        | TermoNorm -> ""
        | TermoLow -> "-"
        | TermoHigh -> "+"

    let update () = 
        
        sprintf "%s, %s, %s, %s" page.ProductionPoint.What2 page.PhysVar.What 
            page.TermoPt.Dscr page.PressPt.What
        |> setActivePageTitle 
        gridProducts.Columns.``remove all columns but`` Columns.main
        let f = page.ProductionPoint
        
        match f with
        | Correction (CorrectionLinScale _ )
        | TestConcErrors _ ->
            for gas in f.Gases do
                let var = page.ProductionPoint, page.PhysVar, gas, page.TermoPt, page.PressPt
                addcol (Vars.property var) gas.What
        | Correction (CorrectionTermoScale _ | CorrectionTermoPress) -> 
            for t in [TermoNorm; TermoLow; TermoHigh] do
                for physvar in f.PhysVars do
                    let var = page.ProductionPoint, physvar, f.Gases.Head, t, PressNorm
                    addcol (Vars.property var) (physvar.What + termoLeter t)
        | Correction CorrectionPressSens  -> 
            for p in f.Pressures do
                for physvar in f.PhysVars do                
                    let var = page.ProductionPoint, physvar, ScaleBeg, TermoNorm, p
                    addcol (Vars.property var) (sprintf "%s.%s" physvar.What p.What)
        
    let private addp () =         
        let p = new Panel(Parent = TabsheetVars.BottomTab, Dock = DockStyle.Top)
        let _ = new Panel(Parent = TabsheetVars.BottomTab, Dock = DockStyle.Top, Height = 10)
        p

    

    module Press =
        let get,set, setVisibility = 
            radioButtons (addp ()) PressPt.values PressPt.what <| fun x -> 
                page <- {page with PressPt = x }
                update()

    module Termo =
        let get,set, setVisibility = 
            radioButtons (addp ()) TermoPt.values TermoPt.what <| fun x -> 
                page <- {page with TermoPt = x }
                update()
    
    module PhysVar =
        let get,set, setVisibility = 
            let vars =
                Vars.vars |> List.map(fun (_,x,_,_,_) -> x)
                |> Set.ofList
                |> Set.toList

            radioButtons (addp ()) vars PhysVar.what <| fun x -> 
                page <- {page with PhysVar = x }
                update()

    module Feat =
        let get,set, setVisibility = 
            let f set setVis = 
                function
                    | [v] -> v,[]
                    | x::_ as xs -> x,xs
                >> fun (x,xs) -> 
                    set x
                    setVis xs

            radioButtons (addp ()) ProductionPoint.values ProductionPoint.what2 <| fun x ->
                f PhysVar.set PhysVar.setVisibility x.PhysVars 
                f Termo.set Termo.setVisibility x.Temperatures 
                f Press.set Press.setVisibility x.Pressures 
                page <- { page with ProductionPoint = x }


                match x with
                | Correction (CorrectionTermoScale _  | CorrectionTermoPress) ->
                    PhysVar.setVisibility []
                    Termo.setVisibility []                
                | Correction CorrectionPressSens  -> 
                    PhysVar.setVisibility []
                    Press.setVisibility []
                | _ -> ()

                update()

    

module TabsheetChart = 
    
    let update() =
        setActivePageTitle <| sprintf "График. %s" Chart.physVar.Dscr 
        AppContent.updateChartSeriesList ()
        let m =Chart.axisScalingViewModel
        m.MaxDateTime <- None
        m.MinDateTime <- None
        m.MinY <- None
        m.MaxY <- None
    
    module PhysVar =
        let get,set,_ = 
            let panelSelectVar = new Panel(Parent = TabsheetChart.BottomTab, Dock = DockStyle.Top)
            let _ = new Panel(Parent = TabsheetChart.BottomTab, Dock = DockStyle.Top, Height = 10)
        
            radioButtons panelSelectVar PhysVar.values PhysVar.what <| fun x -> 
                Chart.physVar <- x
                update()

module TabsheetErrors =
    let private pts = 
        [   SScalePt.Beg1
            SScalePt.Mid11
            SScalePt.End1
            SScalePt.Beg2
            SScalePt.Mid2
            SScalePt.End2 ]
    type K = 
        | Main | Termo 
        member x.What = K.what x
        static member what = function
            | Main  -> "Основная"
            | Termo -> "Температурная"

        static member props = function
            | Main  -> 
                [   for gas in pts do
                        yield gas.What, "Основная погрешность", Property.concError gas ]
            | Termo -> 
                [   for gas in pts do
                        yield gas.What + "-", "Погрешность на пониженной температуре", Property.termoError (gas,TermoLow)
                    for gas in pts do
                        yield gas.What + "+", "Погрешность на повышенной температуре", Property.termoError (gas,TermoHigh)
                     ]
            

    
    
    let getProductOfRow (g:DataGridView) (e:DataGridViewCellFormattingEventArgs) =
        g.Rows.[e.RowIndex].DataBoundItem :?> P

    type private Fn = { 
        f : P -> VE option
        s : string }
    let private colsFns = 
        let x = Dictionary<int, Fn>()
        gridProducts.ColumnRemoved.Add(fun e -> 
            x.Remove(e.Column.GetHashCode()) |> ignore
            )
        x

         

    let formatCell page (g:DataGridView) (e:DataGridViewCellFormattingEventArgs) s ve =  
        let row = g.Rows.[e.RowIndex]
        let col = g.Columns.[e.ColumnIndex]        
        let cell = row.Cells.[e.ColumnIndex]
        let p = getProductOfRow g e
        let decToStr = Decimal.toStr "0.###"
        let what = sprintf "%s, %s" p.What s
        
        ve |> Option.map( fun (ve : VE) ->  
            
            let foreColor, backColor = if ve.IsError then Color.Red, Color.LightGray else Color.Navy, Color.Azure 
            let toolTip = 
                [|  yield "Снятое значение", decToStr ve.Value                     
                    yield "Номинал", decToStr ve.Nominal
                    yield "Предел погрешности", decToStr ve.Limit  |]
                |> Array.map( fun (p,v) -> sprintf "%s = %s" p v)
                |> fun v -> String.Join("\n", v)                
            ve.Value, foreColor, backColor, toolTip  )
        |> function
        | None -> cell.ToolTipText <- sprintf "%s - нет данных" what
        | Some (value, foreColor, backColor, text) ->
            cell.Style.ForeColor <- foreColor
            cell.Style.BackColor <- backColor
            cell.ToolTipText <- sprintf "%s\n%s" what text
            e.Value <- decToStr value
            e.FormattingApplied <- true
             

    let mutable private page = Main
    let update () = 
        setActivePageTitle (page.What + " погрешность")
        gridProducts.Columns.``remove all columns but`` Columns.main
        for h,s,p in K.props page do
            let col = new DataGridViewTextBoxColumn( DataPropertyName = p,  HeaderText = h)
            colsFns.[col.GetHashCode()] <- 
                {   f = fun product ->
                        let t = typeof<P>
                        let prop = t.GetProperty p
                        prop.GetValue(product,null) :?> VE option
                    s = s }
            gridProducts.Columns.AddColumn col

    
    
    let get,set,_ = 
        let p1 = new Panel(Parent = TabsheetErrors.BottomTab, Dock = DockStyle.Top)
        let _ = new Panel(Parent = TabsheetErrors.BottomTab, Dock = DockStyle.Top, Height = 10)

        gridProducts.CellFormatting.Add <| fun e ->
            let column = gridProducts.Columns.[e.ColumnIndex]
            if column.GetHashCode() = Ankat.View.Products.Columns.interrogate.[0].GetHashCode() then
                let text, fore, back =
                    match e.Value :?> Result<string,string> option with
                    | Some (Ok s) -> s, Color.Black, Color.White
                    | Some (Err s) -> s, Color.Red, Color.LightGray
                    | _ -> "", Color.Black, Color.White
                e.Value <- text
                let row = gridProducts.Rows.[e.RowIndex]
                let cell = row.Cells.[e.ColumnIndex]
                cell.Style.ForeColor <- fore
                cell.Style.BackColor <- back
            else
                let b,f = colsFns.TryGetValue (column.GetHashCode())
                if b then 
                    let p = getProductOfRow gridProducts e
                    formatCell page gridProducts e f.s (f.f p)
            

        radioButtons p1 [Main; Termo] K.what <| fun x -> 
            page <- x
            update()
    

let private onSelect = function
    | TabsheetParty -> 
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Columns.AddColumns <| Products.Columns.sets @ Products.Columns.interrogate 
        gridProducts.Parent <- TabsheetParty.RightTab
    | TabsheetVars ->
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Parent <- TabsheetVars.RightTab
        TabsheetVars.update()
    | TabsheetErrors ->
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Parent <- TabsheetErrors.RightTab
        TabsheetErrors.update()
        
    | TabsheetChart ->
        gridProducts.Parent <- null
        TabsheetChart.update()
    | _ -> ()
        
let getSelected, setSelected,_ =
    gridProducts.Columns.CollectionChanged.Add(fun _ ->
        gridProducts.Columns.SetDisplayIndexByOrder()
        )
    gridProducts.Columns.AddColumns  Products.Columns.main
    gridProducts.Columns.AddColumns  Products.Columns.sets
    
    radioButtons 
        tabButtonsPlaceholder 
        Tabsheet.values
        Tabsheet.title
        (fun tabPage -> 
            setActivePageTitle tabPage.Title
            onSelect tabPage
            tabPage.ShowContent() ) 

module TabChart =
    let update() = 
        if getSelected() = TabsheetChart then
            AppContent.updateChartSeriesList ()