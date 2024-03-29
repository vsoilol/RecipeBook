﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Web.Models
{
    public class SearchInfo
    {
        [Required(ErrorMessage = "Name not specified")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+", ErrorMessage = "The name must consist of letters")]
        public string RecipeName { get; set; }

        [Required(ErrorMessage = "Category not specified")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Ingredients not specified")]
        public IEnumerable<int> IngredientsId { get; set; }
    }
}
