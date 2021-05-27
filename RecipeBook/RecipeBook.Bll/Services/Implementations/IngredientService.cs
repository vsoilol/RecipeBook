using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class IngredientService : IService<Ingredient>
    {
        private readonly IRepository<Ingredient> ingredientRepository;

        public IngredientService(IRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<Ingredient> CreateAsync(Ingredient item)
        {
            return await ingredientRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ingredientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await ingredientRepository.GetAllAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await ingredientRepository.GetByIdAsync(id);
        }

        public async Task<Ingredient> UpdateAsync(int id, Ingredient item)
        {
            return await ingredientRepository.UpdateAsync(id, item);
        }
    }
}
