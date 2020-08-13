using System.Collections.Generic;

namespace FloofBot.Core.Services.Database.Models
{
    public class DiscordGuild : DbEntity
    {
        public ulong DiscordId { get; set; }
        public List<string> DisabledModules { get; set; } = new List<string>();
        public List<LocalizationOverride> LocalizationOverrides { get; set; } = new List<LocalizationOverride>();
    }
}