using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities;

namespace BusinessLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        IRepository<Course> courseRepository;
        IMaterialService materialService;
        ISkillService skillService;

        public CourseService(IRepository<Course> repository, IMaterialService materialService, ISkillService skillService)
        {
            this.courseRepository = repository;
            this.materialService = materialService;
            this.skillService = skillService;
        }

        public bool CreateCourse(Course course)
        {
            //check name, may be we have this skill
            bool uniqueName = course != null ? !courseRepository.GetAll().Any(x => x.Name.ToLower().Equals(course.Name.ToLower())) : false;

            //if name is unique => create new skill, otherwise skill == null
            if (uniqueName)
            {
                courseRepository.Create(course);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool AddMaterialToCourse(int id, Material material)
        {
            Course course = courseRepository.Get(id);

            if (course != null && material != null && !course.Materials.Any(x => x.Name.ToLower() == material.Name.ToLower()))
            {
                course.Materials.Add(material);
                courseRepository.Update(course);
                return true;
            }

            return false;
        }

        public bool AddSkillToCourse(int id, Skill skillToAdd)
        {
            //create skill
            skillService.CreateSkill(skillToAdd);
            //find this skill in db
            Skill skill = skillService.GetSkillByName(skillToAdd.Name);
            //find course
            Course course = courseRepository.Get(id);

            if(course != null)
            {
                //add skill and update
                course.Skills.Add(skill);
                courseRepository.Update(course);
                return true;
            }

            return false;
        }

        public bool UpdateCourse(Course courseToUpdate)
        {
            Course course = courseRepository.Get(courseToUpdate.Id);

            if (course == null)
            {
                return false;
            }
            else
            {
                course.Name = courseToUpdate.Name;
                course.Description = courseToUpdate.Description;
                courseRepository.Update(course);
            }

            return true;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courseRepository.GetAll();
        }

        public bool Delete(int id)
        {
            Course course = courseRepository.Get(id);

            if (course == null)
            {
                return false;
            }
            else
            {
                courseRepository.Delete(id);
            }

            return true;
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            var c = courseRepository.Get(id);
            List<Material> materials= courseRepository.Get(id).Materials;

            if(materials != null)
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
            var course = courseRepository.Get(id);
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
