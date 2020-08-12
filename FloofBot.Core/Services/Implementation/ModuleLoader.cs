using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Core.Attributes;
using FloofBot.Core.Common;
using FloofBot.Core.Modules;

namespace FloofBot.Core.Services.Implementation
{
    public class ModuleLoader : IModuleLoader
    {
        public Dictionary<IModuleManifest, HashSet<string>> CommandMap { get; } = new Dictionary<IModuleManifest, HashSet<string>>();

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
                int commandCount = 0;

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
                            IEnumerable<MethodInfo> commandMethods = assembly
                                .GetTypes()
                                .SelectMany(x => x.GetMethods())
                                .Where(x => x.GetCustomAttributes(typeof(FloofCommandAttribute)).Any());
                            
                            HashSet<string> commands = new HashSet<string>();
                            
                            foreach (MethodInfo method in commandMethods)
                            {
                                FloofCommandAttribute attribute =
                                    method.GetCustomAttributes(typeof(FloofCommandAttribute)).FirstOrDefault() as FloofCommandAttribute;

                                if (attribute != null)
                                {
                                    commands.Add(attribute.Text);
                                    commandCount++;
                                }
                            }

                            CommandMap[manifest] = commands;
                            
                            await _commandService.AddModulesAsync(assembly, serviceProvider);

                            _logger.LogInformation($"Loaded module {manifest.Name} of version {manifest.Version} with {commands.Count} {(commands.Count == 1 ? "command" : "commands")}");
                            count++;
                        }
                    }
                }

                time.Stop();

                _logger.LogInformation($"Loaded {count} {(count == 1 ? "module" : "modules")} with {commandCount} {(commandCount == 1 ? "command" : "commands")} in {time.Elapsed}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ModuleLoader {Environment.NewLine}{e}");
            }
        }
    }
}