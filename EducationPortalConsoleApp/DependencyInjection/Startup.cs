using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortalConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            return provider;
        }
    }
}
