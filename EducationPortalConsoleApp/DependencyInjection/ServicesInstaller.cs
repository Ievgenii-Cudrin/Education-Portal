﻿using BusinessLogicLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Services;
using EducationPortal.BLL.ServicesSql;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPortal.PL.DependencyInjection
{
    public static class ServicesInstaller
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IMaterialService, MaterialService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ILogInService, LogInService>();
            services.AddTransient<IUserCourseSqlService, UserCourseService>();
            services.AddTransient<ICourseMaterialService, CourseMaterialService>();
            services.AddTransient<ICourseSkillService, CourseSkillService>();
            services.AddTransient<IUserCourseMaterialSqlService, UserCourseMaterialService>();
            services.AddTransient<IUserMaterialSqlService, UserMaterialService>();
            services.AddTransient<IUserSkillSqlService, UserSkillService>();
            services.AddTransient<IAuthorizedUser, AuthorizerUser>();
            services.AddTransient<IWorkWithAuthorizedUser, AuthorizerUser>();
        }
    }
}