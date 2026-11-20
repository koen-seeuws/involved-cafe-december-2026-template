namespace MyToDoList.Application.Abstractions.Queries.GetMyTodos;

public record ToDoDto
{
    public required Guid Key { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}