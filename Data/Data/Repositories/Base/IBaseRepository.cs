using Entity.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateEntity(T entity);
        Task DeleteEntity(T entity);
        Task<List<T>> GetEntities();
        Task<T> GetEntity(int Id);
        Task UpdateEntity(T entity);
    }
}