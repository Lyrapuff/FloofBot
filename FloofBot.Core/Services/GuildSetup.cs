using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.WebSocket;
using FloofBot.Core.Common;
using FloofBot.Core.Services.Database.Repositories;

namespace FloofBot.Core.Services
{
    public class GuildSetup : IFloofyService
    {
        private DiscordSocketClient _client;
        private Logger _logger;
        private IDiscordGuildRepository _discordGuildRepository;

        public GuildSetup(DiscordSocketClient client, ILoggerProvider loggerProvider, IDiscordGuildRepository discordGuildRepository)
        {
            _client = client;
            _logger = loggerProvider.GetLogger("Main");
            _discordGuildRepository = discordGuildRepository;

            _client.JoinedGuild += HandleJoinedGuild;
            CheckGuilds();
        }

        private void CheckGuilds()
        {
            Stopwatch timer = Stopwatch.StartNew();
            
            foreach (SocketGuild guild in _client.Guilds)
            {
                _discordGuildRepository.EnsureCreated(guild);
            }
            
            int count = _client.Guilds.Count;
            
            timer.Stop();

            _logger.LogInformation($"Checked {count} {(count == 1 ? "guild" : "guilds")} in {timer.Elapsed:g}");
        }
        
        private async Task HandleJoinedGuild(SocketGuild guild)
        {
            _discordGuildRepository.EnsureCreated(guild);
        }
    }
}