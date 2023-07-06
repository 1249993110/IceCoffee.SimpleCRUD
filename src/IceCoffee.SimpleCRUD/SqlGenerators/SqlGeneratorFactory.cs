using System.Collections.Concurrent;

namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class SqlGeneratorFactory : ISqlGeneratorFactory
    {
        private static readonly Lazy<SqlGeneratorFactory> _default = new(true);
        public static SqlGeneratorFactory Default => _default.Value;

        private ConcurrentDictionary<Type, ISqlGenerator> _sqlServerSqlGenerators;
        private ConcurrentDictionary<Type, ISqlGenerator> _sqlLiteSqlGenerators;
        private ConcurrentDictionary<Type, ISqlGenerator> _postgreSqlGenerators;
        private ConcurrentDictionary<Type, ISqlGenerator> _mySqlGenerators;

        public ISqlGenerator GetSqlGenerator(DbType dbType, Type entityType)
        {
            switch (dbType)
            {
                case DbType.SQLServer:
                    return (_sqlServerSqlGenerators ??= new ConcurrentDictionary<Type, ISqlGenerator>())
                        .GetOrAdd(entityType, (entityType) => new SqlServerSqlGenerator(entityType));

                case DbType.SQLite:
                    return (_sqlLiteSqlGenerators ??= new ConcurrentDictionary<Type, ISqlGenerator>())
                        .GetOrAdd(entityType, (entityType) => new SqlLiteSqlGenerator(entityType));

                case DbType.PostgreSQL:
                    return (_postgreSqlGenerators ??= new ConcurrentDictionary<Type, ISqlGenerator>())
                        .GetOrAdd(entityType, (entityType) => new PostgreSqlGenerator(entityType));

                case DbType.MySQL:
                    return (_mySqlGenerators ??= new ConcurrentDictionary<Type, ISqlGenerator>())
                        .GetOrAdd(entityType, (entityType) => new MySqlGenerator(entityType));

                case DbType.Undefined:
                default:
                    throw new NotSupportedException("Undefined dbType.");
            }
        }
    }
}