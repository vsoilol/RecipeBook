using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Implementations;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IService<Category> categoryService;

        public NavigationMenuViewComponent(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await categoryService.GetAllAsync());
        }
    }
}
