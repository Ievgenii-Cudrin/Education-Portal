using BusinessLogicLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Results;
using EducationPortal.BLL.Services;
using EducationPortal.BLL.ServicesSql;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPortal.CoreMVC.LayersDependencyInjections
{
    public static class BllDependencies
    {
        public static void InstallBll(this IServiceCollection services)
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
            services.AddTransient<IOperationResult, OperationResult>();
        }
    }
}
