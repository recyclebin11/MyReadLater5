using Data;
using Data.Repositories;
using Data.Repositories.Base;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            return await _categoryRepository.CreateEntity(category);
        }

        public async void UpdateCategory(Category category)
        {
            await _categoryRepository.UpdateEntity(category);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetEntities();
        }

        public async Task<Category> GetCategory(int Id)
        {
            return await _categoryRepository.GetEntity(Id);
        }

        public async Task<Category> GetCategory(string Name)
        {
            return await _categoryRepository.GetCategoryByName(Name);
        }

        public async void DeleteCategory(Category category)
        {
            await _categoryRepository.DeleteEntity(category);
        }

    }
}
