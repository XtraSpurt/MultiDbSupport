using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.SqlServer
{
    public static class MultiDbSupportSqlServer
    {
        /// <summary>
        /// Register and Configure SqlServer 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void RegisterSqlServerDbContexts(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(MultiDbSupportSqlServer).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<XtraSpurtDbContext>(options
                    =>
                {
                    options.UseSqlServer(connectionString,
                        sql =>
                        {
                            // Apply Migration 
                            sql.MigrationsAssembly(migrationsAssembly);

                            //Split queries (see below) can now be configured as the default for
                            //any query executed by the DbContext. 
                            //This configuration is only available for relational providers
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        }
                    );

                }

            );

          



        }
    }
}
