namespace Ankat

type Units =
    | UnitsVolume 
    | UnitsNkpr 

    member x.Code = Units.context x |> fst
    
    static member context = function
        | UnitsVolume ->    3m, "%об"
        | UnitsNkpr ->      4m, "%НКПР"
    static member code = Units.context >> fst
    static member what = Units.context >> snd

    member x.What = Units.context x |> snd 
    

type Scale =     
    | Sc2
    | Sc5 
    | Sc10 
    | Sc100 

    member x.Code = Scale.code x
    member x.Value = Scale.value x
    member x.What = Scale.what x
    static member context = function
        | Sc2   -> 2m, 2m
        | Sc5   -> 6m, 5m
        | Sc10  -> 7m,  10m 
        | Sc100 -> 21m, 100m 
    static member code = Scale.context >> fst
    static member value = Scale.context >> snd
    static member what = Scale.context >> snd >> sprintf "0-%M"

    static member valuesList = [
        Sc2
        Sc5 
        Sc10 
        Sc100  ]


type Sensor =
    | CO2_2 
    | CO2_5
    | CO2_10
    | CH4 
    | C3H8 
    | SumCH
    
    member x.What = Sensor.what x
    
    member x.SensorCode = Sensor.sensorCode x 

    //member x.GasCode = Sensor.gasGode x 

    member x.IsCH = Sensor.isCH x

    member x.Scale = Sensor.scale x

    member x.Units = Sensor.units x

    static member sensorCode = function
        | CO2_2     -> 11m
        | CO2_5     -> 12m
        | CO2_10    -> 13m
        | C3H8      -> 14m
        | SumCH     -> 15m
        | CH4       -> 16m

    static member what = function
        | CO2_2     -> "CO₂2"
        | CO2_5     -> "CO₂5"
        | CO2_10    -> "CO₂10"
        | C3H8      -> "C₃H₈"
        | SumCH     -> "∑CH"
        | CH4       -> "CH₄"
        
    
    static member values = [
        CO2_2  
        CO2_5
        CO2_10
        C3H8
        SumCH
        CH4 ]

    static member isCH = function
        | CH4 | C3H8 | SumCH -> true
        | _ -> false


//    static member gasGode = function
//        | CH4   -> 5m
//        | C3H8  -> 7m 
//        | SumCH -> 6m
//        | _     -> 4m

    static member units = function
        | CH4 | C3H8 | SumCH -> UnitsNkpr
        | _ -> UnitsVolume

    static member scale = function
        | CO2_2     -> Sc2
        | CO2_5     -> Sc5
        | CO2_10    -> Sc10
        | _         -> Sc100

    member x.ConcErrorlimit = Sensor.concErrorlimit x

    static member errorLimit (x:Sensor) conc = 
        match x with
        | CH4 | C3H8 | SumCH -> 
            2.5m+0.05m*( abs conc) 
        | _  -> 0.5m
    
    static member concErrorlimit x concValue =        
        match x with
        | CH4
        | SumCH 
        | C3H8 ->     2.5m+0.05m * concValue
        | _ ->   0.2m + 0.05m * concValue
        

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
    
    static member new2 n s1 s2 = 
        {   TypeNumber = n
            Sensor = s1
            Sensor2 = Some s2   }

    static member new1 n s1 = 
        {   TypeNumber = n
            Sensor = s1
            Sensor2 = None   }

    

[<AutoOpen>]
module Helper1 =
    let (|IsCO2Sensor|) = function
        | CO2_2 | CO2_5 | CO2_10 -> true
        | _ -> false

    let (|IsCHSensor|) = function
        | CH4 | C3H8 | SumCH -> true
        | _ -> false



    

module private Helper =
    let prodTypes = 
        let co2 = [CO2_2; CO2_5; CO2_10]
        let ch = [CH4; C3H8; SumCH]
        let all = co2 @ ch
        let n1 = ProductType.new1
        let n2 n ch1 = ProductType.new2 n ch1 CH4
        let (<==) = List.map

        [   yield!  n1 10 <== co2
            yield!  n2 11 <== co2
            yield   n1 12 SumCH
            yield   n1 13 C3H8
            yield   n1 14 CH4 
            yield!  n1 15 <== all
            yield!  n1 16 <== all ]

type ProductType with
    static member first = 
        Helper.prodTypes.Head
    static member values = 
        Helper.prodTypes