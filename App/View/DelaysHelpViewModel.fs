namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat
open Pneumo
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
    member x.BlowDelay_Gas0 
        with get() = x.GetDelay (BlowDelay Gas1)
        and set value = x.SetDelay (BlowDelay Gas1) value  

    [<DisplayName("Продувка 1-ПГС2")>]    
    [<Description("Продувка 1-ПГС2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Gas1 
        with get() = x.GetDelay (BlowDelay S1Gas2)
        and set value = x.SetDelay (BlowDelay S1Gas2) value  

    [<DisplayName("Продувка CO₂-ПГС2")>]    
    [<Description("Продувка CO₂-ПГС2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Gas2 
        with get() = x.GetDelay (BlowDelay S1Gas2CO2)
        and set value = x.SetDelay (BlowDelay S1Gas2CO2) value  

    [<DisplayName("Продувка 1-ПГС3")>]    
    [<Description("Продувка 1-ПГС3, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Gas3 
        with get() = x.GetDelay (BlowDelay S1Gas3)
        and set value = x.SetDelay (BlowDelay S1Gas3) value  

    [<DisplayName("Продувка 2-ПГС2")>]    
    [<Description("Продувка 2-ПГС2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Gas4 
        with get() = x.GetDelay (BlowDelay S2Gas2)
        and set value = x.SetDelay (BlowDelay S2Gas2) value  

    [<DisplayName("Продувка 2-ПГС3")>]    
    [<Description("Продувка 2-ПГС3, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Gas5 
        with get() = x.GetDelay (BlowDelay S2Gas3)
        and set value = x.SetDelay (BlowDelay S2Gas3) value  

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
