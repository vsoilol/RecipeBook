using RecipeBook.Common.Models;
using System.Collections.Generic;

namespace RecipeBook.Web.Models
{
    public class SelectCategoryModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public int SelectCategoryId { get; set; }
    }
}
