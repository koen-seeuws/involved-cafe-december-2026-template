using System.Collections.Immutable;
using MyToDoList.Core.Domain;

namespace MyToDoList.Tests.UnitTests;

internal static class ToDoListBuilder
{
    public const string Owner = "owner@example.com";

    public static ToDoList Empty() => new()
    {
        AggregateKey = Owner,
        ToDos = ImmutableList<ToDo>.Empty
    };

    public static ToDoList With(params ToDo[] toDos) => new()
    {
        AggregateKey = Owner,
        ToDos = [..toDos]
    };

    public static ToDo ToDo(string title = "Title", string description = "Description") => new()
    {
        Key = Guid.NewGuid(),
        Title = title,
        Description = description
    };
}
