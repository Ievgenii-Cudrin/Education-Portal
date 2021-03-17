using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using EducationPortal.BLL.DTO;
using EducationPortal.Domain.Entities;

namespace EducationPortal.BLL.Interfaces
{
    public interface IUserCourseSqlService
    {
        Task AddCourseToUser(int userId, int courseId);

        Task<IEnumerable<Course>> GetAllCourseInProgress(int userId);

        Task<bool> ExistUserCourse(int userCourseId);

        Task<IEnumerable<Course>> GetAllPassedAndProgressCoursesForUser(int userId);

        Task<UserCourse> GetUserCourse(int userId, int courseId);

        Task<bool> SetPassForUserCourse(int userId, int courseId);

        Task<IEnumerable<Course>> GetAllPassedCourse(int userId);

        Task<bool> CourseWasStarted(int courseId);

        Task<List<CourseDTO>> AllNotPassedCourseWithCompletedPercent(int userId);

        Task<bool> ExistUserCourseByUserIdCourseId(int userId, int courseId);
    }
}
