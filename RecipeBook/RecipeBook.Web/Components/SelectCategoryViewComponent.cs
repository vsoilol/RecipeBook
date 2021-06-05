﻿using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Components
{
    public class SelectCategoryViewComponent: ViewComponent
    {
        private readonly IService<Category> categoryService;

        public SelectCategoryViewComponent(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Category selectCategory)
        {
            var category = new SelectCategoryModel
            {
                SelectCategory = selectCategory,
                Categories = await categoryService.GetAllAsync(),
            };

            return View(category);
        }
    }
}
