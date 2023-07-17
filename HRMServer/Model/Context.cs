using HRMDomain.Model;

namespace HRMServer.Model
{
    public class Context
    {
        public long Id { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
    }
}
