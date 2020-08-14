using System.Threading.Tasks;
using FloofBot.Core;

namespace FloofBot.Starter
{
    internal static class Program
    {
        public static async Task Main()
        {
            await new BotSetup().SetupAsync();
            await Task.Delay(-1);
        }
    }
}