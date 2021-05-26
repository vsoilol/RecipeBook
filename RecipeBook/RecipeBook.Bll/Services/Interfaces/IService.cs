using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T item);

        Task<T> UpdateAsync(int id, T item);

        Task<bool> DeleteAsync(int id);
    }
}
