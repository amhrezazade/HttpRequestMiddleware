using HRMDomain.Model;
using HRMServer.Model;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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

        public void SetResponse(string data)
        {
            Response response = JsonConvert.DeserializeObject<Response>(data);
            Context c = contexts.FirstOrDefault(x => x.Id == response.RequestId);
            if (c != null) 
            {
                c.Response = response;
            }
        }


        public async Task SendRequest(Request request)
        {
            contexts.Add(new Context
            {
                Request = request,
                Id = request.Id,
                Response = null
            });

            string json = JsonConvert.SerializeObject(request);

            await _hubContext.Clients.All.SendAsync("request", json);
        }

        public Response CheckResponse(long Id)
        {
            return contexts.FirstOrDefault(x => x.Id == Id).Response;
        }

    }
}
