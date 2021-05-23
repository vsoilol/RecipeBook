using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Common.Helpers
{
    public class DbConfig
    {
        public DbConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
