using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Initial
{
    public class DbInitial : IDbInitial
    {
        private readonly IUserRepository userRepository;

        public DbInitial(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task InitialAdmin()
        {
            if ((await userRepository.GetAllByRoleAsync(Role.Admin) == null))
            {
                User user = new User
                {
                    Email = "admin123@email.com",
                    Password = "Admin123",
                    Role = Role.Admin,
                };
                await userRepository.CreateAsync(user);
            }
        }
    }
}
