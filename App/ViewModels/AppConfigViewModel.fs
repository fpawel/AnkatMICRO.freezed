namespace Ankat.ViewModel

open System
open System.ComponentModel

open AppConfig

[<AbstractClass>]
type AppConfigViewModel() = 
    inherit ViewModelBase()

    [<Category("СОМ порты")>]    
    [<DisplayName("СОМ пневмоблок")>]
    [<Description("Имя СОМ порта, к которому подключен пневмоблок")>]
    [<TypeConverter (typeof<ComportConfig.ComPortNamesConverter>) >]
    member x.ComportPneumo
        with get() = config.ComportPneumo.PortName
        and set v = 
            if v <> config.ComportPneumo.PortName then
                config.ComportPneumo.PortName <- v
                x.RaisePropertyChanged "ComportPneumo"

    [<Category("СОМ порты")>]    
    [<DisplayName("СОМ термокамера")>]
    [<Description("Имя СОМ порта, к которому подключена термокамера")>]
    [<TypeConverter (typeof<ComportConfig.ComPortNamesConverter>) >]
    member x.ComportTermo
        with get() = config.ComportTermo.PortName
        and set v = 
            if v <> config.ComportTermo.PortName then
                config.ComportTermo.PortName <- v
                x.RaisePropertyChanged "ComportTermo"

    
    [<DisplayName("Используемые коэффициенты")>]
    [<Description("Диаипазоны порядковых номеров используемых коэффициентов")>]
    member x.VisibleCoefs 
        with get() = config.View.VisibleCoefs
        and set v = 
            if v <> config.View.VisibleCoefs then
                config.View.VisibleCoefs <- v
                x.RaisePropertyChanged "VisibleCoefs"

    [<DisplayName("Опрос")>]
    [<Description("Опрашиваемые параметры прибора")>]
    member val SelectPhysVars = SelectPhysVars()
    

    