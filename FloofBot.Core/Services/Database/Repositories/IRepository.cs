using System.Collections.Generic;
using FloofBot.Core.Services.Database.Models;

namespace FloofBot.Core.Services.Database.Repositories
{
    public interface IRepository<T> : IFloofyService where T : DbEntity
    {
        void Add(T obj);
        void AddRange(params T[] objs);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Remove(int id);
        void RemoveRange(params T[] objs);
        void Update(T obj);
        void UpdateRange(params T[] objs);
    }
}