namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICourseMaterialService
    {
        bool AddMaterialToCourse(int courseId, int materialId);
    }
}
