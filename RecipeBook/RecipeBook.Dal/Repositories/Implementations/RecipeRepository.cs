using RecipeBook.Common.Models;
using RecipeBook.Dal.Helpers.Interfaces;
using RecipeBook.Dal.Mappers;
using RecipeBook.Dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Implementations
{
    public class RecipeRepository : IRecipeRepository, IMapperBase<Recipe>
    {
        private readonly IDbHelper dbHelper;

        public RecipeRepository(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<Recipe> CreateAsync(Recipe item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"INSERT INTO Recipe (Name, CookingTime, CookingTemperature, CategoryId, ImageData, Description, SequenceActions)
                            VALUES (@name, @cookingTime, @cookingTemperature, @categoryId, @imageData, @description, @sequenceActions)
                            SELECT TOP 1 * FROM Recipe ORDER BY Id DESC";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("name", SqlDbType.NVarChar)
                    {
                        Value = item.Name,
                    },
                    new SqlParameter("cookingTime", SqlDbType.Time)
                    {
                        Value = item.CookingTime,
                    },
                    new SqlParameter("cookingTemperature", SqlDbType.Int)
                    {
                        Value = item.CookingTemperature,
                    },
                    new SqlParameter("categoryId", SqlDbType.Int)
                    {
                        Value = item.CategoryId,
                    },
                    new SqlParameter("imageData", SqlDbType.VarBinary)
                    {
                        Value = item.ImageData,
                    },
                    new SqlParameter("description", SqlDbType.NVarChar)
                    {
                        Value = item.Description,
                    },
                    new SqlParameter("sequenceActions", SqlDbType.NVarChar)
                    {
                        Value = item.SequenceActions,
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
                var sql = @"DELETE Recipe
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

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Recipe";

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql);

                return await MapToListAsync(reader);
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllByCategoryIdAsync(int categoryId)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Recipe
                            WHERE CategoryId = @categoryId";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("categoryId", SqlDbType.Int)
                    {
                        Value = categoryId,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);

                return await MapToListAsync(reader);
            }
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Recipe
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

        public async Task<Recipe> UpdateAsync(int id, Recipe item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"UPDATE Recipe
                            SET Name = @name, 
                                CookingTime = @cookingTime, 
                                CookingTemperature = @cookingTemperature, 
                                CategoryId = @categoryId, 
                                ImageData = @imageData, 
                                Description = @description,
                                SequenceActions = @sequenceActions
                            WHERE Id = @id
                            SELECT TOP 1 * FROM Recipe WHERE Id = @id";

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
                    new SqlParameter("cookingTime", SqlDbType.Time)
                    {
                        Value = item.CookingTime,
                    },
                    new SqlParameter("cookingTemperature", SqlDbType.Int)
                    {
                        Value = item.CookingTemperature,
                    },
                    new SqlParameter("categoryId", SqlDbType.Int)
                    {
                        Value = item.CategoryId,
                    },
                    new SqlParameter("imageData", SqlDbType.VarBinary)
                    {
                        Value = item.ImageData,
                    },
                    new SqlParameter("description", SqlDbType.NVarChar)
                    {
                        Value = item.Description,
                    },
                    new SqlParameter("sequenceActions", SqlDbType.NVarChar)
                    {
                        Value = item.SequenceActions,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);
                await reader.ReadAsync();

                return Map(reader);
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllByCategoryAndNameAsync(int categoryId, string recipePartName)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM Recipe
                            WHERE CategoryId = @categoryId AND Name LIKE @name";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("name", SqlDbType.NVarChar)
                    {
                        Value = "%" + recipePartName + "%",
                    },
                    new SqlParameter("categoryId", SqlDbType.Int)
                    {
                        Value = categoryId,
                    },
                };

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sql, sqlParameters);

                return await MapToListAsync(reader);
            }
        }

        public Recipe Map(SqlDataReader reader)
        {
            Recipe recipe = null;
            if (reader.HasRows)
            {
                recipe = new Recipe
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    CookingTime = (TimeSpan)reader["CookingTime"],
                    CookingTemperature = (int)reader["CookingTemperature"],
                    CategoryId = (int)reader["CategoryId"],
                    ImageData = (byte[])reader["ImageData"],
                    Description = (string)reader["Description"],
                    SequenceActions = (string)reader["SequenceActions"],
                };
            }

            return recipe;
        }

        public async Task<IEnumerable<Recipe>> MapToListAsync(SqlDataReader reader)
        {
            List<Recipe> recipes = new List<Recipe>();
            while (await reader.ReadAsync())
            {
                recipes.Add(Map(reader));
            }

            reader.Close();

            return recipes;
        }

        
    }
}
