using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.Mappers;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Services;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.CoreMVC.LayersDependencyInjections;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Repositories;
using EducationPortal.DAL.XML.Repositories;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.InstallDal();
            services.InstallBll();
            
            services.AddTransient<IAutoMapperService, Mapping>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.Cookie.Name = "EducationPortalCookie";
                });

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

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
