using MyToDoList.Core.Domain;

namespace MyToDoList.Tests.UnitTests;

public class ToDoTests
{
    [Fact]
    public void ToDos_WithSameValues_AreEqual()
    {
        var key = Guid.NewGuid();
        var a = new ToDo { Key = key, Title = "T", Description = "D" };
        var b = new ToDo { Key = key, Title = "T", Description = "D" };

        Assert.Equal(a, b);
    }

    [Fact]
    public void ToDos_WithDifferentKeys_AreNotEqual()
    {
        var a = new ToDo { Key = Guid.NewGuid(), Title = "T", Description = "D" };
        var b = a with { Key = Guid.NewGuid() };

        Assert.NotEqual(a, b);
    }
}
