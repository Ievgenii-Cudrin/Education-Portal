namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;

    public interface IUserCourseSqlService
    {
        Task AddCourseToUser(int userId, int courseId);

        Task<List<Course>> GetAllCourseInProgress(int userId);

        Task<bool> ExistUserCourse(int userCourseId);

        Task<List<Course>> GetAllPassedAndProgressCoursesForUser(int userId);

        Task<UserCourse> GetUserCourse(int userId, int courseId);

        Task<bool> SetPassForUserCourse(int userId, int courseId);

        Task<List<Course>> GetAllPassedCourse(int userId);

        Task<bool> CourseWasStarted(int courseId);
    }
}
