namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;
    using Entities;

    public interface ICourseService
    {
        public bool CreateCourse(Course course);

        public bool UpdateCourse(Course course);

        public IEnumerable<Course> GetAllCourses();

        public bool AddMaterialToCourse(int id, Material material);

        public bool AddSkillToCourse(int id, Skill skillToAdd);

        public List<Skill> GetSkillsFromCourse(int id);

        public List<Material> GetMaterialsFromCourse(int id);

        public bool Delete(int id);
    }
}
