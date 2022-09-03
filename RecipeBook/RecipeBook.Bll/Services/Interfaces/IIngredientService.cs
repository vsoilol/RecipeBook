using RecipeBook.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Interfaces
{
    public interface IIngredientService : IService<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetAllByRecipeIdAsync(int recipeId);
    }
}
