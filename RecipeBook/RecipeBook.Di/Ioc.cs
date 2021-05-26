using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Bll.Services.Implementations;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Helpers.Implementations;
using RecipeBook.Dal.Helpers.Interfaces;
using RecipeBook.Dal.Repositories.Implementations;
using RecipeBook.Dal.Repositories.Interfaces;

namespace RecipeBook.Di
{
    public static class Ioc
    {
        public static void Build(this IServiceCollection services)
        {
            //Helpers
            services.AddSingleton<IDbHelper, DbHelper>();

            //Repositories
            services.AddSingleton<IRecipeRepository, RecipeRepository>();
            services.AddSingleton<IRepository<Ingredient>, IngredientRepository>();
            services.AddSingleton<IRepository<Category>, CategoryRepository>();

            //Services
            services.AddSingleton<IRecipeService, RecipeService>();
            services.AddSingleton<IService<Category>, CategoryService>();
            services.AddSingleton<IService<Ingredient>, IngredientService>();
        }
    }
}
