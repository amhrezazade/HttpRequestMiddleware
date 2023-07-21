using HRMDomain.Model;
using HRMServer.Service;
using System.Text;

namespace HRMServer
{
    public class ReequestSenderMiddleware : IMiddleware
    {
        private readonly RequestResponseHandlerService _service;
        public ReequestSenderMiddleware(RequestResponseHandlerService requestResponseHandlerService)
        {
            _service = requestResponseHandlerService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.StartsWithSegments("/Request"))
            {
                var res = await SendRequest(context.Request);
                context.Response.ContentType = res.ContentType;
                context.Response.StatusCode = res.StatusCode;
                byte[] data = Convert.FromBase64String(res.Body);
                await context.Response.Body.WriteAsync(data);
            }
            else
                await next(context);
        }


        private async Task<Response> SendRequest(HttpRequest request)
        {
            
            string body = "";

            if (request.ContentLength is not null && request.ContentLength > 0)
            {
                byte[] data = new byte[request.ContentLength.Value];
                await request.Body.ReadAsync(data, 0, data.Length);
                body = Convert.ToBase64String(data);
            }


            long Id = _service.GetNewId();

            Request requestMessage = new Request()
            {
                Id = Id,
                Method = request.Method,
                Path = request.Path.ToString().Substring(8) + request.QueryString,
                Body = body,
                Headers = request.Headers
                .Select(x => new HeaderModel()
                {
                    Key = x.Key, 
                    Value = x.Value
                })
                .ToArray()  
            };

            await _service.SendRequest(requestMessage);

            const int timeout = 300;

            for (int i = 0; i < timeout; i++)
            {
                var res = _service.CheckResponse(Id);
                if (res is not null)
                    return res;
                await Task.Delay(500);
            }

            _service.Remove(Id);

            return new Response()
            {
                StatusCode = 503,
                Body = Convert.ToBase64String(Encoding.UTF8.GetBytes("503 - Bad Gateway")),
                ContentType = "text/html",
                RequestId = Id
            };

        }

    }
}
