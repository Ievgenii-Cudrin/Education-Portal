using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{
    public class MaterialService : IMaterialService
    {
        private IRepository<Material> materialRepository;
        private ILogger<MaterialService> logger;

        public MaterialService(
            IRepository<Material> repository,
            ILogger<MaterialService> logger)
        {
            this.materialRepository = repository;
            this.logger = logger;
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

                this.logger.LogDebug($"Material ({material.Id}) not created. Material exist");
                return false;
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed create material - {ex.Message}");
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
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed delete material - {ex.Message}");
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
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed get material - {ex.Message}");
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
