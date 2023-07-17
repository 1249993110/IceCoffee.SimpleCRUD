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
    }
}
