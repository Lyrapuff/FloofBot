using Newtonsoft.Json;

namespace FloofBot.Bot.Services
{
    public interface IBotConfiguration : IFloofyService
    {
        [JsonProperty]
        BotCredentials Credentials { get; set; }
    }

    public class BotCredentials
    {
        [JsonProperty]
        public string DiscordToken { get; set; } = "";
    }
}