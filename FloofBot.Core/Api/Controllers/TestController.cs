using Discord.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace FloofBot.Core.Api.Controllers
{
    [ApiController]
    [Route("/api/test")]
    public class TestController : ControllerBase
    {
        private DiscordSocketClient _client;

        public TestController(DiscordSocketClient client)
        {
            _client = client;
        }
        
        public IActionResult Index()
        {
            return Ok($"Username: {_client.CurrentUser.Username}");
        }
    }
}