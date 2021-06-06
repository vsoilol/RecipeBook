using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RecipeBook.Bll.Services.Implementations;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using RecipeBook.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        [Test]
        public async Task GetByIdAsync_WhenIdIsCorrect_ShouldReturnCategoryById()
        {
            // Arrange
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            int id = 2;

            mock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(GetSampleCategory().First(category => category.Id == id));
            CategoryService categoryService = new CategoryService(mock.Object);

            // Act
            var result = await categoryService.GetByIdAsync(id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetAllAsync_WhenDataIsCorrect_ShouldReturnAllCategories()
        {
            // Arrange
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();

            mock.Setup(m => m.GetAllAsync()).ReturnsAsync(GetSampleCategory());
            CategoryService categoryService = new CategoryService(mock.Object);

            // Act
            var result = await categoryService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(GetSampleCategory());
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsCorrect_ShouldReturnTrue()
        {
            // Arrange
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            int id = 3;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleCategory().First(category => category.Id == id) != null);
            CategoryService categoryService = new CategoryService(mock.Object);

            // Act
            var result = await categoryService.DeleteAsync(id);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsNotCorrect_ShouldReturnFalse()
        {
            // Arrange
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            int id = 7;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleCategory().FirstOrDefault(category => category.Id == id) != null);
            CategoryService categoryService = new CategoryService(mock.Object);

            // Act
            var result = await categoryService.DeleteAsync(id);

            // Assert
            Assert.That(!result);
        }

        private IEnumerable<Category> GetSampleCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Main Course",
                },
                new Category
                {
                    Id = 2,
                    Name = "Dessert",
                },
                new Category
                {
                    Id = 3,
                    Name = "Lunch",
                },
                new Category
                {
                    Id = 4,
                    Name = "Salad",
                },
                new Category
                {
                    Id = 5,
                    Name = "Pizza",
                },
            };
        }
    }
}
