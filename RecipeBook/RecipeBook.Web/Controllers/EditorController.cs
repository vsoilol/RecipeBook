using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class EditorController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IService<Category> categoryService;
        private readonly IIngredientService ingredientService;
        private readonly IMapper mapper;

        public EditorController(IRecipeService recipeService, IService<Category> categoryService, IIngredientService ingredientService, IMapper mapper)
        {
            this.recipeService = recipeService;
            this.categoryService = categoryService;
            this.ingredientService = ingredientService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetAllRecipesAsync()
        {
            return View("Index", await recipeService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            RecipeViewModel recipeViewModel = mapper.Map<RecipeViewModel>(recipe);

            recipeViewModel.Category = await categoryService.GetByIdAsync(recipe.CategoryId);
            recipeViewModel.Ingredients = await ingredientService.GetAllByRecipeIdAsync(id);

            SearchElements searchElements = new SearchElements
            {
                Categories = await categoryService.GetAllAsync(),
                Ingredients = await ingredientService.GetAllAsync()
            };

            recipeViewModel.SearchElements = searchElements;

            return View(recipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, Recipe recipe, IFormFile imageData)
        {

            if(imageData != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageData.CopyToAsync(memoryStream);
                    recipe.ImageData = memoryStream.ToArray();
                }
            }

            await recipeService.UpdateAsync(id, recipe);

            return RedirectToAction("GetAllRecipes");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await recipeService.DeleteAsync(id);
            return RedirectToAction("GetAllRecipes");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Recipe recipe, IFormFile imageData)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageData.CopyToAsync(memoryStream);
                recipe.ImageData = memoryStream.ToArray();
            }
            await recipeService.CreateAsync(recipe);
            
            return RedirectToAction("GetAllRecipes");
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            SearchElements searchElements = new SearchElements
            {
                Categories = await categoryService.GetAllAsync(),
                Ingredients = await ingredientService.GetAllAsync()
            };

            return View(searchElements);
        }
    }
}
