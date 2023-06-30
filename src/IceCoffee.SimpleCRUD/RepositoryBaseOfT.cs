using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity>
    {
        protected readonly ISqlGenerator SqlGenerator;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string connectionName) : base(dbConnectionFactory, connectionName)
        {
            SqlGenerator = new SqlGenerator<TEntity>(dbConnectionFactory.GetOptions(connectionName).DbType);
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory)
            : this(dbConnectionFactory, string.Empty)
        {
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, Enum connectionName)
            : this(dbConnectionFactory, connectionName.ToString())
        {
        }

        public int Delete(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Delete(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int DeleteById<TId>(TId id, string? idColumnName = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int DeleteByIds<TId>(IEnumerable<TId> ids, string? idColumnName = null, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TKey>(TKey id, string? idColumnName = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetByRawSql(string sql, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetFirstOrDefault(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetList(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<TEntity> Items, int Total) GetPagedList(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount(string? whereClause = null, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public TScalar GetScalar<TScalar>(string sql, object? param = null, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(TEntity entity, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int InsertOrIgnore(TEntity entity, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int InsertOrReplace(TEntity entity, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Update(string setClause, string? whereClause, object param, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity, string? tableName = null)
        {
            throw new NotImplementedException();
        }

        public int Update(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            throw new NotImplementedException();
        }
    }
}