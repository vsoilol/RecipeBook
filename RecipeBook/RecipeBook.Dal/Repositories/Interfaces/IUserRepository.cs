using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetAllByRoleAsync(Role role);
    }
}
