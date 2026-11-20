namespace MyToDoList.Application.Abstractions.Commands.UpdateToDo;

public record UpdateToDoCommand : ICommand
{
    public required Guid Key { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}
