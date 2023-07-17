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

        #region Protected
        protected virtual TEntity? GetFirstOrDefault(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            return this.GetList(whereClause, orderByClause, param, tableName).FirstOrDefault();
        }
        protected virtual async Task<TEntity?> GetFirstOrDefaultAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            return (await this.GetListAsync(whereClause, orderByClause, param, tableName)).FirstOrDefault();
        }
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
        protected virtual (int Total, IEnumerable<TEntity> Items) GetPagedList(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";"
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = base.ExecuteQueryMultiple(sql, param);
            var total = multi.ReadSingle<int>();
            var items = total > 0 ? multi.Read<TEntity>() : Enumerable.Empty<TEntity>();
            return (total, items);
        }
        protected virtual async Task<(int Total, IEnumerable<TEntity> Items)> GetPagedListAsync(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null)
        {
            string sql = SqlGenerator.GetRecordCountStatement(whereClause, tableName) + ";"
                + SqlGenerator.GetSelectPagedStatement(pageNumber, pageSize, whereClause, orderByClause, tableName);

            using var multi = await base.ExecuteQueryMultipleAsync(sql, param);
            var total = await multi.ReadSingleAsync<int>();
            var items = total > 0 ? await multi.ReadAsync<TEntity>() : Enumerable.Empty<TEntity>();
            return (total, items);
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
        #endregion

    }

    public abstract class RepositoryBase<TEntity> : ProtectedRepositoryBase<TEntity>, IRepository<TEntity>
    {
        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string dbAliase) : base(dbConnectionFactory, dbAliase)
        {
        }

        #region Sync
        public virtual TEntity? GetById<TKey>(TKey id)
        {
            return base.GetFirstOrDefault(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id });
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return base.GetList();
        }

        public virtual (int Total, IEnumerable<TEntity> Items) GetPagedList(int pageNumber, int pageSize)
        {
            return base.GetPagedList(pageNumber, pageSize);
        }

        public virtual int GetRecordCount()
        {
            return base.GetRecordCount();
        }

        public virtual int Delete(TEntity entity)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause());
            return base.Execute(sql, entity);
        }

        public virtual int Delete(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause());
            return base.Execute(sql, entities, useTransaction);
        }

        public virtual int DeleteById<TId>(TId id)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id");
            return base.Execute(sql, new { Id = id });
        }

        public virtual int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + " IN @Ids");
            return base.Execute(sql, new { Ids = ids }, useTransaction);
        }

        public virtual int Insert(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertStatement();
            return base.Execute(sql, entity);
        }

        public virtual int Insert(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertStatement();
            return base.Execute(sql, entities, useTransaction);
        }

        public virtual int InsertOrIgnore(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement();
            return base.Execute(sql, entity);
        }

        public virtual int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement();
            return base.Execute(sql, entities, useTransaction);
        }

        public virtual int InsertOrReplace(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement();
            return base.Execute(sql, entity);
        }

        public virtual int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement();
            return base.Execute(sql, entities, useTransaction);
        }

        public virtual int Update(TEntity entity)
        {
            string sql = SqlGenerator.GetUpdateStatement();
            return base.Execute(sql, entity);
        }

        public virtual int Update(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetUpdateStatement();
            return base.Execute(sql, entities, useTransaction);
        }

        #endregion

        #region Async

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause());
            return base.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetPrimaryKeyWhereClause());
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public virtual Task<int> DeleteByIdAsync<TId>(TId id)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + "=@Id");
            return base.ExecuteAsync(sql, new { Id = id });
        }

        public virtual Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetDeleteStatement(SqlGenerator.GetSingleKey() + " IN @Ids");
            return base.ExecuteAsync(sql, new { Ids = ids }, useTransaction);
        }

        public virtual Task<TEntity?> GetByIdAsync<TKey>(TKey id)
        {
            return base.GetFirstOrDefaultAsync(SqlGenerator.GetSingleKey() + "=@Id", null, new { Id = id });
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return base.GetListAsync();
        }

        public virtual Task<(int Total, IEnumerable<TEntity> Items)> GetPagedListAsync(int pageNumber, int pageSize)
        {
            return base.GetPagedListAsync(pageNumber, pageSize);
        }

        public virtual Task<int> GetRecordCountAsync()
        {
            return base.GetRecordCountAsync();
        }

        public virtual Task<int> InsertAsync(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertStatement();
            return base.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertStatement();
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public virtual Task<int> InsertOrIgnoreAsync(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement();
            return base.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertOrIgnoreStatement();
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public virtual Task<int> InsertOrReplaceAsync(TEntity entity)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement();
            return base.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetInsertOrReplaceStatement();
            return base.ExecuteAsync(sql, entities, useTransaction);
        }

        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            string sql = SqlGenerator.GetUpdateStatement();
            return base.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            string sql = SqlGenerator.GetUpdateStatement();
            return base.ExecuteAsync(sql, entities, useTransaction);
        }
        #endregion
    }
}