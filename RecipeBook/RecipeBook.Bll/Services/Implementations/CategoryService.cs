using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class CategoryService : IService<Category>
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(Category item)
        {
            return await categoryRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category item)
        {
            return await categoryRepository.UpdateAsync(id, item);
        }
    }
}
