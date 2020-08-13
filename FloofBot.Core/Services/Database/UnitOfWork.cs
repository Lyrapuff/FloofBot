using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FloofBot.Core.Services.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public FloofyContext Context { get; set; }

        public UnitOfWork(IBotConfiguration botConfiguration)
        {
            DbContextOptions<FloofyContext> options = new DbContextOptionsBuilder<FloofyContext>()
                .UseNpgsql(botConfiguration.DatabaseConnectionString)
                .Options;

            Context = new FloofyContext(options);
        }
        
        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Revert()
        {
            Context.Dispose();
        }

        public async Task RevertAsync()
        {
            await Context.DisposeAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}