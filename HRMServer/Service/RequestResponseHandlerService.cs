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
        private long count;

        public RequestResponseHandlerService(IHubContext<ApplicationHub> hubContext)
        {
            _hubContext = hubContext;
            contexts = new List<Context>();
            count = 0;
        }

        public long GetNewId()
        {
            count++;
            return count;
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

        public async Task Resend()
        {
            Context[] prndingRequests =
                contexts
                .Where(x => x.Response is null)
                .ToArray();

            foreach(Context context in prndingRequests) 
            {
                string json = JsonConvert.SerializeObject(context.Request);
                await _hubContext.Clients.All.SendAsync("Request", json);
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

            await _hubContext.Clients.All.SendAsync("Request", json);
        }

        public void Remove(long Id)
        {
            contexts.RemoveAll(x => x.Id == Id); 
        }

        public Response CheckResponse(long Id)
        {
            return contexts.FirstOrDefault(x => x.Id == Id).Response;
        }

    }
}
