using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<IIngredientRepository, IngredientRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
        }
    }
}
