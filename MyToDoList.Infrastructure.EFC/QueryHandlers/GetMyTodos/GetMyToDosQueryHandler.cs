using Microsoft.EntityFrameworkCore;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Queries.GetMyTodos;

namespace MyToDoList.Infrastructure.EFC.QueryHandlers.GetMyTodos;

public record GetMyToDosQueryHandler : IQueryHandler<GetMyToDosQuery, IEnumerable<ToDoDto>> 
{
    private readonly ToDoDbContext _dbContext;

    public GetMyToDosQueryHandler(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ToDoDto>> HandleAsync(GetMyToDosQuery query, CancellationToken ct = default)
    {
        var owner = string.Empty; // TODO: retrieve owner name
        
        return await _dbContext.ToDos
            .Where(todo => todo.ToDoList.Owner == owner)
            .Select(todo => new ToDoDto
            {
                Title = todo.Title,
                Description = todo.Description,
            })
            .ToListAsync(ct);
    }
}