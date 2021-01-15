using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDeleteEntity
    {
        public bool CreateUser(string name, string password, string email, string phoneNumber);

        public bool VerifyUser(string name, string password);

        public bool LogOut();

        public bool UpdateUser(int id, string name, string password, string email, string phoneNumber);

        public Dictionary<int, string> GetAllUsers();
    }
}
