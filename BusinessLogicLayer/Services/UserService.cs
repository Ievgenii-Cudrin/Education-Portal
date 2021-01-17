using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using EducationPortalConsoleApp.InstanceCreator;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

namespace EducationPortalConsoleApp.Services
{
    public class UserService : IUserService
    {
        IRepository<User> repository;
        User authorizedUser;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public User AuthorizedUser
        {
            get
            {
                return authorizedUser;
            }
        }

        public bool CreateUser(string name, string password, string email, string phoneNumber)
        {
            //check email, may be we have this email
            bool uniqueEmail = !repository.GetAll().Any(x => x.Email.ToLower().Equals(email.ToLower()));
            //if unique emaeil => create new user, otherwise user == null
            User user = uniqueEmail ? UserInstanceCreator.UserCreator(name, password, email, phoneNumber) : null;

            if (user != null)
            {
                repository.Create(user);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool VerifyUser(string name, string password)
        {
            User user = repository.GetAll().Where(x => x.Name == name && x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                authorizedUser = user;
            }

            return true;
        }

        public bool LogOut()
        {
            //Think about this method
            authorizedUser = null;
            return true;
        }

        public bool UpdateUser(int id, string name, string password, string email, string phoneNumber)
        {
            User user = repository.Get(id);

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
                repository.Update(user);
            }

            return true;
        }

        public Dictionary<int, string> GetAllUsers()
        {
            return repository.GetAll().ToDictionary(x => x.Id, x => x.Name);
        }

        public bool Delete(int id)
        {
            User user = repository.Get(id);

            if (user == null)
            {
                return false;
            }
            else
            {
                repository.Delete(Convert.ToInt32(id));
            }

            return true;
        }
    }
}
