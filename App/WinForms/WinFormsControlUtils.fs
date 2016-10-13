[<AutoOpen>]
module WinFormsControlUtils 

open System.Windows.Forms
open System.Drawing

[<AutoOpen>]
module Helpers1 =
    let rec loopControls (x:Control) chooser mapper = seq{
        match chooser x with
        | None -> ()
        | Some y -> 
            yield mapper y
        for x in x.Controls do
            yield! loopControls x chooser mapper }

    let myCombobox() = 
        let cb = new MyWinForms.FlatComboBox(DropDownStyle = ComboBoxStyle.DropDownList, 
                                    FlatStyle = FlatStyle.Flat, Font = new Font("Consolas", 12.f),
                                    DrawMode = DrawMode.OwnerDrawFixed )
        cb.DrawItem.Add(fun e -> 
            if e.Index < 0 then () else
            if (e.State &&& DrawItemState.Selected) = DrawItemState.Selected then
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds)
            else
                e.Graphics.FillRectangle(new SolidBrush(cb.BackColor), e.Bounds);
            let str = cb.Items.[e.Index].ToString()
            let brs = new SolidBrush(cb.ForeColor)
            let pt = PointF(float32 e.Bounds.X, float32 e.Bounds.Y)
            e.Graphics.DrawString( str, e.Font, brs, pt)

            e.DrawFocusRectangle() )
        cb

    let myListbox() = 
        let cb = new ListBox(Font = new Font("Consolas", 12.f), ItemHeight = 20,
                              BorderStyle = BorderStyle.None,
                              DrawMode = DrawMode.OwnerDrawFixed )
        cb.DrawItem.Add(fun e -> 
            if e.Index < 0 then () else
            if (e.State &&& DrawItemState.Selected) = DrawItemState.Selected then
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds)
            else
                e.Graphics.FillRectangle(new SolidBrush(cb.BackColor), e.Bounds);
            let str = cb.Items.[e.Index].ToString()
            let brs = new SolidBrush(cb.ForeColor)
            let pt = PointF(float32 e.Bounds.X, float32 e.Bounds.Y)
            e.Graphics.DrawString( str, e.Font, brs, pt)

            e.DrawFocusRectangle() )
        cb

type Control with
    static member performThreadSafeAction<'a> (ctrl:Control) (f:unit -> 'a) =
        if ctrl.InvokeRequired then 
            let mutable x : 'a option = None
            ctrl.Invoke <| new  MethodInvoker( fun () -> x <- Some (f()) ) |> ignore
            x.Value
        else f()

    member x.PerformThreadSafeAction f = Control.performThreadSafeAction x f

    member x.EnumControls(chooser,mapper) = loopControls x chooser mapper
    member x.enumControls chooser mapper = loopControls x chooser mapper

    member x.SetInfoStyle () = 
        x.BackColor <- Color.Navy
        x.ForeColor <- Color.White
        x.MouseEnter.Add <| fun _ ->  x.BackColor <- Color.DodgerBlue
        x.MouseLeave.Add <| fun _ ->  x.BackColor <- Color.Navy

type DataGridViewColumnCollection with 
    static member addColumn<'a when 'a :> DataGridViewColumn > (cols:DataGridViewColumnCollection) (col:'a)  =
        cols.Add col |> ignore
    
    static member addColumns xs (cols:DataGridViewColumnCollection) =
        xs |> Seq.iter ( cols.Add >> ignore)

    member x.AddColumn(col) = DataGridViewColumnCollection.addColumn x col
    member x.AddColumns(cols) = DataGridViewColumnCollection.addColumns cols x

    member x.SetDisplayIndexByOrder() = 
        x |> Seq.cast |> Seq.iteri (fun n (col:DataGridViewColumn) -> 
            col.DisplayIndex <- n )

    member x.``remove all columns but``<'T when 'T :> DataGridViewColumn > (columns : 'T seq) = 
        [   for y in x do 
                if columns |> Seq.exists(fun c -> obj.ReferenceEquals(c,y) ) |> not then
                    yield y]
        |> List.iter x.Remove

type DataGridView with
    static member updateBinding (g:DataGridView) = 
        let d = g.DataSource
        g.DataSource <- null
        g.DataSource <- d

    member x.UpdateBinding() = DataGridView.updateBinding x



        