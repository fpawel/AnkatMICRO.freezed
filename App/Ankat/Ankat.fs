namespace Ankat

open System


type PressurePt =
    | Pnorm
    | Phigh
    member x.What = 
        match x with
        | Pnorm -> "P740"
        | Phigh -> "P800"
    

    static member what (x:PressurePt) = x.What
    static member values = [ Pnorm; Phigh ] 


type TermoPt =
    | TermoNorm
    | TermoLow    
    | TermoHigh
    | Termo90
    static member what = function
        | TermoLow -> "T-"
        | TermoNorm -> "НКУ"
        | TermoHigh -> "T+"
        | Termo90 -> "+90"
    static member dscr = function
        | TermoLow -> "Пониженная температура"
        | TermoNorm -> "Нормальная температура"
        | TermoHigh -> "Повышенная температура"
        | Termo90 -> "Температура +90\"C"

    static member values = [ TermoNorm; TermoLow;  TermoHigh; Termo90 ] 
    static member name (x:TermoPt) = FSharpValue.unionCaseName x
    static member defaultTermoTemperature = function
        | TermoLow -> -60m
        | TermoNorm -> 20m
        | TermoHigh -> 80m
        | Termo90 -> 90m

    member x.Dscr = TermoPt.dscr x
    member x.What = TermoPt.what x

type ScalePt = 
    | ScaleBeg
    | ScaleMid
    | ScaleEnd
    member x.What = ScalePt.what x
    member x.Code = ScalePt.code x |> byte
    static member values = FSharpType.unionCasesList<ScalePt>
    static member what = function
        | ScaleBeg -> "0"
        | ScaleMid -> "C"
        | ScaleEnd -> "K"
    static member whatScale = function
        | ScaleBeg -> "начало шкалы"
        | ScaleMid -> "середина шкалы"
        | ScaleEnd -> "конец шкалы"
    static member code = function
        | ScaleBeg -> 1
        | ScaleMid -> 3
        | ScaleEnd -> 4
    static member defaultBallonConc = function
        | ScaleBeg -> 0m
        | ScaleMid -> 50m
        | ScaleEnd -> 100m
    static member name (x:ScalePt) = FSharpValue.unionCaseName x

