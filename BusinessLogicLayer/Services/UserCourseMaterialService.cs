namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;
    using NLog;

    public class UserCourseMaterialService : IUserCourseMaterialSqlService
    {
        private IRepository<UserCourseMaterial> userCourseMaterialRepository;
        private ICourseMaterialService courseMaterialService;

        public UserCourseMaterialService(
            IRepository<UserCourseMaterial> userCourseMaterialRepository,
            ICourseMaterialService courseMaterialService)
        {
            this.userCourseMaterialRepository = userCourseMaterialRepository;
            this.courseMaterialService = courseMaterialService;
        }

        public bool AddMaterialsToUserCourse(int userCourseId, int courseId)
        {
            var materialsFromCourse = this.courseMaterialService.GetAllMaterialsFromCourse(courseId);

            if (materialsFromCourse == null)
            {
                return false;
            }

            foreach (var material in materialsFromCourse)
            {
                var userCourseMaterial = new UserCourseMaterial()
                {
                    UserCourseId = userCourseId,
                    MaterialId = material.Id,
                    IsPassed = false,
                };

                this.userCourseMaterialRepository.Add(userCourseMaterial);
            }

            return true;
        }

        public bool SetPassToMaterial(int userCourseId, int materialId)
        {
            var userCourseMaterialToUpdate = this.userCourseMaterialRepository.GetOne(
                x => x.UserCourseId == userCourseId && x.MaterialId == materialId);

            if (userCourseMaterialToUpdate != null)
            {
                userCourseMaterialToUpdate.IsPassed = true;
                this.userCourseMaterialRepository.Update(userCourseMaterialToUpdate);
                return true;
            }

            return false;
        }

        public List<Material> GetNotPassedMaterialsFromCourseInProgress(int userCourseId)
        {
            if (!this.userCourseMaterialRepository.Exist(x => x.UserCourseId == userCourseId))
            {
                return null;
            }

            return this.userCourseMaterialRepository.Get<Material>(x => x.Material, x => x.UserCourseId == userCourseId && x.IsPassed == false).ToList();
        }
    }
}
