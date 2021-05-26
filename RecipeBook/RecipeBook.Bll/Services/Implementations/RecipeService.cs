using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public async Task<Recipe> CreateAsync(Recipe item)
        {
            return await recipeRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await recipeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await recipeRepository.GetByIdAsync(id);
        }

        public async Task<Recipe> UpdateAsync(int id, Recipe item)
        {
            return await recipeRepository.UpdateAsync(id, item);
        }
    }
}
