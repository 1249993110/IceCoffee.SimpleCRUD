using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
        IRepository<TEntity> GetGenericRepository<TEntity>();

        void Commit();

        IDbConnection DbConnection { get; }
        IDbTransaction DbTransaction { get; }
    }
}
