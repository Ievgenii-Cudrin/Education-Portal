namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.Repositories;
    using Entities;

    public class CourseSqlService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly ISkillService skillService;

        public CourseSqlService(IEnumerable<IRepository<Course>> courseRepo, ISkillService skillService)
        {
            this.courseRepository = courseRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Course>));
            this.skillService = skillService;
        }

        public bool AddMaterialToCourse(int id, Material material)
        {
            throw new NotImplementedException();
        }

        public bool AddSkillToCourse(int id, Skill skillToAdd)
        {
            throw new NotImplementedException();
        }

        public bool CreateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetSkillsFromCourse(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
