namespace Ankat.ViewModel

open System
open System.ComponentModel
open System.Collections.Generic

open Ankat

[<AutoOpen>]
module private ViewModelPartyHelpers =

    type PartyPath = Repository.PartyPath

    type P = Ankat.ViewModel.Product
    type Col = System.Windows.Forms.DataGridViewTextBoxColumn
    type Cols = System.Windows.Forms.DataGridViewColumnCollection

    let getKefCol (p:P) =
        [ for c in MainWindow.gridKefs.Columns -> c ]
        |> List.filter ( fun c -> Object.ReferenceEquals(p, c.Tag) ) 
        |> List.head

    let removeKefCol p =
        MainWindow.gridKefs.Columns.Remove (getKefCol p)

    let updKef (p:P) (colIndex:int) (kef : Coef) = 
        let n = kef.Order
        let row = MainWindow.getRowOfCoef kef

        let cell = row.Cells.[colIndex]
        let value = p.getKefUi kef
        if cell.Value=null || (string) cell.Value <> value then
            row.Cells.[colIndex].Value <- value

    let createProductViewModel getPgs productType partyId p  = 

        let col = new Col(HeaderText = Ankat.Product.what p, Visible = p.IsChecked)
        MainWindow.gridKefs.Columns.Add( col ) |> ignore
        let x = ViewModel.Product(p, productType, getPgs, partyId)     
        Runtime.PropertyChanged.add x <| fun e ->
            match e.PropertyName with
            | "IsChecked" -> col.Visible <- x.IsChecked
            | "What" -> col.HeaderText <- x.What
            | _ -> ()

        x.CoefValueChanged.Add(fun (_, coef, _) -> 
            updKef x col.Index coef
            )
                
        Ankat.Coef.coefs |> List.iter( updKef x col.Index )
        col.Tag <- x
        x


        
