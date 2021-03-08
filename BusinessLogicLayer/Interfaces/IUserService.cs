namespace BusinessLogicLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;
    using Entities;

    public interface IUserService
    {
        Task<bool> CreateUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> AddCourseInProgress(int id);

        Task<bool> AddCourseToPassed(int id);

        Task<bool> AddSkill(Skill skill);

        Task<bool> UpdateValueOfPassMaterialInProgress(int courseId, int materialId);

        Task<IList<Course>> GetListWithCoursesInProgress();

        Task<IList<Material>> GetMaterialsFromCourseInProgress(int id);

        Task<IList<Skill>> GetSkillsFromCourseInProgress(int courseId);

        List<Course> GetAvailableCoursesForUser();

        void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials);

        Task<IList<Skill>> GetAllUserSkills();

        Task<bool> ExistEmail(Expression<Func<User, bool>> predicat);

        Task<bool> Delete(int id);

        Task<IList<Course>> GetAllPassedCourseFromUser();
    }
}
