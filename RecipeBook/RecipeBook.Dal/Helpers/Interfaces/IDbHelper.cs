using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Helpers.Interfaces
{
    public interface IDbHelper
    {
        Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string sqlExpression);

        Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string sqlExpression, IEnumerable<SqlParameter> parameters);

        Task<int> ExecuteNonQueryAsync(SqlConnection connection, string sqlExpression);

        Task<int> ExecuteNonQueryAsync(SqlConnection connection, string sqlExpression, IEnumerable<SqlParameter> parameters);
    }
}
