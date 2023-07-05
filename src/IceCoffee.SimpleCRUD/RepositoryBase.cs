using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase : IRepository, ICloneable
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

        private (IDbConnection conn, IDbTransaction? tran) GetDbContext(bool useTransaction = false)
        {
            if(_unitOfWork != null)
            {
                return (_unitOfWork.DbConnection, _unitOfWork.DbTransaction);
            }

            var connection = _dbConnectionFactory.CreateConnection(_connectionName);
            return (connection, useTransaction ? connection.BeginTransaction() : null);
        }

        protected virtual int Execute(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = conn.Execute(sql, param, tran);
                if (useTransaction && _unitOfWork == null)
                {
                    tran?.Commit();
                }

                return result;
            }
            catch
            {
                if (useTransaction && _unitOfWork == null)
                {
                    tran?.Rollback();
                }

                throw;
            }
        }
        protected virtual async Task<int> ExecuteAsync(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = await conn.ExecuteAsync(sql, param, tran);
                if (useTransaction && _unitOfWork == null)
                {
                    tran?.Commit();
                }

                return result;
            }
            catch
            {
                if (useTransaction && _unitOfWork == null)
                {
                    tran?.Rollback();
                }

                throw;
            }
        }

        protected virtual IEnumerable<TEntity> ExecuteQuery<TEntity>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.Query<TEntity>(sql, param, tran);
        }
        protected virtual Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryAsync<TEntity>(sql, param, tran);
        }

        protected virtual GridReader ExecuteQueryMultiple(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryMultiple(sql, param, tran);
        }
        protected virtual Task<GridReader> ExecuteQueryMultipleAsync(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryMultipleAsync(sql, param, tran);
        }

        protected virtual TReturn ExecuteScalar<TReturn>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.ExecuteScalar<TReturn>(sql, param, tran);
        }
        protected virtual Task<TReturn> ExecuteScalarAsync<TReturn>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.ExecuteScalarAsync<TReturn>(sql, param, tran);
        }

        protected virtual IEnumerable<TEntity> ExecuteProcedure<TEntity>(string procName, DynamicParameters parameters)
        {
            var (conn, tran) = GetDbContext();
            return conn.Query<TEntity>(new CommandDefinition(procName, parameters, tran, commandType: CommandType.StoredProcedure));
        }
        protected virtual Task<IEnumerable<TEntity>> ExecuteProcedureAsync<TEntity>(string procName, DynamicParameters parameters)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryAsync<TEntity>(new CommandDefinition(procName, parameters, tran, commandType: CommandType.StoredProcedure));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}