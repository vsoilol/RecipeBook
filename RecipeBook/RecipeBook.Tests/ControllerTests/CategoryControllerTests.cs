using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Tests.ControllerTests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        [Test]
        public async Task GetAllAsync_WhenCategoryHasData_ShouldReturnAllCategory()
        {
            // Arrange
            Mock<IService<Category>> mock = new Mock<IService<Category>>();

            mock.Setup(m => m.GetAllAsync()).ReturnsAsync(GetSampleCategory);
            CategoryController categoryController = new CategoryController(mock.Object);

            // Act
            var result = await categoryController.GetAllAsync() as ViewResult;

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
