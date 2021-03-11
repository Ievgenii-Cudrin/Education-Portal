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

namespace EducationPortal.BLL.ServicesSql
{

    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ICourseSkillService courseSkillService;
        private readonly ILogger<CourseService> logger;
        private readonly IAuthorizedUser authorizedUser;
        private IOperationResult operationResult;

        private const string success = "Success";
        private const string courseExistInDB = "Operation with course is successfull finished";
        private const string courseNotExist = "Course not exist";

        public CourseService(
            IRepository<Course> courseRepo,
            ICourseMaterialService courseMaterialServ,
            ICourseSkillService courseSkillServ,
            ILogger<CourseService> logger,
            IAuthorizedUser authorizedUser,
            IOperationResult operationResult)
        {
            this.courseRepository = courseRepo;
            this.courseMaterialService = courseMaterialServ;
            this.courseSkillService = courseSkillServ;
            this.logger = logger;
            this.authorizedUser = authorizedUser;
            this.operationResult = operationResult;
        }

        public async Task<IOperationResult> AddMaterialToCourse(int courseId, Material material)
        {
            return await this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
        }

        public async Task<IOperationResult> AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            return await this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
        }

        public async Task<IOperationResult> CreateCourse(Course course)
        {
            bool courseExist = await this.courseRepository.Exist(x => x.Name == course.Name);

            if (course != null && !courseExist)
            {
                await this.courseRepository.Add(course);
                await this.courseRepository.Save();
                this.logger.LogDebug($"Create course ({course.Id}) by user ({this.authorizedUser.User.Id})");
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
            }
            else
            {
                this.logger.LogInformation($"Course ({course.Id}) by user ({this.authorizedUser.User.Id} not created. Course with this name exist!");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = courseExistInDB;
            }

            return this.operationResult;
        }

        public async Task<IOperationResult> Delete(int id)
        {
            try
            {
                await this.courseRepository.Delete(id);
                await this.courseRepository.Save();
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
                this.logger.LogInformation($"Course ({id}) by user ({this.authorizedUser.User.Id} successfull deleted");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed to delete course {id} by user ({this.authorizedUser.User.Id} due {ex.Message}");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = courseNotExist;
            }

            return this.operationResult;
        }

        public async Task<IEnumerable<Material>> GetMaterialsFromCourse(int id)
        {

            return await this.courseMaterialService.GetAllMaterialsFromCourse(id);
        }

        public async Task<IEnumerable<Skill>> GetSkillsFromCourse(int id)
        {
            return await this.courseSkillService.GetAllSkillsFromCourse(id);
        }

        public async Task<IOperationResult> UpdateCourse(Course course)
        {
            try
            {
                await this.courseRepository.Update(course);
                await this.courseRepository.Save();
                this.logger.LogDebug($"Course ({course.Id}) successfully updated by user ({this.authorizedUser.User.Id})");
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = courseExistInDB;
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Course ({course.Id}) not updated by user ({this.authorizedUser.User.Id}) by due ({ex.Message})");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = courseNotExist;
            }

            return this.operationResult;
        }

        public async Task<bool> ExistCourse(int courseId)
        {
            return await this.courseRepository.Exist(x => x.Id == courseId);
        }

        public async Task<IEnumerable<Course>> GetCoursesPerPage(int skip, int take)
        {
            return await this.courseRepository.GetPage(skip, take);
        }

        public async Task<int> GetCount()
        {
            return await this.courseRepository.Count();
        }

        public async Task<Course> GetCourse(int id)
        {
            return await this.courseRepository.GetOne(x => x.Id == id);
        }

        public async Task<int> GetLastId()
        {
            var course = await this.courseRepository.GetLastEntity(x => x.Id);
            return course.Id;
        }
    }
}
