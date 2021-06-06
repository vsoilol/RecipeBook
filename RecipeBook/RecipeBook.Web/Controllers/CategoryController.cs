using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class CategoryController : Controller
    {
        private readonly IService<Category> categoryService;

        public CategoryController(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return View("List", await categoryService.GetAllAsync());
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!(await categoryService.DeleteAsync(id)))
            {
                ModelState.AddModelError("", "You can't delete it because this category is associated with a recipe");
            }

            return await GetAllAsync();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            await categoryService.CreateAsync(category);
            return await GetAllAsync();
        }
    }
}
