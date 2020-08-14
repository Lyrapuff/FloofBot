using System;
using System.Threading.Tasks;
using Derpibooru.Models;
using Derpibooru.Services;
using Discord;
using Discord.Commands;
using FloofBot.Core.Attributes;
using FloofBot.Core.Modules;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Database.Repositories;

namespace Derpibooru.Commands
{
    public class DerpibooruModule : FloofyModule
    {
        private DerpibooruService _derpibooru;
        
        public DerpibooruModule(ILocalization localization, IDiscordGuildRepository discordGuildRepository, DerpibooruService derpibooru)
            : base(localization, discordGuildRepository)
        {
            _derpibooru = derpibooru;
        }

        [FloofyCommand, FloofyAliases]
        public async Task DerpibooruSearch([Remainder] string query)
        {
            DerpiImage[] images = await _derpibooru.Search(query);

            DerpiImage image = images[new Random().Next(0, images.Length)];
            
            string text = GetReplyBuilder().GetString(GetText("derp_imagesearch"));
            
            EmbedBuilder embedBuilder = new EmbedBuilder()
                .WithTitle(text)
                .WithImageUrl(image.ViewUrl);
            
            await Context.Channel.SendMessageAsync("", false, embedBuilder.Build());
        }
    }
}