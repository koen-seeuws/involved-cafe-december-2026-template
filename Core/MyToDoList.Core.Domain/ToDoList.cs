using System.Collections.Immutable;
using MyToDoList.Core.Domain.Abstractions;

namespace MyToDoList.Core.Domain;

public record ToDoList : IAggregate<string>
{
    public string AggregateKey { get; init; } // Filled in with the owner.
    public ImmutableList<ToDo> ToDos { get; set; }
}