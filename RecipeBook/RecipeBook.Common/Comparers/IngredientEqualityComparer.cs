using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RecipeBook.Common.Comparers
{
    public class IngredientEqualityComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals([AllowNull] Ingredient x, [AllowNull] Ingredient y)
        {
            if (x == null || y == null)
            {
                return false;
            }


            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Ingredient obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
