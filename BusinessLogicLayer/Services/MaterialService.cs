﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;

namespace EducationPortal.BLL.ServicesSql
{
    public class MaterialService : IMaterialService
    {
        private IRepository<Material> materialRepository;
        private IUserMaterialSqlService userMaterialService;
        private ICourseMaterialService courseMaterialService;
        private IAuthorizedUser authorizedUser;
        private IMaterialComparerService materialComparer;
        private static IBLLLogger logger;

        public MaterialService(
            IRepository<Material> repository,
            IUserMaterialSqlService userMaterialService,
            IAuthorizedUser authorizedUser,
            ICourseMaterialService courseMaterialService,
            IMaterialComparerService materialComparer,
            IBLLLogger log)
        {
            this.materialRepository = repository;
            this.userMaterialService = userMaterialService;
            this.authorizedUser = authorizedUser;
            this.courseMaterialService = courseMaterialService;
            this.materialComparer = materialComparer;
            logger = log;
        }

        public Material CreateMaterial(Material material)
        {
            if (material != null && !this.materialRepository.Exist(x => x.Name == material.Name))
            {
                this.materialRepository.Add(material);
                this.materialRepository.Save();
                return material;
            }

            logger.Logger.Debug("Material dont create - " + DateTime.Now);
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

            logger.Logger.Debug("Material dont delete - " + DateTime.Now);
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

            logger.Logger.Debug("Material dont exist - " + DateTime.Now);
            return null;
        }

        public bool ExistMaterial(int materialId)
        {
            return this.materialRepository.Exist(x => x.Id == materialId);
        }
    }
}
