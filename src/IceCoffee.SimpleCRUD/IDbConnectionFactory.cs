using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public interface IDbConnectionFactory
    {
        DbConnectionOptions GetOptions(string dbAliase);

        IDbConnection CreateConnection(string dbAliase);
    }
}