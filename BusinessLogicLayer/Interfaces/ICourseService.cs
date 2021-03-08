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

        Task<IList<Skill>> GetSkillsFromCourse(int courseId);

        Task<IList<Material>> GetMaterialsFromCourse(int courseId);

        Task<bool> Delete(int courseId);

        Task<bool> ExistCourse(int courseId);

        Task<IList<Course>> GetCoursesPerPage(int skip, int take);

        Task<int> GetCount();
    }
}
