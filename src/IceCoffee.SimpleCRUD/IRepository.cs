namespace IceCoffee.SimpleCRUD
{
    public interface IRepository
    {
        /// <summary>
        /// Delete all records.
        /// </summary>
        /// <returns></returns>
        int DeleteAll(bool useTransaction = false);

        /// <summary>
        /// Delete all records.
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteAllAsync(bool useTransaction = false);
    }
}
