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

    [<DisplayName("Продувка ПГС0, к.1")>]    
    [<Description("Продувка ПГС0, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleBeg_1 
        with get() = x.GetDelay (BlowDelay (Sens1, ScaleBeg))
        and set value = x.SetDelay (BlowDelay (Sens1, ScaleBeg)) value  

    [<DisplayName("Продувка ПГС1, к.1")>]    
    [<Description("Продувка ПГС1, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleMid1_1 
        with get() = x.GetDelay (BlowDelay (Sens1, ScaleMid1))
        and set value = x.SetDelay (BlowDelay (Sens1, ScaleMid1)) value  

    [<DisplayName("Продувка ПГС2, к.1")>]    
    [<Description("Продувка ПГС2, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleMid2_1 
        with get() = x.GetDelay (BlowDelay (Sens1, ScaleMid2))
        and set value = x.SetDelay (BlowDelay (Sens1, ScaleMid2)) value  

    [<DisplayName("Продувка ПГС4, к.1")>]    
    [<Description("Продувка ПГС4, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleEnd_1 
        with get() = x.GetDelay (BlowDelay (Sens1, ScaleEnd))
        and set value = x.SetDelay (BlowDelay (Sens1, ScaleEnd)) value  

    [<DisplayName("Продувка ПГС0, к.2")>]    
    [<Description("Продувка ПГС0, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleBeg_2 
        with get() = x.GetDelay (BlowDelay (Sens2, ScaleBeg))
        and set value = x.SetDelay (BlowDelay (Sens2, ScaleBeg)) value  

    [<DisplayName("Продувка ПГС1, к.2")>]    
    [<Description("Продувка ПГС1, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleMid1_2 
        with get() = x.GetDelay (BlowDelay (Sens2, ScaleMid1))
        and set value = x.SetDelay (BlowDelay (Sens2, ScaleMid1)) value  

    [<DisplayName("Продувка ПГС4, к.2")>]    
    [<Description("Продувка ПГС4, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_ScaleEnd_2 
        with get() = x.GetDelay (BlowDelay (Sens2, ScaleEnd))
        and set value = x.SetDelay (BlowDelay (Sens2, ScaleEnd)) value  

    [<DisplayName("Прогрев НКУ")>]    
    [<Description("Прогрев НКУ, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_TermoNorm 
        with get() = x.GetDelay (WarmDelay TermoNorm)
        and set value = x.SetDelay (WarmDelay TermoNorm) value  

    [<DisplayName("Прогрев T-")>]    
    [<Description("Прогрев T-, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_TermoLow 
        with get() = x.GetDelay (WarmDelay TermoLow)
        and set value = x.SetDelay (WarmDelay TermoLow) value  

    [<DisplayName("Прогрев T+")>]    
    [<Description("Прогрев T+, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.WarmDelay_TermoHigh 
        with get() = x.GetDelay (WarmDelay TermoHigh)
        and set value = x.SetDelay (WarmDelay TermoHigh) value  

    [<DisplayName("Калибровка ПГС0, к.1")>]    
    [<Description("Калибровка ПГС0, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleBeg_1 
        with get() = x.GetDelay (AdjustDelay (Sens1, ScaleBeg))
        and set value = x.SetDelay (AdjustDelay (Sens1, ScaleBeg)) value  

    [<DisplayName("Калибровка ПГС4, к.1")>]    
    [<Description("Калибровка ПГС4, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleEnd_1 
        with get() = x.GetDelay (AdjustDelay (Sens1, ScaleEnd))
        and set value = x.SetDelay (AdjustDelay (Sens1, ScaleEnd)) value  

    [<DisplayName("Калибровка ПГС0, к.2")>]    
    [<Description("Калибровка ПГС0, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleBeg_2 
        with get() = x.GetDelay (AdjustDelay (Sens2, ScaleBeg))
        and set value = x.SetDelay (AdjustDelay (Sens2, ScaleBeg)) value  

    [<DisplayName("Калибровка ПГС4, к.2")>]    
    [<Description("Калибровка ПГС4, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_ScaleEnd_2 
        with get() = x.GetDelay (AdjustDelay (Sens2, ScaleEnd))
        and set value = x.SetDelay (AdjustDelay (Sens2, ScaleEnd)) value  
