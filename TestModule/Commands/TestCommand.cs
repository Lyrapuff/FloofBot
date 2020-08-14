﻿using System.Threading.Tasks;
using Discord;
using FloofBot.Core.Attributes;
using FloofBot.Core.Modules;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Database.Repositories;

namespace FloofBot.TestModule.Commands
{
    public class TestCommand : FloofyModule
    {
        public TestCommand(ILocalization localization, IDiscordGuildRepository discordGuildRepository) : base(localization, discordGuildRepository)
        {
        }

        [FloofyCommand, FloofyAliases]
        public async Task Test()
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle(GetText("cat"))
                .WithDescription("1321");
            
            await Context.Channel.SendMessageAsync("",false, embed.Build());
        }
    }
}