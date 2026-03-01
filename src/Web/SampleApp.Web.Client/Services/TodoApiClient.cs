using MediatR;
using SampleApp.Modules.Todo.Application.UseCases.CompleteTodo;
using SampleApp.Modules.Todo.Application.UseCases.CreateTodo;
using SampleApp.Modules.Todo.Application.UseCases.DeleteTodo;
using SampleApp.Modules.Todo.Application.UseCases.GetTodoById;
using SampleApp.Modules.Todo.Application.UseCases.GetTodos;
using SampleApp.Web.Client.Models;

namespace SampleApp.Web.Client.Services;

public class TodoApiClient(IMediator mediator) : ITodoApiClient
{
    public async Task<IReadOnlyList<TodoItemDto>> GetTodosAsync(CancellationToken cancellationToken = default)
    {
        var todos = await mediator.Send(new GetTodosQuery(), cancellationToken);
        return todos
            .Select(t => new TodoItemDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt, t.CompletedAt))
            .ToList()
            .AsReadOnly();
    }

    public async Task<TodoItemDto?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await mediator.Send(new GetTodoByIdQuery(id), cancellationToken);
        return todo is null
            ? null
            : new TodoItemDto(todo.Id, todo.Title, todo.Description, todo.IsCompleted, todo.CreatedAt, todo.CompletedAt);
    }

    public async Task<TodoItemDto> CreateTodoAsync(string title, string? description = null, CancellationToken cancellationToken = default)
    {
        var todo = await mediator.Send(new CreateTodoCommand(title, description), cancellationToken);
        return new TodoItemDto(todo.Id, todo.Title, todo.Description, todo.IsCompleted, todo.CreatedAt, todo.CompletedAt);
    }

    public async Task CompleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CompleteTodoCommand(id), cancellationToken);
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await mediator.Send(new DeleteTodoCommand(id), cancellationToken);
    }
}
