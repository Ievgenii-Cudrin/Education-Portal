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
    using NLog;

    public class UserMaterialService : IUserMaterialSqlService
    {
        private readonly IRepository<UserMaterial> userMaterialRepository;

        public UserMaterialService(
            IRepository<UserMaterial> userMaterialRepository)
        {
            this.userMaterialRepository = userMaterialRepository;
        }

        public bool AddMaterialToUser(int userId, int materialId)
        {
            if (this.userMaterialRepository.Exist(x => x.UserId == userId && x.MaterialId == materialId))
            {
                return false;
            }

            var userMaterial = new UserMaterial()
            {
                UserId = userId,
                MaterialId = materialId,
            };

            this.userMaterialRepository.Add(userMaterial);
            return true;
        }

        public List<Material> GetAllMaterialInUser(int userId)
        {
            return this.userMaterialRepository.Get<Material>(x => x.Material, x => x.UserId == userId).ToList();
        }
    }
}
