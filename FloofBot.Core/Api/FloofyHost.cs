using System.Threading.Tasks;
using FloofBot.Core.Services;
using Microsoft.AspNetCore.Hosting;

namespace FloofBot.Core.Api
{
    public class FloofyHost : IFloofyService
    {
        public async Task StartAsync()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>();

            IWebHost host = hostBuilder.Build();
            await host.StartAsync().ConfigureAwait(false);
        }
    }
}