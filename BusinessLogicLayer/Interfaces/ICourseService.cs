using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;
using Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICourseService
    {
        Task<IOperationResult> CreateCourse(Course course);

        Task<IOperationResult> UpdateCourse(Course course);

        Task<IOperationResult> AddMaterialToCourse(int courseId, Material material);

        Task<IOperationResult> AddSkillToCourse(int courseId, Skill skillToAdd);

        Task<IEnumerable<Skill>> GetSkillsFromCourse(int courseId);

        Task<IEnumerable<Material>> GetMaterialsFromCourse(int courseId);

        Task<IOperationResult> Delete(int courseId);

        Task<bool> ExistCourse(int courseId);

        Task<IEnumerable<Course>> GetCoursesPerPage(int skip, int take);

        Task<int> GetCount();

        Task<Course> GetCourse(int id);

        Task<int> GetLastId();
    }
}
