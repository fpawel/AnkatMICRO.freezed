namespace Ankat

open System
open System.ComponentModel
open System.Text.RegularExpressions

type CoefAttribute(n : int, s: string) =
    inherit Attribute()
    member __.N = n
    member __.S = s


type Coef = 
    
    | [<Coef(0, "Номер версии ПО")>]
      VER_PO
    | [<Coef(1, "Номер исполнения прибора")>]
      PPRIBOR_TYPE
    | [<Coef(2, "Год выпуска")>]
      YEAR
    | [<Coef(3, "Серийный номер")>]
      SER_NUMBER
    | [<Coef(4, "Максимальное число регистров в таблице регистров прибора")>]
      Kef4
    | [<Coef(5, "Единицы измерения канала 1 ИКД")>]
      ED_IZMER_1
    | [<Coef(6, "Величина, измеряемая каналом 1 ИКД")>]
      Gas_Type_1
    | [<Coef(7, "Диапазон измерений канала 1 ИКД")>]
      SHKALA_1
    | [<Coef(8, "Начало шкалы канала 1 ИКД")>]
      PREDEL_LO_1
    | [<Coef(9, "Конец шкалы канала 1 ИКД")>]
      PREDEL_HI_1
    | [<Coef(10, "Значение ПГС1 (начало шкалы) канала 1 ИКД")>]
      Pgs1_1
    | [<Coef(11, "Значение ПГС3 (конец шкалы) канала 1 ИКД")>]
      Pgs3_1
    | [<Coef(12, "Коэффициент калибровки нуля канала 1 ИКД")>]
      KNull_1
    | [<Coef(13, "Коэффициент калибровки чувствительности канала 1 ИКД")>]
      KSens_1
    | [<Coef(14, "Единицы измерения канала 2 ИКД")>]
      ED_IZMER_2
    | [<Coef(15, "Величина, измеряемая каналом 2 ИКД")>]
      Gas_Type_2
    | [<Coef(16, "Диапазон измерений канала 2 ИКД")>]
      SHKALA_2
    | [<Coef(17, "Начало шкалы канала 2 ИКД")>]
      PREDEL_LO_2
    | [<Coef(18, "Конец шкалы канала 2 ИКД")>]
      PREDEL_HI_2
    | [<Coef(19, "ПГС1 (начало шкалы) канала 2 ИКД")>]
      Pgs1_2
    | [<Coef(20, "ПГС3 (конец шкалы) канала 2 ИКД")>]
      Pgs3_2
    | [<Coef(21, "Коэффициент калибровки нуля канала 2 ИКД")>]
      KNull_2
    | [<Coef(22, "Коэффициент калибровки чувствительности канала 2 ИКД")>]
      KSens_2
    | [<Coef(23, "0-ой степени кривой линеаризации канала 1 ИКД")>]
      CLin1_0
    | [<Coef(24, "1-ой степени кривой линеаризации канала 1 ИКД")>]
      CLin1_1
    | [<Coef(25, "2-ой степени кривой линеаризации канала 1 ИКД")>]
      CLin1_2
    | [<Coef(26, "3-ей степени кривой линеаризации канала 1 ИКД")>]
      CLin1_3
    | [<Coef(27, "0-ой степени полинома коррекции нуля от температуры канала 1 ИКД")>]
      KNull_T1_0
    | [<Coef(28, "1-ой степени полинома коррекции нуля от температуры канала 1 ИКД")>]
      KNull_T1_1
    | [<Coef(29, "2-ой степени полинома коррекции нуля от температуры канала 1 ИКД")>]
      KNull_T1_2
    | [<Coef(30, "0-ой степени полинома кор. чувств. от температуры канала 1 ИКД")>]
      KSens_T1_0
    | [<Coef(31, "1-ой степени полинома кор. чувств. от температуры канала 1 ИКД")>]
      KSens_T1_1
    | [<Coef(32, "2-ой степени полинома кор. чувств. от температуры канала 1 ИКД")>]
      KSens_T1_2
    | [<Coef(33, "0-ой степени кривой линеаризации канала 2 ИКД")>]
      CLin2_0
    | [<Coef(34, "1-ой степени кривой линеаризации канала 2 ИКД")>]
      CLin2_1
    | [<Coef(35, "2-ой степени кривой линеаризации канала 2 ИКД")>]
      CLin2_2
    | [<Coef(36, "3-ей степени кривой линеаризации канала 2 ИКД")>]
      CLin2_3
    | [<Coef(37, "0-ой степени полинома коррекции нуля от температуры канала 2 ИКД")>]
      KNull_T2_0
    | [<Coef(38, "1-ой степени полинома коррекции нуля от температуры канала 2 ИКД")>]
      KNull_T2_1
    | [<Coef(39, "2-ой степени полинома коррекции нуля от температуры канала 2 ИКД")>]
      KNull_T2_2
    | [<Coef(40, "0-ой степени полинома кор. чувств. от температуры канала 2 ИКД")>]
      KSens_T2_0
    | [<Coef(41, "1-ой степени полинома кор. чувств. от температуры канала 2 ИКД")>]
      KSens_T2_1
    | [<Coef(42, "2-ой степени полинома кор. чувств. от температуры канала 2 ИКД")>]
      KSens_T2_2
    | [<Coef(43, "0-ой степени полинома калибровки датчика P (в мм.рт.ст.)")>]
      Coef_Pmmhg_0
    | [<Coef(44, "1-ой степени полинома калибровки датчика P (в мм.рт.ст.)")>]
      Coef_Pmmhg_1
    | [<Coef(45, "0-ой степени полинома кор. нуля датчика давления от температуры")>]
      KNull_TP_0
    | [<Coef(46, "1-ой степени полинома кор. нуля датчика давления от температуры")>]
      KNull_TP_1
    | [<Coef(47, "2-ой степени полинома кор. нуля датчика давления от температуры")>]
      KNull_TP_2
    | [<Coef(48, "Чувствительность датчика температуры микроконтроллера, град.С/В")>]
      KdFt
    | [<Coef(49, "Смещение датчика температуры микроконтроллера, град.С")>]
      KFt
    | CoefCustom of int * string * (string option)

    

    


