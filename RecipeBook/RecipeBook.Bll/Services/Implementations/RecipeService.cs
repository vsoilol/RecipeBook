using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IRecipeIngredientRepository recipeIngredientRepository;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository, IIngredientRepository ingredientRepository)
        {
            this.recipeRepository = recipeRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await recipeIngredientRepository.DeleteByRecipeIdAsync(id);
            return await recipeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var recipes = await recipeRepository.GetAllAsync();
            return recipes;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            Recipe recipe = await recipeRepository.GetByIdAsync(id);
            recipe.IngredientsId = (await ingredientRepository.GetAllByRecipeIdAsync(id)).Select(item => item.Id);

            return recipe;
        }

        public async Task<Recipe> UpdateAsync(int id, Recipe item)
        {
            await recipeIngredientRepository.DeleteByRecipeIdAsync(id);

            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            
            foreach (var ingredient in item.IngredientsId)
            {
                recipeIngredients.Add(new RecipeIngredient
                {
                    IngredientId = ingredient,
                    RecipeId = id,
                });
            }
            await recipeIngredientRepository.CreateAsync(recipeIngredients);

            return await recipeRepository.UpdateAsync(id, item);
        }

        public async Task<Recipe> CreateAsync(Recipe item)
        {
            Recipe model = await recipeRepository.CreateAsync(item);

            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in item.IngredientsId)
            {
                recipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = model.Id,
                    IngredientId = ingredient,
                });
            }
            await recipeIngredientRepository.CreateAsync(recipeIngredients);
            model.IngredientsId = item.IngredientsId;

            return model;
        }

        public async Task<IEnumerable<Recipe>> GetAllSearchAsync(int categoryId, IEnumerable<int> ingredientsId, string recipePartName)
        {
            var recipesId = (await recipeIngredientRepository.GetAllByIngredientsIdAsync(ingredientsId)).Select(recipeIngredient => recipeIngredient.RecipeId);
            var recipes = await recipeRepository.GetAllByCategoryAndNameAsync(categoryId, recipePartName);

            if(recipesId == null)
            {
                return recipes;
            }
            else
            {
                if (recipes != null)
                {
                    recipes = recipes.Where(recipe => recipesId.Contains(recipe.Id));
                }
            }

            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await recipeRepository.GetAllByCategoryIdAsync(categoryId);
        }
    }
}
