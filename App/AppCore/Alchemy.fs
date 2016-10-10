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



let initKefsValues pgs t =
    let chans = [ 
        yield Chan1, t.Channel
        match t.Channel2 with
        | Some ch -> yield Chan2, ch 
        | _ -> () ]  

    [   for iChan, chan in chans do
            let pgs0, pgsK, shk0, shkK, shk, units, gastype = ChannelIndex.prodTypeCoefs iChan
            yield pgs0, pgs ScaleBeg 
            yield pgsK, pgs ScaleEnd
            yield shk0, 0m
            yield shkK, chan.Scale.Value
            yield shk, chan.Scale.Code  
            yield units, chan.Units.Code
            yield gastype, chan.Gas.Code 
            yield! List.zip (ChannelIndex.coefsLin iChan) [0m; 1m; 0m; 0m] 

            yield! List.zip (ChannelIndex.coefsTermo (iChan,ScaleBeg)) [0m; 0m; 0m] 
            yield! List.zip (ChannelIndex.coefsTermo (iChan,ScaleEnd)) [1m; 0m; 0m]  
            
            let now = DateTime.Now
            yield YEAR, decimal now.Year ]

    


let initializeKefsValues pgs t = state{ 
    for kef,value in initKefsValues pgs t do
        let! p = getState
        do! P.setKef kef (Some value) }
    
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
        |> List.map( fun t -> FeatureKefGroup( KefTermo(chan,gas)) , var, gas, t, Pnorm) |> getVarsValues p

    let getTermoT chan = getValuesTermo chan chan.Termo

    let getVar1T chan = getValuesTermo chan chan.Var1

    let getScaleValues p f =
        ScalePt.values
        |> List.map f
        |> getVarsValues p

    
    let getGaussXY p pgs  = function
        | KefPressureSens -> failwith "KefPressureSens is not for gauss!"
        | KefLin chan -> 
            ScalePt.values 
            |> List.map( fun gas -> FeatureKefGroup( KefLin chan ), chan.Conc, gas, TermoNorm, Pnorm )
            |> getVarsValues p
            |> Result.map ( fun xs -> 
                List.zip xs ( ScalePt.values |> List.map pgs  ) )
            |> Result.mapErr( 
                fmtErr (function  V(_,_,gas,_,_) -> sprintf "%A" gas)
                >> sprintf "нет значения LIN в %s" )
        | KefTermo (chan,ScaleBeg) -> 
            result {
                let! t = getTermoT chan ScaleBeg p
                let! var = getVar1T chan ScaleBeg p
                return List.zip t ( List.map (fun var -> - var) var) }
            |> Result.mapErr( 
                fmtErr (function V(_,var,_,t,_) -> sprintf "%s.%s" (PhysVar.what var) (TermoPt.what t) )                
                >> sprintf "нет значения T0 в %s" )

        | KefTermo (chan,ScaleEnd) -> 
            result {
                let! t = getTermoT chan ScaleEnd p
                let! var = getVar1T chan ScaleEnd p
                let! var0 = getVar1T chan ScaleBeg p
                return List.zip3 t var0 var}
            |> Result.mapErr( 
                fmtErr (function V(_,var,_,t,_) -> sprintf "%s.%s" (PhysVar.what var) (TermoPt.what t) )
                >> sprintf "нет значения TK в %s" )
            |> Result.bind(fun xs ->
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
                    |> sprintf "при расчёте TK деление на ноль в %s"
                    |> Err )
        | KefTermo (_,ScaleMid) ->  failwith "there is no KefTermo (_,ScaleMid) in AnkatMICRO!!"

        

let compute group pgs productType = state {
    let! product = getState
    let kefs = KefGroup.kefs group
    let skefs = Seq.toStr ", " (Coef.order >> string) kefs    
    Logging.info "%s : расчёт коэффициентов %A, %s" (Product.what product) (KefGroup.what group) skefs

    do! kefs |> List.choose(fun kef -> 
            initKefValue pgs productType kef product
            |> Option.map(fun v -> kef,v) )
        |> Product.setKefs 
    let result = getGaussXY product pgs group
    match result with
    | Err e -> Logging.error "%s : %s" (Product.what product) e
    | Ok xy ->
        let x,y = List.toArray xy |> Array.unzip
        let result =  NumericMethod.GaussInterpolation.calculate(x,y) 
        let ff = Seq.toStr ", " string
        Logging.info "метод Гаусса X=%s Y=%s ==> %s=%s" (ff x) (ff y) skefs (ff result)
        do! result |> Array.toList |> List.zip kefs |> Product.setKefs   }

    

let concErrorlimit productType concValue =        
    let scale = ProductType.scale productType        
    if ProductType.isCH productType then 2.5m+0.05m * concValue
    elif scale=Sc4 then 0.2m + 0.05m * concValue
    elif scale=Sc10 then 0.5m
    elif scale=Sc20 then 1.0m else 0.m


let termoErrorlimit chan productType pgs (gas,t) product =
    let concVar = Channel.conc chan
    if ProductType.isCH productType |> not then 
        
        let tempVar = Channel.termo chan
        (Product.getVar (Test, concVar, gas,t,Pnorm) product, Product.getVar (Test, tempVar, gas, t,Pnorm) product) 
        |> Option.map2(fun(c,t) -> 
            let dt = t - 20m     
            let maxc = concErrorlimit productType pgs
            0.5m * abs( maxc*dt ) / 10.0m )
    else
        match gas with
        | ScaleBeg -> Some 5m
        | _ ->
            Product.getVar (Test,concVar,gas,TermoNorm,Pnorm) product
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

    static member concError chan productType pgs gas product = 
        Product.getVar (Test, (Channel.conc chan), gas, TermoNorm, Pnorm) product 
        |> Option.map(fun conc ->                 
            { Value = conc; Nominal = pgs; Limit = concErrorlimit productType pgs } ) 

    static member termoError chan productType pgs (gas,t) p = 
        let concVar = Channel.conc chan
        (   Product.getVar (Test,concVar,gas,t, Pnorm) p,
            Product.getVar (Test,concVar,gas,TermoNorm, Pnorm) p,
            termoErrorlimit chan productType pgs (gas,t) p )
        |> Option.map3( fun (c,c20,limit) -> 
            { Value = c; Nominal = c20; Limit = limit } )

let createNewProduct addr getPgs productType =
    runState 
        ( initializeKefsValues Coef.coefs getPgs productType )
        ( Product.createNew addr )
    |> snd


let createNewParty() = 
    let h,d = Party.createNewEmpty()
    let getPgs = d.BallonConc.TryFind >> Option.getWith 0m
    let productType = h.ProductType
    let product = createNewProduct 1uy getPgs productType
    let products = [ product ]
    {h with ProductsSerials = [product.ProductSerial] }, { d with Products = products }

let createNewParty1( name, productType, pgs1, pgs2, pgs3, count) : Party.Content = 
        let pgs = Map.ofList <| List.zip ScalePt.values [pgs1; pgs2; pgs3]
        let getPgs = pgs.TryFind >> Option.getWith 0m
        let products = 
            [1uy..count] 
            |> List.map( fun addr ->  createNewProduct addr getPgs productType )
        
        {   Id = Product.createNewId()
            ProductsSerials = List.map Product.productSerial products
            Date=DateTime.Now 
            Name = name
            ProductType = productType }, 
                {   Products = products
                    BallonConc = pgs
                    TermoTemperature = Map.empty
                    PerformingJournal = Map.empty}