using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.DeleteToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.CommandHandlers.DeleteToDo;

public class DeleteToDoCommandHandler : ICommandHandler<DeleteToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    private readonly IUserContext _userContext;
    
    public DeleteToDoCommandHandler(IRepository<ToDoList, string> repository, IUserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }
    
    public async Task HandleAsync(DeleteToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = _userContext.UserName;
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken)
            ?? throw new InvalidOperationException($"No to-do list could be loaded for owner '{owner}'.");
        
        toDoList.DeleteToDo(command.Key);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}
