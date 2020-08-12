namespace FloofBot.Core.Services.Database.Models
{
    public class DiscordUser : DbEntity
    {
        public ulong DiscordId { get; set; }
        public string Username { get; set; }
        public string Discriminator { get; set; }
    }
}