type PhysVar =
    | CCh0 
    | CCh1 
    | CCh2 
    | PkPa 
    | Pmm 
    | Tmcu 
    | Vbat 
    | Vref 
    | Vmcu 
    | VdatP 
    | CoutCh0 
    | TppCh0 
    | ILOn0 
    | ILOff0 
    | Uw_Ch0 
    | Ur_Ch0 
    | WORK0 
    | REF0 
    | Var1Ch0 
    | Var2Ch0 
    | Var3Ch0 
    | FppCh0 
    | CoutCh1 
    | TppCh1 
    | ILOn1 
    | ILOff1 
    | Uw_Ch1 
    | Ur_Ch1 
    | WORK1 
    | REF1 
    | Var1Ch1 
    | Var2Ch1 
    | Var3Ch1 
    | FppCh1 


    static member context = function
        
        | CCh0 ->      0, "Концентрация - канал 1 (электрохимия 1)"
        | CCh1 ->      2, "Концентрация - канал 2 (электрохимия 2/оптика 1)"
        | CCh2 ->      4, "Концентрация - канал 3 (оптика 1/оптика 2)"
        | PkPa ->      6, "Давление, кПа"
        | Pmm ->       8, "Давление, мм. рт. ст"
        | Tmcu ->      10, "Температура микроконтроллера, град.С"
        | Vbat ->      12, "Напряжение аккумуляторной батареи, В"
        | Vref ->      14, "Опорное напряжение для электрохимии, В"
        | Vmcu ->      16, "Напряжение питания микроконтроллера, В"
        | VdatP ->     18, "Напряжение на выходе датчика давления, В"
        | CoutCh0 ->   640, "Концентрация - первый канал оптики"
        | TppCh0 ->    642, "Температура пироприемника - первый канал оптики"
        | ILOn0 ->     644, "Лампа ВКЛ - первый канал оптики"
        | ILOff0 ->    646, "Лампа ВЫКЛ - первый канал оптики"
        | Uw_Ch0 ->    648, "Значение исходного сигнала в рабочем канале (АЦП) - первый канал оптики"
        | Ur_Ch0 ->    650, "Значение исходного сигнала в опорном канале (АЦП) - первый канал оптики"
        | WORK0 ->     652, "Значение нормализованного сигнала в рабочем канале (АЦП) - первый канал оптики"
        | REF0 ->      654, "Значение нормализованного сигнала в опроном канале (АЦП) - первый канал оптики"
        | Var1Ch0 ->   656, "Значение дифференциального сигнала - первый канал оптики"
        | Var2Ch0 ->   658, "Значение дифференциального сигнала с поправкой по нулю от температуры - первый канал оптики"
        | Var3Ch0 ->   660, "Значение дифференциального сигнала с поправкой по чувствительности от температуры - первый канал оптики"
        | FppCh0 ->    662, "Частота преобразования АЦП - первый канал оптики"
        | CoutCh1 ->   672, "Концентрация - второй канал оптики"
        | TppCh1 ->    674, "Температура пироприемника - второй канал оптики"
        | ILOn1 ->     676, "Лампа ВКЛ - второй канал оптики"
        | ILOff1 ->    678, "Лампа ВЫКЛ - второй канал оптики"
        | Uw_Ch1 ->    680, "Значение исходного сигнала в рабочем канале (АЦП) - второй канал оптики"
        | Ur_Ch1 ->    682, "Значение исходного сигнала в опорном канале (АЦП) - второй канал оптики"
        | WORK1 ->     684, "Значение нормализованного сигнала в рабочем канале (АЦП) - второй канал оптики"
        | REF1 ->      686, "Значение нормализованного сигнала в опроном канале (АЦП) - второй канал оптики"
        | Var1Ch1 ->   688, "Значение дифференциального сигнала - второй канал оптики"
        | Var2Ch1 ->   690, "Значение дифференциального сигнала с поправкой по нулю от температуры - второй канал оптики"
        | Var3Ch1 ->   692, "Значение дифференциального сигнала с поправкой по чувствительности от температуры - второй канал оптики"
        | FppCh1 ->    694, "Частота преобразования АЦП - второй канал оптики"



    static member code = PhysVar.context >> fst
    static member what (x:PhysVar)= 
        FSharpValue.unionCaseName x
    static member dscr = PhysVar.context >> snd 
    static member values = FSharpType.unionCasesList<PhysVar>
    static member name (x:PhysVar) = FSharpValue.unionCaseName x
    member x.Dscr = PhysVar.dscr x
    member x.What = PhysVar.what x
    
type Id = string

type Channel = 
    | Chan1
    | Chan2

    member x.Conc = Channel.conc x
    member x.Termo = Channel.termo x
    member x.Var1 = Channel.var1 x
    member x.N = Channel.n x
    
    static member conc = function
        | Chan1 -> CCh0
        | Chan2 -> CCh1

    static member termo = function
        | Chan1 -> TppCh0
        | Chan2 -> TppCh1

    static member var1 = function
        | Chan1 -> Var1Ch0
        | Chan2 -> Var1Ch1

    static member n = function
        | Chan1 -> 1
        | Chan2 -> 2

    static member pgsCoef = function
        | Chan1, ScaleBeg -> Some Pgs1_1
        | Chan2, ScaleBeg  -> Some Pgs1_2
        | Chan1, ScaleEnd -> Some Pgs3_1
        | Chan2, ScaleEnd -> Some Pgs3_2
        | _ -> None

    static member values = [Chan1; Chan2]

