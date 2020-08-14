﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FloofBot.Core.Api;
using FloofBot.Core.Extensions;
using FloofBot.Core.Services;
using FloofBot.Core.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FloofBot.Core
{
    public class BotSetup
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IBotConfiguration _botConfiguration;
        private IServiceProvider _serviceProvider;

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
            
            await AddServices();
        }

        private async Task LoginAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _botConfiguration.Credentials.DiscordToken);
            await _client.StartAsync();
        }

        private async Task AddServices()
        {
            Stopwatch timer = Stopwatch.StartNew();
            
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSingleton(_client)
                .AddSingleton(_commandService);
            
            serviceCollection.LoadFrom(Assembly.GetAssembly(typeof(BotSetup)));
            
            string path = "Modules";
            
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                serviceCollection.LoadFrom(assembly);
            }
            
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _serviceProvider.GetService<ILocalization>();
            
            await _serviceProvider.GetService<IModuleLoader>()
                .LoadAsync(_serviceProvider);

            _serviceProvider.GetService<ICommandHandler>()
                .Start(_serviceProvider);
            
            _serviceProvider.GetService<GuildSetup>();

            await _serviceProvider.GetService<FloofyHost>()
                .StartAsync();
            
            timer.Stop();
            
            _serviceProvider.GetService<ILoggerProvider>()
                .GetLogger("Main")
                .LogInformation($"Loaded services in {timer.Elapsed:g}");
        }
    }
}