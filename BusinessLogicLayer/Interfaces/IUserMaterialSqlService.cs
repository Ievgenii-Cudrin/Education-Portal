namespace EducationPortal.BLL.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserMaterialSqlService
    {
        Task<bool> AddMaterialToUser(int userId, int materialId);

        Task<List<Material>> GetAllMaterialInUser(int userId);

        Task<bool> ExistMaterialInUser(int userId, int materialId);
    }
}
