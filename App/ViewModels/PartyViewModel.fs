namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat
open Pneumo

type Party(partyHeader, partyData) =

    inherit ViewModel.Party1(partyHeader, partyData) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("ПГС1")>]    
    [<Description("ПГС1, начало шкалы, концентрация ")>]
    member x.PgsGas0
        with get() = x.GetPgs(Gas1)
        and set v = x.SetPgs ( (Gas1), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("1-ПГС2")>]    
    [<Description("ПГС2, середина шкалы канала 1, концентрация ")>]
    member x.PgsGas1
        with get() = x.GetPgs(S1Gas2)
        and set v = x.SetPgs ( (S1Gas2), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("CO₂-ПГС2")>]    
    [<Description("ПГС2, доп.середина шкалы канала 1 CO₂, концентрация ")>]
    member x.PgsGas2
        with get() = x.GetPgs(S1Gas2CO2)
        and set v = x.SetPgs ( (S1Gas2CO2), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("1-ПГС3")>]    
    [<Description("ПГС3, конец шкалы канала 1, концентрация ")>]
    member x.PgsGas3
        with get() = x.GetPgs(S1Gas3)
        and set v = x.SetPgs ( (S1Gas3), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("2-ПГС2")>]    
    [<Description("ПГС2, середина шкалы канала 2, концентрация ")>]
    member x.PgsGas4
        with get() = x.GetPgs(S2Gas2)
        and set v = x.SetPgs ( (S2Gas2), v) 

    [<Category("Концентрация ПГС")>] 
    [<DisplayName("2-ПГС3")>]    
    [<Description("ПГС3, конец шкалы канала 1, концентрация ")>]
    member x.PgsGas5
        with get() = x.GetPgs(S2Gas3)
        and set v = x.SetPgs ( (S2Gas3), v) 

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
