using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{
    public class MaterialService : IMaterialService
    {
        private IRepository<Material> materialRepository;
        private IUserMaterialSqlService userMaterialService;
        private ICourseMaterialService courseMaterialService;
        private IAuthorizedUser authorizedUser;

        public MaterialService(
            IRepository<Material> repository,
            IUserMaterialSqlService userMaterialService,
            IAuthorizedUser authorizedUser,
            ICourseMaterialService courseMaterialService)
        {
            this.materialRepository = repository;
            this.userMaterialService = userMaterialService;
            this.authorizedUser = authorizedUser;
            this.courseMaterialService = courseMaterialService;
        }

        public bool CreateMaterial(Material material)
        {
            try
            {
                if (material != null && !this.materialRepository.Exist(x => x.Name == material.Name))
                {
                    this.materialRepository.Add(material);
                    return true;
                }

                return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool Delete(int materialId)
        {
            try
            {
                this.materialRepository.Delete(materialId);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public IEnumerable<Material> GetAllMaterialsForOnePage(int take, int skip)
        {
            var a = this.materialRepository.GetPage(take, skip);
            return this.materialRepository.GetPage(take, skip);
        }

        public IEnumerable<Material> GetAllNotPassedMaterialFromUser()
        {
            throw new NotImplementedException();
        }

        public Material GetMaterial(int materialId)
        {
            try
            {
                return this.materialRepository.Get(x => x.Id == materialId).FirstOrDefault();
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool ExistMaterial(int materialId)
        {
            return this.materialRepository.Exist(x => x.Id == materialId);
        }

        public int GetCount()
        {
            return this.materialRepository.Count();
        }
    }
}
