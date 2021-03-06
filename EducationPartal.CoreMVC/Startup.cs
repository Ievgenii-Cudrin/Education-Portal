using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Loggers;
using EducationPortal.BLL.Services;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Interfaces;
using EducationPortal.DAL.Loggers;
using EducationPortal.DAL.Repositories;
using EducationPortal.DAL.XML.Repositories;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlDataBase.Interfaces;
using XmlDataBase.Serialization;

namespace EducationPartal.CoreMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IXmlSet<>), typeof(XmlSet<>));
            services.AddSingleton(typeof(IXmlSerializeContext<>), typeof(XmlSerializationContextGeneric<>));
            // Repositories
            services.AddTransient<IRepository<UserCourse>, UserCourseXmlRepository>();
            services.AddSingleton<IRepository<CourseMaterial>, CourseMaterialXmlRepository>();
            services.AddTransient<IRepository<CourseSkill>, CourseSkillXmlRepository>();
            services.AddTransient<IRepository<UserMaterial>, UserMaterialXmlRepository>();
            services.AddTransient<IRepository<UserSkill>, UserSkillXmlRepository>();
            services.AddTransient<IRepository<UserCourseMaterial>, UserCourseMaterialXmlRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(RepositoryXml<>));
            //services.AddTransient(typeof(IRepository<>), typeof(RepositorySql<>));
            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IMaterialService, MaterialService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ILogInService, LogInService>();
            services.AddScoped<IUserCourseSqlService, UserCourseService>();
            services.AddTransient<ICourseMaterialService, CourseMaterialService>();
            services.AddTransient<ICourseSkillService, CourseSkillService>();
            services.AddTransient<IUserCourseMaterialSqlService, UserCourseMaterialService>();
            services.AddTransient<IUserMaterialSqlService, UserMaterialService>();
            services.AddTransient<IUserSkillSqlService, UserSkillService>();
            services.AddTransient<IMaterialComparerService, MaterialComparerService>();
            services.AddTransient<ICourseComparerService, CourseComparerService>();
            services.AddTransient<IAuthorizedUser, AuthorizerUser>();
            services.AddTransient<IWorkWithAuthorizedUser, AuthorizerUser>();
            //Loggers
            services.AddTransient<IDalSqlLogger, DalSqlNLogLogger>();
            services.AddTransient<IBLLLogger, BLLNlogLogger>();

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
