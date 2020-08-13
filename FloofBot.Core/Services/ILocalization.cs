namespace FloofBot.Core.Services
{
    public interface ILocalization : IFloofyService
    {
        string GetString(string locale, string key);
    }
}