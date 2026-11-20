namespace MyToDoList.Application.Abstractions;

/// <summary>Provides the identity of the user on whose behalf the current request is executed.</summary>
public interface IUserContext
{
    /// <summary>The name of the current user, used as the owner of their to-do list.</summary>
    string UserName { get; }
}
