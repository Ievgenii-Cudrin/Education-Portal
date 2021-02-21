namespace EducationPortal.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
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

        public CourseMaterialSqlService(
            IEnumerable<IRepository<CourseMaterial>> courseMatRepository)
        {
            this.courseMaterialRepository = courseMatRepository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<CourseMaterial>));
        }

        public bool AddMaterialToCourse(int courseId, int materialId)
        {
            if (this.courseMaterialRepository.Exist(x => x.CourseId == courseId && x.MaterialId == materialId))
            {
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
            return this.courseMaterialRepository.Get<Material>(x => x.Material, x => x.CourseId == courseId).ToList();
        }
    }
}
