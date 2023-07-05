namespace IceCoffee.SimpleCRUD
{
    public interface IRepositoryFactory
    {
        IRepository? GetRepository(Type type);

        TRepository? GetRepository<TRepository>() where TRepository : class, IRepository;
    }
}