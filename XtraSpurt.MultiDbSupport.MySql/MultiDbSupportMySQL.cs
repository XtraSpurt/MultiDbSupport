using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Reflection;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.MySql
{
    public static class MultiDbSupportMySQL
    {
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