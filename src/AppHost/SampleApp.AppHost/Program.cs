var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SampleApp_Web_Server>("web-server");

builder.Build().Run();
