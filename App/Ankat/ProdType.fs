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

    member x.IsCH = Gas.isCH x

    static member code = function
        | CO2 ->    4m
        | CH4 ->    5m
        | SumCH ->  6m
        | C3H8 ->   7m
    static member what (x:Gas) = x.What
    static member values = 
        [CH4; C3H8; SumCH; CO2 ]
    static member isCH = function
        | CO2 ->    false
        | CH4 
        | SumCH 
        | C3H8 ->   true
    

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

// датчик концентрации
type Sensor =
    {   Gas : Gas
        Units : Units
        Scale : Scale }
    member x.What = Sensor.what x
    
    member x.ConcErrorlimit = Sensor.concErrorlimit x

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

    static member concErrorlimit x concValue =        
        match x.Gas, x.Scale with
        | CH4,_ 
        | SumCH,_ 
        | C3H8,_ ->     2.5m+0.05m * concValue
        | CO2, Sc4 ->   0.2m + 0.05m * concValue
        | CO2, Sc10 ->  0.5m
        | CO2, Sc20 ->  1m
        | _ -> 0m
        

type ProductType =   
    {   TypeNumber : int
        Sensor : Sensor 
        Sensor2 : Sensor option }  

    member x.What = ProductType.what x

    static member hasTwoSensors x = x.Sensor2.IsSome
    static member hasOneSensor x = x.Sensor2.IsNone
    static member sensors x =
        List.choose id [   Some x.Sensor; x.Sensor2 ]

    static member channelsCount x = ProductType.sensors x |> List.length

    static member what x =
        x |> ProductType.sensors 
        |> Seq.toStr ", " Sensor.what
        |> sprintf "%d, %s" x.TypeNumber
    static member first = 
        {   TypeNumber = 10
            Sensor = Sensor.new' CO2 UnitsVolume Sc100  
            Sensor2 = Some <| Sensor.new' CH4 UnitsVolume Sc100   }