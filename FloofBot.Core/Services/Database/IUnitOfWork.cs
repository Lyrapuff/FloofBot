using System.Threading.Tasks;

namespace FloofBot.Core.Services.Database
{
    public interface IUnitOfWork
    {
        FloofyContext Context { get; set; }
        
        void Commit();
        Task CommitAsync();
        void Revert();
        Task RevertAsync();
    }
}