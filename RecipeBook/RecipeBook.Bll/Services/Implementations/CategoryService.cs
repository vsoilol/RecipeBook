using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RecipeBook.Bll.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(Category item)
        {
            return await categoryRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result;

            try
            {
                result = await categoryRepository.DeleteAsync(id);
            }
            catch (SqlException)
            {
                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }
    }
}
