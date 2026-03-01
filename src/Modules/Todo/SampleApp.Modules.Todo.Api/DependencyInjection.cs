using Microsoft.Extensions.DependencyInjection;
using SampleApp.Modules.Todo.Application;
using SampleApp.Modules.Todo.Infrastructure;

namespace SampleApp.Modules.Todo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoModule(
        this IServiceCollection services,
        string connectionString)
    {
        services
            .AddTodoApplication()
            .AddTodoInfrastructure(connectionString);

        return services;
    }
}
