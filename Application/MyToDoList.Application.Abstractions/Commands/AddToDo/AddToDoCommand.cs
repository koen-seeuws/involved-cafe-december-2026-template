namespace MyToDoList.Application.Abstractions.Commands.AddToDo;

public record AddToDoCommand : ICommand
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}