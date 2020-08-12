using FloofBot.Core.Modules;

namespace FloofBot.TestModule
{
    public class Manifest : IModuleManifest
    {
        public string Name { get; set; } = "TestModule";
        public string Description { get; set; } = "A module for testing. Lol.";
        public string Version { get; set; } = "0";
    }
}