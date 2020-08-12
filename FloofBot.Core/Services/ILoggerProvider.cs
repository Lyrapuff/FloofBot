using FloofBot.Core.Common;

namespace FloofBot.Core.Services
{
    public interface ILoggerProvider : IFloofyService
    {
        Logger GetLogger(string name);
    }
}