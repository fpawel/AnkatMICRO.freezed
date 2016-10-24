namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat
open Operations
open PartyWorks

open MyWinForms.Converters

type DelaysHelperViewModel() =
    inherit DelaysHelperViewModel1()
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    [<DisplayName("Продувка ПГС1")>]    
    [<Description("Продувка ПГС1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_0_0 
        with get() = x.GetDelay (BlowDelay (Sens1, Lin1))
        and set value = x.SetDelay (BlowDelay (Sens1, Lin1)) value  

    [<DisplayName("Продувка ПГС2, канал 1")>]    
    [<Description("Продувка ПГС2, канал 1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_0_1 
        with get() = x.GetDelay (BlowDelay (Sens1, Lin2))
        and set value = x.SetDelay (BlowDelay (Sens1, Lin2)) value  

    [<DisplayName("Продувка ПГС2, CO₂")>]    
    [<Description("Продувка ПГС2, CO₂, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_0_2 
        with get() = x.GetDelay (BlowDelay (Sens1, Lin3))
        and set value = x.SetDelay (BlowDelay (Sens1, Lin3)) value  

    [<DisplayName("Продувка ПГС3, канал 1")>]    
    [<Description("Продувка ПГС3, канал 1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_0_3 
        with get() = x.GetDelay (BlowDelay (Sens1, Lin4))
        and set value = x.SetDelay (BlowDelay (Sens1, Lin4)) value  

    [<DisplayName("Продувка ПГС1")>]    
    [<Description("Продувка ПГС1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_1_0 
        with get() = x.GetDelay (BlowDelay (Sens2, Lin1))
        and set value = x.SetDelay (BlowDelay (Sens2, Lin1)) value  

    [<DisplayName("Продувка ПГС2, канал 2")>]    
    [<Description("Продувка ПГС2, канал 2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_1_1 
        with get() = x.GetDelay (BlowDelay (Sens2, Lin2))
        and set value = x.SetDelay (BlowDelay (Sens2, Lin2)) value  

    [<DisplayName("Продувка ПГС3, канал 2")>]    
    [<Description("Продувка ПГС3, канал 2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_1_3 
        with get() = x.GetDelay (BlowDelay (Sens2, Lin4))
        and set value = x.SetDelay (BlowDelay (Sens2, Lin4)) value  

    [<DisplayName("Прогрев T-")>]    
    [<Description("Прогрев T-, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_L 
        with get() = x.GetDelay (WarmDelay TermoLow)
        and set value = x.SetDelay (WarmDelay TermoLow) value  

    [<DisplayName("Прогрев НКУ")>]    
    [<Description("Прогрев НКУ, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_N 
        with get() = x.GetDelay (WarmDelay TermoNorm)
        and set value = x.SetDelay (WarmDelay TermoNorm) value  

    [<DisplayName("Прогрев T+")>]    
    [<Description("Прогрев T+, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_H 
        with get() = x.GetDelay (WarmDelay TermoHigh)
        and set value = x.SetDelay (WarmDelay TermoHigh) value  

    [<DisplayName("Калибровка ПГС1")>]    
    [<Description("Калибровка ПГС1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleBeg 
        with get() = x.GetDelay (AdjustDelay ScaleBeg)
        and set value = x.SetDelay (AdjustDelay ScaleBeg) value  

    [<DisplayName("Калибровка ПГС3")>]    
    [<Description("Калибровка ПГС3, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleEnd 
        with get() = x.GetDelay (AdjustDelay ScaleEnd)
        and set value = x.SetDelay (AdjustDelay ScaleEnd) value  
