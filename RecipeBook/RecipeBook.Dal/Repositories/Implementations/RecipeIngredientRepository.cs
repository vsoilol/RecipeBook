using RecipeBook.Common.Models;
using RecipeBook.Dal.Helpers.Interfaces;
using RecipeBook.Dal.Mappers;
using RecipeBook.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Repositories.Implementations
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository, IMapperBase<RecipeIngredient>
    {
        private readonly IDbHelper dbHelper;

        public RecipeIngredientRepository(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<IEnumerable<RecipeIngredient>> CreateAsync(IEnumerable<RecipeIngredient> items)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"INSERT INTO RecipeIngredient (IngredientId, RecipeId)
                            VALUES";

                StringBuilder sqlStringBuilder = new StringBuilder(sql);
                var sqlParameters = new SqlParameter[items.Count() * 2];

                int i = 0;
                foreach (var item in items)
                {
                    sqlStringBuilder.AppendFormat("(@ingredientId{0}, @recipeId{0}),", i);

                    sqlParameters[i] = new SqlParameter($"ingredientId{i}", SqlDbType.Int)
                    {
                        Value = item.IngredientId,
                    };

                    sqlParameters[i + 1] = new SqlParameter($"recipeId{i}", SqlDbType.Int)
                    {
                        Value = item.RecipeId,
                    };
                    i += 2;
                }

                sqlStringBuilder.Replace(',', ' ', sqlStringBuilder.Length - 1, 1);

                await dbHelper.ExecuteNonQueryAsync(sqlConnection, sqlStringBuilder.ToString(), sqlParameters);

                return items;
            }
        }

        public async Task<bool> DeleteByRecipeIdAsync(int recipeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"DELETE RecipeIngredient
                            WHERE RecipeId = @recipeId";

                var sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("recipeId", SqlDbType.Int)
                    {
                        Value = recipeId,
                    },
                };

                return (await dbHelper.ExecuteNonQueryAsync(sqlConnection, sql, sqlParameters)) != 0;
            }
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAllByIngredientsIdAsync(IEnumerable<int> ingredientsId)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                var sql = @"SELECT * FROM RecipeIngredient
                            WHERE IngredientId IN (";

                int i = 0;
                StringBuilder sqlStringBuilder = new StringBuilder(sql);
                var sqlParameters = new SqlParameter[ingredientsId.Count()];

                foreach (var id in ingredientsId)
                {
                    sqlStringBuilder.Append($"@ingredientId{i},");

                    sqlParameters[i] = new SqlParameter($"ingredientId{i}", SqlDbType.Int)
                    {
                        Value = id,
                    };

                    i++;
                }

                sqlStringBuilder.Replace(',', ')', sqlStringBuilder.Length - 1, 1);

                var reader = await dbHelper.ExecuteReaderAsync(sqlConnection, sqlStringBuilder.ToString(), sqlParameters);

                return await MapToListAsync(reader);
            }
        }

        public RecipeIngredient Map(SqlDataReader reader)
        {
            RecipeIngredient recipeIngredient = null;
            if (reader.HasRows)
            {
                recipeIngredient = new RecipeIngredient
                {
                    IngredientId = (int)reader["IngredientId"],
                    RecipeId = (int)reader["RecipeId"],
                };
            }

            return recipeIngredient;
        }

        public async Task<IEnumerable<RecipeIngredient>> MapToListAsync(SqlDataReader reader)
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            while (await reader.ReadAsync())
            {
                recipeIngredients.Add(Map(reader));
            }

            reader.Close();

            return recipeIngredients;
        }
    }
}
