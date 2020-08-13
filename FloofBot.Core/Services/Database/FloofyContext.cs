using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FloofBot.Core.Services.Database.Models;
using FloofBot.Core.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FloofBot.Core.Services.Database
{
    public class FloofyContextFactory : IDesignTimeDbContextFactory<FloofyContext>
    {
        public FloofyContextFactory()
        {
            
        }
        
        public FloofyContext CreateDbContext(string[] args)
        {
            IBotConfiguration botConfiguration = new BotConfiguration();

            DbContextOptions<FloofyContext> options = new DbContextOptionsBuilder<FloofyContext>()
                .UseNpgsql(botConfiguration.DatabaseConnectionString)
                .Options;
            
            return new FloofyContext(options);
        }
    }
    
    public sealed class FloofyContext : DbContext
    {
        public DbSet<DiscordUser> DiscordUsers { get; set; }
        public DbSet<DiscordGuild> DiscordGuilds { get; set; }
        public DbSet<LocalizationOverride> LocalizationOverrides { get; set; }

        public FloofyContext(DbContextOptions<FloofyContext> options) : base(options)
        {
            Database.EnsureCreated();
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