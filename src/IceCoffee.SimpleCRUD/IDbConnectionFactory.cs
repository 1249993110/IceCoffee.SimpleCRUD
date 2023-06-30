using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public interface IDbConnectionFactory
    {
        DbConnectionOptions GetOptions(string connectionName);

        DbConnectionOptions GetOptions(Enum connectionName);

        IDbConnection CreateConnection(string connectionName);

        IDbConnection CreateConnection(Enum connectionName);
    }
}