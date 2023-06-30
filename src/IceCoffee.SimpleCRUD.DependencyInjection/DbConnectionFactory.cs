using Microsoft.Extensions.Options;

namespace IceCoffee.SimpleCRUD.DependencyInjection
{
    public class DbConnectionFactory : DbConnectionFactoryBase
    {
        private readonly IOptionsMonitor<DbConnectionOptions> _optionsMonitor;

        public DbConnectionFactory(IOptionsMonitor<DbConnectionOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public override DbConnectionOptions GetOptions(string connectionName)
        {
            return _optionsMonitor.Get(connectionName);
        }
    }
}