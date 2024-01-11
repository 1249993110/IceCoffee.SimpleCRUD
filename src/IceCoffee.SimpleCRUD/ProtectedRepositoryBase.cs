using IceCoffee.SimpleCRUD.Dtos;
using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public abstract class ProtectedRepositoryBase<TEntity> : RepositoryBase
    {
        protected readonly ISqlGenerator SqlGenerator;

        public ProtectedRepositoryBase(IDbConnectionFactory dbConnectionFactory, string dbAliase) : base(dbConnectionFactory, dbAliase)
        {
            SqlGenerator = SqlGeneratorFactory.GetSqlGenerator(dbConnectionFactory.GetOptions(dbAliase).DbType, typeof(TEntity));
        }

        #region Get
        protected virtual IEnumerable<TEntity> GetList(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return base.ExecuteQuery<TEntity>(sql, param);
        }
        protected virtual Task<IEnumerable<TEntity>> GetListAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetSelectStatement(whereClause, orderByClause, tableName);
            return base.ExecuteQueryAsync<TEntity>(sql, param);
        }
        protected virtual TEntity? GetFirstOrDefault(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            return this.GetList(whereClause, orderByClause, param, tableName).FirstOrDefault();
        }
        protected virtual async Task<TEntity?> GetFirstOrDefaultAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            return (await this.GetListAsync(whereClause, orderByClause, param, tableName)).FirstOrDefault();
        }
        protected virtual TEntity? GetById<TKey>(TKey id, string? tableName = null)
        {
            return this.GetFirstOrDefault(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id }, tableName);
        }
        protected virtual Task<TEntity?> GetByIdAsync<TKey>(TKey id, string? tableName = null)
        {
            return this.GetFirstOrDefaultAsync(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id }, tableName);
        }
        protected virtual PagedDto<TEntity> GetPagedList(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";"
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = base.ExecuteQueryMultiple(sql, param);
            var total = multi.ReadSingle<int>();
            var items = total > 0 ? multi.Read<TEntity>() : Enumerable.Empty<TEntity>();
            return new PagedDto<TEntity>() { Total = total, Items = items };
        }
        protected virtual async Task<PagedDto<TEntity>> GetPagedListAsync(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";"
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = await base.ExecuteQueryMultipleAsync(sql, param);
            var total = await multi.ReadSingleAsync<int>();
            var items = total > 0 ? await multi.ReadAsync<TEntity>() : Enumerable.Empty<TEntity>();
            return new PagedDto<TEntity>() { Total = total, Items = items };
        }
        protected virtual int GetRecordCount(string? whereClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName);
            return base.ExecuteScalar<int>(sql, param);
        }
        protected virtual Task<int> GetRecordCountAsync(string? whereClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName);
            return base.ExecuteScalarAsync<int>(sql, param);
        }
        #endregion

        #region Insert
        protected virtual int Insert(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.Execute(sql, entity);
        }
        protected virtual Task<int> InsertAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }
        protected virtual int Insert(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }
        protected virtual Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        protected virtual int InsertOrIgnore(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.Execute(sql, entity);
        }
        protected virtual Task<int> InsertOrIgnoreAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }
        protected virtual int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }
        protected virtual Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        protected virtual int InsertOrReplace(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.Execute(sql, entity);
        }
        protected virtual Task<int> InsertOrReplaceAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }
        protected virtual int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }
        protected virtual Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        #endregion

        #region Update
        protected virtual int Update(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.Execute(sql, entity);
        }
        protected virtual Task<int> UpdateAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.ExecuteAsync(sql, entity);
        }
        protected virtual int Update(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.Execute(sql, entities, useTransaction);
        }
        protected virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetUpdateStatement(tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        #endregion

        #region Delete
        protected virtual int Delete(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(whereClause, tableName);
            return base.Execute(sql, param, useTransaction);
        }
        protected virtual Task<int> DeleteAsync(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(whereClause, tableName);
            return base.ExecuteAsync(sql, param, useTransaction);
        }
        protected virtual int Delete(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause(), tableName);
            return base.Execute(sql, entity);
        }
        protected virtual Task<int> DeleteAsync(TEntity entity, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause(), tableName);
            return base.ExecuteAsync(sql, entity);
        }
        protected virtual int Delete(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause(), tableName);
            return base.Execute(sql, entities, useTransaction);
        }
        protected virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause(), tableName);
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        protected virtual int DeleteById<TId>(TId id, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id", tableName);
            return base.Execute(sql, new { Id = id });
        }
        protected virtual Task<int> DeleteByIdAsync<TId>(TId id, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id", tableName);
            return base.ExecuteAsync(sql, new { Id = id });
        }
        protected virtual int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + SqlGenerator.GetInIdsClause(), tableName);
            return base.Execute(sql, new { Ids = ids }, useTransaction);
        }
        protected virtual Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + SqlGenerator.GetInIdsClause(), tableName);
            return base.ExecuteAsync(sql, new { Ids = ids }, useTransaction);
        }
        #endregion

    }

}
