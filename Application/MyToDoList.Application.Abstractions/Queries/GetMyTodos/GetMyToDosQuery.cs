namespace MyToDoList.Application.Abstractions.Queries.GetMyTodos;

public record GetMyToDosQuery : IQuery<IEnumerable<ToDoDto>>
{
}