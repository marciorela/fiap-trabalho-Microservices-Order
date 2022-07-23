using Geekburger.Order.Data.Repositories;
using Geekburger.Order.Database;
using Messages.WS;

//var config = new ConfigurationBuilder()
//  .SetBasePath(Directory.GetCurrentDirectory())
//  .AddJsonFile("appsettings.json")
//  .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<OrderDbContext>();
        services.AddSingleton<OrderRepository>();
        services.AddHostedService<WorkerMessageNewOrder>();
    })
    .ConfigureAppConfiguration((services, configBuilder) =>
    {
        configBuilder
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();
    })
    .Build();

await host.RunAsync();
