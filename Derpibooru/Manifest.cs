using System;
using FloofBot.Core.Modules;

namespace Derpibooru
{
    public class Manifest : IModuleManifest
    {
        public string Name { get; set; } = "Image search";
        public string Description { get; set; } = "Image searching on popular websites.";
        public Version Version { get; set; } = new Version(1, 0, 0, 0);
    }
}