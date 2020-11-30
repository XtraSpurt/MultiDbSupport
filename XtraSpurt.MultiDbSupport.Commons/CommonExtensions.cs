using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using XtraSpurt.MultiDbSupport.Commons.Configurations;
using XtraSpurt.MultiDbSupport.Domains;
using XtraSpurt.MultiDbSupport.MariaDb;
using XtraSpurt.MultiDbSupport.MySql;
using XtraSpurt.MultiDbSupport.PgSql;
using XtraSpurt.MultiDbSupport.SqlServer;

namespace XtraSpurt.MultiDbSupport.Commons
{
    public static class CommonExtensions
    {
        public static void AddXtraSpurtDependency(this IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IWebHostEnvironment>(); 
            var Configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            var databaseSetting = new DatabaseSetting();
            // Bind Database setting to databasesetting variable
            Configuration.Bind("DatabaseSetting", databaseSetting);

            // DI : Configure DatabaseSetting Options => The options pattern uses classes to provide strongly typed access to groups of related settings
            services.Configure<DatabaseSetting>(options => Configuration?.GetSection("DatabaseSetting").Bind(options));

            if (string.IsNullOrWhiteSpace(databaseSetting.Type) || string.IsNullOrWhiteSpace(databaseSetting.ConnectionString))
            {
                throw new Exception("Verify Database Settings in appsetting.json or appsetting.{Environment}.json");
            }

            switch (databaseSetting.Type)
            {
                case "sqlserver":
                    services.RegisterSqlServerDbContexts(databaseSetting.ConnectionString);
                    break;
                case "pgsql":
                    services.RegisterPgSqlDbContexts(databaseSetting.ConnectionString);
                    break;
                case "mysql":
                    services.RegisterMySQLDbContexts(databaseSetting.ConnectionString);
                    break;

                case "mariadb":
                    services.RegisterMariaDbContexts(databaseSetting.ConnectionString);
                    break;

                default:
                    throw new ArgumentException($"XtraSpurt Does Not Support : {databaseSetting.Type}");
            }

            services.AddXtraSpurtIdentity();
        }

        /// <summary>
        ///  Configure Asp Net Core Idenitity
        /// </summary>
        /// <param name="services"></param>
        private static void AddXtraSpurtIdentity(this IServiceCollection services)
        {
            services.AddIdentity<XtraSpurtUser, XtraSpurtRole>()
                .AddUserManager<UserManager<XtraSpurtUser>>()
                .AddRoleManager<RoleManager<XtraSpurtRole>>()
                .AddEntityFrameworkStores<XtraSpurtDbContext>()
                .AddDefaultTokenProviders();
        }

        //private static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Load Configuration
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        private static IConfigurationRoot LoadConfiguration(IWebHostEnvironment environment = null)
        {
            var configurationbulider = new ConfigurationBuilder();
            configurationbulider
                .SetBasePath(environment?.WebRootPath ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            if (environment != null)
            {
                configurationbulider.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);
            }

            return configurationbulider.Build();
        }
    }
}