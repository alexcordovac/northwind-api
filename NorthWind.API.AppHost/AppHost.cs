var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NorthWind_API>("northwind-api");

builder.Build().Run();
