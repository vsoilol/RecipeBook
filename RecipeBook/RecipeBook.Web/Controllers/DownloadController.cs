using Microsoft.AspNetCore.Mvc;
using RecipeBook.Bll.Converter;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IPdfConverter pdfConverter;
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;
        private readonly IService<Category> categoryService;
        private readonly string StyleFilePath;

        public DownloadController(IPdfConverter pdfConverter, IRecipeService recipeService, IIngredientService ingredientService, IService<Category> categoryService)
        {
            this.pdfConverter = pdfConverter;
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
            this.categoryService = categoryService;
            StyleFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\css\downloadStyle.css");
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> GetFile(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            Category category = await categoryService.GetByIdAsync(recipe.CategoryId);
            var ingredients = await ingredientService.GetAllByRecipeIdAsync(id);

            var pdfFile = pdfConverter.GeneratePdfFromString(StyleFilePath, recipe, category, ingredients);
            return File(pdfFile, "application/octet-stream", $"{recipe.Name}.pdf");
        }
    }
}
