using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Offloader.ServiceDefaults;
using Quartz;
using Docker.DotNet.Models;
using Docker.DotNet;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.SQLite;
using System.Text.RegularExpressions;

//using Azure.ResourceManager.ContainerInstance;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddProblemDetails();

GlobalConfiguration.Configuration.UseSQLiteStorage();
builder.Services
    .AddHangfire(configuration => configuration
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSQLiteStorage("Data Source=db.dat"));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IStorageConnection>(provider => provider.GetRequiredService<JobStorage>().GetConnection());

//builder.Services.AddHostedService<Scheduler>();
var connectionString = "Data Source=database.dat";
//builder.Services.AddQuartz(config => config.UsePersistentStore(x =>
//{
//    x.UseMicrosoftSQLite(connectionString);
//    x.PerformSchemaValidation = true;
//    x.UseNewtonsoftJsonSerializer();
//}));

builder.Services.AddSingleton(services =>
    Task.Run(async () => await services.GetRequiredService<ISchedulerFactory>().GetScheduler())
        .GetAwaiter()
        .GetResult());

builder.Services.AddEntityFrameworkSqlite();

//builder.Services.AddQuartzServer();
builder.Services.AddScoped<DockerService>();

builder.Services.AddDbContext<OffloaderDbContext>(x => x.UseSqlite(connectionString));
EnsureQuartzTablesCreated(connectionString);

var app = builder.Build();


app.UseHangfireDashboard();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.EnableTryItOutByDefault();
});

// Configure the HTTP request pipeline.
//app.UseExceptionHandler();

app
    .MapGet("/container/instances/list", async (
            [FromServices] DockerService dockerService,
            CancellationToken cancellationToken) =>
        await dockerService.Containers(cancellationToken))
    .WithOpenApi();

app
    .MapGet("/container/image/list", async (
            [FromServices] DockerService dockerService,
            CancellationToken cancellationToken) =>
        await dockerService.Images(cancellationToken))
    .WithOpenApi();

app
    .MapGet("/jobs/all", (
            //[FromServices] OffloaderDbContext dbContext,
            [FromServices] IStorageConnection jobStorage,
            CancellationToken cancellationToken) =>

         jobStorage.GetRecurringJobs()
             .Select(job => new ScheduledJob
             {
                 Id = job.Id,
                 Type = job.Job?.Type?.Name,
                 NextExecution = job.NextExecution,
                 LastExecution = job.LastExecution,
                 LastJobState = job.LastJobState,
                 Error = job.Error ?? job.LoadException?.Message,
                 Cron = job.Cron,
             }))
    .WithOpenApi();

app
    .MapGet("/jobs/add/{name}", (
            string name,
            [FromServices] IRecurringJobManager recurringJobManager) =>
            recurringJobManager.AddOrUpdate<StartContainerJob>(name, job => job.Execute(name), Cron.Minutely))
    .WithOpenApi();

app.UseRouting();

app.MapDefaultEndpoints();
app.UseEndpoints(endpoints =>
{

    endpoints.MapHangfireDashboard();
});
app.Run();

static void EnsureQuartzTablesCreated(string connectionString)
{
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    if (TableExists(connection, "QRTZ_JOB_DETAILS")) return;

    var script = File.ReadAllText("tables_sqlite.sql");

    using var command = new SqliteCommand(script, connection);
    command.ExecuteNonQuery();
}

static bool TableExists(SqliteConnection connection, string tableName)
{
    using var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';", connection);
    using var reader = command.ExecuteReader();
    return reader.HasRows;
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class OffloaderDbContext : DbContext
{
    public OffloaderDbContext(DbContextOptions<OffloaderDbContext> options) : base(options) { }

    public DbSet<JobDetail> Jobs { get; set; }
}


[Table("QRTZ_JOB_DETAILS")]
[PrimaryKey("Scheduler", "Name")]
public class JobDetail
{
    [Column("SCHED_NAME")]
    public string Scheduler { get; set; }

    [Column("JOB_NAME")]
    public string Name { get; set; }

    [Column("JOB_GROUP")]
    public string Group { get; set; }

    [Column("Description")]
    public string? Description { get; set; }

    [Column("IS_DURABLE")]
    public bool IsDurable { get; set; }

    [Column("IS_NONCONCURRENT")]
    public bool IsNonConcurrent { get; set; }

    [Column("IS_UPDATE_DATA")]
    public bool IsUpdateData { get; set; }

    [Column("REQUESTS_RECOVERY")]
    public bool RequestRecovery { get; set; }
}


public class DockerService
{

    public Uri DockerUri { get; } = new("npipe://./pipe/docker_engine");

    public async Task<IList<ImagesListResponse>> Images(CancellationToken cancellationToken)
    {
        using var dockerClient = new DockerClientConfiguration(DockerUri).CreateClient();

        var images = await dockerClient.Images.ListImagesAsync(new ImagesListParameters { All = true }, cancellationToken);
        return images;
    }

    public async Task<IList<ContainerListResponse>> Containers(CancellationToken cancellationToken)
    {
        using var dockerClient = new DockerClientConfiguration(DockerUri).CreateClient();
        var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = true }, cancellationToken);
        return containers;
    }
}

