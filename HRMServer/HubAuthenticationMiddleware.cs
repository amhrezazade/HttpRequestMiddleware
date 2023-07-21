using Microsoft.Extensions.Primitives;
using System.Net.Mime;
using System.Text;

namespace HRMServer
{
    public class HubAuthenticationMiddleware : IMiddleware
    {
        private readonly string _sererKey;

        public HubAuthenticationMiddleware(IConfiguration configuration)
        {
            _sererKey = configuration.GetSection("HubKey").Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.StartsWithSegments("/appHub"))
            {
                KeyValuePair<string, StringValues> header
                    = context.Request.Headers.FirstOrDefault(x => x.Key == "hubkey");
                
                if (header.Value != _sererKey)
                {
                    await Return401(context.Response);
                    return;
                }

                await next(context);
            }
            else
                await next(context);
        }

        private async Task Return401(HttpResponse response)
        {
            response.StatusCode = 401;
            response.ContentType = "text/html";
            await response.Body.WriteAsync(Encoding.UTF8.GetBytes("401 - Unauthorised"));
        }

    }
}
