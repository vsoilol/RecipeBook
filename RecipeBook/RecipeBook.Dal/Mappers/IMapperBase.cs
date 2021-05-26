using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Dal.Mappers
{
    public interface IMapperBase<T>
    {
        T Map(SqlDataReader reader);

        Task<IEnumerable<T>> MapToListAsync(SqlDataReader reader);
    }
}
