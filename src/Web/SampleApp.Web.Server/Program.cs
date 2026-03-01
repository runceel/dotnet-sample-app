using SampleApp.Modules.Todo.Api;
using SampleApp.Modules.Todo.Infrastructure.Persistence;
using SampleApp.Web.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("TodoDb")
    ?? "Data Source=todo.db";

builder.Services.AddTodoModule(connectionString);

builder.Services.AddScoped<ITodoApiClient, TodoApiClient>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapDefaultEndpoints();
app.MapTodoEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<SampleApp.Web.Client.App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(SampleApp.Web.Client.App).Assembly);

app.Run();
