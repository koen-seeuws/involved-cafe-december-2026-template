using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain = MyToDoList.Core.Domain;

namespace MyToDoList.Infrastructure.EFC.Repositories;

public class ToDoListBaseRepository : BaseRepository<Domain.ToDoList, string, DataModel.ToDoList>
{
    public ToDoListBaseRepository(ToDoDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<DataModel.ToDoList> Query() => DbSet.Include(l => l.ToDos);

    protected override Expression<Func<DataModel.ToDoList, bool>> HasKey(string key) => l => l.Owner == key;

    protected override Domain.ToDoList CreateNewAggregate(string key) => new()
    {
        AggregateKey = key,
        ToDos = []
    };

    protected override DataModel.ToDoList CreateEntity(Domain.ToDoList aggregate) => new()
    {
        Owner = aggregate.AggregateKey
    };

    protected override Domain.ToDoList ToAggregate(DataModel.ToDoList entity) => new()
    {
        AggregateKey = entity.Owner,
        ToDos = entity.ToDos
            .OrderBy(t => t.Position)
            .Select(t => new Domain.ToDo
            {
                Key = t.Key,
                Title = t.Title,
                Description = t.Description
            })
            .ToImmutableList()
    };

    protected override void ApplyToEntity(Domain.ToDoList aggregate, DataModel.ToDoList entity)
    {
        var existing = entity.ToDos.ToDictionary(t => t.Key);
        var keysInAggregate = aggregate.ToDos.Select(t => t.Key).ToHashSet();

        // Deleted: removing from the required relationship makes EF delete the orphaned row.
        entity.ToDos.RemoveAll(t => !keysInAggregate.Contains(t.Key));

        for (var position = 0; position < aggregate.ToDos.Count; position++)
        {
            var toDo = aggregate.ToDos[position];

            if (existing.TryGetValue(toDo.Key, out var toDoEntity))
            {
                // Updated
                toDoEntity.Title = toDo.Title;
                toDoEntity.Description = toDo.Description;
                toDoEntity.Position = position;
            }
            else
            {
                // Added
                entity.ToDos.Add(new DataModel.ToDo
                {
                    Key = toDo.Key,
                    Title = toDo.Title,
                    Description = toDo.Description,
                    Position = position
                });
            }
        }
    }
}
