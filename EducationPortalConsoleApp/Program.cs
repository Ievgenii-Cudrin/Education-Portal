namespace EducationPortalConsoleApp
{
    using System;
    using System.Threading.Tasks;
    using EducationPortal.PL;
    using EducationPortal.PL.Controller;
    using EducationPortal.PL.DependencyInjection;
    using EducationPortal.PL.Helpers;
    using EducationPortalConsoleApp.Controller;
    using EducationPortalConsoleApp.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<StartApplication>();

                    new Startup().ConfigureServices(services);
                })
                .AddLogging();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var app = services.GetRequiredService<StartApplication>();
                    await app.Run();

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }

            Console.ReadLine();
        }
    }
}
