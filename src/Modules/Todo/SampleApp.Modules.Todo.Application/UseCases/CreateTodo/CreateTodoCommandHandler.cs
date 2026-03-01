using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;
using SampleApp.Modules.Todo.Domain.Entities;
using SampleApp.Modules.Todo.Domain.Repositories;
using SampleApp.Modules.Todo.Domain.ValueObjects;

namespace SampleApp.Modules.Todo.Application.UseCases.CreateTodo;

internal sealed class CreateTodoCommandHandler(ITodoRepository repository)
    : IRequestHandler<CreateTodoCommand, TodoItemDto>
{
    public async Task<TodoItemDto> Handle(
        CreateTodoCommand request,
        CancellationToken cancellationToken)
    {
        var title = TodoTitle.Create(request.Title);
        var todo = TodoItem.Create(title, request.Description);

        await repository.AddAsync(todo, cancellationToken);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new TodoItemDto(
            todo.Id,
            todo.Title.Value,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt,
            todo.CompletedAt);
    }
}
