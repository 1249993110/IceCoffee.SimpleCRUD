namespace IceCoffee.SimpleCRUD
{
    public class RepositoryFactory : RepositoryFactoryBase
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IRepository? GetRepository(Type type)
        {
            return _serviceProvider.GetService(type) as IRepository;
        }
    }
}