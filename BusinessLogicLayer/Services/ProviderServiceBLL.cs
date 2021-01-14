using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public static class ProviderServiceBLL
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
