namespace Ankat

type Units =
    | UnitsmGpm3 
    | UnitsVolume 
    | UnitsNkpr 

    member x.Code = Units.context x |> fst
    
    static member context = function
        | UnitsmGpm3 ->     2m, "мг/м3"
        | UnitsVolume ->    7m, "%об"
        | UnitsNkpr ->      14m, "%НКПР"
    static member code = Units.context >> fst
    static member what = Units.context >> snd

    member x.What = Units.context x |> snd 
    

    


type Gas =
    | CO2 | CH4 | C3H8 | SumCH
    
    member x.What = sprintf "%A" x
    
    member x.Code = Gas.code x

    static member code = function
        | CO2 ->    4m
        | CH4 ->    5m
        | SumCH ->  6m
        | C3H8 ->   7m
    static member what (x:Gas) = x.What
    static member values = 
        [CH4;C3H8; SumCH; CO2 ]
    

type Scale =     
    | Sc4 | Sc10 | Sc20 | Sc50 | Sc100 

    member x.Code = Scale.code x
    member x.Value = Scale.value x
    member x.What = Scale.what x
    static member context = function
        | Sc4   -> 57m, 4m
        | Sc10  -> 7m,  10m
        | Sc20  -> 9m,  20m 
        | Sc50  -> 0m,  50m 
        | Sc100 -> 21m, 100m 
    static member code = Scale.context >> fst
    static member value = Scale.context >> snd
    static member what = Scale.context >> snd >> sprintf "0-%M"

    static member values = FSharpType.unionCasesList<Scale>

type ChannelIndex = 
    | Chan1
    | Chan2

type Channel =
    {   Gas : Gas
        Units : Units
        Scale : Scale }
    member x.What = Channel.what x
    static member errorLimit x conc = 
        match x.Gas, x.Scale with
        | (CH4 | C3H8), _
        | _, Sc4  -> 2.5m+0.05m*( abs conc) 
        | _, Sc10 -> 0.5m
        | _, Sc20 -> 1m
        | _ -> 1m
    static member what x = 
        sprintf "%s, %s" x.Gas.What x.Scale.What
    static member new' gas units scale = { Gas = gas; Scale = scale; Units = units }
        

type ProductType =   
    {   TypeNumber : int
        Channel : Channel 
        Channel2 : Channel option }  

    static member isTwoChannels x = x.Channel2.IsSome
    static member isOneChannels x = x.Channel2.IsNone
    static member channels x =
        List.choose id [   Some x.Channel; x.Channel2 ]

    static member channelsCount x = ProductType.channels x |> List.length
        

    static member what x =
        x |> ProductType.channels 
        |> Seq.toStr ", " Channel.what
        |> sprintf "%d, %s" x.TypeNumber

[<AutoOpen>]
module private Helpers2 =
    open System.IO
    let types, saveTypes = 
        Json.Config.create "types.json" ( fun () ->
            [   {   TypeNumber = 10
                    Channel = Channel.new' CO2 UnitsVolume Sc100  
                    Channel2 = None } ] )

type ProductType with 
    static member values = types
    static member save = saveTypes