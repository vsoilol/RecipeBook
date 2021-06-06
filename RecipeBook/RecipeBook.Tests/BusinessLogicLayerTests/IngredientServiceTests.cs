using FluentAssertions;
using Moq;
using NUnit.Framework;
using RecipeBook.Bll.Services.Implementations;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    public class IngredientServiceTests
    {
        [Test]
        public async Task GetByIdAsync_WhenIdIsCorrect_ShouldReturnIngredientById()
        {
            // Arrange
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            int id = 2;

            mock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(GetSampleIngredients().First(item => item.Id == id));
            IngredientService ingredientService = new IngredientService(mock.Object);

            // Act
            var result = await ingredientService.GetByIdAsync(id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetAllAsync_WhenDataIsCorrect_ShouldReturnAllCategories()
        {
            // Arrange
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();

            mock.Setup(m => m.GetAllAsync()).ReturnsAsync(GetSampleIngredients());
            IngredientService ingredientService = new IngredientService(mock.Object);

            //Act
            var result = await ingredientService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(GetSampleIngredients());
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsCorrect_ShouldReturnTrue()
        {
            // Arrange
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            int id = 3;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleIngredients().First(category => category.Id == id) != null);
            IngredientService ingredientService = new IngredientService(mock.Object);

            // Act
            var result = await ingredientService.DeleteAsync(id);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsNotCorrect_ShouldReturnFalse()
        {
            // Arrange
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            int id = 7;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleIngredients().FirstOrDefault(category => category.Id == id) != null);
            IngredientService ingredientService = new IngredientService(mock.Object);

            // Act
            var result = await ingredientService.DeleteAsync(id);

            // Assert
            Assert.That(!result);
        }

        private IEnumerable<Ingredient> GetSampleIngredients()
        {
            return new List<Ingredient>
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "Ingredient1",
                    Weight = 1,
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Ingredient2",
                    Weight = 2.3,
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Ingredient3",
                    Weight = 10.9,
                },
                new Ingredient
                {
                    Id = 4,
                    Name = "Ingredient4",
                    Weight = 0.9,
                },
                new Ingredient
                {
                    Id = 5,
                    Name = "Ingredient5",
                    Weight = 11.3,
                },
            };
        }
    }
}
