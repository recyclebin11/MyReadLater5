using Data.Repositories.Base;
using Entity;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryByName(string name);
    }
}