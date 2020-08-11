using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace FloofBot.Bot.Services
{
    public interface ICommandHandler : IFloofyService
    {
        Task Handle(SocketMessage message);
        void Start(IServiceProvider serviceProvider);
    }
}