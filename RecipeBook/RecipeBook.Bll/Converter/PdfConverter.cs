using DinkToPdf;
using DinkToPdf.Contracts;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Bll.Converter
{
    public class PdfConverter : IPdfConverter
    {
        private readonly IConverter converter;

        public PdfConverter(IConverter converter)
        {
            this.converter = converter;
        }

        private byte[] GeneratePdf(string htmlContent, string stylefilePath)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = stylefilePath },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return converter.Convert(pdf);
        }

        public byte[] GeneratePdfFromString(string stylefilePath, Recipe recipe, Category category, IEnumerable<Ingredient> ingredients)
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendFormat($@"
                        <html>
                            <head>
                            </head>
                            <body>

                            <br/>
                            <h2 class='text-primary'>{recipe.Name}</h2>
                            <br/>

                             <div class='border backgroundWhite row'>

                             <div class='col-md-8 mt-3'>

                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Название</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <label>{recipe.Name}</label>
                                            </div>
                                        </div>

                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Категория</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <label>{category.Name}</label>
                                            </div>
                                        </div>

                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Описание</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <label>{recipe.Description}</label>
                                            </div>
                                        </div>

                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Время готовки</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <label>{recipe.CookingTime}</label>
                                            </div>
                                        </div>


                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Температура готовки</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <label>{recipe.CookingTemperature.ToString("0\u00B0C")}</label>
                                            </div>
                                        </div>


                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Последовательность действий</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='border p-2'>
                                                    <label>{recipe.SequenceActions}</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='form-group row mb-3'>
                                            <div class='col-md-4'>
                                                <label class='text-primary font-weight-bold'>Ингредиенты</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='border p-2'>");

            var listIngredients = new StringBuilder();

            foreach (var ingredient in ingredients)
            {
                listIngredients.AppendFormat($@"<p>{ingredient.Name}, Вес - {ingredient.Weight.ToString("0.00")} г</p>");
            }

            htmlContent.Append(listIngredients);

            htmlContent.Append($@"
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class='col-3 offset-1 d-none d-md-block'>
                                        <img src = 'data:image/jpeg;base64,{Convert.ToBase64String(recipe.ImageData)}' class='recipeImage'/>
                                    </div>

                                </div>
                            </body>
                        </html>");

            return GeneratePdf(htmlContent.ToString(), stylefilePath);
        }
    }
}
