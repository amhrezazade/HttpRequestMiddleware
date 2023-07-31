using HRMClientService.Models;
using HRMDomain.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HRMClientService.Service
{
    public class ConnectionBackgroundService : BackgroundService
    {
        private readonly RequestService _requestService;
        private readonly ILogger<ConnectionBackgroundService> _logger;
        private readonly HubService _hubService;
        private readonly int _delayTime;
        private long _count;
        private HubConnectionState _lastState;
        public ConnectionBackgroundService(AppConfig config, ILogger<ConnectionBackgroundService> logger, HubService hubService, RequestService requestService)
        {
            _requestService = requestService;
            _logger = logger;
            _hubService = hubService;
            _hubService.OnHandleRequest += HandleRequest;
            _count = 0;
            _delayTime = config.LoopDelay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _lastState = _hubService.State;
            while (!stoppingToken.IsCancellationRequested)
            {
                _count++;
                await Loop();
                await Task.Delay(_delayTime, stoppingToken);
            }
            await _hubService.Stope();
        }

        private async Task Loop()
        {
            if (_hubService.State == HubConnectionState.Disconnected)
                await _hubService.Start();

            if (_hubService.State != _lastState)
            {
                _lastState = _hubService.State;
                _logger.LogInformation("Hub " + _lastState.ToString());
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
