using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace IceCoffee.SimpleCRUD
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _connectionName;

        public RepositoryFactory(string connectionName)
        {
            _connectionName = connectionName;
        }

        private static readonly ConcurrentDictionary<Type, Func<string, object>> _cachedDelegates = new();
        private static Func<string, object> CreateDelegate(Type type)
        {
            var nameParam = Expression.Parameter(typeof(string), "connectionName");
            var constructor = typeof(GenericRepository<>).MakeGenericType(type).GetConstructor(new Type[] { typeof(string) });
            var objExpression = Expression.New(constructor, nameParam);
            var lambda = Expression.Lambda<Func<string, object>>(objExpression, nameParam);
            return lambda.Compile();
        }
        private static object CreateInstance(Type entityType, string connectionName)
        {
            try
            {
                var createObject = _cachedDelegates.GetOrAdd(entityType, CreateDelegate);
                var obj = createObject(connectionName);
                return obj;
            }
            catch
            {
            }

            return Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(entityType), connectionName);
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>()
        {
            return (IRepository<TEntity>)CreateInstance(typeof(TEntity), _connectionName);
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            throw new NotImplementedException();
        }
    }
}