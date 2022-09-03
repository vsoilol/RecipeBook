using RecipeBook.Common.Models;

namespace RecipeBook.Bll.Converter
{
    public interface IPdfConverter
    {
        byte[] GeneratePdfFromString(string stylefilePath, RecipeInfo recipe);
    }
}
