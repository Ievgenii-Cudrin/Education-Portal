using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.InstanceCreator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Services
{
    public class UserService
    {
        IUnitOfWork _uow;
        public UserService()
        {
            this._uow = new EFUnitOfWork();
        }
        public void StartWorkWithUser()
        {
            UserConsoleMessageHelper.ShowTextForChoice();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    CreateUser();
                    break;
                case "2":
                    UpdateUser();
                    break;
                case "3":
                    ShowAllUsers();
                    break;
                case "4":
                    DeleteUser();
                    break;
                case "5":
                    ProgramService.SelectEntityToWork();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            void CreateUser()
            {
                _uow.Users.Create(UserInstanceCreator.UserCreator());
                Console.WriteLine("User created successfully! You can continue. \n");
                StartWorkWithUser();
            }

            void UpdateUser()
            {
                Console.Write($"Enter user ID to update: ");
                int id = Convert.ToInt32(Console.ReadLine());

                User user = _uow.Users.Get(id);
                if (user == null)
                {
                    Console.WriteLine($"User not found");
                }
                else
                {
                    user.Name = GetDataHelper.GetNameFromUser();
                    user.Email = GetDataHelper.GetEmailFromUser();
                    user.PhoneNumber = GetDataHelper.GetPhoneNumberFromUser();

                    _uow.Users.Update(user);
                    Console.WriteLine("User updated");
                }
                StartWorkWithUser();

            }

            void ShowAllUsers()
            {
                IEnumerable<User> users = _uow.Users.GetAll();
                UserConsoleMessageHelper.ShowObjects(users);
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
                    Console.WriteLine($"User not found");
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
}
