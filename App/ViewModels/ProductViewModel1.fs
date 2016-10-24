namespace  Ankat.ViewModel

open System
open System.Text.RegularExpressions

open Ankat
open Ankat.Alchemy

[<AutoOpen>]
module private ViewModelProductHelpers =
    type P = Ankat.Product
    let appCfg = AppConfig.config
    type PSr = Chart.ProductSeriesInfo

    //let nCase<'a when 'a : equality> = FSharpType.caseOrder<'a>


type private K = PhysVarValues.K

[<AbstractClass>]
type Product1(p : P, getProductType, getPgsConc, partyId) =
    inherit ViewModelBase()    
    let mutable p = p

    let mutable connection : Result<string,string> option = None
    
    let getConcError ((n, gas, t) as pt) = 
        let sensor = SensorIndex.sensorOfProdTypeByIndex (getProductType()) n
        
        sensor 
        |> Option.bind( fun sensor ->
            match t with 
            | TermoNorm -> P.concError sensor (apply2 getPgsConc n) (n, gas) p 
            | _ -> P.termoError sensor getPgsConc pt p )

    let getConcErrors () = 
        List.zip3 SensorIndex.valuesList ScalePt.valuesList TermoPt.valuesList
        |> List.map (fun k -> k, getConcError k )
        |> Map.ofList
               
    let getVarsValues() = 
        Points.prod
        |> List.map (fun pt -> pt, Product.getVar pt p)
        |> Map.ofList

    let getKefsValues() = 
        Coef.coefs
        |> List.map(fun ctx -> ctx, P.getKef ctx p )
        |> Map.ofList

    let mutable physVar = Map.empty

    let coefValueChangedEvent = Event<Product1 * Coef * decimal option >()

    let port() = { appCfg.ComportProducts with PortName = p.SerialPortName }
    
    [<CLIEvent>]
    member x.CoefValueChanged = coefValueChangedEvent.Publish

    member x.andThenUpdateAllErrors(f) = 
        let prevConcErrors =  getConcErrors()                
        let prevVarsValues = getVarsValues()
        let prevKefsValues = getKefsValues()
        f()
        let concErrors =  getConcErrors()
        let varsValues = getVarsValues()
        let kefsValues = getKefsValues()

        listOf {
            let! n = [ Sens1; Sens2 ]
            let! scale = ScalePt.valuesList
            let! t = TermoPt.valuesList
            return n,scale,t }
        |> List.filter(fun k -> prevConcErrors.[k] <> concErrors.[k] )
        |> List.iter (Prop.concError >> x.RaisePropertyChanged)

        

    member x.setKefsInitValues () = 
        let productType = getProductType()    

        let prodstate = state {
            let! product = getState
            do!
                (SER_NUMBER, decimal x.SerialNumber)
                :: ( initKefsValues getPgsConc productType )
                |> Product.setKefs  }        
        x.Product <- snd <| runState  prodstate x.Product
            
    member x.setPhysVarValue var value =        
        PhysVarValues.addValue {K.Party = partyId; K.Product = p.Id; K.Var = var } value
        MainWindow.form.PerformThreadSafeAction <| fun () ->
            Chart.addProductValue p.Id var value
        if Map.tryFind var physVar <> Some value then
            physVar <- Map.add var value physVar
            x.RaisePropertyChanged (Prop.physVar var)
            

    member x.getPhysVarValueUi var =
        Map.tryFind var physVar
        |> Option.map Decimal.toStr6
        |> Option.getWith ""

    member x.GetConcError = getConcError        
    
    member x.Connection
        with get () = connection
        and set v = 
            if v <> connection then
                connection <- v
                x.RaisePropertyChanged "Connection"
                
    
    member x.IsChecked 
        with get () = p.IsChecked          
        and set v = 
            if v <> p.IsChecked then
                p <- { p with IsChecked = v}
                x.RaisePropertyChanged "IsChecked"
                if v then
                    Chart.addProductSeries
                        {   PSr.Product = p.Id
                            PSr.Party = partyId
                            PSr.Name = p.What}                else 
                    Chart.removeProductSeries p.Id |> ignore
    member x.SerialNumber
        with get () = p.SerialNumber
        and set v = 
            if v <> p.SerialNumber then
                p <- { p with SerialNumber = v }
                x.RaisePropertyChanged "What"
                x.RaisePropertyChanged "SerialNumber"
                Chart.setProductLegend p.Id x.What
                x.setKef SER_NUMBER  (Some <| decimal v)

    member x.ForceCalculateErrors() =        
        listOf {
            let! n = [ Sens1; Sens2 ]
            let! scale = ScalePt.valuesList
            let! t = TermoPt.valuesList
            return n,scale,t }
        |> List.iter (Prop.concError >> x.RaisePropertyChanged ) 

    member x.Port 
        with get () = p.SerialPortName
        and set other =
            if p.SerialPortName = other then () else
            p <- {p with SerialPortName = other}
            x.RaisePropertyChanged "Port"

    member x.Product 
        with get () = p
        and set other =
            if p = other then () else
            let prevVarsValues = getVarsValues()
            let prevKefsValues = getKefsValues()

            x.andThenUpdateAllErrors <| fun () ->
                p <- other

            let varsValues = getVarsValues()
            let kefsValues = getKefsValues()

            Points.prod
            |> List.filter(fun var -> prevVarsValues.[var] <> varsValues.[var] )
            |> List.iter (Prop.dataPoint >> x.RaisePropertyChanged) 

            Coef.coefs
            |> List.filter(fun coef -> prevKefsValues.[coef] <> kefsValues.[coef] )
            |> List.iter (fun coef -> 
                coefValueChangedEvent.Trigger(x, coef, kefsValues.[coef]) )

            x.RaisePropertyChanged "Product"
            x.RaisePropertyChanged "What"

    member x.What = P.what p

    member x.getKef kef = P.getKef kef p
    member x.setKef kef value =    
        if P.getKef kef p = value then () else
        let s = state{ do! P.setKef kef value}
        p <- runState s p |> snd
        coefValueChangedEvent.Trigger(x,kef,value)

    member x.getVar var = P.getVar var p
    member x.setVar ( var) value =
        if P.getVar var p = value then () else

        x.andThenUpdateAllErrors <| fun () ->
            let s = state{ do! P.setVar var value}
            p <- runState s p |> snd
            var |> Prop.dataPoint |> x.RaisePropertyChanged

        
    member x.getKefUi kef = 
        P.getKef kef p 
        |> Option.map Decimal.toStr6
        |> Option.getWith ""

    member x.setKefUi kef value = 
        String.tryParseDecimal value
        |> x.setKef kef

    member x.getVarUi var = 
        P.getVar var p 
        |> Option.map Decimal.toStr6
        |> Option.getWith ""

    member x.setVarUi var value = 
        String.tryParseDecimal value
        |> x.setVar var

    
    member x.ReadModbus(ctx) = 
        let r = Mdbs.read3decimal (port()) 1uy (ReadContext.code ctx) (ReadContext.what ctx)
        match r, ctx with
        | Ok value, ReadVar var -> 
            x.setPhysVarValue var value
        | Ok value, ReadKef kef -> x.setKef kef (Some value)
        | _ -> ()

        x.Connection <- 
            r 
            |> Result.map(fun v -> sprintf "%s = %s" (ReadContext.what ctx) (Decimal.toStr6 v))
            |> Some 
        r

    member x.WriteModbus (ctx,value) = 
        let what = WriteContext.what ctx
        let r = Mdbs.write (port()) 1uy (WriteContext.code ctx) what value
        match r with 
        | Err e -> Logging.error "%s, %s : %s" x.What what e
        | Ok () -> Logging.info "%s, %s" x.What what
        x.Connection <- 
            r 
            |> Result.map(fun v -> sprintf "%s <-- %s" (WriteContext.what ctx) (Decimal.toStr6 value))
            |> Some 
        r

    member x.SetModbusAddr () = 
        let what = "Установка адреса MODBUS 1"
        let r = result{        
            do! Mdbs.write (port()) 0uy CmdSetAddr.Code what 1m
            let! _ = x.ReadModbus(ReadVar CCh0) 
            return () }
        x.Connection <- 
            r |> Result.map(fun () -> what ) |> Some         
        match r with 
        | Err e ->  Logging.error "%s, %s : %s" x.What what e                
        | Ok () -> Logging.info "%s, %s" x.What what
        Result.someErr r
        

    member x.ComputeKefGroup kefGroup = 
        x.Product <- snd <| runState (Alchemy.compute kefGroup getPgsConc (getProductType())) p