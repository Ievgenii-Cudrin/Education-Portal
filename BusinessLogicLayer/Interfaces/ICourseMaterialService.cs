using System.Collections.Generic;
using System.Threading.Tasks;
using Entities; 

namespace EducationPortal.BLL.Interfaces
{
    public interface ICourseMaterialService
    {
        Task<IOperationResult> AddMaterialToCourse(int courseId, int materialId);

        Task<IEnumerable<Material>> GetAllMaterialsFromCourse(int courseId);

        Task<IOperationResult> DeleteMaterialFromCourse(int courseId, int materialId);

        Task<int> GetCountOfMaterialInCourse(int courseId);
    }
}
