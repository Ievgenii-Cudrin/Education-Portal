namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Comparers;
    using Entities;

    public class MaterialSqlService : IMaterialService
    {
        private IRepository<Material> materialRepository;
        private IUserMaterialSqlService userMaterialService;
        private ICourseMaterialService courseMaterialService;
        private IAuthorizedUser authorizedUser;
        private IMaterialComparerService materialComparer;

        public MaterialSqlService(
            IRepository<Material> repository,
            IUserMaterialSqlService userMaterialService,
            IAuthorizedUser authorizedUser,
            ICourseMaterialService courseMaterialService,
            IMaterialComparerService materialComparer)
        {
            this.materialRepository = repository;
            this.userMaterialService = userMaterialService;
            this.authorizedUser = authorizedUser;
            this.courseMaterialService = courseMaterialService;
            this.materialComparer = materialComparer;
        }

        public Material CreateMaterial(Material material)
        {
            if (material != null && !this.materialRepository.Exist(x => x.Name == material.Name))
            {
                this.materialRepository.Add(material);
                this.materialRepository.Save();
                return material;
            }

            return null;
        }

        public bool Delete(int materialId)
        {
            if (this.materialRepository.Exist(x => x.Id == materialId))
            {
                this.materialRepository.Delete(materialId);
                this.materialRepository.Save();
                return true;
            }

            return false;
        }

        public IEnumerable<Material> GetAllExceptedMaterials(int courseId)
        {
            return this.materialRepository.Except(this.courseMaterialService.GetAllMaterialsFromCourse(courseId), this.materialComparer.MaterialComparer).ToList();
        }

        public IEnumerable<Material> GetAllNotPassedMaterialFromUser()
        {
            return this.materialRepository.Except(this.userMaterialService.GetAllMaterialInUser(this.authorizedUser.User.Id), this.materialComparer.MaterialComparer).ToList();
        }

        public Material GetMaterial(int materialId)
        {
            if (this.materialRepository.Exist(x => x.Id == materialId))
            {
                return this.materialRepository.Get(x => x.Id == materialId).FirstOrDefault();
            }

            return null;
        }

        public bool UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public bool ExistMaterial(int materialId)
        {
            return this.materialRepository.Exist(x => x.Id == materialId);
        }
    }
}
