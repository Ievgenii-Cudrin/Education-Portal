using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortalConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.DependencyInjection;

namespace EducationPortalConsoleApp.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var bllProvider = StartupBll.ConfigureService();

            return provider;
        }
    }
}
