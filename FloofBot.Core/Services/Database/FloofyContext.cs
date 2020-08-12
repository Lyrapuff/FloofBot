using FloofBot.Core.Services.Database.Models;
using FloofBot.Core.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace FloofBot.Core.Services.Database
{
    public class FloofyContext : DbContext
    {
        public DbSet<DiscordUser> DiscordUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(BotConfiguration.Instance.DatabaseConnectionString);
        }
    }
}