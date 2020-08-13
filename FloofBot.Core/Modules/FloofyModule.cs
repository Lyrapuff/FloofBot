using System;
using Discord.Commands;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Database.Models;
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

        protected string GetText(string wordKey)
        {
            string localeKey = _discordGuildRepository.GetLocaleKey(Context.Guild);
            
            string word = _discordGuildRepository.GetLocalizationOverride(Context.Guild, localeKey, wordKey);

            if (string.IsNullOrWhiteSpace(word))
            {
                word = _localization.GetString(localeKey, wordKey);
            }

            return word;
        }
    }
}