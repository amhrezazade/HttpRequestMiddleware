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

        public async Task ResponeToRequest(string data)
        {
            await _service.SetResponse(data);
        }
    }
}
