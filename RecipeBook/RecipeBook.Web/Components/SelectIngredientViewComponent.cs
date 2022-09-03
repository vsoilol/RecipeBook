using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Web.Models;
using System.Collections.Generic;
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
