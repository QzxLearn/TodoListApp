using System.Net.Http.Json;
using TodoListApp.Domain.Entities;

namespace TodoListApp.Web.Services;

public class TodoApiService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "http://localhost:5000/api/todos";

    public TodoApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<TodoItem>>(BaseUrl);
        return result ?? [];
    }

    public async Task<TodoItem> CreateAsync(string title)
    {
        var item = new TodoItem { Title = title };
        var response = await _http.PostAsJsonAsync(BaseUrl, item);
        return (await response.Content.ReadFromJsonAsync<TodoItem>())!;
    }

    public async Task UpdateAsync(TodoItem item)
    {
        await _http.PutAsJsonAsync($"{BaseUrl}/{item.Id}", item);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _http.DeleteAsync($"{BaseUrl}/{id}");
    }
}
