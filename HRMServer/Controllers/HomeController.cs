using Microsoft.AspNetCore.Mvc;

namespace HRMServer.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public ContentResult Get()
        {
            return new ContentResult()
            {
                StatusCode = 200,
                Content = System.IO.File.ReadAllText("Home.html"),
                ContentType = "text/html"
            };
        }


        [HttpGet]
        [Route("/end")]
        public string End([FromQuery] string key)
        {
            return "";

        }

    }
}
