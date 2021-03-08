namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;
    using Microsoft.Extensions.Logging;
    using NLog;

    public class UserCourseMaterialService : IUserCourseMaterialSqlService
    {
        private IRepository<UserCourseMaterial> userCourseMaterialRepository;
        private ICourseMaterialService courseMaterialService;
        private ILogger<UserCourseMaterialService> logger;

        public UserCourseMaterialService(
            IRepository<UserCourseMaterial> userCourseMaterialRepository,
            ICourseMaterialService courseMaterialService,
            ILogger<UserCourseMaterialService> logger)
        {
            this.userCourseMaterialRepository = userCourseMaterialRepository;
            this.courseMaterialService = courseMaterialService;
            this.logger = logger;
        }

        public async Task<bool> AddMaterialsToUserCourse(int userCourseId, int courseId)
        {
            var materialsFromCourse = await this.courseMaterialService.GetAllMaterialsFromCourse(courseId);

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

                try
                {
                    await this.userCourseMaterialRepository.Add(userCourseMaterial);
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"Failed add materials to course {courseId} - {ex.Message}");
                }
            }

            return true;
        }

        public async Task<bool> SetPassToMaterial(int userCourseId, int materialId)
        {
            var userCourseMaterialToUpdate = await this.userCourseMaterialRepository.GetOne(
                x => x.UserCourseId == userCourseId && x.MaterialId == materialId);

            if (userCourseMaterialToUpdate != null)
            {
                userCourseMaterialToUpdate.IsPassed = true;
                await this.userCourseMaterialRepository.Update(userCourseMaterialToUpdate);
                return true;
            }

            return false;
        }

        public async Task<IList<Material>> GetNotPassedMaterialsFromCourseInProgress(int userCourseId)
        {
            bool userCourseMaterialExist = await this.userCourseMaterialRepository.Exist(x => x.UserCourseId == userCourseId);

            if (!userCourseMaterialExist)
            {
                return null;
            }

            return await this.userCourseMaterialRepository.Get<Material>(x => x.Material, x => x.UserCourseId == userCourseId && x.IsPassed == false);
        }
    }
}
