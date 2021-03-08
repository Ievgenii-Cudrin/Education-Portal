using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> CreateMaterial(Material material)
        {
            try
            {
                bool materialExist = await this.materialRepository.Exist(x => x.Name == material.Name);

                if (material != null && !materialExist)
                {
                    await this.materialRepository.Add(material);
                    return true;
                }

                return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int materialId)
        {
            try
            {
                await this.materialRepository.Delete(materialId);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsForOnePage(int take, int skip)
        {
            return await this.materialRepository.GetPage(take, skip);
        }

        public IEnumerable<Material> GetAllNotPassedMaterialFromUser()
        {
            throw new NotImplementedException();
        }

        public async Task<Material> GetMaterial(int materialId)
        {
            try
            {
                return await this.materialRepository.GetOne(x => x.Id == materialId);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<bool> ExistMaterial(int materialId)
        {
            return await this.materialRepository.Exist(x => x.Id == materialId);
        }

        public async Task<int> GetCount()
        {
            return await this.materialRepository.Count();
        }
    }
}
