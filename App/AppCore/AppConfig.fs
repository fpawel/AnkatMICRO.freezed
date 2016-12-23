module AppConfig

open System
open System.IO

open System
open System.Collections.ObjectModel
open System.ComponentModel
open System.ComponentModel.DataAnnotations
open System.Drawing.Design

open MyWinForms.Converters

open Ankat

module View = 
    type Grid =  
        {   mutable ColWidths : int list
            mutable ColumnHeaderHeight : int }

    type Config =  
        {   mutable PartyId : string
            mutable Grids : Map<string,Grid>   
            mutable ScnDetailTextSplitterDistance : int  
            mutable SelectedCoefs : string 
            mutable VisibleCoefs : string
            mutable VisiblePhysVars : Set<Ankat.PhysVar> }

        static member create() =
            {   PartyId = ""
                Grids = Map.empty
                ScnDetailTextSplitterDistance = 0 
                SelectedCoefs = "0-150"
                VisibleCoefs = "0-150"
                VisiblePhysVars = Set.ofList [CCh0; CCh1; CCh2] }

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type Termochamber = 
    {   [<DisplayName("СОМ порт")>]
        [<Description("Параметры приёмопередачи СОМ порта, к которому подключена термокамера")>]
        mutable Comport : ComportConfig.Config

        [<DisplayName("Использовать термокамеру")>]
        [<Description("Использовать термокамеру при снятии для расчёта термокомпенсации")>]
        [<TypeConverter(typeof<MyWinForms.Converters.YesNoConverter>)>]
        mutable UseTermochamber : bool

        [<DisplayName("Погрешность уставки")>]
        [<Description("""Минимальная разница между показаниями и уставкой термокамеры, при которой температура считается установившейся \"С""")>]
        mutable TermoWarmError : decimal

        [<DisplayName("Таймаут уставки")>]
        [<Description("""Максимальная длительность уставки термокамеры, по истечении которой выполнение настройки будет прекращено с сообщением об ошибке""")>]
        mutable TermoWarmTimeOut : TimeSpan }
    override __.ToString() = ""

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type Pneumoblock = 
    {   [<DisplayName("СОМ порт")>]
        [<Description("Параметры приёмопередачи СОМ порта, к которому подключен пневмоблок")>]
        mutable Comport : ComportConfig.Config

        [<DisplayName("Использовать пневмоблок")>]
        [<Description("Использовать пневмоблок при автоматической настройке")>]
        [<TypeConverter(typeof<MyWinForms.Converters.YesNoConverter>)>]
        mutable UsePneumoblock : bool  }
    override __.ToString() = ""

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type Hardware =
    {   [<DisplayName("СОМ порты приборов")>]
        [<Description("Параметры приёмопередачи СОМ портов, к которому подключены настраиваемые приборы чеерз преобразователь интерфейсов RS232, RS485")>]
        mutable ComportProducts : ComportConfig.Config        
        
        [<Category("СОМ порты")>]    
        [<DisplayName("Термокамера")>]
        [<Description("Параметры приёмопередачи СОМ порта, к которому подключена термокамера")>]
        mutable Termochamber : Termochamber

        [<Category("СОМ порты")>]    
        [<DisplayName("Пневмоблок")>]
        [<Description("Параметры приёмопередачи СОМ порта, к которому подключен пневмоблок")>]
        mutable Pneumoblock : Pneumoblock }
    static member create() = {   
        Pneumoblock = 
            {   Comport = ComportConfig.Config.withDescr "пневмоблок"
                UsePneumoblock = true }
        Termochamber = 
            {   Comport = ComportConfig.Config.withDescr "термокамера"
                UseTermochamber = true
                TermoWarmTimeOut = TimeSpan.FromHours 4.
                TermoWarmError = 2m  }
                
        ComportProducts = ComportConfig.Config.withDescr "приборы" }
    override __.ToString() = ""

[<TypeConverter(typeof<ExpandableObjectConverter>)>]
type ApplicatioConfig = 
    {   mutable Hardware : Hardware
        
        View : View.Config  }

    static member create() = {   
        View = View.Config.create()
        Hardware = Hardware.create() }

let config, save = Json.Config.create "app.config.json" ApplicatioConfig.create

