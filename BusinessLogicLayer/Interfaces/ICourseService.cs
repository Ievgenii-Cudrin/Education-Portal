namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;
    using Entities;

    public interface ICourseService
    {
        Task<bool> CreateCourse(Course course);

        Task<bool> UpdateCourse(Course course);

        Task<bool> AddMaterialToCourse(int courseId, Material material);

        Task<bool> AddSkillToCourse(int courseId, Skill skillToAdd);

        Task<List<Skill>> GetSkillsFromCourse(int courseId);

        Task<List<Material>> GetMaterialsFromCourse(int courseId);

        Task<bool> Delete(int courseId);

        Task<bool> ExistCourse(int courseId);

        Task<List<Course>> GetCoursesPerPage(int skip, int take);

        Task<int> GetCount();
    }
}
