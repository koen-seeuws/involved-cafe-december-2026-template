using MyToDoList.Core.Domain;
using static MyToDoList.Tests.UnitTests.ToDoListBuilder;

namespace MyToDoList.Tests.UnitTests;

public class DeleteToDoTests
{
    [Fact]
    public void DeleteToDo_ExistingKey_RemovesThatToDo()
    {
        var toDo = ToDo();
        var list = With(toDo);

        list.DeleteToDo(toDo.Key);

        Assert.Empty(list.ToDos);
    }

    [Fact]
    public void DeleteToDo_RemovesOnlyMatchingToDoAndPreservesOrder()
    {
        var first = ToDo("First");
        var middle = ToDo("Middle");
        var last = ToDo("Last");
        var list = With(first, middle, last);

        list.DeleteToDo(middle.Key);

        Assert.Equal([first, last], list.ToDos);
    }

    [Fact]
    public void DeleteToDo_ReplacesListInstance_LeavingPreviousSnapshotUntouched()
    {
        var toDo = ToDo();
        var list = With(toDo);
        var before = list.ToDos;

        list.DeleteToDo(toDo.Key);

        Assert.Single(before);
        Assert.NotSame(before, list.ToDos);
    }

    [Fact]
    public void DeleteToDo_SameKeyTwice_SecondCallThrows()
    {
        var toDo = ToDo();
        var list = With(toDo);

        list.DeleteToDo(toDo.Key);

        Assert.Throws<KeyNotFoundException>(() => list.DeleteToDo(toDo.Key));
    }

    [Fact]
    public void DeleteToDo_UnknownKey_ThrowsKeyNotFoundExceptionAndLeavesListUnchanged()
    {
        var toDo = ToDo();
        var list = With(toDo);
        var before = list.ToDos;
        var unknownKey = Guid.NewGuid();

        var ex = Assert.Throws<KeyNotFoundException>(() => list.DeleteToDo(unknownKey));

        Assert.Contains(unknownKey.ToString(), ex.Message);
        Assert.Contains(Owner, ex.Message);
        Assert.Same(before, list.ToDos);
    }

    [Fact]
    public void DeleteToDo_OnEmptyList_ThrowsKeyNotFoundException()
    {
        var list = Empty();

        Assert.Throws<KeyNotFoundException>(() => list.DeleteToDo(Guid.NewGuid()));
    }
}
