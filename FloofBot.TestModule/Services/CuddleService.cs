using Discord;
using FloofBot.Bot.Services;

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