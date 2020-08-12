﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FloofBot.Bot.Attributes;
using FloofBot.TestModule.Services;

 namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        private CuddleService _cuddle;

        public TestCommand(CuddleService cuddle)
        {
            _cuddle = cuddle;
        }
        
        [FloofCommand, FloofAliases]
        public async Task Test(IUser user)
        {
            await Context.Channel.SendMessageAsync(_cuddle.Cuddle(user));
        }
    }
}