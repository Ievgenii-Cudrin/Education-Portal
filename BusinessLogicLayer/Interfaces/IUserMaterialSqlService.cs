namespace EducationPortal.BLL.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserMaterialSqlService
    {
        bool AddMaterialToUser(int userId, int materialId);

        List<Material> GetAllMaterialInUser(int userId);
    }
}
