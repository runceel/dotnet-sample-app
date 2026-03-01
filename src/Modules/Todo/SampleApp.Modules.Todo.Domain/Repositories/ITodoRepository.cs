using SampleApp.Modules.Todo.Domain.Entities;
using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Domain.Repositories;

public interface ITodoRepository
{
    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TodoItem todo, CancellationToken cancellationToken = default);
    void Update(TodoItem todo);
    void Delete(TodoItem todo);
    IUnitOfWork UnitOfWork { get; }
}
