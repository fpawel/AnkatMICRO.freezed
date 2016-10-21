module Ankat.PartyWorks

open System

open Thread2
open Ankat.Alchemy

open ViewModel.Operations

[<AutoOpen>]
module private Helpers = 
    type P = Product
    let party = AppContent.party
    let appCfg = AppConfig.config
    let viewCfg = appCfg.View


let checkedProducts() = 
    party.Products
    |> Seq.filter( fun p -> p.IsChecked )
let hasNotCheckedProduct() = checkedProducts() |> Seq.isEmpty 
let hasCheckedProduct() = not <| hasNotCheckedProduct()


let doWithProducts f = 
    checkedProducts() |> Seq.iter ( fun p ->       
        if isKeepRunning() && p.IsChecked then 
            f p ) 

type Ankat.ViewModel.Product1 with
    
    member x.WriteKef (kef,value) =         
        let value =
            match value, P.getKef kef x.Product with
            | Some value, _ -> Some value
            | _, Some value -> Some  value            
            | _ ->  None
        match value with
        | None -> 
            Logging.warn "%s, нет значения записываемого к-та %d, %s" x.What kef.Order kef.Description 
            Ok()
        | Some value -> 
            let r = x.WriteModbus( WriteKef kef, value )
            match r with
            | Ok () -> 
                Logging.info "%s.коэф.%d <- %M" x.What kef.Order value
                x.setKef kef (Some value)
            | Err err -> Logging.error "%s.коэф.%d <- %M : %s" x.What kef.Order value err
            r

    
    member x.WriteKefs kefsValues  = maybeErr {
        for kef,value in kefsValues do
            let! _ = x.WriteKef(kef,value)
            () }
            
    member x.WriteKefsInitValues() = 
        let t = party.getProductType()        
        Alchemy.initKefsValues party.GetPgs t
        |> List.map( fun (coef,value) -> coef, Some value )
        |> x.WriteKefs

    member x.ReadKefs kefs = maybeErr {
        for kef in kefs do
            let r = x.ReadModbus( ReadKef kef )
            match r with
            | Ok value -> 
                Logging.info "%s.коэф.%d = %M" x.What kef.Order value
                x.setKef kef (Some value) 
            | Err err ->
                Logging.error "%s.коэф.%d : %s" x.What kef.Order err 
            let! _ = r
            () }

    
    member x.Interrogate() = maybeErr {
        let xs = 
            let xs = AppConfig.config.View.VisiblePhysVars
            if Set.isEmpty xs then [Sens1.Conc] else
            Set.toList xs
        for var in xs do
            let _ = x.ReadModbus( ReadVar var)
            () }

    member private p.calculateTestConc channel n conc = 
        
        let concErrorlimit = channel.ChannelSensor.ConcErrorlimit conc
        let pgs = party.GetPgs n
        let d = abs(conc - pgs)
        Logging.write 
            (if d < concErrorlimit then Logging.Info else Logging.Error)
            "%s, проверка погрешности %s=%M - конц.=%M, погр.=%M, макс.погр.=%M" 
                p.What n.What pgs conc d concErrorlimit  
        


    member p.TestConc channel n = maybeErr{
        let concVar = SensorIndex.conc channel.ChannelIndex
        let! conc = p.ReadModbus( ReadVar concVar )
        p.setVar (TestConcErrors channel.ChannelIndex, concVar, n.ScalePt, TermoNorm,PressNorm) (Some conc)
        p.calculateTestConc channel n conc }

type Ankat.ViewModel.Party with
    member x.DoForEachProduct f = 
        let xs = x.Products |> Seq.filter(fun p -> p.IsChecked)
        if Seq.isEmpty xs then
            Err "приборы не отмечены"
        else
            for p in xs do 
                if isKeepRunning() && p.IsChecked then 
                    f p
            Ok ()

    member x.Interrogate() = Option.toResult <| maybeErr {
        let xs = x.Products |> Seq.filter(fun p -> p.IsChecked)
        if Seq.isEmpty xs then
            return "приборы не отмечены"
        else
            
            for p in xs do 
                if isKeepRunning() && p.IsChecked then                         
                    do! p.Interrogate() }

    member x.WriteModbus(cmd,value) = maybeErr{
        
        do! x.DoForEachProduct (fun p -> p.WriteModbus(SendCommand cmd,value) |> ignore  ) }

    member x.SetModbusAddrs() = maybeErr{
        do! x.DoForEachProduct (fun p -> p.SetModbusAddr() |> ignore  ) }

    member x.WriteKefs(kefs) = maybeErr{
        do! x.DoForEachProduct (fun p -> 
            p.WriteKefs kefs |> ignore ) }

    member x.ReadKefs(kefs) = maybeErr{
        do! x.DoForEachProduct (fun p -> 
            p.ReadKefs kefs |> ignore ) }

    member x.ComputeAndWriteKefGroup (kefGroup) = 
        x.DoForEachProduct(fun p -> 
            p.ComputeKefGroup kefGroup
            kefGroup.Coefs
            |> List.map (fun x -> x, None)
            |> p.WriteKefs  
            |> ignore )
   
