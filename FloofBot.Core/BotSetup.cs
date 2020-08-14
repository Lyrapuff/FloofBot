using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FloofBot.Core.Api;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FloofBot.Core
{
    public class BotSetup
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IBotConfiguration _botConfiguration;

        public async Task SetupAsync()
        {
            _botConfiguration = new BotConfiguration();
            
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                ConnectionTimeout = int.MaxValue
            });
            
            _commandService = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Sync
            });

            await LoginAsync();

            await StartWebHost();
        }

        private async Task LoginAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _botConfiguration.Credentials.DiscordToken);
            await _client.StartAsync();
        }

        private async Task StartWebHost()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .ConfigureServices(x =>
                {
                    x.AddSingleton(_client);
                    x.AddSingleton(_commandService);
                })
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>();

            IWebHost host = hostBuilder.Build();
            await host.StartAsync().ConfigureAwait(false);
        }
    }
}