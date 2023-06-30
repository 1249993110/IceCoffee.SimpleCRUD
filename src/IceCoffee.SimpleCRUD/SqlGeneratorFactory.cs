using IceCoffee.SimpleCRUD.SqlGenerators;
using System.Collections.Concurrent;

namespace IceCoffee.SimpleCRUD
{
    public class SqlGeneratorFactory
    {
        private readonly ConcurrentDictionary<Type, ISqlGenerator> _cachedSqlGenerator;

        public ISqlGenerator Create(DbType dbTy pe, Type entityType)
        {
            switch (dbType)
            {
                case DbType.SQLServer:
                    return new SqlServerSqlGenerator(entityType);
            }
        }
    }
}