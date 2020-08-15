using System;
using System.Threading.Tasks;
using FloofBot.Web.Models;
using FloofBot.Web.Services;
using FloofBot.Web.Services.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FloofBot.Web.Controllers
{
    public class LoginController : ControllerBase
    {
        private IDiscordApi _discordApi;
        private DiscordCredentials _discordCredentials;

        public LoginController(IDiscordApi discordApi, IOptions<DiscordCredentials> discordCredentials)
        {
            _discordApi = discordApi;
            _discordCredentials = discordCredentials.Value;
        }
        
        public IActionResult Index()
        {
            string clientId = _discordCredentials.ClientId;
            string redirectUrl = _discordCredentials.RedirectUrl;
            
            string url = $"https://discord.com/api/oauth2/authorize?client_id={clientId}&redirect_url={redirectUrl}&response_type=code&scope=identify guilds";

            return Redirect(url);
        }
        
        public async Task<IActionResult> Callback(string code)
        {
            TokenExchangeResponse response = await _discordApi.TokenExchange(code);
            
            Console.WriteLine($"token: {response.AccessToken}");
            
            return Redirect("/");
        }
    }
}