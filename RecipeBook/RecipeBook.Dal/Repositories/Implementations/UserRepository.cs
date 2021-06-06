using RecipeBook.Common.Enums;
using RecipeBook.Common.Models;
using RecipeBook.Dal.Helpers.Interfaces;
using RecipeBook.Dal.Mappers;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Implementations
{
    public class UserRepository : IUserRepository, IMapperBase<User>
    {
        private readonly IDbHelper dbHelper;

        public UserRepository(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<User> CreateAsync(User item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"INSERT INTO UserModel (Password, Email, Role)
                            VALUES (@password, @email, @role)
                            SELECT TOP 1 * FROM UserModel ORDER BY Id DESC";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("password", SqlDbType.NVarChar)
                    {
                        Value = item.Password,
                    },
                    new SqlParameter("email", SqlDbType.NVarChar)
                    {
                        Value = item.Email,
                    },
                    new SqlParameter("role", SqlDbType.Int)
                    {
                        Value = item.Role,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"DELETE UserModel
                            WHERE Id = @id";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("id", SqlDbType.Int)
                    {
                        Value = id,
                    },
                };

                return (await dbHelper.ExecuteNonQueryAsync(sqlConnection, sql, sqlParameters)) != 0;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM UserModel";

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql);

                return await MapToListAsync(reader);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM UserModel
                            WHERE Id = @id";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("id", SqlDbType.Int)
                    {
                        Value = id,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }


        public async Task<User> UpdateAsync(int id, User item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"UPDATE UserModel
                            SET Password = @password,
                                Email = @email,
                                Role = @role
                            WHERE Id = @id
                            SELECT TOP 1 * FROM UserModel WHERE Id = @id";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("id", SqlDbType.Int)
                    {
                        Value = id,
                    },
                    new SqlParameter("password", SqlDbType.NVarChar)
                    {
                        Value = item.Password,
                    },
                    new SqlParameter("email", SqlDbType.NVarChar)
                    {
                        Value = item.Email,
                    },
                    new SqlParameter("role", SqlDbType.Int)
                    {
                        Value = item.Role,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public async Task<IEnumerable<User>> GetAllByRoleAsync(Role role)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM UserModel
                            WHERE Role = @role";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("role", SqlDbType.Int)
                    {
                        Value = (int)role,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);

                return await MapToListAsync(reader);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM UserModel
                            WHERE Email = @email";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("email", SqlDbType.NVarChar)
                    {
                        Value = email,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public User Map(SqlDataReader reader)
        {
            User user = null;
            if (reader.HasRows)
            {
                user = new User
                {
                    Id = (int)reader["Id"],
                    Password = (string)reader["Password"],
                    Email = (string)reader["Email"],
                    Role = (Role)reader["Role"],
                };
            }

            return user;
        }

        public async Task<IEnumerable<User>> MapToListAsync(SqlDataReader reader)
        {
            List<User> users = new List<User>();
            while (await reader.ReadAsync())
            {
                users.Add(Map(reader));
            }

            reader.Close();

            return users;
        }
    }
}
