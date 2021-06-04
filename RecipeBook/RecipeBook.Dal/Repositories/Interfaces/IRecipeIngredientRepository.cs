using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface IRecipeIngredientRepository
    {
        Task<bool> DeleteByRecipeIdAsync(int recipeId);

        Task<RecipeIngredient> CreateAsync(RecipeIngredient item);

        Task<IEnumerable<RecipeIngredient>> GetAllByIngredientsIdAsync(IEnumerable<int> ingredientsId);
    }
}
