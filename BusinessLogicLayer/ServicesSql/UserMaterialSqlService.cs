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

        public UserMaterialSqlService(
            IEnumerable<IRepository<UserMaterial>> userMaterialRepository)
        {
            this.userMaterialRepository = userMaterialRepository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<UserMaterial>));
        }

        public bool AddMaterialToUser(int userId, int materialId)
        {
            if (this.userMaterialRepository.Exist(x => x.UserId == userId && x.MaterialId == materialId))
            {
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
