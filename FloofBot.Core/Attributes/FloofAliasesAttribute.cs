using System.Runtime.CompilerServices;
using Discord.Commands;
using FloofBot.Core.Services.Implementation;

namespace FloofBot.Core.Attributes
{
    public class FloofAliasesAttribute : AliasAttribute
    {
        public FloofAliasesAttribute([CallerMemberName] string memberName = "") : base(BotConfiguration.GetCommand(memberName.ToLowerInvariant()).Aliases)
        {
            
        }
    }
}