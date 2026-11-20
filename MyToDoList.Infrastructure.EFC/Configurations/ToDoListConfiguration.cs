using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDoList.Infrastructure.EFC.DataModel;

namespace MyToDoList.Infrastructure.EFC.Configurations;

public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
{
    public void Configure(EntityTypeBuilder<ToDoList> builder)
    {
        builder.ToTable("ToDoLists");
        
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Owner).HasMaxLength(256).IsRequired();
        builder.HasIndex(l => l.Owner).IsUnique();

        builder.HasMany(l => l.ToDos)
            .WithOne(t => t.ToDoList)
            .HasForeignKey(t => t.ToDoListId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
