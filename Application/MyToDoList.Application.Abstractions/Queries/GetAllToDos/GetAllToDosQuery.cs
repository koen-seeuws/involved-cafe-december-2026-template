namespace MyToDoList.Application.Abstractions.Queries.GetAllToDos;

public record GetAllToDosQuery : IQuery<IEnumerable<ToDoDto>>
{
}