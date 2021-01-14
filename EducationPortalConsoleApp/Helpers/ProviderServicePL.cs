using EducationPortalConsoleApp.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class ProviderServicePL
    {
        static IServiceProvider provider;
        public static IServiceProvider Provider
        {
            get
            {
                if (provider == null)
                {
                    provider = Startup.ConfigureService();
                }
                return provider;
            }
        }
    }
}
