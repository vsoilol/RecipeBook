using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Bll.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetAllByRoleAsync(Role role);

        Task<bool> IsUserExist(string email);
    }
}
