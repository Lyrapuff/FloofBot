﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FloofBot.Core.Attributes;
using FloofBot.TestModule.Services;

 namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        private CuddleService _cuddle;

        public TestCommand(CuddleService cuddle)
        {
            Console.WriteLine("ctor");
            _cuddle = cuddle;
        }
        
        [FloofCommand, FloofAliases]
        public async Task Test(IUser user)
        {
            Console.WriteLine("Command");
            await Context.Channel.SendMessageAsync(_cuddle.Cuddle(user));
        }
    }
}