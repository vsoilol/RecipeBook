using RecipeBook.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);

        Task<Category> CreateAsync(Category item);

        Task<bool> DeleteAsync(int id);
    }
}
