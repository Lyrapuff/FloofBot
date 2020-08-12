using FloofBot.Bot.Common;

namespace FloofBot.Bot.Services
{
    public interface ILoggerProvider : IFloofyService
    {
        Logger GetLogger(string name);
    }
}