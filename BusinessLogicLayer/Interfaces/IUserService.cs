using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDeleteEntity
    {
        public bool CreateUser(User user);

        public bool VerifyUser(string name, string password);

        public bool LogOut();

        public bool UpdateUser(User user);

        public IEnumerable<User> GetAllUsers();

        public bool AddCourseInProgress(int id);

        public bool DeleteCourseFromProgress(int id);

        public bool AddCourseToPassed(int id);

        public bool AddSkill(Skill skill);
    }
}
