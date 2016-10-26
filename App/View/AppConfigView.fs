namespace Ankat.View

open System
open System.ComponentModel

open AppConfig
open Ankat
open Pneumo

[<AutoOpen>]
module private Helpers =
    let party = AppContent.party

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type PgsConfigView() =

    [<DisplayName("ПГС1")>]    
    [<Description("ПГС1, начало шкалы, концентрация ")>]
    member x.PgsGas0
        with get() = party.GetPgs(Gas1)
        and set v = party.SetPgs ( (Gas1), v) 

    [<DisplayName("1-ПГС2")>]    
    [<Description("ПГС2, середина шкалы канала 1, концентрация ")>]
    member x.PgsGas1
        with get() = party.GetPgs(S1Gas2)
        and set v = party.SetPgs ( (S1Gas2), v) 

    [<DisplayName("CO₂-ПГС2")>]    
    [<Description("ПГС2, доп.середина шкалы канала 1 CO₂, концентрация ")>]
    member x.PgsGas2
        with get() = party.GetPgs(S1Gas2CO2)
        and set v = party.SetPgs ( (S1Gas2CO2), v) 

    [<DisplayName("1-ПГС3")>]    
    [<Description("ПГС3, конец шкалы канала 1, концентрация ")>]
    member x.PgsGas3
        with get() = party.GetPgs(S1Gas3)
        and set v = party.SetPgs ( (S1Gas3), v) 

    [<DisplayName("2-ПГС2")>]    
    [<Description("ПГС2, середина шкалы канала 2, концентрация ")>]
    member x.PgsGas4
        with get() = party.GetPgs(S2Gas2)
        and set v = party.SetPgs ( (S2Gas2), v) 

    [<DisplayName("2-ПГС3")>]    
    [<Description("ПГС3, конец шкалы канала 1, концентрация ")>]
    member x.PgsGas5
        with get() = party.GetPgs(S2Gas3)
        and set v = party.SetPgs ( (S2Gas3), v) 

    override __.ToString() = ""

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type TemperatureConfigView() =

    [<DisplayName("T-")>]    
    [<Description("Пониженная температура")>]
    member x.L 
        with get() = party.GetTermoTemperature TermoLow
        and set v = party.SetTermoTemperature (TermoLow,v) 

    [<DisplayName("НКУ")>]    
    [<Description("Нормальная температура")>]
    member x.N 
        with get() = party.GetTermoTemperature TermoNorm
        and set v = party.SetTermoTemperature (TermoNorm,v) 

    [<DisplayName("T+")>]    
    [<Description("Повышенная температура")>]
    member x.H 
        with get() = party.GetTermoTemperature TermoHigh
        and set v = party.SetTermoTemperature (TermoHigh,v) 

    override __.ToString() = ""

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type PartyConfigView() =
    [<DisplayName("Исполнение")>]    
    [<Description("Исполнение приборов партии")>]
    [<TypeConverter (typeof<PartyProductsDialogs.ProductTypesConverter>) >]
    member x.ProductType 
        with get() = party.ProductType
        and set v = 
            party.ProductType <- v

            Thread2.scenary.Set <| PartyWorks.production() 
            Scenary.updateGridViewBinding()            
            match TabPages.getSelected() with
            | MainWindow.TabsheetChart -> 
                TabPages.TabChart.update()           
            | _ -> ()
            Products.updatePhysVarsGridColsVisibility() 
            
    [<DisplayName("Наименование")>]    
    [<Description("Наименование партии")>]
    member x.Name 
        with get() = party.Name
        and set v = 
            party.Name <- v

    [<DisplayName("Концентрация ПГС")>]
    member val  Pgs = PgsConfigView() with get,set

    [<DisplayName("Температура")>]
    [<Description("Значения температур уставки термокамеры в температурных точках термокомпенсации приборов")>]
    member val  Temperature = TemperatureConfigView() with get,set

    override __.ToString() = ""


type AppConfigView() = 
    
    [<DisplayName("Партия")>]    
    member val  Party = PartyConfigView() with get,set

    [<DisplayName("СОМ пневмоблок")>]
    [<Description("Имя СОМ порта, к которому подключен пневмоблок")>]
    [<TypeConverter (typeof<ComportConfig.ComPortNamesConverter>) >]
    member x.ComportPneumo
        with get() = config.Hardware.Pneumoblock.Comport.PortName
        and set v = 
            if v <> config.Hardware.Pneumoblock.Comport.PortName then
                config.Hardware.Pneumoblock.Comport.PortName <- v
                
    [<DisplayName("СОМ термокамера")>]
    [<Description("Имя СОМ порта, к которому подключена термокамера")>]
    [<TypeConverter (typeof<ComportConfig.ComPortNamesConverter>) >]
    member x.ComportTermo
        with get() = config.Hardware.Termochamber.Comport.PortName
        and set v = 
            if v <> config.Hardware.Termochamber.Comport.PortName then
                config.Hardware.Termochamber.Comport.PortName <- v


    [<DisplayName("Параметры апаратной части")>]
    [<Description("Параметры пневмоблока, термокамеры и настройки СОМ портов")>]
    member x.Hardware
        with get() = config.Hardware
        and set v = 
            config.Hardware <- v

    [<DisplayName("Используемые коэффициенты")>]
    [<Description("Диаипазоны порядковых номеров используемых коэффициентов")>]
    member x.VisibleCoefs 
        with get() = config.View.VisibleCoefs
        and set v = 
            if v <> config.View.VisibleCoefs then
                config.View.VisibleCoefs <- v

    override __.ToString() = ""

    



    

    