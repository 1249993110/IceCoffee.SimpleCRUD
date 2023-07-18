namespace IceCoffee.SimpleCRUD.Dtos
{
    public class PaginationDto<TEntity>
    {
        /// <summary>
        /// Total
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }
    }
}
