using Microsoft.EntityFrameworkCore;
using SampleApp.Modules.Todo.Domain.Entities;
using SampleApp.Modules.Todo.Domain.Repositories;
using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Infrastructure.Persistence.Repositories;

internal sealed class TodoRepository(TodoDbContext context) : ITodoRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.TodoItems.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.TodoItems.ToListAsync(cancellationToken);

    public async Task AddAsync(TodoItem todo, CancellationToken cancellationToken = default)
        => await context.TodoItems.AddAsync(todo, cancellationToken);

    public void Update(TodoItem todo)
        => context.TodoItems.Update(todo);

    public void Delete(TodoItem todo)
        => context.TodoItems.Remove(todo);
}
