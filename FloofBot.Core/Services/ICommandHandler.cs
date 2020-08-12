using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace FloofBot.Core.Services
{
    public interface ICommandHandler : IFloofyService
    {
        Task Handle(SocketMessage message);
        void Start(IServiceProvider serviceProvider);
    }
}