namespace Ankat

open System

[<AutoOpen>]
module private Helpers =
    let inline unionCaseName1<'a> (x:'a) = 
        sprintf "%s.%A" (typeof<'a>.Name) x

type PressPt =
    | PressNorm
    | PressHigh
    member x.Name = PressPt.name x
    member x.Property = PressPt.property x

    member x.What = 
        match x with
        | PressNorm -> "740"
        | PressHigh -> "800"
    

    static member what (x:PressPt) = x.What
    static member values = [ PressNorm; PressHigh ] 
    static member name = unionCaseName1<PressPt> 
    static member property (x:PressPt) = FSharpValue.unionCaseName x


type TermoPt =
    | TermoNorm
    | TermoLow    
    | TermoHigh
    
    static member what = function
        | TermoLow -> "T-"
        | TermoNorm -> "НКУ"
        | TermoHigh -> "T+"
        
    static member dscr = function
        | TermoLow -> "Пониженная температура"
        | TermoNorm -> "Нормальная температура"
        | TermoHigh -> "Повышенная температура"
        

    static member values = [ TermoNorm; TermoLow;  TermoHigh  ] 
    static member name = unionCaseName1<TermoPt> 
    static member property (x:TermoPt) = FSharpValue.unionCaseName x
    static member defaultTermoTemperature = function
        | TermoLow -> -60m
        | TermoNorm -> 20m
        | TermoHigh -> 80m
        

    member x.Dscr = TermoPt.dscr x
    member x.What = TermoPt.what x
    member x.Name = TermoPt.name x
    member x.Property = TermoPt.property x

// точка шкалы концентрации
type ScalePt = 
    | ScaleBeg
    | ScaleMid1
    | ScaleMid2
    | ScaleEnd
    member x.What = ScalePt.what x
    member x.Name = ScalePt.name x
    member x.Property = ScalePt.property x
    static member what = function
        | ScaleBeg -> "ПГС1"
        | ScaleMid1 -> "ПГС2"
        | ScaleMid2 -> "ПГС3"
        | ScaleEnd -> "ПГС4"
    static member whatScale = function
        | ScaleBeg -> "начало шкалы"
        | ScaleMid1 -> "1-ая середина шкалы"
        | ScaleMid2 -> "2-ая середина шкалы"
        | ScaleEnd -> "конец шкалы"
    
    static member defaultBallonConc = function
        | ScaleBeg -> 0m
        | ScaleMid1 -> 50m
        | ScaleMid2 -> 70m
        | ScaleEnd -> 100m
    static member name = unionCaseName1<ScalePt> 
    static member property (x:ScalePt) = FSharpValue.unionCaseName x

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

    member x.Name = PhysVar.name x
    member x.Property = PhysVar.property x
    
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
    static member name = unionCaseName1<PhysVar> 
    static member property (x:PhysVar) = FSharpValue.unionCaseName x
    member x.Dscr = PhysVar.dscr x
    member x.What = PhysVar.what x
    
type Id = string

type Command =
    | CmdNorm1
    | CmdAdjustNull1
    | CmdAdjustScale1
    | CmdSetComponent1
    | CmdNorm2
    | CmdAdjustNull2
    | CmdAdjustScale2
    | CmdSetComponent2
    | CmdCorrectT
    | CmdSetAddr
    static member context = function
        | CmdNorm1         ->  8, "Нормировать каналы 1 ИКД"
        | CmdAdjustNull1   ->  1, "Коррекция нуля 1"
        | CmdAdjustScale1  ->  2, "Коррекция конца шкалы 1"
        | CmdSetComponent1 ->  16, "Установить тип газа 1"
        | CmdNorm2         ->  9, "Нормировать каналы 2 ИКД"
        | CmdAdjustNull2   ->  4, "Коррекция нуля 2"
        | CmdAdjustScale2  ->  5, "Коррекция конца шкалы 2"
        | CmdSetComponent2 ->  17, "Установить тип газа 2"
        | CmdCorrectT      ->  20, "Коррекция смещения датчика температуры"
        | CmdSetAddr       ->  7, "Установка адреса MODBUS"

    static member what = Command.context >> snd
    static member code = Command.context >> fst

    member x.What = Command.what x
    member x.Code = Command.code x
    
    static member values = FSharpType.unionCasesList<Command>


// индекс датчика измерения концентрации
type SensorIndex = 
    | Sens1
    | Sens2

    member x.Conc = SensorIndex.conc x
    member x.Termo = SensorIndex.termo x
    member x.Var1 = SensorIndex.var1 x
    member x.N = SensorIndex.n x
    member x.Name = SensorIndex.name x
    member x.Property = SensorIndex.property x

    member x.SensorOfProdTypeByIndex prodType =
        SensorIndex.sensorOfProdTypeByIndex prodType x

    member x.CmdNorm = SensorIndex.cmdNorm x

    member x.ScalePts = SensorIndex.scalePts x

    static member name = unionCaseName1<SensorIndex>
    static member property (x:SensorIndex) = FSharpValue.unionCaseName x

    static member conc = function
        | Sens1 -> CCh0
        | Sens2 -> CCh1

    static member termo = function
        | Sens1 -> TppCh0
        | Sens2 -> TppCh1

    static member var1 = function
        | Sens1 -> Var1Ch0
        | Sens2 -> Var1Ch1

    static member n = function
        | Sens1 -> 1
        | Sens2 -> 2

    static member prodTypeCoefs = function
        | Sens1 -> Pgs1_1, Pgs3_1, PREDEL_LO_1, PREDEL_HI_1, SHKALA_1, ED_IZMER_1, Gas_Type_1
        | Sens2  -> Pgs1_2, Pgs3_2, PREDEL_LO_2, PREDEL_HI_2, SHKALA_2, ED_IZMER_2, Gas_Type_2

    static member values = [Sens1; Sens2]

    static member coefsLin = function
        | Sens1 -> [CLin1_0; CLin1_1; CLin1_2; CLin1_3]
        | Sens2 -> [CLin2_0; CLin1_2; CLin2_2; CLin2_3]

    static member sensorOfProdTypeByIndex prodType sensorIndex = 
        match sensorIndex, prodType.Sensor2 with
        | Sens1, _ -> Some prodType.Sensor
        | Sens2, sensor -> sensor

    static member scalePts = function
        | Sens1 -> [ScaleBeg; ScaleMid1; ScaleMid2; ScaleEnd]
        | Sens2 -> [ScaleBeg; ScaleMid1; ScaleEnd]

    static member cmdNorm = function
        | Sens1 -> CmdNorm1
        | Sens2 -> CmdNorm2

    static member what = function
        | Sens1 -> "Канал 1"
        | Sens2 -> "Канал 2"

// точка шкалы датчика концентрации
type SScalePt = 
    {   SensorIndex : SensorIndex 
        ScalePt  : ScalePt }    

    member x.CoefsTermo = SScalePt.coefsTermo x
    member x.Property = SScalePt.property x
    member x.Name = SScalePt.name x
    member x.What = SScalePt.what x
    member x.CmdAdjust = SScalePt.cmdAdjust x
    

    static member new' s g = {SensorIndex = s; ScalePt = g}
    
    static member coefsTermo = function
        | { SensorIndex = Sens1; ScalePt = ScaleBeg } -> [KNull_T1_0; KNull_T1_1; KNull_T1_2]
        | { SensorIndex = Sens1; ScalePt = ScaleEnd } -> [KSens_T1_0; KSens_T1_1; KSens_T1_1]
        | { SensorIndex = Sens2; ScalePt = ScaleBeg } -> [KNull_T2_0; KNull_T2_1; KNull_T2_2]
        | { SensorIndex = Sens2; ScalePt = ScaleEnd } -> [KSens_T2_0; KSens_T2_1; KSens_T2_1]
        | x -> failwithf "SensorScalePt.coefsTermo %A" x


    static member what x =
        sprintf "%s/%d" x.ScalePt.What x.SensorIndex.N

    static member values_null_end = 
        [   for sensorIndex in SensorIndex.values do
                for gas in [ScaleBeg;ScaleEnd] do
                    yield SScalePt.new' sensorIndex gas ]

    static member name x = 
        sprintf "{SensorIndex = %s; ScalePt = %s}" x.SensorIndex.Name x.ScalePt.Name

    static member property n =
        sprintf "%A_%A" n.SensorIndex n.ScalePt

    static member cmdAdjust = function
        | { SensorIndex = Sens1; ScalePt = ScaleBeg } -> CmdAdjustNull1
        | { SensorIndex = Sens1; ScalePt = ScaleEnd } -> CmdAdjustScale1
        | { SensorIndex = Sens2; ScalePt = ScaleBeg } -> CmdAdjustNull2
        | { SensorIndex = Sens2; ScalePt = ScaleEnd } -> CmdAdjustScale2
        | x -> failwithf "SensorScalePt.cmdAdjust %A" x

[<AutoOpen>]
module private SensorScalePtHelper =
    type S = SScalePt
    let values =        
        List.append
            (Sens1.ScalePts |> List.map (SScalePt.new' Sens1))
            (Sens2.ScalePts |> List.map (SScalePt.new' Sens2))

    let valuesT = 
        [   for sensorIndex in SensorIndex.values do
                for gas in sensorIndex.ScalePts do
                    for t in TermoPt.values do
                        yield S.new' sensorIndex gas, t ]
    
    let pneumoBlockCodes = 
        List.zip values [1uy; 2uy; 3uy; 4uy; 1uy; 5uy; 6uy]
        |> Map.ofList

    let Beg1 = S.new' Sens1 ScaleBeg
    let Mid11 = S.new' Sens1 ScaleMid1
    let Mid21 = S.new' Sens1 ScaleMid2    
    let End1 = S.new' Sens1 ScaleEnd

    let Beg2 = S.new' Sens2 ScaleBeg
    let Mid2 = S.new' Sens2 ScaleMid1
    let End2 = S.new' Sens2 ScaleEnd

type SScalePt with 
    
    member x.PneumoBlockCode = SScalePt.pneumoBlockCode x

    static member values =
        SensorScalePtHelper.values

    static member valuesT =
        SensorScalePtHelper.valuesT

    static member pneumoBlockCode x = 
        try
            SensorScalePtHelper.pneumoBlockCodes |> Map.find x
        with _ ->
            failwithf "SensorScalePt.pneumoBlockCode %A" x

    static member  Beg1 = SensorScalePtHelper.Beg1
    static member  Mid11 = SensorScalePtHelper.Mid11
    static member  Mid21 = SensorScalePtHelper.Mid21
    static member  End1 = SensorScalePtHelper.End1

    static member  Beg2 = SensorScalePtHelper.Beg2
    static member  Mid2 = SensorScalePtHelper.Mid2
    static member  End2 = SensorScalePtHelper.End2

    


// канал измерения концентрации
type Channel = 
    {   ChannelSensor : Sensor
        ChannelIndex : SensorIndex }


type Correction = 
    | CorrectionLinScale of SensorIndex
    | CorrectionTermoScale of SScalePt
    | CorrectionTermoPress
    | CorrectionPressSens
    static member ctx = function
        | CorrectionLinScale chan -> 
            [chan.Conc],
                sprintf "LIN%d" chan.N, sprintf "Линеаризация ф-ии преобразов. к.%d" chan.N, 
                    SensorIndex.coefsLin chan,
                        chan.ScalePts, [TermoNorm], [PressNorm]
        
        | CorrectionTermoScale ({ScalePt = gas; SensorIndex = sens} as n)-> 
            let strDescr = 
                match gas with
                | ScaleBeg -> "на нулев. показ."
                | ScaleEnd -> "чувст."
                | xx -> failwithf "KefGroup.ctx TermoCoefs(_,%A)" xx

            let strScale = 
                match gas with
                | ScaleBeg -> "0"
                | ScaleEnd -> "K"
                | _ -> ""
            [sens.Termo; sens.Var1],
                sprintf "T%s%d" strScale sens.N, sprintf "Комп. вл. темп. на %s к. %d" strDescr sens.N, 
                    n.CoefsTermo,
                        [gas], TermoPt.values, [PressNorm]

        | CorrectionPressSens ->
            [TppCh0; VdatP],
                "PS", "Комп. влиян. давл. на чувст. по каналам",
                    [Coef_Pmmhg_0; Coef_Pmmhg_1],
                        [ScaleBeg], [TermoNorm], PressPt.values

        | CorrectionTermoPress ->
            [Sens1.Termo; VdatP],
                "PT", "Компенс. влиян. темп. на к. измер. давл.",
                    [KNull_TP_0; KNull_TP_1; KNull_TP_2],
                        [ScaleBeg], TermoPt.values, [PressNorm]   

    member x.What = Correction.what x
    member x.Dscr = Correction.dscr x

    member x.PhysVars = Correction.vars x
    member x.Gases = Correction.gases x
    member x.Temps = Correction.temps x
    member x.Press = Correction.press x
    member x.Coefs = Correction.coefs x

    static member vars = Correction.ctx >> (fun (x,_,_,_,_,_,_) -> x)
    static member what = Correction.ctx >> (fun (_,x,_,_,_,_,_) -> x)
    static member dscr = Correction.ctx >> (fun (_,_,x,_,_,_,_) -> x)
    static member coefs = Correction.ctx >> (fun (_,_,_,x,_,_,_) -> x)
    static member gases = Correction.ctx >> (fun (_,_,_,_,x,_,_) -> x)
    static member temps = Correction.ctx >> (fun (_,_,_,_,_,x,_) -> x)
    static member press = Correction.ctx >> (fun (_,_,_,_,_,_,x) -> x)

    static member values = 
        [   for chan in [Sens1; Sens2] do
                yield CorrectionLinScale chan
            for chan in [Sens1; Sens2] do
                for ptgas in [ScaleBeg; ScaleEnd] do
                    yield CorrectionTermoScale (SScalePt.new' chan ptgas)
            yield CorrectionTermoPress
            yield CorrectionPressSens ]

    static member name = 
        function
            | CorrectionTermoPress -> "CorrectionTermoPress"
            | CorrectionPressSens -> "CorrectionPressSens"
            | CorrectionLinScale sensInd -> sprintf "CorrectionLinScale(%s)" (SensorIndex.name sensInd)
            | CorrectionTermoScale n  -> 
                sprintf "CorrectionTermoScale(%s)" n.Name
        >> sprintf "Correction.%s"

    static member property = function
        | CorrectionTermoPress -> "CorrectionTermoPress"
        | CorrectionPressSens -> "CorrectionPressSens"
        | CorrectionLinScale sensInd -> sprintf "CorrectionLinScale_%s" sensInd.Property
        | CorrectionTermoScale n -> sprintf "CorrectionTermoScale_%s" n.Property

type ProductionPoint = 
    | Correction of Correction
    | TestConcErrors of SensorIndex

    member x.Property = ProductionPoint.property x
    member x.Gases = ProductionPoint.gases x
    member x.Temperatures = ProductionPoint.temeratures x
    member x.PhysVars = ProductionPoint.physVars x
    member x.Pressures = ProductionPoint.pressures x

    static member ofSensor sensor = function
        | TestConcErrors s -> s = sensor
        | Correction g ->     
            match g with
            | CorrectionLinScale s -> s = sensor
            | CorrectionTermoScale n -> n.SensorIndex = sensor
            | _ -> sensor = Sens1


    static member physVars = function
        | TestConcErrors s -> [ s.Conc; s.Termo ]
        | Correction g -> g.PhysVars

    static member pressures = function
        | TestConcErrors _ -> [ PressNorm ]
        | Correction g -> g.Press

    static member temeratures = function
        | TestConcErrors _ -> TermoPt.values
        | Correction g -> g.Temps

    static member gases = function
        | TestConcErrors s -> s.ScalePts
        | Correction g -> Correction.gases g

    static member what = function
        | Correction kg -> kg.Dscr, kg.What
        | TestConcErrors s -> sprintf "Проверка к.%d" s.N, sprintf "Проверка%d" s.N

    static member what1 = ProductionPoint.what >> fst 
    static member what2 = ProductionPoint.what >> snd

    static member name = function
        | TestConcErrors s-> sprintf "ProductionPoint.TestConcErrors(%s)" s.Name
        | Correction x -> sprintf "ProductionPoint.Correction(%s)" (Correction.name x)

    static member property = function
        | TestConcErrors n -> sprintf "TestConcErrors_%s" n.Property
        | Correction x -> sprintf "Correction_%s" (Correction.property x)

    static member values = 
        [   for kg in Correction.values do
                yield Correction kg
            yield TestConcErrors Sens1 
            yield TestConcErrors Sens2]

    member x.What1 = ProductionPoint.what1 x
    member x.What2 = ProductionPoint.what2 x



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

type Var = ProductionPoint * PhysVar * ScalePt * TermoPt * PressPt

module Vars = 
    let what ( (f,v,s,t,p) as x : Var ) = 
        sprintf "%s.%s.%s.%s.%s" (ProductionPoint.what1 f) (PhysVar.what v) (ScalePt.what s) (TermoPt.what t) (PressPt.what p)

    let property ( (f,v,s,t,p) as x : Var ) = 
        sprintf "Var_%s_%s_%s_%s_%s" (ProductionPoint.property f) (PhysVar.property v) (ScalePt.property s) (TermoPt.property t) (PressPt.property p)
            
    let vars = 
        [   for f in ProductionPoint.values do
                for var in f.PhysVars do
                        for gas in f.Gases do
                            for t in f.Temperatures do
                                for p in f.Pressures do
                                    yield f,var,gas,t,p ]

    let varsSet = Set.ofList vars 

    let tryGetSensorIndexOfConcVar =
        let xs = SensorIndex.values |> List.map( fun x -> x.Conc, x  ) |> Map.ofList
        xs.TryFind


    let tryGetSensorIndexOfTempVar =
        let xs = SensorIndex.values |> List.map( fun x -> x.Termo, x  ) |> Map.ofList
        xs.TryFind

    let (|SensorIndexOfConcVar|_|) = tryGetSensorIndexOfConcVar
    let (|SensorIndexOfTempVar|_|) = tryGetSensorIndexOfTempVar

    

type DelayContext = 
    | BlowDelay of SScalePt 
    | WarmDelay of TermoPt
    | AdjustDelay of SScalePt
    static member values = 
        [   yield! List.map BlowDelay SScalePt.values
            yield! List.map WarmDelay TermoPt.values
            yield! List.map AdjustDelay SScalePt.values_null_end  ]

    static member what = function
        | BlowDelay n -> sprintf "Продувка %s, к.%d" n.ScalePt.What n.SensorIndex.N
        | WarmDelay t -> sprintf "Прогрев %s" t.What
        | AdjustDelay n -> sprintf "Калибровка %s, к.%d" n.ScalePt.What n.SensorIndex.N
    member x.What = DelayContext.what x
    member x.Prop = DelayContext.prop x

    static member prop = function
        | BlowDelay n -> sprintf "BlowDelay_%s" n.Property
        | WarmDelay t -> sprintf "WarmDelay_%s" t.Property
        | AdjustDelay n -> sprintf "AdjustDelay_%s" n.Property


    

module Property = 
    let concError n = 
        sprintf "ConcError_%s" (SScalePt.property n)
    let termoError (n,termoPt) = 
        sprintf "TermoError_%s_%s" (SScalePt.property n) (TermoPt.property termoPt)
    

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
               
        do! setState { p with VarValue = m p.VarValue  } }

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

    static member termoErrorlimit channel pgs (gas,t) product =
        let concVar = channel.ChannelIndex.Conc
        let f = TestConcErrors channel.ChannelIndex
        if not channel.ChannelSensor.Gas.IsCH then         
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
        BallonConc : Map<SScalePt,decimal>
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
        {   Id = Product.createNewId()
            ProductsSerials = List.map Product.productSerial []
            Date=DateTime.Now 
            Name = ""
            ProductType = ProductType.first }, 
            {   Products = []
                BallonConc = Map.empty
                TermoTemperature = Map.empty
                PerformingJournal = Map.empty}

    let ballonConc k m =
        match Map.tryFind k m with
        | Some x -> x
        | _ -> ScalePt.defaultBallonConc k.ScalePt

