using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetAllByRecipePartNameAsync(string recipePartName);

        Task<IEnumerable<Recipe>> GetAllByIngredientIdAsync(int ingredientId);

        Task<IEnumerable<Recipe>> GetAllByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Recipe>> GetAllByCategoryAndNameAsync(int categoryId, string recipePartName);
    }
}
