using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FloofBot.Core.Modules;

namespace FloofBot.Core.Services
{
    public interface IModuleLoader : IFloofyService
    {
        Dictionary<IModuleManifest, HashSet<string>> CommandMap { get; }
        Task Load(IServiceProvider serviceProvider);
    }
}