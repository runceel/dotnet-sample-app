using Microsoft.EntityFrameworkCore;
using SampleApp.Modules.Todo.Domain.Entities;
using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Infrastructure.Persistence;

public class TodoDbContext(DbContextOptions<TodoDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
    }
}
