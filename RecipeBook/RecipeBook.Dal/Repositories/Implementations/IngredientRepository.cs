using RecipeBook.Common.Models;
using RecipeBook.Dal.Helpers.Interfaces;
using RecipeBook.Dal.Mappers;
using RecipeBook.Dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Implementations
{
    public class IngredientRepository : IIngredientRepository, IMapperBase<Ingredient>
    {
        private readonly IDbHelper dbHelper;

        public IngredientRepository(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<Ingredient> CreateAsync(Ingredient item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"INSERT INTO Ingredient (Name, Weight)
                            VALUES (@name, @weight)
                            SELECT TOP 1 * FROM Ingredient ORDER BY Id DESC";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("name", SqlDbType.NVarChar)
                    {
                        Value = item.Name,
                    },
                    new SqlParameter("weight", SqlDbType.Real)
                    {
                        Value = item.Weight,
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
                var sql = @"DELETE Ingredient
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

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Ingredient";

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql);

                return await MapToListAsync(reader);
            }
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Ingredient
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

        public async Task<Ingredient> UpdateAsync(int id, Ingredient item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"UPDATE Ingredient
                            SET Name = @name, 
                                Weight = @weight
                            WHERE Id = @id
                            SELECT TOP 1 * FROM Ingredient WHERE Id = @id";

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
                    new SqlParameter("weight", SqlDbType.Real)
                    {
                        Value = item.Weight,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public Ingredient Map(SqlDataReader reader)
        {
            Ingredient ingredient = null;
            if (reader.HasRows)
            {
                ingredient = new Ingredient
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Weight = Convert.ToDouble(reader["Weight"]),
                };
            }

            return ingredient;
        }

        public async Task<IEnumerable<Ingredient>> MapToListAsync(SqlDataReader reader)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            while (await reader.ReadAsync())
            {
                ingredients.Add(Map(reader));
            }

            reader.Close();

            return ingredients;
        }
    }
}
