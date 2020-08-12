using System.Runtime.CompilerServices;
using Discord.Commands;
using FloofBot.Bot.Services.Implementation;

namespace FloofBot.Bot.Attributes
{
    public class FloofAliasesAttribute : AliasAttribute
    {
        public FloofAliasesAttribute([CallerMemberName] string memberName = "") : base(BotConfiguration.GetCommand(memberName.ToLowerInvariant()).Aliases)
        {
            
        }
    }
}