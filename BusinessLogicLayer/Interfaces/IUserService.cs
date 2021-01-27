using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDeleteEntity
    {
        public User AuthorizedUser { get; }
        public bool CreateUser(User user);

        public bool VerifyUser(string name, string password);

        public bool LogOut();

        public bool UpdateUser(User user);

        public IEnumerable<User> GetAllUsers();

        public bool AddCourseInProgress(int id);

        public bool DeleteCourseFromProgress(int id);

        public bool AddCourseToPassed(int id);

        public bool AddSkill(Skill skill);

        public bool UpdateValueOfPassMaterialInProgress(int courseId, int materialId);

        public List<Course> GetListWithCoursesInProgress();

        public List<Material> GetMaterialsFromCourseInProgress(int id);

        public List<Skill> GetSkillsFromCourseInProgress(int id);

        public List<Course> GetAvailableCoursesForUser();

        public void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials);

        public List<Skill> GetAllUserSkills();
    }
}
