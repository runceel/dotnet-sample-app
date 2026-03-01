using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;
using SampleApp.Modules.Todo.Domain.Repositories;

namespace SampleApp.Modules.Todo.Application.UseCases.GetTodos;

internal sealed class GetTodosQueryHandler(ITodoRepository repository)
    : IRequestHandler<GetTodosQuery, IReadOnlyList<TodoItemDto>>
{
    public async Task<IReadOnlyList<TodoItemDto>> Handle(
        GetTodosQuery request,
        CancellationToken cancellationToken)
    {
        var todos = await repository.GetAllAsync(cancellationToken);
        return todos
            .Select(t => new TodoItemDto(
                t.Id,
                t.Title.Value,
                t.Description,
                t.IsCompleted,
                t.CreatedAt,
                t.CompletedAt))
            .ToList()
            .AsReadOnly();
    }
}
