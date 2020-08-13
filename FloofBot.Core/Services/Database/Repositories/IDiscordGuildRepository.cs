using Discord;
using FloofBot.Core.Services.Database.Models;

namespace FloofBot.Core.Services.Database.Repositories
{
    public interface IDiscordGuildRepository : IRepository<DiscordGuild>
    {
        DiscordGuild GetByDiscordId(ulong discordId);
        void EnsureCreated(IGuild guild);
        void EnableModule(IGuild guild, string moduleName);
        void DisableModule(IGuild guild, string moduleName);
        void ToggleModule(IGuild guild, string moduleName);
        bool IsModuleEnabled(IGuild guild, string moduleName);
    }
}