using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Interfaces
{
    public interface IUserCourseMaterialSqlService
    {
        Task<IOperationResult> AddMaterialsToUserCourse(int userCourseId, int courseId);

        Task<bool> SetPassToMaterial(int userCourseId, int materialId);

        Task<IEnumerable<Material>> GetNotPassedMaterialsFromCourseInProgress(int userCourseId);

        Task<int> GetCountOfPassedMaterialsInCourse(int userCourseId);
    }
}
