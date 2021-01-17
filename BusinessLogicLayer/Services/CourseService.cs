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
        IRepository<Course> repository;

        public CourseService(IRepository<Course> repository)
        {
            this.repository = repository;
        }

        public bool CreateCourse(Course course)
        {
            //check name, may be we have this skill
            bool uniqueName = course != null ? !repository.GetAll().Any(x => x.Name.ToLower().Equals(course.Name.ToLower())) : false;

            //if name is unique => create new skill, otherwise skill == null
            if (uniqueName)
            {
                repository.Create(course);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateCourse(Course courseToUpdate)
        {
            Course course = repository.Get(courseToUpdate.Id);

            if (course == null)
            {
                return false;
            }
            else
            {
                course.Name = courseToUpdate.Name;
                course.Description = courseToUpdate.Description;
                repository.Update(course);
            }

            return true;
        }

        public IEnumerable<Course> GetAllSkills()
        {
            return repository.GetAll();
        }

        public bool Delete(int id)
        {
            Course course = repository.Get(id);

            if (course == null)
            {
                return false;
            }
            else
            {
                repository.Delete(id);
            }

            return true;
        }
    }
}
