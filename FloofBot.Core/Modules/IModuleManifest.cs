using System;

namespace FloofBot.Core.Modules
{
    public interface IModuleManifest
    {
        string Name { get; set; }
        string Description { get; set; }
        Version Version { get; set; }
    }
}