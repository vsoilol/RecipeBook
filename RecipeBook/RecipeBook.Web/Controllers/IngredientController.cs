using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return View("Index", await ingredientService.GetAllAsync());
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await ingredientService.DeleteAsync(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Нельзя удалить, так как этот ингредиент связан с рецептом");
            }

            return await GetAllAsync();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Ingredient ingredient)
        {
            await ingredientService.CreateAsync(ingredient);
            return await GetAllAsync();
        }
    }
}
