namespace EducationPortal.BLL.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserCourseMaterialSqlService
    {
        Task<bool> AddMaterialsToUserCourse(int userCourseId, int courseId);

        Task<bool> SetPassToMaterial(int userCourseId, int materialId);

        Task<List<Material>> GetNotPassedMaterialsFromCourseInProgress(int userCourseId);
    }
}
