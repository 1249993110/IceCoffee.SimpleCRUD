using System.Security.Cryptography;

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
        /// <returns></returns>
        int Insert(TEntity entity);

        /// <summary>
        /// Insert a record.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">Auto increment id</param>
        /// <returns></returns>
        int Insert<TId>(TEntity entity, out TId id);

        /// <summary>
        /// Bulk insert records.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region InsertOrReplace
        /// <summary>
        /// Insert or replace a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertOrReplace(TEntity entity);

        /// <summary>
        /// Bulk insert or replace records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region InsertOrIgnore
        /// <summary>
        /// Try insert a record based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertOrIgnore(TEntity entity);

        /// <summary>
        /// Try bulk insert records based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region Update

        /// <summary>
        /// Update a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);

        /// <summary>
        /// Bulk update records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region Delete

        /// <summary>
        /// Delete a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(TEntity entity);

        /// <summary>
        /// Bulk delete records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> entities, bool useTransaction = false);

        /// <summary>
        /// Delete a record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById<TId>(TId id);

        /// <summary>
        /// Bulk delete records based on the ids.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="ids"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false);
        #endregion

        #endregion

        #region Async

        #region Insert
        /// <summary>
        /// Insert a record.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Bulk insert records.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region InsertOrReplace
        /// <summary>
        /// Insert or replace a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertOrReplaceAsync(TEntity entity);

        /// <summary>
        /// Bulk insert or replace records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region InsertOrIgnore
        /// <summary>
        /// Try insert a record based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertOrIgnoreAsync(TEntity entity);

        /// <summary>
        /// Try bulk insert records based on the primary key, ignore if the record already exists.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region Update
        /// <summary>
        /// Update a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Bulk update records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false);
        #endregion

        #region Delete
        /// <summary>
        /// Delete a record based on the primary key.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Bulk delete records based on the primary key.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false);

        /// <summary>
        /// Delete a record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteByIdAsync<TId>(TId id);

        /// <summary>
        /// Bulk delete records based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="ids"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false);
        #endregion

        #endregion
    }
}