[<AutoOpen>]
module private Helpers2 =
    open System.IO

    open Microsoft.FSharp.Reflection 

    let filename = IO.Path.Combine( IO.Path.ofExe, "coefs.config" )

    let predefCoefs1 =         
        FSharpType.GetUnionCases typeof<Coef> 
        |> Array.choose( fun case -> 
            if case.GetFields().Length = 0 then
                FSharpValue.MakeUnion(case,[||]) :?> Coef |> Some 
            else None)

    let attr1 = FSharpValue.tryGetUnionCaseAttribute<CoefAttribute,Coef> >> Option.get

    let order1 = function
        | CoefCustom (n,_,_) -> n
        | x -> (attr1 x).N

    let customCoefs1 = 
        File.ReadAllLines filename 
        |> Array.choose (fun s -> 
            let m = Regex.Match(s, @"^(\d+)\s+(\w+)\s*([^$]*)$")
            if not m.Success then None else 
            let (~%%) (n:int) = m.Groups.[n].Value
            let n = Int32.Parse (%% 1)
            let coef = %% 2
            let descr = (%% 3).Trim()
            let descr = if String.IsNullOrEmpty descr then None else Some descr
            Some(CoefCustom(n, coef, descr)) ) 
        |> Array.filter(fun coef -> 
            predefCoefs1 
            |> Array.exists(fun coefx -> order1 coefx = order1 coef) 
            |> not )

    
    let  descr1 = function
        | CoefCustom (_, _, Some s) -> s
        | CoefCustom (_, s, _) -> s
        | x -> (attr1 x).S

    let orderToCoefMap = 
        Array.append customCoefs1 predefCoefs1 
        |> Array.map (fun x -> order1 x, x)
        |> Map.ofArray

    let coefToOrderMap = 
        orderToCoefMap |> Map.toList |> List.map revpair |> Map.ofList

    let coefs = orderToCoefMap |> Map.toList |> List.map snd

    let predefCoefs = Array.toList predefCoefs1

    

type Coef with
    member x.Order = 
        match x with
        | CoefCustom (n,_,_) -> n
        | _ -> coefToOrderMap.[x]

    member x.Description = 
        match x with
        | CoefCustom (_, _, Some s) -> s
        | CoefCustom (_, s, _) -> s
        | _ -> (attr1 x).S

    member x.Reg = 224 + 2 * x.Order
    member x.Cmd = ( 0x80 <<< 8 ) + x.Order


    static member order (x:Coef) = x.Order
    static member readReg x = 224 + 2 * Coef.order x
    static member writeCmd x = ( 0x80 <<< 8 ) + Coef.order x
    static member coefs = coefs

    static member tryGetByOrder = orderToCoefMap.TryFind 