using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class SelectIngredientModel
    {
        public IEnumerable<Ingredient> AllIngredients { get; set; }

        public IEnumerable<int> SelectIngredientsId { get; set; }
    }
}
