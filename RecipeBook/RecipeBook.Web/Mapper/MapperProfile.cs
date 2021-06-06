using AutoMapper;
using RecipeBook.Common.Models;
using RecipeBook.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //mappings
            CreateMap<Recipe, RecipeInfo>();
            CreateMap<RecipeInfo, Recipe>();

            CreateMap<Recipe, RecipeViewModel>();
            CreateMap<RecipeViewModel, Recipe>();
        }
    }
}
