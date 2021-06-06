using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class RecipeViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan CookingTime { get; set; }

        [Required]
        public int CookingTemperature { get; set; }

        [Required]
        public IFormFile ImageDataFile { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SequenceActions { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<int> IngredientsId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public byte[] ImageData { get; set; }
    }
}
