namespace FloofBot.Bot.Services
{
    public interface IBotConfiguration
    {
        BotCredentials Credentials { get; set; }
    }

    public class BotCredentials
    {
        public string DiscordToken { get; set; }
    }
}