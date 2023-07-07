using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace IceCoffee.SimpleCRUD
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _dbAliase;

        public RepositoryFactory(string dbAliase)
        {
            _dbAliase = dbAliase;
        }

        private static readonly ConcurrentDictionary<Type, Func<string, object>> _cachedDelegates = new();
        private static Func<string, object> CreateDelegate(Type type)
        {
            var nameParam = Expression.Parameter(typeof(string), "dbAliase");
            var constructor = typeof(GenericRepository<>).MakeGenericType(type).GetConstructor(new Type[] { typeof(string) });
            var objExpression = Expression.New(constructor, nameParam);
            var lambda = Expression.Lambda<Func<string, object>>(objExpression, nameParam);
            return lambda.Compile();
        }
        private static object CreateInstance(Type entityType, string dbAliase)
        {
            try
            {
                var createObject = _cachedDelegates.GetOrAdd(entityType, CreateDelegate);
                var obj = createObject(dbAliase);
                return obj;
            }
            catch
            {
            }

            return Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(entityType), dbAliase);
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>()
        {
            return (IRepository<TEntity>)CreateInstance(typeof(TEntity), _dbAliase);
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            throw new NotImplementedException();
        }
    }
}