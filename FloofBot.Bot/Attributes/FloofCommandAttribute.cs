using System;
using System.Runtime.CompilerServices;
using Discord.Commands;
using FloofBot.Bot.Services.Implementation;

namespace FloofBot.Bot.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FloofCommandAttribute : CommandAttribute
    {
        public FloofCommandAttribute([CallerMemberName] string memberName = "") : base(BotConfiguration.GetCommand(memberName.ToLowerInvariant()).Name)
        {
            
        }
    }
}