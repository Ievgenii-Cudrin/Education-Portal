using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;

namespace EducationPortal.BLL.Services
{
    public class CourseMaterialService : ICourseMaterialService
    {
        private readonly IAuthorizedUser authorizedUser;
        private readonly IRepository<CourseMaterial> courseMaterialRepository;
        private readonly ILogger<CourseMaterialService> logger;

        public CourseMaterialService(
            IRepository<CourseMaterial> courseMatRepository,
            ILogger<CourseMaterialService> logger,
            IAuthorizedUser authorizedUser)
        {
            this.courseMaterialRepository = courseMatRepository;
            this.logger = logger;
            this.authorizedUser = authorizedUser;
        }

        public async Task<bool> AddMaterialToCourse(int courseId, int materialId)
        {
            try
            {
                if (await this.courseMaterialRepository.Exist(x => x.CourseId == courseId && x.MaterialId == materialId))
                {
                    return false;
                }

                var courseMaterial = new CourseMaterial()
                {
                    CourseId = courseId,
                    MaterialId = materialId,
                };

                await this.courseMaterialRepository.Add(courseMaterial);
                this.logger.LogDebug($"Adding material ({materialId}) to course ({courseId}) by user ({this.authorizedUser.User.Id})");
                return true;
            }
            catch (SqlException ex)
            {
                this.logger.LogWarning($"Failed to connect to db  due {ex.Message}");
                return false;
            }
        }

        public async Task<List<Material>> GetAllMaterialsFromCourse(int courseId)
        {
            return await this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId);
        }
    }
}
