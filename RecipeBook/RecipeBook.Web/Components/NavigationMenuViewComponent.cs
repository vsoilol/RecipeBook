using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using System.Threading.Tasks;

namespace RecipeBook.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public NavigationMenuViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await categoryService.GetAllAsync());
        }
    }
}
