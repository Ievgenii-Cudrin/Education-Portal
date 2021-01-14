using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortalConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.DependencyInjection;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Controller;
using DataAccessLayer.Entities;
using DataAccessLayer.DataContext;

namespace EducationPortalConsoleApp.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IMaterialController, MaterialController>()
                .AddSingleton<IUserController, UserController>()
                .AddSingleton<ICourseController, CourseController>()
                .AddSingleton<ISkillController, SkillController>()
                .BuildServiceProvider();

            var bllProvider = StartupBll.ConfigureService();
            var dalProvider = DataAccessLayer.DependencyInjection.Startup.ConfigureService();
            var qq = new XmlSerializationContextGeneric<User>(dalProvider.GetRequiredService<IXmlSet<User>>());
            return provider;
        }
    }
}
