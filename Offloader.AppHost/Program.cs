var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.Offloader_ApiService>("apiservice");

builder.AddProject<Projects.Offloader_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
