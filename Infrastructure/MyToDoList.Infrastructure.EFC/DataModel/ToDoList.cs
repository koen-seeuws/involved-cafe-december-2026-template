namespace MyToDoList.Infrastructure.EFC.DataModel;

public class ToDoList
{
    public int Id { get; set; }
    
    /// <summary>The domain key of the aggregate (unique).</summary>
    public required string Owner { get; set; }
    
    public List<ToDo> ToDos { get; set; } = [];
}
