using MyToDoList.Core.Domain;
using static MyToDoList.Tests.UnitTests.ToDoListBuilder;

namespace MyToDoList.Tests.UnitTests;

public class UpdateToDoTests
{
    [Fact]
    public void UpdateToDo_ExistingKey_UpdatesTitleAndDescription()
    {
        var toDo = ToDo("Old title", "Old description");
        var list = With(toDo);

        list.UpdateToDo(toDo.Key, "New title", "New description");

        var updated = Assert.Single(list.ToDos);
        Assert.Equal(toDo.Key, updated.Key);
        Assert.Equal("New title", updated.Title);
        Assert.Equal("New description", updated.Description);
    }

    [Fact]
    public void UpdateToDo_KeepsPositionAndLeavesOtherToDosUntouched()
    {
        var first = ToDo("First");
        var middle = ToDo("Middle");
        var last = ToDo("Last");
        var list = With(first, middle, last);

        list.UpdateToDo(middle.Key, "Changed", "Changed");

        Assert.Equal(3, list.ToDos.Count);
        Assert.Same(first, list.ToDos[0]);
        Assert.Equal(middle.Key, list.ToDos[1].Key);
        Assert.Equal("Changed", list.ToDos[1].Title);
        Assert.Same(last, list.ToDos[2]);
    }

    [Fact]
    public void UpdateToDo_DoesNotMutateOriginalToDoRecord()
    {
        var toDo = ToDo("Old title", "Old description");
        var list = With(toDo);

        list.UpdateToDo(toDo.Key, "New title", "New description");

        Assert.Equal("Old title", toDo.Title);
        Assert.Equal("Old description", toDo.Description);
    }

    [Fact]
    public void UpdateToDo_UnknownKey_ThrowsKeyNotFoundExceptionAndLeavesListUnchanged()
    {
        var toDo = ToDo();
        var list = With(toDo);
        var before = list.ToDos;
        var unknownKey = Guid.NewGuid();

        var ex = Assert.Throws<KeyNotFoundException>(() => list.UpdateToDo(unknownKey, "x", "y"));

        Assert.Contains(unknownKey.ToString(), ex.Message);
        Assert.Contains(Owner, ex.Message);
        Assert.Same(before, list.ToDos);
    }

    [Fact]
    public void UpdateToDo_OnEmptyList_ThrowsKeyNotFoundException()
    {
        var list = Empty();

        Assert.Throws<KeyNotFoundException>(() => list.UpdateToDo(Guid.NewGuid(), "x", "y"));
    }
}
