using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FloofBot.Core.Attributes;
using FloofBot.Core.Services.Database.Repositories;

 namespace FloofBot.TestModule.Commands
{
    public class TestCommand : ModuleBase<CommandContext>
    {
        private IDiscordUserRepository _discordUserRepository;

        public TestCommand(IDiscordUserRepository discordUserRepository)
        {
            _discordUserRepository = discordUserRepository;
        }
        
        [FloofCommand, FloofAliases]
        public async Task Test(IUser user)
        {
            _discordUserRepository.EnsureCreated(user);
            await Context.Channel.SendMessageAsync($"{user.Username}'s db id is {_discordUserRepository.GetByDiscordId(user.Id).Id}");
        }
    }
}