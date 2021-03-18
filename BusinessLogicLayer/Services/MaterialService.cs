using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.ServicesSql
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> materialRepository;
        private readonly ILogger<MaterialService> logger;
        private readonly IAuthorizedUser authorizedUser;
        private IOperationResult operationResult;

        private const string success = "Success";
        private const string materialWithThisNameExist = "Material with this name exist";

        public MaterialService(
            IRepository<Material> repository,
            ILogger<MaterialService> logger,
            IOperationResult operationResult,
            IAuthorizedUser authorizedUser)
        {
            this.materialRepository = repository;
            this.logger = logger;
            this.operationResult = operationResult;
            this.authorizedUser = authorizedUser;
        }

        public async Task<IOperationResult> CreateMaterial(Material material)
        {
            bool materialExist = await this.materialRepository.Exist(x => x.Name == material.Name);

            if (material != null && !materialExist)
            {
                await this.materialRepository.Add(material);
                await this.materialRepository.Save();
                this.logger.LogDebug($"Material {material.Id} successfully created by user ({this.authorizedUser.User.Id})");
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
            }
            else
            {
                this.logger.LogDebug($"Material ({material.Id}) not created by user ({this.authorizedUser.User.Id}). Material exist");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = materialWithThisNameExist;
            }

            return this.operationResult;
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
            return await this.materialRepository.GetOne(x => x.Id == materialId);
        }

        public async Task<bool> ExistMaterial(int materialId)
        {
            return await this.materialRepository.Exist(x => x.Id == materialId);
        }

        public async Task<int> GetCount()
        {
            return await this.materialRepository.Count();
        }

        public async Task UpdateMaterial(Material material)
        {
            await this.materialRepository.Update(material);
            await this.materialRepository.Save();
        }
    }
}
