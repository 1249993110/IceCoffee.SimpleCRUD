namespace IceCoffee.SimpleCRUD
{
    public class DbConnectionFactory : DbConnectionFactoryBase
    {
        private static readonly Lazy<DbConnectionFactory> _default = new(true);
        public static DbConnectionFactory Default => _default.Value;

        private readonly IDictionary<string, DbConnectionOptions> _optionsDict;

        public DbConnectionFactory()
        {
            _optionsDict = new Dictionary<string, DbConnectionOptions>();
        }
        public DbConnectionFactory(IDictionary<string, DbConnectionOptions> optionsDict)
        {
            _optionsDict = optionsDict;
        }

        public override DbConnectionOptions GetOptions(string connectionName)
        {
            return _optionsDict[connectionName];
        }

        public virtual DbConnectionFactory ConfigureOptions(DbConnectionOptions options)
        {
            return ConfigureOptions(string.Empty, options);
        }

        public virtual DbConnectionFactory ConfigureOptions(string connectionName, DbConnectionOptions options)
        {
            _optionsDict[connectionName] = options;
            return this;
        }
    }
}