using System.Linq;
using Discord;
using FloofBot.Core.Services.Database.Models;

namespace FloofBot.Core.Services.Database.Repositories
{
    public class DiscordUserRepository : Repository<DiscordUser>, IDiscordUserRepository
    {
        public DiscordUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public DiscordUser GetByDiscordId(ulong discordId)
        {
            return _dbSet.FirstOrDefault(x => x.DiscordId == discordId);
        }

        public void EnsureCreated(IUser user)
        {
            DiscordUser discordUser = GetByDiscordId(user.Id);

            if (discordUser == null)
            {
                discordUser = new DiscordUser
                {
                    DiscordId = user.Id,
                    Username = user.Username,
                    Discriminator = user.Discriminator
                };

                Add(discordUser);
            }
            else
            {
                discordUser.Username = user.Username;
                discordUser.Discriminator = user.Discriminator;
                
                Update(discordUser);
            }
        }
    }
}