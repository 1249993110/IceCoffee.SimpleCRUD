namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryFactoryBase : IRepositoryFactory
    {
        public abstract IRepository GetRepository(Type type);

        public virtual TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return (TRepository)GetRepository(typeof(TRepository));
        }
    }
}