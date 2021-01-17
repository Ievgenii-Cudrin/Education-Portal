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

        public bool CreateUser(User user)
        {
            //check email, may be we have this email
            bool uniqueEmail = user != null ? !repository.GetAll().Any(x => x.Email.ToLower().Equals(user.Email.ToLower())) : false;
            
            //if unique emaeil => create new user, otherwise user == null
            if (uniqueEmail)
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

        public bool UpdateUser(User userToUpdate)
        {
            User user = repository.Get(userToUpdate.Id);

            if (user == null)
            {
                return false;
            }
            else
            {
                user.Name = userToUpdate.Name;
                user.Password = userToUpdate.Password;
                user.Email = userToUpdate.Email;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                repository.Update(user);
            }

            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return repository.GetAll();
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
                repository.Delete(id);
            }

            return true;
        }
    }
}
