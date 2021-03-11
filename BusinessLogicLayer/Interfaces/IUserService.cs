using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> AddCourseInProgress(int id);

        Task<bool> AddCourseToPassed(int id);

        Task<bool> AddSkill(Skill skill);

        Task<bool> UpdateValueOfPassMaterialInProgress(int courseId, int materialId);

        Task<IEnumerable<Course>> GetListWithCoursesInProgress();

        Task<IEnumerable<Material>> GetMaterialsFromCourseInProgress(int id);

        Task<IEnumerable<Skill>> GetSkillsFromCourseInProgress(int courseId);

        List<Course> GetAvailableCoursesForUser();

        void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials);

        Task<IEnumerable<Skill>> GetAllUserSkills();

        Task<bool> ExistEmail(Expression<Func<User, bool>> predicat);

        Task<bool> Delete(int id);

        Task<IEnumerable<Course>> GetAllPassedCourseFromUser();

        Task<IEnumerable<Material>> GetAllNotPassedMaterialsInCourse(int courseId);

        Task AddSkills(List<Skill> skills);
    }
}
