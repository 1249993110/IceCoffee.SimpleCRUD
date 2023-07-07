using IceCoffee.SimpleCRUD.SqlGenerators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace IceCoffee.SimpleCRUD.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region Configure by action

        /// <summary>
        ///
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <param name="configure">The action to configure the <see cref="DbConnectionOptions"/>.</param>
        /// <param name="assembly">The assembly to scan.</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the repositories.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Action<DbConnectionOptions> configure,
            Assembly assembly,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            return services.AddRepositories(string.Empty, configure, assembly, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string dbAliase,
            Action<DbConnectionOptions> configure,
            Assembly assembly,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).Configure(configure);
            return services.InternalAddRepositories(assembly, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Action<DbConnectionOptions> configure,
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            return services.AddRepositories(string.Empty, configure, repositoryTypes, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string dbAliase,
            Action<DbConnectionOptions> configure,
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).Configure(configure);
            return services.InternalAddRepositories(repositoryTypes, serviceLifetime);
        }

        #endregion Configure by action

        #region Configure by section path

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string configurationSectionPath,
            Assembly assembly,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            return services.AddRepositories(string.Empty, configurationSectionPath, assembly, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string dbAliase,
            string configurationSectionPath,
            Assembly assembly,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).BindConfiguration(configurationSectionPath);
            return services.InternalAddRepositories(assembly, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string configurationSectionPath,
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            return services.AddRepositories(string.Empty, configurationSectionPath, repositoryTypes, serviceLifetime);
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            string dbAliase,
            string configurationSectionPath,
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddOptions<DbConnectionOptions>(dbAliase).BindConfiguration(configurationSectionPath);
            return services.InternalAddRepositories(repositoryTypes, serviceLifetime);
        }

        #endregion Configure by section path

        private static IServiceCollection InternalAddRepositories(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime)
        {
            var repositoryTypes = new List<(Type ServiceType, Type ImplType)>();
            foreach (var implType in assembly.GetExportedTypes())
            {
                if (implType.IsSubclassOf(typeof(RepositoryBase)) && implType.IsAbstract == false)
                {
                    var serviceType = implType.GetInterfaces().First(p => typeof(IRepository).IsAssignableFrom(p) && p != typeof(IRepository) && p.IsGenericType == false);
                    repositoryTypes.Add((serviceType, implType));
                }
            }

            return services.InternalAddRepositories(repositoryTypes, serviceLifetime);
        }

        private static IServiceCollection InternalAddRepositories(this IServiceCollection services, IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes, ServiceLifetime serviceLifetime)
        {
            services.TryAddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.TryAddSingleton<ISqlGeneratorFactory, SqlGeneratorFactory>();
            services.TryAddSingleton<IRepositoryFactory, RepositoryFactory>();
            services.TryAddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            foreach (var item in repositoryTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(item.ServiceType, item.ImplType, serviceLifetime);
                services.TryAdd(serviceDescriptor);
            }
            return services;
        }
    }
}