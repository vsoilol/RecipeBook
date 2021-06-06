using RecipeBook.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface IRecipeIngredientRepository
    {
        Task<bool> DeleteByRecipeIdAsync(int recipeId);

        Task<IEnumerable<RecipeIngredient>> CreateAsync(IEnumerable<RecipeIngredient> items);

        Task<IEnumerable<RecipeIngredient>> GetAllByIngredientsIdAsync(IEnumerable<int> ingredientsId);
    }
}
