using Microsoft.AspNetCore.Mvc;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.AddToDo;
using MyToDoList.Application.Abstractions.Commands.DeleteToDo;
using MyToDoList.Application.Abstractions.Commands.UpdateToDo;
using MyToDoList.Application.Abstractions.Queries.GetMyTodos;
using MyToDoList.Presentation.WebApi.Models;

namespace MyToDoList.Presentation.WebApi.Controllers;

/// <summary>The to-do list of the current user.</summary>
[ApiController]
[Route("api/me/todos")]
[Produces("application/json")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
public class MyToDosController : ControllerBase
{
    private readonly IUseCaseDispatcher _dispatcher;

    public MyToDosController(IUseCaseDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<ToDoDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDoDto>>> GetMine(CancellationToken ct)
        => Ok(await _dispatcher.QueryAsync(new GetMyToDosQuery(), ct));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(
        CreateToDoRequest request, CancellationToken ct)
    {
        await _dispatcher.SendAsync(new AddToDoCommand
        {
            Title = request.Title,
            Description = request.Description
        }, ct);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{key:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid key, UpdateToDoRequest request, CancellationToken ct)
    {
        await _dispatcher.SendAsync(new UpdateToDoCommand
        {
            Key = key,
            Title = request.Title,
            Description = request.Description
        }, ct);

        return NoContent();
    }

    [HttpDelete("{key:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid key, CancellationToken ct)
    {
        await _dispatcher.SendAsync(new DeleteToDoCommand { Key = key }, ct);

        return NoContent();
    }
}
