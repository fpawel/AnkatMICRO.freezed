module Pneumo
    

type Clapan = 
    | Gas1
    | S1Gas2
    | S1Gas2CO2
    | S1Gas3
    | S2Gas2
    | S2Gas3

    member x.What = Clapan.what x
    
    static member what = function
        | Gas1      -> "ПГС1"
        | S1Gas2    -> "1-ПГС2"
        | S1Gas2CO2 -> "CO₂-ПГС2"
        | S1Gas3    -> "1-ПГС3"
        | S2Gas2    -> "2-ПГС2"
        | S2Gas3    -> "2-ПГС3"

    static member descr = function
        | Gas1      -> "ПГС1, начало шкалы"
        | S1Gas2    -> "ПГС2, середина шкалы канала 1"
        | S1Gas2CO2 -> "ПГС2, доп.середина шкалы канала 1 CO₂"
        | S1Gas3    -> "ПГС3, конец шкалы канала 1"
        | S2Gas2    -> "ПГС2, середина шкалы канала 2"
        | S2Gas3    -> "ПГС3, конец шкалы канала 1"

module private Helpers =
    let valuesList = [
        Gas1
        S1Gas2
        S1Gas2CO2
        S1Gas3
        S2Gas2
        S2Gas3 ]

type Clapan with

    static member valuesList = Helpers.valuesList

    static member code x = 
        Clapan.valuesList 
        |> List.findIndex ( (=) x) 
        |> ((+) 1)
        |> byte 

    