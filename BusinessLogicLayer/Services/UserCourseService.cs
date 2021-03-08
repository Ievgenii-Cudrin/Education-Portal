namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Microsoft.Extensions.Logging;
    using NLog;

    public class UserCourseService : IUserCourseSqlService
    {
        private IRepository<UserCourse> userCourseRepository;
        private IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private IAuthorizedUser authorizedUser;
        private ILogger<UserCourseService> logger;

        public UserCourseService(
            IRepository<UserCourse> userCourseRepository,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IAuthorizedUser authorizedUser,
            ILogger<UserCourseService> logger)
        {
            this.userCourseRepository = userCourseRepository;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.authorizedUser = authorizedUser;
            this.logger = logger;
        }

        public async Task AddCourseToUser(int userId, int courseId)
        {
            int lastEntityId = 1;

            var userCourseLast = await this.userCourseRepository.GetLastEntity(x => x.Id);

            if (userCourseLast != null)
            {
                lastEntityId = userCourseLast.Id;
            }

            var newUserCourse = new UserCourse()
            {
                Id = ++lastEntityId,
                CourseId = courseId,
                UserId = userId,
                IsPassed = false,
            };

            try
            {
                await this.userCourseRepository.Add(newUserCourse);
                await this.userCourseMaterialSqlService.AddMaterialsToUserCourse(newUserCourse.Id, courseId);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed add course {courseId} to user {userId} exception - {ex.Message}");
            }
        }

        public async Task<List<Course>> GetAllCourseInProgress(int userId)
        {
            return await this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId && s.IsPassed == false);
        }

        public async Task<bool> CourseWasStarted(int courseId)
        {
            return await this.userCourseRepository.Exist(x => x.UserId == this.authorizedUser.User.Id && x.CourseId == courseId);
        }

        public async Task<bool> ExistUserCourse(int userCourseId)
        {
            return await this.userCourseRepository.Exist(x => x.Id == userCourseId);
        }

        public async Task<List<Course>> GetAllPassedAndProgressCoursesForUser(int userId)
        {
            return await this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId);
        }

        public async Task<UserCourse> GetUserCourse(int userId, int courseId)
        {
            return await this.userCourseRepository.GetOne(x => x.UserId == userId && x.CourseId == courseId);
        }

        public async Task<bool> SetPassForUserCourse(int userId, int courseId)
        {
            var userCourse = await this.userCourseRepository.GetOne(x => x.UserId == userId && x.CourseId == courseId);

            if (userCourse == null)
            {
                return false;
            }

            userCourse.IsPassed = true;

            try
            {
                await this.userCourseRepository.Update(userCourse);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed update userCourse {courseId} to user {userId} exception - {ex.Message}");
            }

            return true;
        }

        public async Task<List<Course>> GetAllPassedCourse(int userId)
        {
            return await this.userCourseRepository.Get<Course>(x => x.Course, x => x.UserId == userId && x.IsPassed == true);
        }
    }
}
