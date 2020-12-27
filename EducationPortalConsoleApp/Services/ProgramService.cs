using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Services
{
    public class ProgramService
    {
        IUnitOfWork _uow;
        public ProgramService()
        {
            this._uow = new EFUnitOfWork();
        }
        public void StartApp()
        {
            ShowTextForChoice();

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
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            void CreateUser()
            {
                _uow.Users.Create(GenerateObjectFromUser());
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
                    user.Name = GetNameFromUser();
                    user.Email = GetEmailFromUser();
                    user.PhoneNumber = GetPhoneNumberFromUser();

                    _uow.Users.Update(user);
                }

            }

            void ShowAllUsers()
            {
                IEnumerable<User> users = _uow.Users.GetAll();
                foreach(var user in users)
                {
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Phone number: {user.PhoneNumber}");
                    Console.WriteLine("---------------------------");
                }
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
                    _uow.Users.Delete(user.Id);
                }
            }

            void ShowTextForChoice()
            {
                Console.WriteLine($"Hi, dear user. Please, make your choice: " +
                $"\n1.Create user" +
                $"\n2.Update user" +
                $"\n3.Show all users" +
                $"\n4.Delete user");
            }

            User GenerateObjectFromUser() => new User()
            {
                Name = GetNameFromUser(),
                Email = GetEmailFromUser(),
                PhoneNumber = GetPhoneNumberFromUser()
            };



            string GetNameFromUser()
            {
                EnterName:
                Console.WriteLine($"Enter name: ");
                string name = Console.ReadLine();

                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name must not be empty. Please, try again!");
                    goto EnterName;
                }
                return name;
            }

            string GetEmailFromUser()
            {
                EnterEmail:
                Console.WriteLine($"Enter email: ");
                string email = Console.ReadLine();

                //check this expression
                if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Incorrect email. Please, try again!");
                    goto EnterEmail;
                }
                return email;
            }

            string GetPhoneNumberFromUser()
            {
                EnterPhoneNumber:
                Console.WriteLine($"Enter phone number: ");
                string phoneNumber = Console.ReadLine();

                if (Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Incorrect phone number. Please, try again!");
                    goto EnterPhoneNumber;
                }
                return phoneNumber;
            }
            


        }
    }
}
