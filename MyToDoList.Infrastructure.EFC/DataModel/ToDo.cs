namespace MyToDoList.Infrastructure.EFC.DataModel;

public class ToDo
{
    public int Id { get; set; }
    
    /// <summary>The domain key of the to-do (unique).</summary>
    public required Guid Key { get; set; }
    
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    /// <summary>Preserves the order of the to-dos within their list.</summary>
    public int Position { get; set; }
    
    public int ToDoListId { get; set; }
    public ToDoList ToDoList { get; set; } = null!;
}
