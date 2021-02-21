namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Entities;

    public interface ICourseMaterialService
    {
        bool AddMaterialToCourse(int courseId, int materialId);

        List<Material> GetAllMaterialsFromCourse(int courseId);
    }
}
