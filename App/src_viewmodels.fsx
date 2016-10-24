#load "Utils/FsharpRuntime.fs"
#load "Utils/State.fs"
#load "Utils/Utils.fs"
#load "Utils/StrUtils.fs"
#load "Utils/DateTimeUtils.fs"
#load "Utils/Assembly.fs"
#load "Utils/PathUtils.fs"
#load "Utils/Logging.fs"

#load "Ankat/Coef.fs"
#load "Ankat/Sensor.fs"
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

module Name = 
    let private nn<'a> (x:'a) = sprintf "%A" x

    let (|PhysVar1|) = nn<PhysVar>
    let (|Sens|) = nn<SensorIndex>
    let (|T|) = nn<TermoPt>
    let (|P|) = nn<PressPt>
    let (|ScaleEdge1|) = nn<ScaleEdgePt>
    let (|Gas|) = function
        | ScaleMid -> "ScaleMid"
        | ScaleEdge (ScaleEdge1 x) -> sprintf "ScaleEdge(%s)" x

    let (|LinN|) = nn<LinPt>        

    let sens_gas_t (Sens n, Gas gas, T t) = sprintf "%s, %s, %s" n gas t

    let physVar (PhysVar1 x) = x

    let t (T x) = x

    let dataPoint (x, PhysVar1 y) = 
        let str1 =
            match x with            
            | LinPt (Sens n, LinN m) -> 
                sprintf "LinPt(%s,%s)" n m
            
            | TermoScalePt (Sens n, ScaleEdge1 gas, T t) -> 
                sprintf "TermoScalePt(%s, %s, %s)" n gas t  

            | TermoPressPt (T t) -> sprintf "TermoPressPt(%s)" t

            | PressSensPt (P p) -> sprintf "PressSensPt(%s)" p

            | TestPt (Sens n, Gas gas, T t) -> sprintf "TestPt(%s, %s, %s)" n gas t 
        
        sprintf "%s, %s" str1 y

    let clapan (Sens n, LinN  gas)= 
        sprintf "%s, %s" n gas

    let pgs  = clapan

let createSourceFile_ProductViewModel() = 
  [|  
    yield """namespace Ankat.ViewModel

open Ankat

type Product(p, getProdType, getPgs, partyId) =

    inherit ViewModel.Product1(p, getProdType, getPgs, partyId) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName"""

    for ((prodData,var) as k) in Points.prod do
        yield sprintf """
    member x.%s
        with get () = x.getVarUi (%s)
        and set value = x.setVarUi (%s) value"""  (Prop.dataPoint k) (Name.dataPoint k) (Name.dataPoint k)
        
    for n in Points.sens_gas_t do
        yield sprintf """
    member x.%s = x.GetConcError (%s)"""  (Prop.concError n) (Name.sens_gas_t n)
       
    
    for x in FSharpType.valuesListOf<PhysVar> do        
        yield sprintf """
    member x.%s = x.getPhysVarValueUi(%s)""" (Prop.physVar x)  (Name.physVar x) |]

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

    for n,gas as pt in Points.sens_lin do
        
        yield sprintf """
    [<Category("Концентрация ПГС")>] 
    [<DisplayName("Канал %d, ПГС%d")>]    
    [<Description("Концентрация ПГС%d, канал %d")>]
    member x.%s
        with get() = x.GetPgs(%s)
        and set v = x.SetPgs ( (%s), v) """  
             
            (valueOrderOf n + 1) 
            (valueOrderOf gas + 1) 
            (valueOrderOf gas + 1) 
            (valueOrderOf n + 1) 
            (Prop.pgs pt)
            (Name.pgs pt) (Name.pgs pt)
        
    for t in TermoPt.valuesList  do
        let what = TermoPt.what t
        let descr = TermoPt.dscr t
        

        yield sprintf """
    [<Category("Температура")>] 
    [<DisplayName("%s")>]    
    [<Description("%s")>]
    member x.%s 
        with get() = x.GetTermoTemperature %s
        and set v = x.SetTermoTemperature (%s,v) """  
                what descr (Prop.t t) (Name.t t) (Name.t t) |]
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
        and set value = x.SetDelay (%A) value  """ ctx.What ctx.What (Prop.delayContext ctx) ctx ctx
  |]
|> createSourcefile "View/DelaysHelpViewModel.fs" 


