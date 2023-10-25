using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public class GenericRepository<TEntity> : RepositoryBase<TEntity>
    {
        public GenericRepository() : this(string.Empty)
        {
        }

        public GenericRepository(string dbAliase) : base(DbConnectionFactory.Default, dbAliase)
        {
        }

        public virtual int ExecuteAny(string sql, object? param = null, bool useTransaction = false)
        {
            return base.Execute(sql, param, useTransaction);
        }

        public virtual Task<int> ExecuteAnyAsync(string sql, object? param = null, bool useTransaction = false)
        {
            return base.ExecuteAsync(sql, param, useTransaction);
        }

        public virtual IEnumerable<TEntity> ExecuteQueryAny(string sql, object? param = null)
        {
            return base.ExecuteQuery<TEntity>(sql, param);
        }

        public virtual Task<IEnumerable<TEntity>> ExecuteQueryAnyAsync(string sql, object? param = null)
        {
            return base.ExecuteQueryAsync<TEntity>(sql, param);
        }
    }
}
