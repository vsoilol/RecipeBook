using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class SearchElements
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