type Party
        (   partyHeader:Ankat.Party.Head, 
            partyData : Ankat.Party.Data ) =

    inherit ViewModelBase() 

    let mutable partyHeader = partyHeader
    
    let mutable partyData = partyData
    
    let productType() = partyHeader.ProductType 
    
    let getPgsConc n = 
        Party.getPgsConc (partyHeader,partyData) n
    
    let getTermoTemperature t = 
        partyData.Temperature
        |> Map.tryFind t
        |> Option.getWith (TermoPt.defaultTermoTemperature t)
    
    let products, setProducts = 
        let x = BindingList<P>()
        let setProducts xs = 
            x.Clear()
            xs 
            |> List.map (createProductViewModel getPgsConc productType (PartyPath.fromPartyHead partyHeader))
            |> List.iter x.Add
        setProducts partyData.Products
        x, setProducts
    
    let getProducts() = products |> Seq.map(fun x -> x.Product) |> Seq.toList
    
    let updateProductsTypeAlchemy() = 
        for p in products do
            p.ForceCalculateErrors()

    let getChecked() =
        let xs = getProducts()
        if xs |> List.forall( fun x -> x.IsChecked ) then Nullable<bool>(true) else
        if xs |> List.forall( fun x -> not x.IsChecked ) then Nullable<bool>(false) else
        Nullable<bool>()
    
    let mutable productsChecked = Nullable<bool>()

    let setMainWindowTitle() = 
        MainWindow.form.Text <- 
            sprintf "Партия %s %s %A" 
                (DateTime.format "dd/MM/yy" partyHeader.Date)
                partyHeader.ProductType.What  
                partyHeader.Name
  
    do
        setMainWindowTitle()

    let addLoggingEvent = new Event<_>()

    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    member private __.AddLoggingEvent = addLoggingEvent

    [<CLIEvent>]
    member __.OnAddLogging = addLoggingEvent.Publish
            
    member __.Products = products

    member x.Party 
        with get() = 
            let partyData = { partyData with Products = getProducts() }
            let partyHeader = { partyHeader with ProductsSerials = partyData.Products |> List.map(fun x -> x.SerialNumber) }
            partyHeader, partyData
        and set ( otherHeader, otherPartyData) = 
            partyHeader <- otherHeader
            partyData <- otherPartyData
            products
            |> Seq.toList
            |> List.iter x.DeleteProduct
            setProducts otherPartyData.Products

            Pneumo.Clapan.valuesList
            |> List.iter (Prop.pgs >> x.RaisePropertyChanged)

            TermoPt.valuesList
            |> List.iter (Prop.t >> x.RaisePropertyChanged)

            x.RaisePropertyChanged "ProductType"
            x.RaisePropertyChanged "Name"
            setMainWindowTitle()
            AppConfig.config.View.PartyId <- partyHeader.Id
    
    member x.AddNewProduct() = 
        
        let serial = Seq.length products + 1
        Alchemy.createNewProduct serial x.GetPgs partyHeader.ProductType
        |> createProductViewModel getPgsConc productType (PartyPath.fromPartyHead partyHeader)
        |> products.Add 
        
    member __.DeleteProduct(product) = 
        let r = products.Remove( product )
        if not r then            
            failwith "Ankat.ViewModel.Party.DeleteProduct : missing element"
        removeKefCol product

    member x.UpdateProductsTypeAlchemy _ = updateProductsTypeAlchemy()
    member x.HasOneCheckedProduct() =
        products
        |> Seq.exists( fun p -> p.IsChecked )
    member x.HasNotOneCheckedProduct() =
        products
        |> Seq.exists( fun p -> p.IsChecked )
        |> not


    member x.SetPgs (gas,value) =
        if Some value <>  partyData.Pgs.TryFind gas then
            partyData <- { partyData with Pgs = Map.add gas value partyData.Pgs }
            setMainWindowTitle()
            updateProductsTypeAlchemy()

    member x.SetTermoTemperature (t,value) =
        if Some value <>  partyData.Temperature.TryFind t then
            partyData <- { partyData with Temperature = Map.add t value partyData.Temperature }
            

    member __.GetPgs x = getPgsConc x
    
    member __.GetTermoTemperature t = getTermoTemperature t

    member x.ComputeKefGroup (kefGroup) = 
        products 
        |> Seq.filter(fun p -> p.IsChecked)
        |> Seq.iter( fun p ->
            p.Product <- 
                runState (Alchemy.compute kefGroup getPgsConc partyHeader.ProductType ) p.Product
                |> snd )

    member __.getProductType() = partyHeader.ProductType

    member x.ProductType 
        with get() = partyHeader.ProductType.What
        and set v = 
            if v <> x.ProductType then
                let t = 
                    ProductType.values
                    |> List.tryFind( ProductType.what >> (=) v)
                    |> Option.getWith ProductType.first
                partyHeader <- { partyHeader with ProductType = t}
                x.RaisePropertyChanged "ProductType"
                setMainWindowTitle()
                updateProductsTypeAlchemy()
            
    member x.Name 
        with get() = partyHeader.Name
        and set v = 
            if v <> x.Name then
                partyHeader <- { partyHeader with Name = v}
                x.RaisePropertyChanged "Name"
                setMainWindowTitle()

    member x.Journal 
        with get() = partyData.Journal
        and set value =
            if value <> partyData.Journal then
                partyData <- { partyData with Journal =  value }
                x.RaisePropertyChanged "Journal"


[<AutoOpen>]
module private RunInfoHelpers =
    let private getHash (x:string) = x.GetHashCode()
    let now() = Some DateTime.Now
    let upd op y (x:Party) = 
        x.Journal <- Map.add (getHash op) y x.Journal
    let tryGetOp op (x:Party) = x.Journal.TryFind (getHash op)
    let getOp x party  = tryGetOp x party |> Option.getWithf PerformingOperation.createNew

type Party with
    
    member x.TryGetLogOperation operation  = tryGetOp operation x
    member x.GetLogOperation operation  = getOp operation x

    member x.LogStartOperation operation  = 
        upd operation  { RunStart = now(); RunEnd = None; LoggingRecords = []} x

    member x.LogStopOperation operation =
        upd operation { getOp operation x  with RunEnd = now() } x
    
    member x.WriteJournal operation level text = 
        let perfOp = getOp operation x
        upd operation { perfOp with LoggingRecords = (DateTime.Now,level,text)::perfOp.LoggingRecords } x
        x.AddLoggingEvent.Trigger (operation,level,text)