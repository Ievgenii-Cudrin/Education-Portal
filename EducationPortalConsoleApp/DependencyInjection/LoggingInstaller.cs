using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace EducationPortal.PL.DependencyInjection
{
    public static class LoggingInstaller
    {
        public static IHostBuilder AddLogging(this IHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();

            return builder;
        }
    }
}
