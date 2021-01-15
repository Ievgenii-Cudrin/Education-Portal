using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogicLayer.InstanceCreator;

namespace BusinessLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        IRepository<Course> repository = ProviderServiceBLL.Provider.GetRequiredService<IRepository<Course>>();

        public bool CreateCourse(string name, string description)
        {
            //check name, may be we have this skill
            bool uniqueName = !repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            Course skill = uniqueName ? CourseInstanceCreator.CourseCreator(name, description) : null;

            if (skill != null)
            {
                repository.Create(skill);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateCourse(int id, string name, string description)
        {
            Course course = repository.Get(id);

            if (course == null)
            {
                return false;
            }
            else
            {
                course.Name = name;
                course.Description = description;
                repository.Update(course);
            }

            return true;
        }

        public IEnumerable<string> GetAllSkills()
        {
            return repository.GetAll().Select(n => n.Name);
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
