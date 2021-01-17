using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICourseService : IDeleteEntity
    {
        public bool CreateCourse(Course course);

        public bool UpdateCourse(Course course);

        public IEnumerable<Course> GetAllSkills();


    }
}
