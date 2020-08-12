using Discord;
using FloofBot.Core.Services;

namespace FloofBot.TestModule.Services
{
    public class CuddleService : IFloofyService
    {
        public string Cuddle(IUser user)
        {
            return $"*cuddles {user.Username}*";
        }
    }
}