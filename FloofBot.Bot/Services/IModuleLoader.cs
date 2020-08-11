using System;
using System.Threading.Tasks;

namespace FloofBot.Bot.Services
{
    public interface IModuleLoader : IFloofyService
    {
        Task Load(IServiceProvider serviceProvider);
    }
}