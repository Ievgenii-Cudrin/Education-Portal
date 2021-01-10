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
        IUnitOfWork _uow;
        User authorizedUser;
        public User AuthorizdUser
        {
            get
            {
                return authorizedUser;
            }
        }
        public UserService()
        {
            this._uow = new EFUnitOfWork();
        }
        public void StartWorkWithUser()
        {
            //UserConsoleMessageHelper.ShowTextForChoice();

            //string userChoice = Console.ReadLine();

            //switch (userChoice)
            //{
            //    case "1":
            //        CreateUser();
            //        break;
            //    case "2":
            //        UpdateUser();
            //        break;
            //    case "3":
            //        ShowAllUsers();
            //        break;
            //    case "4":
            //        DeleteUser();
            //        break;
            //    case "5":
            //        ProgramService.SelectEntityToWork();
            //        break;
            //    default:
            //        Console.WriteLine("Default case");
            //        break;
            //}
        }

        public bool CreateUser(string name, string password, string email, string phoneNumber)
        {
            bool success = true;
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
                _uow.Users.Create(user);
            else
                success = false;

            return success;

            //StartWorkWithUser();
        }

        public bool VerifyUser(string name, string password)
        {
            User user = _uow.Users.GetAll().Where(x => x.Name == name && x.Password == password).FirstOrDefault();
            if (user == null)
                return false;
            else
            {
                authorizedUser = user;
                return true;
            }
        }

        void UpdateUser()
        {
            Console.Write($"Enter user ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            User user = _uow.Users.Get(id);
            if (user == null)
            {
                Console.WriteLine($"\nUser not found");
            }
            else
            {
                //user.Name = GetDataHelper.GetNameFromUser();
                //user.Email = GetDataHelper.GetEmailFromUser();
                //user.PhoneNumber = GetDataHelper.GetPhoneNumberFromUser();

                _uow.Users.Update(user);
                Console.WriteLine("\nUser updated\n");
            }
            StartWorkWithUser();

        }

        void ShowAllUsers()
        {
            IEnumerable<User> users = _uow.Users.GetAll();
            //UserConsoleMessageHelper.ShowObjects(users);
            Console.WriteLine("");
            StartWorkWithUser();
        }

        void DeleteUser()
        {
            Console.Write($"Enter user ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            User user = _uow.Users.Get(id);
            if (user == null)
            {
                Console.WriteLine($"\nUser not found\n");
            }
            else
            {
                _uow.Users.Delete(Convert.ToInt32(id));
                Console.WriteLine("User deleted");
            }
            StartWorkWithUser();
        }
    }
}
