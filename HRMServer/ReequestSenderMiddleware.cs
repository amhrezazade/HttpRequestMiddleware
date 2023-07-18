using HRMDomain.Model;
using HRMServer.Service;
using System.Text;

namespace HRMServer
{
    public class ReequestSenderMiddleware : IMiddleware
    {
        private static long count = 0;
        private readonly RequestResponseHandlerService _service;
        public ReequestSenderMiddleware(RequestResponseHandlerService requestResponseHandlerService)
        {
            _service = requestResponseHandlerService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.ToString().StartsWith("/Request/"))
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
            count++;
            string body = "";

            if (request.ContentLength is not null && request.ContentLength > 0)
            {
                byte[] data = new byte[request.ContentLength.Value];
                await request.Body.ReadAsync(data, 0, data.Length);
                body = Convert.ToBase64String(data);
            }

            Request requestMessage = new Request()
            {
                Id = count,
                Method = request.Method,
                Path = request.Path.ToString().Substring(9) + request.Query.ToString(),
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

            await Task.Delay(500);

            const int timeout = 10;

            for (int i = 0; i < timeout; i++)
            {
                var res = _service.CheckResponse(count);
                if (res is not null)
                    return res;
                await Task.Delay(1000);
            }

            return new Response()
            {
                StatusCode = 503,
                Body = "Bad Gateway",
                ContentType = "plain/text",
                RequestId = count
            };

        }

    }
}
