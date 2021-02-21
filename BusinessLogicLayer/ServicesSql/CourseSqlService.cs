namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Comparers;
    using EducationPortal.Domain.Entities;
    using Entities;

    public class CourseSqlService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ICourseSkillService courseSkillService;
        private IMaterialService materialService;
        private ISkillService skillService;
        private ICourseComparerService courseComparerService;


        public CourseSqlService(
            IEnumerable<IRepository<Course>> courseRepo,
            ICourseMaterialService courseMaterialServ,
            ICourseSkillService courseSkillServ,
            IMaterialService materialService,
            ISkillService skillService,
            ICourseComparerService courseComparerService)
        {
            this.courseRepository = courseRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Course>));
            this.courseMaterialService = courseMaterialServ;
            this.courseSkillService = courseSkillServ;
            this.materialService = materialService;
            this.skillService = skillService;
            this.courseComparerService = courseComparerService;
        }

        public bool AddMaterialToCourse(int courseId, Material material)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) &&
                this.materialService.ExistMaterial(material.Id))
            {
                return this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
            }

            return false;
        }

        public bool AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) &&
                this.skillService.ExistSkill(skillToAdd.Id))
            {
                return this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
            }

            return false;
        }

        public bool CreateCourse(Course course)
        {
            bool uniqueCourse = course != null && !this.courseRepository.Exist(x => x.Name == course.Name);

            if (uniqueCourse)
            {
                this.courseRepository.Add(course);
                this.courseRepository.Save();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                this.courseRepository.Delete(id);
                this.courseRepository.Save();
                return true;
            }

            return false;
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                return this.courseMaterialService.GetAllMaterialsFromCourse(id)
                    .Except(this.materialService.GetAllNotPassedMaterialFromUser()).ToList();
            }

            return null;
        }

        public List<Skill> GetSkillsFromCourse(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                return this.courseSkillService.GetAllSkillsFromCourse(id);
            }

            return null;
        }

        public bool UpdateCourse(Course course)
        {
            if (this.courseRepository.Exist(x => x.Id == course.Id))
            {
                this.courseRepository.Update(course);
                this.courseRepository.Save();
                return true;
            }

            return false;
        }

        public bool ExistCourse(int courseId)
        {
            return this.courseRepository.Exist(x => x.Id == courseId);
        }

        public List<Course> AvailableCourses(List<Course> courses)
        {
            return this.courseRepository.Except(courses, this.courseComparerService.CourseComparer).ToList();
        }
    }
}
