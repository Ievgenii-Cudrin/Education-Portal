namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;
    using Entities;

    public interface ICourseService
    {
        bool CreateCourse(Course course);

        bool UpdateCourse(Course course);

        IEnumerable<Course> GetAllCourses();

        bool AddMaterialToCourse(int id, Material material);

        bool AddSkillToCourse(int id, Skill skillToAdd);

        List<Skill> GetSkillsFromCourse(int id);

        List<Material> GetMaterialsFromCourse(int id);

        bool Delete(int id);
    }
}
