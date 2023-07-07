using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;

namespace IceCoffee.SimpleCRUD
{
    public abstract class DbConnectionFactoryBase : IDbConnectionFactory
    {
        private readonly ConcurrentDictionary<string, DbProviderFactory> _cachedDbProviderFactory = new();

        public abstract DbConnectionOptions GetOptions(string dbAliase);

        public virtual IDbConnection CreateConnection(string dbAliase)
        {
            var options = GetOptions(dbAliase);
            var dbProviderFactory = _cachedDbProviderFactory.GetOrAdd(dbAliase, (dbAliase) =>
            {
                return DbProviderFactoryHelper.GetDbProviderFactory(options.DbType);
            });

            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = options.ConnectionString;
            return connection;
        }
    }
}