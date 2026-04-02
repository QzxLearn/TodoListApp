using Microsoft.EntityFrameworkCore;
using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Repositories;
using TodoListApp.Infrastructure.Data;

namespace TodoListApp.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task AddAsync(TodoItem item)
    {
        item.CreatedAt = DateTime.UtcNow;
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item)
    {
        item.UpdatedAt = DateTime.UtcNow;
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item != null)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}