namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Entities;

    public interface ICourseMaterialService
    {
        Task<bool> AddMaterialToCourse(int courseId, int materialId);

        Task<IList<Material>> GetAllMaterialsFromCourse(int courseId);
    }
}
