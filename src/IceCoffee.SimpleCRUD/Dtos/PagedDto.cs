namespace IceCoffee.SimpleCRUD.Dtos
{
    public class PagedDto<TEntity>
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
