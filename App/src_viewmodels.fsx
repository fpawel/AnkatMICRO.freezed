#load "Utils/FsharpRuntime.fs"
#load "Utils/State.fs"
#load "Utils/StrUtils.fs"
#load "Utils/PathUtils.fs"
#load "Utils/DateTimeUtils.fs"
#load "Utils/Logging.fs"
#load "Utils/Utils.fs"
#load "Ankat/Coef.fs"
#load "Ankat/ProdType.fs"
#load "Ankat/Ankat.fs"

open System

open Ankat

let createSourcefile path (source : string []) = 
    System.IO.File.WriteAllLines (__SOURCE_DIRECTORY__ + "/" + path, source)

let fkef (k:Coef) = 
    printfn "| [<Coef(%d, %A)>]" k.Order k.Description
    printfn "  %A" k

let kefss() = 
    List.iter fkef Coef.coefs

let createSourceFile_ProductViewModel() = 
  [|  
    yield """namespace Ankat.ViewModel

open Ankat

type Product(p, getProdType, getPgs, partyId) =

    inherit ViewModel.Product1(p, getProdType, getPgs, partyId) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName"""

    for ((f,v,gas,t,p) as var) in Vars.vars do
        let k1 = 
            sprintf "(Feature.%s, PhysVar.%s, ScalePt.%s, TermoPt.%s, PressurePt.%s)" 
                (Feature.name f) (PhysVar.name v) (ScalePt.name gas) (TermoPt.name t)
                (PressurePt.name p)
        yield sprintf """
    member x.%s
        with get () = x.getVarUi %s
        and set value = x.setVarUi %s value"""  (Property.var var) k1 k1 
        
    for ((sensInd, gas) as v) in Vars.sensor_gas_vars do
        yield sprintf """
    member x.%s = x.GetConcError (SensorIndex.%s, ScalePt.%s) """  
                        (Property.concError v) 
                        (SensorIndex.name sensInd) 
                        (ScalePt.name gas) 
        
    for ((sensInd, gas,t) as v) in Vars.sensor_gas_t_vars do
        yield sprintf """
    member x.%s = x.GetTermoError (SensorIndex.%s, ScalePt.%s, TermoPt.%s) """  
                    (Property.termoError v ) 
                    (SensorIndex.name sensInd)
                    (ScalePt.name gas) 
                    (TermoPt.name t)  
    for var in PhysVar.values do
        let name = PhysVar.name var
        yield sprintf """
    member x.%s = x.getPhysVarValueUi PhysVar.%s """  
                    name
                    name |]

    |> createSourcefile "ViewModels/ProductViewModel.fs" 

let createSourceFile_PartyViewModel() = 
  [|  
    yield """namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat

type Party(partyHeader, partyData) =

    inherit ViewModel.Party1(partyHeader, partyData) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName"""

    
        
    for (sensInd, gas) as k in Sens1.ScalePts1 @ Sens2.ScalePts1 do
        let whatPgs = Vars.formatSensorScalePt k
        let whatScale = ScalePt.whatScale gas
        let name = ScalePt.name gas
        yield sprintf """
    [<Category("Концентрация ПГС")>] 
    [<DisplayName("%s")>]    
    [<Description("Концентрация %s, канал %d, %s")>]
    member x.%s
        with get() = x.GetPgs ScalePt.%s
        and set v = x.SetPgs (ScalePt.%s, v) """  whatPgs whatPgs sensInd.N whatScale name name name
        
    for t in TermoPt.values  do
        let what = TermoPt.what t
        let descr = TermoPt.dscr t
        let name = TermoPt.name t

        yield sprintf """
    [<Category("Температура")>] 
    [<DisplayName("%s")>]    
    [<Description("%s")>]
    member x.%s 
        with get() = x.GetTermoTemperature TermoPt.%s
        and set v = x.SetTermoTemperature (TermoPt.%s,v) """  
                what descr name name name  |]
    |> createSourcefile "ViewModels/PartyViewModel.fs" 

createSourceFile_ProductViewModel()
createSourceFile_PartyViewModel()



[|    yield """namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat
open Operations
open PartyWorks

open MyWinForms.Converters

type DelaysHelperViewModel() =
    inherit DelaysHelperViewModel1()
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName"""
      for ctx in DelayContext.values do
            
            yield sprintf """
    [<DisplayName("%s")>]    
    [<Description("%s, длительность час:мин:сек")>]
    [<TypeConverter(typeof<TimeSpanConverter>)>]
    member x.%s 
        with get() = x.GetDelay (%A)
        and set value = x.SetDelay (%A) value  """ ctx.What ctx.What ctx.Prop ctx ctx
  |]
|> createSourcefile "View/DelaysHelpViewModel.fs" 


