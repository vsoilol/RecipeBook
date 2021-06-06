using AutoMapper;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;

namespace RecipeBook.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //mappings
            CreateMap<Recipe, RecipeInfo>();

            CreateMap<Recipe, RecipeViewModel>();
            CreateMap<RecipeViewModel, Recipe>();
        }
    }
}
