namespace EducationPortalConsoleApp.Interfaces
{
    using System.Collections.Generic;
    using EducationPortal.PL.Models;
    using Entities;

    public interface IMaterialController
    {
        Material CreateVideo();

        Material CreateArticle();

        Material CreateBook();

        void DeleteMaterial(int id);

        Material GetMaterialFromAllMaterials();

        List<MaterialViewModel> GetAllMaterialVMAfterMappingFromMaterialDomain(List<Material> materialsListDomain);
    }
}
