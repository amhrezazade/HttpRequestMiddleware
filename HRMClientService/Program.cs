using HRMClientService;
using HRMClientService.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {      
        services.AddSingleton<RequestService>();
        services.AddSingleton<HubService>();
        services.AddSingleton<ConfigHelperService>();
        services.AddHostedService<ConnectionBackgroundService>();
    })
    .Build();

await host.RunAsync();
