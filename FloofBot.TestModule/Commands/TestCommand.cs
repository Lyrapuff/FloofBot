using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Bot.Attributes;
using FloofBot.Bot.Services;

namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        private IBotConfiguration _botConfiguration;

        public TestCommand(IBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
        }
        
        [FloofCommand("test")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync(string.Format("my token is {0}", _botConfiguration.Credentials.DiscordToken));
        }
    }
}