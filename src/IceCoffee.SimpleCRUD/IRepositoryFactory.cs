namespace IceCoffee.SimpleCRUD
{
    public interface IRepositoryFactory
    {
        TRepository GetRepository<TRepository>() where TRepository : class, IRepository;

        IRepository<TEntity> GetGenericRepository<TEntity>();
    }
}