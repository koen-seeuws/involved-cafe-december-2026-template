namespace MyToDoList.Core.Domain;

public static partial class ToDoListOperations
{
    public static void AddToDo(this ToDoList toDoList, string title, string description)
    {
        var todo = new ToDo
        {
            Key = Guid.NewGuid(),
            Title = title,
            Description = description
        };
        
        toDoList.ToDos = toDoList.ToDos.Add(todo);
    }
}