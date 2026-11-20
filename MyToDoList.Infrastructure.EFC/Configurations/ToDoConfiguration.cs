using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDoList.Infrastructure.EFC.DataModel;

namespace MyToDoList.Infrastructure.EFC.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos");
        
        builder.HasKey(t => t.Id);
        
        builder.HasIndex(t => t.Key).IsUnique();

        builder.Property(t => t.Title).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(2000).IsRequired();
    }
}
