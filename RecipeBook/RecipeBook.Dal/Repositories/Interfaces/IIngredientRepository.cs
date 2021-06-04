using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetAllByRecipeIdAsync(int recipeId);
    }
}
