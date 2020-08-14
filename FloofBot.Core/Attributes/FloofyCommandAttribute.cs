using System;
using System.Runtime.CompilerServices;
using Discord.Commands;
using FloofBot.Core.Services.Implementation;

namespace FloofBot.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FloofyCommandAttribute : CommandAttribute
    {
        public FloofyCommandAttribute([CallerMemberName] string memberName = "") : base(BotConfiguration.GetCommand(memberName.ToLowerInvariant()).Name)
        {
            
        }
    }
}