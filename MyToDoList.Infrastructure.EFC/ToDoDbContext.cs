using Microsoft.EntityFrameworkCore;
using MyToDoList.Infrastructure.EFC.DataModel;

namespace MyToDoList.Infrastructure.EFC;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoList> ToDoLists => Set<ToDoList>();
    public DbSet<ToDo> ToDos => Set<ToDo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
    }
}
