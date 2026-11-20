using Microsoft.EntityFrameworkCore;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Queries.GetMyTodos;

namespace MyToDoList.Infrastructure.EFC.QueryHandlers.GetMyTodos;

public record GetMyToDosQueryHandler : IQueryHandler<GetMyToDosQuery, IEnumerable<ToDoDto>> 
{
    private readonly ToDoDbContext _dbContext;
    private readonly IUserContext _userContext;

    public GetMyToDosQueryHandler(ToDoDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public async Task<IEnumerable<ToDoDto>> HandleAsync(GetMyToDosQuery query, CancellationToken ct = default)
    {
        var owner = _userContext.UserName;
        
        return await _dbContext.ToDos
            .Where(todo => todo.ToDoList.Owner.ToUpper() == owner.ToUpper())
            .OrderBy(todo => todo.Position)
            .Select(todo => new ToDoDto
            {
                Key = todo.Key,
                Title = todo.Title,
                Description = todo.Description,
            })
            .ToListAsync(ct);
    }
}