using RecipeBook.Common.Models;
using System.Collections.Generic;

namespace RecipeBook.Web.Models
{
    public class SelectIngredientModel
    {
        public IEnumerable<Ingredient> AllIngredients { get; set; }

        public IEnumerable<int> SelectIngredientsId { get; set; }
    }
}
