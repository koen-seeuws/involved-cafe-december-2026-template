namespace MyToDoList.Core.Domain;

public static partial class ToDoListOperations
{
    public static void DeleteToDo(this ToDoList toDoList, Guid key)
    {
        var index = toDoList.ToDos.FindIndex(t => t.Key == key);
        
        if (index < 0)
            throw new KeyNotFoundException($"ToDo '{key}' not found in list '{toDoList.AggregateKey}'.");

        toDoList.ToDos = toDoList.ToDos.RemoveAt(index);
    }
}
