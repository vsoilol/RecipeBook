using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Web.Models;
using System.Threading.Tasks;

namespace RecipeBook.Web.Components
{
    public class SelectCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public SelectCategoryViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectCategoryId)
        {
            var category = new SelectCategoryModel
            {
                SelectCategoryId = selectCategoryId,
                Categories = await categoryService.GetAllAsync(),
            };

            return View(category);
        }
    }
}
