using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Services
{
    public class ProgramService
    {
        IUnitOfWork _uow;
        public ProgramService(IUnitOfWork uow)
        {
            this._uow = uow;
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

            }

            void UpdateUser()
            {

            }

            void ShowAllUsers()
            {

            }

            void DeleteUser()
            {

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
                PhoneNumber = GetPhoneNumber()
            };



            string GetNameFromUser()
            {
                EnterName:
                Console.WriteLine($"Enter your name: ");
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
                Console.WriteLine($"Enter your name: ");
                string email = Console.ReadLine();

                //check this expression
                if (Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Incorrect email. Please, try again!");
                    goto EnterEmail;
                }
                return email;
            }

            string GetPhoneNumberFromUser()
            {
                EnterPhoneNumber:
                Console.WriteLine($"Enter your name: ");
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
