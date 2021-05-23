using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Dal.Helpers.Implementations;
using RecipeBook.Dal.Helpers.Interfaces;

namespace RecipeBook.Di
{
    public static class Ioc
    {
        public static void Build(this IServiceCollection services)
        {
            //Helpers
            services.AddSingleton<IDbHelper, DbHelper>();
        }
    }
}
