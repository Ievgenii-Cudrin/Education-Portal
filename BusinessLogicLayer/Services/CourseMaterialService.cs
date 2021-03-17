using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.Services
{
    public class CourseMaterialService : ICourseMaterialService
    {
        private readonly IAuthorizedUser authorizedUser;
        private readonly IRepository<CourseMaterial> courseMaterialRepository;
        private readonly ILogger<CourseMaterialService> logger;
        private readonly IOperationResult operationResult;

        private const string success = "Success";
        private const string materialExistInCourse = "Material exist in course";
        private const string materialNotExistInCourse = "Material not exist in course";

        public CourseMaterialService(
            IRepository<CourseMaterial> courseMatRepository,
            ILogger<CourseMaterialService> logger,
            IAuthorizedUser authorizedUser,
            IOperationResult operationResult)
        {
            this.courseMaterialRepository = courseMatRepository;
            this.logger = logger;
            this.authorizedUser = authorizedUser;
            this.operationResult = operationResult;
        }

        public async Task<IOperationResult> AddMaterialToCourse(int courseId, int materialId)
        {
            if (await this.courseMaterialRepository.Exist(x => x.CourseId == courseId && x.MaterialId == materialId))
            {
                this.logger.LogWarning($"Material {materialId} not added to course {courseId} by user {this.authorizedUser.User}, material exist in course");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = materialExistInCourse;

                return this.operationResult;
            }

            var courseMaterial = new CourseMaterial()
            {
                CourseId = courseId,
                MaterialId = materialId,
            };

            await this.courseMaterialRepository.Add(courseMaterial);
            await this.courseMaterialRepository.Save();
            this.logger.LogDebug($"Adding material ({materialId}) to course ({courseId}) by user ({this.authorizedUser.User.Id})");
            this.operationResult.IsSucceed = true;
            this.operationResult.Message = success;

            return this.operationResult;
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsFromCourse(int courseId)
        {
            return await this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId);
        }

        public async Task<IOperationResult> DeleteMaterialFromCourse(int courseId, int materialId)
        {
            var courseMaterial = await this.courseMaterialRepository.GetOne(x => x.CourseId == courseId && x.MaterialId == materialId);

            if (courseMaterial != null)
            {
                await this.courseMaterialRepository.Delete(courseMaterial);
                await this.courseMaterialRepository.Save();
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
                this.logger.LogDebug($"Deleting material ({materialId}) from course ({courseId}) by user ({this.authorizedUser.User.Id})");
            }
            else
            {
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = materialNotExistInCourse;
                this.logger.LogError($"Deleting material ({materialId}) from course ({courseId}) not done by user ({this.authorizedUser.User.Id})");
            }

            return this.operationResult;
        }

        public async Task<int> GetCountOfMaterialInCourse(int courseId)
        {
            return await this.courseMaterialRepository.GetCountWithPredicate(x => x.CourseId == courseId);
        }
    }
}
