using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMDomain.Model
{
    public class Request
    {
        public long Id { get; set; } 
        public string Method { get; set; }
        public string Path { get; set; }
        public string Body { get; set; }
        public HeaderModel[] Headers { get; set; }
    }
}
