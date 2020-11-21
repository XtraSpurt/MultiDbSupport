using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XtraSpurt.MultiDbSupport.Commons.Configurations;
using XtraSpurt.MultiDbSupport.Domains;

namespace XtraSpurt.MultiDbSupport.Commons
{
    public static class CommonExtensions
    {
        public static void AddXtraSpurtDependency(this IServiceCollection services, IWebHostEnvironment environment = null)
        {
            Configuration = LoadConfiguration(environment) ?? LoadConfiguration();


            var databaseSetting = new DatabaseSetting();
            // Bind Database setting to databasesetting variable 
            Configuration.Bind("DatabaseOptions", databaseSetting);

            // DI : Configure DatabaseSetting Options => The options pattern uses classes to provide strongly typed access to groups of related settings
            services.Configure<DatabaseSetting>(options => Configuration.GetSection("DatabaseOptions").Bind(options));

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

        private static IConfigurationRoot Configuration { get; set; }

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