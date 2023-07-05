using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity>
    {
        protected readonly ISqlGenerator SqlGenerator;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, ISqlGeneratorFactory sqlGeneratorFactory, string connectionName) : base(dbConnectionFactory, connectionName)
        {
            SqlGenerator = sqlGeneratorFactory.GetSqlGenerator(dbConnectionFactory.GetOptions(connectionName).DbType, typeof(TEntity));
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, ISqlGeneratorFactory sqlGeneratorFactory)
            : this(dbConnectionFactory, sqlGeneratorFactory, string.Empty)
        {
        }

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, ISqlGeneratorFactory sqlGeneratorFactory, Enum connectionName)
            : this(dbConnectionFactory, sqlGeneratorFactory, connectionName.ToString())
        {
        }

        #region Sync

        public int Delete(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(whereClause, tableName);
            return base.Execute(sql, param, useTransaction);
        }

        public int Delete(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.PrimaryKeyWhereClause, tableName);
            return base.Execute(sql, entity);
        }

        public int Delete(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.PrimaryKeyWhereClause, tableName);
            return base.Execute(sql, entities, useTransaction);
        }

        public int DeleteById<TId>(TId id, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id", tableName);
            return base.Execute(sql, new { Id = id });
        }

        public int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + " IN @Ids", tableName);
            return base.Execute(sql, new { Ids = ids }, useTransaction);
        }

        public TEntity? GetById<TKey>(TKey id, string? tableName = null)
        {
            return this.GetFirstOrDefault(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id }, tableName);
        }

        public TEntity? GetFirstOrDefault(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return base.ExecuteQuery<TEntity>(sql, param).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetList(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return base.ExecuteQuery<TEntity>(sql, param);
        }

        public (IEnumerable<TEntity> Items, int Total) GetPagedList(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";" 
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = base.ExecuteQueryMultiple(sql, param);
            var total = multi.ReadSingle<int>();
            var items = total > 0 ? multi.Read<TEntity>() : Enumerable.Empty<TEntity>();
            return (items, total);
        }

        public int GetRecordCount(string? whereClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName);
            return base.ExecuteScalar<int>(sql, param);
        }

        public int Insert(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.Execute(sql, entity);
        }

        public int Insert(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }

        public int InsertOrIgnore(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.Execute(sql, entity);
        }

        public int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }

        public int InsertOrReplace(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.Execute(sql, entity);
        }

        public int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }

        public int Update(string setClause, string whereClause, object param, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(setClause, whereClause, tableName);
            return base.Execute(sql, param, useTransaction);
        }

        public int Update(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.Execute(sql, entity);
        }

        public int Update(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }

        #endregion

        #region Async

        public Task<int> DeleteAsync(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(whereClause, tableName);
            return base.ExecuteAsync(sql, param, useTransaction);
        }

        public Task<int> DeleteAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.PrimaryKeyWhereClause, tableName);
            return base.ExecuteAsync(sql, entity);
        }

        public Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.PrimaryKeyWhereClause, tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public Task<int> DeleteByIdAsync<TId>(TId id, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id", tableName);
            return base.ExecuteAsync(sql, new { Id = id });
        }

        public Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + " IN @Ids", tableName);
            return base.ExecuteAsync(sql, new { Ids = ids }, useTransaction);
        }

        public Task<TEntity?> GetByIdAsync<TKey>(TKey id, string? tableName = null)
        {
            return this.GetFirstOrDefaultAsync(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id }, tableName);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return (await base.ExecuteQueryAsync<TEntity>(sql, param)).FirstOrDefault();
        }

        public Task<IEnumerable<TEntity>> GetListAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return base.ExecuteQueryAsync<TEntity>(sql, param);
        }

        public async Task<(IEnumerable<TEntity> Items, int Total)> GetPagedListAsync(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";"
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = await base.ExecuteQueryMultipleAsync(sql, param);
            var total = await multi.ReadSingleAsync<int>();
            var items = total > 0 ? await multi.ReadAsync<TEntity>() : Enumerable.Empty<TEntity>();
            return (items, total);
        }

        public Task<int> GetRecordCountAsync(string? whereClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName);
            return base.ExecuteScalarAsync<int>(sql, param);
        }

        public Task<int> InsertAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }

        public Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public Task<int> InsertOrIgnoreAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }

        public Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public Task<int> InsertOrReplaceAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }

        public Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public Task<int> UpdateAsync(string setClause, string whereClause, object param, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(setClause, whereClause, tableName);
            return base.ExecuteAsync(sql, param, useTransaction);
        }

        public Task<int> UpdateAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }

        public Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        #endregion
    }
}