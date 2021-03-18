using DataAccessLayer.Interfaces;
using EducationPortal.DAL.Repositories;
using EducationPortal.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.DAL.LayersDependencyInjections
{
    public static class DalDependencyInjection
    {
        public static void InstallDal(this IServiceCollection services)
        {
            //MVC

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
