using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.PgSql
{
    public static class MultiDbSupportPgSql
    {
        /// <summary>
        /// Register and Configure PgSql
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void RegisterPgSqlDbContexts(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(MultiDbSupportPgSql).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<XtraSpurtDbContext>(options
                    =>
                {
                    options.UseNpgsql(connectionString,
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