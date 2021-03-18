using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace EducationPortal.BLL.ServicesSql
{
    public class UserCourseService : IUserCourseSqlService
    {
        private readonly IRepository<UserCourse> userCourseRepository;
        private readonly IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ILogger<UserCourseService> logger;

        public UserCourseService(
            IRepository<UserCourse> userCourseRepository,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IAuthorizedUser authorizedUser,
            ILogger<UserCourseService> logger,
            ICourseMaterialService courseMaterialService)
        {
            this.userCourseRepository = userCourseRepository;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.authorizedUser = authorizedUser;
            this.logger = logger;
            this.courseMaterialService = courseMaterialService;
        }

        public async Task AddCourseToUser(int userId, int courseId)
        {
            int lastEntityId = await this.userCourseRepository.Count();

            if (lastEntityId != 0)
            {
                var userCourseLast = await this.userCourseRepository.GetLastEntity(x => x.Id);
                lastEntityId = userCourseLast.Id;
            }

            var newUserCourse = new UserCourse()
            {
                Id = ++lastEntityId,
                CourseId = courseId,
                UserId = userId,
                IsPassed = false,
            };

            await this.userCourseRepository.Add(newUserCourse);
            await this.userCourseMaterialSqlService.AddMaterialsToUserCourse(newUserCourse.Id, courseId);
            await this.userCourseRepository.Save();
        }

        public async Task<IEnumerable<Course>> GetAllCourseInProgress(int userId)
        {
            return await this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId && s.IsPassed == false);
        }

        public async Task<List<CourseDTO>> AllNotPassedCourseWithCompletedPercent(int userId)
        {
            var coursesNotCompleted = await this.userCourseRepository.Get<Course>(s => s.Course, s => s.UserId == userId && s.IsPassed == false);
            List<CourseDTO> courseForView = new List<CourseDTO>();

            foreach (var course in coursesNotCompleted)
            {
                var userCourse = await this.userCourseRepository.GetOne(x => x.UserId == userId && x.CourseId == course.Id);
                double countOfNotPassedMaterial = await this.userCourseMaterialSqlService.GetCountOfPassedMaterialsInCourse(userCourse.Id);
                double countOfAllCourseInMaterial = await this.courseMaterialService.GetCountOfMaterialInCourse(course.Id);
                var percent = countOfNotPassedMaterial / countOfAllCourseInMaterial;

                CourseDTO courseDTO = new CourseDTO()
                {
                    Name = course.Name,
                    Description = course.Description,
                    Completed = percent.ToString("P", CultureInfo.InvariantCulture),
                };

                courseForView.Add(courseDTO);
            }

            return courseForView;
        }

        public async Task<bool> CourseWasStarted(int courseId)
        {
            return await this.userCourseRepository.Exist(x => x.UserId == this.authorizedUser.User.Id && x.CourseId == courseId);
        }

        public async Task<bool> ExistUserCourse(int userCourseId)
        {
            return await this.userCourseRepository.Exist(x => x.Id == userCourseId);
        }

        public async Task<bool> ExistUserCourseByUserIdCourseId(int userId, int courseId)
        {
            return await this.userCourseRepository.Exist(x => x.UserId == userId && x.CourseId == courseId);
        }

        public async Task<IEnumerable<Course>> GetAllPassedAndProgressCoursesForUser(int userId)
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
            await this.userCourseRepository.Save();

            return true;
        }

        public async Task<IEnumerable<Course>> GetAllPassedCourse(int userId)
        {
            return await this.userCourseRepository.Get<Course>(x => x.Course, x => x.UserId == userId && x.IsPassed == true);
        }
    }
}
