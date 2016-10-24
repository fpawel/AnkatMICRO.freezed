namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat

type Party(partyHeader, partyData) =

    inherit ViewModel.Party1(partyHeader, partyData) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 1, ПГС1")>]    
    [<Description("Концентрация ПГС1, канал 1")>]
    member x.PGS0_0
        with get() = x.GetPgs(Sens1, Lin1)
        and set v = x.SetPgs ( (Sens1, Lin1), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 1, ПГС2")>]    
    [<Description("Концентрация ПГС2, канал 1")>]
    member x.PGS0_1
        with get() = x.GetPgs(Sens1, Lin2)
        and set v = x.SetPgs ( (Sens1, Lin2), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 1, ПГС3")>]    
    [<Description("Концентрация ПГС3, канал 1")>]
    member x.PGS0_2
        with get() = x.GetPgs(Sens1, Lin3)
        and set v = x.SetPgs ( (Sens1, Lin3), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 1, ПГС4")>]    
    [<Description("Концентрация ПГС4, канал 1")>]
    member x.PGS0_3
        with get() = x.GetPgs(Sens1, Lin4)
        and set v = x.SetPgs ( (Sens1, Lin4), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 2, ПГС1")>]    
    [<Description("Концентрация ПГС1, канал 2")>]
    member x.PGS1_0
        with get() = x.GetPgs(Sens2, Lin1)
        and set v = x.SetPgs ( (Sens2, Lin1), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 2, ПГС2")>]    
    [<Description("Концентрация ПГС2, канал 2")>]
    member x.PGS1_1
        with get() = x.GetPgs(Sens2, Lin2)
        and set v = x.SetPgs ( (Sens2, Lin2), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал 2, ПГС4")>]    
    [<Description("Концентрация ПГС4, канал 2")>]
    member x.PGS1_3
        with get() = x.GetPgs(Sens2, Lin4)
        and set v = x.SetPgs ( (Sens2, Lin4), v) 

    [<Category("Температура")>] 
    [<DisplayName("T-")>]    
    [<Description("Пониженная температура")>]
    member x.L 
        with get() = x.GetTermoTemperature TermoLow
        and set v = x.SetTermoTemperature (TermoLow,v) 

    [<Category("Температура")>] 
    [<DisplayName("НКУ")>]    
    [<Description("Нормальная температура")>]
    member x.N 
        with get() = x.GetTermoTemperature TermoNorm
        and set v = x.SetTermoTemperature (TermoNorm,v) 

    [<Category("Температура")>] 
    [<DisplayName("T+")>]    
    [<Description("Повышенная температура")>]
    member x.H 
        with get() = x.GetTermoTemperature TermoHigh
        and set v = x.SetTermoTemperature (TermoHigh,v) 
