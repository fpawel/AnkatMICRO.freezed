module Assembly 

open System
open System.Reflection

let private assembly = 
    try
        Some <| Reflection.Assembly.GetExecutingAssembly()
    with _ -> 
        None

let exePath = 
    assembly
    |> Option.bind (fun a -> 
        try
            Some <| IO.Path.GetDirectoryName  a.Location
        with _ ->
            None)
    |> Option.getWith Environment.CurrentDirectory


let version = 
    assembly
    |> Option.map (fun a -> a.GetName().Version)
