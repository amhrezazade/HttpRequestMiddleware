using HRMDomain.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_Client
{
    public delegate Task OnRequest(Request request);

    public class HubService
    {
        private readonly string _hubInvokeFunctionName;
        private HubConnection _hub;
        private bool _ready;
        public event OnRequest OnHandleRequest;

        public HubService()
        {
            AppConfig appConfig = AppConfig.LoadConfig();
            _hubInvokeFunctionName = appConfig.HubInvokeFunctionName;

            try
            {
                _hub = new HubConnectionBuilder()
                .WithUrl(appConfig.HubURL, options =>
                {
                    options.Headers.Add("hubkey", appConfig.HubKey);
                })
                .Build();

                _hub.Closed += OnCLose;
                _hub.Reconnected += OnConnect;
                _hub.On<string>(appConfig.HubListenFunctionName, OnMessage);
                _ready = true;
            }
            catch
            {
                _ready = false;
            }

        }

        private async Task OnConnect(string message)
        {

        }

        private async Task OnCLose(Exception ex)
        {

        }


        private async Task OnMessage(string message)
        {
            await OnHandleRequest(JsonConvert.DeserializeObject<Request>(message));
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
        public bool Ready => _ready;

        public HubConnectionState State => _hub.State;


        public async Task SendMessage(string message)
        {
            await _hub.InvokeAsync(_hubInvokeFunctionName, message);
        }


    }
}
