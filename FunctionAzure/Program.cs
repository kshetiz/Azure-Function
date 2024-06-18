using FunctionAzure;
using FunctionAzure.Repository.Implimentation;
using FunctionAzure.Repository.Interface;
using FunctionAzure.Service.Implimentation;
using FunctionAzure.Service.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
var cnnStr = Environment.GetEnvironmentVariable("DefaultConnection");

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
.ConfigureServices(services =>
{

    services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    //services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseNpgsql("Host=localhost;Port=5432;Database=Azure;Username=postgres;Password=Postgres@123;Pooling=true;"));

    services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer("Server=DESKTOP-7DE129J\\SQLEXPRESS;initial catalog=Azure;MultipleActiveResultSets=true;Connection Timeout=30;TrustServerCertificate=true; Integrated Security=true;"));


    services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

    services.AddScoped<IToDoService, ToDoService>();

    //services.AddDbContext<ApplicationDbContext>(options =>
    //{
    //    var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
    //    //options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));

    //   options.UseNpgsql(connectionString);
    //});
})
    .Build();

host.Run();
