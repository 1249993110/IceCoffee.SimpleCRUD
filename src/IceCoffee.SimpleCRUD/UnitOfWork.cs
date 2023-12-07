using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IsolationLevel? _isolationLevel;

        public UnitOfWork(IDbConnection connection, IRepositoryFactory repositoryFactory)
        {
            _connection = connection;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
            _repositoryFactory = repositoryFactory;
        }

        public UnitOfWork(IDbConnection connection, IRepositoryFactory repositoryFactory, IsolationLevel il)
        {
            _connection = connection;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction(il);
            _repositoryFactory = repositoryFactory;
            _isolationLevel = il;
        }

        public IDbConnection DbConnection => _connection;
        public IDbTransaction DbTransaction => _transaction;

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _isolationLevel.HasValue ? _connection.BeginTransaction(_isolationLevel.Value) : _connection.BeginTransaction();
            }
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                    _connection.Dispose();
                }

                _disposed = true;
            }
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            var tRepository = _repositoryFactory.GetRepository<TRepository>();
            if (tRepository is RepositoryBase repositoryBase)
            {
                repositoryBase.SetUnitOfWork(this);
            }
            else
            {
                throw new InvalidOperationException($"The repository: {typeof(TRepository).Name} must inherit from RepositoryBase.");
            }

            return tRepository;
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>()
        {
            var tRepository = _repositoryFactory.GetGenericRepository<TEntity>();
            if (tRepository is RepositoryBase repositoryBase)
            {
                repositoryBase.SetUnitOfWork(this);
            }
            else
            {
                throw new InvalidOperationException($"The repository: {tRepository.GetType().Name} must inherit from RepositoryBase.");
            }

            return tRepository;
        }
    }
}