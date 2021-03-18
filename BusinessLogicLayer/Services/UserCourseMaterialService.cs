using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.ServicesSql
{
    public class UserCourseMaterialService : IUserCourseMaterialSqlService
    {
        private readonly IRepository<UserCourseMaterial> userCourseMaterialRepository;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ILogger<UserCourseMaterialService> logger;
        private IOperationResult operationResult;

        private const string courseHasntMaterials = "COurse hasnt any materials";
        private const string success = "Success";

        public UserCourseMaterialService(
            IRepository<UserCourseMaterial> userCourseMaterialRepository,
            ICourseMaterialService courseMaterialService,
            ILogger<UserCourseMaterialService> logger,
            IOperationResult operationResult)
        {
            this.userCourseMaterialRepository = userCourseMaterialRepository;
            this.courseMaterialService = courseMaterialService;
            this.logger = logger;
            this.operationResult = operationResult;
        }

        public async Task<IOperationResult> AddMaterialsToUserCourse(int userCourseId, int courseId)
        {
            var materialsFromCourse = await this.courseMaterialService.GetAllMaterialsFromCourse(courseId);

            if (materialsFromCourse == null)
            {
                this.logger.LogInformation($"Course {courseId} hasn't any materials");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = courseHasntMaterials;

                return this.operationResult;
            }

            foreach (var material in materialsFromCourse)
            {
                UserCourseMaterial userCourseMaterial = new UserCourseMaterial()
                {
                    UserCourseId = userCourseId,
                    MaterialId = material.Id,
                    IsPassed = false,
                };

                await this.userCourseMaterialRepository.Add(userCourseMaterial);
            }

            this.operationResult.IsSucceed = true;
            this.operationResult.Message = success;
            await this.userCourseMaterialRepository.Save();

            return this.operationResult;
        }

        public async Task<bool> SetPassToMaterial(int userCourseId, int materialId)
        {
            var userCourseMaterialToUpdate = await this.userCourseMaterialRepository.GetOne(
                x => x.UserCourseId == userCourseId && x.MaterialId == materialId);

            if (userCourseMaterialToUpdate != null)
            {
                userCourseMaterialToUpdate.IsPassed = true;
                await this.userCourseMaterialRepository.Update(userCourseMaterialToUpdate);
                await this.userCourseMaterialRepository.Save();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Material>> GetNotPassedMaterialsFromCourseInProgress(int userCourseId)
        {
            bool userCourseMaterialExist = await this.userCourseMaterialRepository.Exist(x => x.UserCourseId == userCourseId);

            if (!userCourseMaterialExist)
            {
                return null;
            }

            return await this.userCourseMaterialRepository.Get<Material>(x => x.Material, x => x.UserCourseId == userCourseId && x.IsPassed == false);
        }

        public async Task<int> GetCountOfPassedMaterialsInCourse(int userCourseId)
        {
            return await this.userCourseMaterialRepository.GetCountWithPredicate(x => x.UserCourseId == userCourseId && x.IsPassed == true);
        }
    }
}
