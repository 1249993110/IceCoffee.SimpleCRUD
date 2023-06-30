using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.SimpleCRUD.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <param name="assembly">The assembly to scan.</param>
        /// <param name="configure">The action to configure the <see cref="DbConnectionOptions"/>.</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the repositories.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Assembly assembly,
            Action<DbConnectionOptions> configure,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            return services.AddRepositories(assembly, configure, serviceLifetime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <param name="repositoryTypes"></param>
        /// <param name="configure">The action to configure the <see cref="DbConnectionOptions"/>.</param>
        /// <param name="connectionName"></param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the repositories.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            IEnumerable<(Type ServiceType, Type ImplType)> repositoryTypes,
            Action<DbConnectionOptions> configure,
            string connectionName,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            //services.AddOptions<DbConnectionOptions>().Bind();
            //services.AddOptions<DbConnectionOptions>().BindConfiguration();

            services.AddOptions<DbConnectionOptions>(connectionName).Configure(configure);

            services.TryAddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            foreach (var item in repositoryTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(item.ServiceType, item.ImplType, serviceLifetime);
                services.TryAdd(serviceDescriptor);
            }
            return services;
        }
    }
}
