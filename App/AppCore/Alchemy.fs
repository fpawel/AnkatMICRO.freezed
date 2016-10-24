module Ankat.Alchemy  

open System

type private P = Product
type private T = ProductType



let initKefsValues getPgsConc  prodType =
    let chans = [ 
        yield Sens1, prodType.Sensor
        match prodType.Sensor2 with
        | Some ch -> yield Sens2, ch 
        | _ -> () ]  

    let scalePt = ScaleEdge >> ScalePt.toLinPt 

    [   for n, sensor in chans do

            
            
            let pgs0, pgsK, shk0, shkK, shk, units, gastype = SensorIndex.prodTypeCoefs n
            yield pgs0, getPgsConc (n, scalePt ScaleBeg)
            yield pgsK, getPgsConc (n, scalePt ScaleEnd)
            yield shk0, 0m
            yield shkK, sensor.Scale.Value
            yield shk, sensor.Scale.Code  
            yield units, sensor.Units.Code
            yield gastype, sensor.SensorCode
            yield! List.zip (SensorIndex.coefsLin n) [0m; 1m; 0m; 0m] 
            yield! List.zip (Correction.coefsTermo n ScaleBeg) [0m; 0m; 0m] 
            yield! List.zip (Correction.coefsTermo n ScaleEnd) [1m; 0m; 0m]  
            
            let now = DateTime.Now
            yield YEAR, decimal now.Year ]
        
[<AutoOpen>]
module private PivateComputeProduct = 
    let round6 (x:decimal) = System.Math.Round(x,6)
    let tup2 = function [x;y] -> Some(x,y) | _ -> None
    let tup3 = function [x;y;z] -> Some(x,y,z) | _ -> None
    
    type C = 
        | V of ProdDataPt * PhysVar
        | K of Coef
    
    let getVarsValues p vars  =         
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
    
    let getKefsValues kefs p  = 
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

    let getValuesTermo p n gas var  =
        valuesListOf<TermoPt>
        |> List.map( fun t -> TermoScalePt ( n,gas,t) , var  ) 
        |> getVarsValues p

    let fmtCorrErr f = 
        Result.mapErr( function        
            | [V(CorrectionOfProdDataPt cor, var)] -> 
                sprintf "нет значения %s, %s" cor.What var.What
            | xs -> 
                let strCor = 
                    xs 
                    |> List.map( fun (V(CorrectionOfProdDataPt x,_)) -> x.What ) 
                    |> List.head
                xs |> List.rev |> Seq.toStr ", " (fun (V(prodPt, var)) -> sprintf "(%s,%s)" (f prodPt) var.What )     
                |> sprintf "нет значений %s в точках %s" strCor )

    
    let calculatePressureSensCoefs (p:Product) =
        
        [   PressSensPt PressNorm, Pmm
            PressSensPt PressHigh, Pmm
            PressSensPt PressNorm, VdatP
            PressSensPt PressHigh, VdatP ]
        |> getVarsValues p
        |> Result.map (  fun [x0; x1; y0; y1] -> 
            let k0 = (y1-y0)/( x1 - x0 )
            let k1 = y0 - x0*k0
            Logging.info "%s : расчёт коэффициентов %A ==> %M, %M" p.What CorPressSens.Descr k0 k1
            [ k0; k1 ] )
        |> fmtCorrErr ( fun (PressSensPt press) -> press.What) 

    let getGaussXY p getPgsConc prodType  = function
        | CorPressSens -> failwith "PressureSensCoefs is not for gauss!"
        | CorTermoPress ->
            // [ Termo1, ch1.Tpp, Air; Termo1, ch1.Var1, Air ]
            let xs var =
                getVarsValues p (List.map( fun t -> TermoPressPt t, var) valuesListOf<TermoPt>)                
            result {
                let! temps = xs TppCh0
                let! vars = xs VdatP
                return List.zip temps vars }     
            |> fmtCorrErr ( fun (TermoPressPt t) -> t.What)                      

        | CorLin n -> 
            let xs1 = 
                match prodType.Sensor with
                | IsCO2Sensor true -> [Lin1; Lin2; Lin3; Lin4]
                | _ -> [Lin1; Lin2; Lin4]
            let ys = List.map (getPgsConc n) xs1 
            let xs2 = List.map( fun pt -> LinPt (n,pt), n.Conc ) xs1                
            getVarsValues p xs2
            |> Result.map ( fun xs -> List.zip xs ys )
            |> fmtCorrErr ( fun (LinPt (_,lin) ) -> valueOrderOf lin |> string) 
        
        | CorTermoScale ( n, ScaleBeg ) -> 
            let (~%%) = getValuesTermo p n ScaleBeg 
            result {
                let! t = %% n.Termo
                let! var = %% n.Var1
                return List.zip t var }
            |> Result.map( fun xy ->            
                match prodType.Sensor with
                | IsCO2Sensor true -> List.map (fun (x,y) ->  x, -y) xy
                | _ -> xy )
            |> fmtCorrErr ( fun (TermoScalePt (_,_,t) ) -> t.What)

        | CorTermoScale ( n, ScaleEnd ) as corr -> 
            result {
                let! t = getValuesTermo p n ScaleEnd n.Termo
                let! var = getValuesTermo p n ScaleEnd n.Var1
                let! var0 = getValuesTermo p n ScaleBeg n.Var1
                return List.zip3 t var0 var}
            |> fmtCorrErr ( fun (TermoScalePt (_,_,t) ) -> t.What)
            |> Result.bind( fun xs ->
                let errs =
                    xs |> List.zip valuesListOf<TermoPt>
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
                    |> sprintf "при расчёте %s деление на ноль в точке %s" corr.What
                    |> Err )

        


    let doValuesGaussXY group xy =
        let groupCoefs = Correction.coefs group
        let groupCoefsSet = Set.ofList groupCoefs
        let strGroupCoefs = Seq.toStr ", " (Coef.order >> string) groupCoefs

        let x,y = List.toArray xy |> Array.unzip
        let result =  NumericMethod.GaussInterpolation.calculate(x,y) 
        let ff = Seq.toStr ", " string
        Logging.info "метод Гаусса X=%s Y=%s ==> %s=%s" (ff x) (ff y) strGroupCoefs (ff result)
        Array.toList result 
        

