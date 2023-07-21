using HRMClientService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMClientService.Service
{
    public class ConfigHelperService
    {
        private readonly AppConfig _appConfig;
        private readonly JObject _hostMap;
        public ConfigHelperService(IConfiguration configuration) 
        {
            _hostMap = JObject.Parse(File.ReadAllText("HostMap.json"));
            _appConfig = new AppConfig(configuration);
        }

        public JObject HostMapping =>  _hostMap;

        public AppConfig AppConfig => _appConfig;

    }
}
