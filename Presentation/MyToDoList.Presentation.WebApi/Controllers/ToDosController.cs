using Microsoft.AspNetCore.Mvc;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Queries.GetAllToDos;

namespace MyToDoList.Presentation.WebApi.Controllers;

/// <summary>Everybody's to-dos (read-only).</summary>
[ApiController]
[Route("api/todos")]
[Produces("application/json")]
public class ToDosController : ControllerBase
{
    private readonly IUseCaseDispatcher _dispatcher;

    public ToDosController(IUseCaseDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<ToDoDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDoDto>>> GetAll(CancellationToken ct)
        => Ok(await _dispatcher.QueryAsync(new GetAllToDosQuery(), ct));
}
