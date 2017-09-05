module Adding

let AddNumbers (arguments:string[]) = 
    arguments 
        |> Array.map (fun x -> (int)x)
        |> Array.sum