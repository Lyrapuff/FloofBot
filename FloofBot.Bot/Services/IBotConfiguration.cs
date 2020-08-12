using System.Collections.Generic;

namespace FloofBot.Bot.Services
{
    public interface IBotConfiguration : IFloofyService
    {
        string LogLevel { get; set; }
        BotCredentials Credentials { get; set; }
        Dictionary<string, Command> Commands { get; set; }
    }

    public class BotCredentials
    {
        public string DiscordToken { get; set; } = "";
    }

    public class Command
    {
        public string Name { get; set; } = "";
        public string[] Aliases { get; set; } = new string[0];
    }
}