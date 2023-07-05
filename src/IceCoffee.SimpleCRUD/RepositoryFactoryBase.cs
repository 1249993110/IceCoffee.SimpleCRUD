namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryFactoryBase : IRepositoryFactory
    {
        public abstract IRepository? GetRepository(Type type);

        public virtual TRepository? GetRepository<TRepository>() where TRepository : class, IRepository
        {
            return GetRepository(typeof(TRepository)) as TRepository;
        }
    }
}