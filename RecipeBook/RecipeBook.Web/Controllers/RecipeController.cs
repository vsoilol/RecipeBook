using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;
        private readonly IService<Category> categoryService;
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService, IIngredientService ingredientService, IService<Category> categoryService, IMapper mapper)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
            this.ingredientService = ingredientService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipesAsync()
        {
            var recipes = await recipeService.GetAllAsync();
            return View("List", recipes);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipesByCategoryIdAsync(int categoryId)
        {
            var recipes = await recipeService.GetAllByCategoryIdAsync(categoryId);
            return View("List", recipes);
        }

        [HttpGet]
        public IActionResult ShowRecipes(IEnumerable<Recipe> recipes)
        {
            return View("List", recipes);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            RecipeViewModel recipeViewModel = mapper.Map<RecipeViewModel>(recipe);

            recipeViewModel.Category = await categoryService.GetByIdAsync(recipe.CategoryId);
            recipeViewModel.Ingredients = await ingredientService.GetAllByRecipeIdAsync(id);

            return View("RecipeFull", recipeViewModel);
        }
    }
}
