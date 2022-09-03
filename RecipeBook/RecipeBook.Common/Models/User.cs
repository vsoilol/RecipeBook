using RecipeBook.Common.Enums;

namespace RecipeBook.Common.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