type KefGroup = 
    | KefLin of Channel
    | KefTermo of Channel * ScalePt
    | KefTermoPressure
    | KefPressureSens
    static member ctx = function
        | KefLin chan -> 
            [chan.Conc],
                sprintf "LIN%d" chan.N, sprintf "Линеаризация ф-ии преобразов. к.%d" chan.N, 
                    [CLin1_0; CLin1_1; CLin1_2; CLin1_3],
                        ScalePt.values, [TermoNorm], [Pnorm]
        
        | KefTermo (chan,scalePt) -> 
            let s = 
                match scalePt with
                | ScaleBeg -> "на нулев. показ."
                | ScaleMid -> failwith "KefGroup.ctx KefTermo(_,ScaleMid)"
                | ScaleEnd -> "чувст."
            [chan.Termo; chan.Var1],
                sprintf "T%s%d" scalePt.What chan.N, sprintf "Комп. вл. темп. на %s к. %d" s chan.N, 
                    [KNull_T1_0; KNull_T1_1; KNull_T1_2],
                        [ScaleBeg], TermoPt.values, [Pnorm]

        | KefPressureSens ->
            [TppCh0; VdatP],
                "PS", "Комп. влиян. давл. на чувст. по каналам",
                    [Coef_Pmmhg_0; Coef_Pmmhg_1],
                        [ScaleEnd], TermoPt.values, PressurePt.values

        | KefTermoPressure ->
            [Pmm; VdatP],
                "PT", "Компенс. влиян. темп. на к. измер. давл.",
                    [KNull_TP_0; KNull_TP_1; KNull_TP_2],
                        [ScaleBeg], TermoPt.values, PressurePt.values   

    member x.What = KefGroup.what x
    member x.Dscr = KefGroup.dscr x

    member x.Vars = KefGroup.vars x
    member x.Gases = KefGroup.gases x
    member x.Temps = KefGroup.temps x
    member x.Press = KefGroup.press x

    static member vars = KefGroup.ctx >> (fun (x,_,_,_,_,_,_) -> x)
    static member what = KefGroup.ctx >> (fun (_,x,_,_,_,_,_) -> x)
    static member dscr = KefGroup.ctx >> (fun (_,_,x,_,_,_,_) -> x)
    static member kefs = KefGroup.ctx >> (fun (_,_,_,x,_,_,_) -> x)
    static member gases = KefGroup.ctx >> (fun (_,_,_,_,x,_,_) -> x)
    static member temps = KefGroup.ctx >> (fun (_,_,_,_,_,x,_) -> x)
    static member press = KefGroup.ctx >> (fun (_,_,_,_,_,_,x) -> x)

    static member values = 
        [   for chan in [Chan1; Chan2] do
                yield KefLin chan
            for chan in [Chan1; Chan2] do
                for ptgas in [ScaleBeg; ScaleEnd] do
                    yield KefTermo (chan,ptgas)
            yield KefTermoPressure
            yield KefPressureSens ]



type Feature = 
    | FeatureKefGroup of KefGroup
    | Test
    
    static member what = function
        | FeatureKefGroup kg -> kg.Dscr, kg.What
        | Test -> "Проверка","main"

    static member what1 = Feature.what >> fst 
    static member what2 = Feature.what >> snd
    static member name (x:Feature) = FSharpValue.unionCaseName x
    static member values = 
        [   for kg in KefGroup.values do
                yield FeatureKefGroup kg
            yield Test ]

    member x.What1 = Feature.what1 x


type Command =
    | AdjustBeg
    | AdjustEnd
    | Nommalize
    | ResetAddy
    static member context = function
        | AdjustBeg -> 1, "Корректировка нуля"
        | AdjustEnd -> 2, "Корректировка чувствительности"
        | Nommalize -> 8, "Нормировка"
        | ResetAddy -> 7, "Установка адреса"
    static member what = Command.context >> snd
    static member code = Command.context >> fst

    member x.What = Command.what x
    member x.Code = Command.code x
    
    static member values = FSharpType.unionCasesList<Command>

