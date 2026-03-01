using System.Net.Http.Json;
using SampleApp.Web.Client.Models;

namespace SampleApp.Web.Client.Services;

public class TodoApiClient(HttpClient httpClient) : ITodoApiClient
{
    public async Task<IReadOnlyList<TodoItemDto>> GetTodosAsync(CancellationToken cancellationToken = default)
    {
        var todos = await httpClient.GetFromJsonAsync<List<TodoItemDto>>("/api/todos", cancellationToken);
        return todos?.AsReadOnly() ?? new List<TodoItemDto>().AsReadOnly();
    }

    public async Task<TodoItemDto?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TodoItemDto>($"/api/todos/{id}", cancellationToken);
    }

    public async Task<TodoItemDto> CreateTodoAsync(string title, string? description = null, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/todos", new { title, description }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoItemDto>(cancellationToken))!;
    }

    public async Task CompleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/api/todos/{id}/complete", null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/api/todos/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
