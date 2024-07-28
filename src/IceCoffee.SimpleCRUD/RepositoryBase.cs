using Dapper;
using IceCoffee.SimpleCRUD.SqlGenerators;
using System.Data;
using static Dapper.SqlMapper;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase : IRepository, ICloneable
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly string _dbAliase;
        private IUnitOfWork? _unitOfWork;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string dbAliase)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _dbAliase = dbAliase;
        }

        protected virtual ISqlGenerator GetSqlGenerator<T>()
        {
            return SqlGeneratorFactory.GetSqlGenerator(_dbConnectionFactory.GetOptions(_dbAliase).DbType, typeof(T));
        }

        internal void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected (IDbConnection conn, IDbTransaction? tran) GetDbContext(bool useTransaction = false)
        {
            if (_unitOfWork != null)
            {
                return (_unitOfWork.DbConnection, _unitOfWork.DbTransaction);
            }

            var connection = _dbConnectionFactory.CreateConnection(_dbAliase);
            if (useTransaction)
            {
                connection.Open();
                return (connection, connection.BeginTransaction());
            }

            return (connection, null);
        }

        protected virtual int Execute(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = conn.Execute(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
            }
        }
        protected virtual async Task<int> ExecuteAsync(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                int result = await conn.ExecuteAsync(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
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

        protected virtual IEnumerable<TReturn> ExecuteQuery<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? param = null, string splitOn = "Id")
        {
            var (conn, tran) = GetDbContext();
            return conn.Query(sql, map, param, tran, splitOn: splitOn);
        }
        protected virtual Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? param = null, string splitOn = "Id")
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryAsync(sql, map, param, tran, splitOn: splitOn);
        }

        protected virtual IEnumerable<TReturn> ExecuteQuery<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object? param = null, string splitOn = "Id")
        {
            var (conn, tran) = GetDbContext();
            return conn.Query(sql, map, param, tran, splitOn: splitOn);
        }
        protected virtual Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object? param = null, string splitOn = "Id")
        {
            var (conn, tran) = GetDbContext();
            return conn.QueryAsync(sql, map, param, tran, splitOn: splitOn);
        }

        protected virtual GridReader ExecuteQueryMultiple(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                var result = conn.QueryMultiple(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
            }
        }
        protected virtual async Task<GridReader> ExecuteQueryMultipleAsync(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                var result = await conn.QueryMultipleAsync(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
            }
        }

        protected virtual TReturn? ExecuteScalar<TReturn>(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                var result = conn.ExecuteScalar<TReturn>(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
            }
        }
        protected virtual async Task<TReturn?> ExecuteScalarAsync<TReturn>(string sql, object? param = null, bool useTransaction = false)
        {
            var (conn, tran) = GetDbContext(useTransaction);
            try
            {
                var result = await conn.ExecuteScalarAsync<TReturn>(sql, param, tran);
                if (_unitOfWork == null && tran != null)
                {
                    tran.Commit();
                }

                return result;
            }
            catch
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Rollback();
                }

                throw;
            }
            finally
            {
                if (_unitOfWork == null && tran != null)
                {
                    tran.Dispose();
                    conn.Dispose();
                }
            }
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

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public abstract int DeleteAll(bool useTransaction = false);

        public abstract Task<int> DeleteAllAsync(bool useTransaction = false);
    }
}