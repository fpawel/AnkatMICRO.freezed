namespace Ankat

type Units =
    | UnitsmGpm3 
    | UnitsVolume 
    | UnitsNkpr 
    | UnitsCustom of int * string 
    
    static member context = function
        | UnitsmGpm3 ->     2, "мг/м3"
        | UnitsVolume ->    7, "%об"
        | UnitsNkpr ->      14, "%НКПР"
        | UnitsCustom (x,y)  -> x,y
 
    static member code = Units.context >> fst
    static member what = Units.context >> snd

    member x.What = Units.context x |> snd 
    

    


type Gas =
    | CO2 | CH4 | C3H8 | SumCH
    static member context = function
        | CH4 ->    5, ""
        | C3H8 ->   7
        | CO2 ->    4, 7 
    static member code = Gas.context >> fst
    static member unitsCode = Gas.context >> snd
    static member isCH = function  CO2 -> false | _ -> true 
    member x.What = sprintf "%A" x
    
    static member what (x:Gas) = x.What

    static member values = 
        [CH4;C3H8; SumCH; CO2 ]
    

type Scale =     
    | Sc4 | Sc10 | Sc20 | Sc50 | Sc100 

    member x.What = Scale.what x
    static member context = function
        | Sc4   -> 57, 4m
        | Sc10  -> 7,  10m
        | Sc20  -> 9,  20m 
        | Sc50  -> 0,  50m 
        | Sc100 -> 21, 100m 
    static member code = Scale.context >> fst
    static member value = Scale.context >> snd
    static member what = Scale.context >> snd >> sprintf "0-%M"

    static member values = FSharpType.unionCasesList<Scale>



type ProductTypeChannel =
    {   Gas : Gas
        Scale : Scale }
    member x.What = ProductTypeChannel.what x
    static member errorLimit x conc = 
        match x.Gas, x.Scale with
        | (CH4 | C3H8), _
        | _, Sc4  -> 2.5m+0.05m*( abs conc) 
        | _, Sc10 -> 0.5m
        | _, Sc20 -> 1m
        | _ -> 1m
    static member what x = 
        sprintf "%s, %s" x.Gas.What x.Scale.What
    static member new' gas scale = { Gas = gas; Scale = scale }
        

type ProductType =   
    {   TypeNumber : int
        Channels : ProductTypeChannel list }  
    static member what x =
        x.Channels 
        |> Seq.toStr ", " ProductTypeChannel.what
        |> sprintf "%d, %s" x.TypeNumber

[<AutoOpen>]
module private Helpers2 =
    open System.IO
    type Chn = ProductTypeChannel
    let defchns = [
        Chn.new' CO2 Sc100
        Chn.new' CO2 Sc100
        ]
        

[<AutoOpen>]
module  Helpers1 =
    let types, saveTypes = 
        Json.Config.create "types.json" ( fun () ->
            [   {   TypeNumber = 10
                    Channels = [ProductTypeChannel.construct CO2 Sc100] }  
            ] )

[<AutoOpen>]
module private Helpers =
    open System.IO
    let types =
    

        let filename = Path.Combine(Path.ofExe, "ProductTypes.config")
        let isExisted = File.Exists filename
        let xtypes = 
            if not isExisted then [] else 
                match TextConfig.readFromFile filename Parse.productTypes with
                | None -> []
                | Some x -> x
        let types =         
            [   "10.CO2.2", [   PPRIBOR_TYPE, 10m
                                ED_IZMER_1, 1m
                                Gas_Type_1, 1m
                                SHKALA_1, 5m ] 

                "10.CO2.3", [   PPRIBOR_TYPE, 10m
                                ED_IZMER_1, 1m
                                Gas_Type_1, 1m
                                SHKALA_1, 5m ]  
                "11.CH.3", [    PPRIBOR_TYPE, 10m
                                ED_IZMER_1, 1m
                                Gas_Type_1, 1m
                                SHKALA_1, 5m 

                                ED_IZMER_2, 1m
                                Gas_Type_2, 1m
                                SHKALA_2, 5m  ] ]
            |> List.map( fun (s,xs) ->
                s, xs |> List.map( fun ((n,_,_),value) -> n,value ) |> Map.ofList  )

        let types = xtypes @ types |> Map.ofList |> Map.toList
    
        
        let x = 
            types |> List.map( fun (name,kefs) ->
                name, 
                    kefs |> Map.filter( fun nkef _ ->
                        Kef.kefsn |> List.exists( (=) nkef ) ) )
        if not isExisted then
            TextConfig.writeToFile filename 
                (   formatList x "\r\n" (fun (s,kefsVals) -> 
                    sprintf "\"%s\"\r\n\t %s" s 
                        (   formatList kefsVals "; " (fun n_val -> sprintf "%d, %g" n_val.Key n_val.Value) ) ))
        x

type ProductType with 

    static member values = values

    static member index x = 
        List.findIndex ((=) x) values