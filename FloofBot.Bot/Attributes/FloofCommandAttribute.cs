using System;
using System.Runtime.CompilerServices;
using Discord.Commands;

namespace FloofBot.Bot.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FloofCommandAttribute : CommandAttribute
    {
        public FloofCommandAttribute(string name) : base(name)
        {
            
        }
    }
}