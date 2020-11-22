using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.MariaDb
{
    public static class MultiDbSupportMariaDb
    {

        /// <summary>
        /// Add Required Dependenies for Database and Identity 
        /// </summary>
        /// <param name="services"> IServiceCollection </param>
        /// <param name="connectionString"> Database Connection String  </param>
        public static void RegisterMariaDbContexts(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(MultiDbSupportMariaDb).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<XtraSpurtDbContext>(options
                    =>
                {
                    options.UseMySql(
                        connectionString,
                        new MariaDbServerVersion(new Version(10, 4, 17)),
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
