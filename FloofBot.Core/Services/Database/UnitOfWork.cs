using System.Threading.Tasks;

namespace FloofBot.Core.Services.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public FloofyContext Context { get; set; }

        public UnitOfWork()
        {
            Context = new FloofyContext();
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