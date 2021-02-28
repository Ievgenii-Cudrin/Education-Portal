namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;

    public class UserMaterialSqlService : IUserMaterialSqlService
    {
        private readonly IRepository<UserMaterial> userMaterialRepository;
        private static IBLLLogger logger;

        public UserMaterialSqlService(
            IRepository<UserMaterial> userMaterialRepository,
            IBLLLogger log)
        {
            this.userMaterialRepository = userMaterialRepository;
            logger = log;
        }

        public bool AddMaterialToUser(int userId, int materialId)
        {
            if (this.userMaterialRepository.Exist(x => x.UserId == userId && x.MaterialId == materialId))
            {
                logger.Logger.Debug("UserMaterial not exist - " + DateTime.Now);
                return false;
            }

            UserMaterial userMaterial = new UserMaterial()
            {
                UserId = userId,
                MaterialId = materialId,
            };

            this.userMaterialRepository.Add(userMaterial);
            this.userMaterialRepository.Save();
            return true;
        }

        public List<Material> GetAllMaterialInUser(int userId)
        {
            return this.userMaterialRepository.Get<Material>(x => x.Material, x => x.UserId == userId).ToList();
        }
    }
}
