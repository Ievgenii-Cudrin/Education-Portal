using DataAccessLayer.DataContext;
using EducationPortal.PL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using XmlDataBase.Interfaces;
using XmlDataBase.Serialization;
using EducationPortal.PL.Mapping;
using EducationPortal.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using EducationPortal.PL.DependencyInjection;
using EducationPortal.PL;

namespace EducationPortalConsoleApp.DependencyInjection
{
    public class Startup
    {
        static IConfiguration configuration;

        public Startup()
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Config.json", optional: false, reloadOnChange: true)
            .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IXmlSet<>), typeof(XmlSet<>));
            services.AddTransient(typeof(IXmlSerializeContext<>), typeof(XmlSerializationContextGeneric<>));
            services.AddTransient<IMapperService, Mapping>();
            services.AddTransient<IApplication, App>();

            // Repositories
            services.AddRepositories();
            services.AddDbContext<ApplicationContext>(
                options =>
                    options.UseSqlServer(configuration["ConnectionStrings:UserDBConnection"]), ServiceLifetime.Transient);

            // Services
            services.AddBusinessLogicServices();

            // ConsoleControllers
            services.AddConsoleAppControllers();
        }
    }
}
