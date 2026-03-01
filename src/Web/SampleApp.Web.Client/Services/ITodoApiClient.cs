using SampleApp.Web.Client.Models;

namespace SampleApp.Web.Client.Services;

public interface ITodoApiClient
{
    Task<IReadOnlyList<TodoItemDto>> GetTodosAsync(CancellationToken cancellationToken = default);
    Task<TodoItemDto?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TodoItemDto> CreateTodoAsync(string title, string? description = null, CancellationToken cancellationToken = default);
    Task CompleteTodoAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default);
}
