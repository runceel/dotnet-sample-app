using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;
using SampleApp.Modules.Todo.Domain.Repositories;

namespace SampleApp.Modules.Todo.Application.UseCases.GetTodoById;

internal sealed class GetTodoByIdQueryHandler(ITodoRepository repository)
    : IRequestHandler<GetTodoByIdQuery, TodoItemDto?>
{
    public async Task<TodoItemDto?> Handle(
        GetTodoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null) return null;

        return new TodoItemDto(
            todo.Id,
            todo.Title.Value,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt,
            todo.CompletedAt);
    }
}
