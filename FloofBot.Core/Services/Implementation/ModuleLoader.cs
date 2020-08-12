using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Core.Common;
using FloofBot.Core.Modules;

namespace FloofBot.Core.Services.Implementation
{
    public class ModuleLoader : IModuleLoader
    {
        private CommandService _commandService;
        private Logger _logger;
        
        public ModuleLoader(CommandService commandService, ILoggerProvider _loggerProvider)
        {
            _commandService = commandService;
            _logger = _loggerProvider.GetLogger("Main");
        }

        public async Task Load(IServiceProvider serviceProvider)
        {
            try
            {
                Stopwatch time = Stopwatch.StartNew();
                int count = 0;

                string path = "Modules";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
                {
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                    Type manifestType = assembly
                        .GetTypes()
                        .FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IModuleManifest)));

                    if (manifestType != null)
                    {
                        if (Activator.CreateInstance(manifestType) is IModuleManifest manifest)
                        {
                            await _commandService.AddModulesAsync(assembly, serviceProvider);
                            _logger.LogInformation($"Loaded module {manifest.Name} of version {manifest.Version}");
                            count++;
                        }
                    }
                }

                time.Stop();

                _logger.LogInformation($"Loaded {count} {(count == 1 ? "module" : "modules")} in {time.Elapsed}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ModuleLoader {Environment.NewLine}{e}");
            }
        }
    }
}