using System;
using System.Threading.Tasks;

namespace FloofBot.Core.Services
{
    public interface IModuleLoader : IFloofyService
    {
        Task Load(IServiceProvider serviceProvider);
    }
}