using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class SearchInfo
    {
        [Required(ErrorMessage = "Name not specified")]
        public string RecipeName { get; set; }

        [Required(ErrorMessage = "Category not specified")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Ingredients not specified")]
        public IEnumerable<int> IngredientsId { get; set; }
    }
}
