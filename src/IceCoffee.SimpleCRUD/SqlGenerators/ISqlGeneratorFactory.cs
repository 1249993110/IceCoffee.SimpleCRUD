using IceCoffee.SimpleCRUD.SqlGenerators;

namespace IceCoffee.SimpleCRUD
{
    public interface ISqlGeneratorFactory
    {
        ISqlGenerator GetSqlGenerator(DbType dbType, Type type);
    }
}