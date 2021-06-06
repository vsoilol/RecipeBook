using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System.IO;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService, IIngredientService ingredientService, ICategoryService categoryService, IMapper mapper)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
            this.ingredientService = ingredientService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAllRecipesInfoAsync()
        {
            var recipes = await recipeService.GetAllAsync();
            return View("InfoAll", recipes);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAllRecipesByCategoryIdAsync(int categoryId)
        {
            var recipes = await recipeService.GetAllByCategoryIdAsync(categoryId);
            return View("InfoAll", recipes);
        }

        public async Task<IActionResult> GetAllRecipesEditAsync()
        {
            return View("EditAll", await recipeService.GetAllAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            RecipeInfo recipeInfo = mapper.Map<RecipeInfo>(recipe);

            recipeInfo.Category = await categoryService.GetByIdAsync(recipe.CategoryId);
            recipeInfo.Ingredients = await ingredientService.GetAllByRecipeIdAsync(id);

            return View("RecipeInfoFull", recipeInfo);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            RecipeViewModel recipeViewModel = mapper.Map<RecipeViewModel>(recipe);

            ViewBag.ActionTitle = "Edit";


            return View("Edit", recipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(RecipeViewModel recipeViewModel)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = mapper.Map<Recipe>(recipeViewModel);

                recipe.ImageData = await CreateImageAsync(recipeViewModel.ImageDataFile);
                await recipeService.UpdateAsync(recipeViewModel.Id, recipe);

                return await GetAllRecipesEditAsync();
            }
            else
            {
                ViewBag.ActionTitle = "Edit";
                return View("Edit", recipeViewModel);
            }

        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!(await recipeService.DeleteAsync(id)))
            {
                ModelState.AddModelError("", "The recipe cannot be deleted");
            }

            return await GetAllRecipesEditAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecipeViewModel recipeViewModel)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = mapper.Map<Recipe>(recipeViewModel);

                recipe.ImageData = await CreateImageAsync(recipeViewModel.ImageDataFile);
                await recipeService.CreateAsync(recipe);

                return await GetAllRecipesEditAsync();
            }
            else
            {
                ViewBag.ActionTitle = "Create";
                return View("Edit", recipeViewModel);
            }
        }

        public IActionResult CreateAsync()
        {
            ViewBag.ActionTitle = "Create";
            return View("Edit");
        }

        private async Task<byte[]> CreateImageAsync(IFormFile imageData)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageData.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
