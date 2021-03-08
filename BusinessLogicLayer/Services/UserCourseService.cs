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
    using NLog;

    public class UserCourseService : IUserCourseSqlService
    {
        private IRepository<UserCourse> userCourseRepository;
        private IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private IAuthorizedUser authorizedUser;

        public UserCourseService(
            IRepository<UserCourse> userCourseRepository,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IAuthorizedUser authorizedUser)
        {
            this.userCourseRepository = userCourseRepository;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.authorizedUser = authorizedUser;
        }

        public async Task AddCourseToUser(int userId, int courseId)
        {
            int id = 0;

            if (await this.userCourseRepository.Exist(x => x.Id == 0))
            {
                var userCourseLast = await this.userCourseRepository.GetLastEntity(x => x.Id);
                id = userCourseLast.Id;
            }

            var newUserCourse = new UserCourse()
            {
                Id = ++id,
                CourseId = courseId,
                UserId = userId,
                IsPassed = false,
            };

            await this.userCourseRepository.Add(newUserCourse);
            await this.userCourseMaterialSqlService.AddMaterialsToUserCourse(newUserCourse.Id, courseId);
        }

        public async Task<IList<Course>> GetAllCourseInProgress(int userId)
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

        public async Task<IList<Course>> GetAllPassedAndProgressCoursesForUser(int userId)
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
            await this.userCourseRepository.Update(userCourse);

            return true;
        }

        public async Task<IList<Course>> GetAllPassedCourse(int userId)
        {
            return await this.userCourseRepository.Get<Course>(x => x.Course, x => x.UserId == userId && x.IsPassed == true);
        }
    }
}
