namespace BusinessLogicLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DataAccessLayer.Entities;
    using Entities;

    public interface IUserService
    {
        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool AddCourseInProgress(int id);

        bool AddCourseToPassed(int id);

        bool AddSkill(Skill skill);

        bool UpdateValueOfPassMaterialInProgress(int courseId, int materialId);

        List<Course> GetListWithCoursesInProgress();

        List<Material> GetMaterialsFromCourseInProgress(int id);

        List<Skill> GetSkillsFromCourseInProgress(int id);

        List<Course> GetAvailableCoursesForUser();

        void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials);

        List<Skill> GetAllUserSkills();

        public bool ExistEmail(Expression<Func<User, bool>> predicat);

        bool Delete(int id);

        List<Course> GetAllPassedCourseFromUser();
    }
}
