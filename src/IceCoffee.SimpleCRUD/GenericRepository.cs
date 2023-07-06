using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public class GenericRepository<TEntity> : RepositoryBase<TEntity>
    {
        public GenericRepository() : base(DbConnectionFactory.Default, SqlGeneratorFactory.Default)
        {
        }

        public GenericRepository(string connectionName) : base(DbConnectionFactory.Default, SqlGeneratorFactory.Default, connectionName)
        {
        }
    }
}
