using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using FloofBot.Core.Common;
using FloofBot.Core.Modules;
using FloofBot.Core.Services.Database.Repositories;

namespace FloofBot.Core.Services.Implementation
{
    public class CommandHandler : ICommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IServiceProvider _serviceProvider;
        private Logger _logger;
        private IDiscordUserRepository _discordUserRepository;
        private IModuleLoader _moduleLoader;

        public CommandHandler(DiscordSocketClient client, CommandService commandService, IServiceProvider serviceProvider, ILoggerProvider loggerProvider, IDiscordUserRepository discordUserRepository, IModuleLoader moduleLoader)
        {
            _client = client;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
            _logger = loggerProvider.GetLogger("Main");
            _discordUserRepository = discordUserRepository;
            _moduleLoader = moduleLoader;
        }

        public void Start(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _client.MessageReceived += Handle;
        }
        
        public async Task Handle(SocketMessage message)
        {
            try
            {
                if (!message.Author.IsBot && message is SocketUserMessage userMessage)
                {
                    _discordUserRepository.EnsureCreated(message.Author);
                    
                    _logger.LogDebug($"New message from {message.Author.Username}");
                    
                    ISocketMessageChannel channel = message.Channel;
                    SocketGuild guild = (message.Channel as SocketTextChannel)?.Guild;

                    if (userMessage.Content.StartsWith("f!"))
                    {
                        CommandContext context = new CommandContext(_client, userMessage);

                        SearchResult searchResult = _commandService.Search(context, userMessage.Content.Substring(2, userMessage.Content.Length - 2));
                        
                        if (!searchResult.IsSuccess)
                        {
                            _logger.LogDebug($"Search result is not success");
                            return;
                        }
                        
                        _logger.LogDebug($"Search result is success");

                        IReadOnlyList<CommandMatch> commands = searchResult.Commands;
                        Dictionary<CommandMatch, PreconditionResult> preconditionResults = new Dictionary<CommandMatch, PreconditionResult>();
                        
                        foreach (CommandMatch match in commands)
                        {
                            preconditionResults[match] = await match.Command.CheckPreconditionsAsync(context, _serviceProvider);
                        }

                        KeyValuePair<CommandMatch, PreconditionResult>[] succesfulPreconditions = preconditionResults
                            .Where(x => x.Value.IsSuccess)
                            .ToArray();

                        if (succesfulPreconditions.Length < 1)
                        {
                            _logger.LogDebug($"SuccesfulPreconditions is less than 1");
                            return;
                        }
                        
                        KeyValuePair<CommandMatch, PreconditionResult> precondition = succesfulPreconditions.FirstOrDefault();

                        string commandName = precondition.Key.Command.Name;
                        IModuleManifest manifest = _moduleLoader.CommandMap
                            .FirstOrDefault(x => x.Value.Contains(commandName)).Key;

                        if (manifest == null)
                        {
                            _logger.LogError("Trying to execute command of an unknown module." +
                                             $"{Environment.NewLine}Content: {userMessage.Content}" +
                                             $"{Environment.NewLine}User: {userMessage.Author.Mention}" +
                                             $"{Environment.NewLine}Guild: {guild?.Id}");
                            return;
                        }
                        
                        _logger.LogDebug($"Executing a command of the {manifest.Name} module.");
                        
                        ParseResult parseResult = await precondition.Key.ParseAsync(context, searchResult, precondition.Value, _serviceProvider);

                        _logger.LogDebug("Executing command");
                        await precondition.Key.ExecuteAsync(context, parseResult, _serviceProvider);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in command handler: {Environment.NewLine}{e}");
            }
        }
    }
}