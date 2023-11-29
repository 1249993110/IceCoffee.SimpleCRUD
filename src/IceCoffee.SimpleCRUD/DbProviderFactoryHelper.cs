using System.Data.Common;
using System.Reflection;

namespace IceCoffee.SimpleCRUD
{
    public static class DbProviderFactoryHelper
    {
        /// <summary>
        /// Try to load an assembly into the application's app domain.
        /// Loads by name first then checks for filename
        /// </summary>
        /// <param name="assemblyName">Assembly name. (without filename extension)</param>
        /// <returns>null on failure</returns>
        private static Assembly LoadAssembly(string assemblyName)
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load(new AssemblyName(assemblyName));
                return assembly;
            }
            catch { }

            string path = Path.Combine(AppContext.BaseDirectory, assemblyName + ".dll");
            assembly = Assembly.LoadFrom(path);
            return assembly;
        }

        private static DbProviderFactory GetDbProviderFactory(string assemblyName, string @namespace)
        {
            try
            {
#pragma warning disable CS8600,CS8602,CS8603
                return (DbProviderFactory)LoadAssembly(assemblyName)
                        .GetType(@namespace)
                        .GetField("Instance", BindingFlags.Static | BindingFlags.Public)
                        .GetValue(null);
#pragma warning restore
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to retrieve DbProviderFactory form assembly name: {assemblyName}, namespace: {@namespace}.", ex);
            }
        }

        public static DbProviderFactory GetDbProviderFactory(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.SQLServer:
                    return GetDbProviderFactory("Microsoft.Data.SqlClient", "Microsoft.Data.SqlClient.SqlClientFactory");

                case DbType.SQLite:
                    return GetDbProviderFactory("Microsoft.Data.Sqlite", "Microsoft.Data.Sqlite.SqliteFactory");

                case DbType.PostgreSQL:
                    return GetDbProviderFactory("Npgsql", "Npgsql.NpgsqlFactory");

                case DbType.MySQL:
                    return GetDbProviderFactory("MySql.Data", "MySql.Data.MySqlClient.MySqlClientFactory");

                case DbType.Undefined:
                default:
                    throw new NotSupportedException("Undefined database type: " + dbType.ToString());
            }
        }
    }
}