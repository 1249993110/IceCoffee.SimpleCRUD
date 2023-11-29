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
            if(constructor == null)
            {
                throw new Exception("Cannot get the constructor method of the specified class: " + type.FullName);
            }

            var objExpression = Expression.New(constructor, nameParam);
            var lambda = Expression.Lambda<Func<string, object>>(objExpression, nameParam);
            return lambda.Compile();
        }
        private static object CreateInstance(Type entityType, string dbAliase)
        {
            object? obj;
            try
            {
                var createObject = _cachedDelegates.GetOrAdd(entityType, CreateDelegate);
                obj = createObject.Invoke(dbAliase);
                return obj;
            }
            catch
            {
            }

            obj = Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(entityType), dbAliase);
            return obj ?? throw new Exception($"Failed to instantiate GenericRepository with entity type {entityType.FullName}");
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