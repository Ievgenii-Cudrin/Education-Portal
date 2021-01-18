using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public bool AddVideoToCourse(int id, Video video)
        {
            bool success = materialService.CreateVideo(video);
            if (success)
            {
                Course course = courseRepository.Get(id);
                course.Materials.Add(video);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddBookToCourse(int id, Book book)
        {
            bool success = materialService.CreateBook(book);
            if (success)
            {
                Course course = courseRepository.Get(id);
                course.Materials.Add(book);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddArticleToCourse(int id, Article article)
        {
            bool success = materialService.CreateArticle(article);
            if (success)
            {
                Course course = courseRepository.Get(id);
                course.Materials.Add(article);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddSkillToCourse(int id, Skill skillToAdd)
        {
            bool skillExists = skillService.CreateSkill(skillToAdd);
            Course course = courseRepository.Get(id);

            if(course != null)
            {
                course.Skills.Add(skillToAdd);
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

        public IEnumerable<Course> GetAllSkills()
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
    }
}
