using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMClientService.Models
{
    public class AppConfig
    {
        public AppConfig(IConfiguration configuration) 
        {
            HubKey = configuration.GetSection("HubKey").Value;
            HubKeyHeaderName = configuration.GetSection("HubKeyHeaderName").Value;
            HubURL = configuration.GetSection("HubURL").Value;
            HubListenFunctionName = configuration.GetSection("HubListenFunctionName").Value;
            HubInvokeFunctionName = configuration.GetSection("HubInvokeFunctionName").Value;
        }

        public string HubURL { get; set; }
        public string HubInvokeFunctionName { get; set; }
        public string HubListenFunctionName { get; set; }
        public string HubKey { get; set; }
        public string HubKeyHeaderName { get; set; }
    }
}
