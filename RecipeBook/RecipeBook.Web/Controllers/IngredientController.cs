using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
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

        public async Task<IActionResult> GetAllAsync()
        {
            return View("List", await ingredientService.GetAllAsync());
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!(await ingredientService.DeleteAsync(id)))
            {
                ModelState.AddModelError("", "You can't delete it because this ingredient is linked to the recipe");
            }

            return await GetAllAsync();
        }

        public IActionResult Create()
        {
            ViewBag.IngredientTitle = "Add";
            return View("Edit");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.IngredientTitle = "Edit";
            return View("Edit", await ingredientService.GetByIdAsync(id));
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
