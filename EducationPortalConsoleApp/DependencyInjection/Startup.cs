using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using XmlDataBase.Interfaces;
using XmlDataBase.Serialization;

namespace EducationPortalConsoleApp.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton(typeof(IXmlSet<>), typeof(XmlSet<>))
                .AddSingleton(typeof(IXmlSerializeContext<>), typeof(XmlSerializationContextGeneric<>))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICourseService, CourseService>()
                .AddTransient<IMaterialService, MaterialService>()
                .AddTransient<ISkillService, SkillService>()
                .AddTransient<IMaterialController, MaterialController>()
                .AddTransient<IUserController, UserController>()
                .AddTransient<ICourseController, CourseController>()
                .AddTransient<ISkillController, SkillController>()
                .BuildServiceProvider();

            //var bllProvider = StartupBll.ConfigureService();
            return provider;
        }
    }
}
