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
    }
}
