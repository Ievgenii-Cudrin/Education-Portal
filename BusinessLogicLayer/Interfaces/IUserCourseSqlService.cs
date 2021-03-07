namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;

    public interface IUserCourseSqlService
    {
        void AddCourseToUser(int userId, int courseId);

        List<Course> GetAllCourseInProgress(int userId);

        bool ExistUserCourse(int userCourseId);

        List<Course> GetAllPassedAndProgressCoursesForUser(int userId);

        UserCourse GetUserCourse(int userId, int courseId);

        bool SetPassForUserCourse(int userId, int courseId);

        List<Course> GetAllPassedCourse(int userId);

        bool CourseWasStarted(int courseId);
    }
}
