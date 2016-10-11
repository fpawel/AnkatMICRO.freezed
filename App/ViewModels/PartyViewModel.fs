namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat

type Party(partyHeader, partyData) =

    inherit ViewModel.Party1(partyHeader, partyData) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС0/к2")>]    
    [<Description("Концентрация ПГС0/к2, канал 1, начало шкалы")>]
    member x.ScaleBeg
        with get() = x.GetPgs ScalePt.ScaleBeg
        and set v = x.SetPgs (ScalePt.ScaleBeg, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС1/к2")>]    
    [<Description("Концентрация ПГС1/к2, канал 1, 1-ая середина шкалы")>]
    member x.ScaleMid1
        with get() = x.GetPgs ScalePt.ScaleMid1
        and set v = x.SetPgs (ScalePt.ScaleMid1, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС2/к2")>]    
    [<Description("Концентрация ПГС2/к2, канал 1, 2-ая середина шкалы")>]
    member x.ScaleMid2
        with get() = x.GetPgs ScalePt.ScaleMid2
        and set v = x.SetPgs (ScalePt.ScaleMid2, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС4/к2")>]    
    [<Description("Концентрация ПГС4/к2, канал 1, конец шкалы")>]
    member x.ScaleEnd
        with get() = x.GetPgs ScalePt.ScaleEnd
        and set v = x.SetPgs (ScalePt.ScaleEnd, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС0/к3")>]    
    [<Description("Концентрация ПГС0/к3, канал 2, начало шкалы")>]
    member x.ScaleBeg
        with get() = x.GetPgs ScalePt.ScaleBeg
        and set v = x.SetPgs (ScalePt.ScaleBeg, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС1/к3")>]    
    [<Description("Концентрация ПГС1/к3, канал 2, 1-ая середина шкалы")>]
    member x.ScaleMid1
        with get() = x.GetPgs ScalePt.ScaleMid1
        and set v = x.SetPgs (ScalePt.ScaleMid1, v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС4/к3")>]    
    [<Description("Концентрация ПГС4/к3, канал 2, конец шкалы")>]
    member x.ScaleEnd
        with get() = x.GetPgs ScalePt.ScaleEnd
        and set v = x.SetPgs (ScalePt.ScaleEnd, v) 

    [<Category("Температура")>] 
    [<DisplayName("НКУ")>]    
    [<Description("Нормальная температура")>]
    member x.TermoNorm 
        with get() = x.GetTermoTemperature TermoPt.TermoNorm
        and set v = x.SetTermoTemperature (TermoPt.TermoNorm,v) 

    [<Category("Температура")>] 
    [<DisplayName("T-")>]    
    [<Description("Пониженная температура")>]
    member x.TermoLow 
        with get() = x.GetTermoTemperature TermoPt.TermoLow
        and set v = x.SetTermoTemperature (TermoPt.TermoLow,v) 

    [<Category("Температура")>] 
    [<DisplayName("T+")>]    
    [<Description("Повышенная температура")>]
    member x.TermoHigh 
        with get() = x.GetTermoTemperature TermoPt.TermoHigh
        and set v = x.SetTermoTemperature (TermoPt.TermoHigh,v) 
