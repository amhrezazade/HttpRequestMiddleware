using HRMDomain.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;

namespace HRMClientService.Service
{
    public class RequestService
    {
        private JObject _hostMap;
        public RequestService(ConfigHelperService configHelperService)
        {
            _hostMap = configHelperService.HostMapping;
        }

        public async Task<Response> HandleRequest(Request request)
        {
            Response response = new Response();
            try
            {
                int len = request.Path.IndexOf("/", 1) - 1;
                string targetPath = request.Path.Substring(len + 1);
                string section = request.Path.Substring(1, len);
                string baseUrl = _hostMap.GetValue(section).ToString();

                var options = new RestClientOptions(baseUrl + targetPath)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var req = new RestRequest("", GetMethod(request.Method));

                string[] host_blackList = { "Host", ":method" };

                foreach (var header in request.Headers)
                {

                    if (host_blackList.Contains(header.Key))
                        continue;

                    try
                    {
                        req.AddHeader(header.Key, header.Value);
                    }
                    catch
                    {
                    }

                }
                byte[] data = Convert.FromBase64String(request.Body);
                req.AddBody(data);
                RestResponse res = await client.ExecuteAsync(req);


                if ((int)res.StatusCode == 0)
                {
                    response.StatusCode = 500;
                    response.ContentType = "text/html";
                    response.Body = Convert.ToBase64String(Encoding.UTF8.GetBytes("Gateway Internal Error : " + res.ErrorMessage));
                    response.RequestId = request.Id;

                }
                else
                {
                    response.StatusCode = (int)res.StatusCode;
                    response.ContentType = res.ContentType;
                    response.Body = Convert.ToBase64String(res.RawBytes);
                    response.RequestId = request.Id;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.ContentType = "text/html";
                response.Body = Convert.ToBase64String(Encoding.UTF8.GetBytes("Gateway Internal Error : " + ex.Message));
                response.RequestId = request.Id;
            }

            return response;

        }




        private Method GetMethod(string method)
        {
            switch (method)
            {
                case "GET": return Method.Get;
                case "POST": return Method.Post;
                case "PUT": return Method.Put;
                case "DELETE": return Method.Delete;
                case "HEAD": return Method.Get;
            }
            return Method.Get;
        }

    }
}