public class StartContainerJob
{
    public async Task Execute(string jobName)
    //public async Task Execute(IJobExecutionContext context)
    {

        Console.WriteLine($"{jobName}: Beginning job execution at {DateTime.Now.ToLongTimeString()}...");
        var dockerUri = "npipe://./pipe/docker_engine"; // Use the named pipe for Docker on Windows
        var containerName = "elated_chatterjee"; // The name of your container
        var timer = Stopwatch.StartNew();
        using (var dockerClient = new DockerClientConfiguration(new Uri(dockerUri)).CreateClient())
        {
            try
            {
                var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters
                {
                    All = true
                });

                var startTime = DateTime.UtcNow;
                var container = containers.FirstOrDefault(c => c.ID == containerName || c.Names.Contains($"/{containerName}"));
                if (container == null)
                {
                    Console.WriteLine($"{jobName}: Container {containerName} not found.");
                    return;
                }

                if (container.State != "running")
                {
                    Console.WriteLine($"{jobName}: Starting container {containerName} at {DateTime.Now.ToLongTimeString()}...");

                    await dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());

                    Console.WriteLine($"{jobName}: Logs (from {ConvertToUnixTimestamp(startTime)}) :");
                    //var logStream = await dockerClient.Containers.GetContainerLogsAsync(container.ID, new ContainerLogsParameters
                    //{
                    //    ShowStdout = true,
                    //    ShowStderr = true,
                    //    Follow = true,
                    //    Timestamps = false,
                    //    Since = ConvertToUnixTimestamp(startTime).ToString(),
                    //    Tail = "all"
                    //});
                    //// Copy the log stream directly to the console's output stream
                    //await logStream.CopyToAsync(Console.OpenStandardOutput());


                    var parameters = new ContainerLogsParameters
                    {
                        ShowStdout = true,
                        ShowStderr = true,
                        Follow = true,
                        Since = ConvertToUnixTimestamp(startTime).ToString(),
                        Timestamps = false,
                        Tail = "all"
                    };

                    await using (var stream = await dockerClient.Containers.GetContainerLogsAsync(container.ID, parameters))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = await reader.ReadLineAsync();
                                var cleanLine = RemoveAnsiEscapeSequences(line);
                                Console.WriteLine($"{jobName}: > {cleanLine}");
                            }
                        }
                    }
                    Console.WriteLine($"{jobName}: Executed container {containerName} successfully {DateTime.Now.ToLongTimeString()} Duration: {timer.ElapsedMilliseconds} ms");


                }
                else
                {
                    Console.WriteLine($"{jobName}: Container {containerName} is already running.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{jobName}: Failed to start container. Error: {ex.Message}");
            }
        }
    }


    private static long ConvertToUnixTimestamp(DateTime dateTime)
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
        return dateTimeOffset.ToUnixTimeSeconds();
    }


    private static string RemoveAnsiEscapeSequences(string input)
    {
        string ansiRegex = @"\x1B\[[0-9;]*[a-zA-Z]";
        return Regex.Replace(input, ansiRegex, string.Empty);
    }
    //public async Task Execute(IJobExecutionContext context)
    //{
    //    var subscriptionId = "";
    //    var resourceGroupName = "Sandbox";
    //    var containerName = "offloader-runner";

    //    var token = await new DefaultAzureCredential().GetTokenAsync(new TokenRequestContext(new []{ "https://management.azure.com" }), context.CancellationToken);
    //    var client = new HttpClient();
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

    //    var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ContainerInstance/containerGroups/{containerName}/start?api-version=2021-03-01";

    //    var response = await client.PostAsync(url, null);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        Console.WriteLine($"Started container {containerName} successfully.");
    //    }
    //    else
    //    {
    //        var errorContent = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine($"Failed to start container. Status: {response.StatusCode}, Error: {errorContent}");
    //    }
    //}
}

//public class StartContainerJob : IJob
//{
//    public async Task Execute(IJobExecutionContext context)
//    {

//        var armClient = new ArmClient(new DefaultAzureCredential());
//        var subscriptionId = "";
//        var totalTimer = Stopwatch.StartNew();
//        var stepTimer = Stopwatch.StartNew();
//        var subscription = armClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{subscriptionId}"));
//        Console.WriteLine($"Got subscription in {stepTimer.ElapsedMilliseconds} ms");


//        stepTimer = Stopwatch.StartNew();
//        var resourceGroupName = "Sandbox";
//        var resourceGroup = await subscription.GetResourceGroupAsync(resourceGroupName);
//        if (resourceGroup == null)
//        {
//            Console.WriteLine($"Resource group: {resourceGroupName} not found.");
//            return;
//        }
//        Console.WriteLine($"Got resource group in {stepTimer.ElapsedMilliseconds} ms");

//        stepTimer = Stopwatch.StartNew();
//        var containerGroupName = "offloader-runner";
//        var containerGroup = await resourceGroup.Value.GetContainerGroupAsync(containerGroupName);
//        if (containerGroup == null)
//        {
//            Console.WriteLine($"Container group: {containerGroupName} not found.");
//            return;
//        }

//        Console.WriteLine($"Got container instance in {stepTimer.ElapsedMilliseconds} ms");



//        stepTimer = Stopwatch.StartNew();
//        //await containerGroup.Value.AttachContainerAsync(containerGroupName);
//        Console.WriteLine($"Starting container {containerGroupName} at {DateTime.Now}...");
//        var startResult = await containerGroup.Value.StartAsync(WaitUntil.Completed);
//        Console.WriteLine($"Finished container execution in {stepTimer.ElapsedMilliseconds} ms, Total time: {totalTimer.ElapsedMilliseconds} ms");

//    }

//}


public record ScheduledJob
{
    public string Id { get; set; }
    public string? Type { get; set; }
    public DateTime? NextExecution { get; set; }
    public DateTime? LastExecution { get; set; }
    public string? Error { get; set; }
    public string LastJobState { get; set; }
    public string Cron { get; set; }
}