type WriteContext =
    | WriteKef of Coef
    | SendCommand of Command
    static member what = function
        | WriteKef kef -> sprintf "запись коеф.%d" kef.Order
        | SendCommand cmd -> sprintf "отправка команды %A"  <| Command.what cmd
    static member code = function
        | WriteKef kef -> Coef.writeCmd kef
        | SendCommand cmd -> Command.code cmd

type ReadContext =
    | ReadKef of Coef
    | ReadVar of PhysVar
    static member what = function
        | ReadKef kef -> sprintf "коеф.%d" kef.Order
        | ReadVar var -> PhysVar.what var

    static member code = function
        | ReadKef kef -> Coef.readReg kef
        | ReadVar var -> PhysVar.code var

type Var = Feature * PhysVar * ScalePt * TermoPt * PressurePt

type DelayContext = 
    | BlowDelay of ScalePt 
    | WarmDelay of TermoPt
    | TexprogonDelay
    | AdjustDelay of bool
    static member values = 
        [   yield! List.map BlowDelay ScalePt.values
            yield! List.map WarmDelay TermoPt.values
            yield TexprogonDelay
            yield AdjustDelay false
            yield AdjustDelay true ]

    static member what = function
        | BlowDelay gas -> sprintf "Продувка %s" gas.What
        | WarmDelay t -> sprintf "Прогрев %s" t.What
        | TexprogonDelay -> "Выдержка, техпрогон"
        | AdjustDelay false -> "Продувка ПГС1, калибровка"
        | AdjustDelay true -> "Продувка ПГС4, калибровка"
    member x.What = DelayContext.what x
    member x.Prop = DelayContext.prop x

    static member prop = function
        | BlowDelay gas -> FSharpValue.unionCaseName gas 
        | WarmDelay t -> FSharpValue.unionCaseName t 
        | TexprogonDelay -> FSharpValue.unionCaseName TexprogonDelay 
        | AdjustDelay false -> "AdjustDelay_0" 
        | AdjustDelay true -> "AdjustDelay_1" 


module Vars = 
    let what ( (f,v,s,t,p) as x : Var ) = 
        sprintf "%s.%s.%s.%s.%s" (Feature.what1 f) (PhysVar.what v) (ScalePt.what s) (TermoPt.what t) (PressurePt.what p)

    let name ( (f,v,s,t,p) as x : Var ) = 
        sprintf "%s_%s_%s_%s_%s" (Feature.name f) (PhysVar.name v) (ScalePt.name s) (TermoPt.name t) (PressurePt.what p)

    let gas_t_vars = 
        [   for gas in ScalePt.values do
                for t in TermoPt.values @ [TermoPt.Termo90] do
                    yield gas,t ]

    let vars = 
        [   for kg in KefGroup.values do
                for var in kg.Vars do
                    for gas in kg.Gases do
                        for t in kg.Temps do
                            for p in kg.Press do
                                yield (FeatureKefGroup kg), var,gas,t,p
            for chan in Channel.values do
                for gas in ScalePt.values do
                    for t in TermoPt.values do
                        yield Test, chan.Conc, gas, t, Pnorm  
                        yield Test, chan.Termo, gas, t, Pnorm  ]
        |> List.sortBy( fun (feat,var,gas,t,p) -> var, feat, t, gas, p)

    

module Property = 
    let concError scalePt = sprintf "ConcError_%s" (ScalePt.name scalePt)
    let tex1Error scalePt = sprintf "Tex1Error_%s" (ScalePt.name scalePt)
    let tex2Error scalePt = sprintf "Tex2Error_%s" (ScalePt.name scalePt)
    let retNkuError scalePt = sprintf "RetNkuError_%s" (ScalePt.name scalePt)
    let termoError (scalePt,termoPt) = sprintf "TermoError_%s_%s" (ScalePt.name scalePt) (TermoPt.name termoPt)
    let var var = sprintf "Var_%s" (Vars.name var)
//    let ctx = function
//        | ProdVarCtx v -> var v
//        | ProdKefCtx k -> kef k

