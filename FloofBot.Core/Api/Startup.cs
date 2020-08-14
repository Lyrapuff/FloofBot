using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using FloofBot.Core.Extensions;
using FloofBot.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FloofBot.Core.Api
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection serviceCollection)
        {
            Stopwatch timer = Stopwatch.StartNew();

            serviceCollection.AddMvcCore();
            
            serviceCollection.LoadFrom(Assembly.GetAssembly(typeof(BotSetup)));
            
            string path = "Modules";
            
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                serviceCollection.LoadFrom(assembly);
            }
            
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<ILocalization>();
            
            serviceProvider.GetService<IModuleLoader>()
                .Load(serviceProvider);

            serviceProvider.GetService<ICommandHandler>()
                .Start(serviceProvider);
            
            serviceProvider.GetService<GuildSetup>();

            timer.Stop();
            
            serviceProvider.GetService<ILoggerProvider>()
                .GetLogger("Main")
                .LogInformation($"Loaded services in {timer.Elapsed:g}");

            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}