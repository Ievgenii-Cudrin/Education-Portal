using System.Collections.Generic;
using System.Threading.Tasks;
using EducationPortal.BLL.Interfaces;
using Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMaterialService
    {
        Task<IOperationResult> CreateMaterial(Material material);

        Task<IEnumerable<Material>> GetAllMaterialsForOnePage(int take, int skip);

        Task<Material> GetMaterial(int id);

        Task<bool> ExistMaterial(int materialId);

        IEnumerable<Material> GetAllNotPassedMaterialFromUser();

        Task<int> GetCount();

        Task UpdateMaterial(Material material);
    }
}
