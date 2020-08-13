using Discord.Commands;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Database.Repositories;

namespace FloofBot.Core.Modules
{
    public class FloofyModule : ModuleBase<CommandContext>
    {
        private ILocalization _localization;
        private IDiscordGuildRepository _discordGuildRepository;

        public FloofyModule(ILocalization localization, IDiscordGuildRepository discordGuildRepository)
        {
            _localization = localization;
            _discordGuildRepository = discordGuildRepository;
        }

        protected string GetText(string locale, string key)
        {
            string word = _discordGuildRepository.GetLocalizationOverride(Context.Guild, locale, key);

            if (string.IsNullOrWhiteSpace(word))
            {
                word = _localization.GetString(locale, key);
            }

            return word;
        }
    }
}