using MediatR;
using SampleApp.Modules.Todo.Domain.Repositories;

namespace SampleApp.Modules.Todo.Application.UseCases.CompleteTodo;

internal sealed class CompleteTodoCommandHandler(ITodoRepository repository)
    : IRequestHandler<CompleteTodoCommand>
{
    public async Task Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"TodoItem with id {request.Id} not found.");

        todo.Complete();
        repository.Update(todo);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
