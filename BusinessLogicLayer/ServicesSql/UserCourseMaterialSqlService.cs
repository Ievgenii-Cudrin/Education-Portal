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

    public class UserCourseMaterialSqlService : IUserCourseMaterialSqlService
    {
        private readonly IRepository<UserCourseMaterial> userCourseMaterialRepository;
        private ICourseMaterialService courseMaterialService;

        public UserCourseMaterialSqlService(
            IEnumerable<IRepository<UserCourseMaterial>> userCourseMaterialRepository,
            ICourseMaterialService courseMaterialService)
        {
            this.userCourseMaterialRepository = userCourseMaterialRepository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<UserCourseMaterial>));
            this.courseMaterialService = courseMaterialService;
        }

        public bool AddMaterialsToUserCourse(int userCourseId, int courseId)
        {
            List<Material> materialsFromCourse = this.courseMaterialService.GetAllMaterialsFromCourse(courseId);

            foreach (var material in materialsFromCourse)
            {
                UserCourseMaterial userCourseMaterial = new UserCourseMaterial()
                {
                    UserCourseId = userCourseId,
                    MaterialId = material.Id,
                    IsPassed = false,
                };

                this.userCourseMaterialRepository.Add(userCourseMaterial);
                this.userCourseMaterialRepository.Save();
            }

            return true;
        }

        public bool SetPassToMaterial(int userCourseId, int materialId)
        {
            UserCourseMaterial userCourseMaterialToUpdate = this.userCourseMaterialRepository.Get(
                x => x.UserCourseId == userCourseId && x.MaterialId == materialId).FirstOrDefault();

            if (userCourseMaterialToUpdate != null)
            {
                userCourseMaterialToUpdate.IsPassed = true;
                this.userCourseMaterialRepository.Update(userCourseMaterialToUpdate);
                this.userCourseMaterialRepository.Save();
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
