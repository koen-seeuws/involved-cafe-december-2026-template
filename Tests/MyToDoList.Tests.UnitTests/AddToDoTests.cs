using MyToDoList.Core.Domain;
using static MyToDoList.Tests.UnitTests.ToDoListBuilder;

namespace MyToDoList.Tests.UnitTests;

public class AddToDoTests
{
    [Fact]
    public void AddToDo_OnEmptyList_AddsSingleToDoWithGivenValues()
    {
        var list = Empty();

        list.AddToDo("Buy milk", "Semi-skimmed");

        var toDo = Assert.Single(list.ToDos);
        Assert.Equal("Buy milk", toDo.Title);
        Assert.Equal("Semi-skimmed", toDo.Description);
    }

    [Fact]
    public void AddToDo_AssignsNonEmptyKey()
    {
        var list = Empty();

        list.AddToDo("Title", "Description");

        Assert.NotEqual(Guid.Empty, list.ToDos[0].Key);
    }

    [Fact]
    public void AddToDo_MultipleTimes_AssignsUniqueKeys()
    {
        var list = Empty();

        list.AddToDo("Same", "Same");
        list.AddToDo("Same", "Same");

        Assert.Equal(2, list.ToDos.Count);
        Assert.NotEqual(list.ToDos[0].Key, list.ToDos[1].Key);
    }

    [Fact]
    public void AddToDo_AppendsToEndAndKeepsExistingToDos()
    {
        var existing = ToDo("Existing");
        var list = With(existing);

        list.AddToDo("New", "New description");

        Assert.Equal(2, list.ToDos.Count);
        Assert.Same(existing, list.ToDos[0]);
        Assert.Equal("New", list.ToDos[1].Title);
    }

    [Fact]
    public void AddToDo_ReplacesListInstance_LeavingPreviousSnapshotUntouched()
    {
        var list = Empty();
        var before = list.ToDos;

        list.AddToDo("Title", "Description");

        Assert.Empty(before);
        Assert.NotSame(before, list.ToDos);
    }

    [Fact]
    public void AddToDo_DoesNotChangeAggregateKey()
    {
        var list = Empty();

        list.AddToDo("Title", "Description");

        Assert.Equal(Owner, list.AggregateKey);
    }
}
