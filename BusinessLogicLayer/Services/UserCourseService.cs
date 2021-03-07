namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public UserCourseService(IRepository<UserCourse> userCourseRepository,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IAuthorizedUser authorizedUser)
        {
            this.userCourseRepository = userCourseRepository;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.authorizedUser = authorizedUser;
        }

        public void AddCourseToUser(int userId, int courseId)
        {
            int id = 0;

            if (this.userCourseRepository.Exist(x => x.Id == 0))
            {
                id = this.userCourseRepository.GetLastEntity(x => x.Id).Id;
            }

            var newUserCourse = new UserCourse()
            {
                Id = ++id,
                CourseId = courseId,
                UserId = userId,
                IsPassed = false,
            };

            this.userCourseRepository.Add(newUserCourse);
            this.userCourseMaterialSqlService.AddMaterialsToUserCourse(newUserCourse.Id, courseId);
        }

        public List<Course> GetAllCourseInProgress(int userId)
        {
            return this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId && s.IsPassed == false).ToList();
        }

        public bool CourseWasStarted(int courseId)
        {
            return this.userCourseRepository.Exist(x => x.UserId == this.authorizedUser.User.Id && x.CourseId == courseId);
        }

        public bool ExistUserCourse(int userCourseId)
        {
            return this.userCourseRepository.Exist(x => x.Id == userCourseId);
        }

        public List<Course> GetAllPassedAndProgressCoursesForUser(int userId)
        {
            return this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId).ToList();
        }

        public UserCourse GetUserCourse(int userId, int courseId)
        {
            return this.userCourseRepository.Get(x => x.UserId == userId && x.CourseId == courseId).FirstOrDefault();
        }

        public bool SetPassForUserCourse(int userId, int courseId)
        {
            var userCourse = this.userCourseRepository.GetOne(x => x.UserId == userId && x.CourseId == courseId);

            if (userCourse == null)
            {
                return false;
            }

            userCourse.IsPassed = true;
            this.userCourseRepository.Update(userCourse);
            return true;
        }

        public List<Course> GetAllPassedCourse(int userId)
        {
            return this.userCourseRepository.Get<Course>(x => x.Course, x => x.UserId == userId && x.IsPassed == true).ToList();
        }
    }
}
