using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.MySql
{

    public static class MultiDbSupportMySQL
    {

        /// <summary>
        /// Add Required Dependenies for Database and Identity 
        /// </summary>
        /// <param name="services"> IServiceCollection </param>
        /// <param name="connectionString"> Database Connection String  </param>
        public static void RegisterMySQLDbContexts(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(MultiDbSupportMySQL).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<XtraSpurtDbContext>(options
                    =>
                {
                    options.UseMySql(
                        connectionString,
                        new MySqlServerVersion(new Version(8, 0, 22)),
                        sql =>
                        {
                            sql.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                            sql.MigrationsAssembly(migrationsAssembly);
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        }
                    );
                }

            );




        }
    }
}
