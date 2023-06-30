using Dapper;
using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase : IRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly string _connectionName;
        private IUnitOfWork? _unitOfWork;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string connectionName)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionName = connectionName;
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory)
             : this(dbConnectionFactory, string.Empty)
        {
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, Enum connectionName)
            : this(dbConnectionFactory, connectionName.ToString())
        {
        }

        internal void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IDbConnection DbConnection => _unitOfWork?.DbConnection ?? _dbConnectionFactory.CreateConnection(_connectionName);
        private IDbTransaction? DbTransaction => _unitOfWork?.DbTransaction;

        protected virtual IEnumerable<TEntity> ExecuteQuery<TEntity>(string sql, object? param = null)
        {
        }

        protected virtual TEntity ExecuteQueryFirstOrDefault<TEntity>(string sql, object? param = null)
        {
        }

        protected virtual TEntity ExecuteQuerySingleOrDefault<TEntity>(string sql, object? param = null)
        {
        }

        protected virtual int Execute(string sql, object? param = null)
        {
        }

        protected virtual SqlMapper.GridReader ExecuteMultiple(string sql, object? param = null)
        {
        }

        protected virtual TReturn ExecuteScalar<TReturn>(string sql, object? param = null)
        {
        }

        protected virtual IEnumerable<TReturn> ExecuteProcedure<TReturn>(string procName, DynamicParameters parameters)
        {
        }
    }
}