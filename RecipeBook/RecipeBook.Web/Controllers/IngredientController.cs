using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> Update(int id)
        {
            return View(await ingredientService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Ingredient ingredient)
        {
            await ingredientService.UpdateAsync(ingredient.Id, ingredient);

            return await GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Ingredient ingredient)
        {
            await ingredientService.CreateAsync(ingredient);
            return await GetAllAsync();
        }
    }
}
