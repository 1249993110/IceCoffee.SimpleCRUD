using Microsoft.Extensions.DependencyInjection;

namespace IceCoffee.SimpleCRUD.DependencyInjection
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>()
        {
            throw new NotImplementedException();
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            return (TRepository)((ICloneable)_serviceProvider.GetRequiredService<TRepository>()).Clone();
        }
    }
}