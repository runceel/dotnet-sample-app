using MediatR;
using SampleApp.Modules.Todo.Domain.Repositories;

namespace SampleApp.Modules.Todo.Application.UseCases.DeleteTodo;

internal sealed class DeleteTodoCommandHandler(ITodoRepository repository)
    : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"TodoItem with id {request.Id} not found.");

        repository.Delete(todo);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
