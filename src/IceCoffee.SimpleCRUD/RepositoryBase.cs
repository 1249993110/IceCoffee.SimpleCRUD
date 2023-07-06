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
            if(useTransaction)
            {
                connection.Open();
                return (connection, connection.BeginTransaction());
            }

            return (connection, null);
        }

#pragma warning disable CS8602 // 解引用可能出现空引用。
        public virtual int Execute(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = conn.Execute(sql, param, tran);
                if (useTransaction && _unitOfWork == null)
                {
                    tran.Commit();
                    tran.Dispose();
                    conn.Dispose();
                }

                return result;
            }
            catch
            {
                if (useTransaction && _unitOfWork == null)
                {
                    tran.Rollback();
                    conn.Dispose();
                }

                throw;
            }
        }
        public virtual async Task<int> ExecuteAsync(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = await conn.ExecuteAsync(sql, param, tran);
                if (useTransaction && _unitOfWork == null)
                {
                    tran.Commit();
                    tran.Dispose();
                    conn.Dispose();
                }

                return result;
            }
            catch
            {
                if (useTransaction && _unitOfWork == null)
                {
                    tran.Rollback();
                    conn.Dispose();
                }

                throw;
            }
        }
#pragma warning restore CS8602 // 解引用可能出现空引用。

        public virtual IEnumerable<TEntity> ExecuteQuery<TEntity>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.Query<TEntity>(sql, param, tran);
        }
        public virtual Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryAsync<TEntity>(sql, param, tran);
        }

        public virtual GridReader ExecuteQueryMultiple(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryMultiple(sql, param, tran);
        }
        public virtual Task<GridReader> ExecuteQueryMultipleAsync(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryMultipleAsync(sql, param, tran);
        }

        public virtual TReturn ExecuteScalar<TReturn>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.ExecuteScalar<TReturn>(sql, param, tran);
        }
        public virtual Task<TReturn> ExecuteScalarAsync<TReturn>(string sql, object? param = null)
        {
            var (conn, tran) = GetDbContext();
            return conn.ExecuteScalarAsync<TReturn>(sql, param, tran);
        }

        public virtual IEnumerable<TEntity> ExecuteProcedure<TEntity>(string procName, DynamicParameters parameters)
        {
            var (conn, tran) = GetDbContext();
            return conn.Query<TEntity>(new CommandDefinition(procName, parameters, tran, commandType: CommandType.StoredProcedure));
        }
        public virtual Task<IEnumerable<TEntity>> ExecuteProcedureAsync<TEntity>(string procName, DynamicParameters parameters)
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