using Discord;
using FloofBot.Core.Services.Database.Models;

namespace FloofBot.Core.Services.Database.Repositories
{
    public interface IDiscordUserRepository : IRepository<DiscordUser>
    {
        DiscordUser GetByDiscordId(ulong discordId);
        void EnsureCreated(IUser user);
    }
}