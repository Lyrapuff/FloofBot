using System.Linq;
using Discord;
using FloofBot.Core.Services.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FloofBot.Core.Services.Database.Repositories
{
    public class DiscordGuildRepository : Repository<DiscordGuild>, IDiscordGuildRepository
    {
        public DiscordGuildRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public DiscordGuild GetByDiscordId(ulong discordId)
        {
            return _dbSet
                .Include(x => x.LocalizationOverrides)
                .FirstOrDefault(x => x.DiscordId == discordId);
        }

        public void EnsureCreated(IGuild guild)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);

            if (discordGuild == null)
            {
                discordGuild = new DiscordGuild
                {
                    DiscordId = guild.Id
                };

                _dbSet.Add(discordGuild);
                _unitOfWork.Commit();
            }
        }

        public void EnableModule(IGuild guild, string moduleName)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);

            if (discordGuild == null)
            {
                return;
            }
            
            if (discordGuild.DisabledModules.Contains(moduleName))
            {
                discordGuild.DisabledModules.Remove(moduleName);

                Update(discordGuild);
                _unitOfWork.Commit();
            }
        }

        public void DisableModule(IGuild guild, string moduleName)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);

            if (discordGuild == null)
            {
                return;
            }
            
            if (!discordGuild.DisabledModules.Contains(moduleName))
            {
                discordGuild.DisabledModules.Add(moduleName);

                Update(discordGuild);
                _unitOfWork.Commit();
            }
        }

        public void ToggleModule(IGuild guild, string moduleName)
        {
            if (IsModuleEnabled(guild, moduleName))
            {
                DisableModule(guild, moduleName);
            }
            else
            {
                EnableModule(guild, moduleName);
            }
        }

        public bool IsModuleEnabled(IGuild guild, string moduleName)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);

            if (discordGuild == null)
            {
                return false;
            }
            
            return !discordGuild.DisabledModules.Contains(moduleName);
        }

        public string GetLocalizationOverride(IGuild guild, string locale, string key)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);
            
            if (discordGuild == null)
            {
                return "";
            }

            string word = discordGuild.LocalizationOverrides
                .FirstOrDefault(x => x.Locale == locale && x.Key == key)
                ?.Value;

            return word;
        }

        public string GetLocaleKey(IGuild guild)
        {
            DiscordGuild discordGuild = GetByDiscordId(guild.Id);

            return discordGuild == null ? "en" : discordGuild.LocaleKey;
        }
    }
}