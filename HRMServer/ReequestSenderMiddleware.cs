using HRMServer.Service;

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
            if (context.Request.Path.ToString().StartsWith("/Request/"))
            {
                
            }
            else
                await next(context);
        }
    }
}
