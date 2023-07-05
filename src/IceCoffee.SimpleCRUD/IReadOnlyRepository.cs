namespace IceCoffee.SimpleCRUD
{
    public interface IReadOnlyRepository<TEntity> : IRepository
    {
        #region Sync
        /// <summary>
        /// Get single record based on the id.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        TEntity? GetById<TKey>(TKey id, string? tableName = null);

        /// <summary>
        /// Get records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get the first or default record based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        TEntity? GetFirstOrDefault(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get a paged list of records based on the clause and param.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number per page, return all records when the value is less than 0.</param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        (IEnumerable<TEntity> Items, int Total) GetPagedList(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get count of records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int GetRecordCount(string? whereClause = null, object? param = null, string? tableName = null);
        #endregion

        #region Async
        /// <summary>
        /// Get single record based on the id.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<TEntity?> GetByIdAsync<TKey>(TKey id, string? tableName = null);

        /// <summary>
        /// Get records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get the first or default record based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<TEntity?> GetFirstOrDefaultAsync(string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get a paged list of records based on the clause and param.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number per page, return all records when the value is less than 0.</param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<(IEnumerable<TEntity> Items, int Total)> GetPagedListAsync(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, object? param = null, string? tableName = null);

        /// <summary>
        /// Get count of records based on the clause and param.
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> GetRecordCountAsync(string? whereClause = null, object? param = null, string? tableName = null);
        #endregion
    }
}
