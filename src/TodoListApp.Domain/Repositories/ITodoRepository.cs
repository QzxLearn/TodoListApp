using TodoListApp.Domain.Entities;

namespace TodoListApp.Domain.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(Guid id);
}