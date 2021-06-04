using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IService<Category> categoryService;
        private readonly IIngredientService ingredientService;

        public SearchController(IRecipeService recipeService, IService<Category> categoryService, IIngredientService ingredientService)
        {
            this.recipeService = recipeService;
            this.categoryService = categoryService;
            this.ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SearchElements searchElements = new SearchElements
            {
                Categories = await categoryService.GetAllAsync(),
                Ingredients = await ingredientService.GetAllAsync()
            };

            return View(searchElements);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string recipeName, int categoryId, IEnumerable<int> ingredientsId)
        {
            var recipes = await recipeService.GetAllSearchAsync(categoryId, ingredientsId, recipeName);

            return View("~/Views/Recipe/List.cshtml", recipes);
        }

    }
}
