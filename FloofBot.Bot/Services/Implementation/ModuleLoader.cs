using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using FloofBot.Bot.Modules;

namespace FloofBot.Bot.Services.Implementation
{
    public class ModuleLoader : IModuleLoader
    {
        private CommandService _commandService;
        
        public ModuleLoader(CommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task Load(IServiceProvider serviceProvider)
        {
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
                        Console.WriteLine("Loaded module {0} of version {1}", manifest.Name, manifest.Version);
                    }
                }
            }
        }
    }
}