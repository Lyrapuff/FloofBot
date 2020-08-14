using Microsoft.AspNetCore.Mvc;

namespace FloofBot.Core.Api.Controllers
{
    [ApiController]
    [Route("/api/test")]
    public class TestController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Asshole!");
        }
    }
}