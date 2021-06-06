using AutoMapper;
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
        private readonly ICategoryService categoryService;
        private readonly string StyleFilePath;
        private readonly IMapper mapper;

        public DownloadController(IPdfConverter pdfConverter, IRecipeService recipeService, 
            IIngredientService ingredientService, ICategoryService categoryService, IMapper mapper)
        {
            this.pdfConverter = pdfConverter;
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            StyleFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\css\downloadStyle.css");
        }
        
        public async Task<IActionResult> GetFile(int id)
        {
            Recipe recipe = await recipeService.GetByIdAsync(id);
            RecipeInfo recipeInfo = mapper.Map<RecipeInfo>(recipe);

            recipeInfo.Category = await categoryService.GetByIdAsync(recipe.CategoryId);
            recipeInfo.Ingredients = await ingredientService.GetAllByRecipeIdAsync(id);

            var pdfFile = pdfConverter.GeneratePdfFromString(StyleFilePath, recipeInfo);
            return File(pdfFile, "application/octet-stream", $"{recipeInfo.Name}.pdf");
        }
    }
}
