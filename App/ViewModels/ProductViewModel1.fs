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


type private K = PhysVarValues.K

[<AbstractClass>]
type Product1(p : P, getProductType, getPgs, partyId) =
    inherit ViewModelBase()    
    let mutable p = p

    let mutable connection : Result<string,string> option = None
    
    let getTermoError (sensorIndex,scalePt,termoPt) = 
        SensorIndex.sensorOfProdTypeByIndex (getProductType()) sensorIndex
        |> Option.bind( fun sensor ->
            P.termoError 
                {ChannelSensor = sensor; ChannelIndex = sensorIndex} 
                (getPgs sensorIndex scalePt) (scalePt,termoPt) p )

    let getConcError (sensorIndex,scalePt) = 
        SensorIndex.sensorOfProdTypeByIndex (getProductType()) sensorIndex
        |> Option.bind( fun sensor ->
            P.concError 
                {ChannelSensor = sensor; ChannelIndex = sensorIndex} 
                (getPgs sensorIndex scalePt) scalePt p )

    let getConcErrors () = 
        Vars.sensor_gas_vars
        |> List.map(fun k -> k, getConcError k )
        |> Map.ofList

    let getTermoErrors () = 
        Vars.sensor_gas_t_vars
        |> List.map(fun k -> k, getTermoError k )
        |> Map.ofList
               
    let getVarsValues() = 
        Vars.vars
        |> List.map(fun ctx -> ctx, P.getVar ctx p )
        |> Map.ofList

    let getKefsValues() = 
        Coef.coefs
        |> List.map(fun ctx -> ctx, P.getKef ctx p )
        |> Map.ofList

    let mutable physVar = Map.empty

    let coefValueChangedEvent = Event<Product1 * Coef * decimal option >()

    
    
    [<CLIEvent>]
    member x.CoefValueChanged = coefValueChangedEvent.Publish

    member x.andThenUpdateAllErrors(f) = 
        let prevConcErrors =  getConcErrors()                
        let prevTermoError = getTermoErrors()
        let prevVarsValues = getVarsValues()
        let prevKefsValues = getKefsValues()
        f()
        let concErrors =  getConcErrors()
        let termoError = getTermoErrors()
        let varsValues = getVarsValues()
        let kefsValues = getKefsValues()

        Vars.sensor_gas_vars
        |> List.filter(fun gas -> prevConcErrors.[gas] <> concErrors.[gas] )
        |> List.iter (Property.concError >> x.RaisePropertyChanged)

        Vars.sensor_gas_t_vars 
        |> List.filter(fun var -> prevTermoError.[var] <> termoError.[var] )
        |> List.iter (Property.termoError >> x.RaisePropertyChanged) 

    member x.setKefsInitValues () = 
        let productType = getProductType()    

        let prodstate = state {
            let! product = getState
            do!
                initKefsValues getPgs productType
                |> Product.setKefs  }        
        x.Product <- snd <| runState  prodstate x.Product
            
    member x.setPhysVarValue var value =        
        PhysVarValues.addValue {K.Party = partyId; K.Product = p.Id; K.Var = var } value
        MainWindow.form.PerformThreadSafeAction <| fun () ->
            Chart.addProductValue p.Id var value
        if Map.tryFind var physVar <> Some value then
            physVar <- Map.add var value physVar
            x.RaisePropertyChanged <| PhysVar.name var
            

    member x.getPhysVarValueUi var =
        Map.tryFind var physVar
        |> Option.map Decimal.toStr6
        |> Option.getWith ""

    member x.GetConcError = getConcError        
    
    member x.GetTermoError = getTermoError

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
    member x.Addr
        with get () = p.Addr          
        and set v = 
            if v <> p.Addr then
                p <- { p with Addr = v}
                x.RaisePropertyChanged "Addr"
                x.RaisePropertyChanged "What"
                Chart.setProductLegend p.Id x.What

    member x.SerialNumber
        with get () = p.ProductSerial.SerialNumber
        and set v = 
            if v <> p.ProductSerial.SerialNumber then
                p <- { p with ProductSerial = {p.ProductSerial with SerialNumber = v} }
                x.RaisePropertyChanged "What"
                x.RaisePropertyChanged "SerialNumber"
                Chart.setProductLegend p.Id x.What

    member x.ProdReady
        with get () = p.ProductSerial.ProdMonthYear.IsSome
        and set v = 
            if v <> x.ProdReady then
                let ym =
                    if v then 
                        let now = DateTime.Now
                        Some ( byte now.Month, byte (now.Year - 2000) )
                    else None 
                p <- { p with ProductSerial = {p.ProductSerial with ProdMonthYear = ym } }
                x.RaisePropertyChanged "ProdReady"
                x.RaisePropertyChanged "MonthYearStr"

    member x.MonthYearStr
        with get () = 
            p.ProductSerial.ProdMonthYear
            |> Option.map(fun (m,y) ->
                let m = if m<10uy then sprintf "0%d" m else m.ToString()
                sprintf "%s.20%d" m y )
            |> Option.getWith ""

    member x.ForceCalculateErrors() =
        let (~%%) = x.RaisePropertyChanged
        for sensInd in SensorIndex.values do
            for ( (sensInd,gas) as k) in Sens1.ScalePts1 @ Sens2.ScalePts1 do
                %% Property.concError k
                for t in TermoPt.values do
                    %% Property.termoError (sensInd,gas,t) 

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

            Vars.vars
            |> List.filter(fun var -> prevVarsValues.[var] <> varsValues.[var] )
            |> List.iter (Property.var >> x.RaisePropertyChanged) 

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
    member x.setVar ( (feat, physVar, scalePt, termoPt, presPt) as var) value =
        if P.getVar var p = value then () else

        x.andThenUpdateAllErrors <| fun () ->
            let s = state{ do! P.setVar var value}
            p <- runState s p |> snd
            var |> Property.var |> x.RaisePropertyChanged

        
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
        let r = Mdbs.read3decimal appCfg.ComportProducts x.Addr (ReadContext.code ctx) (ReadContext.what ctx)
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
        let r = Mdbs.write appCfg.ComportProducts x.Addr (WriteContext.code ctx) what value
        match r with 
        | Err e -> Logging.error "%s, %s : %s" x.What what e
        | Ok () -> Logging.info "%s, %s" x.What what
        x.Connection <- 
            r 
            |> Result.map(fun v -> sprintf "%s <-- %s" (WriteContext.what ctx) (Decimal.toStr6 value))
            |> Some 
        r

    member x.ComputeKefGroup kefGroup = 
        x.Product <- snd <| runState (Alchemy.compute kefGroup getPgs (getProductType())) p