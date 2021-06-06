using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class SelectCategoryModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public int SelectCategoryId { get; set; }
    }
}
