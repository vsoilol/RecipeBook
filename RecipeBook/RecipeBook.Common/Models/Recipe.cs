﻿using System;
using System.Collections.Generic;

namespace RecipeBook.Common.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int CookingTemperature { get; set; }

        public int CategoryId { get; set; }

        public byte[] ImageData { get; set; }

        public string Description { get; set; }

        public string SequenceActions { get; set; }

        public IEnumerable<int> IngredientsId { get; set; }
    }
}
