namespace EducationPortalConsoleApp.Interfaces
{
    using System.Collections.Generic;
    using EducationPortal.PL.Models;
    using Entities;

    public interface IMaterialController
    {
        public Material CreateVideo();

        public Material CreateArticle();

        public Material CreateBook();

        public void DeleteMaterial(int id);

        public Material GetMaterialFromAllMaterials();

        public List<MaterialViewModel> GetAllMaterialVMAfterMappingFromMaterialDomain(List<Material> materialsListDomain);
    }
}
