using Microsoft.Data.Sqlite;

namespace IceCoffee.SimpleCRUD.UnitTest
{
    internal static class InMemorySample
    {
        private static SqliteConnection _connection;
        public static void Init()
        {
            if( _connection == null)
            {
                string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
                DbConnectionFactory.Default.ConfigureOptions(connectionString, DbType.SQLite);

                // Keep Open.
                _connection = new SqliteConnection(connectionString);
                _connection.Open();
            }
        }
    }
}