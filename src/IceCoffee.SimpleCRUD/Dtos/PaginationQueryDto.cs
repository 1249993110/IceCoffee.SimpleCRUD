namespace IceCoffee.SimpleCRUD.Dtos
{
    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class PaginationQueryDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string? Keyword { get; set; }
    }

    /// <summary>
    /// 分页查询参数
    /// </summary>
    /// <typeparam name="TOrder"></typeparam>
    public class PaginationQueryDto<TOrder> : PaginationQueryDto where TOrder : Enum
    {
        /// <summary>
        /// 排序
        /// </summary>
        public TOrder? Order { get; set; }

        /// <summary>
        /// 是否降序
        /// </summary>
        public bool Desc { get; set; }
    }
}
