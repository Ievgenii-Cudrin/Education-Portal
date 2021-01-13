using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationPortalConsoleApp.InstanceCreator;

namespace EducationPortalConsoleApp.Services
{
    public class UserService
    {
        IUnitOfWork uow;
        User authorizedUser;
        public User AuthorizedUser
        {
            get
            {
                return authorizedUser;
            }
        }

        public UserService()
        {
            this.uow = new EFUnitOfWork();
        }

        public bool CreateUser(string name, string password, string email, string phoneNumber)
        {
            bool uniqueEmail = !uow.Users.GetAll().Any(x => x.Email.Equals(email));
            User user = uniqueEmail ? UserInstanceCreator.UserCreator(name, password, email, phoneNumber) : null;

            if (user != null)
                uow.Users.Create(user);
            else
                return false;

            return true;
        }

        public bool VerifyUser(string name, string password)
        {
            User user = uow.Users.GetAll().Where(x => x.Name == name && x.Password == password).FirstOrDefault();
            if (user == null)
                return false;
            else
            {
                authorizedUser = user;
                return true;
            }
        }

        public bool LogOut()
        {
            //Think about this method
            authorizedUser = null;
            return true;
        }

        public bool UpdateUser(int id, string name, string password, string email, string phoneNumber)
        {
            User user = uow.Users.Get(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.Name = name;
                user.Password = password;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                uow.Users.Update(user);
                return true;
            }
        }

        public IEnumerable<string> GetAllUsers()
        {
            return uow.Users.GetAll().Select(n => n.Name);
        }

        public bool DeleteUser(int id)
        {
            User user = uow.Users.Get(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                uow.Users.Delete(Convert.ToInt32(id));
                return true;
            }
        }
    }
}
