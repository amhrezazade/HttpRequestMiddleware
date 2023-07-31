using HRMClientService;
using HRMClientService.Models;
using HRMClientService.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {      
        services.AddSingleton<RequestService>();
        services.AddSingleton<HubService>();
        services.AddSingleton<AppConfig>();
        services.AddHostedService<ConnectionBackgroundService>();
    })
    .Build();

await host.RunAsync();
