using Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ReadLaterDataContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(ReadLaterDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> CreateEntity(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateEntity(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetEntities()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetEntity(int Id)
        {
            return await _dbSet.Where(c => c.ID == Id).FirstOrDefaultAsync();
        }

        public virtual async Task DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
