using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.ServicesSql
{
    public class UserMaterialService : IUserMaterialSqlService
    {
        private readonly IRepository<UserMaterial> userMaterialRepository;
        private readonly ILogger<UserMaterialService> logger;

        public UserMaterialService(
            IRepository<UserMaterial> userMaterialRepository,
            ILogger<UserMaterialService> logger)
        {
            this.userMaterialRepository = userMaterialRepository;
            this.logger = logger;
        }

        public async Task<bool> AddMaterialToUser(int userId, int materialId)
        {
            if (await this.userMaterialRepository.Exist(x => x.UserId == userId && x.MaterialId == materialId))
            {
                return false;
            }

            var userMaterial = new UserMaterial()
            {
                UserId = userId,
                MaterialId = materialId,
            };

            await this.userMaterialRepository.Add(userMaterial);
            await this.userMaterialRepository.Save();

            return true;
        }

        public async Task<IEnumerable<Material>> GetAllMaterialInUser(int userId)
        {
            return await this.userMaterialRepository.Get<Material>(x => x.Material, x => x.UserId == userId);
        }

        public async Task<bool> ExistMaterialInUser(int userId, int materialId)
        {
            return await this.userMaterialRepository.Exist(x => x.UserId == userId && x.MaterialId == materialId);
        }
    }
}
