using IceCoffee.SimpleCRUD.Dtos;

namespace IceCoffee.SimpleCRUD
{
    public interface IReadOnlyRepository<TEntity> : IRepository
    {
        #region Sync
        /// <summary>
        /// Get single record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity? GetById<TId>(TId id);

        /// <summary>
        /// Get all records.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get a paged list of records based on the offset and limit.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number per page.</param>
        /// <returns></returns>
        PagedDto<TEntity> GetPagedList(int pageNumber, int pageSize);

        /// <summary>
        /// Get count of records.
        /// </summary>
        /// <returns></returns>
        int GetRecordCount();
        #endregion

        #region Async
        /// <summary>
        /// Get single record based on the id.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> GetByIdAsync<TId>(TId id);

        /// <summary>
        /// Get all records.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get a paged list of records based on the offset and limit.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number per page.</param>
        /// <returns></returns>
        Task<PagedDto<TEntity>> GetPagedListAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Get count of records.
        /// </summary>
        /// <returns></returns>
        Task<int> GetRecordCountAsync();
        #endregion
    }
}
