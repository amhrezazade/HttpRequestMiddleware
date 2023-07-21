using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_Client
{
    public class AppConfig
    {
        public string HubURL { get; set; }
        public string HubInvokeFunctionName { get; set; }
        public string HubListenFunctionName { get; set; }
        public string HubKey { get; set; }
    }
}
