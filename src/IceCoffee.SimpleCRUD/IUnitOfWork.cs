using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
        
        void Commit();

        IDbConnection? DbConnection { get; }
        IDbTransaction? DbTransaction { get; }
    }
}
