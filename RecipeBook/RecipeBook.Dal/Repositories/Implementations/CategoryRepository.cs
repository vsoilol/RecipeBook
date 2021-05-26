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
    public class CategoryRepository : IRepository<Category>, IMapperBase<Category>
    {
        private readonly IDbHelper dbHelper;

        public CategoryRepository(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<Category> CreateAsync(Category item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"INSERT INTO Category (Name)
                            VALUES (@name)
                            SELECT TOP 1 * FROM Category ORDER BY Id DESC";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("name", SqlDbType.NVarChar)
                    {
                        Value = item.Name,
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
                var sql = @"DELETE Category
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Category";

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql);

                return await MapToListAsync(reader);
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Category
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

        public async Task<Category> UpdateAsync(int id, Category item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"UPDATE Category
                            SET Name = @name
                            WHERE Id = @id
                            SELECT TOP 1 * FROM Category WHERE Id = @id";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("id", SqlDbType.Int)
                    {
                        Value = id,
                    },
                    new SqlParameter("name", SqlDbType.NVarChar)
                    {
                        Value = item.Name,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public Category Map(SqlDataReader reader)
        {
            Category category = null;
            if (reader.HasRows)
            {
                category = new Category
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                };
            }

            return category;
        }

        public async Task<IEnumerable<Category>> MapToListAsync(SqlDataReader reader)
        {
            List<Category> categories = new List<Category>();
            while (await reader.ReadAsync())
            {
                categories.Add(Map(reader));
            }

            reader.Close();

            return categories;
        }
    }
}
