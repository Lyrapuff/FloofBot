using System.Collections.Generic;
using System.Linq;
using FloofBot.Core.Services.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FloofBot.Core.Services.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : DbEntity
    {
        protected IUnitOfWork _unitOfWork { get; set; }
        protected DbSet<T> _dbSet { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = unitOfWork.Context.Set<T>();
        }

        public void Add(T obj)
        {
            _dbSet.Add(obj);
            _unitOfWork.Commit();
        }

        public void AddRange(params T[] objs)
        {
            _dbSet.AddRange(objs);
            _unitOfWork.Commit();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(int id)
        {
            _dbSet.Remove(GetById(id));
            _unitOfWork.Commit();
        }

        public void RemoveRange(params T[] objs)
        {
            _dbSet.RemoveRange(objs);
            _unitOfWork.Commit();
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
            _unitOfWork.Commit();
        }
        
        public void UpdateRange(params T[] objs)
        {
            _dbSet.UpdateRange(objs);
            _unitOfWork.Commit();
        }
    }
}