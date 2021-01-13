using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            bool success = true;
            bool requredPassword = uow.Users.GetAll().Any(x => x.Email.Equals(email));
            User user = null;
            if (name != null && password != null && email != null && phoneNumber != null)
            {
                user = new User()
                {
                    Name = name,
                    Password = password,
                    Email = email,
                    PhoneNumber = phoneNumber
                };
            }

            if (user != null)
                uow.Users.Create(user);
            else
                success = false;

            return success;
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

        void UpdateUser(int id, string name, string password, string email, string phoneNumber)
        {

            Console.Write($"Enter user ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            User user = uow.Users.Get(id);
            if (user == null)
            {
                Console.WriteLine($"\nUser not found");
            }
            else
            {
                //user.Name = GetDataHelper.GetNameFromUser();
                //user.Email = GetDataHelper.GetEmailFromUser();
                //user.PhoneNumber = GetDataHelper.GetPhoneNumberFromUser();

                uow.Users.Update(user);
                Console.WriteLine("\nUser updated\n");
            }
        }

        void ShowAllUsers()
        {
            IEnumerable<User> users = uow.Users.GetAll();
            Console.WriteLine("");
        }

        void DeleteUser()
        {
            Console.Write($"Enter user ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            User user = uow.Users.Get(id);
            if (user == null)
            {
                Console.WriteLine($"\nUser not found\n");
            }
            else
            {
                uow.Users.Delete(Convert.ToInt32(id));
                Console.WriteLine("User deleted");
            }
        }
    }
}
