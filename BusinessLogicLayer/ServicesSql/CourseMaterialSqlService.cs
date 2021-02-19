namespace EducationPortal.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Services;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;

    public class CourseMaterialSqlService : ICourseMaterialService
    {
        private readonly IRepository<CourseMaterial> courseMaterialRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Material> materialRepository;

        public CourseMaterialSqlService(
            IEnumerable<IRepository<CourseMaterial>> courseMatRepository,
            IEnumerable<IRepository<Material>> materialRepo,
            IEnumerable<IRepository<Course>> courseRepo
            )
        {
            this.courseMaterialRepository = courseMatRepository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<CourseMaterial>));
            this.courseRepository = courseRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Course>));
            this.materialRepository = materialRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Material>));
        }

        public bool AddMaterialToCourse(int courseId, int materialId)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) && this.materialRepository.Exist(x => x.Id == materialId))
            {
                CourseMaterial courseMaterial = new CourseMaterial()
                {
                    CourseId = courseId,
                    MaterialId = materialId,
                };

                this.courseMaterialRepository.Add(courseMaterial);
                this.courseMaterialRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
