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
