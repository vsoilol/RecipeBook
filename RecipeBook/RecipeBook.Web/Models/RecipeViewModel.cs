using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int CookingTemperature { get; set; }

        public byte[] ImageData { get; set; }

        public string Description { get; set; }

        public string SequenceActions { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public SearchModel SearchElements { get; set; }
    }
}
