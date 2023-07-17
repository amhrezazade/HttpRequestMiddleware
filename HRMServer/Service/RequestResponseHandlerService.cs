using HRMDomain.Model;
using HRMServer.Model;
using Microsoft.AspNetCore.SignalR;

namespace HRMServer.Service
{
    public class RequestResponseHandlerService
    {
        private readonly IHubContext<ApplicationHub> _hubContext;
        private List<Context> contexts;

        public RequestResponseHandlerService(IHubContext<ApplicationHub> hubContext)
        {
            _hubContext = hubContext;
            contexts = new List<Context>();
        }

        public async Task<long> SetResponse(string data)
        {
            return 0;
        }


        public async Task<long> HandleRequest(Request request)
        {
            return 0;
        }

        public async Task<Response> CheckResponse(long Id)
        {
            return null;
        }

    }
}
