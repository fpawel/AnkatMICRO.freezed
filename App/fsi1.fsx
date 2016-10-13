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

let xs = Set.ofList ProductionPoint.values 

xs.Contains ( Correction (CorrectionLinScale Sens1) )
