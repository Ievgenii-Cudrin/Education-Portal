using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using EducationPortal.PL.Models;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface IMaterialController
    {
        Task<Material> CreateVideo();

        Task<Material> CreateArticle();

        Task<Material> CreateBook();

        void DeleteMaterial(int id);

        Task<Material> GetMaterialFromAllMaterials(int courseId);

        List<MaterialViewModel> GetAllMaterialVMAfterMappingFromMaterialDomain(List<Material> materialsListDomain);
    }
}
