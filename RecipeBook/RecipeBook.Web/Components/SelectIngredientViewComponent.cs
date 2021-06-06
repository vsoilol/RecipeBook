using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Components
{
    public class SelectIngredientViewComponent : ViewComponent
    {
        private readonly IIngredientService ingredientService;

        public SelectIngredientViewComponent(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> selectIngredientsId)
        {
            SelectIngredientModel selectIngredientModel = new SelectIngredientModel
            {
                AllIngredients = await ingredientService.GetAllAsync(),
                SelectIngredientsId = selectIngredientsId,
            };

            return View(selectIngredientModel);
        }
    }
}
