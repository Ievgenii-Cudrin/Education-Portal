namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;
    using Microsoft.Extensions.Logging;
    using NLog;

    public class UserMaterialService : IUserMaterialSqlService
    {
        private readonly IRepository<UserMaterial> userMaterialRepository;
        private ILogger<UserMaterialService> logger;

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

            try
            {
                await this.userMaterialRepository.Add(userMaterial);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed add material {materialId} to user {userId} - {ex.Message}");
            }
            return true;
        }

        public async Task<IList<Material>> GetAllMaterialInUser(int userId)
        {
            return await this.userMaterialRepository.Get<Material>(x => x.Material, x => x.UserId == userId);
        }
    }
}
