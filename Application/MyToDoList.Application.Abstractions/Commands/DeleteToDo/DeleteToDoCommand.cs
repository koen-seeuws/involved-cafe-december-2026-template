namespace MyToDoList.Application.Abstractions.Commands.DeleteToDo;

public record DeleteToDoCommand : ICommand
{
    public required Guid Key { get; init; }
}
