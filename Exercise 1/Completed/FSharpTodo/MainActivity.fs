namespace FSharpTodo

open System
open System.Collections.Generic
open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "F# Todo", Theme = "@android:style/Theme.Holo.Light", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    member val items : List<TodoItem> = new List<TodoItem>() with get, set

    member this.GetMainListView() = 
        this.FindViewById<ListView>(Resource_Id.allItems)

    member this.UpdateData (listView : ListView) = 
        this.items <- App.ItemManager.GetItems()
        let adapter = new ArrayAdapter<TodoItem>(this, Android.Resource.Layout.SimpleListItem1, this.items)
        this.GetMainListView().Adapter <- adapter

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        this.SetContentView (Resource_Layout.Main)

        let taskName = this.FindViewById<EditText>(Resource_Id.taskName)
        let addButton = this.FindViewById<Button>(Resource_Id.addButton)
        let errorMessage = this.FindViewById<TextView>(Resource_Id.errorMessage)
        let allItems = this.FindViewById<ListView>(Resource_Id.allItems)

        allItems.ItemClick.Add(fun args -> 
            let selectedItem = this.items.[args.Position]

            let i = new Intent(this, typeof<EditToDoActivity>)
            i.PutExtra("id", selectedItem.Id) |> ignore
            this.StartActivity(i) |> ignore
        )

        addButton.Click.Add (fun args -> 
            try
                App.ItemManager.AddItem(taskName.Text) |> ignore
                errorMessage.Text <- ""
                taskName.Text <- ""
                this.UpdateData(allItems)
            with 
                | _ -> errorMessage.Text <- "There was a problem adding the item. Check that you've provided an item and there are no duplicates"
        )

    override this.OnResume() = 

        base.OnResume()

        this.UpdateData(this.GetMainListView())