module Delay = 
    let onStart = Ref.Initializable<_>(sprintf "Delay.start %s:%s" __LINE__ __SOURCE_FILE__ )
    let onStop = Ref.Initializable<_>(sprintf "Delay.stop %s:%s" __LINE__ __SOURCE_FILE__ )
    let onUpdate = Ref.Initializable<_>(sprintf "Delay.stop %s:%s" __LINE__ __SOURCE_FILE__ )

    let mutable private keepRunning = false

    let cancel() = keepRunning <- false

    let perform what gettime interrogate = maybeErr{
        onStart.Value what gettime
        keepRunning <- true
        let start'time = DateTime.Now
        while keepRunning && Thread2.isKeepRunning() && (DateTime.Now - start'time < gettime()) do
            onUpdate.Value start'time gettime
            if interrogate then
                do! party.Interrogate()
            else
                Threading.Thread.Sleep 10
        keepRunning <- false
        onStop.Value() }

module ModalMessage = 
    let onShow = Ref.Initializable<_>(sprintf "ModalMessage.onShow %s:%s" __LINE__ __SOURCE_FILE__ )
    let getIsVivisble = Ref.Initializable<_>(sprintf "ModalMessage.getIsVivisble %s:%s" __LINE__ __SOURCE_FILE__ )
    let onClose = Ref.Initializable<_>(sprintf "ModalMessage.onClose %s:%s" __LINE__ __SOURCE_FILE__ )
    
    let show (level:Logging.Level) (title:string) (text:string) = 
        onShow.Value title level text
        while Thread2.isKeepRunning() && getIsVivisble.Value() do
            Threading.Thread.Sleep 50
        onClose.Value()    

[<AutoOpen>]
module private Helpers1 = 
    
    let none _ = None
    let (<|>) what f = 
        Operation.CreateSingle (what, none) f 
    let (<-|->) (what,time,whatDelay) f = 
        Operation.CreateTimed (what, none) (Delay.create time whatDelay) f
    let (<||>) what xs =  Operation.CreateScenary ( what, none)  xs

    let computeGroup kefGroup = 
        sprintf "Расчёт %A" (Correction.what kefGroup) <|> fun () -> 
            party.ComputeKefGroup kefGroup
            None

    let writeGroup kefGroup = 
        sprintf "Запись к-тов группы %A" (Correction.what kefGroup) <|> fun () -> 
            kefGroup.Coefs
            |> List.map(fun x -> x, None)
            |> party.WriteKefs 

    let computeAndWriteGroup kefGroup = 
        (Correction.what kefGroup) <||> [   
            "Расчёт" <|> fun () -> 
                party.ComputeKefGroup kefGroup
                None
            "Запись" <|> fun () ->  
                kefGroup.Coefs
                |> List.map(fun x -> x, None)
                |> party.WriteKefs ]

    
    type Op = Operation

    let switchPneumo gas = maybeErr{
        let code, title, text = 
            match gas with
            | Some gas -> 
                let code = SScalePt.pneumoBlockCode gas
                byte code, "Продувка " + gas.What, "Подайте " + gas.What
            | _ -> 0uy, "Выключить пневмоблок", "Отключите газ"

        if appCfg.UsePneumoblock then
            do! Hardware.Pneumo.switch code
        else            
            ModalMessage.show Logging.Info  title text 
            if isKeepRunning() then
                match gas with
                | Some gas -> Logging.info "газ %s подан вручную" gas.What
                | _ -> Logging.info "пневмоблок закрыт вручную" }

    let blow minutes gas what = 
        let s = SScalePt.what gas
        let title, text = "Продувка " + s, "Подайте " + s

        (what, TimeSpan.FromMinutes (float minutes), BlowDelay gas ) <-|-> fun gettime -> maybeErr{        
            do! switchPneumo <| Some gas
            do! Delay.perform title gettime true }

    let warm t = maybeErr{    
        if appCfg.UsePneumoblock && Hardware.Pneumo.isOpened()  then
            do! switchPneumo None
        let value = party.GetTermoTemperature t
        Logging.info "Установка температуры %A %M\"C" (TermoPt.what t) value
        if not appCfg.UseTermochamber then             
            ModalMessage.show Logging.Info
                "Уставка термокамеры" (sprintf "Установите в термокамере температуру %M\"C" value) 
            if isKeepRunning() then
                Logging.info "температура %s установлена вручную" t.What
        else 
            do! Hardware.Warm.warm value Thread2.isKeepRunning party.Interrogate  }

    type SScalePt with
        static member adjust n = 
            let cmd = SScalePt.cmdAdjust n            
            let defaultDelayTime = TimeSpan.FromMinutes 3.
            (cmd.What, defaultDelayTime, AdjustDelay n)  <-|-> fun gettime -> maybeErr{
                let pgs = party.GetPgs n
                Logging.info  "%s, %M" n.What pgs
                do! switchPneumo <| Some n
                do! Delay.perform (sprintf  "Продувка перед калибровкой %A" n.What) gettime true
                do! party.WriteModbus( cmd, pgs ) 
                do! Delay.perform (sprintf  "Выдержка после калибровки %A" n.What) (fun () -> TimeSpan.FromSeconds 10.) true
                }

    type SensorIndex with
        member x.Adjust = SensorIndex.adjust x
        static member adjust sensInd =
            sprintf "К.%d" (SensorIndex.n sensInd) <||> [
                SScalePt.adjust <| SScalePt.new' sensInd ScaleBeg
                SScalePt.adjust <| SScalePt.new' sensInd ScaleEnd ]
        
    let isSens2() = party.getProductType().Sensor2.IsSome

    let blowAir() = 
        "Продувка воздухом" <||> [   
            blow 1 SScalePt.Beg1 "Продуть воздух"
            "Закрыть пневмоблок" <|> fun () -> switchPneumo None
        ]

    let formatIsSens2() = 
        if isSens2() then "к.1,2" else "к.1"

    let adjust() =
        "Калибровка" <||> [   
            yield Sens1.Adjust
            if isSens2() then
                yield Sens2.Adjust 
            yield blowAir()]

    let goNku = "Установка НКУ" <|> fun () -> warm TermoNorm

    let readVars (feat,gas,t,press) = 
        let what = 
            sprintf "Снятие %A, %s, %s, %s" (ProductionPoint.what2 feat ) 
                (ScalePt.what gas ) (TermoPt.what t) (PressPt.what press)
        what <|> fun () -> maybeErr{
            do! party.DoForEachProduct(fun p -> 
                maybeErr{
                    for physVar in feat.PhysVars do
                        let! readedValue = p.ReadModbus( ReadVar physVar)
                        p.setVar (feat,physVar,gas,t,press) (Some readedValue)
                        Logging.info "%s : %s = %s" p.What (PhysVar.what physVar) (Decimal.toStr6 readedValue) } 
                |> function 
                    | Some error -> Logging.error "%s" error
                    | _ -> () ) }


    let formatGasFeatsList (gas, feats) =
        let strFeats = feats |> Set.ofSeq |> Seq.toStr ", " ProductionPoint.what2
        let strGas = SScalePt.what gas
        sprintf "%s, %s" strFeats strGas

    let formatFeatsGasesList featsGasesList =        
        let gases, feats' = List.unzip featsGasesList
        let feats = List.concat feats' 
        let strGases = gases |> Set.ofSeq |> Seq.toStr ", " SScalePt.what
        let strFeats = feats |> Set.ofSeq |> Seq.toStr ", " ProductionPoint.what2
        sprintf "%s, %s" strFeats strGases


    let blowAndRead featsGasesList (temp,press) =

        sprintf "Снятие %s" (formatFeatsGasesList featsGasesList) <||>
            [   for gas, feats as xs in featsGasesList do
                    yield blow 3 gas ("Продувка " + gas.What)                    
                    for feat in feats do                        
                        yield readVars (feat, gas.ScalePt, temp, press) 
                yield blowAir() ]

    let warmAndRead featsGasesList temp  =

        sprintf "Снятие %s, %s" (TermoPt.what temp) (formatFeatsGasesList featsGasesList) <||> 
            [   yield sprintf "Установка %s" (TermoPt.what temp) <||> [
                    yield "Установка"  <|> fun () -> warm temp
                    yield ("Выдержка", TimeSpan.FromHours 1., WarmDelay temp) <-|-> fun gettime -> maybeErr{    
                        do! switchPneumo None    
                        do! Delay.perform ( sprintf "Выдержка термокамеры %A" (TermoPt.what temp) ) gettime true } ]        
                yield blowAndRead featsGasesList (temp,PressNorm)  ]
    
    let featGases1 s f = 
        SensorIndex.scalePts s 
        |> List.map( fun gas -> SScalePt.new' s gas, [f s] )

    let test() = 
        let xs =
            [   yield! featGases1 Sens1 TestConcErrors
                if isSens2() then
                    yield! featGases1 Sens2 TestConcErrors ]
        "Проверка" <||> [   
            adjust()        
            blowAndRead xs (TermoNorm,PressNorm )
            warmAndRead xs TermoLow 
            warmAndRead xs TermoHigh  ]
    let lin() =        
        let xs =
            [   yield! featGases1 Sens1 (CorrectionLinScale >> Correction)
                if isSens2() then
                    yield! featGases1 Sens2 (CorrectionLinScale >> Correction) ]

        "Линеаризация" <||> [
                yield blowAndRead xs (TermoNorm, PressNorm)
                yield computeAndWriteGroup <| CorrectionLinScale Sens1
                if isSens2() then
                    yield computeAndWriteGroup <| CorrectionLinScale Sens2 ]

    let norming() = 
        "Нормировка" <|> fun () -> maybeErr{
            do! party.WriteModbus( Sens1.CmdNorm, 100m ) 
            if isSens2() then
                do! party.WriteModbus( Sens2.CmdNorm, 100m )  }

    
let production() = 
    
    let termoFeatsGases =
        let mk gas n = 
            let x = SScalePt.new' n gas
            x, [Correction <| CorrectionTermoScale x]
        [   for gas in [ScaleBeg; ScaleEnd] do
                yield mk gas Sens1 
                if isSens2() then
                    yield mk gas Sens2 ]

    let gases = 
        [   yield SScalePt.Beg1
            yield SScalePt.End1
            if isSens2() then
                yield SScalePt.Beg2
                yield SScalePt.End2 ]
    "ИКД" + (if isSens2() then "2" else "1") <||> [
        "Установка к-тов исплнения" <|> fun () -> 
            party.DoForEachProduct (fun p -> 
                p.WriteKefsInitValues()
                |> ignore ) 
            |> Result.someErr
                
                
        goNku
        blowAir()
        norming()
        adjust()
        lin()
        "Термокомпенсация"  <||> [
            warmAndRead termoFeatsGases TermoLow 
            warmAndRead termoFeatsGases TermoHigh 
            warmAndRead termoFeatsGases TermoNorm  
            "Ввод" <||> ( gases |> List.map (CorrectionTermoScale >> computeAndWriteGroup) )  ]
        test() ]


module Works =
    let all() = Op.MapReduce Some (production()) 

    let blow() = 
        all() |> List.choose ( function 
            | (Op.Timed (_, ({DelayContext = BlowDelay gas} as delay),_) as op) -> 
                Some (op,delay,gas)
            | _ -> None)

    let warm = 
        all() |> List.choose ( function 
            | (Op.Timed (_, ({DelayContext = WarmDelay t} as delay),_) as op) -> 
                Some (op,delay,t)
            | _ -> None)



[<AutoOpen>]
module private Helpers3 =
    let ( -->> ) s f =
        s <|> f
        |> Thread2.run DontNeedStopHardware

    
let runInterrogate() = "Опрос" -->> fun () -> maybeErr{ 
    while Thread2.isKeepRunning() do
        do! party.Interrogate() }


let setAddr() = "Установка адреса 1" -->> party.SetModbusAddrs

let sendCommand (cmd,value as x) = 
    sprintf "%s <- %M" (Command.what cmd) value -->> fun () -> 
        party.WriteModbus x


module Pneumoblock =
    let clapans = [   
        SScalePt.Beg1
        SScalePt.Mid11
        SScalePt.Mid21
        SScalePt.End1
        SScalePt.Mid2
        SScalePt.End2 ]

    let switch gas = 
        
        SScalePt.what gas -->> fun () -> 
            Hardware.Pneumo.switch gas.PneumoBlockCode |> Result.someErr
    let close() = 
        "Выкл." -->> fun () ->
            Hardware.Pneumo.switch 0uy |> Result.someErr

module Kefs = 
    
    let private run s f = 
        s -->> fun () ->
            let x = appCfg.View
            let kefs = 
                Set.intersect 
                    (IntRanges.parseSet x.SelectedCoefs)
                    (IntRanges.parseSet x.VisibleCoefs)
                |> Set.map Coef.tryGetByOrder  
                |> Set.toList
                |> List.choose id
                
            if kefs.IsEmpty then Some "не выбрано ни одного коэффициента" else f kefs

    let write() = run "Запись к-тов" ( List.map(fun x -> x, None) >> party.WriteKefs  )
    let read() = run "Считывание к-тов"  party.ReadKefs

module TermoChamber =
    let private (-->>) s f = 
        s -->> fun () ->
            f () |> Result.someErr

    let start() = "Старт" -->> Hardware.Termo.start
    let stop() = "Стоп" -->> Hardware.Termo.stop
    let setSetpoint value = "Уставка" -->> fun () -> 
        Hardware.Termo.setSetpoint value
    let read () = "Замер" -->> fun () -> 
        let r = Hardware.Termo.read ()
        Logging.write (Logging.resultToLevel r) "%A" r
        Result.map (fun _ -> () ) r