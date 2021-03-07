using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool AddMaterialToCourse(int courseId, int materialId)
        {
            try
            {
                if (this.courseMaterialRepository.Exist(x => x.CourseId == courseId && x.MaterialId == materialId))
                {
                    return false;
                }

                var courseMaterial = new CourseMaterial()
                {
                    CourseId = courseId,
                    MaterialId = materialId,
                };

                this.courseMaterialRepository.Add(courseMaterial);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Material> GetAllMaterialsFromCourse(int courseId)
        {
            return this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId).ToList();
        }
    }
}
