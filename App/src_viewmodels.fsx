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
            sprintf "(%s, %s, %s, %s, %s)" 
                (ProductionPoint.name f) (PhysVar.name v) (ScalePt.name gas) (TermoPt.name t) (PressPt.name p)
        yield sprintf """
    member x.%s
        with get () = x.getVarUi %s
        and set value = x.setVarUi %s value"""  (Vars.property var) k1 k1 
        
    for n in SScalePt.values do
        yield sprintf """
    member x.%s = x.GetConcError %s """  (Property.concError n) n.Name
        
    for n,t in SScalePt.valuesT do
        yield sprintf """
    member x.%s = x.GetTermoError (%s, %s) """  (Property.termoError (n,t) ) n.Name t.Name
    for var in PhysVar.values do
        
        yield sprintf """
    member x.%s = x.getPhysVarValueUi %s """ var.Property var.Name |]

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

    
        
    for n in SScalePt.values do
        let whatPgs = SScalePt.what n
        let whatScale = ScalePt.whatScale n.ScalePt
        
        yield sprintf """
    [<Category("Концентрация ПГС")>] 
    [<DisplayName("%s")>]    
    [<Description("Концентрация %s, канал %d, %s")>]
    member x.%s
        with get() = x.GetPgs %s
        and set v = x.SetPgs (%s, v) """  whatPgs whatPgs n.SensorIndex.N whatScale n.Property n.Name n.Name
        
    for t in TermoPt.values  do
        let what = TermoPt.what t
        let descr = TermoPt.dscr t
        

        yield sprintf """
    [<Category("Температура")>] 
    [<DisplayName("%s")>]    
    [<Description("%s")>]
    member x.%s 
        with get() = x.GetTermoTemperature %s
        and set v = x.SetTermoTemperature (%s,v) """  
                what descr t.Property t.Name t.Name |]
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


