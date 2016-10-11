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

    [<DisplayName("Продувка ПГС1, к.1")>]    
    [<Description("Продувка ПГС1, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens1_ScaleBeg 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleBeg;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleBeg;}) value  

    [<DisplayName("Продувка ПГС2, к.1")>]    
    [<Description("Продувка ПГС2, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens1_ScaleMid1 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleMid1;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleMid1;}) value  

    [<DisplayName("Продувка ПГС3, к.1")>]    
    [<Description("Продувка ПГС3, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens1_ScaleMid2 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleMid2;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleMid2;}) value  

    [<DisplayName("Продувка ПГС4, к.1")>]    
    [<Description("Продувка ПГС4, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens1_ScaleEnd 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleEnd;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens1;
           ScalePt = ScaleEnd;}) value  

    [<DisplayName("Продувка ПГС1, к.2")>]    
    [<Description("Продувка ПГС1, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens2_ScaleBeg 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleBeg;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleBeg;}) value  

    [<DisplayName("Продувка ПГС2, к.2")>]    
    [<Description("Продувка ПГС2, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens2_ScaleMid1 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleMid1;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleMid1;}) value  

    [<DisplayName("Продувка ПГС4, к.2")>]    
    [<Description("Продувка ПГС4, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.BlowDelay_Sens2_ScaleEnd 
        with get() = x.GetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleEnd;})
        and set value = x.SetDelay (BlowDelay {SensorIndex = Sens2;
           ScalePt = ScaleEnd;}) value  

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

    [<DisplayName("Калибровка ПГС1, к.1")>]    
    [<Description("Калибровка ПГС1, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_Sens1_ScaleBeg 
        with get() = x.GetDelay (AdjustDelay {SensorIndex = Sens1;
             ScalePt = ScaleBeg;})
        and set value = x.SetDelay (AdjustDelay {SensorIndex = Sens1;
             ScalePt = ScaleBeg;}) value  

    [<DisplayName("Калибровка ПГС4, к.1")>]    
    [<Description("Калибровка ПГС4, к.1, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_Sens1_ScaleEnd 
        with get() = x.GetDelay (AdjustDelay {SensorIndex = Sens1;
             ScalePt = ScaleEnd;})
        and set value = x.SetDelay (AdjustDelay {SensorIndex = Sens1;
             ScalePt = ScaleEnd;}) value  

    [<DisplayName("Калибровка ПГС1, к.2")>]    
    [<Description("Калибровка ПГС1, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_Sens2_ScaleBeg 
        with get() = x.GetDelay (AdjustDelay {SensorIndex = Sens2;
             ScalePt = ScaleBeg;})
        and set value = x.SetDelay (AdjustDelay {SensorIndex = Sens2;
             ScalePt = ScaleBeg;}) value  

    [<DisplayName("Калибровка ПГС4, к.2")>]    
    [<Description("Калибровка ПГС4, к.2, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.AdjustDelay_Sens2_ScaleEnd 
        with get() = x.GetDelay (AdjustDelay {SensorIndex = Sens2;
             ScalePt = ScaleEnd;})
        and set value = x.SetDelay (AdjustDelay {SensorIndex = Sens2;
             ScalePt = ScaleEnd;}) value  
