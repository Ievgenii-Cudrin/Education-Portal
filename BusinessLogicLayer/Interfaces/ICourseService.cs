namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;
    using Entities;

    public interface ICourseService
    {
        bool CreateCourse(Course course);

        bool UpdateCourse(Course course);

        bool AddMaterialToCourse(int courseId, Material material);

        bool AddSkillToCourse(int courseId, Skill skillToAdd);

        List<Skill> GetSkillsFromCourse(int courseId);

        List<Material> GetMaterialsFromCourse(int courseId);

        bool Delete(int courseId);

        bool ExistCourse(int courseId);

        List<Course> AvailableCourses(List<Course> courses);
    }
}
