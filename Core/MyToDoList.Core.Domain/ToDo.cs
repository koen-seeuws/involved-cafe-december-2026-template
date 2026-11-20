namespace MyToDoList.Core.Domain;

public record ToDo
{
    public required Guid Key { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}