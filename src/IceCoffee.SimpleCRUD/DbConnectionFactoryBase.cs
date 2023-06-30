using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;

namespace IceCoffee.SimpleCRUD
{
    public abstract class DbConnectionFactoryBase : IDbConnectionFactory
    {
        private readonly ConcurrentDictionary<string, DbProviderFactory> _cachedDbProviderFactory;

        public abstract DbConnectionOptions GetOptions(string connectionName);

        public virtual IDbConnection CreateConnection(string connectionName)
        {
            var options = GetOptions(connectionName);
            var dbProviderFactory = _cachedDbProviderFactory.GetOrAdd(connectionName, (connectionName) =>
            {
                return DbProviderFactoryHelper.GetDbProviderFactory(options.DbType);
            });

            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = options.ConnectionString;
            return connection;
        }

        public virtual DbConnectionOptions GetOptions(Enum connectionName)
        {
            return GetOptions(connectionName.ToString());
        }

        public virtual IDbConnection CreateConnection(Enum connectionName)
        {
            return CreateConnection(connectionName.ToString());
        }
    }
}