type ProductSerial = 
    {   SerialNumber : uint16
        ProdMonthYear : (byte * byte) option }

type Product = 
    {   Id : Id
        IsChecked : bool        
        ProductSerial : ProductSerial
        Addr : byte
        VarValue : Map<Var, decimal> 
        CoefValue : Map<Coef, decimal>  }

    member x.What = Product.what x
    static member id x = x.Id
    static member productSerial x = x.ProductSerial
    static member createNewId() = String.getUniqueKey 12
    static member what x = sprintf "№%d.#%d" x.ProductSerial.SerialNumber x.Addr 

    static member getVar k p =p.VarValue.TryFind k 

    static member setVar k v =  state{                
        let! p = getState 
        let m = 
            match v with
            | None -> Map.remove k
            | Some v -> Map.add k v
               
        do! setState { p with VarValue = m p.VarValue   } }


    static member getKef k p =p.CoefValue.TryFind k 

    static member setKef k v =  state{                
        let! p = getState 
        let m = 
            match v with
            | None -> Map.remove k
            | Some v -> Map.add k v
               
        do! setState { p with CoefValue = m p.CoefValue   } }

    static member setVars varsValues =  state{ 
        for var,value in varsValues do
            do! Product.setVar var (Some value)    }

    static member setKefs kefsVals = state{            
        for kef,value in kefsVals do
            do! Product.setKef kef (Some value)  }

    static member createNew addy = 
        let now = DateTime.Now
        {   Id = Product.createNewId()
            ProductSerial =
                {   SerialNumber = 0us
                    ProdMonthYear = None }
            Addr = addy
            IsChecked = true
            VarValue = Map.empty 
            CoefValue = Map.empty }

    static member tryParseSerailMonthYear s =
        let m = Text.RegularExpressions.Regex.Match(s, @"(\d\d)\s*[\./\s]\s*(\d+)")
        if not m.Success then None else
        let y = Int32.Parse m.Groups.[2].Value - 2000
        let mn = Byte.Parse m.Groups.[1].Value
        if y >= 16 && mn > 0uy && mn < 13uy then 
            Some ( mn, byte y )
        else None

    static member formatSerailMonthYear (m:byte, y:byte) =
        let sm = if m<10uy then sprintf "0%d" m else m.ToString()
        sprintf "%s.%d" sm (2000 + int y)

type LoggingRecord = DateTime * Logging.Level * string

type PerformingOperation =
    {   RunStart : DateTime option 
        RunEnd : DateTime option
        LoggingRecords : LoggingRecord list }
    static member createNew() = 
        {   RunStart = None
            RunEnd = None
            LoggingRecords = [] }
        

type PerformingJournal = Map<int, PerformingOperation >

module Party =
    type Head = 
        {   Id : Id
            Date : DateTime
            ProductType : ProductType
            Name : string
            ProductsSerials : ProductSerial list   }
        static member id x = x.Id 
    type Data = {
        Products : Product list
        BallonConc : Map<ScalePt,decimal>
        TermoTemperature : Map<TermoPt,decimal>
        PerformingJournal : PerformingJournal }

    type Content = Head * Data

    let getNewValidAddy addrs = 
        let rec loop n = 
            if addrs |> Seq.exists( (=) n ) then
                if n=127uy then 1uy else loop (n+1uy)
            else n
        loop 1uy

    let createNewEmpty() : Content =         
        //let products = [ Product.createNew 1uy 1 A00 ]
        let t = A00
        {   Id = Product.createNewId()
            ProductsSerials = List.map Product.productSerial []
            Date=DateTime.Now 
            Name = ""
            ProductType = A00 }, 
            {   Products = []
                BallonConc = Map.empty
                TermoTemperature = Map.empty
                PerformingJournal = Map.empty}

    

    let ballonConc gas m =
        match Map.tryFind gas m with
        | Some x -> x
        | _ -> ScalePt.defaultBallonConc gas

