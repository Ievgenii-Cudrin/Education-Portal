using DataAccessLayer.Interfaces;
using EducationPortal.DAL.Repositories;
using EducationPortal.DAL.XML.Repositories;
using EducationPortal.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPortal.PL.DependencyInjection
{
    public static class RepositoriesInstaller
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IRepository<UserCourse>, UserCourseXmlRepository>();
            //services.AddTransient<IRepository<CourseMaterial>, CourseMaterialXmlRepository>();
            //services.AddTransient<IRepository<CourseSkill>, CourseSkillXmlRepository>();
            //services.AddTransient<IRepository<UserMaterial>, UserMaterialXmlRepository>();
            //services.AddTransient<IRepository<UserSkill>, UserSkillXmlRepository>();
            //services.AddTransient<IRepository<UserCourseMaterial>, UserCourseMaterialXmlRepository>();
            //services.AddTransient(typeof(IRepository<>), typeof(RepositoryXml<>));
            services.AddTransient(typeof(IRepository<>), typeof(RepositorySql<>));
        }
    }
}
