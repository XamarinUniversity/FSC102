namespace FSharpTodo

open System
open System.Collections.Generic
open System.Text

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "Edit Item", Theme = "@android:style/Theme.Holo.Light")>]
type EditToDoActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    let id = this.Intent.GetIntExtra("id", 0)

    let item = App.ItemManager.GetItem(id)

    this.SetContentView (Resource_Layout.SingleToDoItem)

    let taskName = this.FindViewById<TextView>(Resource_Id.taskName)
    let isComplete = this.FindViewById<CheckBox>(Resource_Id.isComplete)
    let saveButton = this.FindViewById<Button>(Resource_Id.saveButton)
    let deleteButton = this.FindViewById<Button>(Resource_Id.deleteButton)
    let errorMessage = this.FindViewById<TextView>(Resource_Id.errorMessage)

    taskName.Text <- item.Name
    isComplete.Checked <- item.IsComplete

    saveButton.Click.Add(fun args -> 
        item.Name <- taskName.Text
        item.IsComplete <- isComplete.Checked

        try
            App.ItemManager.UpdateItem(item) |> ignore
            this.Finish() 
        with 
            | _ -> errorMessage.Text <- "There was a problem adding the item. Check that your update hasn't resulted in a duplicate item"
    )

    deleteButton.Click.Add(fun args ->
        try
            App.ItemManager.DeleteItem(item) |> ignore
            this.Finish()  
        with 
            | _ -> errorMessage.Text <- "There was a problem removing the item."
    )