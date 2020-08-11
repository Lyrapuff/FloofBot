using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FloofBot.Bot.Extensions;
using FloofBot.Bot.Services;
using FloofBot.Bot.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FloofBot.Bot
{
    public class BotSetup
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IBotConfiguration _botConfiguration;
        private IServiceProvider _serviceProvider;

        public async Task Setup()
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

            await _commandService.AddModulesAsync(Assembly.GetAssembly(typeof(BotSetup)), _serviceProvider);

            await LoginAsync();
            AddServices();
        }

        private async Task LoginAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _botConfiguration.Credentials.DiscordToken);
            await _client.StartAsync();
        }

        private void AddServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSingleton(_client)
                .AddSingleton(_commandService);
            
            serviceCollection.LoadFrom(Assembly.GetAssembly(typeof(BotSetup)));

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _serviceProvider.GetService<IModuleLoader>()
                .Load(_serviceProvider);
            
            _serviceProvider.GetService<ICommandHandler>()
                .Start(_serviceProvider);
        }
    }
}