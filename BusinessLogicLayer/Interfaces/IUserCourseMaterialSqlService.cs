namespace EducationPortal.BLL.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserCourseMaterialSqlService
    {
        bool AddMaterialsToUserCourse(int userCourseId, int courseId);

        bool SetPassToMaterial(int userCourseId, int materialId);

        List<Material> GetNotPassedMaterialsFromCourseInProgress(int userCourseId);
    }
}
