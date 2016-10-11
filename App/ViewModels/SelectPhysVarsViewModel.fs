namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat

type InterrogateConverter() =
    inherit MyWinForms.Converters.BooleanTypeConverter("Опрашивать", "Не опрашивать")

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type SelectPhysVars() = 
    let cfg = AppConfig.config.View 

    [<DisplayName("CCh0")>]
    [<Description("Концентрация - канал 1 (электрохимия 1)")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.CCh0 
        with get() =
            Set.contains CCh0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) CCh0 cfg.VisiblePhysVars            
            

    [<DisplayName("CCh1")>]
    [<Description("Концентрация - канал 2 (электрохимия 2/оптика 1)")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.CCh1 
        with get() =
            Set.contains CCh1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) CCh1 cfg.VisiblePhysVars            
            

    [<DisplayName("CCh2")>]
    [<Description("Концентрация - канал 3 (оптика 1/оптика 2)")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.CCh2 
        with get() =
            Set.contains CCh2 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) CCh2 cfg.VisiblePhysVars            
            

    [<DisplayName("PkPa")>]
    [<Description("Давление, кПа")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.PkPa 
        with get() =
            Set.contains PkPa cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) PkPa cfg.VisiblePhysVars            
            

    [<DisplayName("Pmm")>]
    [<Description("Давление, мм. рт. ст")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Pmm 
        with get() =
            Set.contains Pmm cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Pmm cfg.VisiblePhysVars            
            

    [<DisplayName("Tmcu")>]
    [<Description("Температура микроконтроллера, град.С")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Tmcu 
        with get() =
            Set.contains Tmcu cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Tmcu cfg.VisiblePhysVars            
            

    [<DisplayName("Vbat")>]
    [<Description("Напряжение аккумуляторной батареи, В")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Vbat 
        with get() =
            Set.contains Vbat cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Vbat cfg.VisiblePhysVars            
            

    [<DisplayName("Vref")>]
    [<Description("Опорное напряжение для электрохимии, В")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Vref 
        with get() =
            Set.contains Vref cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Vref cfg.VisiblePhysVars            
            

    [<DisplayName("Vmcu")>]
    [<Description("Напряжение питания микроконтроллера, В")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Vmcu 
        with get() =
            Set.contains Vmcu cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Vmcu cfg.VisiblePhysVars            
            

    [<DisplayName("VdatP")>]
    [<Description("Напряжение на выходе датчика давления, В")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.VdatP 
        with get() =
            Set.contains VdatP cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) VdatP cfg.VisiblePhysVars            
            

    [<DisplayName("CoutCh0")>]
    [<Description("Концентрация - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.CoutCh0 
        with get() =
            Set.contains CoutCh0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) CoutCh0 cfg.VisiblePhysVars            
            

    [<DisplayName("TppCh0")>]
    [<Description("Температура пироприемника - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.TppCh0 
        with get() =
            Set.contains TppCh0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) TppCh0 cfg.VisiblePhysVars            
            

    [<DisplayName("ILOn0")>]
    [<Description("Лампа ВКЛ - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.ILOn0 
        with get() =
            Set.contains ILOn0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) ILOn0 cfg.VisiblePhysVars            
            

    [<DisplayName("ILOff0")>]
    [<Description("Лампа ВЫКЛ - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.ILOff0 
        with get() =
            Set.contains ILOff0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) ILOff0 cfg.VisiblePhysVars            
            

    [<DisplayName("Uw_Ch0")>]
    [<Description("Значение исходного сигнала в рабочем канале (АЦП) - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Uw_Ch0 
        with get() =
            Set.contains Uw_Ch0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Uw_Ch0 cfg.VisiblePhysVars            
            

    [<DisplayName("Ur_Ch0")>]
    [<Description("Значение исходного сигнала в опорном канале (АЦП) - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Ur_Ch0 
        with get() =
            Set.contains Ur_Ch0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Ur_Ch0 cfg.VisiblePhysVars            
            

    [<DisplayName("WORK0")>]
    [<Description("Значение нормализованного сигнала в рабочем канале (АЦП) - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.WORK0 
        with get() =
            Set.contains WORK0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) WORK0 cfg.VisiblePhysVars            
            

    [<DisplayName("REF0")>]
    [<Description("Значение нормализованного сигнала в опроном канале (АЦП) - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.REF0 
        with get() =
            Set.contains REF0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) REF0 cfg.VisiblePhysVars            
            

    [<DisplayName("Var1Ch0")>]
    [<Description("Значение дифференциального сигнала - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var1Ch0 
        with get() =
            Set.contains Var1Ch0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var1Ch0 cfg.VisiblePhysVars            
            

    [<DisplayName("Var2Ch0")>]
    [<Description("Значение дифференциального сигнала с поправкой по нулю от температуры - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var2Ch0 
        with get() =
            Set.contains Var2Ch0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var2Ch0 cfg.VisiblePhysVars            
            

    [<DisplayName("Var3Ch0")>]
    [<Description("Значение дифференциального сигнала с поправкой по чувствительности от температуры - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var3Ch0 
        with get() =
            Set.contains Var3Ch0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var3Ch0 cfg.VisiblePhysVars            
            

    [<DisplayName("FppCh0")>]
    [<Description("Частота преобразования АЦП - первый канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.FppCh0 
        with get() =
            Set.contains FppCh0 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) FppCh0 cfg.VisiblePhysVars            
            

    [<DisplayName("CoutCh1")>]
    [<Description("Концентрация - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.CoutCh1 
        with get() =
            Set.contains CoutCh1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) CoutCh1 cfg.VisiblePhysVars            
            

    [<DisplayName("TppCh1")>]
    [<Description("Температура пироприемника - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.TppCh1 
        with get() =
            Set.contains TppCh1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) TppCh1 cfg.VisiblePhysVars            
            

    [<DisplayName("ILOn1")>]
    [<Description("Лампа ВКЛ - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.ILOn1 
        with get() =
            Set.contains ILOn1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) ILOn1 cfg.VisiblePhysVars            
            

    [<DisplayName("ILOff1")>]
    [<Description("Лампа ВЫКЛ - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.ILOff1 
        with get() =
            Set.contains ILOff1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) ILOff1 cfg.VisiblePhysVars            
            

    [<DisplayName("Uw_Ch1")>]
    [<Description("Значение исходного сигнала в рабочем канале (АЦП) - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Uw_Ch1 
        with get() =
            Set.contains Uw_Ch1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Uw_Ch1 cfg.VisiblePhysVars            
            

    [<DisplayName("Ur_Ch1")>]
    [<Description("Значение исходного сигнала в опорном канале (АЦП) - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Ur_Ch1 
        with get() =
            Set.contains Ur_Ch1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Ur_Ch1 cfg.VisiblePhysVars            
            

    [<DisplayName("WORK1")>]
    [<Description("Значение нормализованного сигнала в рабочем канале (АЦП) - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.WORK1 
        with get() =
            Set.contains WORK1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) WORK1 cfg.VisiblePhysVars            
            

    [<DisplayName("REF1")>]
    [<Description("Значение нормализованного сигнала в опроном канале (АЦП) - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.REF1 
        with get() =
            Set.contains REF1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) REF1 cfg.VisiblePhysVars            
            

    [<DisplayName("Var1Ch1")>]
    [<Description("Значение дифференциального сигнала - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var1Ch1 
        with get() =
            Set.contains Var1Ch1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var1Ch1 cfg.VisiblePhysVars            
            

    [<DisplayName("Var2Ch1")>]
    [<Description("Значение дифференциального сигнала с поправкой по нулю от температуры - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var2Ch1 
        with get() =
            Set.contains Var2Ch1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var2Ch1 cfg.VisiblePhysVars            
            

    [<DisplayName("Var3Ch1")>]
    [<Description("Значение дифференциального сигнала с поправкой по чувствительности от температуры - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.Var3Ch1 
        with get() =
            Set.contains Var3Ch1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) Var3Ch1 cfg.VisiblePhysVars            
            

    [<DisplayName("FppCh1")>]
    [<Description("Частота преобразования АЦП - второй канал оптики")>]
    [<TypeConverter (typeof<InterrogateConverter>) >]
    member x.FppCh1 
        with get() =
            Set.contains FppCh1 cfg.VisiblePhysVars 
        and set value =
            cfg.VisiblePhysVars <- 
                (if value then Set.add else Set.remove) FppCh1 cfg.VisiblePhysVars            
            

    override x.ToString() = cfg.VisiblePhysVars |> Seq.toStr ", " PhysVar.what
