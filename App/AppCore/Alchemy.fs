module Ankat.Alchemy  

open System

type private P = Product
type private T = ProductType


[<AutoOpen>]
module ReportHelper = 
    type Content = 
        | Info of string
        | Error of string
        | Bitmap of System.IO.MemoryStream

let encodeDate (date : DateTime) = 
    let year = decimal date.Year - 2000m
    let month = decimal date.Month 
    let day = decimal date.Day
    year * 10000m + month * 100m + day

let initKefsValues pgs prodType =
    let chans = [ 
        yield Sens1, prodType.Sensor
        match prodType.Sensor2 with
        | Some ch -> yield Sens2, ch 
        | _ -> () ]  

    [   for sensInd, sensor in chans do
            let n0 = SScalePt.new' sensInd ScaleBeg
            let nk = SScalePt.new' sensInd ScaleEnd

            let pgs0, pgsK, shk0, shkK, shk, units, gastype = SensorIndex.prodTypeCoefs sensInd
            yield pgs0, pgs n0
            yield pgsK, pgs nk
            yield shk0, 0m
            yield shkK, sensor.Scale.Value
            yield shk, sensor.Scale.Code  
            yield units, sensor.Units.Code
            yield gastype, sensor.SensorCode
            yield! List.zip (SensorIndex.coefsLin sensInd) [0m; 1m; 0m; 0m] 

            yield! List.zip n0.CoefsTermo [0m; 0m; 0m] 
            yield! List.zip nk.CoefsTermo [1m; 0m; 0m]  
            
            let now = DateTime.Now
            yield YEAR, decimal now.Year ]
        
