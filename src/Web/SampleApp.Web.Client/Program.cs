using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SampleApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddScoped<ITodoApiClient, TodoApiClient>();

await builder.Build().RunAsync();
