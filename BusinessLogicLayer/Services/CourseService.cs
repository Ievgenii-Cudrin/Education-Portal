using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{

    public class CourseService : ICourseService
    {
        private IRepository<Course> courseRepository;
        private ICourseMaterialService courseMaterialService;
        private ICourseSkillService courseSkillService;
        private ILogger<CourseService> logger;
        private IAuthorizedUser authorizedUser;

        public CourseService(
            IRepository<Course> courseRepo,
            ICourseMaterialService courseMaterialServ,
            ICourseSkillService courseSkillServ,
            ILogger<CourseService> logger,
            IAuthorizedUser authorizedUser)
        {
            this.courseRepository = courseRepo;
            this.courseMaterialService = courseMaterialServ;
            this.courseSkillService = courseSkillServ;
            this.logger = logger;
            this.authorizedUser = authorizedUser;
        }

        public async Task<bool> AddMaterialToCourse(int courseId, Material material)
        {
            try
            {
                return await this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
            }
            catch (SqlException ex)
            {
                this.logger.LogWarning($"Failed to connect to db  due {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            return await this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
        }

        public async Task<bool> CreateCourse(Course course)
        {
            try
            {
                bool courseExist = await this.courseRepository.Exist(x => x.Name == course.Name);

                if (course != null && !courseExist)
                {
                    await this.courseRepository.Add(course);
                    this.logger.LogDebug($"Create course ({course.Id}) by user ({this.authorizedUser.User.Id})");
                    return true;
                }

                this.logger.LogInformation($"Course ({course.Id}) not created. Course exist!");
                return false;
            }
            catch (SqlException ex)
            {
                this.logger.LogWarning($"Failed to connect to db  due {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await this.courseRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed to connect to db  due {ex.Message}");
                return false;
            }
        }

        public async Task<List<Material>> GetMaterialsFromCourse(int id)
        {
            try
            {
                return await this.courseMaterialService.GetAllMaterialsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<List<Skill>> GetSkillsFromCourse(int id)
        {
            try
            {
                return await this.courseSkillService.GetAllSkillsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            try
            {
                await this.courseRepository.Update(course);
                this.logger.LogDebug($"Course ({course.Id}) successfully updated by user ({this.authorizedUser.User.Id})");
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Course ({course.Id}) not updated ({ex.Message})");
                return false;
            }
        }

        public async Task<bool> ExistCourse(int courseId)
        {
            return await this.courseRepository.Exist(x => x.Id == courseId);
        }

        public async Task<List<Course>> GetCoursesPerPage(int skip, int take)
        {
            return await this.courseRepository.GetPage(skip, take);
        }

        public async Task<int> GetCount()
        {
            return await this.courseRepository.Count();
        }
    }
}
