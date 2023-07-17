using Microsoft.AspNetCore.Mvc;

namespace HRMServer.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Hello from web app";
        }


        [HttpGet]
        [Route("/end")]
        public string End([FromQuery] string key)
        {
            return "";

        }

    }
}
