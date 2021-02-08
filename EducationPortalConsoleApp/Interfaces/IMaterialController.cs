using DataAccessLayer.Entities;
using EducationPortal.PL.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Interfaces
{
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
