namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IMaterialService
    {
        Task<bool> CreateMaterial(Material material);

        Task<IEnumerable<Material>> GetAllMaterialsForOnePage(int take, int skip);

        Task<Material> GetMaterial(int id);

        Task<bool> Delete(int id);

        Task<bool> ExistMaterial(int materialId);

        IEnumerable<Material> GetAllNotPassedMaterialFromUser();

        Task<int> GetCount();
    }
}
