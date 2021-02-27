namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IMaterialService
    {
        Material CreateMaterial(Material material);

        IEnumerable<Material> GetAllExceptedMaterials(int courseId);

        Material GetMaterial(int id);

        bool Delete(int id);

        bool ExistMaterial(int materialId);

        IEnumerable<Material> GetAllNotPassedMaterialFromUser();
    }
}
