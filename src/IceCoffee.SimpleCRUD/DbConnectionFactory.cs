namespace IceCoffee.SimpleCRUD
{
    public class DbConnectionFactory : DbConnectionFactoryBase
    {
        private readonly IDictionary<string, DbConnectionOptions> _optionsDict;

        public DbConnectionFactory(IDictionary<string, DbConnectionOptions> optionsDict)
        {
            _optionsDict = optionsDict;
        }

        public override DbConnectionOptions GetOptions(string connectionName)
        {
            return _optionsDict[connectionName];
        }
    }
}