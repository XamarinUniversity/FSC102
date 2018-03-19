// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System.IO
open Apitron.Image2Pdf

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let directory = __PATH_TO_CLONED_EXERCISE_FOLDER__ + @"Assets/MonkeyPictures/"

    let images = Directory.GetFiles directory
    //optionally, create a filtered array with only png/jpg files
    //let filteredImages = images |> Array.filter (fun x -> x.Contains(".jpg") || x.Contains(".png"))

    let document = new Document()
    let settings = new PageSettings(ScaleMode = ImageScaleMode.PreserveAspectRatio, KeepImageOrientation = true)

    Array.iter (fun image -> document.AddImage(image, settings)) images
    //Array.iter (fun image -> document.AddImage(image, settings)) filteredImages

    document.Save(directory + "MonkeyPictures.pdf")

    0 // return an integer exit code
