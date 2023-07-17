using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMDomain.Model
{
    public class Response
    {
        public long RequestId { get; set; }
        public int StatusCode { get; set; }
        public string Body { get; set; }
    }
}
