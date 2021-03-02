using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;

namespace EducationPortal.BLL.Services
{
    public class CourseMaterialService : ICourseMaterialService
    {
        private readonly IRepository<CourseMaterial> courseMaterialRepository;
        private static IBLLLogger logger;

        public CourseMaterialService(IRepository<CourseMaterial> courseMatRepository, IBLLLogger log)
        {
            logger = log;
            this.courseMaterialRepository = courseMatRepository;
        }

        public bool AddMaterialToCourse(int courseId, int materialId)
        {
            if (this.courseMaterialRepository.Exist(x => x.CourseId == courseId && x.MaterialId == materialId))
            {
                logger.Logger.Debug("CourseMaterial not exist - " + DateTime.Now);
                return false;
            }

            CourseMaterial courseMaterial = new CourseMaterial()
            {
                CourseId = courseId,
                MaterialId = materialId,
            };

            this.courseMaterialRepository.Add(courseMaterial);
            this.courseMaterialRepository.Save();
            return true;
        }

        public List<Material> GetAllMaterialsFromCourse(int courseId)
        {
            var a = this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId).ToList();
            return this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId).ToList();
        }
    }
}
