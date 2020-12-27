using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

            User GetDataFromUser()
            {
                Console.WriteLine($"Enter your name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter your email adress: ");
                string email = Console.ReadLine();

                Console.WriteLine($"Enter your phone number: ");
                string phoneNumber = Console.ReadLine();
            }
        }
    }
}
