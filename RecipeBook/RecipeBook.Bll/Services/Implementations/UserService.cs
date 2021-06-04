using RecipeBook.Bll.Services.Interfaces;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
            return await userRepository.DeleteAsync(id);
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

        public async Task<User> UpdateAsync(int id, User item)
        {
            return await userRepository.UpdateAsync(id, item);
        }
    }
}
