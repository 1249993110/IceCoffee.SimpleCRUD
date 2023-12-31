﻿namespace IceCoffee.SimpleCRUD
{
    public class DbConnectionFactory : DbConnectionFactoryBase
    {
        private static readonly Lazy<DbConnectionFactory> _default = new(true);
        public static DbConnectionFactory Default => _default.Value;

        private readonly IDictionary<string, DbConnectionOptions> _optionsDict;

        public DbConnectionFactory()
        {
            _optionsDict = new Dictionary<string, DbConnectionOptions>();
        }
        public DbConnectionFactory(IDictionary<string, DbConnectionOptions> optionsDict)
        {
            _optionsDict = optionsDict;
        }

        public override DbConnectionOptions GetOptions(string dbAliase)
        {
            return _optionsDict[dbAliase];
        }

        public virtual DbConnectionFactory ConfigureOptions(DbConnectionOptions options)
        {
            return ConfigureOptions(string.Empty, options);
        }

        public virtual DbConnectionFactory ConfigureOptions(string dbAliase, DbConnectionOptions options)
        {
            _optionsDict[dbAliase] = options;
            return this;
        }

        public virtual DbConnectionFactory ConfigureOptions(string connectionString, DbType dbType)
        {
            return ConfigureOptions(string.Empty, new DbConnectionOptions() { ConnectionString = connectionString, DbType = dbType });
        }

        public virtual DbConnectionFactory ConfigureOptions(string dbAliase, string connectionString, DbType dbType)
        {
            return ConfigureOptions(dbAliase, new DbConnectionOptions() { ConnectionString = connectionString, DbType = dbType });
        }
    }
}