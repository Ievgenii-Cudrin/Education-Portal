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

        public IEnumerable<Course> GetAllCourses();

        public bool AddMaterialToCourse(int id, Material material);

        public bool AddSkillToCourse(int id, Skill skillToAdd);

        public List<Skill> GetSkillsFromCourse(int id);

        public List<Material> GetMaterialsFromCourse(int id);
    }
}
