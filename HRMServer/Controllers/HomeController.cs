using HRMServer.Model;
using HRMServer.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMServer.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly TelegramService _telegramService;
        public HomeController(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

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

        [HttpPost("Publisher")]
        public async Task<IActionResult> SendMessage([FromBody] TelegramSendMessageModel model)
        {

            if(!_telegramService.CHeckKey(model.Key))
                return BadRequest("Failed to send message.");

            bool result = await _telegramService.SendMessageAsync(model.botToken, model.chatId, model.messageText);

            if (result)
            {
                return Ok("Message sent successfully!");
            }

            return BadRequest("Can not send message.");

        }



    }
}
