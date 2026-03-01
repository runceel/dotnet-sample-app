using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Modules.Todo.Domain.Repositories;
using SampleApp.Modules.Todo.Infrastructure.Persistence;
using SampleApp.Modules.Todo.Infrastructure.Persistence.Repositories;

namespace SampleApp.Modules.Todo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}
