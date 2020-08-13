using System.Collections.Generic;
using System.Linq;
using FloofBot.Core.Services.Database.Models;
using FloofBot.Core.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FloofBot.Core.Services.Database
{
    public sealed class FloofyContext : DbContext
    {
        public DbSet<DiscordUser> DiscordUsers { get; set; }
        public DbSet<DiscordGuild> DiscordGuilds { get; set; }

        public FloofyContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(BotConfiguration.Instance.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ValueConverter<List<string>, string> stringCollection =
                new ValueConverter<List<string>, string>(x => string.Join(";", x), x => x.Split(new[] {';'}).ToList());

            modelBuilder.Entity<DiscordGuild>()
                .Property(nameof(DiscordGuild.DisabledModules))
                .HasConversion(stringCollection);
        }
    }
}