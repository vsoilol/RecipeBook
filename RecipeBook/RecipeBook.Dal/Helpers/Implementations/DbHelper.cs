using RecipeBook.Common.Helpers;
using RecipeBook.Dal.Helpers.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Helpers.Implementations
{
    public class DbHelper : IDbHelper
    {
        private readonly DbConfig config;

        public DbHelper(DbConfig config)
        {
            this.config = config;
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string sqlExpression)
        {
            connection.ConnectionString = config.ConnectionString;
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            return reader;
        }

        public async Task<int> ExecuteNonQueryAsync(SqlConnection connection, string sqlExpression)
        {
            connection.ConnectionString = config.ConnectionString;
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string sqlExpression, IEnumerable<SqlParameter> parameters)
        {
            connection.ConnectionString = config.ConnectionString;
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddRange(parameters.ToArray());
            SqlDataReader reader = await command.ExecuteReaderAsync();

            return reader;
        }

        public async Task<int> ExecuteNonQueryAsync(SqlConnection connection, string sqlExpression, IEnumerable<SqlParameter> parameters)
        {
            connection.ConnectionString = config.ConnectionString;
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddRange(parameters.ToArray());
            return await command.ExecuteNonQueryAsync();
        }
    }
}
