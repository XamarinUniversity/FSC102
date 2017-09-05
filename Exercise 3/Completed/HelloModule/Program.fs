open Adding

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    if argv.Length >= 2 then 
        printfn "%i" <| Adding.AddNumbers argv

    0 // return an integer exit code

