using System.Threading.Tasks;
using FloofBot.Web.Services.Responses;

namespace FloofBot.Web.Services
{
    public interface IDiscordApi
    {
        Task<TokenExchangeResponse> TokenExchange(string code);
    }
}