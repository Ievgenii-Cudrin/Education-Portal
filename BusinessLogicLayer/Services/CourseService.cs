namespace BusinessLogicLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using Entities;

    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly ISkillService skillService;

        public CourseService(IRepository<Course> repository, ISkillService skillService)
        {
            this.courseRepository = repository;
            this.skillService = skillService;
        }

        public bool CreateCourse(Course course)
        {
            bool uniqueName = course != null && !this.courseRepository.GetAll().Any(x => x.Name.ToLower().Equals(course.Name.ToLower()));

            if (uniqueName)
            {
                this.courseRepository.Create(course);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool AddMaterialToCourse(int id, Material material)
        {
            Course course = this.courseRepository.Get(id);

            if (course != null && material != null && !course.Materials.Any(x => x.Name.ToLower() == material.Name.ToLower()))
            {
                course.Materials.Add(material);
                this.courseRepository.Update(course);
                return true;
            }

            return false;
        }

        public bool AddSkillToCourse(int id, Skill skillToAdd)
        {
            this.skillService.CreateSkill(skillToAdd);
            Skill skill = this.skillService.GetSkillByName(skillToAdd.Name);
            Course course = this.courseRepository.Get(id);

            if (course != null)
            {
                course.Skills.Add(skill);
                this.courseRepository.Update(course);
                return true;
            }

            return false;
        }

        public bool UpdateCourse(Course courseToUpdate)
        {
            Course course = this.courseRepository.Get(courseToUpdate.Id);

            if (course == null)
            {
                return false;
            }
            else
            {
                course.Name = courseToUpdate.Name;
                course.Description = courseToUpdate.Description;
                this.courseRepository.Update(course);
            }

            return true;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return this.courseRepository.GetAll();
        }

        public bool Delete(int id)
        {
            Course course = this.courseRepository.Get(id);

            if (course == null)
            {
                return false;
            }
            else
            {
                this.courseRepository.Delete(id);
            }

            return true;
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            List<Material> materials = this.courseRepository.Get(id).Materials;

            if (materials != null)
            {
                return materials;
            }
            else
            {
                return null;
            }
        }

        public List<Skill> GetSkillsFromCourse(int id)
        {
            var course = this.courseRepository.Get(id);
            List<Skill> skills = course.Skills;

            if (skills != null)
            {
                return skills;
            }
            else
            {
                return null;
            }
        }
    }
}