[<AutoOpen>]
module private PivateComputeProduct = 
    let round6 (x:decimal) = System.Math.Round(x,6)
    let tup2 = function [x;y] -> Some(x,y) | _ -> None
    let tup3 = function [x;y;z] -> Some(x,y,z) | _ -> None
    
    type C = 
        | V of Var
        | K of Coef
    
    let getVarsValues p vars =         
        let oks, errs =
            vars |> List.map( fun k ->
                match Product.getVar k p with 
                | None ->  Err k
                | Some value -> Ok (k,value) )
            |> List.partition Result.isOk
        if List.isEmpty errs then 
            Ok ( oks |> List.map ( Result.Unwrap.ok >> snd) ) 
        else 
            errs 
            |> List.map ( Result.Unwrap.err  >> V)
            |> Err
    
    let getKefsValues p kefs = 
        let oks, errs =
            kefs |> List.map( fun k ->
                match Product.getKef k p with 
                | None ->  Err k
                | Some value -> Ok (k,value) )
            |> List.partition Result.isOk
        if List.isEmpty errs then 
            Ok ( oks |> List.map ( Result.Unwrap.ok >> snd) ) 
        else 
            errs 
            |> List.map ( Result.Unwrap.err >> K )
            |> Err

    let fmtErr<'a> (fmt : 'a -> string) = function
        | [x] -> sprintf "точке %A" (fmt x)
        | xs -> 
            xs |> List.rev |> Seq.toStr ", " fmt     
            |> sprintf "точках %s"

    let getValuesTermo chan var gas p   =
        TermoPt.values
        |> List.map( fun t -> Correction( CorrectionTermoScale( SScalePt.new' chan gas)) , var, gas, t, PressNorm) |> getVarsValues p

    let getTermoT chan = getValuesTermo chan chan.Termo

    let getVar1T chan = getValuesTermo chan chan.Var1

    let calculatePressureSensCoefs (p:Product) =
        let ctx v p = Correction CorrectionPressSens , v, ScaleBeg, TermoNorm, p
        match getVarsValues p [ ctx Pmm PressNorm; ctx Pmm PressHigh; ctx VdatP PressNorm;  ctx VdatP PressHigh ]  with
        | Ok [x0; x1; y0; y1] -> 
            let k0 = (y1-y0)/( x1 - x0 )
            let k1 = y0 - x0*k0
            Logging.info "%s : расчёт коэффициентов %A ==> %M, %M" p.What CorrectionPressSens.Dscr k0 k1
            Ok [ k0; k1 ]
        | _ -> "не достаточно исходных данных" |> Err

    let getGaussXY p getPgsConc  = function
        | CorrectionPressSens -> failwith "PressureSensCoefs is not for gauss!"
        | CorrectionTermoPress ->
            // [ Termo1, ch1.Tpp, Air; Termo1, ch1.Var1, Air ]
            let g = Correction CorrectionTermoPress
            let xs var = 
                TermoPt.values
                |> List.map( fun t -> g , var, ScaleBeg, t, PressNorm) 
                |> getVarsValues p
            result {
                let! temps = xs Sens1.Termo
                let! vars = xs VdatP
                return List.zip temps vars }
            |> Result.mapErr( 
                fmtErr (function  V(_,_,_,t,_) -> sprintf "%A" t)
                >> sprintf "нет значения %A в %s" CorrectionTermoPress.Dscr )

        | CorrectionLinScale chan -> 
            chan.ScalePts 
            |> List.map( fun gas -> Correction( CorrectionLinScale chan ), chan.Conc, gas, TermoNorm, PressNorm )
            |> getVarsValues p
            |> Result.map ( fun xs -> 
                List.zip xs ( chan.ScalePts |> List.map ( SScalePt.new' chan >> getPgsConc )  ) )
            |> Result.mapErr( 
                fmtErr (function  V(_,_,gas,_,_) -> sprintf "%A" gas)
                >> sprintf "нет значения LIN в %s" )

        | CorrectionTermoScale { SensorIndex = Sens1; ScalePt = ScaleBeg} -> 
            result {
                let! t = getTermoT Sens1 ScaleBeg p
                let! var = getVar1T Sens1 ScaleBeg p
                return List.zip t var }
            |> Result.mapErr( 
                fmtErr (function V(_,var,_,t,_) -> sprintf "%s.%s" (PhysVar.what var) (TermoPt.what t) )                
                >> sprintf "нет значения T0.к1 в %s" )

        | CorrectionTermoScale { SensorIndex = Sens2; ScalePt = ScaleBeg} -> 
            result {
                let! t = getTermoT Sens2 ScaleBeg p
                let! var = getVar1T Sens2 ScaleBeg p
                return List.zip t ( List.map (fun var -> - var) var) }
            |> Result.mapErr( 
                fmtErr (function V(_,var,_,t,_) -> sprintf "%s.%s" (PhysVar.what var) (TermoPt.what t) )                
                >> sprintf "нет значения T0.к2 в %s" )

        | CorrectionTermoScale { SensorIndex = chan; ScalePt = ScaleEnd} -> 
            result {
                let! t = getTermoT chan ScaleEnd p
                let! var = getVar1T chan ScaleEnd p
                let! var0 = getVar1T chan ScaleBeg p
                return List.zip3 t var0 var}
            |> Result.mapErr( 
                fmtErr (function V(_,var,_,t,_) -> sprintf "%s.%s" (PhysVar.what var) (TermoPt.what t) )
                >> sprintf "нет значения TK.к%d в %s" chan.N )
            |> Result.bind( fun xs ->
                let errs =
                    xs |> List.zip TermoPt.values 
                    |> List.map(fun (ptT,(_,var0,var)) ->  if var0 = var then Some ptT else None )
                    |> List.filter Option.isSome
                if List.isEmpty errs then 
                    let vk = xs |> List.map( fun (t,var0,var) -> t, var - var0)
                    vk |> List.map(fun (t,x) -> t, snd vk.[1] / x)
                    |> Ok
                else
                    errs 
                    |> List.map Option.get
                    |> fmtErr TermoPt.what 
                    |> sprintf "при расчёте TK.к%d деление на ноль в %s" chan.N
                    |> Err )
        | CorrectionTermoScale { ScalePt = ScaleMid1} 
        | CorrectionTermoScale { ScalePt = ScaleMid2} ->  failwith "there is no TermoCoefs (_,ScaleMid) in AnkatMICRO!!"


    let doValuesGaussXY group xy =
        let groupCoefs = Correction.coefs group
        let groupCoefsSet = Set.ofList groupCoefs
        let strGroupCoefs = Seq.toStr ", " (Coef.order >> string) groupCoefs

        let x,y = List.toArray xy |> Array.unzip
        let result =  NumericMethod.GaussInterpolation.calculate(x,y) 
        let ff = Seq.toStr ", " string
        Logging.info "метод Гаусса X=%s Y=%s ==> %s=%s" (ff x) (ff y) strGroupCoefs (ff result)
        Array.toList result 
        

let compute group getPgsConc productType = state {
    let! product = getState
    let groupCoefs = Correction.coefs group
    let groupCoefsSet = Set.ofList groupCoefs
    let strGroupCoefs = Seq.toStr ", " (Coef.order >> string) groupCoefs
    Logging.info "%s : расчёт коэффициентов %A, %s" (Product.what product) (Correction.what group) strGroupCoefs
    do!
        initKefsValues getPgsConc productType
        |> List.filter(fst >> groupCoefsSet.Contains)
        |> Product.setKefs 

    let result = 
        match group with
        | CorrectionPressSens -> calculatePressureSensCoefs product
        | _ ->
            getGaussXY product getPgsConc group
            |> Result.map (doValuesGaussXY group)

    match result with
    | Err e -> 
        Logging.error "%s : %s" (Product.what product) e
    | Ok result ->
        do! result |> List.zipCuty 0m groupCoefs |> Product.setKefs   }

let getProductTermoErrorlimit channel pgs (gas,t) product =
    let concVar = channel.ChannelIndex.Conc
    let f = TestConcErrors channel.ChannelIndex
    if not channel.ChannelSensor.IsCH then         
        let tempVar = channel.ChannelIndex.Termo
        
        (Product.getVar (f, concVar, gas,t,PressNorm) product, Product.getVar (f, tempVar, gas, t,PressNorm) product) 
        |> Option.map2(fun(c,t) -> 
            let dt = t - 20m     
            let maxc = channel.ChannelSensor.ConcErrorlimit pgs
            0.5m * abs( maxc*dt ) / 10.0m )
    else
        match gas with
        | ScaleBeg -> Some 5m
        | _ ->
            Product.getVar (f,concVar,gas,TermoNorm,PressNorm) product
            |> Option.map(fun conc20 -> conc20 * 0.15m |> abs  |> decimal )

type ValueError = 
    {   Value : decimal
        Nominal : decimal
        Limit : decimal }
    member x.IsError = abs x.Error >= abs x.Limit 
    member x.Error = x.Value - x.Nominal
    static member error = function 
        | Some (x : ValueError ) -> Decimal.toStr ".###" x.Error
        | _ -> ""

type Product with

    static member concError channel pgs gas product = 
        Product.getVar (TestConcErrors channel.ChannelIndex, channel.ChannelIndex.Conc, gas, TermoNorm, PressNorm) product 
        |> Option.map(fun conc ->                 
            { Value = conc; Nominal = pgs; Limit = channel.ChannelSensor.ConcErrorlimit pgs } ) 

    static member termoError channel pgs (gas,t) p = 
        let concVar = channel.ChannelIndex.Conc
        (   Product.getVar (TestConcErrors channel.ChannelIndex,concVar,gas,t, PressNorm) p,
            Product.getVar (TestConcErrors channel.ChannelIndex,concVar,gas,TermoNorm, PressNorm) p,
            Product.termoErrorlimit channel pgs (gas,t) p )
        |> Option.map3( fun (c,c20,limit) -> 
            { Value = c; Nominal = c20; Limit = limit } )

let createNewProduct serialNumber getPgs productType =
    let prodstate = state {
        let! product = getState
        do!
            (SER_NUMBER, decimal serialNumber)
            :: ( initKefsValues getPgs productType )
            |> Product.setKefs  }
    runState prodstate (Product.createNew serialNumber)
    |> snd


let createNewParty() = 
    let h,d = Party.createNewEmpty()
    let getPgsConc = d.BallonConc.TryFind >> Option.getWith 0m
    let productType = h.ProductType
    let product = createNewProduct 1 getPgsConc productType
    let products = [ product ]
    { h with ProductsSerials = [product.SerialNumber] }, { d with Products = products }

let createNewParty1( name, productType, count) : Party.Content = 
        let pgs =
            SScalePt.values
            |> List.map(fun pt -> pt, ScalePt.defaultBallonConc pt.ScalePt )             
            |> Map.ofList 
        let getPgsConc = pgs.TryFind >> Option.getWith 0m
        let products = 
            [1..count] 
            |> List.map( fun n ->  createNewProduct n getPgsConc productType )
        
        {   Id = Product.createNewId()
            ProductsSerials = List.map Product.productSerial products
            Date=DateTime.Now 
            Name = name
            ProductType = productType }, 
                {   Products = products
                    BallonConc = pgs
                    TermoTemperature = Map.empty
                    PerformingJournal = Map.empty}
