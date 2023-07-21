using HRMDomain.Model;
using HRMServer.Service;
using Microsoft.AspNetCore.SignalR;

namespace HRMServer
{
    public class ApplicationHub : Hub
    {
        private readonly RequestResponseHandlerService _service;

        public ApplicationHub(RequestResponseHandlerService service)
        {
            _service = service;
        }

        public void ResponeToRequest(string data)
        {
           _service.SetResponse(data);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await _service.Resend();
        }

    }
}
