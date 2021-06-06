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

        public SearchController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchInfo searchInfo)
        {
            if (ModelState.IsValid)
            {
                var recipes = await recipeService.GetAllSearchAsync(searchInfo.CategoryId, searchInfo.IngredientsId, searchInfo.RecipeName);

                return View("~/Views/Recipe/List.cshtml", recipes);
            }
            else
            {
                return View(searchInfo);
            }
        }

    }
}
