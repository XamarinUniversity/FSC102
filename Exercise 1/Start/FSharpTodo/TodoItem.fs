namespace FSharpTodo

open System
open System.Linq
open SQLite

type TodoItem() = 
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val Id = 0 with get, set

    [<Unique>]
    member val Name = "" with get, set
    member val IsComplete = false with get, set

    override this.ToString() =
        this.Name

type ToDoItemManager() = 

    member this.GetConnection() : SQLiteConnection = 
        let folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        let fullPath = IO.Path.Combine(folder, "todoitems.db3")

        let conn = new SQLiteConnection(fullPath, true) 
        let table = conn.CreateTable<TodoItem> ()

        conn

    member this.connection : SQLiteConnection = this.GetConnection() 

    member this.AddItem (name) = 
        let item = new TodoItem(Name = name)
        let recordsAffected = this.connection.Insert(item)

        item

    member this.UpdateItem (item) =
        this.connection.Update(item) 

    member this.DeleteItem (item) = 
        this.connection.Delete(item)

    member this.GetItems () = 
        this.connection.Table<TodoItem>().ToList()

    member this.GetItem(id : int) = 
        this.connection.Find<TodoItem>(id)
