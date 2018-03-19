namespace FSharpTodo

open System
open Android.App
open Android.Runtime

[<Application>]
type App (handle : IntPtr, transfer : JniHandleOwnership) = 
    inherit Application(handle, transfer) 

    static member val ItemManager : ToDoItemManager = new ToDoItemManager() with get, set
