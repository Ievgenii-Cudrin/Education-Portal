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

namespace EducationPortalConsoleApp.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IProgramBranch, ProgramBranch>()
                .AddSingleton<IMaterialController, MaterialController>()
                .AddSingleton<IUserController, UserController>()
                .AddSingleton<ICourseController, CourseController>()
                .AddSingleton<ISkillController, SkillController>()
                .BuildServiceProvider();

            var bllProvider = StartupBll.ConfigureService();

            return provider;
        }
    }
}
