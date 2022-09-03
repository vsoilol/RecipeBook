using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User item)
        {
            return await userRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result;

            try
            {
                result = await userRepository.DeleteAsync(id);
            }
            catch (SqlException)
            {
                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllByRoleAsync(Role role)
        {
            return await userRepository.GetAllByRoleAsync(role);
        }

        public async Task<User> UpdateAsync(int id, User item)
        {
            return await userRepository.UpdateAsync(id, item);
        }

        public async Task<bool> IsUserExist(string email)
        {
            if ((await userRepository.GetByEmailAsync(email)) == null)
            {
                return false;
            }
            return true;
        }
    }
}
