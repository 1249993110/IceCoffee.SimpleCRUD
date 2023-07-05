namespace IceCoffee.SimpleCRUD
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        #region Sync

        #region Insert
        /// <summary>
        /// Insert a record.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Insert(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk insert records.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region Update
        /// <summary>
        /// Update records based on the clause and param.
        /// </summary>
        /// <param name="setClause"></param>
        /// <param name="whereClause"></param>
        /// <param name="param"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Update(string setClause, string whereClause, object param, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Update a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Update(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk update records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region Delete
        /// <summary>
        /// Delete records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="param"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Delete(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Delete a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Delete(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk delete records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Delete a record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int DeleteById<TId>(TId id, string? tableName = null);

        /// <summary>
        /// Bulk delete records based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="ids"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null);
        #endregion

        #region InsertOrReplace
        /// <summary>
        /// Insert or replace a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int InsertOrReplace(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk insert or replace records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region InsertOrIgnore
        /// <summary>
        /// Try insert a record based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int InsertOrIgnore(TEntity entity, string? tableName = null);

        /// <summary>
        /// Try bulk insert records based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #endregion

        #region Async

        #region Insert
        /// <summary>
        /// Insert a record.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk insert records.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region Update
        /// <summary>
        /// Update records based on the clause and param.
        /// </summary>
        /// <param name="setClause"></param>
        /// <param name="whereClause"></param>
        /// <param name="param"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(string setClause, string whereClause, object param, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Update a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk update records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region Delete
        /// <summary>
        /// Delete records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="param"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string whereClause, object? param = null, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Delete a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk delete records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);

        /// <summary>
        /// Delete a record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> DeleteByIdAsync<TId>(TId id, string? tableName = null);

        /// <summary>
        /// Bulk delete records based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="ids"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false, string? tableName = null);
        #endregion

        #region InsertOrReplace
        /// <summary>
        /// Insert or replace a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertOrReplaceAsync(TEntity entity, string? tableName = null);

        /// <summary>
        /// Bulk insert or replace records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #region InsertOrIgnore
        /// <summary>
        /// Try insert a record based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertOrIgnoreAsync(TEntity entity, string? tableName = null);

        /// <summary>
        /// Try bulk insert records based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false, string? tableName = null);
        #endregion

        #endregion
    }
}
