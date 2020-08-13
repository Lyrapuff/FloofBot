using System.Threading.Tasks;
using FloofBot.Core.Attributes;
using FloofBot.Core.Modules;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Database.Repositories;

namespace FloofBot.TestModule.Commands
{
    public class TestCommand : FloofyModule
    {
        public TestCommand(ILocalization localization, IDiscordGuildRepository discordGuildRepository) : base(localization, discordGuildRepository)
        {
        }

        [FloofCommand, FloofAliases]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync(GetText("cat"));
        }
    }
}