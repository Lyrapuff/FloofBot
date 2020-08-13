using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Core.Attributes;
using FloofBot.Core.Services;

namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        private ILocalization _localization;

        public TestCommand(ILocalization localization)
        {
            _localization = localization;
        }

        [FloofCommand, FloofAliases]
        public async Task Test([Remainder]string locale = "en")
        {
            await Context.Channel.SendMessageAsync(_localization.GetString(locale, "cat"));
        }
    }
}