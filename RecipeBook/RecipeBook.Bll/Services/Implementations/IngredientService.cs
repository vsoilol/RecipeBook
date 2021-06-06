using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<Ingredient> CreateAsync(Ingredient item)
        {
            return await ingredientRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result;

            try
            {
                result = await ingredientRepository.DeleteAsync(id);
            }
            catch (SqlException)
            {
                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await ingredientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllByRecipeIdAsync(int recipeId)
        {
            return await ingredientRepository.GetAllByRecipeIdAsync(recipeId);
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
