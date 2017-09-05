open System.IO
open Apitron.Image2Pdf

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let directory = __SOURCE_DIRECTORY__ + @"/MonkeyPictures/"

    let images = Directory.GetFiles directory
    //optionally, create a filtered array with only png/jpg files
    //let filteredImages = images |> Array.filter (fun x -> x.Contains(".jpg") || x.Contains(".png"))

    let document = new Document()
    let settings = new PageSettings(ScaleMode = ImageScaleMode.PreserveAspectRatio, KeepImageOrientation = true)

    Array.iter (fun image -> document.AddImage(image, settings)) images
    //Array.iter (fun image -> document.AddImage(image, settings)) filteredImages

    document.Save(directory + "MonkeyPictures.pdf")

    0 // return an integer exit code

