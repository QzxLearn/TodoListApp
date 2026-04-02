using Microsoft.EntityFrameworkCore;
using TodoListApp.Domain.Entities;

namespace TodoListApp.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.ToTable("TodoItems");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}