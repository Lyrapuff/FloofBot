namespace FloofBot.Core.Services.Database.Models
{
    public class LocalizationOverride : DbEntity
    {
        public string Locale { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}