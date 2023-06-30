using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;
        private readonly IRepositoryFactory _repositoryFactory;

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
        }

        public IDbConnection? DbConnection => _connection;
        public IDbTransaction? DbTransaction => _transaction;

        public void Commit()
        {
#pragma warning disable CS8602 // 解引用可能出现空引用。
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
                _transaction = _connection.BeginTransaction();
            }
#pragma warning restore CS8602 // 解引用可能出现空引用。
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
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var repository = _repositoryFactory.GetRepository<TRepository>();
            if (repository is RepositoryBase repositoryBase)
            {
                repositoryBase.SetUnitOfWork(this);
                return repository;
            }
            else
            {
                throw new InvalidOperationException($"The repository: {typeof(TRepository).Name} must inherit from RepositoryBase.");
            }
        }
    }
}