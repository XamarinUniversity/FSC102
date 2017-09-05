namespace FSharpTodo

open System
open Android.App

[<Application>]
type App () = 
    inherit Application ()

    static member val ItemManager : ToDoItemManager = new ToDoItemManager() with get, set
