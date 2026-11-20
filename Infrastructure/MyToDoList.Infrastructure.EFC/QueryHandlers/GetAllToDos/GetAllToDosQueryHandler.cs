using Microsoft.EntityFrameworkCore;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Queries.GetAllToDos;


namespace MyToDoList.Infrastructure.EFC.QueryHandlers.GetAllToDos;

public record GetAllToDosQueryHandler : IQueryHandler<GetAllToDosQuery, IEnumerable<ToDoDto>> 
{
    private readonly ToDoDbContext _dbContext;

    public GetAllToDosQueryHandler(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ToDoDto>> HandleAsync(GetAllToDosQuery query, CancellationToken ct = default)
        => await _dbContext.ToDos
            .Select(todo => new ToDoDto
            {
                Key = todo.Key,
                Title = todo.Title,
                Description = todo.Description,
                Owner = todo.ToDoList.Owner,
            })
            .ToListAsync(ct);
}