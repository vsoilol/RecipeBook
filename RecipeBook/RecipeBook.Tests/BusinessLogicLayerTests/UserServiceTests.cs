using FluentAssertions;
using Moq;
using NUnit.Framework;
using RecipeBook.Bll.Services.Implementations;
using RecipeBook.Common.Enums;
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
    public class UserServiceTests
    {
        [Test]
        public async Task GetByIdAsync_WhenIdIsCorrect_ShouldReturnUserById()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            int id = 2;

            mock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(GetSampleUsers().First(item => item.Id == id));
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.GetByIdAsync(id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetAllByRoleAsync_WhenRoleIsCorrect_ShouldReturnUsersByRole()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            Role role = Role.Editor;
            var usersByRole = GetSampleUsers().Where(item => item.Role == role);

            mock.Setup(m => m.GetAllByRoleAsync(role)).ReturnsAsync(usersByRole);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.GetAllByRoleAsync(role);

            // Assert
            result.Should().BeEquivalentTo(usersByRole);
        }

        [Test]
        public async Task GetByEmailAsync_WhenEmailIsCorrect_ShouldReturnUserByEmail()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            string email = "email1.com";
            User userByEmail = GetSampleUsers().First(item => item.Email == email);

            mock.Setup(m => m.GetByEmailAsync(email)).ReturnsAsync(userByEmail);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.GetByEmailAsync(email);

            // Assert
            Assert.That(result.Id, Is.EqualTo(userByEmail.Id));
        }

        [Test]
        public async Task IsUserExist_WhenUserIsExist_ShouldReturnTrue()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            string email = "email1.com";
            User userByEmail = GetSampleUsers().First(item => item.Email == email);

            mock.Setup(m => m.GetByEmailAsync(email)).ReturnsAsync(userByEmail);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.IsUserExist(email);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task IsUserExist_WhenUserIsNotExist_ShouldReturnFalse()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            string email = "email9.com";
            User userByEmail = GetSampleUsers().FirstOrDefault(item => item.Email == email);

            mock.Setup(m => m.GetByEmailAsync(email)).ReturnsAsync(userByEmail);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.IsUserExist(email);

            // Assert
            Assert.That(!result);
        }

        [Test]
        public async Task GetAllAsync_WhenDataIsCorrect_ShouldReturnAllUsers()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            mock.Setup(m => m.GetAllAsync()).ReturnsAsync(GetSampleUsers());
            UserService userService = new UserService(mock.Object);

            //Act
            var result = await userService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(GetSampleUsers());
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsCorrect_ShouldReturnTrue()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            int id = 3;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleUsers().First(item => item.Id == id) != null);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.DeleteAsync(id);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task DeleteAsync_WhenIdIsNotCorrect_ShouldReturnFalse()
        {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            int id = 7;

            mock.Setup(m => m.DeleteAsync(id)).ReturnsAsync(GetSampleUsers().FirstOrDefault(item => item.Id == id) != null);
            UserService userService = new UserService(mock.Object);

            // Act
            var result = await userService.DeleteAsync(id);

            // Assert
            Assert.That(!result);
        }

        private IEnumerable<User> GetSampleUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "email1.com",
                    Password = "Password1",
                    Role = Role.Editor,
                },
                new User
                {
                    Id = 2,
                    Email = "email2.com",
                    Password = "Password2",
                    Role = Role.Admin,
                },
                new User
                {
                    Id = 3,
                    Email = "email3.com",
                    Password = "Password3",
                    Role = Role.Editor,
                },
                new User
                {
                    Id = 4,
                    Email = "email4.com",
                    Password = "Password4",
                    Role = Role.Editor,
                },
                new User
                {
                    Id = 5,
                    Email = "email5.com",
                    Password = "Password5",
                    Role = Role.Admin,
                },
            };
        }
    }
}
