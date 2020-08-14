using System.Runtime.CompilerServices;
using Discord.Commands;
using FloofBot.Core.Services.Implementation;

namespace FloofBot.Core.Attributes
{
    public class FloofyAliasesAttribute : AliasAttribute
    {
        public FloofyAliasesAttribute([CallerMemberName] string memberName = "") : base(BotConfiguration.GetCommand(memberName.ToLowerInvariant()).Aliases)
        {
            
        }
    }
}