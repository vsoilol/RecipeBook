using RecipeBook.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        Task<IEnumerable<Recipe>> GetAllByRecipePartNameAsync(string recipePartName);

        Task<IEnumerable<Recipe>> GetAllByIngredientIdAsync(int ingredientId);

        Task<IEnumerable<Recipe>> GetAllByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Recipe>> GetAllSearchAsync(int categoryId, IEnumerable<int> ingredientsId, string recipePartName);
    }
}
