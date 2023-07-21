using HRMClientService.Delegate;
using HRMClientService.Models;
using HRMDomain.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;


namespace HRMClientService.Service
{
    public class HubService
    {
        private readonly string _hubInvokeFunctionName;
        private readonly ILogger<HubService> _logger;

        private HubConnection _hub;

        public HubService(ConfigHelperService configHelperService, ILogger<HubService> logger)
        {
            _logger = logger;

            _hubInvokeFunctionName = configHelperService.AppConfig.HubInvokeFunctionName;

            _hub = new HubConnectionBuilder()
            .WithUrl(configHelperService.AppConfig.HubURL, options =>
            {
                options.Headers.Add("hubkey", configHelperService.AppConfig.HubKey);
            })
            .Build();

            _hub.Closed += OnCLose;
            _hub.Reconnected += OnConnect;
            _hub.On<string>(configHelperService.AppConfig.HubListenFunctionName, OnMessage);
        }

        public event OnRequest OnHandleRequest;


        private Task OnMessage(string message)
            => OnHandleRequest(JsonConvert.DeserializeObject<Request>(message));


        private async Task OnConnect(string message)
        {
            _logger.LogInformation("Connected " + message);
        }

        private async Task OnCLose(Exception ex)
        {
            _logger.LogInformation("Closed " + ex.Message);
        }


        public async Task Start()
        {
            try
            {
                await _hub.StartAsync();
            }
            catch { }
        }

        public async Task Stope()
        {
            await _hub.StopAsync();
        }

        public HubConnectionState State => _hub.State;


        public async Task SendMessage(string message)
        {
            await _hub.InvokeAsync(_hubInvokeFunctionName, message);
        }


    }
}
