namespace MyToDoList.Core.Domain;

public static partial class ToDoListOperations
{
    public static void UpdateToDo(this ToDoList toDoList, Guid key, string title, string description)
    {
        var index = toDoList.ToDos.FindIndex(t => t.Key == key);
        
        if (index < 0)
            throw new KeyNotFoundException($"ToDo '{key}' not found in list '{toDoList.AggregateKey}'.");

        var updatedToDo = toDoList.ToDos[index] with { Title = title, Description = description };
        
        toDoList.ToDos = toDoList.ToDos.SetItem(index, updatedToDo);
    }
}
