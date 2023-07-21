using HRMDomain.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMClientService.Service
{
    public class ConnectionBackgroundService : BackgroundService
    {
        private readonly RequestService _requestService;
        private readonly ILogger<ConnectionBackgroundService> _logger;
        private readonly HubService _hubService;

        public ConnectionBackgroundService(ILogger<ConnectionBackgroundService> logger, HubService hubService, RequestService requestService)
        {
            _requestService = requestService;
            _logger = logger;
            _hubService = hubService;
            _hubService.OnHandleRequest += HandleRequest;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HubConnectionState lastState = _hubService.State;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_hubService.State == HubConnectionState.Disconnected)
                    await _hubService.Start();              

                if (_hubService.State != lastState)
                {
                    lastState = _hubService.State;
                    _logger.LogInformation("Hub " + lastState.ToString());
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task HandleRequest(Request request)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Request with Id=" + request.Id);
            Response response = await _requestService.HandleRequest(request);
            string json = JsonConvert.SerializeObject(response);
            _logger.LogInformation("Request " + request.Id + " responsed in " + stopwatch.ElapsedMilliseconds + " Milliseconds");
            await _hubService.SendMessage(json);
        }

    }
}
