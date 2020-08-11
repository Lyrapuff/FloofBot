﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace FloofBot.Bot.Services.Implementation
{
    public class CommandHandler : ICommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IServiceProvider _serviceProvider;

        public CommandHandler(DiscordSocketClient client, CommandService commandService)
        {
            
            _client = client;
            _commandService = commandService;
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
                    Console.WriteLine("hello");
                    
                    ISocketMessageChannel channel = message.Channel;
                    SocketGuild guild = (message.Channel as SocketTextChannel)?.Guild;

                    if (userMessage.Content.StartsWith("f!"))
                    {
                        
                        CommandContext context = new CommandContext(_client, userMessage);

                        SearchResult searchResult = _commandService.Search(context, userMessage.Content.Substring(2, userMessage.Content.Length - 2));
                        
                        if (!searchResult.IsSuccess)
                        {
                            return;
                        }

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
                            return;
                        }
                        
                        KeyValuePair<CommandMatch, PreconditionResult> precondition = succesfulPreconditions.FirstOrDefault();

                        ParseResult parseResult = await precondition.Key.ParseAsync(context, searchResult, precondition.Value, _serviceProvider);

                        await precondition.Key.ExecuteAsync(context, parseResult, _serviceProvider);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: log
            }
        }
    }
}