using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.UpdateToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.CommandHandlers.UpdateToDo;

public class UpdateToDoCommandHandler : ICommandHandler<UpdateToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    private readonly IUserContext _userContext;
    
    public UpdateToDoCommandHandler(IRepository<ToDoList, string> repository, IUserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }
    
    public async Task HandleAsync(UpdateToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = _userContext.UserName;
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken)
            ?? throw new InvalidOperationException($"No to-do list could be loaded for owner '{owner}'.");
        
        toDoList.UpdateToDo(command.Key, command.Title, command.Description);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}
