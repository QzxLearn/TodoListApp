using Microsoft.AspNetCore.Mvc;
using TodoListApp.Domain.Entities;
using TodoListApp.Domain.Repositories;

namespace TodoListApp.Api.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoRepository _repository;

    public TodoItemsController(ITodoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<TodoItem>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem item)
    {
        item.Id = Guid.NewGuid();
        await _repository.AddAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoItem item)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Title = item.Title;
        existing.IsCompleted = item.IsCompleted;
        await _repository.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}