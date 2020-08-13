using System.Threading.Tasks;
using Discord.Commands;
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
        public async Task Test([Remainder]string locale = "en")
        {
            await Context.Channel.SendMessageAsync(GetText(locale, "cat"));
        }
    }
}