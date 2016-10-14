module Ankat.View.TabPages

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

    [<AutoOpen>]
    module private Helpers =

        type Page = 
            {   PhysVar : PhysVar
                ProductionPoint : ProductionPoint
                TermoPt : TermoPt } 
        
        let mutable page = { 
            PhysVar = Sens1.Conc
            ProductionPoint = Correction <| CorrectionLinScale Sens1
            TermoPt = TermoNorm }

        let addcol dataPropertyName headerText = 
            new DataGridViewTextBoxColumn( DataPropertyName = dataPropertyName, HeaderText = headerText)
            |> gridProducts.Columns.AddColumn

        let termoLeter = function
            | TermoNorm -> ""
            | TermoLow -> "-"
            | TermoHigh -> "+"

        let pressLeter = function
            | PressNorm -> ""
            | PressHigh -> "+"

        let addp () = 
            let p = new Panel(Parent = TabsheetVars.BottomTab, Dock = DockStyle.Top)
            let _ = new Panel(Parent = TabsheetVars.BottomTab, Dock = DockStyle.Top, Height = 10)
            p

    let update () = 
        
        gridProducts.Columns.``remove all columns but`` Columns.main
        let f = page.ProductionPoint
        
        match f with
        | Correction (CorrectionLinScale sens ) ->
            for gas in f.Gases do
                let var = f, sens.Conc, gas, TermoNorm, PressNorm
                addcol (Vars.property var) gas.What
                setActivePageTitle f.What1
        | TestConcErrors _ ->
            for gas in f.Gases do
                let var = page.ProductionPoint, page.PhysVar, gas, page.TermoPt, PressNorm
                addcol (Vars.property var) gas.What
                setActivePageTitle <| sprintf "%s, %s, %s" page.ProductionPoint.What2 page.PhysVar.What page.TermoPt.What
        | Correction (CorrectionTermoScale _ | CorrectionTermoPress) -> 
            for t in [TermoNorm; TermoLow; TermoHigh] do
                for physvar in f.PhysVars do
                    let var = page.ProductionPoint, physvar, f.Gases.Head, t, PressNorm
                    addcol (Vars.property var) (physvar.What + termoLeter t)
            setActivePageTitle page.ProductionPoint.What1
        | Correction CorrectionPressSens  -> 
            for p in f.Pressures do
                for physvar in f.PhysVars do                
                    let var = page.ProductionPoint, physvar, ScaleBeg, TermoNorm, p
                    addcol (Vars.property var) (physvar.What + pressLeter p)
            setActivePageTitle page.ProductionPoint.What1

    module Termo =
        let get,set, setVisibility = 
            radioButtons (addp ()) TermoPt.values TermoPt.what TermoPt.dscr <| fun x -> 
                page <- {page with TermoPt = x }
                update()
    
    module PhysVar =
        let get,set, setVisibility = 
            let vars =
                Vars.vars |> List.map(fun (_,x,_,_,_) -> x)
                |> Set.ofList
                |> Set.toList

            radioButtons (addp ()) vars PhysVar.what PhysVar.dscr <| fun x -> 
                page <- {page with PhysVar = x }
                update()

    module ProductionPoint =
        let get, set, setVisibility = 
            let f x y =
                PhysVar.setVisibility x
                Termo.setVisibility y
                    
            radioButtons (addp ()) ProductionPoint.values ProductionPoint.what2 ProductionPoint.what1 <| fun x ->
                page <- { page with ProductionPoint = x }
                match x with
                | TestConcErrors _ -> 
                    PhysVar.set x.PhysVars.Head 
                    Termo.set x.Temperatures.Head
                    PhysVar.setVisibility x.PhysVars
                    Termo.setVisibility x.Temperatures
                | _ -> 
                    PhysVar.setVisibility []
                    Termo.setVisibility []
                page <- { page with ProductionPoint = x}
                
                update()
                

        let private vis x = 
            AppContent.party.getProductType().Sensor2.IsSome ||
                ProductionPoint.isSens1 x

        let updateVisibility() =             
            let xs = List.filter vis ProductionPoint.values 
            setVisibility xs
            set xs.Head

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
        
            radioButtons panelSelectVar PhysVar.values PhysVar.what PhysVar.dscr <| fun x -> 
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

    

    [<AutoOpen>]
    module private Helpers =

        type Page = 
            {   SensorIndex  : SensorIndex
                TermoPt : TermoPt } 

            
            static member ctx x = 
                match x.TermoPt with
                | TermoNorm ->
                    x.SensorIndex.ScalePts
                    |> List.map (fun gas -> 
                        let n = SScalePt.new' x.SensorIndex gas
                        n.What, Property.concError n )
                | t ->
                    x.SensorIndex.ScalePts
                    |> List.map (fun gas -> 
                        let n = SScalePt.new' x.SensorIndex gas
                        n.What, Property.termoError (n, t) )
                
        let mutable page = { 
            TermoPt = TermoNorm 
            SensorIndex = Sens1 }

        let addp () = 
            let p = new Panel(Parent = TabsheetErrors.BottomTab, Dock = DockStyle.Top)
            let _ = new Panel(Parent = TabsheetErrors.BottomTab, Dock = DockStyle.Top, Height = 10)
            p

        type Fn = P -> VE option
        let colsFns = 
            let x = Dictionary<int, Fn>()
            gridProducts.ColumnRemoved.Add(fun e -> 
                x.Remove(e.Column.GetHashCode()) |> ignore
                )
            x

        let getProductOfRow (g:DataGridView) (e:DataGridViewCellFormattingEventArgs) =
            g.Rows.[e.RowIndex].DataBoundItem :?> P         

        let formatCell (g:DataGridView) (e:DataGridViewCellFormattingEventArgs) ve =  
            let row = g.Rows.[e.RowIndex]
            let col = g.Columns.[e.ColumnIndex]        
            let cell = row.Cells.[e.ColumnIndex]
            let p = getProductOfRow g e
            let decToStr = Decimal.toStr "0.###"
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
            | None -> cell.ToolTipText <- sprintf "%s - нет данных" p.What
            | Some (value, foreColor, backColor, text) ->
                cell.Style.ForeColor <- foreColor
                cell.Style.BackColor <- backColor
                cell.ToolTipText <- sprintf "%s\n%s" p.What text
                e.Value <- decToStr value
                e.FormattingApplied <- true

    let update () =
        setActivePageTitle (sprintf "Погрешность %s, %s"  page.SensorIndex.What page.TermoPt.What)
        gridProducts.Columns.``remove all columns but`` Columns.main
        for h,p in Page.ctx page do
            let col = new DataGridViewTextBoxColumn( DataPropertyName = p,  HeaderText = h)
            colsFns.[col.GetHashCode()] <- fun product ->
                let t = typeof<P>
                let prop = t.GetProperty p
                prop.GetValue(product,null) :?> VE option
            gridProducts.Columns.AddColumn col

    
    

    module Termo =
        let get,set, _ = 
            radioButtons (addp ()) TermoPt.values TermoPt.what TermoPt.dscr <| fun x -> 
                page <- {page with TermoPt = x }
                update()

    module SensorIndex =
        let get,set, _ = 
            gridProducts.CellFormatting.Add <| fun e ->
                let column = gridProducts.Columns.[e.ColumnIndex]
                if obj.ReferenceEquals( column, Ankat.View.Products.Columns.columnConnection) then
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
                        formatCell gridProducts e (f p)

            radioButtons (addp ()) SensorIndex.values SensorIndex.what SensorIndex.what <| fun x -> 
                page <- {page with SensorIndex = x }
                update()
    

let private onSelect = function
    | TabsheetParty -> 
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Columns.AddColumns Products.Columns.interrogate 
        gridProducts.Parent <- TabsheetParty.RightTab
    | TabsheetVars ->
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Parent <- TabsheetVars.RightTab
        TabsheetVars.ProductionPoint.updateVisibility()
        TabsheetVars.update()
    | TabsheetErrors ->
        gridProducts.Columns.``remove all columns but`` Products.Columns.main
        gridProducts.Parent <- TabsheetErrors.RightTab
        TabsheetErrors.Termo.set TermoNorm
        TabsheetErrors.SensorIndex.set Sens1
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
    
    radioButtons 
        tabButtonsPlaceholder 
        Tabsheet.values
        Tabsheet.title
        Tabsheet.descr
        (fun tabPage -> 
            setActivePageTitle tabPage.Title
            onSelect tabPage
            tabPage.ShowContent() ) 

module TabChart =
    let update() = 
        if getSelected() = TabsheetChart then
            AppContent.updateChartSeriesList ()