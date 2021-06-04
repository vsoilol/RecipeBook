using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Bll.Converter
{
    public interface IPdfConverter
    {
        byte[] GeneratePdfFromString(string stylefilePath, Recipe recipe, Category category, IEnumerable<Ingredient> ingredients);
    }
}
