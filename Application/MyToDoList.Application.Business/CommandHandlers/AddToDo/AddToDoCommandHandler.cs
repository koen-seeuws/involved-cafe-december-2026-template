using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.AddToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.CommandHandlers.AddToDo;

public class AddToDoCommandHandler : ICommandHandler<AddToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    private readonly IUserContext _userContext;
    
    public AddToDoCommandHandler(IRepository<ToDoList, string> repository, IUserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }
    
    public async Task HandleAsync(AddToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = _userContext.UserName;
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken)
            ?? throw new InvalidOperationException($"No to-do list could be loaded for owner '{owner}'.");
        
        toDoList.AddToDo(command.Title, command.Description);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}