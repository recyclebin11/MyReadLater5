using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(Category category);
        void DeleteCategory(Category category);
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int Id);
        Task<Category> GetCategory(string Name);
        void UpdateCategory(Category category);
    }
}