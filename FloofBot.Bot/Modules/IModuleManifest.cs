namespace FloofBot.Bot.Modules
{
    public interface IModuleManifest
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }
    }
}