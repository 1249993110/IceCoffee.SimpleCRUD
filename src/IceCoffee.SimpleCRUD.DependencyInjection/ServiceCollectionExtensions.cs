using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace IceCoffee.SimpleCRUD.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services, Action<DbConnectionOptions> configure)
        {
            services.AddOptions<DbConnectionOptions>(string.Empty).Configure(configure);
            return services;
        }

        public static IServiceCollection AddDbConnection(this IServiceCollection services, string dbAliase, Action<DbConnectionOptions> configure)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).Configure(configure);
            return services;
        }

        public static IServiceCollection AddDbConnection(this IServiceCollection services, string configurationSectionPath)
        {
            services.AddOptions<DbConnectionOptions>(string.Empty).BindConfiguration(configurationSectionPath);
            return services;
        }

        public static IServiceCollection AddDbConnection(this IServiceCollection services, string dbAliase, string configurationSectionPath)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).BindConfiguration(configurationSectionPath);
            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.TryAddSingleton<IRepositoryFactory, RepositoryFactory>();
            services.TryAddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            foreach (var item in repositoryTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(item.ServiceType, item.ImplType, serviceLifetime);
                services.TryAdd(serviceDescriptor);
            }
            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            Assembly assembly, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var repositoryTypes = new List<(Type ServiceType, Type ImplType)>();
            foreach (var implType in assembly.GetExportedTypes())
            {
                if (implType.IsSubclassOf(typeof(RepositoryBase)) && implType.IsAbstract == false && implType.IsGenericType == false)
                {
                    var serviceType = implType.GetInterfaces().First(p => typeof(IRepository).IsAssignableFrom(p) && p != typeof(IRepository) && p.IsGenericType == false);
                    repositoryTypes.Add((serviceType, implType));
                }
            }

            return services.AddRepositories(repositoryTypes, serviceLifetime);
        }
    }
}