using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Interfaces
{
    public interface IUserMaterialSqlService
    {
        Task<bool> AddMaterialToUser(int userId, int materialId);

        Task<IEnumerable<Material>> GetAllMaterialInUser(int userId);

        Task<bool> ExistMaterialInUser(int userId, int materialId);
    }
}
