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
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        [Test]
        public async Task CreateAsync_WhenCategoryHasData_ShouldReturnNewCategory()
        {
            // Arrange
            Mock<IRepository<Category>> mock = new Mock<IRepository<Category>>();
            Category createCategory = new Category
            {
                Id = 6,
                Name = "New Category",
            };

            mock.Setup(m => m.CreateAsync(createCategory)).ReturnsAsync(createCategory);
            CategoryService categoryService = new CategoryService(mock.Object);

            // Act
            var result = await categoryService.CreateAsync(createCategory);

            // Assert
            result.Model.Should().BeEquivalentTo(GetSampleCategory());
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
