using EducationPortal.PL.Controller;
using EducationPortal.PL.Interfaces;
using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPortal.PL.DependencyInjection
{
    public static class ConsoleAppControllersInstaller
    {
        public static void AddConsoleAppControllers(this IServiceCollection services)
        {
            services.AddTransient<IMaterialController, MaterialController>();
            services.AddTransient<IUserController, UserController>();
            services.AddTransient<ICourseController, CourseController>();
            services.AddTransient<ISkillController, SkillController>();
            services.AddTransient<IPassCourseController, PassCourseController>();
            services.AddTransient<IApplication, App>();
            //.AddTransient(typeof(IRepository<>), typeof(RepositorySql<>))
        }
    }
}
