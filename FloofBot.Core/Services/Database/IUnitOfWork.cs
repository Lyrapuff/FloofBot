using System;
using System.Threading.Tasks;

namespace FloofBot.Core.Services.Database
{
    public interface IUnitOfWork : IFloofyService, IDisposable
    {
        FloofyContext Context { get; set; }
        
        void Commit();
        Task CommitAsync();
        void Revert();
        Task RevertAsync();
    }
}