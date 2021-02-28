﻿// <auto-generated />
namespace EducationPortalConsoleApp.DependencyInjection
{
    using System;
    using BusinessLogicLayer.Interfaces;
    using BusinessLogicLayer.Services;
    using DataAccessLayer.DataContext;
    using DataAccessLayer.Interfaces;
    using DataAccessLayer.Repositories;
    using EducationPortal.PL.Controller;
    using EducationPortal.PL.Interfaces;
    using EducationPortalConsoleApp.Controller;
    using EducationPortalConsoleApp.Interfaces;
    using EducationPortalConsoleApp.Services;
    using Microsoft.Extensions.DependencyInjection;
    using XmlDataBase.Interfaces;
    using XmlDataBase.Serialization;
    using EducationPortal.PL.Mapping;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.BLL.ServicesSql;
    using EducationPortal.DAL.DataContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using EducationPortal.BLL.Services;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Interfaces;
    using EducationPortal.DAL.Loggers;

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
        public IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton(typeof(IXmlSet<>), typeof(XmlSet<>))
                .AddSingleton(typeof(IXmlSerializeContext<>), typeof(XmlSerializationContextGeneric<>))
                //.AddTransient(typeof(IRepository<>), typeof(RepositoryXml<>))
                .AddTransient(typeof(IRepository<>), typeof(RepositorySql<>))
                .AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration["ConnectionStrings:UserDBConnection"]))
                 //.AddTransient<IUserService, UserService>()
                .AddTransient<IUserService, UserSqlService>()
                //.AddTransient<ICourseService, CourseService>()
                .AddTransient<ICourseService, CourseSqlService>()
                //.AddTransient<IMaterialService, MaterialService>()
                .AddTransient<IMaterialService, MaterialSqlService>()
                //.AddTransient<ISkillService, SkillService>()
                .AddTransient<ISkillService, SkillSqlService>()
                .AddTransient<ILogInService, LogInService>()
                .AddScoped<IUserCourseSqlService, UserCourseSqlService>()
                .AddTransient<ICourseMaterialService, CourseMaterialSqlService>()
                .AddTransient<ICourseSkillService, CourseSkillSqlService>()
                .AddTransient<IAuthorizedUser, AuthorizerUser>()
                .AddTransient<IWorkWithAuthorizedUser, AuthorizerUser>()
                .AddTransient<IMaterialController, MaterialController>()
                .AddTransient<IUserController, UserController>()
                .AddTransient<ICourseController, CourseController>()
                .AddTransient<ISkillController, SkillController>()
                .AddTransient<IPassCourseController, PassCourseController>()
                .AddTransient<IMapperService, Mapping>()
                .AddTransient<IUserCourseMaterialSqlService, UserCourseMaterialSqlService>()
                .AddTransient<IUserMaterialSqlService, UserMaterialSqlService>()
                .AddTransient<IUserSkillSqlService, UserSkillSqlService>()
                .AddTransient<IMaterialComparerService, MaterialComparerService>()
                .AddTransient<ICourseComparerService, CourseComparerService>()
                .AddTransient<IDalSqlLogger, DalSqlNLogLogger>()
                .BuildServiceProvider();

            return provider;
        }
    }
}
