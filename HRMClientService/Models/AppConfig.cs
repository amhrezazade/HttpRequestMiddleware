using Newtonsoft.Json.Linq;
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
            HostMapping = JObject.Parse(File.ReadAllText("HostMap.json"));
            HubKey = configuration.GetSection("HubKey").Value;
            HubKeyHeaderName = configuration.GetSection("HubKeyHeaderName").Value;
            HubURL = configuration.GetSection("HubURL").Value;
            HubListenFunctionName = configuration.GetSection("HubListenFunctionName").Value;
            HubInvokeFunctionName = configuration.GetSection("HubInvokeFunctionName").Value;
            LoopDelay = int.Parse(configuration.GetSection("LoopDelay").Value);
        }

        public string HubURL { get; }
        public string HubInvokeFunctionName { get; }
        public string HubListenFunctionName { get; }
        public string HubKey { get; }
        public string HubKeyHeaderName { get;}
        public int LoopDelay { get; }
        public JObject HostMapping { get; }
    }
}
