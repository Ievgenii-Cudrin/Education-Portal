using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using NLog;

namespace EducationPortal.BLL.Services
{
    public class CourseMaterialService : ICourseMaterialService
    {
        private readonly IRepository<CourseMaterial> courseMaterialRepository;

        public CourseMaterialService(IRepository<CourseMaterial> courseMatRepository)
        {
            this.courseMaterialRepository = courseMatRepository;
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<Material>> GetAllMaterialsFromCourse(int courseId)
        {
            return await this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId);
        }
    }
}
