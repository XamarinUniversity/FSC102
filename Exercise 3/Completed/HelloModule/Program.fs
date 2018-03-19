// Learn more about F# at http://fsharp.org

open System
open Adding

[<EntryPoint>]
let main argv =
    if argv.Length >= 2 then
        printfn "%i" <| Adding.AddNumbers(argv)
    Console.Read() |> ignore
    0 // return an integer exit code