let compute group getPgsConc prodType = state {
    let! product = getState
    let groupCoefs = Correction.coefs group
    let groupCoefsSet = Set.ofList groupCoefs
    let strGroupCoefs = Seq.toStr ", " (Coef.order >> string) groupCoefs
    Logging.info "%s : расчёт коэффициентов %A, %s" (Product.what product) (Correction.what group) strGroupCoefs
    do!
        initKefsValues getPgsConc prodType
        |> List.filter(fst >> groupCoefsSet.Contains)
        |> Product.setKefs 
    let result = 
        match group with
        | CorPressSens -> calculatePressureSensCoefs product
        | _ ->
            getGaussXY product (apply2 getPgsConc) prodType group
            |> Result.map (doValuesGaussXY group)
    match result with
    | Err e -> 
        Logging.error "%s : %s" (Product.what product) e
    | Ok result ->
        do! result |> List.zipCuty 0m groupCoefs |> Product.setKefs   }

let getProductTermoErrorlimit sensor getPgsConc (n,gas,t)  product =
    let f = TestPt (n,gas,t) 
    match sensor with
    | IsCHSensor true ->
        match gas with
        | ScaleEdge ScaleBeg -> Some 5m
        | _ ->
            Product.getVar (f, n.Conc) product
            |> Option.map(fun conc20 -> conc20 * 0.15m |> abs  |> decimal )
    | _ ->
        (Product.getVar (f, n.Conc) product, Product.getVar (f, n.Termo) product) 
        |> Option.map2(fun(c,t) -> 
            let dt = t - 20m
            let linPt = ScalePt.toLinPt gas
            let pgsConc = getPgsConc (n,linPt)
            let maxc = sensor.ConcErrorlimit pgsConc
            0.5m * abs( maxc*dt ) / 10.0m )
    

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

    static member concError sensor getPgsConc (n,gas) product = 
        Product.getVar (TestPt(n,gas,TermoNorm), n.Conc) product 
        |> Option.map(fun conc ->                 
            let pgs = ScalePt.mapLin getPgsConc gas
            {   Value = conc
                Nominal = pgs
                Limit = Sensor.concErrorlimit sensor pgs  }  ) 


    static member termoError sensor getPgsConc ((n,gas,t) as pt) product = 
        (   Product.getVar (TestPt pt, n.Conc) product,
            Product.getVar (TestPt (n,gas,TermoNorm), n.Conc) product,
            getProductTermoErrorlimit sensor getPgsConc pt product )
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
    let getPgsConc = d.Pgs.TryFind >> Option.getWith 0m
    let productType = h.ProductType
    let product = createNewProduct 1 getPgsConc productType
    let products = [ product ]
    { h with ProductsSerials = [product.SerialNumber] }, { d with Products = products }

let createNewParty1( name, productType, count) : Party.Content = 
    let sensor1 = productType.Sensor
                
    let products = 
        [1..count] 
        |> List.map( fun n ->  createNewProduct n productType.GetPgsConc productType )

    
        
    {   Id = Product.createNewId()
        ProductsSerials = List.map Product.productSerial products
        Date=DateTime.Now 
        Name = name
        ProductType = productType }, 
            {   Products = products
                Pgs = productType.DefaultPgsConcMap
                Temperature = Map.empty
                Journal = Map.empty}
