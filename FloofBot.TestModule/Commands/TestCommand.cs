using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Bot.Attributes;

namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        [FloofCommand("test")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync("mew!");
        }
    }
}